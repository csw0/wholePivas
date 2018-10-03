using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PIVAsDBhelp;

namespace DrugListConform
{
    public partial class CompareTime : UserControl
    {
        private string patientCode = string.Empty;
        DB_Help db = new DB_Help();
        SQL Mysql = new SQL();


        public CompareTime(string patcode)
        {
            InitializeComponent();
            this.patientCode = patcode;
        }

        private void ShowDrugList()
        {
            DataSet ds = db.GetPIVAsDB(Mysql.GetDrugList(patientCode));

           
            
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                dgvDruglist.Rows.Clear();
               
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    dgvDruglist.Rows.Add(1);
                    dgvDruglist.Rows[i].Cells["GroupNo"].Value = ds.Tables[0].Rows[i]["GroupNo"].ToString();
                    dgvDruglist.Rows[i].Cells["RecipeID"].Value = ds.Tables[0].Rows[i]["RecipeID"].ToString();
                    dgvDruglist.Rows[i].Cells["FregCode"].Value = ds.Tables[0].Rows[i]["FregCode"].ToString();
                    dgvDruglist.Rows[i].Cells["DrugCode"].Value = ds.Tables[0].Rows[i]["DrugCode"].ToString();
                    dgvDruglist.Rows[i].Cells["DrugName"].Value = ds.Tables[0].Rows[i]["DrugName"].ToString();
                    dgvDruglist.Rows[i].Cells["StartDt"].Value = ds.Tables[0].Rows[i]["begindt"].ToString();
                    dgvDruglist.Rows[i].Cells["occdt"].Value = ds.Tables[0].Rows[i]["OccDT"].ToString();
                    dgvDruglist.Rows[i].Cells["Spec"].Value = ds.Tables[0].Rows[i]["Spec"].ToString();
                    dgvDruglist.Rows[i].Cells["EndDt"].Value = ds.Tables[0].Rows[i]["enddt"].ToString();

                    DateTime dt1 = Convert.ToDateTime(ds.Tables[0].Rows[i]["occdt"].ToString());
                    DateTime dt2 = Convert.ToDateTime(ds.Tables[0].Rows[i]["enddt"].ToString());
                    DateTime dt3 = Convert.ToDateTime(ds.Tables[0].Rows[i]["begindt"].ToString());
                    if (dt1.ToString("yyyy-MM-dd") == dt3.ToString("yyyy-MM-dd")||dt1<DateTime.Now)
                    {
                        dgvDruglist.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }

                }
            }
        }

        private void dgvDruglist_DoubleClick(object sender, EventArgs e)
        {
            ChangeTime CT = new ChangeTime(dgvDruglist.CurrentRow.Cells["GroupNo"].Value.ToString(), dgvDruglist.CurrentRow.Cells["RecipeID"].Value.ToString());
            CT.ShowDialog();
            if (CT.DialogResult == DialogResult.Yes)
            {
                ShowDrugList();
            }
        }

        private void CompareTime_Load(object sender, EventArgs e)
        {
            ShowDrugList();
        }
    }
}
