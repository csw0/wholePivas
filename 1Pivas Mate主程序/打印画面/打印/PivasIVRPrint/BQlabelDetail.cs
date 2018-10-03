using System;
using System.Data;
using System.Windows.Forms;

namespace PivasIVRPrint
{
    public partial class BQlabelDetail : UserControl
    {
        protected internal DataSet ds;
        public BQlabelDetail()
        {
            InitializeComponent();
        }
        public BQlabelDetail(DataSet dss)
        {
            ds = dss;
            InitializeComponent();
        }
        public void BQlabelDetail_Load(object sender, EventArgs e)
        {
            label6.Text =
            label7.Text =
            label9.Text =
            label10.Text =
            label11.Text =
            label12.Text =
            label17.Text =
            label18.Text =
            label19.Text =
            label20.Text =
            label23.Text =
            label24.Text = string.Empty;
            this.Size = this.Parent.Size;
            flowLayoutPanel1.Controls.Clear();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                label6.Text = dr["WardName"].ToString();
                label7.Text = dr["DEmployeeName"].ToString();
                label9.Text = string.Empty;
                label10.Text = dr["StartDT"].ToString();
                label11.Text = dr["EndDT"].ToString();
                label12.Text = dr["FreqCode"].ToString();
                label17.Text = dr["PatName"].ToString();
                label18.Text = dr["Age"].ToString();
                label19.Text = dr["BedNo"].ToString();
                label20.Text = dr["CaseID"].ToString();
                label23.Text = dr["Sex"].ToString().Trim() == "1" ? "男" : (dr["Sex"].ToString().Trim() == "2" ? "女" : "其他");
                label24.Text = dr["Weight"].ToString();
                foreach (DataRow drs in ds.Tables[0].Rows)
                {
                    flowLayoutPanel1.Controls.Add(new BQDTdrugs(drs));
                }
                ds.Dispose();
            }
        }
    }
}
