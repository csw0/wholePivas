using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace FirstCenterWebService
{
    public class ChargeTest
    {
         string chargeproc = "PHARMACY.DRUG_TRANS_PKG.TRANS_DRUG_PIVAS";
        //OracleCommand proc = new OracleCommand();
        //OracleCommand com = new OracleCommand();
        //OracleConnection conn;
        OleDbCommand proc = new OleDbCommand();
        OleDbCommand com = new OleDbCommand();



        string connString = "Provider=MSDAORA;Data Source=test11g1;User ID=inter_pivas;Password=inter_pivas";


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

            }
        }

        public String charge(String xml, out string msg) {
            msg = "开始计费";
            HISConnection();
            XmlToDataSet xds = new XmlToDataSet();
            DataSet ds = xds.getDS(xml);
            string s = execProc(ds, ref msg);
            return s;

        
        
        }

        private void InitiProc()
        {
            //proc.Connection.ConnectionString = connstring;
            proc.Connection = new OleDbConnection(connString);
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

            OleDbTransaction trunsaction = null;
            trunsaction = com.Connection.BeginTransaction(IsolationLevel.ReadCommitted);

            string ret = "0";
            int judgeflag = 1;
            try
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

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
                    proc.Parameters["iv_ADD_NURSE"].Value = ds.Tables[0].Rows[0]["iv_ADD_NURSE"].ToString();
                    proc.Parameters["in_stock_flag"].Value = ds.Tables[0].Rows[i]["in_stock_flag"].ToString();
                    proc.Parameters["on_price"].Value = "";
                    proc.Parameters["on_charges"].Value = "";
                    proc.Parameters["od_DISPENSING_DATE_TIME"].Value = DateTime.Now;
                    proc.Parameters["on_exec_val"].Value = "";
                    proc.Parameters["ov_exec_txt"].Value = "";
                    proc.Connection.Open();
                    proc.ExecuteNonQuery();
                    if (proc.Parameters["on_exec_val"].Value.ToString() == "1")
                    {
                        proc.Connection.Close();
                        judgeflag = 1;
                        continue;
                    }
                    else
                    {
                        proc.Connection.Close();
                        s = proc.Parameters["ov_exec_txt"].Value.ToString();
                        ret = proc.Parameters["on_exec_val"].Value.ToString();
                        judgeflag = 0;
                        //com.CommandText = "rollback";
                        //com.ExecuteNonQuery();
                        return ret;
                    }
                }

                s = proc.Parameters["ov_exec_txt"].Value.ToString();
                ret = proc.Parameters["on_exec_val"].Value.ToString();
                if (judgeflag == 1)
                {
                    trunsaction.Commit();
                }
                else
                {
                    trunsaction.Rollback();
                }
                //com.CommandText = "commit";

                //com.ExecuteNonQuery();
                com.Connection.Close();

                return ret;
            }
            catch (System.Exception ex)
            {
                //com.CommandText = "rollback";
                //com.ExecuteNonQuery();
                trunsaction.Rollback();
                com.Connection.Close();
                s = ex.Message;
                return "0";
            }
            finally
            {
                proc.Connection.Close();
            }


        }

    }
}