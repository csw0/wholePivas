﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Reflection;

namespace DWardChange
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

            Process instance = RunningInstance();


            if (instance == null)
            {
               Application.Run(new Form1(args[0]));
               //Application.Run(new Form1());
            }
            else
            {
                HandleRunningInstance(instance);
            }
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
        /// 显示已运行的程序。 
        /// </summary> 
        public static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL); //显示，可以注释掉 
            SetForegroundWindow(instance.MainWindowHandle);            //放到前端
        }
    }
}