using log4net;

namespace CSW.Common
{
    static class InternalLogger
    {
        static InternalLogger()
        {
            string logName = typeof (InternalLogger).Assembly.FullName.Split(',')[0];
            Log = LogManager.GetLogger(logName);
        }

        public static ILog Log { get; private set; }
    }
}
