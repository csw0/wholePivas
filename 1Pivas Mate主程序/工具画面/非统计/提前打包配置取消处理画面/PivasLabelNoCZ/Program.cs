using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PivasLabelNoCZ
{
    static class Program
    {
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Process current = Process.GetCurrentProcess();
            bool newinstance = true;
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "//") == current.MainModule.FileName)
                    {
                        current = process;
                        newinstance = false;
                        break;
                    }
                }
            }
            if (newinstance)
            {
                Application.Run(new PivasLableNo(args[0], args[1]));//两个参数，员工工号和exe类型（0：提前打包，1：配置取消）
            }
            else
            {
                ShowWindowAsync(current.MainWindowHandle, WS_SHOWNORMAL);
                SetForegroundWindow(current.MainWindowHandle);
            }
        }
    }
}
