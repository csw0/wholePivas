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
    public partial class BaiYao : UserControl
    {
        private string userType = string.Empty;
        public BaiYao(string a)
        {
            InitializeComponent();
            this.userType = a;
        }
    }
}
