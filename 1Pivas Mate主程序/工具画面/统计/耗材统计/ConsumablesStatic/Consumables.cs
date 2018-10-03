using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace ConsumablesStatic
{
    public partial class Consumables : Form
    {
        DB_Help db = new DB_Help();
        public Consumables()
        {
            InitializeComponent();
        }

        private void btnAddLow_Click(object sender, EventArgs e)
        {
            AddConsumables ac = new AddConsumables();
            ac.ShowDialog();
            if (ac.DialogResult == DialogResult.OK)
            {
                getConsumable();
            
            }
        }
        private void getConsumable()
        {
            string sql = "SELECT [ConsumablesCode],[ConsumablesName],[Spec],[ItemUnit] FROM [dbo].[Consumables]";
            DataSet ds = db.GetPIVAsDB(sql);
            if (ds != null)
            {
                dgv.DataSource = ds.Tables[0];
            
            }
        }

        private void Consumables_Load(object sender, EventArgs e)
        {
            getConsumable();
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = string.Format(" delete from Consumables where ConsumablesCode='{0}'", dgv.CurrentRow.Cells["ConsumablesCode"].Value.ToString());
            db.SetPIVAsDB(sql);
            getConsumable();
        }
    }
}
