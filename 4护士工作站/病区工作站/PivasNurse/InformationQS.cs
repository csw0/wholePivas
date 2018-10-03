using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using PivasLabelSelect;
using PIVAsCommon.Helper;

namespace PivasNurse
{
    public partial class InformationQS : UserControl
    {
        public InformationQS()
        {
            InitializeComponent();
        }
        DB_Help DB = new DB_Help();
        SQL SQLStr = new SQL();
        public string ID = "";
        Label l = new Label();
        Label Label_txt = new Label();
        result r = new result();
        public void SetInformation(string preID)//设置显示信息
        {
            try
            {
                string str = SQLStr.INFO2(preID);
                DataSet ds = DB.GetPIVAsDB(str);
              
                DataTable dtinfo =ds.Tables[0];
            
              DataTable dt = ds.Tables[1];
                clear();
                if (dtinfo.Rows.Count == 0)
                {
                    return;
                }
                setPerson(dtinfo.Rows[0]);
                setCurrentDrug(dtinfo);
                //ShowLabelCheck(LabelNo);
                setResult(dt);

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
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

            lblPatient.Text = "";
            ID = "";

            lblWeight.Text = "";
            lblAge.Text = "";
            Label_DrawerName.Text = "";
            lblBatch.Text = "";
            pnlInfo.Controls.Clear();
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
            
            DataView dv = new DataView(dt);
            DataTable newdt = dv.ToTable(true, "DrugName", "FregCode", "Spec", "PiShi", "Quantity", "UniPreparationID");
            dgv1.DataSource = newdt;
        }

        private void setResult(DataTable dt)
        {
            pnlInfo.Controls.Clear();
            l.Text = "审方结果：";
            //l.Font = new Font("宋体", 12,);
            l.Location = new Point();
            l.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            l.ForeColor = Color.DarkGreen;
            l.Height = 16;
            l.Width = 320;
            pnlInfo.Controls.Add(l);
            //l.Margin = new System.Windows.Forms.Padding(10);
            int Count = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if ((dt.Rows[i]["CensorItem"] != null && dt.Rows[i]["CensorItem"].ToString().Trim() != "")
                   || (dt.Rows[i]["ReferenName"] != null && dt.Rows[i]["ReferenName"].ToString().Trim() != ""))
                {
                    result r = new result();
                    r.setResult(dt.Rows[i]);                
                    r.Width = pnlInfo.Width-20;
                    Count++;
                    pnlInfo.Controls.Add(r);
                }

            }
            if (Count <= 0)
            {
                Label_txt.Text = "通过！";
                Label_txt.Location = new Point(l.Left+20, l.Top +20);
                Label_txt.Size = new Size(100, 40);
            
                //Label_txt.Margin = new System.Windows.Forms.Padding(10);
                pnlInfo.Controls.Add(Label_txt);
            }
            pnlInfo.HorizontalScroll.Visible = false;
            pnlInfo.VerticalScroll.Visible = true;//竖的
        }

        private void ShowLabelCheck(string Label)
        {
            int i = 0;
            pnlInfo.Controls.Clear();
            DB_Help db = new DB_Help();
            SQL sql = new SQL();
            DataSet ds = db.GetPIVAsDB(sql.msg(Label));
            for (int J = 0; J < ds.Tables[1].Rows.Count; J++)
            {
                CancelMessage msg = new CancelMessage();
                msg.setmsg("核对", ds.Tables[1].Rows[J]);
                msg.Top = i * 20;
                pnlInfo.Controls.Add(msg);
                i++;
            }
            if (int.Parse(ds.Tables[0].Rows[0]["LabelOver"].ToString()) < 0)
            {
                CancelMessage msg = new CancelMessage();
                msg.setmsg("配置取消", ds.Tables[0].Rows[0]);
                msg.Top = i * 20;
                pnlInfo.Controls.Add(msg);
                i++;
            }
            if (int.Parse(ds.Tables[0].Rows[0]["WardRetreat"].ToString()) > 0)
            {
                CancelMessage msg = new CancelMessage();
                msg.setmsg("退药", ds.Tables[0].Rows[0]);
                msg.Top = i * 20;
                pnlInfo.Controls.Add(msg);
                i++;
            }

        }

        private void dgv1_DoubleClick(object sender, EventArgs e)
        {
            LibDrugManual.RichFrom mf = new LibDrugManual.RichFrom();
            string UniPreID = dgv1.CurrentRow.Cells["UniPreparationID"].Value.ToString();
            mf.UniPreparationID = UniPreID;//UniPreID 是Drug表中的UniPreparationID 字段
            mf.ShowDialog();
        }

      
       

      
    }
}
