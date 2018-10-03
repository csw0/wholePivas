using PIVAsCommon.Helper;
using PivasLimitDES;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PivasDrugNotPs
{
    internal sealed partial class MainForm : Form
    {
        private string UserID = string.Empty;
        private DB_Help db = new DB_Help();
        private DataTable MainShow = new DataTable();
        private DataTable Drugs = new DataTable();
        private DataTable Demps = new DataTable();
        private DataTable Dward = new DataTable();
        private DataTable Items = new DataTable();
        private DataTable DBDT = new DataTable();
        private DateTime beginTime = DateTime.Now.Date;
        private DateTime endTime = DateTime.Now.Date;

        internal MainForm(string s)
        {
            this.UserID = s;
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now.Date.AddDays(1 - DateTime.Now.Day);
            dateTimePicker2.Value = DateTime.Now.Date;
            radioButton1.Checked = true;
            radioButton3.Checked = true;
            StringBuilder SB = new StringBuilder();
            SB.AppendLine("SELECT DrugCode,DrugName,Spec FROM DDrug ");
            SB.AppendLine("SELECT WardCode,WardName FROM DWard where IsOpen=1 Order by WardSeqNo ");
            SB.AppendLine("SELECT DEmployeeID,DEmployeeCode,DEmployeeName FROM DEmployee ");
            using (DataSet DS = db.GetPIVAsDB(SB.ToString()))
            {
                Drugs = DS.Tables[0];
                Drugs.PrimaryKey = new DataColumn[] { Drugs.Columns[0] };
                Dward = DS.Tables[1];
                Dward.PrimaryKey = new DataColumn[] { Dward.Columns[0] };
                Demps = DS.Tables[2];
                Demps.PrimaryKey = new DataColumn[] { Demps.Columns[0], Demps.Columns[1] };
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!GetPivasLimit.Instance.Limit(UserID, "PivasSafe"))
            {
                MessageBox.Show("没有使用权限！");
                this.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder SB = new StringBuilder();
                MainShow.Clear();
                MainShow = new DataTable();
                MainShow.Columns.Add("编码", typeof(string));
                if (radioButton1.Checked)
                {
                    if (radioButton3.Checked)
                    {
                        MainShow.Columns.Add("病区", typeof(string));
                        SB.AppendLine("select distinct p.WardCode,p.GroupNo,rs.CensorItem,rs.DrugACode,rs.DrugBCode,rs.Description,rs.ReferenName");
                    }
                    else
                    {
                        MainShow.Columns.Add("医生", typeof(string));
                        SB.AppendLine("select distinct p.DoctorCode,p.GroupNo,rs.CensorItem,rs.DrugACode,rs.DrugBCode,rs.Description,rs.ReferenName");
                    }
                    SB.AppendLine("from CPResult rs inner join CPRecord rd on rs.CheckRecID=rd.CPRecordID inner join Prescription  p on rd.PrescriptionID=p.PrescriptionID");
                }
                else
                {
                    if (radioButton3.Checked)
                    {
                        MainShow.Columns.Add("病区", typeof(string));
                        SB.AppendLine("select distinct p.WardCode,p.GroupNo,rs.CensorItem,rs.DrugACode,rs.DrugBCode,rs.Description,rs.ReferenName");
                    }
                    else
                    {
                        MainShow.Columns.Add("医生", typeof(string));
                        SB.AppendLine("select distinct p.DoctorCode,p.GroupNo,rs.CensorItem,rs.DrugACode,rs.DrugBCode,rs.Description,rs.ReferenName");
                    }
                    SB.AppendLine(",(select top(1) bp.BPCode from [dbo].[BPRecord] bp where bp.PrescriptionID=rs.PrescriptionID order by bp.BPDT desc) pid ");
                    SB.AppendLine("from CPResultRG rs inner join CPRecord rd on Type=1 and rs.CheckRecID=rd.CPRecordID inner join Prescription  p on rd.PrescriptionID=p.PrescriptionID");
                }
                SB.AppendLine(string.Format("WHERE DATEDIFF(DAY,'{0}',InsertDT) between 0 and {1}", beginTime.Date.ToString("yyyy-MM-dd"), (endTime.Date - beginTime.Date).Days));

                MainShow.Columns.Add("总计", typeof(int));
                MainShow.PrimaryKey = new DataColumn[] { MainShow.Columns[0] };
                using (DataSet ds = db.GetPIVAsDB(SB.ToString()))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DBDT.Clear();
                        DBDT = ds.Tables[0];
                    }
                }
                if (DBDT.Rows.Count > 0)
                {
                    Items.Clear();
                    Items = DBDT.DefaultView.ToTable(true, "CensorItem");

                    Items.PrimaryKey = new DataColumn[] { Items.Columns[0] };
                    foreach (DataRow dr in Items.Rows)
                    {
                        MainShow.Columns.Add(dr[0].ToString(), typeof(int));
                    }

                    if (radioButton3.Checked)
                    {
                        using (DataTable WardCode = DBDT.DefaultView.ToTable(true, "WardCode"))
                        {
                            WardCode.PrimaryKey = new DataColumn[] { WardCode.Columns[0] };
                            foreach (DataRow dr in Dward.Rows)
                            {
                                if (WardCode.Rows.Contains(dr[0]))
                                {
                                    DataRow drs = MainShow.NewRow();
                                    drs[0] = dr[0];
                                    drs[1] = dr[1];
                                    int all = 0;
                                    foreach (DataRow ims in Items.Rows)
                                    {
                                        int i = DBDT.Select(string.Format("CensorItem='{0}' and WardCode='{1}'", ims[0], dr[0])).Length;
                                        drs[ims[0].ToString()] = i;
                                        all = all + i;
                                    }
                                    drs[2] = all;
                                    MainShow.Rows.Add(drs);
                                }
                            }
                        }
                    }
                    else
                    {
                        using (DataTable DoctorCode = DBDT.DefaultView.ToTable(true, "DoctorCode"))
                        {
                            DoctorCode.PrimaryKey = new DataColumn[] { DoctorCode.Columns[0] };
                            foreach (DataRow dr in Demps.Rows)
                            {
                                if (DoctorCode.Rows.Contains(dr[1]))
                                {
                                    DataRow drs = MainShow.NewRow();
                                    drs[0] = dr[1];
                                    drs[1] = dr[2];
                                    int all = 0;
                                    foreach (DataRow ims in Items.Rows)
                                    {
                                        int i = DBDT.Select(string.Format("CensorItem='{0}' and DoctorCode='{1}'", ims[0], dr[1])).Length;
                                        drs[ims[0].ToString()] = i;
                                        all = all + i;
                                    }
                                    drs[2] = all;
                                    MainShow.Rows.Add(drs);
                                }
                            }
                        }
                    }
                    dataGridView1.DataSource = MainShow;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Width = radioButton3.Checked ? 150 : 100;
                }
                else
                {
                    MessageBox.Show("未查询到数据！！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = "Execl files 2007 (*.xls)|*.xls|Execl files 2010 (*.xlsx)|*.xlsx";
                    dlg.FilterIndex = 0;
                    dlg.RestoreDirectory = true;
                    dlg.CreatePrompt = true;
                    dlg.Title = "保存为Excel文件";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        using (Stream myStream = dlg.OpenFile())
                        {
                            using (StreamWriter sw = new StreamWriter(myStream, Encoding.GetEncoding(-0)))
                            {
                                string columnTitle = string.Empty;
                                //写入列标题
                                foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                                {
                                    if (dgvc.Visible)
                                    {
                                        if (dgvc.Index > 0)
                                        {
                                            columnTitle += "\t";
                                        }
                                        columnTitle += dgvc.HeaderText;
                                    }
                                }
                                sw.WriteLine(columnTitle);
                                //写入列内容
                                foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                                {
                                    string columnValue = string.Empty;
                                    foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                                    {
                                        if (dgvc.Visible)
                                        {
                                            if (dgvc.Index > 0)
                                            {
                                                columnValue += "\t";
                                            }
                                            if (string.IsNullOrEmpty(dgvr.Cells[dgvc.Index].Value.ToString()))
                                                columnValue += string.Empty;
                                            else
                                                columnValue += "   " + dgvr.Cells[dgvc.Index].Value.ToString();
                                        }
                                    }
                                    sw.WriteLine(columnValue);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                dateTimePicker1.Value = beginTime;
            }
            else
            {
                beginTime = dateTimePicker1.Value;
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                dateTimePicker2.Value = endTime;
            }
            else
            {
                endTime = dateTimePicker2.Value;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    using (DataTable dt = new DataTable())
                    {
                        dt.Columns.Add("处方号", typeof(string));
                        dt.Columns.Add("药品", typeof(string));
                        dt.Columns.Add("作用药品", typeof(string));
                        dt.Columns.Add("描述", typeof(string));
                        dt.Columns.Add("来源", typeof(string));
                        if (radioButton2.Checked)
                        {
                            dt.Columns.Add("退方人", typeof(string));
                        }
                        flowLayoutPanel1.Controls.Clear();
                        foreach (DataRow dr in Items.Rows)
                        {
                            if (Convert.ToInt32(dataGridView1.CurrentRow.Cells[dr[0].ToString()].Value) > 0)
                            {
                                using (DataTable indt = dt.Clone())
                                {
                                    foreach (DataRow drs in DBDT.Select(string.Format("CensorItem='{0}' and " + (radioButton3.Checked ? "WardCode" : "DoctorCode") + "='{1}'", dr[0], dataGridView1.CurrentRow.Cells[0].Value)))
                                    {
                                        DataRow its = indt.NewRow();
                                        its[0] = drs["GroupNo"];
                                        its[1] = Drugs.Rows.Contains(drs["DrugACode"].ToString()) ? (Drugs.Rows.Find(drs["DrugACode"])[1].ToString() + Drugs.Rows.Find(drs["DrugACode"])[2]) : drs["DrugACode"];
                                        its[2] = Drugs.Rows.Contains(drs["DrugBCode"].ToString()) ? (Drugs.Rows.Find(drs["DrugBCode"])[1].ToString() + Drugs.Rows.Find(drs["DrugBCode"])[2]) : drs["DrugBCode"];
                                        its[3] = drs["Description"];
                                        its[4] = drs["ReferenName"];
                                        if (radioButton2.Checked)
                                        {
                                            DataRow[] emps = Demps.Select(string.Format("DEmployeeID='{0}'", drs["pid"]));
                                            its[5] = emps.Length > 0 ? emps[0][2] : drs["pid"];
                                        }
                                        indt.Rows.Add(its);
                                    }
                                    Showitems sis = new Showitems(dr[0].ToString(), indt);
                                    flowLayoutPanel1.Controls.Add(sis);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
