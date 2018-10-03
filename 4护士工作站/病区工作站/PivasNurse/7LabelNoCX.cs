using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PIVAsCommon.Helper;

namespace PivasNurse
{
    public partial class LabelNoSearch : UserControl
    {
        //public LabelNoSearch()
        //{
        //    InitializeComponent();
        //}
        public LabelNoSearch(string Employee, string WardCode)
        {
            InitializeComponent();
            this.WardCode = WardCode;
            this.EmployeeID = Employee;
        }
        private int time = 0;
        private static DB_Help db = new DB_Help();
        SQL sql = new SQL();
        private string WardCode, EmployeeID;

        private DataTable dt;

       
        /// <summary>
        /// 添加动态批次数字
        /// </summary>
        private void LoadBatch()
        {
            string str = sql.AllBatch();
            DataTable dt_Batch = db.GetPIVAsDB(str).Tables[0];

            for (int i = 0; i < dt_Batch.Rows.Count; i++)
            {
                CheckBox cb = new CheckBox();
                cb.Text = dt_Batch.Rows[i][0].ToString();
                cb.Size = new Size(30, 16);
                cb.AutoSize = true;
                cb.Checked = false;
                cb.Click+= new System.EventHandler(cbx);
                ftp.Controls.Add(cb);
            }
        }

        private void cbx(object sender, EventArgs e)
        {
            ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
            if (rb1.Checked)
            {
                LoadPrescription();
            }
            else if (rb2.Checked)
            {
                LoadPrint(); 
            }

        }
        /// <summary>
        /// dgvgre的sql语句
        /// </summary>
        /// <returns></returns>
        private string makeSql()
        {
            StringBuilder str = new StringBuilder();
            str.Append(" select distinct LabelNo,PT.BedNo,Batch,PT.PatName,IV.PrescriptionID,iv.GroupNo,FreqCode,IVStatus,WardRetreat,LabelOver");
            str.Append("  from IVRecord IV");
            str.Append(" inner join Patient PT ON PT.PatCode=IV.PatCode");
            str.Append(" inner join Prescription p on p.PrescriptionID=IV.PrescriptionID ");
            str.Append(" where 1=1 and PT.WardCode='" + WardCode + "' ");
            str.Append(" and DateDiff(dd,InfusionDT,'" + dtp.Text + "')=0 ");
            if (cb1.Checked && cb2.Checked)
            {

            }
            else if (cb1.Checked)
            {
                str.Append(" and (Ltrim(Rtrim(P.Remark5))='' or p.Remark5 is null or p.Remark5=''or p.Remark5='长期')  ");
            }
            else if (cb2.Checked)
            {
                str.Append(" and p.Remark5='ST'  ");
            }
            else if (!cb1.Checked && !cb2.Checked)
            {

                str.Append("and p.Remark5='m' ");
                //MessageBox.Show("未选中长期临时");

            }

            if (cb3.Checked && cb4.Checked)
            {

            }
            else if (cb3.Checked == true)
            {
                str.Append(" and Batch like '%" + cb3.Text + "' ");
            }
            else if (cb4.Checked == true)
            {
                str.Append(" and Batch like '%" + cb4.Text + "' ");
            }

            else if (!cb3.Checked && !cb4.Checked)
            {
                str.Append("and Batch='aaa'");
            }
            str.Append(" and TeamNumber in (-1");
            foreach (CheckBox o in ftp.Controls)
            {
                if (o.Checked == true)
                {
                    str.Append("," + (o.Text));
                }
            }
            str.Append(") ");

            switch (comboBox1.SelectedItem.ToString())
            {
                case "全部": break;
                case "已打印": str.Append(" and IVStatus=3  "); break;
                case "已打包": str.Append(" and IVStatus=13 "); break;
                case "已签收": str.Append(" and IVStatus=15 "); break;
                case "提前打包": str.Append(" and PackAdvance=1 "); break;
                case "已退药": str.Append(" and( WardRetreat =2 or WardRetreat =1)"); break;
                default: break;
            }

            if (comboBox1.SelectedItem.ToString() != "全部" && comboBox1.SelectedItem.ToString() != "已退药")
            {

                if (comboBox1.SelectedItem.ToString() == "配置取消")
                {
                    str.Append(" and LabelOver<0 ");
                }
                else
                {
                    str.Append(" and LabelOver=0 ");
                }
            }
            str.Append(" order by BedNo");
            return str.ToString();
        }
        private void LoadPrescription() //绑定dvg
        {
            button1.Visible = false;
            splitContainer1.Visible = true;
            previewQD.Visible = false;
            splitContainer1.Dock = DockStyle.Fill;
            dgvPre.Rows.Clear();
            string str = makeSql();
            using (DataSet ds = db.GetPIVAsDB(str))
            {

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {//添加瓶签信息
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvPre.Rows.Add(1);
                        dgvPre.Rows[i].Cells["LabelNo"].Value = ds.Tables[0].Rows[i]["LabelNo"].ToString();
                        dgvPre.Rows[i].Cells["BedNo"].Value = ds.Tables[0].Rows[i]["BedNo"].ToString();
                        dgvPre.Rows[i].Cells["PatName"].Value = ds.Tables[0].Rows[i]["PatName"].ToString();
                        dgvPre.Rows[i].Cells["IVStatus"].Value = CheckReturn(int.Parse(ds.Tables[0].Rows[i]["IVStatus"].ToString()), ds.Tables[0].Rows[i]["WardRetreat"].ToString(), int.Parse(ds.Tables[0].Rows[i]["LabelOver"].ToString()));
                        dgvPre.Rows[i].Cells["Batch"].Value = ds.Tables[0].Rows[i]["Batch"].ToString().Trim();
                        dgvPre.Rows[i].Cells["FreqCode"].Value = ds.Tables[0].Rows[i]["FreqCode"].ToString().Trim();
                        dgvPre.Rows[i].Cells["PreCode"].Value = ds.Tables[0].Rows[i]["PrescriptionID"].ToString();
                        dgvPre.Rows[i].Cells["TrueStatus"].Value = CheckReturn(int.Parse(ds.Tables[0].Rows[i]["IVStatus"].ToString()), "False", 0);

                        if (ds.Tables[0].Rows[i]["WardRetreat"].ToString() == "1" || ds.Tables[0].Rows[i]["WardRetreat"].ToString() == "2" || int.Parse(ds.Tables[0].Rows[i]["LabelOver"].ToString()) < 0)

                            dgvPre.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
 
                }
            }

        }


