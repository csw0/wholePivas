using System;
using System.Windows.Forms;

namespace SynServer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 2 && args[0] == "CheckSyn")
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new SyncWork(args[1]));
            }
        }
    }
}
