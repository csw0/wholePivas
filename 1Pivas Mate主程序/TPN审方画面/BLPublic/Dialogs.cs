using System;
using System.Windows.Forms;
using System.Text;

namespace BLPublic
{
    public class Dialogs
    {
        public const int None = 0;
        public const int OK = 1;
        public const int Cancel = 2;
        public const int Abort = 3;
        public const int Retry = 4;
        public const int Ignore = 5;
        public const int Yes = 6;
        public const int No = 7;

        public static void Info(string _txt)
        {
            MessageBox.Show(_txt, "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Alert(string _txt)
        {
            MessageBox.Show(_txt, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Error(string _txt)
        {
            MessageBox.Show(_txt, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static int Ask(string _txt)
        {
            return (int)MessageBox.Show(_txt, "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static int Ask2(string _txt)
        {
            return (int)MessageBox.Show(_txt, "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }
    }
}
