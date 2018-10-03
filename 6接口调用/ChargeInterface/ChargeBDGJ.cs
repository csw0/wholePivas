using PIVAsCommon;
using System;
using System.Collections.Generic;
using System.Data;

namespace ChargeInterface
{
    /// <summary>
    /// 北大国际计费接口
    /// </summary>
    public class ChargeBDGJ : AbstractCharge
    {
        /// <summary>
        /// 主要是更新IVRecord表中remark3(标记是否已成功计过费)和labelover(标记是否被各种情况取消，0为正常)，
        /// 然后插入计费记录表ToHisChargeLog
        /// </summary>
        /// <param name="labelno"></param>
        /// <param name="UserID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override string Charge(string labelno, string UserID, out string msg)
        {
            string str13;
            string str16;
            string str6 = "0";
            msg = string.Empty;
            string str = null;
            string str2 = null;
            try
            {
                DataSet dsTVRecord = this.dbHelp.GetPIVAsDB(string.Format("select * from IVRecord where LabelNo = {0}", labelno));
                string str3 = dsTVRecord.Tables[0].Rows[0]["GroupNo"].ToString().Trim();
                string str4 = dsTVRecord.Tables[0].Rows[0]["InfusionDT"].ToString().Trim();
                string str5 = DateTime.Now.ToString();
                string sqlStr = "select DEmployeeCode from DEmployee where DEmployeeID='" + UserID + " '";
                string format = ("update ivrecord set remark3='{0}' where labelno='" + labelno + "' ") + "update IVRecordDetail set ReturnFromHis='{1}' where ivrecordid in (select ivrecordid from ivrecord where labelno='" + labelno + "')";
                string str9 = this.dbHelp.GetPIVAsDB(sqlStr).Tables[0].Rows[0]["DEmployeeCode"].ToString();
                string str11 = ("Groupno='" + str3 + "'  occdt='" + str4 + "'  usercode='" + str9 + "'").Replace("'", "");
                if (dsTVRecord.Tables[0].Rows[0]["Remark3"].ToString().Trim() == "15")
                {
                    msg = "已成功计费";
                    str6 = "2";
                    str13 = DateTime.Now.ToString();
                    this.dbHelp.GetPIVAsDB("insert into ToHisChargeLog (begintime,endtime,labelno,msg,ChargeResult,Parameters,Remark2) values ('" + str5 + "','" + str13 + "','" + labelno + "','" + msg + "','" + str6 + "' ,'" + str11 + "','" + str9 + "' )");
                    return "1";
                }

                if (Convert.ToInt32(dsTVRecord.Tables[0].Rows[0]["labelover"].ToString()) < 0)
                {
                    msg = "已配置取消";
                    str6 = "3";
                    str13 = DateTime.Now.ToString();
                    this.dbHelp.GetPIVAsDB("insert into ToHisChargeLog (begintime,endtime,labelno,msg,ChargeResult,Parameters,Remark2) values ('" + str5 + "','" + str13 + "','" + labelno + "','" + msg + "','" + str6 + "' ,'" + str11 + "','" + str9 + "' )");
                    return "0";
                }

                DataSet pIVAsHISDBSQL = new DataSet();
                pIVAsHISDBSQL = this.dbHelp.GetPIVAsHISDBSQL("exec dbo.proc_pivas_charge_revoke '" + str3 + "'");

                if (pIVAsHISDBSQL.Tables[0].Rows.Count > 0)
                {
                    string str14 = pIVAsHISDBSQL.Tables[0].Rows[0]["ret"].ToString().Trim();
                    string str15 = pIVAsHISDBSQL.Tables[0].Rows[0]["msg"].ToString().Trim();

                    InternalLogger.Log.Debug(String.Format("根据GroupNo{0}执行HIS存储过程proc_pivas_charge_revoke返回值{0},消息{1}",
                        str3, str14, str15));

                    if (str15.Contains("已经撤销"))
                    {
                        str6 = "0";
                        str13 = DateTime.Now.ToString();
                        this.dbHelp.SetPIVAsDB(string.Format(format, 12, 1));
                        str16 = "update ivrecord set  labelover=-3,labelOverTime='{0}',labeloverID='{1}' where labelno= {2}";
                        this.dbHelp.SetPIVAsDB(string.Format(str16, DateTime.Now, UserID, labelno));
                        this.dbHelp.GetPIVAsDB("insert into ToHisChargeLog (begintime,endtime,labelno,msg,ChargeResult,Parameters,HisReturn,Remark2) values ('" + str5 + "','" + str13 + "','" + labelno + "','" + str15 + "','" + str6 + "' ,'" + str11 + "','" + str6 + "','" + str9 + "' )");
                        return "0";
                    }
                }
            
                DataSet set4 = new DataSet();
                set4 = this.dbHelp.GetPIVAsHISDBSQL("exec dbo.proc_pivas_charge '" + str3 + "','" + str4 + "','" + str9 + "'");
                str13 = DateTime.Now.ToString();
                str = set4.Tables[0].Rows[0]["ret"].ToString().Trim();
                str2 = set4.Tables[0].Rows[0]["msg"].ToString().Trim();

                InternalLogger.Log.Debug(String.Format("执行HIS存储过程proc_pivas_charge返回值{0},消息{1}", str, str2));

                if (str == "1")
                {
                    str6 = "1";
                    this.dbHelp.SetPIVAsDB(string.Format(format, 15, 1));
                }
                else
                {
                    str6 = "0";
                    this.dbHelp.SetPIVAsDB(string.Format(format, 12, 1));
                    str16 = "update ivrecord set  labelover=-3,labelOverTime='{0}',labeloverID='{1}' where labelno= {2}";
                    this.dbHelp.SetPIVAsDB(string.Format(str16, DateTime.Now, UserID, labelno));
                }
                msg = str2;
                this.dbHelp.GetPIVAsDB("insert into ToHisChargeLog (begintime,endtime,labelno,msg,ChargeResult,Parameters,HisReturn,Remark2) values ('" + str5 + "','" + str13 + "','" + labelno + "','" + msg + "','" + str6 + "' ,'" + str11 + "','" + str + "','" + str9 + "' )");
            }
            catch (Exception ex)
            {
                msg = String.Format("计费接口异常({0}),计费失败", ex.Message);
                str6 = "0";
            }
            return str6;
        }

