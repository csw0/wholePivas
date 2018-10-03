using System;
using System.Data;
using System.Windows.Forms;

namespace PivasIVRPrint
{
    public partial class BQDrugs : UserControl
    {
        private static BQlabel bq;
        private static DataRow dr;
        public BQDrugs()
        {
            InitializeComponent();
        }
        public BQDrugs(BQlabel bqs,DataRow drs)
        {
            bq = bqs;
            dr = drs;
            InitializeComponent();
        }
        private void BQDrugs_Click(object sender, EventArgs e)
        {
            if (bq != null)
                bq.BQlabel_Click(sender, e);
        }
        private void BQDrugs_Load(object sender, EventArgs e)
        {
            label1.Text = dr["DrugName"].ToString();
            label6.Text = dr["Spec"].ToString();
            label2.Text = dr["Dosage"].ToString().TrimEnd('0').TrimEnd('.');
            label3.Text = dr["DosageUnit"].ToString();
            this.Width = bq.flowLayoutPanel1.Width;
        }
    }
}
