using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PivasInfor.Syn
{
    public partial class SynForm : UserControl
    {
        private string userType = string.Empty;
        public SynForm(string a)
        {
            InitializeComponent();
            this.userType = a;
        }

        private void SynForm_Load(object sender, EventArgs e)
        {
            if (userType == "0")
            {
                richTextBox5.Visible = false;
                panel2.Visible = false;
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
      

     
    }
}
