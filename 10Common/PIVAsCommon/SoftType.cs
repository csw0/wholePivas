namespace PIVAsCommon
{
    /// <summary>
    /// 软件类型
    /// </summary>
    public enum SoftType
    {
        PivasMcc = 1,//MCC
        PivasMate = 2//Mate
    }

    public class StaticDictionary
    {
        public static string SPLITTER = "\r\n";

        //安卓屏0，迪文屏1，微软屏2
        public static string ANDROID_SCREEN = "0";
        public static string DIWEN_SCREEN = "1";
        public static string WINDOWS_SCREEN = "2";

        //药师登录状态，0未登录状态，1登录状态
        public static short DOCTOR_STATUS_FALSE = 0;
        public static short DOCTOR_STATUS_TRUE = 1;

        //瓶签计费结果，0计费失败，1计费成功
        public static short CHARGE_RESULT_FAIL = 0;
        public static short CHARGE_RESULT_SUCCESS = 1;
    }
}
