using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace qqClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入账号或密码","提示",MessageBoxButtons.OK);
                return; 
            }
            //Main m = new Main(textBox1.Text, "感染二科护理单元");
            //this.Hide();
            //m.ShowDialog();
            
        }
    }
}
