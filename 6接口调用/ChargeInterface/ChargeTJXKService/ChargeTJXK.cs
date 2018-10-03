using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ChargeInterface.ChargeTJXKService
{
    /// <summary>
    /// 天津胸科，计费接口；由贾晓宇实现
    /// </summary>
    public class ChargeTJXK : AbstractCharge
    {
        /// <summary>
        /// 计费接口（舱内扫描程序调用）
        /// 0 计费不成功
        /// 1 计费成功
        /// msg 失败原因
        /// </summary>
        /// <param name="labelno"></param>
        /// <param name="UserID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override string Charge(string labelno, string UserID, out string msg)
        {
            msg = "计费出错";
            string begintime = "";
            string endtime = "";
            string reqMsgSerial = "";//发送方报文流水号

            try
            {
                StringBuilder selsql = new StringBuilder();

                String sql1 = "select * from V_drugCharge "
                    + " where LabelNo = '" + labelno + "'"
                    + " select * from DEmployee where DEmployeeID='" + UserID + "'"
                    + "select distinct  3 NUM from IVRecord "
                    + "inner join  Prescription on IVRecord.PrescriptionID = Prescription.PrescriptionID "
                    + "inner join PrescriptionDetail on Prescription.PrescriptionID=PrescriptionDetail.PrescriptionID "
                    + "where LabelNo ='" + labelno + "'";

                String sql2 = "select * from V_itemCharge"
                    + " where LabelNo = '" + labelno + "'"
                    + " select * from DEmployee where DEmployeeID='" + UserID + "'"
                    + "select distinct  3 NUM from IVRecord "
                    + "inner join  Prescription on IVRecord.PrescriptionID = Prescription.PrescriptionID "
                    + "inner join PrescriptionDetail on Prescription.PrescriptionID=PrescriptionDetail.PrescriptionID "
                    + "where LabelNo ='" + labelno + "'";

                using (DataSet ds = dbHelp.GetPIVAsDB(sql1))
                {
                    if (ds == null || ds.Tables[0].Rows.Count <= 0 || ds.Tables[1].Rows.Count <= 0)
                    {
                        msg = "数据为空，无法计费。未进入his接口";
                        string errsql = "INSERT INTO ToHisChargeLog(LabelNo,ChargeResult,HisReturn,Msg,Remark2) VALUES ('" + labelno + "','" + -1 + "','" + 0 + "','" + msg + "','" + UserID + "') ";
                        dbHelp.GetPIVAsDB(errsql);
                        dbHelp.SetPIVAsDB("update ivrecord set labelover='-3',labeloverid='" + UserID + "',labelovertime='" + DateTime.Now + "' where labelno='" + labelno + "'");

                        return "0";
                    }
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["PStatus"].ToString()) == 4)
                    {
                        msg = "医嘱已停。未进入his接口";
                        dbHelp.SetPIVAsDB("update ivrecord set labelover='-3',labeloverid='" + UserID + "',labelovertime='" + DateTime.Now + "' where labelno='" + labelno + "' and remark3<>15");
                        string errsql = "INSERT INTO ToHisChargeLog(LabelNo,ChargeResult,HisReturn,Msg,Remark2) VALUES ('" + labelno + "','" + -1 + "','" + 0 + "','" + msg + "','" + UserID + "') ";
                        dbHelp.GetPIVAsDB(errsql);
                        return "0";
                    }
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["result"].ToString()) == 15)
                    {
                        msg = "已计费";
                        return "1";
                    }
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["LabelOver"].ToString()) < 0)
                    {
                        msg = "配置取消";
                        return "0";
                    }
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["IVStatus"].ToString()) > 9)
                    {
                        msg = "已配置核对";
                        return "1";
                    }

                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["PackAdvance"].ToString()) == 1)
                    {
                        msg = "提前打包";
                        return "0";
                    }
                    DataSet ds2 = dbHelp.GetPIVAsDB(sql2);

                    for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                    {
                        string drug = ds2.Tables[0].Rows[i]["drug_code"].ToString();
                        if (drug.Trim() == "" || drug.Trim().Equals("") || drug == null)
                        {
                            ds2.Tables[0].Rows[i].Delete();
                        }
                    }

                    ds2.AcceptChanges();
                    String XML;
                    if (ds.Tables[0].Rows[0]["Batch"].ToString().Contains("K"))
                    {
                        XML = WriteXml.SetXmlStr(ds);
                    }
                    else if (ds2 == null || ds2.Tables[0].Rows.Count <= 0 || ds2.Tables[1].Rows.Count <= 0)
                    {
                        XML = WriteXml.SetXmlStr(ds);
                    }
                    else
                    {
                        XML = WriteXml.set(ds, ds2);
                    }

                    string mystr = XML.Replace("\"", "'");
                    string chargePayLoads = MakeHeader.makeRequestPayLoad("xmldata", mystr.Trim());

                    string chargeJSONs = MakeHeader.requestHeader("HIS06010", chargePayLoads);
                    //发送给HIS数据

                    //hisService his = new hisService();
                    string url = "http://192.168.200.23:8081/pivasGather/webservice/hisService";
                    string[] args = new string[1];
                    args[0] = chargeJSONs;

                    String result = ServiceTest.InvokeWebService(url, "invoke", args).ToString();

                    begintime = DateTime.Now.ToString();

                    //处理HIS数据
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    rows = MakeJson.JsonToDataTable(result, out reqMsgSerial);

                    string PZcode = ds.Tables[1].Rows[0]["DEmployeeCode"].ToString();//配置人

                    //药品计费返回信息和返回数值
                    msg = rows[0]["execTxt"].ToString();//msg 全局变量
                    string s = rows[0]["execVal"].ToString();//s发送信息后his返回值 0,-1,1

                    //更新瓶签信息sql语句
                    string sql = "update ivrecord set remark3='{0}' where labelno  ='" + labelno + "'";
                    sql += " update IVRecordDetail set ReturnFromHis='{1}' where ivrecordid in (select ivrecordid from ivrecord where labelno='" + labelno + "')";
                    endtime = DateTime.Now.ToString();
                    if (s == "1")
                    {
                        dbHelp.GetPIVAsDB(string.Format(sql, "15", "1"));
                        String logsql = "INSERT INTO ToHisChargeLog VALUES ('" + begintime + "','" + endtime + "','" + labelno + "','" + "null" + "','" + 1 + "','" + s + "'," + "'" + msg + "'," + "null" + ",'" + UserID + "',null" + ",null) ";
                        dbHelp.GetPIVAsDB(logsql);
                        return "1";
                    }
                    else
                    {
                        if (msg.Contains("医嘱") || msg.Contains("病人"))
                        {
                            string updatesql = "update  Prescription  set PStatus  =4 "
                                + " where  GroupNo  in("
                                + " select distinct  GroupNo from IVRecord  "
                                + "  where LabelNo ='{0}') ";
                            dbHelp.SetPIVAsDB(string.Format(updatesql, labelno));
                        }
                        dbHelp.SetPIVAsDB("update ivrecord set labelover='-3',labeloverid='" + UserID + "',labelovertime='" + DateTime.Now + "' where labelno='" + labelno + "'");
                        dbHelp.GetPIVAsDB(string.Format(sql, "12", "0"));
                        String logsql = "INSERT INTO ToHisChargeLog VALUES ('" + begintime + "','" + endtime + "','" + labelno + "','" + "null" + "','" + 0 + "','" + s + "'," + "'" + msg + "'," + "null" + ",'" + UserID + "',null" + ",null) ";
                        dbHelp.GetPIVAsDB(logsql);
                        return "0";
                    }
                }
            }
            catch (Exception ex)
            {
                //  msg = "计费出错,请确定是否连接到his接口,并确保传参是否正确";
                string errsql = "INSERT INTO ToHisChargeLog(LabelNo,ChargeResult,HisReturn,Msg,Remark2) VALUES ('" + labelno + "','" + -1 + "','" + 0 + "','" + ex.Message + "','" + UserID + "') ";
                dbHelp.GetPIVAsDB(errsql);
                return "0";
            }
        }

        /// <summary>
        /// 审方通过调用
        /// 0 不成功
        /// 1 成功
        /// 不管返回成功 不成功，程序都暂不做任何处理。只会将MSG报出来。
        /// </summary>
        /// <param name="GroupNo">处方组号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override string PivasRevPreTrue(string GroupNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        /// <summary>
        /// 审方不通过调用
        /// 0 不成功
        /// 1 成功
        /// 不管返回成功 不成功，程序都暂不做任何处理。只会将MSG报出来。
        /// </summary>
        /// <param name="GroupNo">处方组号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override string PivasRevPreFalse(string GroupNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        /// <summary>
        /// 打印计费按钮调用
        /// </summary>
        /// <param name="LabelNos"></param>
        /// <param name="DEmployeeID">计费人ID</param>
        /// <returns></returns>
        public override bool PrintCharge(List<string> LabelNos, string DEmployeeID)
        {
            return true;
        }

        /// <summary>
        /// 排药核对
        /// 0 不成功
        /// 1 成功
        /// 若不成功，程序将终止后续操作
        /// </summary>
        /// <param name="LabelNo">瓶签号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override string ScanPY5(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        /// <summary>
        /// 进仓核对
        /// 0 不成功
        /// 1 成功
        /// 若不成功，程序将终止后续操作
        /// </summary>
        /// <param name="LabelNo">瓶签号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override string ScanJC7(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        /// <summary>
        /// 配置核对（可通过此方法调用舱内扫描计费接口）
        /// 0 不成功
        /// 1 成功
        /// 若不成功，程序将终止后续操作
        /// </summary>
        /// <param name="LabelNo">瓶签号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override string ScanPZ9(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        /// <summary>
        /// 出仓核对
        /// 0 不成功
        /// 1 成功
        /// 若不成功，程序将终止后续操作
        /// </summary>
        /// <param name="LabelNo">瓶签号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override string ScanCC11(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        /// <summary>
        /// 打包核对
        /// 0 不成功
        /// 1 成功
        /// 若不成功，程序将终止后续操作
        /// </summary>
        /// <param name="LabelNo">瓶签号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override string ScanDB13(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        /// <summary>
        /// 病区签收
        /// 0 不成功
        /// 1 成功
        /// 若不成功，程序将终止后续操作
        /// </summary>
        /// <param name="LabelNo">瓶签号</param>
        /// <param name="DEmployeeID">操作者</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override string ScanQS15(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        /// <summary>
        /// 更改ScreenDeatils表的状态
        /// </summary>
        /// <param name="LabelNo">瓶签号</param>
        /// <param name="DEmployeeID">员工号</param>
        /// <param name="Status">状态 0：未知；1：核对已成功</param>
        public override void ChangeStatus(string LabelNo, string DEmployeeID, string MoxaIP, string port, int Status)
        {
            //db.GetPIVAsDB("update ScreenDetail set Result='1',Msg='核对成功' where MoxaIP='" + LvS[i].MoxaIP + "' and port='" + LvS[i].MoxaPort + "'");
        }
    }
}
