using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Data.OleDb;
using System.Threading;

namespace PivasWebServiceInterface
{
    public class ChargeService
    {
        string chargeproc = "PHARMACY.DRUG_TRANS_PKG.TRANS_DRUG_PIVAS";
        OleDbCommand proc = new OleDbCommand();
        OleDbCommand com = new OleDbCommand();
        OleDbTransaction tran = null;

        //下面注释不要删,MSDAORA微软提供的连接Oracle数据库的OleDB驱动
        string connString = "Provider=MSDAORA;Data Source=yzxzh9i;User ID=inter_pivas;Password=pivas_inter";
        //string connString = "Provider=MSDAORA;Data Source=test11g1;User ID=inter_pivas;Password=pivas_inter";
        //string Conn = "server=192.168.10.29;database=Pivas2014s;uid=laennec;pwd=13816350872";

        //string connString = "Provider=MSDAORA;Data Source=sss;User ID=inter_pivas;Password=inter_pivas";
        //string connString = "Provider=MSDAORA;Data Source=test11g1;User ID=inter_pivas;Password=pivas_inter";
        //string Conn = "server=192.168.0.76;database=Pivas2014s;uid=laennec;pwd=13816350872";

        #region 韦天锐移交计费接口
        public void HISConnection()
        {
            try
            {
                com.Connection = new OleDbConnection(connString);
                com.Connection.Open();

                InitiProc();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public String charge(String DemployeeName, String xml, out string msg)
        {
            msg = "开始计费";

            HISConnection();
            XmlToDataSet xds = new XmlToDataSet();
            DataSet ds = xds.getDS(xml);
            string s = execProc(ds, ref msg);
            return s;
        }

        private void InitiProc()
        {
            proc.Connection = com.Connection;
            proc.CommandText = chargeproc;
            proc.CommandType = CommandType.StoredProcedure;
            proc.Parameters.Clear();

            proc.Parameters.Add("iv_dispensary", OleDbType.VarChar);
            proc.Parameters.Add("iv_patient_id", OleDbType.VarChar);
            proc.Parameters.Add("in_visit_id", OleDbType.VarChar);
            proc.Parameters.Add("in_baby_no", OleDbType.VarChar);
            proc.Parameters.Add("in_order_no", OleDbType.VarChar);
            proc.Parameters.Add("in_order_sub_no", OleDbType.VarChar);
            proc.Parameters.Add("iv_drug_code", OleDbType.VarChar);
            proc.Parameters.Add("iv_drug_spec", OleDbType.VarChar);
            proc.Parameters.Add("iv_drug_units", OleDbType.VarChar);
            proc.Parameters.Add("iv_firm_id", OleDbType.VarChar);
            proc.Parameters.Add("in_dispense_amount", OleDbType.VarChar);
            proc.Parameters.Add("iv_DISPENSING_PROVIDER", OleDbType.VarChar);
            proc.Parameters.Add("in_order_sub_sub_no", OleDbType.VarChar);
            proc.Parameters.Add("iv_bar_code", OleDbType.VarChar);
            proc.Parameters.Add("id_PREFORM_DATETIME", OleDbType.DBDate);
            proc.Parameters.Add("in_SINA_MOUNT", OleDbType.Integer);
            proc.Parameters.Add("iv_ADD_NURSE", OleDbType.VarChar);
            proc.Parameters.Add("in_stock_flag", OleDbType.Integer);
            for (int i = 0; i < 18; i++)
            {
                proc.Parameters[i].Direction = ParameterDirection.Input;
            }

            proc.Parameters.Add("on_price", OleDbType.VarChar);
            proc.Parameters.Add("on_charges", OleDbType.VarChar);
            proc.Parameters.Add("od_DISPENSING_DATE_TIME", OleDbType.DBDate);
            proc.Parameters.Add("on_exec_val", OleDbType.VarChar);
            proc.Parameters.Add("ov_exec_txt", OleDbType.VarChar);

            for (int i = 18; i < 23; i++)
            {
                proc.Parameters[i].Direction = ParameterDirection.Output;
                proc.Parameters[i].Size = 255;
            }
        }

        private string execProc(DataSet ds, ref string s)
        {
            string ret = "1";
            int num = 1;
            string hismsg = "计费成功";
            string hisret = "";
            tran = proc.Connection.BeginTransaction();
            proc.Transaction = tran;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string pcode = ds.Tables[0].Rows[i]["iv_patient_id"].ToString();
                string visit_time = ds.Tables[0].Rows[i]["in_visit_id"].ToString();
                string order_no = ds.Tables[0].Rows[i]["in_order_no"].ToString();
                string order_sub_no = ds.Tables[0].Rows[i]["in_order_sub_no"].ToString();

                string charge_id = pcode + "_" + visit_time + "-" + order_no + "-" + order_sub_no;
                string drugcode = ds.Tables[0].Rows[i]["iv_drug_code"].ToString();
                string labelno = ds.Tables[0].Rows[i]["iv_bar_code"].ToString();
                string log_sql = "insert into charge_log values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')";
                string begintime = DateTime.Now.ToString();

                string query = "select 1 from charge_log where labelno='" + labelno + "' and hisret=2";

                DBClass db = new DBClass();
                DataSet ds2 = db.GetDB(query);

                if (ds2.Tables[0].Rows.Count > 0)
                {
                    num = 2;
                    break;
                }

                proc.Parameters["iv_dispensary"].Value = ds.Tables[0].Rows[i]["iv_dispensary"].ToString();// "5101";

                proc.Parameters["iv_patient_id"].Value = ds.Tables[0].Rows[i]["iv_patient_id"].ToString();
                proc.Parameters["in_visit_id"].Value = ds.Tables[0].Rows[i]["in_visit_id"].ToString();
                proc.Parameters["in_baby_no"].Value = ds.Tables[0].Rows[i]["in_baby_no"].ToString();
                proc.Parameters["in_order_no"].Value = ds.Tables[0].Rows[i]["in_order_no"].ToString();
                proc.Parameters["in_order_sub_no"].Value = ds.Tables[0].Rows[i]["in_order_sub_no"].ToString();
                proc.Parameters["iv_drug_code"].Value = ds.Tables[0].Rows[i]["iv_drug_code"].ToString();

                proc.Parameters["iv_drug_spec"].Value = ds.Tables[0].Rows[i]["iv_drug_spec"].ToString();
                proc.Parameters["iv_drug_units"].Value = ds.Tables[0].Rows[i]["iv_drug_units"].ToString();
                proc.Parameters["iv_firm_id"].Value = ds.Tables[0].Rows[i]["iv_firm_id"].ToString();
                proc.Parameters["in_dispense_amount"].Value = ds.Tables[0].Rows[i]["in_dispense_amount"].ToString();
                proc.Parameters["iv_DISPENSING_PROVIDER"].Value = ds.Tables[0].Rows[i]["iv_DISPENSING_PROVIDER"].ToString();
                proc.Parameters["in_order_sub_sub_no"].Value = ds.Tables[0].Rows[i]["in_order_sub_sub_no"].ToString();
                proc.Parameters["iv_bar_code"].Value = ds.Tables[0].Rows[i]["iv_bar_code"].ToString();
                proc.Parameters["id_PREFORM_DATETIME"].Value = DateTime.Now;
                proc.Parameters["in_SINA_MOUNT"].Value = "1";
                proc.Parameters["iv_ADD_NURSE"].Value = ds.Tables[0].Rows[i]["iv_ADD_NURSE"].ToString();
                proc.Parameters["in_stock_flag"].Value = ds.Tables[0].Rows[i]["in_stock_flag"].ToString();
                proc.Parameters["on_price"].Value = "";
                proc.Parameters["on_charges"].Value = "";
                proc.Parameters["od_DISPENSING_DATE_TIME"].Value = DateTime.Now;
                proc.Parameters["on_exec_val"].Value = "";
                proc.Parameters["ov_exec_txt"].Value = "";

                try
                {
                    proc.ExecuteNonQuery();
                    string endtime = DateTime.Now.ToString();
                    string inceptdt = DateTime.Now.ToString();
                    hisret = proc.Parameters["on_exec_val"].Value.ToString();
                    string msg = proc.Parameters["ov_exec_txt"].Value.ToString();

                    DB_Help dbhelp = new DB_Help();

                    dbhelp.addAndUpdate(string.Format(log_sql, begintime, endtime, inceptdt, labelno, charge_id, drugcode, hisret, msg));

                    if (hisret == "0" || hisret == "-1")
                    {
                        hismsg = proc.Parameters["ov_exec_txt"].Value.ToString();
                        // dbhelp.addAndUpdate("update charge_log set hisret=0 where labelno='"+labelno +"'");
                        num = 0;
                        break;
                    }
                    else
                    {
                        num = 1;
                    }
                }
                catch (Exception ex)
                {

                    DB_Help dbhelp = new DB_Help();
                    string endtime = DateTime.Now.ToString();
                    string inceptdt = DateTime.Now.ToString();
                    hismsg = ex.Message;
                    hisret = "0";
                    dbhelp.addAndUpdate(string.Format(log_sql, begintime, endtime, inceptdt, labelno, charge_id, drugcode, hisret, hismsg));
                    //dbhelp.addAndUpdate("update charge_log set hisret=0 where labelno='" + labelno + "'");
                    num = 0;
                    break;

                }
                num = num * num;
            }

            s = hismsg;
            ret = num.ToString();
            if (ret == "0")
            {
                tran.Rollback();
                com.Connection.Close();

                return "0";
            }
            else if (ret == "2")
            {
                tran.Rollback();
                com.Connection.Close();
                s = "已计费";
                return "1";
            }
            else
            {
                tran.Commit();
                com.Connection.Close();
                string label = ds.Tables[0].Rows[0]["iv_bar_code"].ToString();
                string insert = "insert into charge_log (labelno,hisret) values('{0}','{1}')";
                DBClass db2 = new DBClass();
                db2.SetDB(string.Format(insert, label, 2));
                return "1";
            }
        }
        #endregion

        #region 贾晓宇修改的计费接口
        public String chargeEx(String DemployeeName, DataSet ds, out string msg)
        {
            msg = "开始计费";
            string s = execProcEx(ds, ref msg);
            return s;
        }

        private string execProcEx(DataSet ds, ref string s)
        {
            string rett = "";
            string begindt = DateTime.Now.ToString();

            string chargeproc = "PHARMACY.DRUG_TRANS_PKG.TRANS_DRUG_PIVAS";
            string connString = "Provider=MSDAORA;Data Source=yzxzh9i;User ID=inter_pivas;Password=pivas_inter; Persist Security Info=True";

            OleDbCommand proc = new OleDbCommand();

            OleDbConnection conn = new OleDbConnection(connString);

            proc.CommandText = chargeproc;
            proc.CommandType = CommandType.StoredProcedure;
            proc.Parameters.Clear();
            proc.Parameters.Add("iv_dispensary", OleDbType.VarChar);
            proc.Parameters.Add("iv_patient_id", OleDbType.VarChar);
            proc.Parameters.Add("in_visit_id", OleDbType.VarChar);
            proc.Parameters.Add("in_baby_no", OleDbType.VarChar);
            proc.Parameters.Add("in_order_no", OleDbType.VarChar);
            proc.Parameters.Add("in_order_sub_no", OleDbType.VarChar);
            proc.Parameters.Add("iv_drug_code", OleDbType.VarChar);
            proc.Parameters.Add("iv_drug_spec", OleDbType.VarChar);
            proc.Parameters.Add("iv_drug_units", OleDbType.VarChar);
            proc.Parameters.Add("iv_firm_id", OleDbType.VarChar);
            proc.Parameters.Add("in_dispense_amount", OleDbType.VarChar);
            proc.Parameters.Add("iv_DISPENSING_PROVIDER", OleDbType.VarChar);
            proc.Parameters.Add("in_order_sub_sub_no", OleDbType.VarChar);
            proc.Parameters.Add("iv_bar_code", OleDbType.VarChar);
            proc.Parameters.Add("id_PREFORM_DATETIME", OleDbType.DBDate);
            proc.Parameters.Add("in_SINA_MOUNT", OleDbType.Integer);
            proc.Parameters.Add("iv_ADD_NURSE", OleDbType.VarChar);
            proc.Parameters.Add("in_stock_flag", OleDbType.Integer);
            for (int i = 0; i < 18; i++)
            {
                proc.Parameters[i].Direction = ParameterDirection.Input;
            }

            proc.Parameters.Add("on_price", OleDbType.VarChar);
            proc.Parameters.Add("on_charges", OleDbType.VarChar);
            proc.Parameters.Add("od_DISPENSING_DATE_TIME", OleDbType.DBDate);
            proc.Parameters.Add("on_exec_val", OleDbType.VarChar);
            proc.Parameters.Add("ov_exec_txt", OleDbType.VarChar);

            for (int i = 18; i < 23; i++)
            {
                proc.Parameters[i].Direction = ParameterDirection.Output;
                proc.Parameters[i].Size = 255;
            }

            proc.Connection = conn;

            if (conn.State != ConnectionState.Open)
                conn.Open();



            OleDbTransaction tran = conn.BeginTransaction();
            proc.Transaction = tran;

            //循环执行存储过程
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string hisret;
                string hismsg;

                string pcode = ds.Tables[0].Rows[i]["iv_patient_id"].ToString();
                string visit_time = ds.Tables[0].Rows[i]["in_visit_id"].ToString();
                string order_no = ds.Tables[0].Rows[i]["in_order_no"].ToString();
                string order_sub_no = ds.Tables[0].Rows[i]["in_order_sub_no"].ToString();

                string charge_id = pcode + "_" + visit_time + "-" + order_no + "-" + order_sub_no;
                string drugcode = ds.Tables[0].Rows[i]["iv_drug_code"].ToString();
                string labelno = ds.Tables[0].Rows[i]["iv_bar_code"].ToString();
                string log_sql = "insert into charge_log values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')";
                string begintime = DateTime.Now.ToString();

                string query = "select 1 from charge_log where labelno='" + labelno + "' and hisret=2";

                DBClass db = new DBClass();
                DataSet ds2 = db.GetDB(query);

                if (ds2.Tables[0].Rows.Count > 0)
                {
                    s = "已计费";
                    tran = null;
                    return "1";

                }

                //存储过程参数赋值
                proc.Parameters["iv_dispensary"].Value = ds.Tables[0].Rows[i]["iv_dispensary"].ToString();

                proc.Parameters["iv_patient_id"].Value = ds.Tables[0].Rows[i]["iv_patient_id"].ToString();
                proc.Parameters["in_visit_id"].Value = ds.Tables[0].Rows[i]["in_visit_id"].ToString();
                proc.Parameters["in_baby_no"].Value = ds.Tables[0].Rows[i]["in_baby_no"].ToString();
                proc.Parameters["in_order_no"].Value = ds.Tables[0].Rows[i]["in_order_no"].ToString();
                proc.Parameters["in_order_sub_no"].Value = ds.Tables[0].Rows[i]["in_order_sub_no"].ToString();
                proc.Parameters["iv_drug_code"].Value = ds.Tables[0].Rows[i]["iv_drug_code"].ToString();

                proc.Parameters["iv_drug_spec"].Value = ds.Tables[0].Rows[i]["iv_drug_spec"].ToString();
                proc.Parameters["iv_drug_units"].Value = ds.Tables[0].Rows[i]["iv_drug_units"].ToString();
                proc.Parameters["iv_firm_id"].Value = ds.Tables[0].Rows[i]["iv_firm_id"].ToString();
                proc.Parameters["in_dispense_amount"].Value = ds.Tables[0].Rows[i]["in_dispense_amount"].ToString();
                proc.Parameters["iv_DISPENSING_PROVIDER"].Value = ds.Tables[0].Rows[i]["iv_DISPENSING_PROVIDER"].ToString();
                proc.Parameters["in_order_sub_sub_no"].Value = ds.Tables[0].Rows[i]["in_order_sub_sub_no"].ToString();
                proc.Parameters["iv_bar_code"].Value = ds.Tables[0].Rows[i]["iv_bar_code"].ToString();
                proc.Parameters["id_PREFORM_DATETIME"].Value = DateTime.Now;
                proc.Parameters["in_SINA_MOUNT"].Value = "1";
                proc.Parameters["iv_ADD_NURSE"].Value = ds.Tables[0].Rows[i]["iv_ADD_NURSE"].ToString();
                proc.Parameters["in_stock_flag"].Value = ds.Tables[0].Rows[i]["in_stock_flag"].ToString();
                proc.Parameters["on_price"].Value = "";
                proc.Parameters["on_charges"].Value = "";
                proc.Parameters["od_DISPENSING_DATE_TIME"].Value = DateTime.Now;
                proc.Parameters["on_exec_val"].Value = "";
                proc.Parameters["ov_exec_txt"].Value = "";

                //执行存储过程,设定超时时间，过了就终止循环
                try
                {
                    Thread threadToKill = null;
                    Action wrappedAction = () =>
                    {
                        threadToKill = Thread.CurrentThread;
                        proc.ExecuteNonQuery();

                    };
                    IAsyncResult result = wrappedAction.BeginInvoke(null, null);
                    if (result.AsyncWaitHandle.WaitOne(6000))
                    {
                        wrappedAction.EndInvoke(result);
                    }
                    else
                    {
                        rett = "0";
                        s = " 捕获到超时，失败";
                        //threadToKill.Abort();
                        //throw new TimeoutException();
                        break;
                    }

                    string endtime = DateTime.Now.ToString();
                    string inceptdt = DateTime.Now.ToString();
                    hisret = proc.Parameters["on_exec_val"].Value.ToString();
                    string msg = proc.Parameters["ov_exec_txt"].Value.ToString();
                    s = msg;

                    DB_Help dbhelp = new DB_Help();

                    dbhelp.addAndUpdate(string.Format(log_sql, begintime, endtime, inceptdt, labelno, charge_id, drugcode, hisret, msg));
                    if (hisret == "1")
                    {
                        rett = "1";
                        continue;
                    }
                    else
                    {
                        rett = "0";
                        break;
                    }
                }

                catch (Exception ex)
                {
                    DB_Help dbhelp = new DB_Help();
                    string endtime = DateTime.Now.ToString();
                    string inceptdt = DateTime.Now.ToString();
                    hismsg = " 捕获到异常 ," + ex.Message;
                    s = hismsg;
                    hisret = "0";
                    dbhelp.addAndUpdate(string.Format(log_sql, begintime, endtime, inceptdt, labelno, charge_id, drugcode, hisret, hismsg));
                    rett = "0";
                    break;
                }
            }

            //如果正常计费
            if (rett == "1")
            {
                //事务提交
                if (conn != null && tran != null)
                {
                    tran.Commit();
                    tran = null;
                }
                //写入日志
                string label = ds.Tables[0].Rows[0]["iv_bar_code"].ToString();
                string insert = "insert into charge_log ( begindt,enddt,  labelno,hisret) values('{0}','{1}','{2}','{3}')";
                DBClass db2 = new DBClass();
                db2.SetDB(string.Format(insert, begindt, DateTime.Now.ToString(), label, 2));

                //释放连接 
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
                proc.Parameters.Clear();
                proc.Dispose();

                return "1";
            }

            //如果计费失败或者有异常
            //事务回滚 
            if (rett == "0")
            {
                if (conn != null && tran != null)
                {
                    tran.Rollback();
                    tran = null;
                }
                //释放连接 
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
                proc.Parameters.Clear();
                proc.Dispose();
                return "0";

            }

            return "1";
        }
        #endregion

