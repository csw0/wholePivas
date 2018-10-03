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
    public partial class BatchRule : UserControl
    {
        private string userType = string.Empty;
        frmPivasInfor f = new frmPivasInfor();
        public BatchRule(string a)
        {
            InitializeComponent();
            this.userType = a;
        }

       
     
    

    }
}
