using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PivasRevPre
{
    public partial class BackPre : Form
    {
        public BackPre()
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

        public string strExplain;
        private void btnOK_Click(object sender, EventArgs e)
        {
            strExplain = richTextBox1.Text.Replace("'","");
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==(Char)Keys.Enter)
            {
                strExplain = richTextBox1.Text;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
    }
}
