using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BLPublic
{
    public class LogOperate
    { 
        private string logFile = null; 

        /// <summary>
        /// 日志操作
        /// </summary>
        /// <param name="_logPath">日志存放目录</param>
        /// <param name="_logName">日志名称</param>
        /// <param name="_isDayLog">是否按日生成日志</param>
        public LogOperate(string _logPath, string _logName, bool _isDayLog=true)
        {
            if (string.IsNullOrWhiteSpace(_logPath))
                _logPath = "log\\";

            if (string.IsNullOrWhiteSpace(_logName))
                _logName = "log";

            try
            {
                this.logFile = _logPath;
                if (_isDayLog)
                    this.logFile += string.Format("{0}{1:yyyyMMdd}.log", _logName, DateTime.Now);
                else
                    this.logFile += string.Format("{0}.log", _logName, DateTime.Now);

                if (!Directory.Exists(_logPath))
                    Directory.CreateDirectory(_logPath);

                if (!File.Exists(this.logFile))
                {
                    FileStream fs = File.Create(this.logFile);
                    fs.Close();
                }  
            }
            catch (Exception ex)
            { }

            if (_isDayLog) //删除超过半月的日志
            try
            {
                string[] files = Directory.GetFiles(_logPath, _logName + "*.log", SearchOption.TopDirectoryOnly);
                if (0 < files.Length)
                {
                    int delDate = Convert.ToInt32(string.Format("{0:yyyyMMdd}", DateTime.Now.AddDays(-15)));
                    int nLen = _logName.Length;
                    foreach (string file in files)
                    {
                        if (Convert.ToInt32(file.Substring(file.Length - 8 - 4, 8)) < delDate)
                            File.Delete(file);
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        ~LogOperate()
        {
             
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="_log">日志内容</param>
        public void log(string _txt)
        {
            if (string.IsNullOrWhiteSpace(this.logFile))
                return;

            string txt = string.Format("[{0:yyyy-MM-dd HH:mm:ss}]", DateTime.Now) + _txt;
            txt += "\r\n";
            try
            {
                byte[] bytes = Encoding.Default.GetBytes(txt);
                FileStream fsLog = File.OpenWrite(this.logFile);
                fsLog.Position = fsLog.Length;
                fsLog.Write(bytes, 0, bytes.Length);
                fsLog.Flush();
                fsLog.Close();
            }
            catch (Exception ex)
            { }
        }

        public static void writeLog(string _path, string _name, string _txt, bool _isDayLog=true)
        {
            LogOperate log = new LogOperate(_path, _name, _isDayLog);
            log.log(_txt);
            log = null; 
        }

        public static void writeLog(string _name, string _txt)
        {
            LogOperate log = new LogOperate("log/", _name, false);
            log.log(_txt);
            log = null;
        }
    }
}
