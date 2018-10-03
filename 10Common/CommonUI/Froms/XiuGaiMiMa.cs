using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CommonUI.Controls;
using PIVAsCommon.Helper;

namespace CommonUI.Froms
{
    public partial class XiuGaiMiMa : Form
    {
        DB_Help db = new DB_Help();
        public string AccountID;

        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion

        public XiuGaiMiMa()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != textBox2.Text.Trim())
            { MessageBox.Show("两次输入新密码不一致！"); }
            else
            {
                StringBuilder Sb = new StringBuilder();
                Sb.Append("UPDATE DEmployee ");
                Sb.Append("SET Pas = '");
                Sb.Append(textBox1.Text.Trim());
                Sb.Append("' ");
                Sb.Append(" WHERE  ");
                Sb.Append("[DEmployeeID] =  '");
                Sb.Append(AccountID.Trim());
                Sb.Append("' ");
                db.SetPIVAsDB(Sb.ToString());
                this.Dispose();
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Keyboard.CloseFrom();
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);    
        }
    }
}
