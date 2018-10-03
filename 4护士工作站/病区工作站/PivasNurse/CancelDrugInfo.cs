using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace PivasNurse
{
    public partial class CancelDrugInfo : UserControl
    {
        DB_Help DB = new DB_Help();
        SQL SQLStr = new SQL();
        public string ID = "";
        Label l = new Label();
        Label Label_txt = new Label();
        result r = new result();
        public CancelDrugInfo()
        {
            InitializeComponent();
        }
        public void SetInformation(string preID,int a)
        {
            try
            {
                string str = SQLStr.INFO1(preID);
                DataSet ds = DB.GetPIVAsDB(str);
                if (ds != null)
                {
                    DataTable dtinfo = ds.Tables[0];

                    DataTable dt1 =ds.Tables[1];
                    clear();
                    if (dtinfo.Rows.Count == 0)
                    {
                        return;
                    }
                    setPerson(dtinfo.Rows[0]);
                    setCurrentDrug(dtinfo);
                    if (a == 0)
                    {
                        setCancel(dt1);
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public void clear()//清空信息
        {
            lblBedNo.Text = "";
            lblCaseID.Text = "";
            lblDoctor.Text = "";
            lblEndDT.Text = "";
            lblSex.Text = "";
            lblStartDT.Text = "";
            lblWard.Text = "";
            dgv1.Rows.Clear();
            lblPatient.Text = "";
            ID = "";
         
            lblWeight.Text = "";
            lblAge.Text = "";
            Label_DrawerName.Text = "";
            lblBatch.Text = "";
            rtb.Text = "";
            label15.Text = "";
        }
        private void setCancel(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                label15.Text = "退方人：" + dt.Rows[0]["DEmployeeName"].ToString() + "(" + dt.Rows[0]["BPCode"].ToString() + ")";
                rtb.Text = "退方理由："+dt.Rows[0]["BPExplain"].ToString();
            }
        }
        private void setPerson(DataRow R)//设置信息
        {
            try
            {
                if (R["BedNo"].ToString().Trim() != R["BedNo1"].ToString().Trim())
                {
                    lblBedNo.Text = R["BedNo1"].ToString().Trim() + "-->" + R["BedNo"].ToString();
                }
                else
                {
                    lblBedNo.Text = R["BedNo"].ToString().Trim();
                }
                lblCaseID.Text = R["CaseID"].ToString();
                lblDoctor.Text = R["DocName"].ToString() + "(" + R["DoctorCode"].ToString() + ")";
                Label_DrawerName.Text = R["DrawerName"].ToString() + "(" + R["DrawerCode"].ToString() + ")";
                lblEndDT.Text = R["EndDT"].ToString();

                string sex = R["Sex"].ToString().ToLower().Trim();
                if ("2" == sex)
                    lblSex.Text = "女";
                else if ("1" == sex)
                    lblSex.Text = "男";
                lblStartDT.Text = R["StartDT"].ToString();
                lblWard.Text = R["WardName"].ToString();
                lblPatient.Text = R["PatName"].ToString();
                ID = R["PrescriptionID"].ToString();

                lblWeight.Text = R["Weight"].ToString();
                lblAge.Text = R["Birthday"].ToString();

                lblBatch.Text = R["FregCode"].ToString();
               
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private void setCurrentDrug(DataTable dt)//设置当前处方所对药品
        {

            DataView dv = new DataView(dt);

            DataTable newdt = dv.ToTable(true, "DrugName", "Spec", "FregCode", "Quantity", "UniPreparationID");
            for (int i = 0; i < newdt.Rows.Count; i++)
            {
                dgv1.Rows.Add(1);
                dgv1.Rows[i].Cells["DrugName"].Value = newdt.Rows[i]["DrugName"].ToString();
                dgv1.Rows[i].Cells["Spec"].Value = newdt.Rows[i]["Spec"].ToString();
                dgv1.Rows[i].Cells["FregCode"].Value = newdt.Rows[i]["FregCode"].ToString();
                dgv1.Rows[i].Cells["Quantity"].Value = newdt.Rows[i]["Quantity"].ToString();
                dgv1.Rows[i].Cells["UniPreparationID"].Value = newdt.Rows[i]["UniPreparationID"].ToString();
            }
        }

        private void dgv1_Click(object sender, EventArgs e)
        {
            LibDrugManual.RichFrom mf = new LibDrugManual.RichFrom();
            string UniPreID = dgv1.CurrentRow.Cells["UniPreparationID"].Value.ToString();
            mf.UniPreparationID = UniPreID;//UniPreID 是Drug表中的UniPreparationID 字段
            mf.ShowDialog();
        }
   
    }
}
