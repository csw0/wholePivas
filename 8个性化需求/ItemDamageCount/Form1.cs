using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ItemDamageCount
{
    public partial class Form1 : Form
    {

        private Class.Item item;
        private Dao.sql sel=new ItemDamageCount.Dao.sql ();
        private List<Class.Item> LI;
        private DataSet ds;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDrug.Text == null || txtDrug.Text == "")
                {
                    lblWarn.Text = "药品不能为空";
                    lblWarn.ForeColor = Color.Red;
                }
                else
                {
                    item = new ItemDamageCount.Class.Item();
                    item.Drugcode = txtDrug.Tag.ToString();
                    item.Drugname = txtDrug.Text;
                    item.Spec = cbSpec.Text;
                    item.Count = Convert.ToInt32(txtCount.Text);
                    item.Money = txtMoney.Text;
                    item.Reason = cbReason.Text;
                    item.Responsibilityid = txtRespons.Tag.ToString ();
                    item.Responsibilityer = txtRespons.Text;
                    item.Reportid = txtReport.Tag.ToString();
                    item.Reporter = txtReport.Text;
                    item.Damagetime = dateTimePicker1.Value.ToShortDateString();
                    item.Date = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
                    sel = new ItemDamageCount.Dao.sql();

                    lblWarn.Text = sel.InsertRemark(item);
                    lblWarn.ForeColor = SystemColors.ControlText;

                    DgvLoad();
                }
            }
            catch(Exception ex)
            {
                lblWarn.Text = ex.Message;
                lblWarn.ForeColor  = Color.Red;
            }
        }

        //数量只允许数字
        private void txtCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        //金额只允许数字
        private void txtMoney_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar)&& e.KeyChar!=46)
            {
                e.Handled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DrugLoad();
            DgvLoad();
            EmployeeLoad();
            SpecLoad();
    
        }

        private void DgvLoad()
        {
            ds = new DataSet();
            LI = new List<ItemDamageCount.Class.Item>();
            ds = sel.SelectRemarkByDate(dateTimePicker2.Value.ToShortDateString(), dateTimePicker3.Value.ToShortDateString());

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                item = new ItemDamageCount.Class.Item();
                item.Id = r["ID"].ToString();
                item.Drugcode = r["Drugcode"].ToString();
                item.Drugname = r["Drugname"].ToString();
                item.Spec = r["Spec"].ToString();
                item.Count = Convert.ToInt32(r["Count"].ToString());
                item.Money = r["Money"].ToString();
                item.Reason = r["Reason"].ToString();
                item.Responsibilityid = r["Responsibilityid"].ToString();
                item.Responsibilityer = r["Responsibilityer"].ToString();
                item.Reportid = r["Reportid"].ToString();
                item.Reporter = r["Reporter"].ToString();
                item.Damagetime = r["Damagetime"].ToString();
                item.Date = r["Date"].ToString();

                LI.Add(item);
            }

            dataGridView1.DataSource = LI;
        }


        private void DrugLoad()
        {
            try
            {
                dgvdrug.DataSource = sel.SelectDrug();
                dgvdrug.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                //throw new Exception("药品加载错误："+ex.Message);
            }
        }

        private void EmployeeLoad()
        {
            try
            {
                dgvemployee.DataSource = sel.SelectEmployee();
                dgvemployee.Columns["DemployeeID"].Visible = dgvemployee.Columns["DemployeeCode"].Visible = false;
            }
            catch (Exception ex)
            {
                //throw new Exception("员工加载错误："+ex.Message );
            }
        }

        private void SpecLoad()
        {
            try
            {
                cbSpec.DataSource = sel.SelectSpec();
                cbSpec.DisplayMember = "Spec";
            }
            catch (Exception ex)
            {

            }
        }

        private void txtDrug_TextChanged(object sender, EventArgs e)
        {
            if (txtDrug.Text != "")
            {
                dgvdrug.Visible = true;
                UniPreparation(txtDrug.Text, dgvdrug, sel.SelectDrug(), 0);
            }
            else
            {
                dgvdrug.Visible = false;
            }
        }



        private void UniPreparation(string UnName,DataGridView dgv,DataTable dt,int mode)
        {
            string sql;
            dgv.DataSource = null;

            if (UnName != "")
            {
                string NewUnname = string.Empty;
                //dr = ds.Tables[0].Select(" 通用名 like '% + UnName + "%'");
                DataTable DTable = dt;
                DTable = dt.Copy();
                DTable.Rows.Clear();
                NewUnname = UnName.Replace("%", " ");
                NewUnname = NewUnname.Replace("*", " ");
                NewUnname = NewUnname.Replace("|", " ");
                NewUnname = NewUnname.Replace(":", " ");
                NewUnname = NewUnname.Replace("：", " ");

                switch (mode)
                {
                    case 0: sql = string.Format(" Drugcode like '%{0}%' OR Drugname like '%{0}%'", NewUnname); break;
                    case 1: sql = string.Format(" DemployeeCode like '%{0}%' OR DemployeeName like '%{0}%'", NewUnname); break;
                    //case 2: sql = string.Format(" Reportid like '%{0}%' OR Reporter like '%{0}%'", NewUnname); break;
                    default :sql="";break;

                }
                try
                {
                    DataRow[] rowsArry = dt.Select(sql);
                    foreach (DataRow row in rowsArry)
                    {
                        DTable.ImportRow(row);
                    }
                    //dt.Rows.Add(dr);
                    if (DTable.Rows.Count > 0)
                    {
                        dgv.DataSource = DTable;
                    }
                }
                catch
                {
                    //DTable = db.GetPIVAsDB(sel.UniPreparation(UnName)).Tables[0];
                    if (DTable.Rows.Count > 0)
                    {
                        dgv.DataSource = dt;
                    }
                }
            }
            else
            {
                dgv.DataSource = ds.Tables[0];
            }

            if (dgv.Rows.Count > 0)
            {
                dgv.Columns[0].Visible = false;
                if (dgv.Columns.Count > 2) { dgv.Columns[1].Visible = false; }
                //DG_UniPreparation.Columns[5].Width = 200*4; 
                dgv.ClearSelection();
            }
        }

        private void dgvdrug_DoubleClick(object sender, EventArgs e)
        {
            txtDrug.Tag = dgvdrug.Rows[dgvdrug.CurrentRow.Index].Cells["Drugcode"].Value.ToString();
            txtDrug.Text = dgvdrug.Rows[dgvdrug.CurrentRow.Index].Cells["Drugname"].Value.ToString();
            dgvdrug.Visible = false;
        }

        private void txtRespons_TextChanged(object sender, EventArgs e)
        {
            if (txtRespons.Text != "")
            {
                dgvemployee.Location = new Point (txtRespons.Location.X+2,txtRespons.Location.Y+txtRespons.Height+2);
                dgvemployee.Visible = true;
                UniPreparation(txtRespons.Text, dgvemployee, sel.SelectEmployee(), 1);
            }
            else
            {
                dgvemployee.Visible = false;
            }
        }

        private void txtReport_TextChanged(object sender, EventArgs e)
        {
            if (txtReport.Text != "")
            {
                dgvemployee.Location = new Point(txtReport.Location.X + 2, txtReport.Location.Y + txtRespons.Height + 2);
                dgvemployee.Visible = true;
                UniPreparation(txtReport.Text, dgvemployee, sel.SelectEmployee(), 1);
            }
            else
            {
                dgvemployee.Visible = false;
            }
        }

        private void dgvemployee_DoubleClick(object sender, EventArgs e)
        {
            if (dgvemployee.Location.X == txtRespons.Location.X + 2)
            {
                txtRespons.Tag = dgvemployee.Rows[dgvemployee.CurrentRow.Index].Cells["DemployeeID"].Value.ToString();
                txtRespons.Text = dgvemployee.Rows[dgvemployee.CurrentRow.Index].Cells["DemployeeName"].Value.ToString();
                dgvemployee.Visible = false;
            }
            else if (dgvemployee.Location.X == txtReport.Location.X + 2)
            {
                txtReport.Tag = dgvemployee.Rows[dgvemployee.CurrentRow.Index].Cells["DemployeeID"].Value.ToString();
                txtReport.Text = dgvemployee.Rows[dgvemployee.CurrentRow.Index].Cells["DemployeeName"].Value.ToString();
                dgvemployee.Visible = false;
            }
            else
            {

            }
        }

        private void dateTimePicker2_CloseUp(object sender, EventArgs e)
        {
            DgvLoad();
        }

        private void dateTimePicker3_CloseUp(object sender, EventArgs e)
        {
            DgvLoad();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                { MessageBox.Show("请选中要删除的行！！！"); }

                item = new ItemDamageCount.Class.Item();
                item.Id = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();
                item.Drugcode = dataGridView1.SelectedRows[0].Cells["Drugcode"].Value.ToString();
                item.Drugname = dataGridView1.SelectedRows[0].Cells["Drugname"].Value.ToString();
                item.Spec = dataGridView1.SelectedRows[0].Cells["Spec"].Value.ToString();
                item.Count = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Count"].Value.ToString());
                item.Money = dataGridView1.SelectedRows[0].Cells["Money"].Value.ToString();
                item.Reason = dataGridView1.SelectedRows[0].Cells["Reason"].Value.ToString();
                item.Responsibilityid = dataGridView1.SelectedRows[0].Cells["Responsibilityid"].Value.ToString();
                item.Responsibilityer = dataGridView1.SelectedRows[0].Cells["Responsibilityer"].Value.ToString();
                item.Reportid = dataGridView1.SelectedRows[0].Cells["Reportid"].Value.ToString();
                item.Reporter = dataGridView1.SelectedRows[0].Cells["Reporter"].Value.ToString();
                item.Damagetime = dataGridView1.SelectedRows[0].Cells["Damagetime"].Value.ToString();
                item.Date = dataGridView1.SelectedRows[0].Cells["Date"].Value.ToString();

                sel.DeleteItem(item);
                DgvLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        


    }
}
