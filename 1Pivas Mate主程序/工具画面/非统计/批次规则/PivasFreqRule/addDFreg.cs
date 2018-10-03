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
    public partial class addDFreg : Form
    {
        public addDFreg()
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

        public delegate void DelegateChangeTextValS();
        // 2.定义委托事件  
        public event DelegateChangeTextValS ChangeTextVal;



        DataTable dt = new DataTable();
        updatedeletedb insert = new updatedeletedb();
        updatedeletedb update = new updatedeletedb();
        seldb sel = new seldb();
        string[] time;
        public  static string j;
        /// <summary>
        /// 添加按钮，保存新增用法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder warn = new StringBuilder();
            dt=sel.getDFregID().Tables[0];
           // DFreg f = (DFreg)this.Owner;
            if(textBox1.Text.ToString()=="")
            {
                warn.Append("请输入名称\n");
            }
            if (textBox2.Text.ToString() =="")
            {
                warn.Append("请输入编码\n");
            }
            if (textBox3.Text.ToString() == "") 
            {
                warn.Append("请输入使用时间\n");
            }
            else
            {
                time = textBox3.Text.Split('_');
                if (time.Length != numericUpDown2.Value)
                {
                    warn.Append("请修改使用时间");
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (textBox1.Text == dt.Rows[i][0].ToString())
                {
                    warn.Append("用法已存在");
                    break;
                }
                
            }
            if (warn.ToString()!="")
            {
              MessageBox.Show(warn.ToString());
            }
            else
            {
              insert.insertDFreq(textBox1.Text, textBox2.Text, numericUpDown1.Value.ToString(), numericUpDown2.Value.ToString(),textBox3.Text);
              insertFreqRule(Convert.ToInt32(numericUpDown2.Value));
              this.Close();
              ChangeTextVal();
            //  ((DFreg)(this.Parent)).refresh();
            }
        }

        private void insertFreqRule(int subcode)
        {
            StringBuilder mrg=new StringBuilder();
            string codeid;
            for (int i = 1; i <= subcode; i++)
            {
                codeid = textBox1.Text + i;
                insert.intsertFreqRule(textBox2.Text, codeid);
                
            }
            string[] time = textBox3.Text.ToString().Split('_');
            if (time.Length > 0)
            {
                    for (int j = 0; j < time.Length; j++)
                    {
                        if (sel.getOrderID(time[j]).Tables[0].Rows.Count > 0)
                        {
                            string order = sel.getOrderID(time[j]).Tables[0].Rows[0][0].ToString();
                            codeid = textBox1.Text + (j + 1);
                            update.updateFreqRule(time[j], order, textBox1.Text, codeid);
                        }
                        else
                        {
                            mrg.Append("找不到" + time[j] + "的批次" + "\n");
                        }

                    }
            }
            if (mrg.ToString() != "")
            {
                MessageBox.Show(mrg.ToString(),"请到时间规则维护");
            }
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text;
        }

        private void Pic_Min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void textBox3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (numericUpDown2.Value == 0)
            {
                MessageBox.Show("请先修改每日次数");
            }
            else 
            {
            j = textBox3.Text;
            Time_Text freg_rule = new Time_Text();
            freg_rule.ShowDialog(this);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
