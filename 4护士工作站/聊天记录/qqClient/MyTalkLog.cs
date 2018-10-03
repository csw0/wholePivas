using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace qqClient
{
    public partial class MyTalkLog : UserControl
    {
        private string deName = string.Empty;
        private string time = string.Empty;
        private string talkContent = string.Empty;
        private string WardName = string.Empty;
        private string UserName = string.Empty;
        private string usertype = string.Empty;
        private string Totalk = string.Empty;
        public MyTalkLog(string Name, string Time, string Content,string wardName,string userName,string toTalk, string type)
        {
            InitializeComponent();
            this.deName = Name;
            this.time = Time;
            this.talkContent = Content;
            this.WardName = wardName;
            this.UserName = userName;
            this.usertype = type;
            this.Totalk = toTalk;
        }

      

        private void MyTalkLog_Load(object sender, EventArgs e)
        {
          
            //richTextBox1.Enabled = false;
            SetControlEnabled(this, false);
            richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            SetLineSpace(richTextBox1, 300);
            TalkShow();
            //JudgeShow();  
            PivasMateShow();
        }

        private void JudgeShow()
        {
            if (usertype== "PivasMate")
            {
                PivasMateShow();   
            }
            else
            {
                if (deName != UserName)
                {

                    if (WardName == "配置中心")
                    {
                        this.richTextBox1.BackColor = Color.GreenYellow;
                        this.panel2.BackColor = Color.GreenYellow;
                        //richTextBox1.BackColor = Color.Aquamarine;
                        //panel2.BackColor = Color.Aquamarine;

                    }

                    else
                    {
                        richTextBox1.BackColor = Color.Tomato;
                        panel2.BackColor = Color.Tomato;

                    }
                    richTextBox1.RightToLeft = RightToLeft.No;
                    panel1.Location = new Point(4, 4);
                    this.panel2.Location = new Point(14, 28);
                }

            }
        
        }
        /// <summary>
        /// 配置中心显示规则
        /// </summary>
        private void PivasMateShow()
        {
            if (deName != UserName)
            {
                if (WardName == "配置中心")
                {
                    this.richTextBox1.BackColor = Color.GreenYellow;
                    this.panel2.BackColor = Color.GreenYellow;
                }
                else
                {
                    this.richTextBox1.BackColor = Color.Aquamarine; 
                    this.panel2.BackColor = Color.Aquamarine;
                }
                this.panel2.Location = new Point(4, 28);
                richTextBox1.RightToLeft = RightToLeft.No;
                panel1.Location = new Point(4, 4);
            }
           
        }
        private void TalkShow()
        {
            label1.Text = deName;
            label2.Text = time;
            label3.Text = WardName;
            richTextBox1.Text = talkContent;

        }


        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            int EM_GETLINECOUNT = 0x00BA;//获取总行数的消息号
            int lc = SendMessage(this.richTextBox1.Handle, EM_GETLINECOUNT, IntPtr.Zero, "");
            int sf = (this.richTextBox1.Font.Height + 6) * lc;
          
            this.richTextBox1.Size = new Size(this.richTextBox1.Width, sf);
            this.panel2.Size = new Size(this.panel2.Width, sf + 3);
            this.Size = new Size(this.Width, panel2.Height + panel2.Top);

        }


        #region 设置richtextbox行间距

        public const int WM_USER = 0x0400;
        public const int EM_GETPARAFORMAT = WM_USER + 61;
        public const int EM_SETPARAFORMAT = WM_USER + 71;
        public const long MAX_TAB_STOPS = 32;
        public const uint PFM_LINESPACING = 0x00000100;
        [StructLayout(LayoutKind.Sequential)]
        private struct PARAFORMAT2
        {
            public int cbSize;
            public uint dwMask;
            public short wNumbering;
            public short wReserved;
            public int dxStartIndent;
            public int dxRightIndent;
            public int dxOffset;
            public short wAlignment;
            public short cTabCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public int[] rgxTabs;
            public int dySpaceBefore;
            public int dySpaceAfter;
            public int dyLineSpacing;
            public short sStyle;
            public byte bLineSpacingRule;
            public byte bOutlineLevel;
            public short wShadingWeight;
            public short wShadingStyle;
            public short wNumberingStart;
            public short wNumberingStyle;
            public short wNumberingTab;
            public short wBorderSpace;
            public short wBorderWidth;
            public short wBorders;
        }

        [DllImport("user32", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref PARAFORMAT2 lParam);

        /// <summary>
        /// 设置行距
        /// </summary>
        /// <param name="ctl">控件</param>
        /// <param name="dyLineSpacing">间距</param>
        public static void SetLineSpace(Control ctl, int dyLineSpacing)
        {
            PARAFORMAT2 fmt = new PARAFORMAT2();
            fmt.cbSize = Marshal.SizeOf(fmt);
            fmt.bLineSpacingRule = 4;// bLineSpacingRule;
            fmt.dyLineSpacing = dyLineSpacing;
            fmt.dwMask = PFM_LINESPACING;
            try
            {
                SendMessage(new HandleRef(ctl, ctl.Handle), EM_SETPARAFORMAT, 0, ref fmt);
            }
            catch
            {

            }
        }
        #endregion 

        #region enabled设为false后改变控件颜色

        [System.Runtime.InteropServices.DllImport("user32.dll ")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int wndproc);
        [System.Runtime.InteropServices.DllImport("user32.dll ")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        public const int GWL_STYLE = -16;
        public const int WS_DISABLED = 0x8000000;

        public static void SetControlEnabled(Control c, bool enabled)
        {
            if (enabled)
            { SetWindowLong(c.Handle, GWL_STYLE, (~WS_DISABLED) & GetWindowLong(c.Handle, GWL_STYLE)); }
            else
            { SetWindowLong(c.Handle, GWL_STYLE, WS_DISABLED + GetWindowLong(c.Handle, GWL_STYLE)); }
        }
        #endregion

      
    }
}
