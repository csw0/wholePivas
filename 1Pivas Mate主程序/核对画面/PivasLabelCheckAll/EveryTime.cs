using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PivasLabelCheckAll
{
    public partial class EveryTime : Form
    {
        private ListView Time;

        public EveryTime()
        {
            InitializeComponent();
        }

        public EveryTime(ListView l)
        {
            InitializeComponent();
            this.Time = l;
        }

        private void EveryTime_Load(object sender, EventArgs e)
        {
            this.Controls.Add(Time);
            Time.Visible = true;
            Time.Dock=DockStyle.Fill;
           // Time.Show();
        }
    }
}
