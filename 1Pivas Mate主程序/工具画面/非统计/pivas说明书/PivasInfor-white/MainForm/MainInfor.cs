using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PivasInfor.MainForm
{
    public partial class MainInfor : UserControl
    {
        private string userType = string.Empty;
        public MainInfor(string a)
        {
            InitializeComponent();
            this.userType = a;
        }

        private void MainInfor_Click(object sender, EventArgs e)
        {
            this.Focus();
            frmPivasInfor f=new frmPivasInfor ();
            f.panel1.Focus();
        }

        private void MainInfor_Load(object sender, EventArgs e)
        {

        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            panel1.Focus();
            frmPivasInfor f = new frmPivasInfor();
            f.panel1.Focus();
        }

      
    }
}
