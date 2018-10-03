using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PivasIVRPrint
{
    public partial class DrugsColorSelect : Form
    {
        private UserControlPrint piv;
        public DrugsColorSelect(UserControlPrint dt)
        {
            this.piv = dt;
            InitializeComponent();
        }

        private void DrugsColorSelect_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = piv.DrugColor.Tables[0];
            dataGridView1.Columns["DrugName"].HeaderText = "药品名";
            dataGridView1.Columns["Spec"].HeaderText = "规格";
            foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
            {
                dgvc.Visible = (dgvc.Name == "DrugName" || dgvc.Name == "Spec");
            }
            dataGridView1.Columns["DrugName"].Width = 170;
            dataGridView1.Columns["Spec"].Width = 160;
            foreach (DataGridViewRow dgvr in dataGridView1.Rows)
            {
                dgvr.DefaultCellStyle.BackColor = Color.FromArgb(Convert.ToInt32(dgvr.Cells["DrugColor"].Value));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            piv.SaveDrugColor();
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex > -1)
            {
                using (ColorDialog cd = new ColorDialog())
                {
                    cd.Color = dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor;
                    if (cd.ShowDialog() == DialogResult.OK)
                    {
                        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = cd.Color;
                        piv.DrugColor.Tables[0].Rows.Find(dataGridView1.Rows[e.RowIndex].Cells["DrugCode"].Value)["DrugColor"] = cd.Color.ToArgb();
                    }
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(textBox1.Text.Trim()))
                {
                    dataGridView1.DataSource = piv.DrugColor.Tables[0];
                }
                else
                {
                    DataTable dt = piv.DrugColor.Tables[0].Clone();
                    dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };
                    foreach (DataRow dr in piv.DrugColor.Tables[0].Select(string.Format("DrugName like ('%{0}%') or SpellCode like ('%{0}%')", textBox1.Text.Trim())))
                    {
                        dt.Rows.Add(dr.ItemArray);
                    }
                    dataGridView1.DataSource = dt;
                }
                dataGridView1.Columns["DrugName"].HeaderText = "药品名";
                dataGridView1.Columns["Spec"].HeaderText = "规格";
                dataGridView1.Columns["DrugName"].Width = 170;
                dataGridView1.Columns["Spec"].Width = 160;
                foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                {
                    dgvc.Visible = (dgvc.Name == "DrugName" || dgvc.Name == "Spec");
                }
                foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                {
                    dgvr.DefaultCellStyle.BackColor = Color.FromArgb(Convert.ToInt32(dgvr.Cells["DrugColor"].Value));
                }
            }
        }
    }
}
