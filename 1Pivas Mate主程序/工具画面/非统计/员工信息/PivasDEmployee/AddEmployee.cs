using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace EmployeeManage
{
    public partial class AddEmployee : Form
    {        
        DB_Help db = new DB_Help();
        DataTable dt = new DataTable();
        frmLimit f = new frmLimit();
        public string Limitstr="";

        public AddEmployee()
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


        private void Add_Click(object sender, EventArgs e)//增加数据库员工信息，同时界面刷新
        {
            try
            {
                string Isvalid, pasword, type="";
                if (Pas.Text != "")
                    // pasword = db.Encrypt(Pas.Text);
                    pasword = Pas.Text;
                else
                    pasword = "";

                if (IsValid.Checked)
                    Isvalid =  "1";                
                else
                    Isvalid = "0";
                switch(comboBox1.SelectedIndex)
                {
                    case 0:
                        type = "0";
                        break;
                    case 1:
                        type = "1";
                        break;
                    case 2:
                        type = "2";
                        break;
                    case 3:
                        type = "3";
                        break;
                }
                //db.Decrypt
                string str = "insert into DEmployee values('" + Account.Text + "','" + pasword + "','" + Position.Text + "','" + ECode.Text +
                         "','" + EName.Text + "','" + Isvalid + "'," + type + ")";

                FormEmployee form = (FormEmployee)this.Owner;                
                db.SetPIVAsDB(str);                
                form.refresh();

                if (LimitShow)
                {
                    Limitstr = f.getLimitStr();
                    f.Dispose();
                }
                str = "select DEmployeeID from DEmployee where AccountID = '" + Account.Text + "'";
                DataSet ds = db.GetPIVAsDB(str);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string eid = ds.Tables[0].Rows[0]["DEmployeeID"].ToString();
                    if (Limitstr != "")
                    {
                        string[] l = Limitstr.Split(',');
                        for (int i = 0; i < l.Length; i++)
                        {
                            str = "insert into ManageLimit values(" + eid + ",'" + l[i] + "')";
                            db.SetPIVAsDB(str);
                        }
                    }
                }


                this.DialogResult = DialogResult.OK;
                this.Close();
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }                       
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            if (LimitShow)
            {
                f.Dispose();
            }
            this.Dispose();
            //this.Close();
        }

        private void Panel_Close_MouseHover(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.FromArgb(86, 160, 255);
        }

        private void Panel_Close_MouseLeave(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.FromArgb(16, 107, 225);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void lblAdd_MouseHover(object sender, EventArgs e)
        {
            lblAdd.BackColor = Color.FromArgb(17, 161, 227);
        }

        private void lblAdd_MouseLeave(object sender, EventArgs e)
        {
            lblAdd.BackColor = Color.FromArgb(19, 179, 253);
        }

        private void AddEmployee_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }


        bool LimitShow = false;
        private void btnLimit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!LimitShow)
                {
                    f = new frmLimit();

                    f.Left = this.Right;
                    f.Top = this.Top;

                    f.Location = new Point(this.Right, this.Top);

                    //MessageBox.Show((this.Right).ToString());
                    //MessageBox.Show(f.Left.ToString());
                    LimitShow = true;
                    btnLimit.Text = "权限<<";
                    f.Show();
                }
                else
                {
                    Limitstr = "";
                    Limitstr = f.getLimitStr();                    
                    f.Dispose();
                    btnLimit.Text = "权限>>";
                    LimitShow = false;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void AddEmployee_LocationChanged(object sender, EventArgs e)
        {
            f.Location = new Point(this.Right, this.Top);
        }      
    }
}
