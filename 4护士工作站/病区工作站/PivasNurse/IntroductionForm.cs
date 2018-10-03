using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PivasNurse
{
    public partial class IntroductionForm : Form
    {
        public IntroductionForm()
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

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void Panel_Close_MouseHover(object sender, EventArgs e)
        {
            //Panel_Close.BackColor = Color.FromArgb(84, 199, 253);
            Panel_Close.BackColor = Color.FromArgb(247, 141, 136);
            //Panel_Close.BackColor = Color.FromArgb(240, 171, 170);
        }

        private void Panel_Close_MouseLeave(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.Transparent;
        }

        private void Panel_Close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
