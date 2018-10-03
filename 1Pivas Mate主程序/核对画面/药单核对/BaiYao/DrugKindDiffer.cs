using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace BaiYaoCheck
{
    public partial class DrugKindDiffer : UserControl
    {
        DB_Help db = new DB_Help();
        SQL Mysql = new SQL();

        public DrugKindDiffer()
        {
            InitializeComponent();
        }

        private void DrugKindDiffer_Load(object sender, EventArgs e)
        {
            GetDrugKind();
        }
        private void GetDrugKind()
        {

            DataSet ds = db.GetPIVAsDB("exec comDrugKinds");
            if (ds.Tables != null && ds.Tables.Count> 0)
            {
                dgv1.Rows.Clear();
                if (checkBox1.Checked)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["num1"].ToString() != ds.Tables[0].Rows[i]["num3"].ToString() && ds.Tables[0].Rows[i]["num3"].ToString()!="0" )
                        {
                            dgv1.Rows.Add(1);
                            dgv1.Rows[dgv1.Rows.Count - 1].Cells["PatCode"].Value = ds.Tables[0].Rows[i]["PatCode"].ToString();
                            dgv1.Rows[dgv1.Rows.Count - 1].Cells["patName"].Value = ds.Tables[0].Rows[i]["PatName"].ToString();
                            dgv1.Rows[dgv1.Rows.Count - 1].Cells["GroupNo"].Value = ds.Tables[0].Rows[i]["GroupNo"].ToString();
                            dgv1.Rows[dgv1.Rows.Count - 1].Cells["Quantity"].Value = ds.Tables[0].Rows[i]["num1"].ToString();
                            dgv1.Rows[dgv1.Rows.Count - 1].Cells["DrugCount"].Value = ds.Tables[0].Rows[i]["num3"].ToString();
                            dgv1.Rows[dgv1.Rows.Count - 1].Cells["BedNo"].Value = ds.Tables[0].Rows[i]["BedNo"].ToString();
                            dgv1.Rows[dgv1.Rows.Count - 1].Cells["WardCode"].Value = ds.Tables[0].Rows[i]["WardCode"].ToString();
                            dgv1.Rows[dgv1.Rows.Count - 1].Cells["WardName"].Value = ds.Tables[0].Rows[i]["WardName"].ToString();
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["num1"].ToString() != ds.Tables[0].Rows[i]["num3"].ToString())
                        {
                            dgv1.Rows.Add(1);
                            dgv1.Rows[dgv1.Rows.Count - 1].Cells["PatCode"].Value = ds.Tables[0].Rows[i]["PatCode"].ToString();
                            dgv1.Rows[dgv1.Rows.Count - 1].Cells["patName"].Value = ds.Tables[0].Rows[i]["PatName"].ToString();
                            dgv1.Rows[dgv1.Rows.Count - 1].Cells["GroupNo"].Value = ds.Tables[0].Rows[i]["GroupNo"].ToString();
                            dgv1.Rows[dgv1.Rows.Count - 1].Cells["Quantity"].Value = ds.Tables[0].Rows[i]["num1"].ToString();
                            dgv1.Rows[dgv1.Rows.Count - 1].Cells["DrugCount"].Value = ds.Tables[0].Rows[i]["num3"].ToString();
                            dgv1.Rows[dgv1.Rows.Count - 1].Cells["BedNo"].Value = ds.Tables[0].Rows[i]["BedNo"].ToString();
                            dgv1.Rows[dgv1.Rows.Count - 1].Cells["WardCode"].Value = ds.Tables[0].Rows[i]["WardCode"].ToString();
                            dgv1.Rows[dgv1.Rows.Count - 1].Cells["WardName"].Value = ds.Tables[0].Rows[i]["WardName"].ToString();
                        }
                    }
                }
            }
        }

        private void dgv1_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count > 0)
            {
                label1.Text = dgv1.CurrentRow.Cells["patName"].Value.ToString();
                label4.Text = dgv1.CurrentRow.Cells["PatCode"].Value.ToString();
                label3.Text = dgv1.CurrentRow.Cells["WardName"].Value.ToString();
                label2.Text = dgv1.CurrentRow.Cells["BedNo"].Value.ToString();
             
                DataSet ds = db.GetPIVAsDB(Mysql.GetDrug(dgv1.CurrentRow.Cells["GroupNo"].Value.ToString()));
                if (ds.Tables != null && ds.Tables.Count > 1)
                {
                    dgv3.DataSource = ds.Tables[0];
                    dgv2.DataSource = ds.Tables[1];
                }
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            GetDrugKind();
        }

      

      
    }
}
