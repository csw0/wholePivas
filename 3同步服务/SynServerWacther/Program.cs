using PIVAsCommon;
using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Threading;

namespace SynServerWacther
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //初始化同步程序运行路径
                synServerPath = Environment.CurrentDirectory + "\\" + "SynServer.exe";

                string fullPath = Environment.CurrentDirectory + "\\SynServerWacther.exe";
                int WINDOW_HANDLER = FindWindow(null, fullPath);
                IntPtr CLOSE_MENU = GetSystemMenu((IntPtr)WINDOW_HANDLER, IntPtr.Zero);
                int SC_CLOSE = 0xF060;
                RemoveMenu(CLOSE_MENU, SC_CLOSE, 0x0);

                if (SetConsoleCtrlHandler(consoleDelegete, true))
                {
                    InternalLogger.Log.Info("请使用Exit退出此程序...");

                    Thread t = new Thread(WatchSynServer);
                    t.IsBackground = true;
                    t.Start();

                    //使用命令关闭
                    while (true)
                    {
                        string key = Console.ReadLine();
                        if (!String.IsNullOrEmpty(key) && key.ToLower().Trim() == "exit")
                        {
                            InternalLogger.Log.Debug("使用exit退出了看门狗");
                            return;
                        }
                    }
                }
                else
                    InternalLogger.Log.Warn("注入关闭前事件处理失败");
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("开启同步程序看门狗出错：" + ex.Message);
            }
        }

        #region 看门狗
        static int noRespondCount = 0;//同步不响应次数计数
        private const int CHECK_INTERNAL = 60000;//检测间隔，毫秒
        private const int NORESPONSE_COUNT = 5;////同步程序不响应，次数达到这个值，就重启
        private static string synServerPath = string.Empty;
        private static string synServerAddress = string.Empty;
        
        /// <summary>
        /// 同步程序的开门狗初始化地址参数
        /// </summary>
        static void InitWacther()
        {
            try
            {
                using (ManagementObjectCollection MOC = new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances())
                {
                    string strMAC = string.Empty;
                    string strIP = string.Empty;

                    foreach (ManagementObject MO in MOC)
                    {
                        if (true.Equals(MO.GetPropertyValue("IPEnabled")))
                        {
                            foreach (PropertyData pd in MO.Properties)
                            {
                                if (pd.Value != null)
                                {
                                    if (pd.Name == "MACAddress")
                                    {
                                        strMAC = pd.Value.ToString();
                                        Console.WriteLine("Mac:"+strMAC);
                                    }
                                    else if (pd.Name == "IPAddress")
                                    {
                                        strIP = pd.IsArray ? ((string[])pd.Value)[0] : pd.Value.ToString();
                                        Console.WriteLine("ip:"+ strIP);
                                    }
                                }
                            }
                        }
                    }
                    synServerAddress = strMAC + "|" + strIP;
                    InternalLogger.Log.Info("运行同步程序时参数=" + synServerAddress);
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Log.Error("同步程序的开门狗初始化地址参数出错:" + ex.Message);
            }
        }

        /// <summary>
        /// 看护线程
        /// </summary>
        static void WatchSynServer()
        {
            while (true)
            {
                try
                {
                    if (Process.GetProcessesByName("SynServer").Length == 0)//首次开启或死机重启SynServer
                    {
                        InitWacther();//每次都要重复获取参数吗？
                        if (File.Exists(synServerPath))
                            Process.Start(synServerPath, "CheckSyn " + synServerAddress);//两个参数，用空格分割
                        else
                            InternalLogger.Log.Warn("同步程序路径不正确，请确保同步程序与看门狗程序在同一目录下。");
                    }
                    else
                        ResponseCrash();

                    Thread.Sleep(CHECK_INTERNAL);//检测一次
                }
                catch (Exception ex)
                {
                    InternalLogger.Log.Error("实时监控同步程序出错：" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 响应SynServer程序崩溃，的处理
        /// </summary>
        static void ResponseCrash()
        {
            using (Process p = Process.GetProcessesByName("SynServer")[0])
            {
                if (!p.Responding)
                {
                    Interlocked.Add(ref noRespondCount, 1);//加1
                    if (noRespondCount >= NORESPONSE_COUNT)
                    {
                        Interlocked.Add(ref noRespondCount, -noRespondCount);//置零
                        p.Kill();
                        InternalLogger.Log.Info(String.Format("同步程序处于假死状态已经{0}秒,现将其强制关闭，等待{1}秒后重启。"
                                , NORESPONSE_COUNT * CHECK_INTERNAL / 1000, CHECK_INTERNAL / 1000));
                    }
                }
                else
                {
                    if (noRespondCount > 0)//有假死发生，现恢复
                    {
                        Interlocked.Add(ref noRespondCount, -noRespondCount);//置零
                        InternalLogger.Log.Info(String.Format("同步程序处于假死{0}秒后，自动重新恢复正常(未重启)。",
                            noRespondCount * CHECK_INTERNAL / 1000));
                    }
                    else
                        InternalLogger.Log.Info("同步程序处于正常状态，看门狗工作正常。");
                }
            }
        }
        #endregion

        #region 关闭窗体时，进行提示
        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate HandlerRoutine, bool Add);
        //当用户关闭Console时，系统会发送次消息
        private const int CTRL_CLOSE_EVENT = 2;
        //Ctrl+C，系统会发送次消息
        private const int CTRL_C_EVENT = 0;
        //Ctrl+break，系统会发送次消息
        private const int CTRL_BREAK_EVENT = 1;
        //用户退出（注销），系统会发送次消息
        private const int CTRL_LOGOFF_EVENT = 5;
        //系统关闭，系统会发送次消息
        private const int CTRL_SHUTDOWN_EVENT = 6;

        public delegate bool ConsoleCtrlDelegate(int ctrlType);
        private static ConsoleCtrlDelegate consoleDelegete = new ConsoleCtrlDelegate(HandlerRoutine);

        private static bool HandlerRoutine(int ctrlType)
        {
            switch (ctrlType)
            {
                case CTRL_C_EVENT:
                case CTRL_BREAK_EVENT:
                    Console.WriteLine("Ctrl+C(Break)快捷键被屏蔽使用,请输入exit,退出程序");
                    return true; //这里返回true，表示阻止响应系统对该程序的操作
            }
            return false;//忽略处理，让系统进行默认操作
        }
        #endregion

        #region 禁用关闭按钮
        [DllImport("User32.dll ", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll ", EntryPoint = "GetSystemMenu")]
        extern static IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);

        [DllImport("user32.dll ", EntryPoint = "RemoveMenu")]
        extern static int RemoveMenu(IntPtr hMenu, int nPos, int flags);
        #endregion
    }
}
