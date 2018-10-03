using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PivasWebServiceInterface
{
    public class DBClass
    {
        string DataBse = System.Configuration.ConfigurationManager.AppSettings["Databse"].ToString();
        string OracBse = System.Configuration.ConfigurationManager.AppSettings["ORCLConnString"].ToString();

        public DataSet GetDB(string sql)
        {
            string connstring = "server=192.168.0.76;database=Pivas2014s;uid=laennec;pwd=13816350872";
            SqlConnection conn = new SqlConnection(connstring);
            conn.Open();
            SqlDataAdapter adp = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            adp.Dispose();
            conn.Close();
            return ds;
        }

        public int SetDB(string sql)
        {
            try
            {
                SqlConnection conn1 = new SqlConnection(DataBse);
                conn1.Open();
                SqlCommand cmd = new SqlCommand(sql, conn1);
                int i = cmd.ExecuteNonQuery();
                conn1.Close();
                return i;
            }
            catch
            {
                return 0;
            }
        }
        public DataSet getOraDB(String sql)
        {

            
            DataSet ds = new DataSet();
            using (OleDbCommand sqlc = new OleDbCommand(sql, new OleDbConnection(OracBse)))
            {

                using (OleDbDataAdapter oraDap = new OleDbDataAdapter(sqlc))
                {
                    oraDap.Fill(ds);
                }
                sqlc.Connection.Close();
                return ds;

            }
        }

    }
}