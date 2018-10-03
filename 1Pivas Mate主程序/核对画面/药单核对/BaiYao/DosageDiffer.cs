using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BaiYaoCheck
{
    public partial class DosageDiffer : UserControl
    {
        DB_Help db = new DB_Help();
        SQL mySQL = new SQL();
        public DosageDiffer()
        {
            InitializeComponent();
        }
        private void DosageDiffer_Load(object sender, EventArgs e)
        {
            getDrugList();
        }
        private void getDrugList()
        {
            StringBuilder str = new StringBuilder();
            str.Append("select distinct udc.[DrugName],udc.DrugCode, pd.Spec,pd.Dosage,pd.DosageUnit,udc.[Quantity],udc.QuantityUnit ");
            str.Append("from UseDrugListCheck udc   ");
            str.Append("left join PrescriptionDetail pd on pd.GroupNo= udc.GroupNo and pd.DrugCode=udc.DrugCode ");
            str.Append("where udc.CheckResult=0 and ");
            str.Append("udc.DrugCode not in(select DrugCode from DrugModel dm ");
            str.Append("where dm.DrugCode=udc.DrugCode and dm.Spec=udc.Spec and dm.Dosage=pd.Dosage)  ");

            str.Append(" update UseDrugListCheck  set Quantity=dm.DrugCount ,QuantityUnit=dm.DrugCountUnit,QuantityCheck=1 ");
            str.Append("from DrugModel dm where dm.DrugCode= UseDrugListCheck.DrugCode and QuantityCheck=0 ");
            str.Append("and dm.Spec= UseDrugListCheck.Spec and dm.Dosage= UseDrugListCheck.Dosage ");
           
            DataSet ds = db.GetPIVAsDB(str.ToString());
            if (ds.Tables != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["Quantity"].ToString() == null || ds.Tables[0].Rows[i]["Quantity"].ToString() == "0" || ds.Tables[0].Rows[i]["Quantity"].ToString().IndexOf('.') != -1 || int.Parse(ds.Tables[0].Rows[i]["Quantity"].ToString()) >= 20)
                    {
                        dgv.Rows.Add(1);
                        dgv.Rows[dgv.Rows.Count - 1].Cells["QueRen"].Value = true;
                        dgv.Rows[dgv.Rows.Count - 1].Cells["DrugName"].Value = ds.Tables[0].Rows[i]["DrugName"].ToString();
                        dgv.Rows[dgv.Rows.Count - 1].Cells["Spec"].Value = ds.Tables[0].Rows[i]["Spec"].ToString();
                        dgv.Rows[dgv.Rows.Count - 1].Cells["Dosage"].Value = float.Parse(ds.Tables[0].Rows[i]["Dosage"].ToString()) + ds.Tables[0].Rows[i]["DosageUnit"].ToString();
                        dgv.Rows[dgv.Rows.Count - 1].Cells["DosageUnit"].Value = ds.Tables[0].Rows[i]["DosageUnit"].ToString();
                        dgv.Rows[dgv.Rows.Count - 1].Cells["Quantity"].Value = ds.Tables[0].Rows[i]["Quantity"].ToString();
                        dgv.Rows[dgv.Rows.Count - 1].Cells["QuantityUnit"].Value = ds.Tables[0].Rows[i]["QuantityUnit"].ToString();
                    }
                    else
                    {
                        StringBuilder str1 = new StringBuilder();
                        str1.Append("insert into DrugModel([DrugName],[DrugCode],[Spec],[Dosage],[DosageUnit],[DrugCount],[DrugCountUnit]) ");
                        str1.Append("values('");
                        str1.Append(ds.Tables[0].Rows[i]["DrugName"].ToString());
                        str1.Append("','" + ds.Tables[0].Rows[i]["DrugCode"].ToString());
                        str1.Append("','" + ds.Tables[0].Rows[i]["Spec"].ToString());
                        str1.Append("','" + ds.Tables[0].Rows[i]["Dosage"].ToString());
                        str1.Append("','" + ds.Tables[0].Rows[i]["DosageUnit"].ToString());
                        str1.Append("','" + ds.Tables[0].Rows[i]["Quantity"].ToString());
                        str1.Append("','" + ds.Tables[0].Rows[i]["QuantityUnit"].ToString());
                        str1.Append("')  ");
                        str1.Append("update UseDrugListCheck  set QuantityCheck=1 where DrugCode='");
                        str1.Append(ds.Tables[0].Rows[i]["DrugCode"].ToString());
                        str1.Append("' and Dosage='");
                        str1.Append(ds.Tables[0].Rows[i]["Dosage"].ToString());
                        str1.Append("' and DosageUnit='");
                        str1.Append(ds.Tables[0].Rows[i]["QuantityUnit"].ToString());
                        str1.Append("'");
                        db.SetPIVAsDB(str1.ToString());
                    }
                }
            }

        }
        public int getDruglistNum()
        {
            StringBuilder str = new StringBuilder();
            str.Append("select distinct udc.[DrugName], pd.Spec,pd.Dosage,pd.DosageUnit,udc.[Quantity],udc.QuantityUnit ");
            str.Append("from UseDrugListCheck udc   ");
            str.Append("left join PrescriptionDetail pd on pd.GroupNo= udc.GroupNo and pd.DrugCode=udc.DrugCode ");
            str.Append("where udc.CheckResult=0 and ");
            str.Append("udc.DrugCode not in(select DrugCode from DrugModel dm ");
            str.Append("where dm.DrugCode=udc.DrugCode and dm.Spec=udc.Spec and dm.Dosage=pd.Dosage)  ");

            DataSet ds = db.GetPIVAsDB(str.ToString());
            if (ds.Tables != null && ds.Tables.Count > 0)
                return ds.Tables[0].Rows.Count;

            return 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
