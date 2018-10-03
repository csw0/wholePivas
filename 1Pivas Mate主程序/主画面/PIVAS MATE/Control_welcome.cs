using PIVAsCommon.Helper;
using System;
using System.Windows.Forms;

namespace PIVAS_MATE
{
    public delegate void GetFrom(int StartForm);

    public partial class Control_welcome : UserControl
    {
        public static event GetFrom FormSt;
        public Control_welcome()
        {
            InitializeComponent();
            FormSt += new GetFrom(SubForm);
        }

        DB_Help db = new DB_Help();
        //员工编号
        public static int DEmployeeID;
        //记录主要功能模块编号
        public static int StartForm;
        //登陆人姓名
        public static string Logname = string.Empty;

        private void Control_welcome_Load(object sender, EventArgs e)
        {
            lab_logname.Text = Logname;
        }
        /// <summary>
        /// 保存每个用户欢迎页面的信息到数据库表PivasMateFormSet
        /// </summary>
        /// <param name="StartForm">功能模块编号</param>
        protected void SubForm(int StartForm)
        {
            //chbox_check选中时，不显示欢迎页面，
            int noShowWelcome = chbox_check.Checked ? 1 : 0;
            db.SetPIVAsDB("EXEC bl_synformset " + DEmployeeID + "," + noShowWelcome + "," + StartForm + "");
        }
        /// <summary>
        /// 保存欢迎页面
        /// </summary>
        /// <param name="i"></param>
        public static void SetForm(int StartForm)
        {
            if (FormSt != null)
            {
                FormSt(StartForm);
            }
        }
        /// <summary>
        /// 保存操作状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SubForm(0);
            if (chbox_check.Checked == true)
            {
                MessageBox.Show("设置成功，您下次登陆将不再显示");
            }
            else
            {
                MessageBox.Show("设置成功，欢迎您使用PIVAS MATE系统");
            }
        }
    }
}
