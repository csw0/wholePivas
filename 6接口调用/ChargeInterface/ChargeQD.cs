using ChargeInterface.PivasCharge;
using PIVAsCommon;
using PIVAsCommon.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace ChargeInterface
{
    /// <summary>
    /// 启东计费接口
    /// </summary>
    public class ChargeQD : AbstractCharge
    {
        public override int BackPre(string p_group_no, string prm_EXEC_DOCTOR, string prm_jujueyy, out string PRM_DATABUFFER, out string PRM_APPCODE)
        {
            PRM_DATABUFFER = string.Empty;
            PRM_APPCODE = string.Empty;
            OleDbConnection connection = new OleDbConnection("Provider=MSDAORA.1;Password=his3;User ID=his3;Data Source=zsk;   Persist Security Info=True");
            OleDbCommand command = new OleDbCommand();
            try
            {
                int num;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "pkg_sy_jizhang.zy_ShenHeZT";
                OleDbParameter[] parameterArray = new OleDbParameter[] { new OleDbParameter("p_group_no", OleDbType.VarChar, 50), new OleDbParameter("prm_EXEC_DOCTOR", OleDbType.VarChar, 500), new OleDbParameter("prm_jujueyy", OleDbType.VarChar, 500), new OleDbParameter("PRM_DATABUFFER", OleDbType.VarChar, 50), new OleDbParameter("PRM_APPCODE", OleDbType.VarChar, 200) };
                for (num = 0; num < (parameterArray.Length - 2); num++)
                {
                    parameterArray[num].Direction = ParameterDirection.Input;
                }
                parameterArray[3].Direction = ParameterDirection.Output;
                parameterArray[4].Direction = ParameterDirection.Output;
                parameterArray[0].Value = p_group_no;
                parameterArray[1].Value = prm_EXEC_DOCTOR;
                parameterArray[2].Value = prm_jujueyy;
                for (num = 0; num < parameterArray.Length; num++)
                {
                    command.Parameters.Add(parameterArray[num]);
                }
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                command.Connection = connection;
                command.ExecuteNonQuery();
                PRM_DATABUFFER = parameterArray[3].Value.ToString();
                PRM_APPCODE = parameterArray[4].Value.ToString();
            }
            catch (Exception exception)
            {
                PRM_DATABUFFER = "pivas:" + exception.Message;
                return 0;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Dispose();
                command.Parameters.Clear();
                command.Dispose();
            }
            return 1;
        }

        public override string Charge(string LabelNo, string DEmployeeID, out string msg)
        {
            string str2;
            string str15;
            string str = DateTime.Now.ToString();
            try
            {
                string strUpdateIVRecord = ("update IVRecord set Remark3='{0}' where LabelNo='" + LabelNo + "' ")
                    + "update IVRecordDetail set ReturnFromHis='{1}' where IVRecordID in " +
                    "(select IVRecordID from IVRecord where LabelNo='" + LabelNo + "')";
                string str4 = "select iv.GroupNo,iv.InfusionDT,iv.Remark3,iv.PackAdvance,iv.LabelOver,"+
                    "p.PStatus from IVRecord iv inner join Prescription p on p.PrescriptionID =iv.PrescriptionID "+
                    "where iv.LabelNo ='{0}'";

                //在调用HIS计费接口前，通过pivas数据库判断是否还需要调接口
                DataSet dsBeforeOfCharge = this.dbHelp.GetPIVAsDB(string.Format(str4, LabelNo));
                string groupno = dsBeforeOfCharge.Tables[0].Rows[0]["groupno"].ToString();
                string infusionDT = dsBeforeOfCharge.Tables[0].Rows[0]["InfusionDT"].ToString();
                string str7 = dsBeforeOfCharge.Tables[0].Rows[0]["Remark3"].ToString();
                string str8 = dsBeforeOfCharge.Tables[0].Rows[0]["PackAdvance"].ToString();
                string str9 = dsBeforeOfCharge.Tables[0].Rows[0]["LabelOver"].ToString();
                string str10 = dsBeforeOfCharge.Tables[0].Rows[0]["PStatus"].ToString();

                #region 三个if判断，是否可调用计费接口
                if (Convert.ToInt32(str7) == 15)//已通过其他流程计费
                {
                    if (Convert.ToInt32(str8) == 1)//若已提前打包/空包，则在病区冲配。
                    {
                        msg = ChargeResult_Const.PACKADVANCE_WARDCOMPOUND;
                        return "0";
                    }
                    msg = ChargeResult_Const.OTHERWAY_SUCCESS;
                    return "1";
                }
                if (Convert.ToInt32(str10) == 4)//医嘱已停
                {
                    msg = ChargeResult_Const.PRESCRIPTION_STOP;
                    dbHelp.SetPIVAsDB(string.Format("update IVRecord set LabelOver={0},LabelOverID='{1}',LabelOverTime='{2}' where LabelNo='{3}'", 
                        -3, DEmployeeID, DateTime.Now, LabelNo));
                    return "0";
                }
                if (Convert.ToInt32(str9) < 0)//瓶签已打印到瓶签配置扫描这段时间，被各种情况造成瓶签配置取消
                {
                    msg = ChargeResult_Const.MANUAL_CANCEL;
                    return "0";
                }
                #endregion

                #region 调HIS计费接口
                string sqlStr = " select DEmployeeCode  from DEmployee  where DEmployeeID = '" + DEmployeeID + "'";
                string userCode = this.dbHelp.GetPIVAsDB(sqlStr).Tables[0].Rows[0][0].ToString();
           
                string strHisRtnValue,strHisRtnMsg = string.Empty;
                PivasWebServiceSoapClient serviceClient = new PivasWebServiceSoapClient();
                serviceClient.Charge(groupno, infusionDT, userCode, out strHisRtnMsg, out strHisRtnValue);
                serviceClient.Abort();

                str15 = DateTime.Now.ToString();
                msg = strHisRtnMsg;
                str2 = strHisRtnValue;
                string str16 = groupno + "||" + infusionDT + "||" + userCode;
                #endregion

                if (strHisRtnMsg.Contains("重复"))//his对重复计费，返回值有可能不是“1”，这里做了保护
                    strHisRtnValue = "1";
                this.dbHelp.GetPIVAsDB("insert into ToHisChargeLog (begintime,endtime,labelno,msg,ChargeResult,Parameters,HisReturn,"+
                    "Remark2) values ('" + str + "','" + str15 + "','" + LabelNo + "','" + strHisRtnMsg + "','" + strHisRtnValue + 
                    "' ,'" + str16 + "','" + str2 + "','" + userCode + "' )");

                #region 调用HIS计费接口后的数据更新
                if (strHisRtnValue.Trim().Equals("1"))//启东返回值目前枚举0：异常，1：计费成功，-1：重复或失败，-2：医嘱停止，-5：库存不存在
                {
                    if (Convert.ToInt32(str8) == 1)
                    {
                        //只计费，不配置液体。但在配置核对的页面显示时，怎么显示？
                        msg = ChargeResult_Const.PACKADVANCE_WARDCOMPOUND; 
                        this.dbHelp.SetPIVAsDB(string.Format(strUpdateIVRecord, 15,str2));
                        return "0";
                    }
                    msg = ChargeResult_Const.CHARGE_SUCCESS;
                    dbHelp.SetPIVAsDB(string.Format(strUpdateIVRecord, 15,str2));
                    return "1";
                }

                //0是属于异常，可能是网络或his短时故障，所以不等于0时才已明确配置取消
                //含"ORA"的返回值为-1，但返回值为-1的还要重复情况。这里将-5的情况都不取消
                if (strHisRtnValue.Trim().Equals("0")|| strHisRtnValue.Trim().Equals("-5"))
                {
                    return "0";//Msg默认是his返回的
                }
                else
                {
                    dbHelp.SetPIVAsDB(string.Format(strUpdateIVRecord, 12, str2));
                    //HIS计费失败可能多种原因，都统一更新labelover值
                    dbHelp.SetPIVAsDB(string.Format("update IVRecord set LabelOver={0},LabelOverID='{1}',LabelOverTime='{2}' where LabelNo='{3}'",
                        -3, DEmployeeID, DateTime.Now, LabelNo));
                    return "0";
                }
                #endregion
            }
            catch (Exception exception)
            {
                str2 = "0";
                str15 = DateTime.Now.ToString();
                msg = string.Format("计费接口异常({0}),计费失败", exception.Message);
                InternalLogger.Log.Error(msg);
                dbHelp.SetPIVAsDB(string.Concat(new object[] { "INSERT INTO ToHisChargeLog(BeginTime,EndTime,LabelNo,ChargeResult,HisReturn,msg,remark2) VALUES ('", str, "','", str15, "','", LabelNo, "','", 0, "','", str2, "','", msg, "') " }));
                return "0";
            }
        }

        public override string PivasRevPreFalse(string GroupNo, string DEmployeeID, out string msg)
        {
            msg = "退药操作";
            string str = "";
            string str2 = "";
            string str3 = "";
            string format = "update Prescription  set PStatus =1 where GroupNo ='{0}'";
            string str5 = "update Prescription  set PStatus =3 where GroupNo ='{0}'";
            string str6 = " select top 1 CensorItem ,  Description  from CPResultRG  where CheckResultID =0 and PrescriptionID  =(select distinct PrescriptionID  from Prescription  where GroupNo ='{0}') order by insertdt desc";
            DataSet pIVAsDB = this.dbHelp.GetPIVAsDB(string.Format(str6, GroupNo));
            str = pIVAsDB.Tables[0].Rows[0]["Description"].ToString();
            string str7 = pIVAsDB.Tables[0].Rows[0]["CensorItem"].ToString();

            PivasWebServiceSoapClient serviceClient = new PivasWebServiceSoapClient();
            serviceClient.BackPre(GroupNo, str7, str, out str3, out str2);
            serviceClient.Abort();

            if ((str2 == "1") || (str3 == "1"))
            {
                msg = "退方成功";
                this.dbHelp.GetPIVAsDB(string.Format(str5, GroupNo));
                string sqlStr = "update ivrecord set labelover=-3 ,labelovertime=getdate() where groupno='" + GroupNo + " ' and ivstatus<9";
                this.dbHelp.SetPIVAsDB(sqlStr);
                return "1";
            }
            msg = str3;
            this.dbHelp.GetPIVAsDB(string.Format(format, GroupNo));
            return "0";
        }

        public override string PivasRevPreTrue(string GroupNo, string DEmployeeID, out string msg)
        {
            msg = "";
            return "1";
        }

        public override bool PrintCharge(List<string> LabelNos, string DEmployeeID)
        {
            string str = DateTime.Now.ToString();
            string str2 = "";
            foreach (string str3 in LabelNos)
            {
                string format = " select ivd.IVRecordID ,dd.DrugCode ,dd.DrugName ,dd .Spec ,ivd.DgNo ,iv.Batch ,ivd.dosage ,iv.labelover,  dd.firm from IVRecordDetail ivd  inner join DDrug  dd on   dd.DrugCode =ivd.DrugCode   inner join IVRecord  iv on iv.IVRecordID =ivd.IVRecordID where iv.LabelNo ='{0}'";
                DataSet set = this.dbHelp.GetPIVAsDB(string.Format(format, str3));
                int num = Convert.ToInt32(set.Tables[0].Rows.Count.ToString());
                switch (num)
                {
                    case 1:
                        if (str2.Contains("单药"))
                        {
                            str2 = str2 ?? "";
                        }
                        else
                        {
                            str2 = str2 + "单药";
                        }
                        break;

                    case 2:
                        if (str2.Contains("双药"))
                        {
                            str2 = str2 ?? "";
                        }
                        else
                        {
                            str2 = str2 + "双药";
                        }
                        break;

                    case 3:
                        if (str2.Contains("三药"))
                        {
                            str2 = str2 ?? "";
                        }
                        else
                        {
                            str2 = str2 + "三药";
                        }
                        break;
                }
                if (num > 3)
                {
                    if (str2.Contains("多药"))
                    {
                        str2 = str2 ?? "";
                    }
                    else
                    {
                        str2 = str2 + "多药";
                    }
                }
                if (Convert.ToInt32(set.Tables[0].Rows[0]["labelover"].ToString()) >= 0)
                {
                    for (int j = 0; j < set.Tables[0].Rows.Count; j++)
                    {
                        string str6 = "";
                        string str7 = set.Tables[0].Rows[j][0].ToString();
                        string str8 = set.Tables[0].Rows[j][1].ToString();
                        string str9 = set.Tables[0].Rows[j][2].ToString();
                        if (str9.Contains("胰岛素"))
                        {
                            str6 = set.Tables[0].Rows[j]["dosage"].ToString();
                        }
                        else
                        {
                            str6 = set.Tables[0].Rows[j][4].ToString();
                        }
                        string str10 = set.Tables[0].Rows[j][3].ToString();
                        string str11 = set.Tables[0].Rows[j][5].ToString();
                        string str12 = set.Tables[0].Rows[j]["firm"].ToString();
                        string str13 = "insert into do_list_out1 (MedOnlyCode,MedName,MedUnit,MedOutAMT,MedFactory,labelno,batch,MedOutTime)    values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}') ";
                        this.dbHelp.SetPIVAsDB(string.Format(str13, new object[] { str8, str9, str10, str6, str12, str3, str11, str }));
                        string str14 = "update ivrecord set remark3=10 where labelno='" + str3 + "'";
                        this.dbHelp.SetPIVAsDB(str14);
                    }
                }
            }
            string sqlStr = "select distinct batch from do_list_out1 where isnull(PresNo,'')='' ";
            string str16 = "";
            DataSet pIVAsDB = this.dbHelp.GetPIVAsDB(sqlStr);
            for (int i = 0; i < pIVAsDB.Tables[0].Rows.Count; i++)
            {
                string str17 = pIVAsDB.Tables[0].Rows[i][0].ToString();
                str16 = str16 + str17;
            }
            string str18 = DateTime.Now.ToString();
            string str19 = str16 + "||" + str18;
            string str20 = "update do_list_out1 set PresNo=  '" + str19 + "||" + str2 + "' where isnull(PresNo,'')='' ";
            this.dbHelp.SetPIVAsDB(str20);
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
            msg = "可以出仓";
            string sqlStr = "select  remark3 from ivrecord where labelno='" + LabelNo + "'";
            if (Convert.ToInt32(this.dbHelp.GetPIVAsDB(sqlStr).Tables[0].Rows[0]["remark3"].ToString()) < 15)
            {
                msg = "未计费";
                return "0";
            }
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

        public override int WXCharge(string Groupno, string infusionDT, string UserCode, out string hismsg, out string hisret)
        {
            hismsg = string.Empty;
            hisret = string.Empty;
            OleDbConnection connection = new OleDbConnection("Provider=MSDAORA.1;Password=his3;User ID=his3;Data Source=zsk; Persist Security Info=True");
            OleDbCommand command = new OleDbCommand();
            try
            {
                int num;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PKG_Sy_Jizhang.by_Sy_Jz";
                OleDbParameter[] parameterArray = new OleDbParameter[] { new OleDbParameter("p_group_no", OleDbType.VarChar, 50), new OleDbParameter("p_performance_time_x ", OleDbType.VarChar, 500), new OleDbParameter("p_jzr  ", OleDbType.VarChar, 50), new OleDbParameter("p_msg", OleDbType.VarChar, 50), new OleDbParameter("p_return", OleDbType.VarChar, 200) };
                for (num = 0; num < (parameterArray.Length - 2); num++)
                {
                    parameterArray[num].Direction = ParameterDirection.Input;
                }
                parameterArray[3].Direction = ParameterDirection.Output;
                parameterArray[4].Direction = ParameterDirection.Output;
                parameterArray[0].Value = Groupno;
                parameterArray[1].Value = infusionDT;
                parameterArray[2].Value = UserCode;
                for (num = 0; num < parameterArray.Length; num++)
                {
                    command.Parameters.Add(parameterArray[num]);
                }
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                command.Connection = connection;
                command.ExecuteNonQuery();
                hisret = parameterArray[4].Value.ToString();
                hismsg = parameterArray[3].Value.ToString();
            }
            catch (Exception exception)
            {
                hismsg = "pivas:" + exception.Message;
                return 0;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Dispose();
                command.Parameters.Clear();
                command.Dispose();
            }
            return 1;
        }
    }
}
