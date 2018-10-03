using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;
using PivasLimitDES;

/// <summary>
/// 
/// 
/// 
/// </summary>
namespace PivasFreqRule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PivasFreqRule : Form
    {
        public static string Choice;

        public PivasFreqRule()
        {
            InitializeComponent();
        }

        public PivasFreqRule(string ID)
        {
            InitializeComponent();
            UserID = ID;
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

        string RuleCode=string.Empty;
        //string TreeCode;
        CommonRule CommonRule;
        private string UserID = string.Empty;
        DB_Help db = new DB_Help();    

        private void label1_Common_Click(object sender, EventArgs e)
        {
            save(label_Common.Tag.ToString());
            panel_U.Controls.Clear();
            CommonRule = new CommonRule(RuleCode);
            panel_U.Controls.Add(CommonRule);
        }

        /// <summary>
        /// 药品优先规则模块的显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label2_Freq_Click(object sender, EventArgs e)
        {
            save(label_Freq.Tag.ToString());
            Choice = "medcine";
            panel_U.Controls.Clear();
            MedcineRule medcine = new MedcineRule();
            panel_U.Controls.Add(medcine);
        }

        private void save(string TreeCode)
        {
            panel_Common.BackColor=Color.Transparent;
            panel_Freq.BackColor = Color.Transparent;
            panel_Volume.BackColor = Color.Transparent;
            panel_Time.BackColor = Color.Transparent;
            panel4.BackColor = Color.Transparent;
            Label_freg.BackColor = Color.Transparent;
            panel7.BackColor = Color.Transparent;
            p_SpecialChar.BackColor = Color.Transparent;
            switch (TreeCode) 
            {
                case "0":
                    panel_Common.BackColor=Color.White;
                    break;
                case "1":
                    panel_Freq.BackColor = Color.White;
                    break;
                case "2":
                    panel_Volume.BackColor = Color.White;
                    break;
                case "3":
                    panel_Time.BackColor = Color.White;
                    break;
                case "4":
                    panel4.BackColor = Color.White;
                    break;
                case "5":
                    Label_freg.BackColor = Color.White;
                    break;
                case "6":
                    panel7.BackColor = Color.White;
                    break;
                case "7":
                    p_SpecialChar.BackColor = Color.White;
                    break;
                default:
                    break;
            }

        }

        private void panel_Freq_Click(object sender, EventArgs e)
        {
            save(panel_Freq.Tag.ToString());
            Choice = "medcine";
            panel_U.Controls.Clear();
            MedcineRule medcine = new MedcineRule();
            panel_U.Controls.Add(medcine);
        }

        private void panel_Volume_Click(object sender, EventArgs e)
        {
            save(panel_Volume.Tag.ToString());
            Choice = "volume";
            panel_U.Controls.Clear();
            VolumeRule Volume = new VolumeRule();
            panel_U.Controls.Add(Volume);
        }

        private void label_Volume_Click(object sender, EventArgs e)
        {
            save(label_Volume.Tag.ToString());
            Choice = "volume";
            panel_U.Controls.Clear();
            VolumeRule Volume = new VolumeRule();
            panel_U.Controls.Add(Volume);
        }
        private void label6_Click(object sender, EventArgs e)
        {
            save(label6.Tag.ToString());
            Choice = "warddrug";
            panel_U.Controls.Clear();
            WardDrug wd = new WardDrug();
            panel_U.Controls.Add(wd);

        }
        private void panel1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label_Time_Click(object sender, EventArgs e)
        {
            save(label_Time.Tag.ToString());
            panel_U.Controls.Clear();
            TimeRule time = new TimeRule();
            panel_U.Controls.Add(time);
            
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void Panel_Close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Panel_Close_MouseHover(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.FromArgb(255, 255, 192);
        }

        private void Panel_Close_MouseLeave(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.Transparent;
        }

        private void Pic_Min_MouseHover(object sender, EventArgs e)
        {
            Pic_Min.BackColor = Color.FromArgb(255, 255, 192);
        }

        private void Pic_Min_MouseLeave(object sender, EventArgs e)
        {
            Pic_Min.BackColor = Color.Transparent;
        }

        private void Pic_Min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void PivasFreqRule_Load(object sender, EventArgs e)
        {
            label1.Parent = this.pictureBox1;
            label9.Parent = this.pictureBox1;
            Pic_Min.Parent = this.pictureBox1;
            Panel_Close.Parent = this.pictureBox1;

            label3.Parent = this.pictureBox2;

            if (GetPivasLimit.Instance.Limit(UserID, "PivasFreqRule"))
            {
                DataSet nm = new DataSet();
                label9.Text = "";
                string str1 = "select DEmployeeName from DEmployee where DEMployeeID = " + UserID + "";
                nm = db.GetPIVAsDB(str1);
                if (nm != null && nm.Tables[0].Rows.Count > 0)
                {
                    label9.Text = nm.Tables[0].Rows[0][0].ToString();
                }
                Label_freg_Click(null, null);
            }
            else
            {
                MessageBox.Show("您没有操作权限，请与管理员联系！");
                this.Dispose();
            }
        }

        private void label_Common_MouseHover(object sender, EventArgs e)
        {
            if (panel_Common.BackColor != Color.White)
            {
                panel_Common.BackColor = Color.FromArgb(16, 107, 225); ;
            }
        }

        private void label_Common_MouseLeave(object sender, EventArgs e)
        {
            if (panel_Common.BackColor!=Color.White)
            {
                panel_Common.BackColor = Color.Transparent;
            }
        }

        private void label_Freq_MouseHover(object sender, EventArgs e)
        {

            if (panel_Freq.BackColor != Color.White)
            {
                panel_Freq.BackColor = Color.FromArgb(16, 107, 225); ;
            }
        }

        private void label_Freq_MouseLeave(object sender, EventArgs e)
        {
            if (panel_Freq.BackColor != Color.White)
            {
                panel_Freq.BackColor = Color.Transparent;
            } 
        }

        private void label_Volume_MouseHover(object sender, EventArgs e)
        {
            if (panel_Volume.BackColor != Color.White)
            {
                panel_Volume.BackColor = Color.FromArgb(16, 107, 225); ;
            }
        }

        private void label_Volume_MouseLeave(object sender, EventArgs e)
        {
            if (panel_Volume.BackColor != Color.White)
            {
                panel_Volume.BackColor = Color.Transparent;
            } 
        }

        private void label_Time_MouseHover(object sender, EventArgs e)
        {
            if (panel_Time.BackColor != Color.White)
            {
                panel_Time.BackColor = Color.FromArgb(16, 107, 225); ;
            }
        }

        private void label_Time_MouseLeave(object sender, EventArgs e)
        {
            if (panel_Time.BackColor != Color.White)
            {
                panel_Time.BackColor = Color.Transparent;
            } 
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            if (panel4.BackColor != Color.White)
            {
                panel4.BackColor = Color.FromArgb(16, 107, 225); ;
            }
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            if (panel4.BackColor != Color.White)
            {
                panel4.BackColor = Color.Transparent;
            } 
        }

        private void label2_Click(object sender, EventArgs e)
        {
            save(label2.Tag.ToString());
            panel_U.Controls.Clear();
            QiTa qt = new QiTa();
            panel_U.Controls.Add(qt);
        }

        /// <summary>
        /// 权限
        /// </summary>
        /// <param name="DEmployeeID"></param>
        /// <param name="LimitName"></param>
        /// <returns></returns>
        public bool Limit(string DEmployeeID, string LimitName)
        {
            try
            {
                DataSet ds = new DataSet();
                string str = "select * from ManageLimit where DEMployeeID = " + DEmployeeID + " and LimitName = '" + LimitName + "'";
                ds = db.GetPIVAsDB(str);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                ds.Dispose();
                return false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 用法模块加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_freg_Click(object sender, EventArgs e)
        {
            save(Label_freg.Tag.ToString());
            panel_U.Controls.Clear();
            DFreg df = new DFreg(UserID);
            panel_U.Controls.Add(df);
        }

        #region 内存回收

        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]

        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);

        /// <summary>

        /// 释放内存

        /// </summary>

        public static void ClearMemory()
        {

            GC.Collect();

            GC.WaitForPendingFinalizers();

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {

                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);

            }

        }
        #endregion

        /// <summary>
        /// 时间控件事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            ClearMemory();//释放内存
        }

        private void Label_freg_MouseLeave(object sender, EventArgs e)
        {
            if (Label_freg.BackColor != Color.White)
            {
                Label_freg.BackColor = Color.Transparent;
            } 
        }

        private void Label_freg_MouseHover(object sender, EventArgs e)
        {
            if (Label_freg.BackColor != Color.White)
            {
                Label_freg.BackColor = Color.FromArgb(16, 107, 225);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void label6_MouseHover(object sender, EventArgs e)
        {
            if (panel7.BackColor != Color.White)
            {
                panel7.BackColor = Color.FromArgb(16, 107, 225); ;
            }

        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {
            if (panel7.BackColor != Color.White)
            {
                panel7.BackColor = Color.Transparent;
            } 
        }

        private void lb_SpecialChar_MouseHover(object sender, EventArgs e)
        {
            if (p_SpecialChar.BackColor != Color.White)
            {
               p_SpecialChar.BackColor = Color.FromArgb(16, 107, 225); ;
            }
        }

        private void lb_SpecialChar_MouseLeave(object sender, EventArgs e)
        {
            if (p_SpecialChar.BackColor != Color.White)
            {
                p_SpecialChar.BackColor = Color.Transparent;
            }
        }

        private void lb_SpecialChar_Click(object sender, EventArgs e)
        {
            save(lb_SpecialChar.Tag.ToString());
            panel_U.Controls.Clear();
            SpecialChar sc = new SpecialChar();
            panel_U.Controls.Add(sc);
        }

      
    }
}
