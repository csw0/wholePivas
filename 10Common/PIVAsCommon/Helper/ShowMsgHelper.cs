using System;
using System.Windows.Forms;

namespace PIVAsCommon.Helper
{
    public class ShowMsgHelper
    {
        /// <summary>
        /// 系统错误
        /// </summary>
        /// <param name="Msg"></param>
        public static void ShowError(string Msg)
        {
            MessageBox.Show(Msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            InternalLogger.Log.Error(Msg);
        }

        /// <summary>
        /// 系统警告
        /// </summary>
        /// <param name="Msg"></param>
        public static void ShowWarning(String Msg)
        {
            MessageBox.Show(Msg, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 系统提示
        /// </summary>
        /// <param name="Msg"></param>
        public static void ShowHand(String Msg)
        {
            MessageBox.Show(Msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