        #region 重写计费接口
        /// <summary>
        /// 陈松伟重写计费接口
        /// </summary>
        /// <param name="labelno"></param>
        /// <param name="UserID"></param>
        /// <param name="msg"></param>
        /// <returns>"0"=计费失败;"1"=计费成功</returns>
        public string ChargeEX(string labelno, string UserID, out string msg)
        {
            msg = string.Empty;
            try
            {
                DataSet dsIVRecord = this.dbHelp.GetPIVAsDB(string.Format("select * from IVRecord where LabelNo = {0}", labelno));
                if (!CheckDataSet(dsIVRecord))
                {
                    msg = "瓶签尚未生成，计费失败";
                    return "0";
                }

                if (dsIVRecord.Tables[0].Rows[0]["Remark3"].ToString().Trim() == "15")
                {
                    msg = "重复操作，已成功计过费";
                    return "1";
                }

                int labelover = Convert.ToInt32(dsIVRecord.Tables[0].Rows[0]["labelover"].ToString());
                if (labelover < 0)
                {
                    switch (labelover)
                    {
                        case -1: msg = "护士工作站取消,计费失败"; break;
                        case -2: msg = "系统取消,计费失败"; break;
                        case -3: msg = "收费处取消,计费失败"; break;
                        case -4: msg = "瓶签查询处取消,计费失败"; break;
                        default:
                            break;
                    }
                    return "0";
                }

                //查询his，判断是否已撤销
                string strGroupNo = dsIVRecord.Tables[0].Rows[0]["GroupNo"].ToString().Trim();//处方组号
                DataSet dsChargeRevoke = this.dbHelp.GetPIVAsHISDBSQL("exec dbo.proc_pivas_charge_revoke '" + strGroupNo + "'");
                if (CheckDataSet(dsChargeRevoke))
                {
                    if (dsChargeRevoke.Tables[0].Rows[0]["msg"].ToString().Trim().Contains("已经撤销"))
                    {
                        UpdateIVRecordDetail(1, labelno);
                        UpdateIVRecord(12, -3, UserID, labelno);
                        msg = "调用HIS返回，收费处取消,计费失败";
                        return "0";
                    }
                }

                //调用his接口，进行计费
                string strInfusionDT = dsIVRecord.Tables[0].Rows[0]["InfusionDT"].ToString().Trim();//输液日期
                DataSet dsDEmployee = this.dbHelp.GetPIVAsDB("select DEmployeeCode from DEmployee where DEmployeeID='" + UserID + " '");
                if (!CheckDataSet(dsDEmployee))
                {
                    msg = "员工未注册，计费失败";
                    return "0";
                }
                string strDEmployeeCode = dsDEmployee.Tables[0].Rows[0]["DEmployeeCode"].ToString();//员工工号
                DataSet dsCharge = this.dbHelp.GetPIVAsHISDBSQL("exec dbo.proc_pivas_charge '" + strGroupNo + "','" + strInfusionDT + "','" + strDEmployeeCode + "'");
                if (CheckDataSet(dsCharge))
                {
                    msg = dsCharge.Tables[0].Rows[0]["msg"].ToString().Trim();
                    UpdateIVRecordDetail(1, labelno);
                    if (dsCharge.Tables[0].Rows[0]["ret"].ToString().Trim() == "1")
                    {
                        this.dbHelp.SetPIVAsDB(string.Format("update ivrecord set remark3='{0}' where labelno= {3}",
                            15, labelno));
                        return "1";//计费成功，更新瓶签表
                    }
                    else
                    {
                        this.dbHelp.SetPIVAsDB(string.Format("update ivrecord set remark3='{0}',labeloverID='{1}' where labelno= '{2}'",
                            12, UserID, labelno));
                        return "0";//计费失败，更新瓶签表
                    }
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("计费接口出错：" + ex.Message);
                msg = "计费接口异常，计费失败";
            }
            return "0";//默认计费失败
        }

        /// <summary>
        /// 对DataSet校验；存在表且表中存在行数据
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>返回是否可用</returns>
        private bool CheckDataSet(DataSet ds)
        {
            try
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    return true;
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("DataSet校验出错：" + ex.Message);
            }
            return false;
        }

