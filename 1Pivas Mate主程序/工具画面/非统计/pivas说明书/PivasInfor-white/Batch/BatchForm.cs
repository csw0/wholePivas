using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PivasInfor.Batch
{
    public partial class BatchForm : UserControl
    {
        private string userType = string.Empty;
        public BatchForm(string a)
        {
            InitializeComponent();
           this. userType = a;
        }
    }
}
