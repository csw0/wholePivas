using System;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace EmployeeManage
{
    public partial class frmLimit : Form
    {
        public frmLimit()
        {
            InitializeComponent();
        }

        public frmLimit(string DEmployeeID)
        {
            InitializeComponent();
            this.DEmployeeID = DEmployeeID;
        }

        private DB_Help db = new DB_Help();
        private string DEmployeeID;
        public string Limitstr;

        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        private string UserID;
        #endregion

        private void Checkpre_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Checkpre.Checked)
                {                    
                    CheckPrePass.Enabled = true;                    
                    CheckPreBack.Enabled = true;                    
                    CheckPreReCheck.Enabled = true;
                }
                else
                {
                    CheckPrePass.Checked = false;
                    CheckPrePass.Enabled = false;
                    CheckPreBack.Checked = false;
                    CheckPreBack.Enabled = false;
                    CheckPreReCheck.Checked = false;
                    CheckPreReCheck.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmLimit_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void frmLimit_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(DEmployeeID))
                {
                    string str = "select * from ManageLimit where DEmployeeID = " + DEmployeeID;
                    DataSet ds = new DataSet();
                    ds = db.GetPIVAsDB(str);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            for (int j = 0; j < pnlLimit.Controls.Count; j++)
                            {
                                if (((CheckBox)(pnlLimit.Controls[j])).Tag.ToString() == ds.Tables[0].Rows[i]["LimitName"].ToString())
                                {
                                    ((CheckBox)(pnlLimit.Controls[j])).Checked = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
      

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public string getLimitStr()
        {
            Limitstr = "";
            for (int i = 0; i < pnlLimit.Controls.Count; i++)
            {
                if (((CheckBox)(pnlLimit.Controls[i])).Checked)
                {
                    Limitstr = Limitstr + "," + pnlLimit.Controls[i].Tag;
                }
            }
            if (Limitstr != "")
            {
                Limitstr = Limitstr.Substring(1, Limitstr.Length - 1);
            }
            return Limitstr;
        }

        private void CanCheck_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "13816350872")
            {
                CheckPreBack.Visible = true;      //同步设置
                checkBox1.Visible = true;         //审方设置
                checkBox19.Visible = true;        //打印设置
                checkBox4.Visible = true;         //工具画面编辑
            }
            else
            {
                CheckPreBack.Visible =false ;     //同步设置
                checkBox1.Visible = false;        //审方设置
                checkBox19.Visible = false;       //打印设置
                checkBox4.Visible = false;        //工具画面编辑
            }
        }
    }
}
