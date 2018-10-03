using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DFregManage;
using System.Runtime.InteropServices;

namespace PivasFreqRule
{
    public partial class Freg_Rule : Form
    {
        public Freg_Rule()
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

        updatedeletedb update = new updatedeletedb();
        seldb sel = new seldb();
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 初始化时间选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Freg_Rule_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 12; i++) 
            {
                time time = new time();
                time.row(i, sel.getDFregid(rowDFreg.i).Tables[0].Rows[0][4].ToString());
                time.Location = new Point(22,21 * i+10 );
                time.Name = "time" + i;
                panel1.Controls.Add(time);
            }
            for (int i = 12; i < 24; i++) 
            {
                time time = new time();
                time.row(i, sel.getDFregid(rowDFreg.i).Tables[0].Rows[0][4].ToString());
                time.Location = new Point(22,21 * (i - 12)+10);
                panel2.Controls.Add(time);
            }
        }

        /// <summary>
        /// 确定按钮，更改用法的使用时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            int k=0;
            string str="";
            foreach (Control c in  panel1.Controls) 
            {
                if (((time)c).checkBox1.Checked ==true)
                { 
                    str = str + ((time)c).label1.Text+"_";
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
            if (k > Convert.ToInt32(sel.getDFregid(rowDFreg.i).Tables[0].Rows[0][3]))
            {
                MessageBox.Show("选择过多");
            }
            else if (k < Convert.ToInt32(sel.getDFregid(rowDFreg.i).Tables[0].Rows[0][3]))
            { 
                MessageBox.Show("选择过少");
            }
            else if (str.Length == 0) 
            {
                update.updateTime(str, rowDFreg.i);
                FregRule(str);
            }
            else
            {

                update.updateTime(str.Remove(str.Length - 1), rowDFreg.i);
                FregRule(str);
            }
        }

        /// <summary>
        /// 同步更新批次
        /// </summary>
        /// <param name="str">用法时间</param>
        public void FregRule(string str) 
        {
           
            StringBuilder mrg = new StringBuilder();
           // DFeg form1 = (DFeg)this.Owner;
            string[] time = str.Split('_');
            for (int i = 0; i < time.Length-1; i++)
            {
                if (sel.getOrderID(time[i]).Tables[0].Rows.Count>0)
                {
                    string order = sel.getOrderID(time[i]).Tables[0].Rows[0][0].ToString();
                    string codeid = rowDFreg.i + (i+1);
                    update.updateFreqRule(time[i], order, rowDFreg.i, codeid);
                }
                else 
                {
                    mrg.Append( "找不到" + time[i] + "的批次"+"\n");
                }
                
            }
            if (mrg.ToString() != "")
            {
                MessageBox.Show(mrg.ToString());
            }
            else 
            {
                ChangeTextVal();
               // form1.refresh();
                this.Close();
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