        private void UpdateIVRecord(int remark3, int labelover, string labeloverID, string labelno)
        {
            string strSQL =
                string.Format("update ivrecord set remark3='{0}',labelover='{1}',labeloverID='{2}' where labelno= '{3}'",
                remark3, labelover, labeloverID, labelno);
            this.dbHelp.SetPIVAsDB(strSQL);
        }
        private void UpdateIVRecordDetail(int ReturnFromHis, string labelno)
        {
            string strSQL =
                string.Format("update IVRecordDetail set ReturnFromHis='{0}' where ivrecordid in (select ivrecordid from ivrecord where labelno='{1}')",
                ReturnFromHis, labelno);

            this.dbHelp.SetPIVAsDB(strSQL);
        }
        #endregion

        public override string PivasRevPreFalse(string GroupNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        public override string PivasRevPreTrue(string GroupNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

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

        /// <summary>
        /// 与Charge方法完全一样
        /// </summary>
        /// <param name="labelno"></param>
        /// <param name="UserID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override string ScanPZ9(string labelno, string UserID, out string msg)
        {
            string str13;
            string str16;
            msg = string.Empty;
            string str = null;
            string str2 = null;
            DataSet pIVAsDB = this.dbHelp.GetPIVAsDB(string.Format("select * from IVRecord where LabelNo = {0}", labelno));
            string str3 = pIVAsDB.Tables[0].Rows[0]["GroupNo"].ToString().Trim();
            string str4 = pIVAsDB.Tables[0].Rows[0]["InfusionDT"].ToString().Trim();
            string str5 = DateTime.Now.ToString();
            string str6 = "";
            string sqlStr = "select DEmployeeCode from DEmployee where DEmployeeID='" + UserID + " '";
            string format = ("update ivrecord set remark3='{0}' where labelno='" + labelno + "' ") + "update IVRecordDetail set ReturnFromHis='{1}' where ivrecordid in (select ivrecordid from ivrecord where labelno='" + labelno + "')";
            string str9 = this.dbHelp.GetPIVAsDB(sqlStr).Tables[0].Rows[0]["DEmployeeCode"].ToString();
            string str11 = ("Groupno='" + str3 + "'  occdt='" + str4 + "'  usercode='" + str9 + "'").Replace("'", "");
            if (pIVAsDB.Tables[0].Rows[0]["Remark3"].ToString().Trim() == "15")
            {
                msg = "已成功计费";
                str6 = "2";
                str13 = DateTime.Now.ToString();
                this.dbHelp.GetPIVAsDB("insert into ToHisChargeLog (begintime,endtime,labelno,msg,ChargeResult,Parameters,Remark2) values ('" + str5 + "','" + str13 + "','" + labelno + "','" + msg + "','" + str6 + "' ,'" + str11 + "','" + str9 + "' )");
                return "1";
            }
            if (Convert.ToInt32(pIVAsDB.Tables[0].Rows[0]["labelover"].ToString()) < 0)
            {
                msg = "已配置取消";
                str6 = "3";
                str13 = DateTime.Now.ToString();
                this.dbHelp.GetPIVAsDB("insert into ToHisChargeLog (begintime,endtime,labelno,msg,ChargeResult,Parameters,Remark2) values ('" + str5 + "','" + str13 + "','" + labelno + "','" + msg + "','" + str6 + "' ,'" + str11 + "','" + str9 + "' )");
                return "0";
            }
            DataSet pIVAsHISDBSQL = new DataSet();
            pIVAsHISDBSQL = this.dbHelp.GetPIVAsHISDBSQL("exec dbo.proc_pivas_charge_revoke '" + str3 + "'");
            if (pIVAsHISDBSQL.Tables[0].Rows.Count > 0)
            {
                string str14 = pIVAsHISDBSQL.Tables[0].Rows[0]["ret"].ToString().Trim();
                string str15 = pIVAsHISDBSQL.Tables[0].Rows[0]["msg"].ToString().Trim();
                if (str15.Contains("已经撤销"))
                {
                    str6 = "0";
                    str13 = DateTime.Now.ToString();
                    this.dbHelp.SetPIVAsDB(string.Format(format, 12, 1));
                    str16 = "update ivrecord set  labelover=-3,labelOverTime='{0}',labeloverID='{1}' where labelno= {2}";
                    this.dbHelp.SetPIVAsDB(string.Format(str16, DateTime.Now, UserID, labelno));
                    this.dbHelp.GetPIVAsDB("insert into ToHisChargeLog (begintime,endtime,labelno,msg,ChargeResult,Parameters,HisReturn,Remark2) values ('" + str5 + "','" + str13 + "','" + labelno + "','" + str15 + "','" + str6 + "' ,'" + str11 + "','" + str6 + "','" + str9 + "' )");
                    return "0";
                }
            }
            try
            {
                DataSet set4 = new DataSet();
                set4 = this.dbHelp.GetPIVAsHISDBSQL("exec dbo.proc_pivas_charge '" + str3 + "','" + str4 + "','" + str9 + "'");
                str13 = DateTime.Now.ToString();
                str = set4.Tables[0].Rows[0]["ret"].ToString().Trim();
                str2 = set4.Tables[0].Rows[0]["msg"].ToString().Trim();
                if (str == "1")
                {
                    str6 = "1";
                    this.dbHelp.SetPIVAsDB(string.Format(format, 15, 1));
                }
                else
                {
                    str6 = "0";
                    this.dbHelp.SetPIVAsDB(string.Format(format, 12, 1));
                    str16 = "update ivrecord set  labelover=-3,labelOverTime='{0}',labeloverID='{1}' where labelno= {2}";
                    this.dbHelp.SetPIVAsDB(string.Format(str16, DateTime.Now, UserID, labelno));
                }
                msg = str2;
                this.dbHelp.GetPIVAsDB("insert into ToHisChargeLog (begintime,endtime,labelno,msg,ChargeResult,Parameters,HisReturn,Remark2) values ('" + str5 + "','" + str13 + "','" + labelno + "','" + msg + "','" + str6 + "' ,'" + str11 + "','" + str + "','" + str9 + "' )");
            }
            catch (Exception exception)
            {
                msg = "pivas:" + exception.Message;
                str6 = "0";
            }
            return str6;
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
