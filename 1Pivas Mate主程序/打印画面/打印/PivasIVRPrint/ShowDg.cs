using System;
using System.Windows.Forms;

namespace PivasIVRPrint
{
    public partial class ShowDg : Form
    {
        private printlabel pl;
        public ShowDg(printlabel p)
        {
            pl = p;
            InitializeComponent();
        }
        private void ShowDg_Load(object sender, EventArgs e)
        {
            pl.ShowNewInfo();
            this.Dispose(true);
        }
    }
}
