using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastReport;

namespace Damage
{
    public partial class Form3 : Form
    {
        //public DataTable dt;
        public Form3(DataTable dt)
        {
            InitializeComponent();
            this.printer(dt);
        }

        private void printer(DataTable dt)
        {
            previewControl1.Visible = true;
            report1.Clear();
            if (dt != null)
            {
                report1.Preview = previewControl1;
                report1.Load(".\\cctj_hz.frx");
                //report1.GetParameter("PrintDT").Value = DateTime.Now.ToString("");
                //report1.GetParameter("Total").Value = dt.Rows.Count;
                //report1.GetParameter("DemployName").Value = this.demployname;
                report1.RegisterData(dt, "dt");
                ((report1.FindObject("Data1")) as FastReport.DataBand).DataSource = report1.GetDataSource("dt");
                report1.Show();
            }
            else
            {
                previewControl1.Clear();

            }
        }



    }
}
