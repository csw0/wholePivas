using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PivasInfor
{
    public partial class PassWord : Form
    {
        private string userType = string.Empty;
        public PassWord(string a)
        {
            InitializeComponent();
           this.userType = a;
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (userType == "1" && textBox1.Text == "13816350872")
                {
                    this.DialogResult = DialogResult.Yes;
                }
                else if (userType == "2" && textBox1.Text == "9999")
                {
                    this.DialogResult = DialogResult.Yes;
                }
                else
                {
                    MessageBox.Show("密码错误");
                    //this.DialogResult = DialogResult.No;
                }


                this.Close();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            label2.BackColor = Color.Red;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
        }


    }
}
