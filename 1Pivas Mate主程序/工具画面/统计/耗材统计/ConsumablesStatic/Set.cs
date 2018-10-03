using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ConsumablesStatic
{
    public partial class Set : Form
    {
        DB_Help db = new DB_Help();
        MYSQL sql = new MYSQL();
        public Set()
        {
            InitializeComponent();
        }


        private void 添加耗材ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Consumables c = new Consumables();
            c.ShowDialog();

        }

        private void 添加配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPeiZhi apz = new AddPeiZhi();
            apz.ShowDialog();
            if (apz.DialogResult == DialogResult.OK)
            {
                selectchange();
            
            }
        }
        /// <summary>
        /// 得到普通配置
        /// </summary>
        private void GetPeiZhi1()
        {
            dgv.Rows.Clear();
            DataSet ds = db.GetPIVAsDB(sql.GetPZ());
            if (ds != null&&ds.Tables[0].Rows.Count>0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dgv.Rows.Add(1);
                    dgv.Rows[i].Cells["RuleId"].Value = ds.Tables[0].Rows[i]["RuleId"].ToString();
                    dgv.Rows[i].Cells["ConsumablesCode"].Value = ds.Tables[0].Rows[i]["ConsumablesCode"].ToString();
                    dgv.Rows[i].Cells["ComnsumableMaterial"].Value = ds.Tables[0].Rows[i]["ConsumablesName"].ToString();                    
                    dgv.Rows[i].Cells["PType"].Value = ds.Tables[0].Rows[i]["drugtype"].ToString();
                    dgv.Rows[i].Cells["ConQuantity"].Value = ds.Tables[0].Rows[i]["cbrq"].ToString();                 
                    dgv.Rows[i].DefaultCellStyle.BackColor = Getcolor(ds.Tables[0].Rows[i]["drugtype"].ToString());
                }

            }
        
        }
        /// <summary>
        /// 得到特殊配置
        /// </summary>
        private void GetSpecialPZ()
        {
            dgv2.Rows.Clear();
            DataSet ds = db.GetPIVAsDB(sql.GetSpecialPZ());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dgv2.Rows.Add(1);
                    dgv2.Rows[i].Cells["RuleId1"].Value = ds.Tables[0].Rows[i]["RuleId"].ToString();
                    dgv2.Rows[i].Cells["DrugName1"].Value = ds.Tables[0].Rows[i]["DrugName"].ToString();
                    dgv2.Rows[i].Cells["ConsumablesName1"].Value = ds.Tables[0].Rows[i]["ConsumablesName"].ToString();
                    dgv2.Rows[i].Cells["ConsumablesQuantity1"].Value = ds.Tables[0].Rows[i]["SpecQuantity"].ToString() + ds.Tables[0].Rows[i]["SpecQuantityUnit"].ToString();
                }
            }
            
        }
        /// <summary>
        /// 选项卡显示那个选项
        /// </summary>
        private void selectchange()
        {
            if (tabControl1.SelectedIndex == 0)
            {
                GetPeiZhi1();
            }
            else
            {
                GetSpecialPZ();

            }
        }

        private Color Getcolor(string color)
        {
            Color c;
            if (color == "普通药")
            {
                c = Color.Silver;
            }
            else if (color == "抗生素")
            {
                c = Color.MistyRose;
            }
            else if(color=="化疗药")
            {
                c = Color.Moccasin;
            }
            else if (color == "营养药")
            {
                c = Color.Aquamarine;
            }
            else
            {
                c = Color.White;
            }
            return c;
        }

        private void ConsumableNum_Load(object sender, EventArgs e)
        {
            GetPeiZhi1();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectchange();

        }

        private void dgv_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    dgv.ClearSelection();
                    dgv.Rows[e.RowIndex].Selected = true;
                    //弹出操作菜单
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                string sql = " delete from ConsumablesRule where RuleId=" + dgv.CurrentRow.Cells["RuleId"].Value.ToString();
                db.SetPIVAsDB(sql);
                GetPeiZhi1();
            }
            else
            {
                string sql = "delete from ConsumablesRule_Spec where RuleId=" + dgv2.CurrentRow.Cells["RuleId1"].Value.ToString();
                db.SetPIVAsDB(sql);
                GetSpecialPZ();
            }
        }

        private void dgv2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    dgv2.ClearSelection();
                    dgv2.Rows[e.RowIndex].Selected = true;
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void 添加统计条件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCondition ac = new AddCondition();
            ac.ShowDialog();
        }
    }
}
