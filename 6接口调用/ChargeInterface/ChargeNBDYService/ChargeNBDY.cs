using PIVAsCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using WebSynInterface.WebReference;

namespace ChargeInterface.ChargeNBDYService
{
    /// <summary>
    /// 宁波第一医院计费接口
    /// </summary>
    public class ChargeNBDY : AbstractCharge
    {
        private EsbBusService esbBusService = new EsbBusService();

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
            msg = string.Empty;
            try
            {           
                string str = "";
                string str2 = "select Batch,Remark6,LabelOver,Remark3 from ivrecord ";
                str2 = (str2 + " where labelno = '" + labelno + "'") + " select AccountID from DEmployee where DEmployeeID='" + UserID + "'";
                string format = ("update ivrecord set remark3='{0}' where labelno='" + labelno + "'") +
                    " update IVRecordDetail set ReturnFromHis='{1}' where ivrecordid in (select ivrecordid from ivrecord where labelno='"
                    + labelno + "')";
                string str7 = "";
                string str8 = "";
                string str9 = "0";
                string str10 = "";

                DataSet pIVAsDB = dbHelp.GetPIVAsDB(str2);
                if (pIVAsDB == null || pIVAsDB.Tables.Count <= 0 || pIVAsDB.Tables[0].Rows.Count <= 0)
                {
                    msg = "无计费信息";
                    return "0";
                }
                string str3 = pIVAsDB.Tables[0].Rows[0]["Batch"].ToString().Trim();
                string str4 = pIVAsDB.Tables[0].Rows[0]["Remark6"].ToString().Trim();
                string str5 = pIVAsDB.Tables[1].Rows[0]["AccountID"].ToString().Trim();

                if (pIVAsDB.Tables[0].Rows[0]["Remark3"].ToString().Trim() == "15")
                {
                    msg = "已成功计费";
                    return "1";
                }

                if (Convert.ToInt32(pIVAsDB.Tables[0].Rows[0]["LabelOver"].ToString()) < 0)
                {
                    msg = "已配置取消";
                    return "0";
                }
               
                if (str3.Contains("#"))
                {
                    str10 = DateTime.Now.ToString();
                }
                string xmlString = "";               
                xmlString = ((xmlString + "<body><head><userid>JPZX</userid><password>123</password><trans_no>C07_01_00_02</trans_no></head>") + 
                    "<resquest><C07_01_00_02><TXM>" + str4 + "</TXM>") + "<FYRY></FYRY><FYSJ></FYSJ>" + "<PYHDSJ></PYHDSJ><PYRY></PYRY>";
                xmlString = xmlString + "<TPHDRY>" + str5 + "</TPHDRY><TPHDSJ>" + str10 + " </TPHDSJ>";
                xmlString = (xmlString + "<TPRY>" + str5 + "</TPRY><TPSJ> " + str10 + " </TPSJ>") + "<ZT>2</ZT></C07_01_00_02></resquest></body>";
                try
                {
                    string str12 = DateTime.Now.AddDays(-1.0).ToString("yyyy-MM-dd");
                    string str13 = DateTime.Now.ToString("yyyy-MM-dd");
                    string str14 = "<body><head><userid>JPZX</userid><password>123</password><trans_no>C07_01_00_01</trans_no></head>";
                    str14 = (str14 + "<resquest><RQ1>" + str12 + "</RQ1><RQ2>" + str13 + "</RQ2>") + "<TXM>" + str4 + 
                        "</TXM><C07_01_00_01></C07_01_00_01></resquest></body>";
                    str7 = DateTime.Now.ToString();
                    InternalLogger.Log.Info("开始调用his计费接口");
                    StringReader reader = new StringReader(esbBusService.Process(str14));
                    str8 = DateTime.Now.ToString();
                    DataSet set2 = new DataSet();
                    set2.ReadXml(reader);
                    DataTable table = set2.Tables[1];
                    str = table.Rows[0]["STATUS"].ToString();
                    string str20 = str.Trim();
                    if (str20 == null)
                    {
                        goto Label_052C;
                    }
                    InternalLogger.Log.Info("调用his计费接口后STATUS的值是(0计费成功，非0失败)："+ str20);
                    if (!(str20 == "0"))
                    {
                        if (str20 == "1")
                        {
                            goto Label_044C;
                        }
                        if (str20 == "2")
                        {
                            goto Label_047C;
                        }
                        if (str20 == "4")
                        {
                            goto Label_04AC;
                        }
                        if (str20 == "5")
                        {
                            goto Label_04BF;
                        }
                        goto Label_052C;
                    }
                    msg = "计费成功";
                    str9 = "1";
                    InternalLogger.Log.Info("调用his计费接口后STATUS的值" + str20 + "，更新瓶签状态为计费成功并返回1");

                    this.dbHelp.SetPIVAsDB(string.Format(format, "15", "3"));
                    StringReader reader2 = new StringReader(this.esbBusService.Process(xmlString));
                    DataSet set3 = new DataSet();
                    set3.ReadXml(reader2);
                    DataTable table2 = set3.Tables[0];
                    string str17 = table2.Rows[0]["ret_code"].ToString();
                    string str18 = table2.Rows[0]["ret_info"].ToString();
                    goto Label_0559;
                    Label_044C:
                    msg = "计费失败";
                    str9 = "0";
                    dbHelp.SetPIVAsDB(string.Format(format, "12", "0"));
                    goto Label_0559;
                    Label_047C:
                    msg = "已计费";
                    str9 = "1";
                    dbHelp.SetPIVAsDB(string.Format(format, "16", "5"));
                    goto Label_0559;
                    Label_04AC:
                    msg = "已打包";
                    str9 = "1";
                    goto Label_0559;
                    Label_04BF:
                    msg = "医嘱已停";
                    str9 = "0";
                    dbHelp.SetPIVAsDB(string.Format(format, "12", "5"));
                    dbHelp.SetPIVAsDB("update IVRecord set LabelOver='-2' ,LabelOverID='" + UserID + 
                        "',LabelOverTime=GETDATE() where LabelNo='" + labelno + "' ");
                    goto Label_0559;
                    Label_052C:
                    msg = "其他原因";
                    str9 = "0";
                    dbHelp.SetPIVAsDB(string.Format(format, "20", "0"));
                    Label_0559:;
                    dbHelp.GetPIVAsDB("INSERT INTO ToHisChargeLog  VALUES ('" + str7 + "','" + str8 + "','" + 
                        labelno + "','" + str14 + "','" + str9 + "','" + str + "','" + msg + "','" + UserID + "','" + str4 + "','" 
                        + xmlString + "',null) ");
                    return str9;
                }
                catch (Exception)
                {
                    dbHelp.SetPIVAsDB("INSERT INTO ToHisChargeLog  VALUES ('" + str7 + "','" + str8 + "','" + labelno + "','" + 
                        str4 + "','" + str9 + "','" + str + "','" + msg + "','" + UserID + "',null,null,null ) ");
                    return "0";
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("调用计费接口出错：" + ex.Message);
                msg = "调用计费出错";
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
            msg = string.Empty;
            string str = "";
            string str2 = "select remark6 from ivrecord ";
            str2 = (str2 + " where LabelNo = '" + LabelNo + "'") + " select AccountID from DEmployee where DEmployeeID='" + DEmployeeID + "'";
            DataSet pIVAsDB = dbHelp.GetPIVAsDB(str2);
            if (pIVAsDB == null)
            {
                msg = "无打包信息";
                return "0";
            }
            string str3 = pIVAsDB.Tables[0].Rows[0]["remark6"].ToString().Trim();
            string str4 = pIVAsDB.Tables[1].Rows[0]["AccountID"].ToString().Trim();
            string str5 = "";
            string str6 = "";
            string str7 = "";
            try
            {
                string xmlString = "<?xmlversion=\"1.0\"encoding=\"utf-8\"?>";
                xmlString = ((((xmlString + "<body><head><userid>test</userid><password>123</password><trans_no>C07.01.00.02</trans_no></head>") 
                    + "<resquest><TEST>" + LabelNo + "<TXM></TXM><DYSJ><!--打印时间--></DYSJ><DYSX><!--DYSX--></DYSX>") 
                    + "<FYRY><!--发药人员--></FYRY><FYSJ><!--发药时间--></FYSJ>") + "<PYHDSJ><!—排药时间--></PYHDSJ><PYRY><!—排药人员--></PYRY>" 
                    + "<TPHDRY><!--调配核对人员--></TPHDRY><TPHDSJ><!--调配核对时间--></TPHDSJ>") 
                    + "<TPRY><!--调配人员--></TPRY><TPSJ><!--调配时间--></TPSJ>" 
                    + "<TQRY><!--提取人员--></TQRY><TQSJ><!--提取时间--></TQSJ><ZT><!--状态--></ZT></TEST></resquest></body>";
                str5 = DateTime.Now.ToString();
                StringReader reader = new StringReader(this.esbBusService.Process(xmlString));
                str6 = DateTime.Now.ToString();
                DataSet set2 = new DataSet();
                set2.ReadXml(reader);
                DataTable table = set2.Tables[0];
                str = table.Rows[0]["ret_code"].ToString();
                string str10 = table.Rows[0]["ret_info"].ToString();
                string str12 = str.Trim();
                if ((str12 != null) && (str12 == "0"))
                {
                    msg = "计费成功";
                    str7 = "1";
                }
                else
                {
                    msg = "打包失败";
                    str7 = "0";
                }
                return str7;
            }
            catch (Exception)
            {
                return "0";
            }
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
        public override void ChangeStatus(string LabelNo,string DEmployeeID,string MoxaIP,string port,int Status)
        {
            //db.GetPIVAsDB("update ScreenDetail set Result='1',Msg='核对成功' where MoxaIP='" + LvS[i].MoxaIP + "' and port='" + LvS[i].MoxaPort + "'");
        }

        private DataTable GetDataTable(XmlElement doc)
        {
            XmlNodeList list = doc.SelectNodes("//table");
            DataTable table = new DataTable();
            for (int i = 0; i < list.Count; i++)
            {
                DataRow row = table.NewRow();
                XmlElement element = (XmlElement)list.Item(i);
                int index = 0;
                while (index < element.Attributes.Count)
                {
                    if (!table.Columns.Contains("@" + element.Attributes[index].Name))
                    {
                        table.Columns.Add("@" + element.Attributes[index].Name);
                    }
                    row["@" + element.Attributes[index].Name] = element.Attributes[index].Value;
                    index++;
                }
                for (index = 0; index < element.ChildNodes.Count; index++)
                {
                    if (!table.Columns.Contains(element.ChildNodes.Item(index).Name))
                    {
                        table.Columns.Add(element.ChildNodes.Item(index).Name);
                    }
                    row[element.ChildNodes.Item(index).Name] = element.ChildNodes.Item(index).InnerText;
                }
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
