using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace PivasBatchDiff
{
    public partial class Confirm : Form
    {
        public Confirm()
        {
            InitializeComponent();
        }

        public Confirm(string rmk2)
        {
            InitializeComponent();
            rmk = rmk2;
         

        }
        const int HTLEFT = 10;
        const int HTRIGHT = 11;
        const int HTTOP = 12;
        const int HTTOPLEFT = 13;
        const int HTTOPRIGHT = 14;
        const int HTBOTTOM = 15;
        const int HTBOTTOMLEFT = 0x10;
        const int HTBOTTOMRIGHT = 17;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0084:
                    base.WndProc(ref m);
                    Point vPoint = new Point((int)m.LParam & 0xFFFF,
                        (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMLEFT;
                        else m.Result = (IntPtr)HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)HTRIGHT;
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)HTBOTTOM;
                    break;
                case 0x0201://鼠标左键按下的消息 
                    m.Msg = 0x00A1;//更改消息为非客户区按下鼠标 
                    m.LParam = IntPtr.Zero;//默认值 
                    m.WParam = new IntPtr(2);//鼠标放在标题栏内 
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
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

        string rmk;
        public string DoctorExplain;
        public string ecode = "", epass = "";
        DB_Help DB = new DB_Help();

        public delegate void NewDelegate(string ecode, string txt,string rmk);
        public event NewDelegate check;

        private void btnOK_Click(object sender, EventArgs e)
        {            
            ecode = txtCode.Text;
            epass = txtPass.Text;
            if (txtCode.Text.Trim()=="")
            {
                MessageBox.Show("帐号不可为空");
                return;
            }
            string str = "select * from DEmployee where AccountID = '" + ecode + "' and Pas = '" + epass + "'";
            DataSet ds = DB.GetPIVAsDB(str);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ecode = ds.Tables[0].Rows[0]["DEmployeeID"].ToString();
                DoctorExplain = richTextBox1.Text.Replace("'", "");
                //this.DialogResult = DialogResult.OK;
                check(ecode, DoctorExplain,rmk);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("用户不存在或密码错误");
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
                        ecode = ds.Tables[0].Rows[0]["DEmployeeID"].ToString();
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
                        ecode = ds.Tables[0].Rows[0]["DEmployeeID"].ToString();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
