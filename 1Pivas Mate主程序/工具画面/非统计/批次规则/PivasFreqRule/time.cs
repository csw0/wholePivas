using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PivasFreqRule
{
    public partial class time : UserControl
    {
        public time()
        {
            InitializeComponent();
        }
        public void row(int i,string time) 
        {
           
                checkBox1.Text = i.ToString("D2") + ":00";
        
            string[] hour =time.Split('_');
            for (int j = 0; j < hour.Length; j++)
            {
                if (hour[j].Length >=2)
                {

                    if (checkBox1.Text.Replace(":00", "") == hour[j].Substring(0, 2))
                    {
                        checkBox1.Checked = true;
                        textBox1.Text = hour[j].Substring(3, 2);
                    }
                }
            }
          
        }
        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.Visible = true;
                textBox1.Clear();
                textBox1.Text = "00";
                label1.Text = checkBox1.Text;
                textBox1.Focus();
            }
            else 
            {
                textBox1.Visible = false;
                label1.Text = null;
                
            }

            
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int mm = 0;
            try
            {
                if (textBox1.Text!= null) 
                {
                    mm = Convert.ToInt32(textBox1.Text); 
                }   
                if (mm > 59 || mm < 0)
                {
                   MessageBox.Show("输入错误");
                   textBox1.Clear();
                }
                else
                {
                   int i = Convert.ToInt32(textBox1.Text);
                   label1.Text = checkBox1.Text.Substring(0,3) + i.ToString("D2");
                        
                }
                
            }
            catch 
            {
                
                textBox1.Clear();
            }

        }

        
    }
}
