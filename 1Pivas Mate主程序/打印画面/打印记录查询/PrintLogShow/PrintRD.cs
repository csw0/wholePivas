using System;
using System.Windows.Forms;

namespace PrintLogShow
{
    public partial class PrintRD : Form
    {
        private MainShow ms;
        public PrintRD(MainShow ms)
        {
            this.ms = ms;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ms.printed = true;
            this.Dispose(true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ms.printed = false;
            this.Dispose(true);
        }

        private void PrintRD_Load(object sender, EventArgs e)
        {

        }
    }
}
