using System;
using System.Windows.Forms;

namespace PivasIVRPrint
{
    public partial class ShowMessage : Form
    {
        public ShowMessage(string value)
        {
            InitializeComponent();
            label1.Text = value;
        }

        private void ShowMessage_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Close();
        }
    }
}
