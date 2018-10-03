using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DFregManage;

namespace PivasFreqRule
{
    public partial class Update_Times : Form
    {
        
        public Update_Times()
        {
            InitializeComponent();
        }
        seldb sel = new seldb();
        updatedeletedb ud = new updatedeletedb();

        public delegate void DelegateChangeTextValS();
        // 2.定义委托事件  
        public event DelegateChangeTextValS ChangeTextVal;
        private void Update_Times_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = sel.getDFregid(rowDFreg.i.ToString()).Tables[0];
            if (dt.Rows[0][3].ToString() == "")
            {
                numericUpDown2.Value = 0;
            }
            else
            {
                numericUpDown2.Value = Convert.ToInt32(dt.Rows[0][3]);
            }
        }
        /// <summary>
        /// 更新间隔时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDown2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //DFeg form1 = (DFeg)this.Owner;
                ud.updatDFreq2( numericUpDown2.Value.ToString(), rowDFreg.i);
                //form1.refresh();

                ChangeTextVal();
                this.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                numericUpDown2.Value--;
            }
            catch 
            {
                
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                numericUpDown2.Value++;
            }
            catch
            {

            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ud.updatDFreq2(numericUpDown2.Value.ToString(), rowDFreg.i);
            ChangeTextVal();
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
