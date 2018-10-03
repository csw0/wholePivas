using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BaiYaoCheck
{
    public partial class DrugNumDiffer : UserControl
    {
        DB_Help db = new DB_Help();
        public DrugNumDiffer()
        {
            InitializeComponent();
        }
        private void DrugNumDiffer_Load(object sender, EventArgs e)
        {
            GetDrugNum();
        }

        private void GetDrugNum()
        {
            StringBuilder str = new StringBuilder();
            str.Append(" select udc.RecipeID, udc.DrugListID, udc.GroupNo,udc.DrugCode, udc.DrugName,Spec,Quantity,QuantityUnit, hdc.DrugCount   ");
            str.Append(" from UseDrugListCheck udc ");
            str.Append("left join HISDrugListCount hdc on hdc.GroupNo=udc.GroupNo and hdc.DrugCode=udc.DrugCode  ");
            str.Append("where udc.DrugCountCheck=0 or udc.DrugCountCheck is null");
            
            DataSet ds =db.GetPIVAsDB(str.ToString());
            if (ds.Tables != null && ds.Tables.Count > 0)
            {
                dgv1.Rows.Clear();              
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["Quantity"].ToString() != ds.Tables[0].Rows[i]["DrugCount"].ToString())
                    {
                        dgv1.Rows.Add(1);
                        dgv1.Rows[dgv1.Rows.Count - 1].Cells["GroupNo"].Value = ds.Tables[0].Rows[i]["GroupNo"].ToString();
                        dgv1.Rows[dgv1.Rows.Count - 1].Cells["DrugName"].Value = ds.Tables[0].Rows[i]["DrugName"].ToString();
                        dgv1.Rows[dgv1.Rows.Count - 1].Cells["Spec"].Value = ds.Tables[0].Rows[i]["Spec"].ToString();
                        dgv1.Rows[dgv1.Rows.Count - 1].Cells["Quantity"].Value = ds.Tables[0].Rows[i]["Quantity"].ToString();
                        dgv1.Rows[dgv1.Rows.Count - 1].Cells["DrugCount"].Value = ds.Tables[0].Rows[i]["DrugCount"].ToString();
                        dgv1.Rows[dgv1.Rows.Count - 1].Cells["RecipeID"].Value = ds.Tables[0].Rows[i]["RecipeID"].ToString();
                        dgv1.Rows[dgv1.Rows.Count - 1].Cells["DrugListID"].Value = ds.Tables[0].Rows[i]["DrugListID"].ToString();
                        dgv1.Rows[dgv1.Rows.Count - 1].Cells["QuantityUnit"].Value = ds.Tables[0].Rows[i]["QuantityUnit"].ToString();
                    }
                    else
                    {
                        StringBuilder str1 = new StringBuilder();
                        str1.Append("update UseDrugListCheck  set DrugCountCheck=1 where DrugCode='");
                        str1.Append(ds.Tables[0].Rows[i]["DrugCode"].ToString());
                        str1.Append("' and GroupNo='");
                        str1.Append(ds.Tables[0].Rows[i]["GroupNo"].ToString());
                        str1.Append("'");
                        db.SetPIVAsDB(str1.ToString());
                    }
                }
            }
        }
    }
}
