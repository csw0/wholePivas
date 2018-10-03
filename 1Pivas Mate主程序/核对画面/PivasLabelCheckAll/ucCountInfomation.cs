using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace PivasLabelCheckAll
{
    public partial class ucCountInfomation : UserControl
    {
        private UCCommonCheck ck = null;
        public ucCountInfomation()
        {
            InitializeComponent();
        }

        public ucCountInfomation(UCCommonCheck ccc)
        {
            InitializeComponent();
            this.ck = ccc;
        }

        public void changeColor(DataTable dt)
        {
            for (int i = 1; i < dgvCountInformation.Columns.Count; i++)
            {
                dgvCountInformation.Columns[i].DefaultCellStyle.ForeColor = ColorTranslator.FromHtml(dt.Rows[0]["Color" + i].ToString());
            }
        }

        private void dgvCountInformation_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ck!=null)
                ck.Rechoice(dgvCountInformation.CurrentRow.Cells["病区"].Value.ToString());
        }
    }
}
