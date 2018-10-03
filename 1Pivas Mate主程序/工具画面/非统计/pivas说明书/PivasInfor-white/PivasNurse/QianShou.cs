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
    public partial class QianShou : UserControl
    {
        private string userType = string.Empty;
        public QianShou(string a)
        {
            InitializeComponent();
            this.userType = a;
        }
    }
}
