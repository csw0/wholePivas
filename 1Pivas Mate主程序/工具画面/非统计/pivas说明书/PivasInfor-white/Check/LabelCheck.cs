using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PivasInfor.Check
{
    public partial class LabelCheck : UserControl
    {
        private string userType = string.Empty;
        public LabelCheck(string a)
        {
            InitializeComponent();
            this.userType = a;
        }
    }
}
