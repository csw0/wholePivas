using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace PivasNurse
{
    public partial class Information2 : UserControl
    {
        public Information2()
        {
            InitializeComponent();
        }
        DB_Help DB = new DB_Help();
        SQL SQLStr = new SQL();        
        public string ID="";

        public void SetInformation(string preID)//设置显示信息
        {
            try
            {
                clear();
                string str = SQLStr.INFO2(preID);
                DataTable dtinfo = new DataTable();
                dtinfo = DB.GetPIVAsDB(str).Tables[0];

                if (dtinfo.Rows.Count==0)
                {
                    return;
                }
                setPerson(dtinfo.Rows[0]);
                setCurrentDrug(dtinfo);
                //setResult(dtinfo);
                
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
            lblWard.Text ="";
            lblPatient.Text ="";
            ID = "";
            lblWeight.Text = "";
            lblAge.Text = "";
            Label_DrawerName.Text = "";
            lblBatch.Text = "";
            //pnlInfo.Controls.Clear();
        }

        private void setPerson(DataRow R)//设置信息
        {
            try
            {
                if (R["ivBedNo"].ToString().Trim() != R["PaBedNo"].ToString().Trim())
                {
                    lblBedNo.Text = R["ivBedNo"].ToString() + "→" + R["PaBedNo"].ToString();
                }
                else
                {
                    lblBedNo.Text = R["PaBedNo"].ToString();
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

                if (R["ivWardCode"].ToString().Trim() == R["DWardCode"].ToString().Trim())
                {
                    lblWard.Text = R["DWardName"].ToString();
                }
                else
                { 
                    lblWard.Text = R["ivWardName"].ToString() + "→" + R["DWardName"].ToString(); 
                }

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
            dt.Columns.Add("pishi", Type.GetType("System.String"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["PiShi"].ToString() == "False")
                {

                    dt.Rows[i]["pishi"] = "不需要皮试";
                }
                else
                {
                    dt.Rows[i]["pishi"] = "需要皮试";
                }
            }
            DataView dv = new DataView(dt);
            DataTable newdt = dv.ToTable(true, "DrugName", "Spec", "FregCode", "Quantity", "pishi", "UniPreparationID");
            dgv1.DataSource = newdt;
        }

        private void dgv1_DoubleClick(object sender, EventArgs e)
        {
            LibDrugManual.RichFrom mf = new LibDrugManual.RichFrom();
            string UniPreID = dgv1.CurrentRow.Cells["UniPreparationID"].Value.ToString();
            mf.UniPreparationID = UniPreID;//UniPreID 是Drug表中的UniPreparationID 字段
            mf.ShowDialog();
        }

      
     
       

        //private void setResult(DataTable dt)
        //{
        //    pnlInfo.Controls.Clear();
        //    Label l = new Label();
        //    l.Text = "审方结果：";
        //    l.Font = new Font("宋体", 12);
        //   // l.ForeColor = Color.FromArgb(184, 194, 20);
        //    l.Height = 16;
        //    l.Width = 320;
        //    pnlInfo.Controls.Add(l);

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        result r = new result();
        //        r.setResult(dt.Rows[i]);
        //        pnlInfo.Controls.Add(r);
        //    }
        //}
    
    }
}
