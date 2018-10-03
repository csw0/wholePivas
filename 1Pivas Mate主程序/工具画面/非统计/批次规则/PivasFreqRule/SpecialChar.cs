using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace PivasFreqRule
{
    public partial class SpecialChar : UserControl
    {
        seldb sd = new seldb();
        DB_Help db = new DB_Help();
        public SpecialChar()
        {
            InitializeComponent();
        }

        private void SpecialChar_Load(object sender, EventArgs e)
        {
            GetSpecialch();
            GetBatch();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                label2.Visible = true;
                comboBox1.Visible = true;
               
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                label2.Visible = false;
                comboBox1.Visible = false;
            }
        }

        private void btnAddUp_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入特殊字符","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(textBox1.Text.Contains("]")||textBox1.Text.Contains("["))
            {
                MessageBox.Show("特殊字符中含有不可识别的字符", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (checkBox1.Checked)
            {
                db.SetPIVAsDB(sd.InsertSpecialChar(textBox1.Text, checkBox1.Checked.ToString(), comboBox1.SelectedItem.ToString()));
            }
            else
            {
                db.SetPIVAsDB(sd.InsertSpecialChar(textBox1.Text, checkBox1.Checked.ToString(),"0"));
            }
            GetSpecialch();
        }

        private void GetSpecialch()
        {
            dgv.Rows.Clear();
            DataSet ds = db.GetPIVAsDB(sd.GetSpecialChar());
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dgv.Rows.Add(1);

                    dgv.Rows[i].Cells["pictrue"].Value = global::PivasFreqRule.Properties.Resources.delete_16;
                    dgv.Rows[i].Cells["Special"].Value = ds.Tables[0].Rows[i]["StrS"].ToString();
                    dgv.Rows[i].Cells["BindBatch"].Value = ds.Tables[0].Rows[i]["BindBatch"].ToString();
                    dgv.Rows[i].Cells["Id"].Value = ds.Tables[0].Rows[i]["id"].ToString();
                }

            }
        }

      

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.Rows.Count > 0 && e.ColumnIndex == 0&&e.RowIndex>=0)
            {
                string sql = "delete from OrderDrugKandORD where Id='" + dgv.CurrentRow.Cells["Id"].Value.ToString() + "'";
                db.SetPIVAsDB(sql);
                GetSpecialch();

            }
        }

        private void GetBatch()
        {
            comboBox1.Items.Clear();
            string sql = "select OrderID from DOrder";
            DataSet ds = db.GetPIVAsDB(sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    comboBox1.Items.Add(ds.Tables[0].Rows[i]["OrderID"].ToString());
                }
            }
        }

       

      

    }
}
