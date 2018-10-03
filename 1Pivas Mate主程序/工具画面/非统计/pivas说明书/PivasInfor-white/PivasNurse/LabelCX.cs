using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PivasInfor.PivasNurse
{
    public partial class LabelCX : UserControl
    {
        private string userType = string.Empty;
        public LabelCX(string a)
        {
            InitializeComponent();
           this.userType = a;
        }
    }
}
