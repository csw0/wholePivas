using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConsumablesStatic
{
    public partial class Print : UserControl
    {
        DataTable dtPrint = new DataTable();
        public Print(DataTable dt)
        {
            InitializeComponent();
            this.dtPrint = dt;
        }

        private void Print_Load(object sender, EventArgs e)
        {
            previewQD.Visible = true;
            report.Preview = previewQD;
            report.Load(".\\Crystal\\ConsumablesStatic.frx");
            report.GetParameter("PrintDT").Value = DateTime.Now.ToString();
            report.GetParameter("Total").Value = dtPrint.Rows.Count;
            report.RegisterData(dtPrint, "dt");
            ((report.FindObject("Data1")) as FastReport.DataBand).DataSource = report.GetDataSource("dt");
            report.Show();
            
        }
    }
}
