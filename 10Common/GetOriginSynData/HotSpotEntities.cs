using System;
using System.Data.SqlClient;

namespace GetOriginSynData
{
    public class HotSpotEntities
    {
        public HotSpotEntities()
        {
            //if (conn == null)
            //{
                conn = new ConnConnection();
            //}
        }
        private ConnConnection conn = null;
        
        public ConnConnection Connection { get { return conn; } }
    }

    public class ConnConnection
    {
        //Data Source=.;Initial Catalog=pivas_LY;User ID=sa;Password=123456 
        private string strConn = "Data Source=.;Initial Catalog=pivas.LY;User ID=sa;Password=123456";
        public bool Open()
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(strConn);
                sqlConn.Open();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("打开数据库连接错误"+ex.Message);
                return false;
            }
        }
    }
}