        private string CheckReturn(int a, string WardRetreat, int LabelOver)
        {
            if (WardRetreat == "2" || WardRetreat == "1")
                return "已退药";
            else if (LabelOver < 0)
                return "配置取消";
            else
            {
                switch (a)
                {
                    case 3: return "已打印";
                    case 4: return "已摆药";
                    case 5: return "已排药";
                    case 7: return "已进仓";
                    case 9: return "已配置";
                    case 11: return "已出仓";
                    case 13: return "已打包";
                    case 15: return "已签收";
                    default:
                        break;
                }
            }
            return "";
        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
            time = 120;
            if (rb1.Checked)
            {
                LoadPrescription();
            }
            else if (rb2.Checked)
            {
                LoadPrint();
            }

            //aftQianShou();
        }

        private void cb1_CheckedChanged(object sender, EventArgs e)
        {
            ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
            time = 120;
            if (rb1.Checked)
            {
                LoadPrescription();
            }
            else if (rb2.Checked)
            {
                LoadPrint();
            }
        }

        private void cb2_CheckedChanged(object sender, EventArgs e)
        {
            ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
            time = 120;
            if (rb1.Checked)
            {
                LoadPrescription();
            }
            else if (rb2.Checked)
            {
                LoadPrint();
            }
        }

        private void cb3_CheckedChanged(object sender, EventArgs e)
        {
            ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
            time = 120;
            if (rb1.Checked)
            {
                LoadPrescription();
            }
            else if (rb2.Checked)
            {
                LoadPrint();
            }
        }

        private void cb4_CheckedChanged(object sender, EventArgs e)
        {
            ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
            if (rb1.Checked)
            {
                LoadPrescription();
            }
            else if (rb2.Checked)
            {
                LoadPrint();
            }
        }

     

        private void dgvPre_Click(object sender, EventArgs e)
        {
            ((NurseWorkStation)(this.Parent.Parent)).SetTimeNO();
            if (dgvPre.Rows.Count <= 0)
                return;
            time = 120;
            string PrescriptionCode = dgvPre.CurrentRow.Cells["PreCode"].Value.ToString();
            informationQS1.SetInformation(PrescriptionCode);

        }

