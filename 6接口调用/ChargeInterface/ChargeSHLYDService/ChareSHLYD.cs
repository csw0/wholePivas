using PIVAsCommon;
using PIVAsCommon.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Xml;

namespace ChargeInterface.ChargeSHLYDService
{
    /// <summary>
    /// 上海六院东，计费接口
    /// </summary>
    public class ChareSHLYD : AbstractCharge
    {
        //两个webservice。17年3月his切换，两个his并行，所以计费分别调用两个web；
        //两个服务接口相同，仅仅服务所在地址不同，配置不同ip和端口即可，计费接口将只调用一个
        SPDWebService hisService = new SPDWebService();

        public override string Charge(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = string.Empty;
            string hisret = "";
            string hismsg = "";
            string ret = "0";
            string selsql = "select u.GroupNo,u.schedule,p.CaseID,iv.IVStatus,iv.remark6,iv.Remark3,iv.LabelOver from UseDrugList U ";
            selsql += " inner join IVRecord iv on iv.DrugLGroupNo = U.DrugLGroupNo";
            selsql += " inner join Prescription p on iv.GroupNo=p.GroupNo";
            selsql += " where iv.LabelNo = '" + LabelNo + "'";
            string selsql2 = "select AccountID CZYGH,DemployeeName CZYXM from DEmployee where DEmployeeID='" + DEmployeeID + "'";

            DataSet Lds2 = dbHelp.GetPIVAsDB(selsql2);
            string CZYGH = Lds2.Tables[0].Rows[0]["CZYGH"].ToString().Trim();//操作员工号
            string CZYXM = Lds2.Tables[0].Rows[0]["CZYXM"].ToString().Trim();//操作员姓名

            DataSet Lds = dbHelp.GetPIVAsDB(selsql);
            string IVStatus = Lds.Tables[0].Rows[0]["IVStatus"].ToString().Trim();
            string remark3Value = Lds.Tables[0].Rows[0]["Remark3"].ToString().Trim();
            string labelOver = Lds.Tables[0].Rows[0]["LabelOver"].ToString().Trim();

            string sql = "update ivrecord set remark3='{0}' where labelno='" + LabelNo + "'";
            sql += " update IVRecordDetail set ReturnFromHis='{1}' where IVRecordid in (select IVRecordid from ivrecord where labelno='"
                + LabelNo + "')";
            string begintime = "";
            string endtime = "";

            if (Lds == null)
            {
                msg = "无计费信息";
                return ret;
            }
            else if (int.Parse(IVStatus) >= 9 && int.Parse(remark3Value) == 15)
            {
                msg = "已经计费成功";
                return "1";
            }

            if (int.Parse(labelOver) != 0)
            {
                msg = ChargeResult_Const.MANUAL_CANCEL;
                return "0";             
            }
            InternalLogger.Log.Debug("上海六院东调用计费服务开始~~~");
            try
            {
                if (Lds.Tables[0].Rows.Count > 1)
                    InternalLogger.Log.Warn(String.Format("瓶签号{0}有{1}个药，可能造成计费慢"));

                for (int i = 0; i < Lds.Tables[0].Rows.Count; i++)
                {
                    string hisxmlcharg = "";

                    string GroupNo = Lds.Tables[0].Rows[i]["GroupNo"].ToString().Trim();
                    string hisCode = Lds.Tables[0].Rows[i]["schedule"].ToString().Trim();
                    string schedule = hisCode.Split('_')[0].ToString();//SPDID
                    string DrugListID = hisCode.Split('_')[1].ToString();//hisID
                    string CaseID = Lds.Tables[0].Rows[i]["CaseID"].ToString().Trim();

                    begintime = DateTime.Now.ToString(); ///////开始时间
                    hisxmlcharg += "<DOC>";
                    hisxmlcharg += "<HEADER DOC_CODE=\"10001\" UPLOAD_ORG_CODE=\"\" DOC_TYPE=\"89\" TOTAL_RECORDS=\"2\" CREATE_TIME=\"" 
                    + begintime + "\" />";
                    hisxmlcharg += "<TRANS SPDXH = \"" + schedule + "\"  CaseID=\"" + CaseID + "\" CZYGH =\"" + CZYGH + "\"  CZYXM =\""
                    + CZYXM + "\" YZH=\"" + GroupNo + "\" BZ =\"" + "1" + "\" LYXH=\"" + DrugListID + "\" />";
                    hisxmlcharg += "</DOC>";

                    string hisresult = string.Empty;
                    bool bTimeOut = false;
                    try
                    {
                        CallWithTimeout((() => { hisresult = hisService.DataExchange(hisxmlcharg); }), 5000);
                    }
                    catch (TimeoutException )
                    {
                        InternalLogger.Log.Error("因HIS计费接口调用一次超过5秒，直接退出，并认为计费失败,亮红灯");
                        msg = "HIS计费接口调用超过5秒,计费失败";
                        ret = "0";
                        bTimeOut = true;
                    }
                    if (!bTimeOut)
                    {
                        XmlDocument myXDoc = new XmlDocument();
                        myXDoc.LoadXml(hisresult);
                        XmlNode noder = myXDoc.SelectSingleNode("/DOC/HEADER");

                        hisret = noder.Attributes["SUCCEED"].Value;
                        hismsg = noder.Attributes["MESSAGE"].Value;

                        endtime = DateTime.Now.ToString();//结束时间 
                        switch (hisret.Trim())
                        {
                            case "1"://计费成功
                                msg = "计费成功";
                                ret = "1";
                                dbHelp.SetPIVAsDB(string.Format(sql, "15", "3"));
                                dbHelp.SetPIVAsDB("update IVRecord set remark3='15'  where labelno='" + LabelNo + "'");
                                break;

                            case "2"://医嘱已停
                                msg = "医嘱已停";
                                ret = "0";
                                dbHelp.SetPIVAsDB(string.Format(sql, "12", "0"));
                                dbHelp.SetPIVAsDB("update IVRecord set LabelOver=-3 , remark3='12',LabelOverID='" + DEmployeeID +
                                    "',LabelOverTime ='" + begintime + "' where labelno='" + LabelNo + "'");
                                break;

                            case "3": //库存不足
                                msg = "库存不足";
                                ret = "0";
                                dbHelp.SetPIVAsDB(string.Format(sql, "18", "5"));
                                break;

                            case "0"://计费失败
                                msg = "计费失败";
                                ret = "0";
                                dbHelp.SetPIVAsDB(string.Format(sql, "12", "3"));
                                break;

                            default://其他
                                msg = "其他原因";
                                ret = "0";
                                dbHelp.SetPIVAsDB(string.Format(sql, "20", "0"));
                                break;
                        }
                    }
                   
                    dbHelp.GetPIVAsDB("INSERT INTO ToHisChargeLog (begintime,endtime,LabelNo,[Parameters],ChargeResult,HisReturn,msg) "+
                        "VALUES ('" + begintime + "','" + endtime + "','" + LabelNo + "','" + hisxmlcharg + "','" + ret + "','" 
                        + hisret + "','" + hisresult + "')");
                    InternalLogger.Log.Debug("上海六院东调用计费服务结束~~~");
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("上海六院东调用计费服务出错~~~" + ex.Message);
                return "0";
            }
            return ret;
        }

        /// <summary>
        /// 超时退出，扔超时异常
        /// </summary>
        /// <param name="action">方法</param>
        /// <param name="timeoutMilliseconds">毫秒</param>
        private void CallWithTimeout(Action action, int timeoutMilliseconds)
        {
            Thread threadToKill = null;
            Action wrappedAction = () =>
            {
                threadToKill = Thread.CurrentThread;
                action();
            };

            IAsyncResult result = wrappedAction.BeginInvoke(null, null);
            if (result.AsyncWaitHandle.WaitOne(timeoutMilliseconds))
            {
                wrappedAction.EndInvoke(result);
            }
            else
            {
                threadToKill.Abort();
                throw new TimeoutException();
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

        public override string ScanPY5(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        public override string ScanJC7(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        public override string ScanPZ9(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        public override string ScanCC11(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        public override string ScanDB13(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        public override string ScanQS15(string LabelNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }
    }
}
