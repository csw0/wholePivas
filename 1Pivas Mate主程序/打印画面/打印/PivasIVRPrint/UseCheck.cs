using System;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace PivasIVRPrint
{
    public partial class UseCheck : Form
    {
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;

        #region 属性
        private DB_Help DB = new DB_Help();
        private string IsUseCheck = string.Empty;
        private string PWD = string .Empty;
        private string NewPwd = string.Empty;

        #endregion

        #region 事件
        public UseCheck()
        {
            InitializeComponent();
        }
        public UseCheck(string userID,string isusecheck)
        {
            InitializeComponent();
            this.IsUseCheck = isusecheck;
            DataSet ds = new DataSet();
            ds = DB.GetPIVAsDB("    select DEmployeeID,AccountID ,DEmployeeName from DEmployee where DemployeeID ='" + userID + "'");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                PWD = ds.Tables[0].Rows[0]["DEmployeeID"].ToString() + " " + ds.Tables[0].Rows[0]["AccountID"]+ " " + ds.Tables[0].Rows[0]["DEmployeeName"].ToString();
            }
        }

        private void UseCheck_Load(object sender, EventArgs e)
        {
            label1.Text = DB.GetPivasAllSetValue2("打印-打印确认-打印完成后调用画面");
            label2.Text = CheckPro();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsUseCheck == "1")
                {
                    Process process = new Process();
                    //process.Start(Application.StartupPath + "\\BaiYaoCheck.exe", PWD);
                    System.Diagnostics.Process.Start(Application.StartupPath + "\\BaiYaoCheck.exe", PWD);
                }
                else
                {
                    Process process = new Process();
                    System.Diagnostics.Process.Start(Application.StartupPath + "\\PivasLabelCheckAll.exe", NewPwd);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion



        #region 方法
        private string CheckPro()
        {
            switch (IsUseCheck)
            {
                case "1": return "摆药核对";
                case "2": NewPwd = PWD + " 排药核对"; return "排药核对";
                case "3": NewPwd = PWD + " 进仓核对"; return "进仓核对";
                case "4": NewPwd = PWD + " 配置核对"; return "配置核对";
                case "5": NewPwd = PWD + " 出仓核对"; return "出仓核对";
                case "6": NewPwd = PWD + " 打包核对"; return "打包核对";
                default: return "";
            }

        }
        #endregion
    }
}
