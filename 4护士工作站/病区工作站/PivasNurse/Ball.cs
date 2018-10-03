using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace PivasNurse
{
    public partial class Ball : Form
    {
        private string wardCode;
        private string wardName;

        bool Init = false;
        DB_Help dbHelp = new DB_Help();
           /// <summary> 
        /// 构造方法 
        /// </summary> 
        /// <param name="INIPath">文件路径</param> 
        public delegate void MouseOn(bool On);
        public event MouseOn setMouseOn;

        public delegate void RemindTime(int time, int keetime);
        public event RemindTime setRemindTime;

        public delegate void DelayTime(int time, int delayindex);
        public event DelayTime setDelayTime;

 
        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion
        public Ball()
        {
            InitializeComponent();
        }
        public Ball(string WardName, string WardCode)
        {
            wardCode = WardCode;
            wardName = WardName;
            InitializeComponent();
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void label10_MouseHover(object sender, EventArgs e)
        {
            label10.BackColor = Color.Red;
        }

        private void label10_MouseLeave(object sender, EventArgs e)
        {
           label10.BackColor = SystemColors.Control;
        }

        private void Ball_Load(object sender, EventArgs e)
        {
            label1.Text = wardName + "病区";
            PivasNurseBall.PivasNurseBall pnb = new PivasNurseBall.PivasNurseBall();

            string[,] fn = new string[3, 4];
            fn = pnb.GetInfor(wardCode);
            label2.Text = fn[0, 0];
            label3.Text = fn[0, 1];
            label4.Text = fn[0, 2];
            label5.Text = fn[0, 3];
            label6.Text = fn[1, 0];
            label7.Text = fn[1, 1];
            label8.Text = fn[1, 2];
            label9.Text = fn[1, 3];

            btn1.Text = fn[2, 0];
            btn2.Text = fn[2, 1];
            btn3.Text = fn[2, 2];
            btn4.Text = fn[2, 3];
            btn1.Visible = !string.IsNullOrEmpty(fn[2, 0]);
            btn2.Visible = !string.IsNullOrEmpty(fn[2, 1]);
            btn3.Visible = !string.IsNullOrEmpty(fn[2, 2]);
            btn4.Visible = !string.IsNullOrEmpty(fn[2, 3]);
        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 设置窗口信息
        /// </summary>
        /// <param name="ds">显示的数据</param>
        /// <param name="index1">间隔时间选项 combobox index</param>
        /// <param name="index2">停留时间选项 combobox index</param>
        /// <returns></returns>
        public bool SetRemindForm( int index1, int index2, int Delayindex)
        {
            Init = true;
            cbbtime.SelectedIndex = index1;
            Init = false;
            cbbKeeptime.SelectedIndex = index2;
            comboBox3.SelectedIndex = Delayindex;
           
            return true;             
        }

        private void cbbtime_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               
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

        private void cbbKeeptime_SelectedIndexChanged(object sender, EventArgs e)
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
            switch (comboBox3.SelectedIndex)
            {
                case 0: setDelayTime(1800, 0); break;
                case 1: setDelayTime(3600, 1); break;
                case 2: setDelayTime(0, 2); break;
            }           
        }

        private void Ball_MouseHover(object sender, EventArgs e)
        {
            setMouseOn(true);
        }

        private void Ball_MouseLeave(object sender, EventArgs e)
        {
            setMouseOn(false);
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            PivasNurseBall.PivasNurseBall pnb = new PivasNurseBall.PivasNurseBall();
            pnb.Extra1(wardCode,wardName,"","","","");
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            PivasNurseBall.PivasNurseBall pnb = new PivasNurseBall.PivasNurseBall();
            pnb.Extra2(wardCode, wardName, "", "", "", "");
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            PivasNurseBall.PivasNurseBall pnb = new PivasNurseBall.PivasNurseBall();
            pnb.Extra3(wardCode, wardName, "", "", "", "");
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            PivasNurseBall.PivasNurseBall pnb = new PivasNurseBall.PivasNurseBall();
            pnb.Extra4(wardCode, wardName, "", "", "", "");
        }



    }
}
