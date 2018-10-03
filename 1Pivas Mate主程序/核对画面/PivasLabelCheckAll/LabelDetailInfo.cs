using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PivasLabelCheckAll.LabelDetails;
using PIVAsCommon.Helper;

namespace PivasLabelCheckAll
{
    public partial class LabelDetailInfo : Form
    {
        #region//属性
        private string LabelNo = string.Empty;
        private string CheckDt = string.Empty;
        private string CheckTable = string.Empty;
        private PivasLabelCheckAll.dao.seldb sel = new PivasLabelCheckAll.dao.seldb();
        private PivasLabelCheckAll.LabelDetails.sql sql = new sql();
        private DB_Help db = new DB_Help();

        #endregion


        #region//方法
        public LabelDetailInfo()
        {
            InitializeComponent();
        }

        public LabelDetailInfo(string labelno, string checktable, string checkdt)
        {
            InitializeComponent();
            this.LabelNo = labelno;
            this.CheckTable = checktable;
            this.CheckDt=checkdt;
        }

        /// <summary>
        /// 药品详细信息
        /// </summary>
        /// <param name="dt1"></param>
        public void ShowLabelDetail(DataTable dt1)
        {
            dgvDrugs.Rows.Clear();
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    dgvDrugs.Rows.Add(1);
                    dgvDrugs.Rows[i].Cells["DrugName"].Value = dt1.Rows[i]["DrugName"].ToString().Trim();
                    dgvDrugs.Rows[i].Cells["Spec"].Value = dt1.Rows[i]["Spec"].ToString().Trim();
                    dgvDrugs.Rows[i].Cells["Dosage"].Value = dt1.Rows[i]["Dosage"].ToString().Trim().TrimEnd('0').TrimEnd('.') + dt1.Rows[i]["DosageUnit"].ToString().Trim();
                }

            }


        }

        /// <summary>
        /// 详细操作信息
        /// </summary>
        /// <param name="Label"></param>
        private void ShowLabelCheck(string Label)
        {
            int i = 0;
            panel1.Controls.Clear();

            DataSet ds = db.GetPIVAsDB(sql.msg(Label));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //JugeResons(ds.Tables[0]);
                //if (string.IsNullOrEmpty(richTextBox1.Text))
                //{
                //    richTextBox1.Text = "该瓶签可以扫描";
                //}
                for (int J = 0; J < ds.Tables[1].Rows.Count; J++)
                {
                    CancelMessage msg = new CancelMessage();
                    msg.setmsg("核对", ds.Tables[1].Rows[J]);
                    msg.Top = i * 20;
                    panel1.Controls.Add(msg);
                    i++;
                }
                if (int.Parse(ds.Tables[0].Rows[0]["LabelOver"].ToString()) < 0)
                {
                    CancelMessage msg = new CancelMessage();
                    msg.setmsg("配置取消", ds.Tables[0].Rows[0]);
                    msg.Top = i * 20;
                    panel1.Controls.Add(msg);
                    i++;
                }
                if (int.Parse(ds.Tables[0].Rows[0]["WardRetreat"].ToString()) > 0)
                {
                    CancelMessage msg = new CancelMessage();
                    msg.setmsg("退药", ds.Tables[0].Rows[0]);
                    msg.Top = i * 20;
                    panel1.Controls.Add(msg);
                    i++;
                }

            }
            else
            {
                //richTextBox1.Text = "不可扫描：当前瓶签号不存在或者不是瓶签号!";
            }

        }

        #endregion


        #region//事件
        private void LabelDetailInfo_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            if (LabelNo != "" && LabelNo != string.Empty)
            {

                ds = db.GetPIVAsDB(sel.GetLabelDetailInfo(LabelNo, CheckTable, CheckDt));
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = ds.Tables[0];
                }

                DataTable dt1 = sel.getIVRecordDetail(LabelNo).Tables[0];///药品信息
                if (dt1.Rows.Count > 0)
                {
                    ShowLabelDetail(dt1);
                }
                ShowLabelCheck(LabelNo);
            }
        }

        #endregion
    }
}
