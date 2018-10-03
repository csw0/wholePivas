using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PivasNurse
{
    public partial class Exit : Form
    {
       public  int Res = 0;
        public Exit()
        {
            InitializeComponent();
        }
        public Exit(string head, string content)
        {
            InitializeComponent();
            button1.Text = head;
            label1.Text = content;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Res = 1;
            this.DialogResult = DialogResult.Yes;
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Res = 0;
            this.DialogResult = DialogResult.Yes;
            this.Dispose();
        }

        
    }
}
