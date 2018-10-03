using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PivasFreqRule
{
    public partial class UDFreq : UserControl
    {
        public UDFreq()
        {
            InitializeComponent();
        }
        
        public void rowDFReq(DataRow row)
        {
            label1.Text = row["FreqCode"].ToString();
            label2.Text = row["TimesOfDay"].ToString();
            label3.Text = row["FreqName"].ToString();
        }

      

        private void UDFreq_Click(object sender, EventArgs e)
        {
            this.Focus();
            ((CommonRule)this.Parent.Parent).rowFReqRule(label1.Text, label3.Text);
        }

        private void UDFreq_Enter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(140, 140, 255);
        }

        private void UDFreq_Leave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }
    }
}
