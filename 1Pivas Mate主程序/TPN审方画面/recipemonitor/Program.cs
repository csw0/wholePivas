using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace recipemonitor
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (0 == args.Length)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());
            }
            else if (args.Contains("-BG"))
            {
                (new Thread(() =>
                        {
                            Thread.Sleep(1000); //等待Application.Run  
                            (new SilenceMonitor(Application.StartupPath)).monitor(); 
                            Application.Exit();
                        })
                 ).Start();

                Application.Run(); 
            }
        }
    }
}
