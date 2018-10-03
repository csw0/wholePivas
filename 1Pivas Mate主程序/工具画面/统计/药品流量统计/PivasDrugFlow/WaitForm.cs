using System.Windows.Forms;

namespace PivasDrugFlow
{
    public partial class WaitForm : Form
    {
        private int i = 0;
        private MainForm MF;
        public WaitForm(MainForm mf)
        {
            this.MF = mf;
            InitializeComponent();
        }

        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            label1.Text = i++ < 3 ? label1.Text + "." : "正在查询和生成图表";
            if (MF.isok)
            {
                MF.locked = false;
                this.Dispose(true);
            }
        }
    }
}
