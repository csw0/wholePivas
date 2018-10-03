using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DMetricManage
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                bool isrun;
                System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName, out isrun);
                if (isrun)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FormMetric(args[0]));
                    mutex.ReleaseMutex();
                }
            }
        }
    }
}
