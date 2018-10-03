using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CommonUI.Controls;
using PIVAsCommon;
using PIVAsCommon.Helper;
using PIVAsCommon.Extensions;
using PivasLimitDES;

namespace PivasNurse
{

    //定义枚举
    public delegate void GetPwd(string pwd);
    public delegate void ClarPwd();
    public delegate void WordName(string word);
    public delegate void ShowHide();

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
        DB_Help db = new DB_Help();
        conntion con = new conntion();
        string EmployeeID;
        public Login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 给密码输入框赋值
        /// </summary>
        /// <param name="i"></param>
        public void txtpwd(string i)
        {
            this.txt_pwd.Text = txt_pwd.Text + i;
        }

        public void txtword(string i)
        {
            this.txt_word.Text = i;
        }

        public void ClearPwd()
        {
            if (txt_pwd.Text.Length > 0)
            {
                txt_pwd.Text = txt_pwd.Text.Substring(0, txt_pwd.Text.Length - 1);
            }
        }
        /// <summary>
        /// 画面拉动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
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
            Application.Exit();
        }

        /// <summary>
        /// 登录按钮鼠标放上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_MouseHover(object sender, EventArgs e)
        {
            label1.BackColor = Color.FromArgb(99, 142, 190);
        }

        /// <summary>
        /// 登录按钮鼠标离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.BackColor = Color.FromArgb(50, 97, 144);
        }

        /// <summary>
        /// 登录按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_Click(object sender, EventArgs e)
        {
            try
            {
                LoginProving(cbox_user.Text.Trim(), txt_pwd.Text.Trim());
            }
            catch
            {
                MessageBox.Show("登陆失败，网络配置错误");
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
                if (ds == null || ds.Tables[0].Rows.Count <= 0)
                {
                    MessageBox.Show("登录失败，输入密码错误或网络错误");
                }
                else if (ds.Tables[0].Rows.Count > 0)
                {
                    label1.BackColor = Color.FromArgb(19, 179, 253);
                    if (user.Length >= 4 && user.Substring(0, 4) != "7777")//扫描二维码不保存本地
                    {
                        con.PreserveLog(cbox_user.Text, txt_pwd.Text);
                    }
                   
                    db.IniWriteValuePivas("User", "Ward", txt_word.Text);
                    DataSet dss = db.GetPIVAsDB("select wardcode from dward where wardname ='" + txt_word.Text + "'");
                    if (dss != null && dss.Tables[0].Rows.Count > 0)
                    {
                        txt_word.Tag = dss.Tables[0].Rows[0]["wardcode"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("不存在该病区");
                    }

                    NurseFormSet();
                    EmployeeID = ds.Tables[0].Rows[0]["DEmployeeID"].ToString();
                    if (GetPivasLimit.Instance.Limit(EmployeeID, "08000"))
                    {
                        NurseWorkStation IForm = new NurseWorkStation();
                        IForm.Owner = this;
                        IForm.Txt_ward.Text = txt_word.Text;
                        IForm.Txt_ward.Tag = txt_word.Tag;
                        IForm.Login_Name.Text = ds.Tables[0].Rows[0]["DEmployeeName"].ToString();
                            
                        IForm.EmployeeID = this.EmployeeID;
                        IForm.Show();
                        this.Hide();
                    }
                }
            }
        }
        public void showhide()
        {
            this.Show();
            cbox_user.Text = "";
            txt_pwd.Text = "";
        }

        /// <summary>
        /// 用户名鼠标移上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_MouseHover(object sender, EventArgs e)
        {
            panel4.BackColor = Color.Red;
        }

        /// <summary>
        /// 用户名鼠标移开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            panel4.BackColor = Color.FromArgb(19, 179, 253);
        }

        /// <summary>
        /// 用户名下拉框鼠标以上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 用户名下拉框鼠标离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {

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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label2_Click(object sender, EventArgs e)
        {
            //IntroductionForm IForm = new IntroductionForm();
            //IForm.ShowDialog();
        }

        private void pbox_pwd_Click(object sender, EventArgs e)
        {
            txt_pwd.Text = "";
            db.IniWriteValuePivas("User", "pwd", "");
            Keyboard form = new Keyboard();
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

        private void Txt_User_MouseHover(object sender, EventArgs e)
        {
            panel2.BackColor = Color.Red;
        }

        private void Txt_User_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                label1_Click(null, null);
            }
        }

        private void Txt_User_MouseLeave(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(19, 179, 253);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (RedWard())
            {
                ShowCob();
            }
            if (RedUser())
            { }

        }
        /// <summary>
        /// 从本地读取用户
        /// </summary>
        protected bool RedUser()
        {
            try
            {
                DataSet ds = con.XmlToDataTable(con.RedStringTxT());
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 5)
                    {
                        DataTable dt = con.DtSelectTop(5, ds.Tables[0]);
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
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 从本地读取病区
        /// </summary>
        protected bool RedWard()
        {
            try
            {
                if (db.IniReadValuePivas("User", "Ward") != null && db.IniReadValuePivas("User", "Ward") != "")
                {
                    txt_word.Text = db.IniReadValuePivas("User", "Ward");
                    cob_word.Text = db.IniReadValuePivas("User", "Ward");
                    cob_word.Visible = false;
                    return false;

                }
                else
                {
                    cob_word.Visible = true;
                    return true;
                }

            }
            catch
            {
                return true;
            }

        }

        private void Label_Set_Click(object sender, EventArgs e)
        {
            DataSet dss = db.GetPIVAsDB("select wardcode from dward where wardname='" + txt_word.Text + "'");
            if (dss != null && dss.Tables[0].Rows.Count > 0)
            {
                txt_word.Tag = dss.Tables[0].Rows[0]["wardcode"].ToString();
            }
            NurseSet nur = new NurseSet();
            if (cob_word.Visible == false)
                nur.NurseWardCode = txt_word.Tag.ToString();
            else
            {
                dss = db.GetPIVAsDB("select wardcode from dward where wardname='" + txt_word.Text + "'");
                if (dss != null && dss.Tables[0].Rows.Count > 0)
                {
                  nur.NurseWardCode = dss.Tables[0].Rows[0]["wardcode"].ToString();
                }
            }

            DialogResult dd = nur.ShowDialog();
            if (dd == DialogResult.Yes)
            {

                if (RedWard())
                {
                    ShowCob();
                }

            }
        }

        private void ShowCob()
        {
            string str = "select WardCode,WardName from DWard where isopen=1 order by WardSeqNo";
            DataSet ds = db.GetPIVAsDB(str);
            if (ds != null)
            {
                //cob_word.DataSource = ds.Tables[0];
                //cob_word.DisplayMember = "WardName";
                //cob_word.ValueMember = "WardCode";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cob_word.Items.Add(ds.Tables[0].Rows[i]["WardName"].ToString());
                }
                //cob_word.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbox_user_MouseHover(object sender, EventArgs e)
        {
            panel2.BackColor = Color.Red;
        }

        private void cbox_user_MouseLeave(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(19, 179, 253);
        }

        private void cbox_user_MouseMove(object sender, MouseEventArgs e)
        {
            panel2.BackColor = Color.Red;
        }


        private void NurseFormSet()
        {
            string SqlSet = "Select * from PivasNurseFormSet where WardCode='" + txt_word.Tag + "'";
            DataTable dt = new DataTable();
            dt = db.GetPIVAsDB(SqlSet).Tables[0];
            if (dt.Rows.Count == 0)
            {
                string sql = "Select * from PivasNurseFormSet where DateFrom='1'";
                DataSet ds = db.GetPIVAsDB(sql);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    StringBuilder str = new StringBuilder();
                    str.Append("insert into PivasNurseFormSet([DateFrom],[WardCode] ,[LabelOverFor],[PackOverFor],[LabelPack] ,[LabelPackAir]) ");
                    str.Append("values('0','");
                    str.Append(txt_word.Tag);
                    str.Append("','" + ds.Tables[0].Rows[0]["LabelOverFor"].ToString());
                    str.Append("','" + ds.Tables[0].Rows[0]["PackOverFor"].ToString());
                    str.Append("','" + ds.Tables[0].Rows[0]["LabelPack"].ToString());
                    str.Append("','" + ds.Tables[0].Rows[0]["LabelPackAir"].ToString());
                    str.Append("') ");

                    db.SetPIVAsDB(str.ToString());

                }
                else
                {
                    MessageBox.Show("病区操作条件未设置，请重新登录进行设置");
                    return;
                }
             
            }


        }

        private void cbox_user_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    //========二维码扫描登录 write by 张建双========================================//

                    string str = "select * from QRcodeLog where QRcode = '" + cbox_user.Text + "' and DelDT IS NULL";
                    DataSet ds = db.GetPIVAsDB(str);
                    if (ds == null || ds.Tables[0].Rows.Count <= 0)
                    {
                        SendKeys.Send("{Tab}");
                        return;
                    }
                    label1.BackColor = Color.FromArgb(19, 179, 253);
                    //if (cob_word.Visible)
                    //{
                    //    txt_word.Text = cob_word.Text;
                    //    txt_word.Tag = cob_word.SelectedValue;
                    //}
                    //else
                    //{

                        DataSet dss = db.GetPIVAsDB("select wardcode from dward where wardname='" + txt_word.Text + "'");
                        if (dss != null || dss.Tables[0].Rows.Count > 0)
                        {
                            txt_word.Tag = dss.Tables[0].Rows[0]["wardcode"].ToString();
                        }

                    //}
                    NurseWorkStation IForm = new NurseWorkStation();
                    IForm.Owner = this;
                    IForm.Txt_ward.Text = txt_word.Text;
                    IForm.Txt_ward.Tag = txt_word.Tag;
                    IForm.Login_Name.Text = ds.Tables[0].Rows[0]["DEmployeeName"].ToString();
                    NurseFormSet();
                    EmployeeID = ds.Tables[0].Rows[0]["DEmployeeID"].ToString();
                    IForm.EmployeeID = this.EmployeeID;
                    IForm.Show();
                    this.Hide();
                    //========二维码扫描登录 write by 张建双========================================//
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            LoginCheck(cbox_user.Text.Trim(), txt_pwd.Text.Trim());
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
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    MessageBox.Show("用户名与密码不匹配，不可修改密码！");
                }
                else if (ds.Tables[0].Rows.Count > 0)
                {
                    XiuGaiMiMa form = new XiuGaiMiMa();
                    form.AccountID = ds.Tables[0].Rows[0]["DEmployeeID"].ToString();
                    form.ShowDialog();
                }
            }
        }

        private void cob_word_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txt_word.Text = cob_word.SelectedItem.ToString();
        }
    }
}
