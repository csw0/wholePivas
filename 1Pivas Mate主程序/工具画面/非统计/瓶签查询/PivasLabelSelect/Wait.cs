using System;
using System.Windows.Forms;

namespace PivasLabelSelect
{
    public partial class Wait : Form
    {
        public Wait()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (label1.Text.Length < 14)
                label1.Text += label1.Text + ".";
            else
                label1.Text = "正在加载";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
