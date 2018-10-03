using System;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace PivasRevPre
{
    public partial class CPConfirm : Form
    {
        public CPConfirm()
        {
            InitializeComponent();
        }

        public CPConfirm(string model)
        {
            InitializeComponent();
            this.model = model;
            label4.Text = model;
            //this.DefEcode = DefEcode;
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

        string model;
        public string DoctorExplain;
        public string ecode = "", epass = "", eid = "";
        DB_Help DB = new DB_Help();

        private void btnOK_Click(object sender, EventArgs e)
        {            
            ecode = txtCode.Text;
            epass = txtPass.Text;
            if (txtCode.Text.Trim() == "" && CheckPre.Confirmation == "1")
            {
                MessageBox.Show("帐号不可为空");
                return;
            }
            string str = "";
            string str1 = string.Empty;
            if (CheckPre.Confirmation == "1")
            {
                str = "select * from DEmployee where AccountID = '" + ecode + "' and Pas = '" + epass + "'";
                str1 = "select *from ManageLimit left join DEmployee de on de.DEmployeeID=ManageLimit.DEmployeeID  where de.AccountID='"+ecode+"' and LimitName='02001'";
            
            }
            else
            {
                str = "select * from DEmployee where  DEmployeeID= '" + CheckPre.employeeID + "'";
                str1 = "select *from ManageLimit left join DEmployee de on de.DEmployeeID=ManageLimit.DEmployeeID  where  de.DEmployeeID= '" + CheckPre.employeeID + "' and LimitName='02001'";
            }
            DataSet ds = DB.GetPIVAsDB(str);
            DataSet ds1 = DB.GetPIVAsDB(str1);
            if (ds != null && ds.Tables[0].Rows.Count > 0&&ds1!=null&&ds1.Tables[0].Rows.Count>0)
            {
                eid = ds.Tables[0].Rows[0]["DEmployeeID"].ToString();
                DoctorExplain = richTextBox1.Text.Replace("'", "");
                this.DialogResult = DialogResult.OK;
            }
            else if(ds==null||ds.Tables[0].Rows.Count==0)
            {
                MessageBox.Show("用户不存在或密码错误");
                return;
            }
            else if (ds1 == null || ds1.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("该用户没有权限");
                return;
            }

        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void CPConfirm_Load(object sender, EventArgs e)
        {
            txtCode.Focus();
            if (CheckPre.Confirmation == "0")
            {
                txtCode.Visible = false;
                txtPass.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
            }
        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (Char)Keys.Enter)
                {

                    string str = "select * from QRcodeLog where QRcode = '" + txtCode.Text + "' and DelDT IS NULL";
                    DataSet ds = DB.GetPIVAsDB(str);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        eid = ds.Tables[0].Rows[0]["DEmployeeID"].ToString();
                        DoctorExplain = richTextBox1.Text.Replace("'", "");
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        txtPass.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    ecode = txtCode.Text;
                    epass = txtPass.Text;
                    if (txtCode.Text.Trim() == "")
                    {
                        MessageBox.Show("帐号不可为空");
                        return;
                    }
                    string str = "select * from DEmployee where AccountID = '" + ecode + "' and Pas = '" + epass + "'";
                    DataSet ds = DB.GetPIVAsDB(str);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        eid = ds.Tables[0].Rows[0]["DEmployeeID"].ToString();
                        DoctorExplain = richTextBox1.Text.Replace("'", "");
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("用户不存在或密码错误");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
