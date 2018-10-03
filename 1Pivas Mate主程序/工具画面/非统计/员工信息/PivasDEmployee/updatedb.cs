using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using PIVAsDBhelp;

namespace EmployeeManage
{
    class updatedb
    {
        DB_Help dbhelp = new DB_Help();
        public void updatdEMployee(string Account, string Pas, string Position, string EName, string ECode, string Isvalid, int EID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update DEmployee set ");
            str.Append("AccountID='" + Account + "',Pas='" + Pas + "', ");
            str.Append("Position='" + Position + "',DEmployeeName='"+EName+"',DEmployeeCode='"+ECode+"', ");
            str.Append("IsValid ='"+Isvalid+"' where DEmployeeID="+EID);
            try
            {
                dbhelp.SetPIVAsDB(str.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("错误：" + e.Message);
            }
        }
    }
}
