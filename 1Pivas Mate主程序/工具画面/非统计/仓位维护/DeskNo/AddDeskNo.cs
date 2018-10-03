using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace DeskNo
{
    public partial class AddDeskNo : Form
    {
        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion
        public AddDeskNo()
        {
            InitializeComponent();
        }
        DB_Help db = new DB_Help();
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null&&textBox1.Text!="")
            {
                StringBuilder str = new StringBuilder();
                str.Append("if not exists(select *from IVRecordDeskSet where DeskNo='");
                str.Append(textBox1.Text);
                str.Append("')  ");
                str.Append("insert into IVRecordDeskSet ([DeskNo],[IsPTY],[IsKSS],[IsHLY],[IsYYY],[IsOpen]) values('");
                str.Append(textBox1.Text);
                str.Append("',1,1,1,1,1)");
                int a = db.SetPIVAsDB(str.ToString());
                if (a == 1)
                {
                    MessageBox.Show("插入成功");                  
                }
                else
                {
                    MessageBox.Show("更新失败");
                }
                this.Close();
            }
            else
            {
                this.Close();

            }
        }

 

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

       

    }
}
