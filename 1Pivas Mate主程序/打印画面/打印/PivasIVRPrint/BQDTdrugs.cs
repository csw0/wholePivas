using System.Data;
using System.Windows.Forms;

namespace PivasIVRPrint
{
    public partial class BQDTdrugs : UserControl
    {
        private static DataRow dr;
        public BQDTdrugs(DataRow drs)
        {
            dr = drs;
            InitializeComponent();
        }
        private void BQDTdrugs_Load(object sender, System.EventArgs e)
        {
            label1.Text = dr["DrugName"].ToString();
            label2.Text = dr["spec"].ToString();
            label3.Text = (dr["Dosage"].ToString().Contains(".") ? dr["Dosage"].ToString().TrimEnd('0').TrimEnd('.') : dr["Dosage"].ToString()) + dr["DosageUnit"];
        }
    }
}
