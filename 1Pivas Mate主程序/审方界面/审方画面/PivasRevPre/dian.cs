using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PivasRevPre
{
    public partial class dian : UserControl
    {
        bool Code;
        public dian(bool code)
        {
            InitializeComponent();
            Code = code;
            Select(code);
        }
        private void Select(bool code) 
        {
            if (code == false)
            {
                //this.BackgroundImage = Properties.Resources._15;
                this.pictureBox1.Visible = true;
                this.pictureBox2.Visible = false ;
            }
            else 
            {
                //this.BackgroundImage = Properties.Resources._16;
                this.pictureBox1.Visible = false;
                this.pictureBox2.Visible = true;
            }
        }
    }
}
