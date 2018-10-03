using System;
using System.Windows.Forms;

namespace PivasIVRPrint
{
    internal partial class Abut : Form
    {
        private UserControlPrint p;
        private string adds = string.Empty;
        private int i = 0;
        internal Abut(UserControlPrint p)
        {
            this.p = p;
            InitializeComponent();
        }
        private void timer2_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (p.NowST.Contains("完成"))
            {
                this.Dispose();
            }
            else
            {
                label1.Text = p.NowST;
                if ((++i) >= 10)
                {
                    i = 0;
                    adds = (adds.Length < 3 ? adds + "." : string.Empty);
                }
                label1.Text = label1.Text + adds;
            }
        }
    }
}
