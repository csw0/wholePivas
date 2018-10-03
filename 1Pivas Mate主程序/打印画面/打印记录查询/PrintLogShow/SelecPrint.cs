using System;
using System.Windows.Forms;

namespace PrintLogShow
{
    public partial class SelecPrint : Form
    {
        private MainShow ms;
        public SelecPrint(MainShow ms)
        {
            this.ms = ms;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ms.cancon = false;
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ms.selecpint = comboBox1.SelectedIndex;
            ms.cancon = true;
            this.Dispose();
        }

        private void SelecPrint_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = ms.selecpint;
        }
    }
}
