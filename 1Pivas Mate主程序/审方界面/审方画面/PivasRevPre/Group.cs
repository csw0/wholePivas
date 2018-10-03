using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PivasRevPre
{
    public partial class Group : UserControl
    {
        public Group()
        {
            InitializeComponent();
        }

        public void SetGroup(DataRow R)
        {
            try
            {
                lblbatch.Text = R["Batch"].ToString();
                lblGroupNo.Text = R["GroupNo"].ToString();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }//设置显示组号

        private void lblGroupNo_Click(object sender, EventArgs e)
        {
            this.Parent.Parent.Focus();
        }

        public void clear()
        {
            lblbatch.Text = "";
            lblGroupNo.Text = "";
        }
    }
}
