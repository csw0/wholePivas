using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using CommonUI.Controls;
using PIVAsCommon.Helper;

namespace CommonUI.Froms
{
    /// <summary>
    /// 数据库类型枚举
    /// </summary>
    public enum DatabaseType
    {
        PivasDB = 1,
        HISDB = 2
    }

    public partial class DBSet : Form
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

        DB_Help db = new DB_Help();
        public event EventHandler ConnectDBResult;
        private DatabaseType DBType;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlType"></param>
        public DBSet(DatabaseType type)
        {
            InitializeComponent();
            DBType = type;
        }

        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 画面初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DBSet_Load(object sender, EventArgs e)
        {
            switch (DBType)
            {
                case DatabaseType.PivasDB:
                    tbDataSource.Text = db.IniReadValuePivas("Database", "Data Source");
                    tbDBName.Text = db.IniReadValuePivas("Database", "Initial Catalog");
                    tbUserID.Text = db.Decrypt(db.IniReadValuePivas("Database", "User ID"));
                    tbPassword.Text = db.Decrypt(db.IniReadValuePivas("Database", "Password"));
                    break;
                case DatabaseType.HISDB:
                    btnTestConnect.Visible = false;//HIS数据库暂不支持测试
                    btnSaveConfig.Enabled = true;//保存按钮可用
                    tbDataSource.Text = db.IniReadValueHIS("Database", "Data Source");
                    tbDBName.Text = db.IniReadValueHIS("Database", "Initial Catalog");
                    tbUserID.Text = db.Decrypt(db.IniReadValueHIS("Database", "User ID"));
                    tbPassword.Text = db.Decrypt(db.IniReadValueHIS("Database", "Password"));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 画面移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Keyboard.CloseFrom();

            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);    
        }

        /// <summary>
        /// 关闭按钮颜色变换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel_Close_MouseHover(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.FromArgb(255, 192, 128);
        }

        /// <summary>
        /// 关闭按钮颜色变换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel_Close_MouseLeave(object sender, EventArgs e)
        {
            Panel_Close.BackColor = Color.Transparent;
        }

        #region 首页面
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text == "13816350872")
                panelIndex.Visible = false;
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox5.Text == "13816350872")
                    panelIndex.Visible = false;
                else
                    MessageBox.Show("密码不正确 ！");
            }
        }
        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            switch (DBType)
            {
                case DatabaseType.PivasDB:
                    tbDataSource.Text = db.IniReadValuePivas("Database", "Data Source");
                    tbDBName.Text = db.IniReadValuePivas("Database", "Initial Catalog");
                    tbUserID.Text = db.Decrypt(db.IniReadValuePivas("Database", "User ID"));
                    tbPassword.Text = db.Decrypt(db.IniReadValuePivas("Database", "Password"));
                    break;
                case DatabaseType.HISDB:
                    tbDataSource.Text = db.IniReadValueHIS("Database", "Data Source");
                    tbDBName.Text = db.IniReadValueHIS("Database", "Initial Catalog");
                    tbUserID.Text = db.Decrypt(db.IniReadValueHIS("Database", "User ID"));
                    tbPassword.Text = db.Decrypt(db.IniReadValueHIS("Database", "Password"));
                    break;
                default:
                    break;
            }
        }

        private void btnTestConnect_Click(object sender, EventArgs e)
        {
            SaveConfig();
            if (db.TestDB())
            {
                btnSaveConfig.Enabled = true;
                MessageBox.Show("  连接成功，确认后请按保存键！");
            }
            else
            {
                btnSaveConfig.Enabled = false;
                MessageBox.Show("  连接失败！！！");
            }

            if (ConnectDBResult != null)
                ConnectDBResult(btnSaveConfig.Enabled,null);
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            try
            {
                SaveConfig();

                this.Close();
                this.Dispose();
            }
            catch
            {
                MessageBox.Show("保存连接出错！");
            }
        }

        void SaveConfig()
        {
            switch (DBType)
            {
                case DatabaseType.PivasDB:
                    db.IniWriteValuePivas("Database", "Data Source", tbDataSource.Text);
                    db.IniWriteValuePivas("Database", "Initial Catalog", tbDBName.Text);
                    db.IniWriteValuePivas("Database", "User ID", db.Encrypt(tbUserID.Text));
                    db.IniWriteValuePivas("Database", "Password", db.Encrypt(tbPassword.Text));
                    break;
                case DatabaseType.HISDB:
                    db.IniWriteValueHIS("Database", "Data Source", tbDataSource.Text);
                    db.IniWriteValueHIS("Database", "Initial Catalog", tbDBName.Text);
                    db.IniWriteValueHIS("Database", "User ID", db.Encrypt(tbUserID.Text));
                    db.IniWriteValueHIS("Database", "Password", db.Encrypt(tbPassword.Text));
                    break;
                default:
                    break;
            }
        }
    }
}
