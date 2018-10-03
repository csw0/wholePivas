using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastReport; 
using FastReport.Preview;

namespace FormReport
{
    public partial class formReport : Form
    {
        private PreviewControl preview = null;

        public formReport()
        {
            InitializeComponent();


            this.TopLevel = false;
            this.FormBorderStyle = FormBorderStyle.None;

            FastReport.Utils.Res.LoadLocale(@"./Simplified_Chinese.frl");

            preview = new PreviewControl();
            preview.BackgroundImage = null;
            preview.Dock = DockStyle.Fill;
            //preview.StatusbarVisible = false;
            preview.ToolbarVisible = false;
            preview.Parent = this;
        }

        public void showReport(FastReport.Report _report)
        {
            this.preview.Clear();
            if (null == _report)
                return;

            _report.Preview = this.preview;
            _report.Show();
        }

        public void print()
        {
            preview.Print();
        }
    }
}
