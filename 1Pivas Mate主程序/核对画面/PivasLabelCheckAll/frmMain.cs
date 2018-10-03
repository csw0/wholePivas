using System;
using System.Data;
using System.Windows.Forms;

namespace PivasLabelCheckAll
{
    public partial class frmMain : Form
    {
        string SEQNO="";
        string UserID = "";
        string UserName = "";
        string CheckKind = "";

        public frmMain()
        {
            InitializeComponent();
        }

        public frmMain(string seqno,string userid,string username,string checkKind)
        {
            InitializeComponent();
            SEQNO = seqno;
            UserID = userid;
            UserName = username;
            CheckKind = checkKind;
            this.Text = CheckKind;
        }

        void LoginUser_loginUser(DataRow dr,string CheckKind)
        {
            this.Text = CheckKind;
            UCCommonCheck check = new UCCommonCheck(dr["DEmployeeID"].ToString(), dr["AccountID"].ToString(), dr["DEmployeeName"].ToString(),CheckKind);
            check.Dock = DockStyle.Fill;
            panel1.Controls.Add(check);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (SEQNO == null || SEQNO.Trim() == "")
            {
                LoginUser.loginUser += new LoginUser.mydelegate(LoginUser_loginUser);
                LoginUser ls = new LoginUser();
                ls.Owner = this;
                ls.ShowDialog();
            }
            else
            {
                UCCommonCheck check = new UCCommonCheck(SEQNO, UserID, UserName, CheckKind);
                check.Size = panel1.Size;
                check.Dock = DockStyle.Fill;
                panel1.Controls.Add(check);
            }
        }
    }
}