        private void dgvPre_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPre.Rows.Count > 1)
            {
                string PrescriptionCode = dgvPre.CurrentRow.Cells["PreCode"].Value.ToString();
                informationQS1.SetInformation(PrescriptionCode);
            }
        }
        private string BaoBiaoSql()
        {
            StringBuilder str = new StringBuilder();
            str.Append("select distinct  DWard.WardCode, DWard.WardName,PT.BedNo,Batch,PT.PatName,Iv.LabelNo,DD.DrugName,DD.Spec,IVRecordDetail.DgNo  ");
            str.Append("from IVRecord IV   ");
            str.Append("inner join Patient PT ON PT.PatCode=IV.PatCode  ");
            str.Append("left join IVRecordDetail on iv.IVRecordID=IVRecordDetail.IVRecordID  ");
            str.Append("left join Prescription p on iv.PrescriptionID=p.PrescriptionID  ");
            str.Append(" left JOIN DDrug DD ON DD.DrugCode=IVRecordDetail.DrugCode ");
            str.Append("left join DWard on DWard.WardCode=pt.WardCode ");
            str.Append(" where 1=1 and PT.WardCode='");
            str.Append(WardCode);
            str.Append("' and DateDiff(dd,InfusionDT,'");
            str.Append(dtp.Value.ToString());
            str.Append("')=0   ");

            if (cb1.Checked && cb2.Checked)
            {

            }
            else if (cb1.Checked)
            {
                str.Append(" and (Ltrim(Rtrim(P.Remark5))='' or p.Remark5 is null or p.Remark5=''or p.Remark5='长期')  ");
            }
            else if (cb2.Checked)
            {
                str.Append(" and p.Remark5='ST'  ");
            }
            else if (!cb1.Checked && !cb2.Checked)
            {

                str.Append("and p.Remark5='m' ");
                //MessageBox.Show("未选中长期临时");

            }

            if (cb3.Checked && cb4.Checked)
            {

            }
            else if (cb3.Checked == true)
            {
                str.Append(" and Batch like '%" + cb3.Text + "' ");
            }
            else if (cb4.Checked == true)
            {
                str.Append(" and Batch like '%" + cb4.Text + "' ");
            }

            else if (!cb3.Checked && !cb4.Checked)
            {
                str.Append("and Batch='aaa'");
            }
            str.Append(" and TeamNumber in (-1");
            foreach (CheckBox o in ftp.Controls)
            {
                if (o.Checked == true)
                {
                    str.Append("," + (o.Text));
                }
            }
            str.Append(") ");

            switch (comboBox1.SelectedItem.ToString())
            {
                case "全部": break;
                case "已打印": str.Append(" and IVStatus=3  "); break;
                case "已打包": str.Append(" and IVStatus=13 "); break;
                case "已签收": str.Append(" and IVStatus=15 "); break;
                case "提前打包": str.Append(" and PackAdvance=1 "); break;
                case "已退药": str.Append(" and( WardRetreat =2 or WardRetreat =1)"); break;
                default: break;
            }

            if (comboBox1.SelectedItem.ToString() != "全部" && comboBox1.SelectedItem.ToString() != "已退药")
            {

                if (comboBox1.SelectedItem.ToString() == "配置取消")
                {
                    str.Append(" and LabelOver<0 ");
                }
                else
                {
                    str.Append(" and LabelOver=0 ");
                }
            }
            str.Append(" order by BedNo");
            return str.ToString();
        }
        private int count(DataTable dt)
        { 
            List<string> lsLabel=new List<string>();
            int count1 = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string labelNO = dt.Rows[i]["LabelNo"].ToString();
                if (lsLabel.Contains(labelNO))
                {
                    continue;
                }
                else
                {
                    lsLabel.Add(labelNO);
                }
            }
            count1 = lsLabel.Count;
            return count1;
        }
        private void LoadPrint()
        {
            button1.Visible = true;
            splitContainer1.Visible = false;
            previewQD.Dock = DockStyle.Fill;
            previewQD.Visible = true;
            string str = BaoBiaoSql();
            DataSet ds = db.GetPIVAsDB(str);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
                report.Preview = previewQD;
                report.Load(".\\Crystal\\CanelDrug.frx");
                report.GetParameter("Title").Value = comboBox1.SelectedItem.ToString();
                report.GetParameter("PrintDT").Value = DateTime.Now.ToString();
                report.GetParameter("Total").Value = count(dt);
                report.RegisterData(dt, "dt");
                ((report.FindObject("Data1")) as FastReport.DataBand).DataSource = report.GetDataSource("dt");
                report.Show();
            }
            else
            {
                previewQD.Clear();
            }
        }
        private void rb2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb2.Checked)
            {
                LoadPrint();
            }
        }

        private void rb1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1.Checked)
            {
                LoadPrescription();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            previewQD.Print();
        }

        private void dgvPre_DoubleClick(object sender, EventArgs e)
        {
            time = 120;
            if (dgvPre.Rows.Count <= 0)
                return;
            #region 旧双击
            string status = dgvPre.CurrentRow.Cells["IVStatus"].Value.ToString();
            //if(status=="配置取消"||status=="已退药")
            //{
            //    status=dgvPre.CurrentRow.Cells["TrueStatus"].Value.ToString();
            //}
            //if (status != "已打印")
            //{
            //    LabelCheck lb = new LabelCheck(dgvPre.CurrentRow.Cells["LabelNo"].Value.ToString(), status);
            //    lb.Show();
            //}
            #endregion
            LabelCheck lb = new LabelCheck(dgvPre.CurrentRow.Cells["LabelNo"].Value.ToString(), status);
            lb.ShowDialog(); 
        }

        private void dtp_CloseUp(object sender, EventArgs e)
        {
            if (rb1.Checked)
            {
                LoadPrescription();
            }
            else if (rb2.Checked)
            {
                LoadPrint();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {       
            LoadPrescription();
        }

        private void LabelNoSearch_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            time = int.Parse(label12.Text);
            LoadBatch();

        }

     

    }
}
