using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using PivasLabelSelect;
using PIVAsCommon.Helper;

namespace ErrorRecord
{
    public partial class PreInformation : UserControl
    {
        public PreInformation()
        {
            InitializeComponent();
            //CheckPreID(PreID);
        }
        public PreInformation(string PreID)
        {
            InitializeComponent();
            ID = PreID;
        }
        public PreInformation(string PreID, string LabelNo)
        {
            InitializeComponent();
            ID = PreID;
            LNo = LabelNo;

        }
        //result r = new result();
        Label l = new Label();
        Label Label_txt = new Label();
        string ID=string.Empty;
        string LNo = string.Empty;
        int rc = 0;
        DB_Help DB = new DB_Help();
        SQL sql = new SQL();
        private void PreInformation_Load(object sender, EventArgs e)
        {
            if (ID != string.Empty)
            {
                if (LNo == string.Empty)
                {
                    SetInformation(ID);
                    splitContainer1.Panel2.Controls.Clear();
                    setResult(ID);
                    string sql = "select distinct labelno from ivrecord where prescriptionid = '" + ID + "'";
                    DataTable dt = DB.GetPIVAsDB(sql).Tables[0];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ShowLabelCheck(dt.Rows[i][0].ToString());
                    }
                }
                else
                {
                    SetInformation(ID);
                    setResult(ID);
                    splitContainer1.Panel2.Controls.Clear();
                    ShowLabelCheck(LNo);
                }
            }
        }

        public void SetInformation(string PreID)
        {


            DataTable dt = DB.GetPIVAsDB(sql.SelPreInfo(PreID)).Tables[0];
            SetPreson(dt.Rows[0]);
        }
        private void SetPreson(DataRow R)
        {
            try
            {
                label14.Text = ID;
                label14.Visible = true;
                if (R["ivBedNo"].ToString().Trim() != R["PaBedNo"].ToString().Trim())
                {
                    textBedNo.Text = R["ivBedNo"].ToString() + "→" + R["PaBedNo"].ToString();
                }
                else
                {
                    textBedNo.Text = R["PaBedNo"].ToString();
                }
                textCaseID.Text = R["CaseID"].ToString();
                textDoctor.Text = R["DocName"].ToString() + "(" + R["DoctorCode"].ToString() + ")";
                textDrawer.Text = R["DrawerName"].ToString() + "(" + R["DrawerCode"].ToString() + ")";
                textEndDT.Text = R["EndDT"].ToString();

                string sex = R["Sex"].ToString().ToLower().Trim();
                if ("2" == sex)
                    textSex.Text = "女";
                else if ("1" == sex)
                    textSex.Text = "男";
                textStartDT.Text = R["StartDT"].ToString();

                if (R["ivWardCode"].ToString().Trim() == R["DWardCode"].ToString().Trim())
                {
                    textDWard.Text = R["DWardName"].ToString();
                }
                else
                {
                    textDWard.Text = R["ivWardName"].ToString() + "→" + R["DWardName"].ToString();
                }

                textPatient.Text = R["PatName"].ToString();
                // ID = R["PrescriptionID"].ToString();

                textWeight.Text = R["Weight"].ToString();
                textAge.Text = R["Birthday"].ToString();

                textBatch.Text = R["FregCode"].ToString();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private void setResult(string PreID)
        {
            DataTable dt = DB.GetPIVAsDB(sql.GetResult(PreID)).Tables[0];
            splitContainer1.Panel1.Controls.Clear();
            l.Text = "审方结果：";
            //l.Font = new Font("宋体", 12,);
            l.Location = new Point();
            l.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            l.ForeColor = Color.DarkGreen;
            l.Height = 16;
            l.Width = 320;//splitContainer1.Panel1.Width;
            splitContainer1.Panel1.Controls.Add(l);
            //l.Margin = new System.Windows.Forms.Padding(10);
            int Count = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if ((dt.Rows[i]["CensorItem"] != null && dt.Rows[i]["CensorItem"].ToString().Trim() != "")
                   || (dt.Rows[i]["ReferenName"] != null && dt.Rows[i]["ReferenName"].ToString().Trim() != ""))
                {
                    result r = new result();
                    //r.Width = splitContainer1.Panel1.Width;
                    r.Height = splitContainer1.Panel1.Height - 40;
                    r.Dock = System.Windows.Forms.DockStyle.Bottom;
                    r.setResult(dt.Rows[i]);
                    //r.Width = splitContainer1.Panel1.Width - 20;
                    Count++;
                    splitContainer1.Panel1.Controls.Add(r);
                }

            }
            if (Count <= 0)
            {
                Label_txt.Text = "通过！";
                Label_txt.Location = new Point(l.Left + 20, l.Top + 20);
                Label_txt.Size = new Size(50, 40);

                //Label_txt.Margin = new System.Windows.Forms.Padding(10);
                splitContainer1.Panel1.Controls.Add(Label_txt);
            }
            splitContainer1.Panel1.HorizontalScroll.Visible = false;
            splitContainer1.Panel1.VerticalScroll.Visible = true;//竖的
        }

        private void ShowLabelCheck(string Label)
        {
            DataSet ds = DB.GetPIVAsDB(sql.msg(Label));
            for (int J = 0; J < ds.Tables[1].Rows.Count; J++)
            {
                CancelMessage msg = new CancelMessage();
                msg.setmsg("核对", ds.Tables[1].Rows[J]);
                msg.Top = rc * 20;
                splitContainer1.Panel2.Controls.Add(msg);
                rc++;
            }
            if (int.Parse(ds.Tables[0].Rows[0]["LabelOver"].ToString()) < 0)
            {
                CancelMessage msg = new CancelMessage();
                msg.setmsg("配置取消", ds.Tables[0].Rows[0]);
                msg.Top = rc * 20;
                splitContainer1.Panel2.Controls.Add(msg);
                rc++;
            }
            if (int.Parse(ds.Tables[0].Rows[0]["WardRetreat"].ToString()) > 0)
            {
                CancelMessage msg = new CancelMessage();
                msg.setmsg("退药", ds.Tables[0].Rows[0]);
                msg.Top = rc * 20;
                splitContainer1.Panel2.Controls.Add(msg);
                rc++;
            }

        }

    }
}
