using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace EmployeeManage
{
    public partial class deleteEmployee : Form
    {
        
        DB_Help db = new DB_Help();
        DataTable dt = new DataTable();       

        public deleteEmployee()
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

        private void updateDEmployee_Load(object sender, EventArgs e)//显示要删除的员工信息
        {            
            try
            {
                Account.Text = FormEmployee.AccountID;
                Position.Text = FormEmployee.Position;
                EName.Text = FormEmployee.DEmployeeName;
                ECode.Text = FormEmployee.DEmployeeCode;                
            }
            catch(Exception ex)
            {
                MessageBox.Show("错误：" + ex.Message);
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Panel_Close_MouseHover(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.FromArgb(86, 160, 255);
        }

        private void Panel_Close_MouseLeave(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.FromArgb(255, 128, 128);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void lblDelete_Click(object sender, EventArgs e)//返回值 OK
        {
            this.DialogResult = DialogResult.OK;
        }

        private void lblDelete_MouseHover(object sender, EventArgs e)
        {
            lblDelete.BackColor = Color.Red ;
        }

        private void lblDelete_MouseLeave(object sender, EventArgs e)
        {
            lblDelete.BackColor = Color.FromArgb(19, 179, 253);
        }
     
    }
}
