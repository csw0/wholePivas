using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PivasLabelSelect
{
    public partial class User : Form
    {
        private DB_Help db = new DB_Help();
        public User()
        {
            InitializeComponent();
        }
        #region    移动窗体用到的变量
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MOVE = 0xF010;
        private const int HTCAPTION = 0x0002;
        private const int CS_DropSHADOW = 0x20000;
        private const int GCL_STYLE = (-26);
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        [DllImport("user32.dll")]
        private static extern int SetClassLong(IntPtr hwnd, int nlndex, int dwNewLong);
        [DllImport("user32.dll")]
        private static extern int GetClassLong(IntPtr hwnd, int nlndex);

        #endregion

        private void User_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

      
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = string.Format("select DEmployeeID from DEmployee where DEmployeeCode='{0}' and Pas='{1}'", textBox1.Text, textBox2.Text);
            DataSet ds = db.GetPIVAsDB(sql);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                this.Tag = ds.Tables[0].Rows[0][0].ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                label1.Visible = true;
            }

        }

    
      

      

    }
}
