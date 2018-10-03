using System.Data;
using System.Windows.Forms;

namespace PivasDrugNotPs
{
    internal sealed partial class Showitems : UserControl
    {
        internal Showitems(string s, DataTable dt)
        {
            InitializeComponent();
            label1.Text = s;
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Width = 110;
            if (dt.Columns.Count > 5)
            {
                dataGridView1.Columns[1].Width = 140;
                dataGridView1.Columns[2].Width = 140;
                dataGridView1.Columns[3].Width = 230;
                dataGridView1.Columns[5].Width = 60;
            }
            else
            {
                dataGridView1.Columns[1].Width = 160;
                dataGridView1.Columns[2].Width = 160;
                dataGridView1.Columns[3].Width = 250;
            }
            dataGridView1.Columns[4].Width = 135;
            if (dt.Rows.Count < 3)
            {
                this.Height = 120;
            }
            else if (dt.Rows.Count < 5)
            {
                this.Height = 160;
            }
            else if (dt.Rows.Count < 7)
            {
                this.Height = 200;
            }
            else if (dt.Rows.Count < 10)
            {
                this.Height = 270;
            }
            else
            {
                this.Height = 270;
                dataGridView1.Columns[4].Width = 120;
            }
        }
    }
}
