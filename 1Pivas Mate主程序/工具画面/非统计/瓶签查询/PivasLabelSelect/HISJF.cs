using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace PivasLabelSelect
{
    public partial class HISJF : Form
    {
        DB_Help db = new DB_Help();
        private string labelno = string.Empty;
        public HISJF(string labelno)
        {
            InitializeComponent();
            this.labelno = labelno;
        }

        private void HISJF_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = this.labelno+" 扫描计费结果查询";
                string sql =string.Format( "SELECT [BeginTime] ,[EndTime],[Parameters],[ChargeResult],[HisReturn],[Msg],[Remark1],[Remark2],[Remark3],[Remark4] FROM [ToHisChargeLog] where labelno='{0}'",labelno);
                DataSet ds = db.GetPIVAsDB(sql);
                dataGridView1.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库表数据出错！"+ex.Message);
            }
        }
    }
}
