using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Owc11;

namespace PIVAsPAstatic
{
    public partial class DataChart : Form
    {
        DataBase db = new DataBase();
        public DataChart(string date)
        {
            InitializeComponent();
            DataTable dt = default(DataTable);
            dt = db.getChartCount(date).Tables[0];
            //设置图表的数据源
            this.chart1.DataSource = dt;
            //设置图表Y轴对应项
            this.chart1.Series[0].YValueMembers = "Total";

            //this.chart1.Series[1].YValueMembers = "Volume2";
            //设置图表X轴对应项
            this.chart1.Series[0].XValueMember = "Item";
            //绑定数据
            this.chart1.DataBind();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.chart1.Series[0].Points[i].IsValueShownAsLabel = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = default(DataTable);
            dt = CreateDataTable();
            //设置图表的数据源
            this.chart1.DataSource = dt;
            //设置图表Y轴对应项
            this.chart1.Series[0].YValueMembers = "Volume1";
            
            //this.chart1.Series[1].YValueMembers = "Volume2";
            //设置图表X轴对应项
            this.chart1.Series[0].XValueMember = "Date";
            //绑定数据
            this.chart1.DataBind();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.chart1.Series[0].Points[i].IsValueShownAsLabel = true;
            }
            this.chart1.Series[0].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            
        }

        private DataTable CreateDataTable()
        {
            //Create a DataTable as the data source of the Chart control
            DataTable dt = new DataTable();
            //Add three columns to the DataTable
            dt.Columns.Add("Date");
            dt.Columns.Add("Volume1");
            dt.Columns.Add("Volume2");
            DataRow dr;
            //Add rows to the table which contains some random data for demonstration
            dr = dt.NewRow();
            dr["Date"] = "Jan";
            dr["Volume1"] = 731;
            dr["Volume2"] = 4101;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["Date"] = "Feb";
            dr["Volume1"] = 24;
            dr["Volume2"] = 4324;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["Date"] = "Mar";
            dr["Volume1"] = 935;
            dr["Volume2"] = 2935;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["Date"] = "Apr";
            dr["Volume1"] = 466;
            dr["Volume2"] = 5644;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["Date"] = "May";
            dr["Volume1"] = 117;
            dr["Volume2"] = 5671;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["Date"] = "Jun";
            dr["Volume1"] = 346;
            dr["Volume2"] = 4646;
            dt.Rows.Add(dr);
            return dt;

        }
    }
}
