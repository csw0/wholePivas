using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PivasDrugCash
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
            if (args.Length > 0)
            {
                bool newinstance = true;
                Process current = Process.GetCurrentProcess();
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
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm(args[0]));
                }
                else
                {
                    ShowWindowAsync(current.MainWindowHandle, WS_SHOWNORMAL);
                    SetForegroundWindow(current.MainWindowHandle);
                }
            }
        }
    }
}
