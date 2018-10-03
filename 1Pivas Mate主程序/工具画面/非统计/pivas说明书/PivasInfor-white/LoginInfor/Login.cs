using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PivasInfor.LoginInfor
{
    public partial class Login : UserControl
    {
        private string show = string.Empty; //a传递参数
        public Login(string a)
        {
            InitializeComponent();
            this.show = a;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (show == "0")
            {
                panel3.Visible = false;

            }
            else
            {
                panel3.Visible = true; 
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            panel1.Focus();
            frmPivasInfor f = new frmPivasInfor();
            f.panel1.Focus();
        }

    

     

      


    }
}
