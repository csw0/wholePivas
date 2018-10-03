using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PivasInfor.RevPre
{
    public partial class RevpreForm : UserControl
    {
        private string userType = string.Empty;
        public RevpreForm(string a)
        {
            InitializeComponent();
           this.userType = a;
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            this.panel4.Focus();
            frmPivasInfor f = new frmPivasInfor();
            f.panel1.Focus();
        }

        private void RevpreForm_Load(object sender, EventArgs e)
        {
            if (userType == "0")
            {
                panel5.Visible = false;
                this.Size = new Size(733, 600);
            }
            else if (userType == "1")
            {
                panel2.Visible = true;
                richTextBox5.Visible = false;
                this.Size = new Size(733, 1800);
            }
            else
            {
                panel2.Visible = true;
                richTextBox5.Visible = true;
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {
            panel2.Focus();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            panel3.Focus();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            this.panel3.Focus();
            frmPivasInfor f = new frmPivasInfor();
            f.panel1.Focus();
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            this.panel2.Focus();
            frmPivasInfor f = new frmPivasInfor();
            f.panel1.Focus();
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            this.panel5.Focus();
            frmPivasInfor f = new frmPivasInfor();
            f.panel1.Focus();
        }
    }
}
