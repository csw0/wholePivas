using System;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace PivasLabelCheckAll
{
    public static class Program
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
            Process instance = RunningInstance();//获取正在运行的程序

             if (instance == null)//没有正在运行的程序
             {
                 if (args.Length > 0)//有参数
                 {
                     Application.EnableVisualStyles();
                     Application.SetCompatibleTextRenderingDefault(false);
                     Application.Run(new frmMain(args[0], args[1], args[2], args[3]));
                     
                 }
                 else//无参数
                 {
                     Application.EnableVisualStyles();
                     Application.SetCompatibleTextRenderingDefault(false);
                     Application.Run(new frmMain());
                 }
             }
             else//存在已运行的进程
             {
                 if (args.Length > 0 && !IsSame(args[3]))//有参数且不是已打开的进程
                 {
                     Application.EnableVisualStyles();
                     Application.SetCompatibleTextRenderingDefault(false);
                     Application.Run(new frmMain(args[0], args[1], args[2], args[3]));

                 }
                 else //有参数且点击的进程已经运行
                 {
                     if (args.Length > 0)
                     {
                         Process newinstence = RunningInstance1(args[3]);
                         if (newinstence == null)
                             HandleRunningInstance(instance);
                         else
                             HandleRunningInstance(newinstence);
                     }
                     else
                     {
                         HandleRunningInstance(instance);
                     }
                 }
             }
        }

       /// <summary>
       /// 获取是否已存在点击的进程，没有返回false
       /// </summary>
       /// <param name="arg">arg3</param>
       /// <returns></returns>
       public static bool IsSame(string arg)
       {
           Process[] processes = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
           foreach (Process process in processes) 
           {
               if (process.MainWindowTitle == arg)
                   return true;
           }
           return false;
       }

        /// <summary> 
        /// 获取正在运行的实例，没有运行的实例返回null; 
        /// </summary> 
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
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

        /// <summary> 
        /// 获取当前进程是否点击的进程，返回正在运行的实例，没有运行的实例返回null; 
        /// </summary> 
        public static Process RunningInstance1(string arg)
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "//") == current.MainModule.FileName&& process.MainWindowTitle==arg)
                    {
                        return process;
                    }
                }
            }
            return null;
        }

        /// <summary> 
        /// 显示已运行的程序。 
        /// </summary> 
        public static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, 3); //显示
            SetForegroundWindow(instance.MainWindowHandle);            //放到前端
        }    
    }
}
