using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PIVAS_MATE
{
    public partial class frmNotify : Form
    {
        private string userID;
        public frmNotify(string userID)
        {
            this.userID = userID;
            InitializeComponent();
            this.TopMost = true;
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

        public delegate void MouseOn(bool On);
        public event MouseOn setMouseOn;

        public delegate void RemindTime(int time, int keetime);
        public event RemindTime setRemindTime;

        public delegate void DelayTime(int time,int delayindex);
        public event DelayTime setDelayTime;

        bool Init = false;

        private DB_Help dbHelp = new DB_Help();

        private void frmNotify_MouseHover(object sender, EventArgs e)
        {
            setMouseOn(true);
        }

        private void frmNotify_MouseLeave(object sender, EventArgs e)
        {
            setMouseOn(false);
        }

        /// <summary>
        /// 设置窗口信息
        /// </summary>
        /// <param name="ds">显示的数据</param>
        /// <param name="index1">间隔时间选项 combobox index</param>
        /// <param name="index2">停留时间选项 combobox index</param>
        /// <returns></returns>
        public bool SetRemindForm(DataSet ds,int index1,int index2,int Delayindex)
        {
            try 
            {
                if (ds == null)
                {
                    return false;
                }
                else
                {
                    Init = true;
                    cbbtime.SelectedIndex = index1;
                    cbbKeeptime.SelectedIndex = index2;
                    comboBox3.SelectedIndex = Delayindex;
                    Init = false;
                    lblPass.Text = ds.Tables[0].Rows[0][0].ToString();
                    lblUnPass.Text = ds.Tables[0].Rows[0][1].ToString();
                    lblToday.Text = ds.Tables[0].Rows[1][0].ToString();
                    lblnextday.Text = ds.Tables[0].Rows[1][1].ToString();
                    lblUncheck.Text = ds.Tables[0].Rows[2][0].ToString();
                    lblTodaypack.Text = ds.Tables[0].Rows[3][0].ToString();
                    lblnextDaypack.Text = ds.Tables[0].Rows[3][1].ToString();
                    lblTodayback.Text = ds.Tables[0].Rows[4][0].ToString();
                    lblNextdayback.Text = ds.Tables[0].Rows[4][1].ToString();

                    if (ds.Tables[0].Rows[5][0].ToString() == "0")//提前打包
                    {
                        label12.Visible = false;
                        label15.Visible = false;
                    }
                    else
                    {
                        label12.Visible =true ;
                        label15.Visible = true;
                        label15.Text = ds.Tables[0].Rows[6][0].ToString();
                    }

                    if (ds.Tables[0].Rows[6][0].ToString() == "0")//配置取消
                    {
                        label20.Visible = false;
                        label21.Visible = false;
                    }
                    else
                    {
                        label20.Visible = true;
                        label21.Visible = true;
                        label21.Text = ds.Tables[0].Rows[7][0].ToString();
                    }

                    return true;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void cbbtime_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                label11.Focus();
                if (!Init)
                {
                    dbHelp.IniWriteValuePivas("Remind", "RemindTime", cbbtime.Text);
                    dbHelp.IniWriteValuePivas("Remind", "KeepTime", cbbKeeptime.Text);
                    setRemindTime(Convert.ToInt32(cbbtime.Text.Trim()) * 60, Convert.ToInt32(cbbKeeptime.Text.Trim()));
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            label11.Focus();
            switch(comboBox3.SelectedIndex)
            {
                case 0: setDelayTime(1800,0); break;
                case 1: setDelayTime(3600,1); break;
                case 2: setDelayTime(0,2); break;
            }           
        }

        private void label12_Click(object sender, EventArgs e)
        {
            this.Close();
        }
 
        private void Panel_Close_MouseHover(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.FromArgb(237, 51, 31);
            setMouseOn(true);
        }

        private void Panel_Close_MouseLeave(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.FromArgb(194, 224, 233);
            setMouseOn(false);
        }

        private void label11_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }


        /// <summary>
        /// 配置取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label21_Click(object sender, EventArgs e)
        {
            using (Process p = new Process())
            {
                p.StartInfo.WorkingDirectory = Application.StartupPath;
                p.StartInfo.FileName = "PivasLabelNoCZ.exe";
                p.StartInfo.Arguments = userID + " 1";
                p.Start();
            }
        }

        /// <summary>
        /// 提前打包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label15_Click(object sender, EventArgs e)
        {
            using (Process p = new Process())
            {
                p.StartInfo.WorkingDirectory = Application.StartupPath;
                p.StartInfo.FileName = "PivasLabelNoCZ.exe";
                p.StartInfo.Arguments = userID + " 0";
                p.Start();
            }
        }
    }
}
