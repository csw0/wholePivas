using System;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PIVAsCommon.Helper;

namespace ScanPre
{
    public partial class Information : UserControl
    {
        DB_Help DB = new DB_Help();
        mySQL SQLStr = new mySQL();
      
        public string ID = "";
        DataTable dtinfo = new DataTable();
      
        Label Label_txt = new Label();
       
        public Information()
        {
            InitializeComponent();
            dgv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
      
        public void SetInformation(string preID)//设置显示信息
        {
            try
            {
                string pishi = "";
                string str = SQLStr.INFO(preID);

                DataSet ds = DB.GetPIVAsDB(str);
                dtinfo = ds.Tables[0];
                if (dtinfo.Rows.Count > 0)
                {
                    setPerson(dtinfo.Rows[0]);
                    dgv1.Rows.Clear();
                    pnlInfo.Controls.Clear();
                    for (int i = 0; i < dtinfo.Rows.Count; i++)
                    {
                        if (dtinfo.Rows[i][2].ToString() == "False")
                        {
                            pishi = "不需皮试";
                        }
                        else
                        {
                            pishi = "需皮试";
                        }
                        float f = float.Parse(dtinfo.Rows[i]["Dosage"].ToString());
                        dgv1.Rows.Add(dtinfo.Rows[i]["DrugName"].ToString(), dtinfo.Rows[i]["Spec"].ToString(), f + dtinfo.Rows[i]["DosageUnit"].ToString(), pishi, dtinfo.Rows[i]["UniPreparationID"].ToString());
                    }
                }
   

               StringBuilder s = new StringBuilder();
               s.Append(" sELECT distinct CT.CensorItem,CT.DrugACode,du.DrugName as 药品名称1, du.DrugNameJC as 药品简称1, ");
               s.Append("  CT.DrugBCode,du2.DrugName as 药品名称2, du2.DrugNameJC as 药品简称2  ");
               s.Append(" , CT.ReferenName,CT.Description,P.PrescriptionID,P.Level,  CT.Level,DoctorExplain from CPRecord CD   ");
               s.Append(" INNER JOIN Prescription P ON P.PrescriptionID=CD.PrescriptionID  ");
               s.Append(" Left JOIN CPResult CT ON CT.CheckRecID=cd.CPRecordID    ");
               s.Append(" left JOIN DDrug du on du.DrugCode=ct.DrugACode   ");
               s.Append("left JOIN DDrug du2 on du2.DrugCode=ct.DrugBCode    ");
               s.Append(" WHERE P.PrescriptionID = "+preID);
               s.Append("  ");
               s.Append("  ");


               s.Append(" SELECT distinct CT.CensorItem,CT.DrugACode,du.DrugName as 药品名称1, du.DrugNameJC as 药品简称1,");
               s.Append(" CT.DrugBCode,du2.DrugName as 药品名称2, du2.DrugNameJC as 药品简称2,");
               s.Append(" CT.ReferenName,CT.Description,P.PrescriptionID,P.Level,de.DEmployeeName,de.AccountID, ");
               s.Append(" CT.Level,DoctorExplain,BPExplain from CPRecord CD ");
               s.Append(" INNER JOIN Prescription P ON P.PrescriptionID=CD.PrescriptionID Left JOIN CPResultRG CT ON CT.CheckRecID=CD.CPRecordID ");
               s.Append(" left join [BPRecord] bp on bp.PrescriptionID= CT.PrescriptionID left JOIN DDrug du on du.DrugCode=ct.DrugACode");
               s.Append(" left JOIN DDrug du2 on du2.DrugCode=ct.DrugBCode");
               s.Append(" left join DEmployee de on de.DEmployeeID=bp.BPCode ");
               s.Append(" WHERE   ct.ReferenName='人工退单' and P.PrescriptionID = " + preID);

              
               DataSet  ds1 = DB.GetPIVAsDB(s.ToString());
               setResult1(ds1.Tables[0]); //系统审方结果
               setResult(ds1.Tables[1]);//人工审方结果显示
              
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                //Mes.ShowDialog("提示", ex.Message);
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
            tbHeight.Text = "";
            lblWeight.Text = "";
            lblAge.Text = "";

            lblBatch.Text = "";
            dgv1.Rows.Clear();
            pnlInfo.Controls.Clear();
        }

        private void setPerson(DataRow R)//设置信息
        {
            try
            {
                lblBedNo.Text = R["BedNo"].ToString();
                lblCaseID.Text = R["CaseID"].ToString();
                lblDoctor.Text = R["DocName"].ToString() + "(" + R["DoctorCode"].ToString() + ")";
                lblEndDT.Text = R["EndDT"].ToString();

                string sex = R["Sex"].ToString().ToLower().Trim();
                if ("2" == sex)
                    lblSex.Text = "女";
                else if("1" == sex)
                    lblSex.Text = "男";
                lblStartDT.Text = R["StartDT"].ToString();
                lblWard.Text = R["WardName"].ToString();

                lblPatient.Text = R["PatName"].ToString();
                ID = R["PrescriptionID"].ToString();
                if ( R["Weight"].ToString()== ""||  Convert.ToInt32(R["Weight"]) == 0)
                {
                    lblWeight.Text = "--";
                }
                else
                {
                    lblWeight.Text = R["Weight"].ToString();
                }
                if (R["Height"].ToString()=="" ||  Convert.ToInt32(R["Height"]) == 0)
                {
                    tbHeight.Text = "--";
                }
                else
                {
                    tbHeight.Text = R["Height"].ToString();
                }

                lblAge.Text = R["Birthday"].ToString();

                lblBatch.Text = R["FregCode"].ToString();
            }
            catch (System.Exception ex)
            {
                //Mes.ShowDialog("提示", ex.Message);
            }
        }

        /// <summary>
        /// 系统审方结果 
        /// </summary>
        /// <param name="dt"></param>
        private void setResult1(DataTable dt)
        {
            Label l = new Label();
            l.Text = "系统审方结果：";
            //l.Font = new Font("宋体", 12,);
            l.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            l.ForeColor = Color.OrangeRed;
            l.Height = 16;
            l.Width = 320;
            pnlInfo.Controls.Add(l);
           
            if (dt.Rows[0]["Level"].ToString() == "0")
            {
               
                Label l1 = new Label();
                l.Height = 25;
                
                l1.Text = " 通过！";
                //l1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
              //  l1.Height = 16;
                //l1.Width = 3200;
                pnlInfo.Controls.Add(l1);
            }
            else
            {
                //l.Margin = new System.Windows.Forms.Padding(10);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i]["CensorItem"].ToString().Trim()))
                    {
                        result r = new result();
                        r.setResult1(dt.Rows[i]);
                        r.Location = new Point(l.Left, l.Top + 40);
                        r.Width = pnlInfo.Width;

                        pnlInfo.Controls.Add(r);
                    }
                }
            }
            
        }

        /// <summary>
        /// 人工审方结果 
        /// </summary>
        /// <param name="dt"></param>
        private void setResult(DataTable dt)
        {
            Label l = new Label();
            l.Text = "人工审方结果：" ;
            //l.Font = new Font("宋体", 12,);
            l.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            l.ForeColor = Color.OrangeRed;
            l.Height = 16;
            l.Width = 320;
            pnlInfo.Controls.Add(l);
            //l.Margin = new System.Windows.Forms.Padding(10);
            int Count = 0;
            if (dt.Rows.Count == 0)
            {
                Label l1 = new Label();
                l.Height = 25;
                l1.Text = " 通过！";
                pnlInfo.Controls.Add(l1);
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i]["CensorItem"].ToString().Trim()))
                    {
                        result r = new result();
                        r.setResult(dt.Rows[i]);
                        r.Location = new Point(l.Left, l.Top + 40);
                        r.Width = pnlInfo.Width;
                        Count++;
                        pnlInfo.Controls.Add(r);
                    }
                }
            }
        }

        private void dgv1_DoubleClick(object sender, EventArgs e)
        {
            LibDrugManual.RichFrom mf = new LibDrugManual.RichFrom();
            string UniPreID = dgv1.CurrentRow.Cells[4].Value.ToString();
            mf.UniPreparationID = UniPreID;//UniPreID 是Drug表中的UniPreparationID 字段
            mf.ShowDialog();
        }

      

        
       
    
       

     
    }
}
