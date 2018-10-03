using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CommonUI.Controls;
using PIVAsCommon;
using PivasInfor;
using PIVAsCommon.Helper;
using PIVAsCommon.Extensions;

namespace CommonUI.Froms
{
    public partial class Login : Form
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

        public string pwd = string.Empty;
        DB_Help db  = new DB_Help();
        //保证只有一个软键盘
        private Keyboard form = null;

        public Login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 画面拉动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Keyboard.CloseFrom();
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);            
        }

        private void Panel_Close_MouseHover(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.FromArgb(84, 199, 253);
        }

        private void Panel_Close_MouseLeave(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.Transparent;
        }

        private void Panel_Close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// 登录按钮鼠标放上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_MouseHover(object sender, EventArgs e)
        {
            label1.BackColor = Color.FromArgb(47, 193, 254);
        }

        /// <summary>
        /// 登录按钮鼠标离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.BackColor = Color.Transparent ;
        }

        /// <summary>
        /// 登录按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_Click(object sender, EventArgs e)
        {
            db = new DB_Help();
            try
            {
                if (TestDB())
                {
                    LoginProving(cbox_user.Text.Trim(), txt_pwd.Text.Trim());
                }
                else
                {
                    MessageBox.Show("  连接失败  ！！！");
                }
            }
            catch
            {
                MessageBox.Show("登陆失败! 未知错误。。。");
            }
        }

        public Boolean TestDB()
        {
            try
            {
                DataSet DS = db.GetPIVAsDB("select top 1 AccountID from DEmployee");
                if (DS != null)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show(" 数据库连接失败  ！！！");
                    return false; 
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("TestDB function error:"+ex.Message);
                return false;
            }
        }

        protected void LoginProving(string user, string pwd)
        {
            if (user == "")
            {
                MessageBox.Show("请输入登录用户名");
            }
            else
            {
                DataSet ds = db.GetPIVAsDB("select top 1 DEmployeeID,AccountID,Pas,DEmployeeCode,DEmployeeName from DEmployee where AccountID='" + user + "' and Pas='" + pwd + "'");

                if (ds==null||ds.Tables.Count==0||ds.Tables[0].Rows.Count <= 0)
                {
                    MessageBox.Show(" 登录失败，输入用户名与密码不匹配 ！！！（连接成功） ");                  
                }
                else if (ds.Tables[0].Rows.Count > 0)
                {
                    string User = cbox_user.Text;
                    if (user.Length >= 4 && user.Substring(0, 4) != "7777")//扫描二维码不保存本地
                    {
                        db.SaveXML(cbox_user.Text, txt_pwd.Text);
                    }
                    db.IniWriteValuePivas("User", "pwd", "");
                   
                    //信息保存，返回给主窗体使用
                    _DEmployeeID = ds.Tables[0].Rows[0]["DEmployeeID"].ToString();
                    _EmpCode = ds.Tables[0].Rows[0]["DEmployeeCode"].ToString();
                    _EmpName = ds.Tables[0].Rows[0]["DEmployeeName"].ToString();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        protected void LoginCheck(string user, string pwd)
        {
            if (user == "")
            {
                MessageBox.Show("请输入登录用户名");
            }
            else
            {
                DataSet ds = db.GetPIVAsDB("select top 1 DEmployeeID,AccountID,Pas,DEmployeeCode,DEmployeeName from DEmployee where AccountID='" + user + "' and Pas='" + pwd + "'");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    XiuGaiMiMa form = new XiuGaiMiMa();
                    form.AccountID = ds.Tables[0].Rows[0]["DEmployeeID"].ToString();
                    form.ShowDialog();
                }
                else 
                {
                    MessageBox.Show("用户名与密码不匹配，不可修改密码！");
                }
            }
        }

        /// <summary>
        /// 密码框鼠标移上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_MouseHover(object sender, EventArgs e)
        {
            panel3.BackColor = Color.Red;
        }

        /// <summary>
        /// 密码框鼠标离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_MouseLeave(object sender, EventArgs e)
        {
            panel3.BackColor = Color.FromArgb(19, 179, 253);           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            panel3.BackColor = Color.Red;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            panel3.BackColor = Color.FromArgb(19, 179, 253);
        }

        /// <summary>
        /// 登陆Load事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_Load(object sender, EventArgs e)
        {
            //根据软件类型，设置相关显示
            if (this._SoftType == SoftType.PivasMate)
            {
                panelHelp.Visible = true;
                this.Icon = global::CommonUI.Properties.Resources.PivasMate;
                labelSoftName.Text = "PIVAS MATE";
            }
            else
            {
                panelHelp.Visible = false;
                this.Icon = global::CommonUI.Properties.Resources.PivasMcc;
                labelSoftName.Text = "PIVAS MCC";
            }
            
            RedUser();
        }
        /// <summary>
        /// 从本地读取用户
        /// </summary>
        protected void RedUser()
        {
            try
            {
                DataSet ds = XmlSerializeHelper.XmlToDataTable(db.ReadXML());
                if (ds.Tables[0].Rows.Count > 5)
                {
                    DataTable dt = CommonHelp.DtSelectTop(5, ds.Tables[0]);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cbox_user.Items.Add(dt.Rows[i]["name"].ToString());
                    }
                }
                else
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        cbox_user.Items.Add(ds.Tables[0].Rows[i]["name"].ToString());
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// 点击软键盘按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbox_pwd_Click(object sender, EventArgs e)
        {
            txt_pwd.Text = "";
            db.IniWriteValuePivas("User", "pwd", "");

            if (form != null)//已存在，将其关闭
                Keyboard.CloseFrom();

            form = new Keyboard();
            form.KeyDataReceived += Form_KeyDataReceived;
            form.KeyDeleteReceived += Form_KeyDeleteReceived;
            form.StartPosition = FormStartPosition.Manual;
            Point pt = MousePosition;//获取鼠标的屏幕坐标            
            form.Left = pt.X - 210;
            form.Top = pt.Y + 20;
            form.ShowInTaskbar = false;
            form.Show();
        }

        private void Form_KeyDeleteReceived(object sender, PIVAsCommon.PivasEventArgs<string> e)
        {
            txt_pwd.SafeAction(() =>
            {
                if (txt_pwd.Text.Length > 0)
                {
                    txt_pwd.Text = txt_pwd.Text.Substring(0, txt_pwd.Text.Length - 1);
                }
            });           
        }

        private void Form_KeyDataReceived(object sender, PIVAsCommon.PivasEventArgs<string> e)
        {
            txt_pwd.SafeAction(() =>
            {
                this.txt_pwd.Text = txt_pwd.Text + e.Value.Trim();
            });
        }

        private void cbox_user_MouseHover(object sender, EventArgs e)
        {
           panel2.BackColor = Color.Red;
        }

        /// <summary>
        /// 密码框回车直接登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_pwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (cbox_user.Text.Trim() == "")
                {
                    MessageBox.Show("用户ID不能为空 ！！！ ");
                    cbox_user.Focus();
                }
                else
                {
                    LoginProving(cbox_user.Text.Trim(), txt_pwd.Text.Trim());
                }
            }
        }

        private void cbox_user_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                //========二维码扫描登录 write by 张建双========================================//

                if (cbox_user.Text.Trim().Length == 22 && cbox_user.Text.Trim().Substring(0, 4) == "7777")
                {
                    string ecode;
                    string str = "select * from QRcodeLog where QRcode = '" + cbox_user.Text.Trim() + "' and DelDT IS NULL";
                    DataSet ds = db.GetPIVAsDB(str);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ecode = ds.Tables[0].Rows[0]["DEmployeeID"].ToString();
                        str = "select top 1 DEmployeeID,AccountID,Pas,DEmployeeCode,DEmployeeName from DEmployee where DEmployeeID='" + ecode + "'";
                        ds = db.GetPIVAsDB(str);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            _DEmployeeID = ds.Tables[0].Rows[0]["DEmployeeID"].ToString();
                            _EmpCode = ds.Tables[0].Rows[0]["DEmployeeCode"].ToString();
                            _EmpName = ds.Tables[0].Rows[0]["DEmployeeName"].ToString();
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                    ds.Dispose();
                }
                else if(cbox_user .Text .Trim ()==string.Empty)
                {
                    MessageBox.Show("用户ID 不能为空 ！！！ ");
                    cbox_user.Focus();
                }
                //========二维码扫描登录 write by 张建双========================================//
                else
                {
                    txt_pwd.Focus();
                }
            }
        }

        private void cbox_user_MouseMove(object sender, MouseEventArgs e)
        {
            panel2.BackColor = Color.Red;
        }

        private void cbox_user_MouseLeave(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(19, 179, 253);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label5_Click(object sender, EventArgs e)
        {
            LoginCheck(cbox_user.Text.Trim(), txt_pwd.Text.Trim());
        }

        private void label4_Click(object sender, EventArgs e)
        {
            DBSet form = new DBSet(DatabaseType.PivasDB);
            form.ShowDialog();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);            
        }

        private void label9_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);            
        }

        private void label12_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);            
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);            
        }

        private void panelHelp_Click(object sender, EventArgs e)
        {
            frmPivasInfor ph = new frmPivasInfor("login");
            ph.ShowDialog();
        }

        private void panelHelp_MouseHover(object sender, EventArgs e)
        {
            this.panelHelp.BackgroundImage = global::CommonUI.Properties.Resources.Help_1;
        }

        private void panelHelp_MouseLeave(object sender, EventArgs e)
        {
            this.panelHelp.BackgroundImage = global::CommonUI.Properties.Resources.Help_2;
        }

        #region 登录后主窗体需要信息
        //员工表ID，是自增的
        private string _DEmployeeID = string.Empty;
        public string DEmployeeID
        {
            get { return _DEmployeeID; }
        }
        //员工工号
        private string _EmpCode = string.Empty;
        public string EmpCode
        {
            get { return _EmpCode; }
        }
        //员工姓名
        private string _EmpName = string.Empty;
        public string EmpName
        {
            get { return _EmpName; }
        }

        //软件类型枚举
        private SoftType _SoftType = SoftType.PivasMate;
        public SoftType SoftType
        {
            set { _SoftType = value; }
        }
        #endregion
    }
}
