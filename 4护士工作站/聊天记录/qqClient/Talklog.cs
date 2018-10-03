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
    public partial class Talklog : UserControl
    {
        private string UserName = string.Empty;
        private string time = string.Empty;
        private string talkContent = string.Empty;
        public Talklog(string Name,string Time,string Content)
        {
            InitializeComponent();
            this.UserName = Name;
            this.time = Time;
            this.talkContent = Content;
        }

        private void log_Load(object sender, EventArgs e)
        {
            SetLineSpace(richTextBox1, 300);
            TalkShow();
        }
        private void TalkShow()
        {
            label1.Text =UserName;
            label2.Text = time;
            richTextBox1.Text = talkContent;
        }
        
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            int EM_GETLINECOUNT = 0x00BA;//获取总行数的消息号
            int lc = SendMessage(this.richTextBox1.Handle, EM_GETLINECOUNT, IntPtr.Zero, "");
            int sf = (this.richTextBox1.Font.Height+6) * lc;
           
                this.richTextBox1.Size= new Size(this.Width-5, sf);

                this.Size = new Size(this.Width, richTextBox1.Height + richTextBox1.Top);

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

    }
}
