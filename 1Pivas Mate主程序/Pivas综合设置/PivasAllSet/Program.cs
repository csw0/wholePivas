using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PivasAllSet
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
            //if (args.Length > 0)
            //{
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
                    Application.Run(new PivasSet());
                }
                else
                {
                    ShowWindowAsync(current.MainWindowHandle, WS_SHOWNORMAL);
                    SetForegroundWindow(current.MainWindowHandle);
                }
            //}
            //else
            //{
            //    Application.Exit();
            //}
        }
    }
}
