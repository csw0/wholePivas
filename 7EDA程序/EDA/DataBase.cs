using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

namespace EDA
{
    
    class DataBase
    {
        string _constr;
        XmlDocument xml = new XmlDocument();
        DataSet ds = new DataSet();
        SqlConnection Con = new SqlConnection();
        SqlCommand Com = new SqlCommand();

        public DataBase()
        {
            try 
            {
                //_constr = getconstr();
                _constr = "Data Source =.;Initial Catalog =Pivas20141016核对;User ID =laennec;Password =13816350872";
                Con.ConnectionString = _constr;
                Con.Open();
            }
             catch (Exception ex)
            {
               MessageBox.Show(ex.ToString());
            }
        }

        private string getconstr()
        {
            try
            {
                
                xml.Load(@"E:\\IMEQPIVAs.xml");
                //xml.Load(Application.StartPath + "\\IMEQPIVAs.xml");
                
                XmlNode node = xml.SelectSingleNode("Database");                
                _constr = "Data Source = " + node.SelectSingleNode("DataSource").InnerText + ";";
                _constr = _constr + "Initial Catalog = " + node.SelectSingleNode("InitialCatalog").InnerText + ";";
                _constr = _constr + "User ID = " + node.SelectSingleNode("UserID").InnerText + ";";
                //_constr = _constr + "User ID = " + "laennec" + ";";
                _constr = _constr + "Password = " + node.SelectSingleNode("Password").InnerText;

               
                return _constr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        /// <summary>
        /// 返回数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public string Constr()
        {
            return _constr;
        }

        public DataSet GetDataset(string Sql)
        {
            try
            
            
           {
                ds.Clear();
                SqlDataAdapter read = new SqlDataAdapter(Sql, Con);                
                read.Fill(ds);
                read.Dispose();
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public int ExecSql(string sql)
        {
            try
            {
                Com.CommandText = sql;
                Com.Connection = Con;
                int n = Com.ExecuteNonQuery();
                return n;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return 0;
            }
        }
    }
}
