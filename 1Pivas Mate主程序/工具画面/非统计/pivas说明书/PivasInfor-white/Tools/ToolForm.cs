using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PivasInfor.Tools
{
    public partial class ToolForm : UserControl
    {
        private string userType = string.Empty;
        public ToolForm(string a)
        {
            InitializeComponent();
            this.userType = a;
        }
    }
}
