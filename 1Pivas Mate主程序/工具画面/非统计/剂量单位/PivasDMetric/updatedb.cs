using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using PIVAsDBhelp;

namespace DMetricManage
{
    class updatedb
    {
        DB_Help dbhelp = new DB_Help();
        public void updatdUnit(string MetricCode, string MetricName, string UnitID, string oldID, string oldcode, string oldname)
        {
            StringBuilder str = new StringBuilder();
            str.Append("update DMetric set ");
            str.Append("MetricCode='" + MetricCode + "',  ");
            str.Append("MetricName='" + MetricName + "',  ");
            str.Append("UnitID = " + UnitID + " ");
            str.Append("where UnitID = " + oldID + " and MetricCode = '"+oldcode+"' ");
            str.Append("and MetricName = '" + oldname + "'");
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