        #region 启东反编译计费接口
        public string ChargeQD(string LabelNo, string UserCode, out string hismsg, out string hisret)
        {
            hismsg = "";
            hisret = "";
            DBClass class2 = new DBClass();
            string str = DateTime.Now.ToString();
            string format = ("update ivrecord set remark3='{0}',remark6='{1}' where labelno='" + LabelNo + "' ") + "update IVRecordDetail set ReturnFromHis='{2}' where ivrecordid in (select ivrecordid from ivrecord where labelno='" + LabelNo + "')";
            string str3 = "select  iv.GroupNo ,iv.InfusionDT ,  iv.Remark3 ,iv.PackAdvance ,iv.LabelOver ,p.PStatus   from IVRecord iv inner join Prescription p on p.PrescriptionID =iv.PrescriptionID where iv.LabelNo ='{0}'";
            DataSet dB = class2.GetDB(string.Format(str3, LabelNo));
            string groupno = dB.Tables[0].Rows[0]["groupno"].ToString();
            string infusionDT = dB.Tables[0].Rows[0]["InfusionDT"].ToString();
            string str6 = dB.Tables[0].Rows[0]["Remark3"].ToString();
            string str7 = dB.Tables[0].Rows[0]["PackAdvance"].ToString();
            string str8 = dB.Tables[0].Rows[0]["LabelOver"].ToString();
            string str9 = dB.Tables[0].Rows[0]["PStatus"].ToString();
            if (Convert.ToInt32(str6) == 15)
            {
                if (Convert.ToInt32(str7) == 1)
                {
                    hismsg = "已计费(提前打包)";
                    hisret = "0";
                    return "0";
                }
                hismsg = "已计费";
                hisret = "1";
                return "1";
            }
            if (Convert.ToInt32(str9) == 4)
            {
                hismsg = "医嘱已停止";
                hisret = "0";
                class2.SetDB(string.Concat(new object[] { "update ivrecord set labelover='-3',labeloverid='", UserCode, "',labelovertime='", DateTime.Now, "' where labelno='", LabelNo, "'" }));
                return "0";
            }
            if (Convert.ToInt32(str8) < 0)
            {
                hismsg = "pivas已配置取消";
                hisret = "0";
                return "0";
            }
            try
            {
                string str10;
                string str11;
                this.WXChargeQD(groupno, infusionDT, UserCode, out str10, out str11);
                string str12 = DateTime.Now.ToString();
                string str13 = groupno + "||" + infusionDT + "||" + UserCode;
                class2.SetDB("insert into ToHisChargeLog (begintime,endtime,labelno,msg,ChargeResult,Parameters,HisReturn,Remark2) values ('" + str + "','" + str12 + "','" + LabelNo + "','" + str10 + "','" + str11 + "' ,'" + str13 + "','" + str11 + "','" + UserCode + "' )");
                if (str10.Contains("记账成功") || str10.Contains("已接收"))
                {
                    string str14 = "";
                    string sql = "select top 1  demployeeid   from  demployee  where demployeecode='" + UserCode + "'";
                    DataTable table = class2.GetDB(sql).Tables[0];
                    if (table.Rows.Count > 0)
                    {
                        str14 = table.Rows[0][0].ToString();
                    }
                    string str16 = "    update  IVRecord set IVStatus=9 where labelNo = '{0}'    insert into IVRecord_PZ(IVrecordID,PZDT,ScanCount,pcode,Location,[Type])     values('{1}',getdate(), (SELECT  COUNT(2) from IVRecord_PZ where IVrecordID = '{2}' and Invalid is null),  '{3}','I',2)";
                    class2.SetDB(string.Format(str16, new object[] { LabelNo, LabelNo, LabelNo, str14 }));
                    if (Convert.ToInt32(str7) == 1)
                    {
                        hismsg = "记账成功(提前打包)";
                        hisret = "0";
                        class2.SetDB(string.Format(format, 15, str10, str11));
                        return "0";
                    }
                    hismsg = "记账成功";
                    hisret = "0";
                    class2.SetDB(string.Format(format, 15, str10, str11));
                    return "1";
                }
                hismsg = str10;
                hisret = str11;
                class2.SetDB(string.Format(format, 12, str10, str11));
                if (str10.Contains("医嘱"))
                {
                    class2.SetDB(string.Concat(new object[] { "update ivrecord set labelover='-3',labeloverid='", UserCode, "',labelovertime='", DateTime.Now, "' where labelno='", LabelNo, "'" }));
                }
                return "0";
            }
            catch (Exception exception)
            {
                string str17 = DateTime.Now.ToString();
                hismsg = exception.Message;
                hisret = "0";
                class2.SetDB(string.Concat(new object[] { "INSERT INTO ToHisChargeLog(BeginTime,EndTime,LabelNo,ChargeResult,HisReturn,msg,remark2) VALUES ('", str, "','", str17, "','", LabelNo, "','", 0, "','", hisret, "','", hismsg, "') " }));
                return "0";
            }
        }

