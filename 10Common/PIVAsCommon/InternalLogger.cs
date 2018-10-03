using log4net;
using System.IO;

namespace PIVAsCommon
{
    public static class InternalLogger
    {
        static InternalLogger()
        {
            #region 初始化log4net的配置文件路径
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo("log4net.config"));
            #endregion

            string logName = typeof (InternalLogger).Assembly.FullName.Split(',')[0];
            Log = LogManager.GetLogger(logName);
        }

        public static ILog Log { get; private set; }
    }
}
