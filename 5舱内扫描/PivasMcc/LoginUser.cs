using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace PivasMcc
{
    public partial class LoginUser : Form
    {
        int I;

        public LoginUser()
        {
            InitializeComponent();
        }

        public LoginUser(int i)
        {
            InitializeComponent();
            this.I = i;
        }

        public delegate void mydelegate(string user,string usercode);
        public static event mydelegate loginUser;
        public static string sss;
        private void button1_Click(object sender, EventArgs e)
        {
            DengLu();
        }

        private void DengLu()
        {
            DB_Help DB = new DB_Help();
            if (string.IsNullOrEmpty(txtDoc.Text))
            {
                MessageBox.Show("工号为空！");
                txtDoc.Focus();
                return;
            }
            DataTable dt = DB.GetPIVAsDB("SELECT * FROM DEmployee WHERE AccountID='" + txtDoc.Text + "' and [Pas]='" + txtPas.Text.Trim() + "'").Tables[0];
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("用户名或者密码错误！");
                txtPas.Text = null;
                txtDoc.Focus();
                return;
            }
            loginUser(dt.Rows[0]["DEmployeeName"].ToString(), dt.Rows[0]["DEmployeeID"].ToString());
            this.Dispose();
        }

        private void txtDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) 
            {
                if (txtDoc.Text.Trim() != "")
                {
                    txtPas.Focus();
                }
            }
        }

        private void txtPas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                DengLu();
            }
        }

        private void LoginUser_Load(object sender, EventArgs e)
        {
            if (I < 0)
            {
                MessageBox.Show("请先打开端口");
                this.Dispose();
            }
        }
    }
}
    