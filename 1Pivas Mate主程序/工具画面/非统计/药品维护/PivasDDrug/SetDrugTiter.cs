using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PivasDDrug
{
    public partial class SetDrugTiter : Form
    {
        public string DrugCode = string.Empty;
        public SetDrugTiter()
        {
            InitializeComponent();
        }

        //public string Major;
        //public string MajorUnit;
        DB_Help db = new DB_Help();
        Select select = new Select();
        
        public void show(decimal Major, string MajorUnit)
        {
            Txt_Major.Text = Major.ToString();
            Cb_MajorUnit.Text = MajorUnit;
            DataSet ds = db.GetPIVAsDB(select.DrugTiter(DrugCode));
            //Cb_MajorUnit
            if(ds.Tables[0].Rows.Count >0)
            {
                DataRow dr=ds.Tables[0].Rows[0];
                Txt_MinValue.Text = dr["MinValue"].ToString() == "0" ? "" : dr["MinValue"].ToString();
                Txt_MaxValue.Text = dr["MaxValue"].ToString() == "0" ? "" : dr["MaxValue"].ToString();
                Cb_LessLevel.SelectedIndex = int.Parse(dr["LessLevel"].ToString()) == 0 ? 2 : int.Parse(dr["LessLevel"].ToString()) - 1;
                Cb_LargeLevel.SelectedIndex = int.Parse(dr["LargeLevel"].ToString()) == 0 ? 2 : int.Parse(dr["LargeLevel"].ToString()) - 1;
                Txt_LessResult.Text = dr["LessResult"].ToString();
                Txt_LargeResult.Text = dr["LargeResult"].ToString();
            }
        }
        private void Btn_YES_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public bool update(string DrugCode)
        {
            decimal MinValue = Txt_MinValue.Text.ToString() == "" ? (decimal)0 : decimal.Parse(Txt_MinValue.Text.ToString());
            decimal MaxValue = Txt_MaxValue.Text.ToString() == "" ? (decimal)0 : decimal.Parse(Txt_MaxValue.Text.ToString());
            string LessLevel = (Cb_LessLevel.SelectedIndex+1).ToString();
            string LargeLevel = (Cb_LargeLevel.SelectedIndex + 1).ToString();
            string LessResult=Txt_LessResult.Text.ToString();
            string LargeResult=Txt_LargeResult.Text.ToString();
            StringBuilder str = new StringBuilder();
            str.Append("EXEC bl_adddrugconcentration ");
            str.Append("'" + DrugCode + "', " + MinValue + ", '" + LessLevel + "','" + LessResult + "', " + MaxValue + ", '" + LargeLevel + "', '" + LargeResult + "'");
            int Tags = db.SetPIVAsDB(str.ToString());
            if (Tags>0)
            {
                return true; 
            } 
            else
            {
                return false;
            }
        }

        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            Txt_MinValue.Text = "";
            Txt_LargeResult.Text = "";
            Txt_MaxValue.Text = "";
            Txt_LessResult.Text = "";
            Cb_MajorUnit.SelectedIndex = 0;
            Cb_LessLevel.SelectedIndex = 2;
            Cb_LargeLevel.SelectedIndex = 2;
        }
    }
}
