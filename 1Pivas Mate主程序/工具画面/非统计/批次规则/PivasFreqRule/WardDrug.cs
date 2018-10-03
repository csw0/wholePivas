using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using PIVAsCommon.Helper;

namespace PivasFreqRule
{
    public partial class WardDrug : UserControl
    {
        DB_Help DB = new DB_Help();
        DataSet DS = new DataSet();
        DataTable DT = new DataTable();
        public WardDrug()
        {
            InitializeComponent();
        }
       
        private void WardDrug_Load(object sender, EventArgs e)
        {
            ShowWard();
            if (DS.Tables[0].Rows.Count > 0)
            {
                label5.Text = DS.Tables[0].Rows[0]["WardName"].ToString();
                label5.Tag = DS.Tables[0].Rows[0]["WardCode"].ToString();
                ShowLimit(DS.Tables[0].Rows[0]["WardCode"].ToString());
            }
        }

        /// <summary>
        /// 病区加载
        /// </summary>
        private void ShowWard()
        {
            string str = "select WardCode, WardName From DWard where IsOpen = 1";
            try
            {
                DS = DB.GetPIVAsDB(str);
                DT = DS.Tables[0];
                pnlWard.Controls.Clear();
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    DWard Ward = new DWard(DT.Rows[i]["WardCode"].ToString(), pnlWard);
                    Ward.Show(DT.Rows[i]);
                    Ward.Top = i * 30;
                    //默认选中第一行数据
                    if (i == 0)
                    {
                        Ward.BackColor = Color.FromArgb(140, 140, 255);
                    }
                    pnlWard.Controls.Add(Ward);
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddUp_Click(object sender, EventArgs e)
        {
            AddDrugK ak = new AddDrugK(label5.Tag.ToString());
            ak.ShowDialog();
            if (ak.DialogResult == DialogResult.Yes)
            {
                ShowLimit(label5.Tag.ToString());
            }
        }

        public void ShowLimit(string wardCode)
        {
            dgv.Rows.Clear();
            string sql = "select  DrugName,owd.DrugCode,Spec from DDrug left join OrderWcodeDrugK owd on owd.DrugCode=DDrug.DrugCode"+ 
                            " where WardCode='" + wardCode + "'";
            DataSet ds = DB.GetPIVAsDB(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dgv.Rows.Add(1);
                    dgv.Rows[i].Cells["DrugName"].Value = ds.Tables[0].Rows[i]["DrugName"].ToString();
                    dgv.Rows[i].Cells["DrugCode"].Value = ds.Tables[0].Rows[i]["DrugCode"].ToString();
                    dgv.Rows[i].Cells["Spec"].Value = ds.Tables[0].Rows[i]["Spec"].ToString();
                    dgv.Rows[i].Cells["Delete"].Value = global::PivasFreqRule.Properties.Resources.delete_16;
                }
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.Rows.Count > 0 && e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                string sql = "delete from OrderWcodeDrugK where WardCode='"+label5.Tag.ToString()+"' and DrugCode='"+dgv.CurrentRow.Cells["DrugCode"].Value.ToString()+"'";
                DB.SetPIVAsDB(sql);
                ShowLimit(label5.Tag.ToString());
            }
        }


    }
}
