using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PivasDrugFlow
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
        static void Main(string []args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (Process instance = RunningInstance())
            {
                if (instance == null)
                {
                    Application.Run(new MainForm(args[0]));
                    //Application.Run(new MainForm("1"));
                }
                else
                {
                    HandleRunningInstance(instance);
                }
            }
        }
        private static Process RunningInstance()
        {
            using (Process current = Process.GetCurrentProcess())
            {
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
        }
        private static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL); //显示，可以注释掉 
            SetForegroundWindow(instance.MainWindowHandle);            //放到前端
        }
    }
}
