using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PivasInfor.Printer
{
    public partial class PrintForm : UserControl
    {
        private string userType = string.Empty;
        public PrintForm(string a)
        {
            InitializeComponent();
            userType = string.Empty;
        }
    }
}
