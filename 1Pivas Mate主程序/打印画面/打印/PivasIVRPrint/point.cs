using System;
using System.Drawing;
using System.Windows.Forms;

namespace PivasIVRPrint
{
    public partial class point : UserControl
    {
        private BQDetail bq;
        private static int count;
        public point()
        {
            InitializeComponent();
        }
        public point(BQDetail b,int coun)
        {
            count = coun;
            bq = b;
            InitializeComponent();
        }
        public void point_Click(object sender, EventArgs e)
        {
            if (this.BackColor != Color.Cyan)
            {
                bq.resrun(this, count);
                foreach (point c in this.Parent.Controls)
                {
                    c.BackColor = Color.DeepSkyBlue;
                    c.BorderStyle = BorderStyle.Fixed3D;
                }
                this.BackColor = Color.Cyan;
                this.BorderStyle = BorderStyle.FixedSingle;
            }
        }
    }
}
