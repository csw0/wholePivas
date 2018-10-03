using System;
using System.Windows.Forms;

namespace WorkStatic
{
    internal sealed partial class WaitForm : Form
    {
        private Form1 f;
        internal WaitForm(Form1 f)
        {
            this.f = f;
            InitializeComponent();
        }
        private void WaitForm_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if(f.Hadrun)
            {
                label1.Text = label1.Text.Split('.').Length < 4 ? label1.Text + "." : "正在查询和计算工作量";
            }
            else
            {
                this.Dispose();
            }
        }
    }
}
