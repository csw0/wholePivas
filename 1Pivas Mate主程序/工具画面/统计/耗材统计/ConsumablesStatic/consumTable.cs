using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConsumablesStatic
{
    public partial class consumTable : UserControl
    {
        DataTable dt1 = new DataTable();
        public consumTable(DataTable dt)
        {
            InitializeComponent();
            this.dataGridView1.DataSource = dt;
        }
    }
}
