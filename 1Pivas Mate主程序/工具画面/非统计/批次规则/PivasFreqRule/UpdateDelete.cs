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
    public partial class UpdateDelete : Form
    {
        public UpdateDelete()
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

        seldb sel = new seldb();
        updatedeletedb ud = new updatedeletedb();
        public delegate void DelegateChangeTextValS();
        // 2.定义委托事件  
        public event DelegateChangeTextValS ChangeTextVal;

        private void UpdateDelete_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = sel.getDFregid(rowDFreg.i.ToString()).Tables[0];
            if (dt.Rows[0][2].ToString() == "")
            {
                numericUpDown1.Value = 0; 
            }
            else
            {
                numericUpDown1.Value = Convert.ToInt32(dt.Rows[0][2]); 
            }
         
            
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DFeg form1 = (DFeg)this.Owner;
            ud.deleteDFreq(rowDFreg.i);
            form1.refresh();
            this.Close();
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
        /// <summary>
        /// 更新每日次数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue==13) 
            {
                DFeg form1 = (DFeg)this.Owner;
                ud.updatDFreq(numericUpDown1.Value.ToString(), rowDFreg.i);
                form1.refresh();
                this.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                numericUpDown1.Value--;
            }
            catch
            { 

            }
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                numericUpDown1.Value++;
            }
            catch 
            { 

            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //DFeg form1 = (DFeg)this.Owner;
            ud.updatDFreq(numericUpDown1.Value.ToString(), rowDFreg.i);
            ChangeTextVal();
            //form1.refresh();
            this.Close();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.FromArgb(224, 224, 224);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent;
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.FromArgb(224, 224, 224);
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.Transparent;
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.FromArgb(224, 224, 224);
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Transparent;
        }

    }
}
