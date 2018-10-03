using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace QrUserCode
{
    static class Program
    {
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
     
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool isrun;
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName, out isrun);
            if (isrun)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);


                if (args.Length > 0)
                {
                    Application.Run(new QrUserCode(args[0]));
                }
                else
                {
                    Application.Exit();
                }
                mutex.ReleaseMutex();
            }
            SetForegroundWindow(Process.GetProcessesByName(Application.ProductName)[0].MainWindowHandle);            //放到前端
        }
    }
}
