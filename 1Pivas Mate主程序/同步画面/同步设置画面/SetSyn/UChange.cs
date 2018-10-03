using System;
using System.Windows.Forms;

namespace SetSyn
{
    public partial class UChange : UserControl
    {
        public USynSet us;
        public UChange()
        {
            InitializeComponent();
        }
        private void label1_Click(object sender, EventArgs e)
        {
            us.Label_TypeText.Text = label1.Text;
            this.Visible = false;
        }
        private void label2_Click(object sender, EventArgs e)
        {
            us.Label_TypeText.Text = label2.Text;
            this.Visible = false;
        }
        private void label3_Click(object sender, EventArgs e)
        {
            us.Label_TypeText.Text = label3.Text;
            this.Visible = false;
        }
    }
}
