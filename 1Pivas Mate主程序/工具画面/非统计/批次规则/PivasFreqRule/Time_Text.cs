using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PivasFreqRule
{
    public partial class Time_Text : Form
    {
        public Time_Text()
        {
            InitializeComponent();
        }

        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion


        private void Time_Text_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 12; i++)
            {
                time time = new time();
                time.row(i,addDFreg.j);
                time.Location = new Point(22, 21 * i + 10);
                time.Name = "time" + i;
                panel1.Controls.Add(time);
            }
            for (int i = 12; i < 24; i++)
            {
                time time = new time();
                time.row(i, addDFreg.j);
                time.Location = new Point(22, 21 * (i - 12) + 10);
                panel2.Controls.Add(time);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int k = 0;
            addDFreg form1 = (addDFreg)this.Owner;
            string str = "";
            foreach (Control c in panel1.Controls)
            {
                if (((time)c).checkBox1.Checked == true)
                {
                    str = str + ((time)c).label1.Text + "_";
                    k++;
                }
            }
            foreach (Control c in panel2.Controls)
            {
                if (((time)c).checkBox1.Checked == true)
                {
                    str = str + ((time)c).label1.Text + "_";
                    k++;
                }
            }
            if (k == form1.numericUpDown2.Value)
            {
                if (str.Length > 0)
                {
                    form1.textBox3.Text = str.Remove(str.Length - 1);
                }
                else
                {
                    form1.textBox3.Text = null;
                }
                this.Close();
            }
            else if (k > form1.numericUpDown2.Value)
            {
                MessageBox.Show("时间过多");
                form1.textBox3.Text = null;
            }
            else
            {
                MessageBox.Show("时间过少");
                form1.textBox3.Text = null;
            }
            
        }

        private void Panel_Close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void panel_head_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

    }
}
