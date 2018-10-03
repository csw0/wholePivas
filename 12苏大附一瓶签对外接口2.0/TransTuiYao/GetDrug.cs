using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PIVAsDBhelp;
using System.Xml;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;


namespace TransTuiYao
{
    class GetDrug
    {
        private SqlConnection sc = new SqlConnection("Data Source=10.1.1.58;User ID=bolo;Password=bolo@123;Initial Catalog=G_ISDB;");

        private SqlConnection scc = new SqlConnection("Data Source=10.1.1.51;User ID=laennec;Password=13816350872;Initial Catalog=Pivas2015;");

        DB_Help DB = new DB_Help();

        public void GetYTJDrug()
        {
            try
            {
                //清除英特吉药品表(Drug_YTJ)全部数据
                string firstSql = " truncate table Drug_YTJ ";
                DB.SetPIVAsDB(firstSql);

                //插入查询的数据到英特吉药品表(Drug_YTJ)
                string secondSQL = " select distinct drugcode from v_druginmachine ";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(secondSQL, sc);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        string thirdSQL = " insert into Drug_YTJ values ('" + r["drugcode"].ToString().Trim() + "')";
                        DB.SetPIVAsDB(thirdSQL);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private int SSqlExec(string s, SqlConnection ss)
        {
            try
            {
                sc.Open();
                SqlCommand cmd = new SqlCommand(s, ss);
                int i = cmd.ExecuteNonQuery();
                return i;
                sc.Close();
            }
            catch (Exception ex)
            {
                //异常处理
                return 0;
            }
        }

    }
}
