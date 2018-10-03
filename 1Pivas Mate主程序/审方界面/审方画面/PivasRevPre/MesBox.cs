using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PivasRevPre
{
    public partial class MesBox : Form
    {
        public MesBox()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog(string title,string txt)
        {
            this.title.Text = title;
            this.txt.Text = txt;
            return this.ShowDialog();
        }
    }
}
