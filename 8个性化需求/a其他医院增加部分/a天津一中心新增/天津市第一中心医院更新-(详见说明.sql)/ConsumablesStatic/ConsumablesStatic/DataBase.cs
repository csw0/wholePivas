using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PIVAsDBhelp;
using System.Data;
using System.Windows.Forms;

namespace ConsumablesStatic
{
    class DataBase
    {
        DB_Help db = new DB_Help();
        public DataTable getCount(string sql)
        {
            DataSet ds = null;
            try 
            {
                ds = db.GetPIVAsDB(sql);
            }
            catch (Exception e) { MessageBox.Show("检查费用计算是否为0"); }
            return ds.Tables[0];
        }
    }
}