        public int WXChargeQD(string Groupno, string infusionDT, string UserCode, out string hismsg, out string hisret)
        {
            hismsg = string.Empty;
            hisret = string.Empty;
            OleDbConnection connection = new OleDbConnection("Provider=MSDAORA.1;Password=his3;User ID=his3;Data Source=zsk; Persist Security Info=True");
            OleDbCommand command = new OleDbCommand();
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PKG_Sy_Jizhang.by_Sy_Jz";
                OleDbParameter[] parameterArray = new OleDbParameter[] 
                {
                    new OleDbParameter("p_group_no", OleDbType.VarChar, 50),
                    new OleDbParameter("p_performance_time_x ", OleDbType.VarChar, 500),
                    new OleDbParameter("p_jzr  ", OleDbType.VarChar, 50),
                    new OleDbParameter("p_msg", OleDbType.VarChar, 50),
                    new OleDbParameter("p_return", OleDbType.VarChar, 200)
                };
                for (int i = 0; i < (parameterArray.Length - 2); i++)
                {
                    parameterArray[i].Direction = ParameterDirection.Input;
                }
                parameterArray[3].Direction = ParameterDirection.Output;
                parameterArray[4].Direction = ParameterDirection.Output;
                parameterArray[0].Value = Groupno;
                parameterArray[1].Value = infusionDT;
                parameterArray[2].Value = UserCode;
                for (int j = 0; j < parameterArray.Length; j++)
                {
                    command.Parameters.Add(parameterArray[j]);
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
        #endregion
    }
}
