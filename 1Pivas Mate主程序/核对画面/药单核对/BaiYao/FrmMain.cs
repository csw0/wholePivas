using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace BaiYaoCheck
{
    public partial class FrmMain : Form
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
        DB_Help db = new DB_Help();
       
        SQL Mysql = new SQL();

        private string code = "";
        private int a;
        public FrmMain()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                label11.ForeColor = Color.Blue;
                label10.ForeColor = Color.Red;
                panel3.Controls.Clear();
                ChangeDate ch = new ChangeDate();
                panel3.Controls.Add(ch);
                label2.Text = "9999";
            }
            catch (Exception ex)
            {
                ShowMsgHelper.ShowError("摆药核对主界面加载失败:" + ex.Message);
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.Dispose();
        } 

        private void linkLabel_Click(object sender, EventArgs e)
        {
           
            label11.ForeColor = Color.Blue;
            label10.ForeColor = Color.Blue;
            panel3.Controls.Clear();
            DosageDiffer dd = new DosageDiffer();
            //dd.Dock = DockStyle.Fill;
            panel3.Controls.Add(dd);
        }

        private void label10_Click(object sender, EventArgs e)
        {
          
            label11.ForeColor = Color.Blue;
            label10.ForeColor = Color.Red;
            label4.ForeColor = Color.Blue;
            panel3.Controls.Clear();

            ChangeDate ch = new ChangeDate();
            panel3.Controls.Add(ch);

        }

        private void label11_Click(object sender, EventArgs e)
        {
           
            label11.ForeColor = Color.Red;
            label10.ForeColor = Color.Blue;
            label4.ForeColor = Color.Blue;
            panel3.Controls.Clear();
            DrugKindDiffer dkd = new DrugKindDiffer();
            dkd.Dock = DockStyle.Fill;
            panel3.Controls.Add(dkd);
        }

      

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Red;
            label11.ForeColor = Color.Blue;
            label10.ForeColor = Color.Blue;
            panel3.Controls.Clear();
            ChangeSpec cs = new ChangeSpec();
            cs.Dock = DockStyle.Fill;
            panel3.Controls.Add(cs);
        }
    }
}
