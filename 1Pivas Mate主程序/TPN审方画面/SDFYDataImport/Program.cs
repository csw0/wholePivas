using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace SDFYDataImport
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (0 == args.Length)
            {
                Application.Run(new frmMain());
            }
            else 
            {
                bool hadRun = false;
                bool byDate = false;
                foreach (string arg in args)
                {
                    if ("-ISynSDFYTPNLabAll".Equals(arg)) 
                        hadRun = true;
                    else if ("-ISynSDFYTPNLab".Equals(arg))
                    {
                        hadRun = true;
                        byDate = true;
                    }
                }

                if (hadRun)
                {  
                    (new Thread(() =>
                    { 
                        Thread.Sleep(1000); //等待Application.Run 
                        (new DataSyn(Application.StartupPath)).synTPNPatientLab(byDate);
                        Application.Exit();
                    }
                    )).Start();

                    Application.Run();
                }
            }
        }
    }
}
