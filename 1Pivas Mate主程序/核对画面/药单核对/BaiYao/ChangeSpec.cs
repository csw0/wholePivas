using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace BaiYaoCheck
{
    public partial class ChangeSpec : UserControl
    {
        DB_Help db = new DB_Help();
        SQL Mysql = new SQL();
        public ChangeSpec()
        {
            InitializeComponent();
        }

        private void ChangeSpec_Load(object sender, EventArgs e)
        {
            DataSet ds = db.GetPIVAsDB(Mysql.ChangeSpec());
            if (ds.Tables != null && ds.Tables.Count > 0)
                dataGridView1.DataSource = ds.Tables[0];
        }
    }
}
