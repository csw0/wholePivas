using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using PIVAsCommon.Helper;
using PIVAsCommon;

namespace PivasLabelCheck
{
    public partial class UserControlCheck : UserControl, IMenuManager
    {
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;

        private DB_Help db = new DB_Help();


        #region 变量
        private string UserSeqNo = "";
        private string UserID = "";
        private string UserName = "";
        string PWD = "";
        #endregion

        #region 方法
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="userunqID"></param>
        /// <param name="userID"></param>
        /// <param name="username"></param>
        public UserControlCheck(string userunqID, string userID, string username)
        {
            InitializeComponent();
            UserSeqNo = userunqID.Trim();
            UserID = userID.Trim();
            UserName = username.Replace(" ","").Trim();
            PWD = UserSeqNo + " " + userID + " " + UserName;
        }
        #endregion


        /// <summary>
        /// 打包核对按钮（画面调出）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPackage_Click(object sender, EventArgs e)
        {
            try
            {
                string newpwd = PWD + " 打包核对";
                Process process = new Process();
                Process.Start(Application.StartupPath + "\\PivasLabelCheckAll.exe", newpwd);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        /// <summary>
        /// 配置核对按钮（画面调出）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfiguration_Click(object sender, EventArgs e)
        {
            try
            {
                string newpwd = PWD + " 配置核对";
                Process process = new Process();
                Process.Start(Application.StartupPath + "\\PivasLabelCheckAll.exe", newpwd);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        /// <summary>
        /// 出仓扫描按钮（画面调出）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChuCang_Click(object sender, EventArgs e)
        {
            try
            {
                string newpwd = PWD + " 出仓扫描";
                Process process = new Process();
                process.StartInfo.Arguments = newpwd;

                process.StartInfo.FileName = Application.StartupPath + "\\PivasLabelCheckAll.exe";
                process.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        private void btnJinCang_Click(object sender, EventArgs e)
        {
            try
            {
                string newpwd = PWD + " 进仓扫描";
                Process process = new Process();
                Process.Start(Application.StartupPath + "\\PivasLabelCheckAll.exe", newpwd);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        private void btnPaiyao_Click(object sender, EventArgs e)
        {
            try
            {
                string newpwd = PWD + " 排药核对";
                Process process = new Process();
                Process.Start(Application.StartupPath + "\\PivasLabelCheckAll.exe", newpwd);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        private void btnBaiyao_Click(object sender, EventArgs e)
        {
            try
            {
                Process process = new Process();
                Process.Start(Application.StartupPath + "\\BaiYaoCheck.exe", PWD);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        private void btnTieQian_Click(object sender, EventArgs e)
        {
            try
            {
                string newpwd = PWD + " 贴签核对";
                Process process = new Process();
                Process.Start(Application.StartupPath + "\\PivasLabelCheckAll.exe", newpwd);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }   


        /// <summary> 
        /// 获取正在运行的实例，没有运行的实例返回null; 
        /// </summary> 
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "//") == current.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }
        /// <summary> 
        /// 显示已运行的程序。 
        /// </summary> 
        public static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, 1); //显示，可以注释掉 
            SetForegroundWindow(instance.MainWindowHandle);            //放到前端
        }

        private void PivasLabelCheck_Load(object sender, EventArgs e) { }

        #region 接口实现
        void IMenuManager.menuBeforeSelect()
        {
            if (db.GetPivasAllSet("核对-摆药核对-按钮显示") == "0")
            {
                btnBaiyao.Visible = false;
            }
            else { btnBaiyao.Visible = true; }

            if (db.GetPivasAllSet("核对-排药核对-按钮显示") == "0")
            {
                btnPaiyao.Visible = false;
            }
            else { btnPaiyao.Visible = true; }

            if (db.GetPivasAllSet("核对-进仓核对-按钮显示") == "0")
            {
                btnJinCang.Visible = false;
            }
            else { btnJinCang.Visible = true; }

            if (db.GetPivasAllSet("核对-配置核对-按钮显示") == "0")
            {
                btnConfiguration.Visible = false;
            }
            else { btnConfiguration.Visible = true; }

            if (db.GetPivasAllSet("核对-出仓核对-按钮显示") == "0")
            {
                btnChuCang.Visible = false;
            }
            else { btnChuCang.Visible = true; }

            if (db.GetPivasAllSet("核对-打包核对-按钮显示") == "0")
            {
                btnPackage.Visible = false;
            }
            else { btnPackage.Visible = true; }

            if (db.GetPivasAllSet("核对-贴签核对-按钮显示") == "0")
            {
                btnTieQian.Visible = false;
            }
            else { btnTieQian.Visible = true; }
        }

        void IMenuManager.menuAfterSelect()
        {
        }
        #endregion

    }
}
