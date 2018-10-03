using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace EmployeeManage
{
    public partial class updateEmployee : Form
    {
        DB_Help db = new DB_Help();
        DataTable dt = new DataTable();
        frmLimit f = new frmLimit();

        public static string uAccountID;
        public static string uPas;
        public static string uID;
        public static string uPosition;
        public static string uDEmployeeName;
        public static string uDEmployeeCode;
        public static bool uIsvalid;
        public static string utype;

        public string Limitstr="";

        public updateEmployee()
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


        private void save_Click(object sender, EventArgs e)//保存更新
        {
            string V,pasword;

            try
            {
                if (Pas.Text != "")
                    //pasword = db.Encrypt(Pas.Text);
                    pasword = Pas.Text;
                else
                    pasword = "";

                if (IsValid.Checked)
                    V = "1";
                else
                    V = "0";

                utype = comboBox1.SelectedIndex.ToString();
                updateE(Account.Text, pasword, Position.Text, EName.Text, ECode.Text, V, FormEmployee.ID, utype);


                string str = "Delete from ManageLimit where DEmployeeID = " + FormEmployee.ID;
                db.GetPIVAsDB(str);

                if (LimitShow)
                {
                    Limitstr = f.getLimitStr();
                    f.Dispose();
                }
                if (Limitstr != "")
                {
                    string[] l = Limitstr.Split(',');
                    for (int i = 0; i < l.Length; i++)
                    {
                        str = "insert into ManageLimit values(" + FormEmployee.ID + ",'" + l[i] + "')";
                        db.SetPIVAsDB(str);
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
            this.Close();
        }

        private void updateEmployee_Load(object sender, EventArgs e)//显示要修改的员工信息
        {
            
            try
            {
                switch (FormEmployee.CIndex)
                {
                    case 2: Pas.Enabled=true; break;
                    case 3: Account.Enabled = true; break;
                    case 5: EName.Enabled = true; break;
                    case 6: ECode.Enabled = true; break;
                    case 7: Position.Enabled = true; break;
                    case 8: IsValid.Enabled = true; break;
                }
                utype = FormEmployee.EType;

                switch(utype)
                {
                    case "":
                        comboBox1.SelectedIndex=0;
                        break;
                    default:
                        comboBox1.SelectedIndex=Convert.ToInt32(utype);
                        break;
                        
                }
                Account.Text = FormEmployee.AccountID;
                Position.Text = FormEmployee.Position;
                EName.Text = FormEmployee.DEmployeeName;
                ECode.Text = FormEmployee.DEmployeeCode;
                Pas.Text = FormEmployee.Pas;
                if ("True" == FormEmployee.Isvalid)
                    IsValid.Checked = true;
                else
                    IsValid.Checked = false;
                Button();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误：" + ex.Message);
            }
        }

        private void updateE(string strA, string strPa, string strPo, string strN, string strC, string strV, string strID,string strtype)//更新数据库中的员工信息
        {
            try
            {
                //string str = "update DEmployee set AccountID ='" + strA + "', Pas = '" + strPa + "', Position = '" + strPo + "', DEmployeeName = '" +
                //        strN + "', DEmployeeCode ='" + strC + "', IsValid = '" + strV + "',Type = "+strtype+" where DEmployeeID = " + strID;
                string str = "update DEmployee set AccountID ='" + strA + "', Position = '" + strPo + "', DEmployeeName = '" +
               strN + "', DEmployeeCode ='" + strC + "', IsValid = '" + strV + "',Type = " + strtype + " where DEmployeeID = " + strID;
                db.SetPIVAsDB(str);

                uAccountID = strA;
                uPas = strPa;
                uPosition = strPo;
                uDEmployeeName = strN;
                uDEmployeeCode = strC;
                utype = strtype;
                if ("1" == strV)
                    uIsvalid = true;
                else
                    uIsvalid = false;
                uID = strID;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Account_KeyPress(object sender, KeyPressEventArgs e)//响应回车确定
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                //string pasword = db.Encrypt(Pas.Text);
                string pasword = Pas.Text;
                string V;
                try
                {
                    if (IsValid.Checked)
                        V = "1";
                    else
                        V = "0";
                    updateE(Account.Text, pasword, Position.Text, EName.Text, ECode.Text, V, FormEmployee.ID,utype);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
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

        private void lblSave_MouseHover(object sender, EventArgs e)
        {
            lblSave.BackColor = Color.FromArgb(17, 161, 227);
        }

        private void lblSave_MouseLeave(object sender, EventArgs e)
        {
            lblSave.BackColor = Color.FromArgb(19, 179, 253);
        }

        bool LimitShow = false;

        private void button1_Click(object sender, EventArgs e)
        {
          Button();
        }

          private void Button ()
          {
            try
            {
                if (!LimitShow)
                {
                    f = new frmLimit(FormEmployee.ID);
                    f.Location = new Point(this.Right, this.Top);
                    LimitShow = true;
                    button1.Text = "权限<<";
                    f.Show();
                }
                else
                {
                    Limitstr = "";
                    Limitstr = f.getLimitStr();                    

                    button1.Text = "权限>>";
                    LimitShow = false;
                    f.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          }
        private void button1_LocationChanged(object sender, EventArgs e)
        {
            f.Location = new Point(this.Right, this.Top);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string Pass=db.Encrypt("8888");
            string Pass = "8888";
            string str = " update DEmployee set pas='"+Pass+"' where DEmployeeID =" + FormEmployee.ID;
            db.SetPIVAsDB(str);
            MessageBox.Show(" 密码初始化完成 ！！！");
        }


    }
}
