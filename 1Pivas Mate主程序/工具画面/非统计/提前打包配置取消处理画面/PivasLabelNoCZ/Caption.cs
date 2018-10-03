using System;
using System.Windows.Forms;

namespace PivasLabelNoCZ
{
    public partial class Caption : Form
    {
        public Caption()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Dispose(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Dispose();
        }

        private void Caption_Load(object sender, EventArgs e)
        {

        }
    }
}
