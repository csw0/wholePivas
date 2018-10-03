using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PIVAsCommon.Helper
{
    /// <summary>
    /// 进程帮助类
    /// </summary>
    public class ProcessHelper
    {
        /// <summary>
        /// 开启线程
        /// </summary>
        /// <param name="arguments">，进程参数，（员工编号与员工登录账户一致）</param>
        /// <param name="processFileName">进程exe所在文件路径</param>
        public static void StartProcess(string arguments, string processFileName)
        {
            try
            {
                Process p = new Process();
                p.StartInfo.Arguments = arguments;
                p.StartInfo.FileName = processFileName;
                p.Start();
            }
            catch (Exception ex)
            {
                ShowMsgHelper.ShowError("开启线程出错"+ processFileName + ex.Message);
            }
        }
    }
}
