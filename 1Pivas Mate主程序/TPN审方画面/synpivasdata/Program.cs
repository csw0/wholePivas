using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace synpivasdata
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
                Application.Run(new frmMain());
            }
            else 
            {
                int synTyp = 0;
                foreach (string arg in args)
                {
                    if ("-ISynDrug".Equals(arg))
                        synTyp = 1;
                    else if ("-ISynPatient".Equals(arg))
                        synTyp = 2;
                }

                if (0 == synTyp)
                    return;

                (new Thread(() =>
                            {
                                Thread.Sleep(1000); //等待Application.Run  
                                BLPublic.LogOperate.writeLog("", "synpivasdata", "启动PIVAS数据同步");
                                if (1 == synTyp)
                                    (new SynPivasData(Application.StartupPath)).synDrug();
                                else
                                    (new SynPivasData(Application.StartupPath)).synPatient();

                                Application.Exit();
                            })
                 ).Start();

                Application.Run();
            }
        }
    }
}
