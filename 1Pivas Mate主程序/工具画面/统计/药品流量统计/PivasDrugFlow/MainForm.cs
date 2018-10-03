using PIVAsCommon.Helper;
using PivasLimitDES;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PivasDrugFlow
{
    public partial class MainForm : Form
    {
        private string UserID = string.Empty;
        private DB_Help DB = new DB_Help();
        protected internal DataTable Drugs = new DataTable();
        protected internal string DrugForClass = string.Empty;
        protected internal bool isok = true;
        protected internal bool locked = false;
        protected internal string SelectBatch = string.Empty;
        protected internal string SelectAge = string.Empty;

        private DataTable ShowDT = new DataTable();
        private DataTable DWards = new DataTable();
        private DataTable ShowWard = new DataTable();
        private DataTable AllCount = new DataTable();
        private DataTable ShowCount = new DataTable();
        private DataTable ShowDrugs = new DataTable();
        private string SelectWard = string.Empty;
        private string BatchS = string.Empty;
        private string Ages = string.Empty;

        public MainForm(string UserID)
        {
            this.UserID = UserID;
            InitializeComponent();
            FirstLoad();
        }

        private void FirstLoad()
        {
            Drugs.Columns.Add("code", typeof(string));
            Drugs.Columns.Add("name", typeof(string));
            Drugs.PrimaryKey = new DataColumn[] { Drugs.Columns[0] };
            ShowDT.Columns.Add("日期", typeof(string));
            ShowDT.PrimaryKey = new DataColumn[] { ShowDT.Columns[0] };
            dataGridView1.DataSource = ShowDT;
            dataGridView1.Columns[0].Width = dataGridView1.Width;
            dataGridView1.Columns[0].HeaderText = string.Empty;
            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            using (DataSet ds = DB.GetPIVAsDB("select distinct CONVERT(bit,0) BT,dw.WardCode,dw.WardName,WardSeqNo,Spellcode from DWard dw inner join Prescription p on dw.WardCode=p.WardCode and IsOpen=1 order by WardSeqNo,WardName"))
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    DWards = ds.Tables[0].Copy();
                    DWards.PrimaryKey = new DataColumn[] { DWards.Columns[1] };
                    ShowWard = DWards.Clone();
                    ShowWard.PrimaryKey = new DataColumn[] { ShowWard.Columns[1] };
                }
            }
            dataGridView2.DataSource = ShowWard;
            dataGridView2.Columns[0].Width = 25;
            dataGridView2.Columns[0].HeaderText = string.Empty;
            dataGridView2.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView2.Columns[1].Visible = false;
            dataGridView2.Columns[2].Width = dataGridView2.Width - 45;
            dataGridView2.Columns[2].HeaderText = string.Empty;
            dataGridView2.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView2.Columns[3].Visible = false;
            dataGridView2.Columns[4].Visible = false;
            dataGridView2_SizeChanged(null, null);

            dateTimePicker1.Value = DateTime.Now.Date.AddDays(1 - DateTime.Now.Day);
            dateTimePicker2.Value = DateTime.Now.Date;
            comboBox1.SelectedIndex = 0;
            radioButton1.Checked = true;
            radioButton6.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (button1.Text)
            {
                case "药品选择":
                    {
                        new DrugSelect(this, button1.Text).ShowDialog();
                        break;
                    }
                case "类别选择":
                    {
                        new DrugClass(this).ShowDialog();
                        break;
                    }
                case "成分选择":
                    {
                        new DrugSelect(this, button1.Text).ShowDialog();
                        break;
                    }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = radioButton1.Checked ? "药品选择" : (radioButton2.Checked ? "类别选择" : "成分选择");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox1.Enabled = !(comboBox1.SelectedIndex == 0);
            if(comboBox1.SelectedIndex==0)
            {
                checkBox1.Checked = false;
            }
            switch (comboBox1.SelectedIndex)
            {
                case 2:
                    {
                        if ((dateTimePicker2.Value - dateTimePicker1.Value).Days > 2 * 366)
                        {
                            MessageBox.Show("按月统计的日期段过长！\n(不超过24个月)");
                            comboBox1.SelectedIndex = 1;
                        }
                        break;
                    }
                case 3:
                    {
                        if ((dateTimePicker2.Value - dateTimePicker1.Value).Days > 31)
                        {
                            MessageBox.Show("按日统计的日期段过长！\n(不超过31天)");
                            comboBox1.SelectedIndex = (dateTimePicker2.Value - dateTimePicker1.Value).Days > 2 * 366 ? 1 : 2;
                        }
                        break;
                    }
                default: { break; }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                dateTimePicker1.Value = dateTimePicker2.Value;
                MessageBox.Show("开始日期不能大于结束日期！");
            }
            else
            {
                if (comboBox1.SelectedIndex > 1)
                {
                    comboBox1.SelectedIndex = (dateTimePicker2.Value - dateTimePicker1.Value).Days > 2 * 366 ? 1 : 2;
                }
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value < dateTimePicker1.Value)
            {
                dateTimePicker2.Value = dateTimePicker1.Value;
                MessageBox.Show("结束日期不能小于开始日期！");
            }
            else
            {
                if (comboBox1.SelectedIndex > 1)
                {
                    comboBox1.SelectedIndex = (dateTimePicker2.Value - dateTimePicker1.Value).Days > 2 * 366 ? 1 : 2;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new BatchSelect(this, button2.Text).ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new BatchSelect(this, button3.Text).ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string err = string.Empty;
            if (isok)
            {
                locked = true;
                isok = false;
                Thread th = new Thread(() => { new WaitForm(this).ShowDialog(); });
                th.IsBackground = true;
                th.Start();
                try
                {
                    ShowDate();
                    string infdt = string.Empty;
                    switch (comboBox1.SelectedIndex)
                    {
                        case 1: { infdt = "CONVERT(VARCHAR,DATEPART(YYYY,InfusionDT))"; break; }
                        case 2: { infdt = "CONVERT(VARCHAR,DATEPART(YYYY,InfusionDT))+'-'+ CASE WHEN DATEPART(MM,InfusionDT)<10 THEN '0' ELSE '' END+CONVERT(VARCHAR,DATEPART(MM,InfusionDT))"; break; }
                        default: { infdt = "CONVERT(VARCHAR,CONVERT(DATE,InfusionDT))"; break; }
                    }
                    BatchS = SelectBatch;
                    Ages = SelectAge;
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("select DrugCode,InfusionDT,WardCode,");
                    sb.Append(string.IsNullOrEmpty(Ages) ? string.Empty : "Age,");
                    sb.Append(string.IsNullOrEmpty(BatchS) ? string.Empty : "Batch,");
                    sb.Append("SUM(DgNo) as DgNo,COUNT(1) as CUN from (");
                    sb.AppendLine("select DrugCode,");
                    sb.AppendLine(infdt);
                    sb.Append("as InfusionDT,WardCode,");
                    sb.AppendLine(string.IsNullOrEmpty(Ages) ? string.Empty : "(select case AgeSTR when '岁' then case when Age<7  then '幼儿' else case when Age<19 then '儿童' else case when Age>60 then '老年' else '成人' end end end when '月' then '婴儿' when '天' then '新生儿' end from Patient p where p.PatCode=iv.PatCode)Age,");
                    sb.Append(string.IsNullOrEmpty(BatchS) ? string.Empty : "Batch,");
                    sb.Append("DgNo ");
                    sb.AppendLine("from IVRecord iv,IVRecordDetail ivd ");
                    sb.AppendLine("where iv.IVRecordID = ivd.IVRecordID and BatchSaved = 1 and LabelOver> -1 {0} {1} ");
                    sb.AppendLine(")t group by DrugCode,InfusionDT,WardCode ");
                    sb.Append(string.IsNullOrEmpty(Ages) ? string.Empty : ",Age ");
                    sb.Append(string.IsNullOrEmpty(BatchS) ? string.Empty : ",Batch ");
                    sb.AppendLine("select DrugCode,DrugName+Spec as DrugName from DDrug order by DrugName ");
                    string dat = string.Format(" and DATEDIFF(DAY,'{0}',InfusionDT) BETWEEN 0 AND {1}", dateTimePicker1.Value.ToString("yyyy-MM-dd"), (dateTimePicker2.Value - dateTimePicker1.Value).Days);
                    //MessageBox.Show(string.Format(sb.ToString(), dat, checkBox3.Checked ? string.Empty : " and IVStatus>3 "));
                    using (DataSet ds = DB.GetPIVAsDB(string.Format(sb.ToString(), dat, checkBox3.Checked ? string.Empty : " and IVStatus>3 ")))
                    {
                        if (ds != null && ds.Tables.Count > 1)
                        {
                            AllCount = ds.Tables[0];
                            AllCount.PrimaryKey = new DataColumn[] { AllCount.Columns[0], AllCount.Columns[1], AllCount.Columns[2], AllCount.Columns[3], AllCount.Columns[4] };
                            ShowDrugs = ds.Tables[1];
                            ShowDrugs.PrimaryKey = new DataColumn[] { ShowDrugs.Columns[0] };
                            ShowWD(true);
                        }
                    }
                    SelectWard = string.Empty;
                    if (checkBox2.CheckState == CheckState.Indeterminate)
                    {
                        foreach (DataRow dr in ShowWard.Rows)
                        {
                            if (Equals(dr[0], true))
                            {
                                SelectWard = "'" + dr["WardCode"].ToString().Trim() + "'," + SelectWard;
                            }
                        }
                    }
                    if (checkBox2.CheckState == CheckState.Unchecked && dataGridView2.Rows.Count > 0)
                    {
                        SelectWard = "'" + dataGridView2.CurrentRow.Cells[1].Value.ToString() + "'";
                    }
                    SelectWard = SelectWard.TrimEnd(',');
                    if (!string.IsNullOrEmpty(DrugForClass))
                    {
                        if (radioButton2.Checked)
                        {
                            using (DataSet ds = DB.GetPIVAsDB(string.Format("SELECT distinct [DrugCode] FROM [dbo].[DDrug] dd inner join [KD0100]..[UniPrep-Medicine] um on dd.UniPreparationID =um.UniPreparationID inner join [KD0100]..[MedicineClass-Medicine] mm on mm.MedicineID= um.MedicineID and [MedicineClassID] in ({0})", DrugForClass)))
                            {
                                if (ds != null && ds.Tables.Count > 0)
                                {
                                    Drugs.Rows.Clear();
                                    foreach (DataRow dr in ds.Tables[0].Rows)
                                    {
                                        Drugs.Rows.Add(dr[0]);
                                    }
                                }
                            }
                        }
                        if (radioButton3.Checked)
                        {
                            using (DataSet ds = DB.GetPIVAsDB(string.Format("SELECT distinct dd.DrugCode FROM [KD0100]..[UniPrep-Medicine] um inner join DDrug dd on um.UniPreparationID=dd.UniPreparationID and MedicineID in ({0})", DrugForClass)))
                            {
                                if (ds != null && ds.Tables.Count > 0)
                                {
                                    Drugs.Rows.Clear();
                                    foreach (DataRow dr in ds.Tables[0].Rows)
                                    {
                                        Drugs.Rows.Add(dr[0]);
                                    }
                                }
                            }
                        }
                    }
                    locked = false;
                    ShowMainCount();
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                }
                finally
                {
                    isok = true;
                    if (!string.IsNullOrEmpty(err))
                    {
                        MessageBox.Show(err);
                    }
                }
            }
        }

        private void ShowDate()
        {
            ShowDT.Rows.Clear();
            ShowDT.Rows.Add("全部汇总");
            for (DateTime dt = dateTimePicker1.Value.Date; dt <= dateTimePicker2.Value.Date; dt = dt.AddDays(1))
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 1:
                        {
                            if (!ShowDT.Rows.Contains(dt.ToString("yyyy")))
                            {
                                ShowDT.Rows.Add(dt.ToString("yyyy"));
                            }
                            break;
                        }
                    case 2:
                        {
                            if (!ShowDT.Rows.Contains(dt.ToString("yyyy-MM")))
                            {
                                ShowDT.Rows.Add(dt.ToString("yyyy-MM"));
                            }
                            break;
                        }
                    default: { ShowDT.Rows.Add(dt.ToString("yyyy-MM-dd")); break; }
                }
            }
            dataGridView1.Rows[0].Selected = true;
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 0)
                    {
                        dataGridView2.Rows[e.RowIndex].Cells[0].Value = !true.Equals(dataGridView2.Rows[e.RowIndex].Cells[0].Value);
                        CheckALL();
                        SelectWard = string.Empty;
                        if (checkBox2.CheckState == CheckState.Indeterminate)
                        {
                            foreach (DataRow dr in ShowWard.Rows)
                            {
                                if (Equals(dr[0], true))
                                {
                                    SelectWard = "'" + dr["WardCode"].ToString().Trim() + "'," + SelectWard;
                                }
                            }
                        }
                        if (checkBox2.CheckState == CheckState.Unchecked && dataGridView2.Rows.Count > 0)
                        {
                            SelectWard = "'" + dataGridView2.CurrentRow.Cells[1].Value.ToString() + "'";
                        }
                        SelectWard = SelectWard.TrimEnd(',');
                        ShowMainCount();
                    }
                }
            }
        }

        private void CheckALL()
        {
            bool isAllSelect = true;
            bool isALLNotSelect = true;
            foreach (DataGridViewRow dgvr in dataGridView2.Rows)
            {
                if (Equals(dgvr.Cells[0].Value, true))
                {
                    isALLNotSelect = false;
                }
                else
                {
                    isAllSelect = false;
                }
            }
            checkBox2.CheckState = isAllSelect ? CheckState.Checked : (isALLNotSelect ? CheckState.Unchecked : CheckState.Indeterminate);
        }

        private void checkBox2_MouseClick(object sender, MouseEventArgs e)
        {
            checkBox2.Checked = !checkBox2.Checked;
            foreach (DataRow dr in ShowWard.Rows)
            {
                dr[0] = checkBox2.Checked;
            }
            SelectWard = string.Empty;
            if (checkBox2.CheckState == CheckState.Indeterminate)
            {
                foreach (DataRow dr in ShowWard.Rows)
                {
                    if (Equals(dr[0], true))
                    {
                        SelectWard = "'" + dr["WardCode"].ToString().Trim() + "'," + SelectWard;
                    }
                }
            }
            if (checkBox2.CheckState == CheckState.Unchecked && dataGridView2.Rows.Count > 0)
            {
                SelectWard = "'" + dataGridView2.CurrentRow.Cells[1].Value.ToString() + "'";
            }
            SelectWard = SelectWard.TrimEnd(',');
            ShowMainCount();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                ShowWD(false);
            }
        }
        private void ShowWD(bool allselect)
        {
            try
            {
                using (DataTable dt = DWards.Clone())
                {
                    foreach (DataRow dr in ShowWard.Select("BT=1"))
                    {
                        dt.Rows.Add(dr.ItemArray);
                    }
                    foreach (DataRow dr in DWards.Select(string.Format("{0} LIKE('%{1}%') AND {2} LIKE('%{1}%')", ShowWard.Columns[2].ColumnName, textBox1.Text.Trim(), ShowWard.Columns[4].ColumnName)))
                    {
                        if (!dt.Rows.Contains(dr[1]) && AllCount.Select(string.Format("WardCode='{0}'", dr["WardCode"])).Length > 0)
                        {
                            dt.Rows.Add(dr.ItemArray);
                        }
                    }
                    ShowWard.Rows.Clear();
                    foreach (DataRow dr in dt.Select(string.Empty, "WardSeqNo,WardName"))
                    {
                        ShowWard.Rows.Add(dr.ItemArray);
                    }
                    if (allselect && !checkBox2.Checked)
                    {
                        checkBox2_MouseClick(null, new MouseEventArgs(MouseButtons.Left, 1, 1, 1, 0));
                    }
                    dataGridView2_SizeChanged(null, null);
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!GetPivasLimit.Instance.Limit(UserID, "PivasDrugFlow"))
            {
                this.Dispose();
            }
        }

        private void ShowMainCount()
        {
            string err = string.Empty;
            if (locked)
            {
                return;
            }
            else
            {
                if(isok)
                {
                    Thread th = new Thread(() => { new WaitForm(this).ShowDialog(); });
                    th.IsBackground = true;
                    th.Start();
                    isok = false;
                    locked = true;
                }
            }
            try
            {
                ShowCount = new DataTable();
                ShowCount.Columns.Add("DrugCode", typeof(string));
                ShowCount.Columns.Add("药品", typeof(string));
                ShowCount.Columns.Add("用药量", typeof(int));
                ShowCount.PrimaryKey = new DataColumn[] { ShowCount.Columns[0] };
                if (!string.IsNullOrEmpty(BatchS))
                {
                    foreach (string s in BatchS.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        ShowCount.Columns.Add(s, typeof(int));
                    }
                }
                if (!string.IsNullOrEmpty(Ages))
                {
                    foreach (string s in Ages.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        ShowCount.Columns.Add(s, typeof(int));
                    }
                }
                if (checkBox1.Checked)
                {
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        if (dgvr.Index > 0 && !ShowCount.Columns.Contains(dgvr.Cells[0].Value.ToString()))
                        {
                            ShowCount.Columns.Add(dgvr.Cells[0].Value.ToString(), typeof(int));
                        }
                    }
                }
                if (AllCount.Rows.Count > 0)
                {
                    string WHDT = dataGridView1.CurrentRow.Index == 0 || checkBox1.Checked ? string.Empty : string.Format(" InfusionDT like('{0}%')", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    string WHWC = string.IsNullOrEmpty(SelectWard) ? string.Empty : string.Format(" WardCode in ({0})", SelectWard);
                    foreach (DataRow dr in ShowDrugs.Rows)
                    {
                        if (Drugs.Rows.Count > 0)
                        {
                            if (Drugs.Rows.Contains(dr["DrugCode"]) && !ShowCount.Rows.Contains(dr["DrugCode"]))
                            {
                                int all = 0;
                                bool have = false;
                                foreach (DataRow drs in AllCount.Select(string.Format("{0}{1}{2}", string.Format("DrugCode='{0}'", dr["DrugCode"]), string.IsNullOrEmpty(WHDT) ? WHDT : " and " + WHDT, string.IsNullOrEmpty(WHWC) ? WHWC : " and " + WHWC)))
                                {
                                    all = all + Convert.ToInt32((radioButton6.Checked ? drs["DgNo"] : drs["CUN"]));
                                    have = true;
                                }
                                if (have)
                                {
                                    ShowCount.Rows.Add(dr["DrugCode"], dr["DrugName"], all);
                                }
                            }
                        }
                        else
                        {
                            if (!ShowCount.Rows.Contains(dr["DrugCode"]))
                            {
                                int all = 0;
                                bool have = false;
                                foreach (DataRow drs in AllCount.Select(string.Format("{0}{1}{2}", string.Format("DrugCode='{0}'", dr["DrugCode"]), string.IsNullOrEmpty(WHDT) ? WHDT : " and " + WHDT, string.IsNullOrEmpty(WHWC) ? WHWC : " and " + WHWC)))
                                {
                                    all = all + Convert.ToInt32((radioButton6.Checked ? drs["DgNo"] : drs["CUN"]));
                                    have = true;
                                }
                                if (have)
                                {
                                    ShowCount.Rows.Add(dr["DrugCode"], dr["DrugName"], all);
                                }
                            }
                        }
                    }
                    foreach (DataRow dr in ShowCount.Rows)
                    {
                        foreach (DataColumn dc in ShowCount.Columns)
                        {
                            if (ShowCount.Columns.IndexOf(dc) > 2)
                            {
                                int all = 0;
                                if (ShowDT.Rows.Contains(dc.ColumnName))
                                {
                                    WHDT = string.Format(" and InfusionDT like('{0}%')", dc.ColumnName);
                                    foreach (DataRow drs in AllCount.Select(string.Format("{0}{1}{2}", string.Format("DrugCode='{0}'", dr["DrugCode"]), WHDT, string.IsNullOrEmpty(WHWC) ? WHWC : " and " + WHWC)))
                                    {
                                        all = all + Convert.ToInt32((radioButton6.Checked ? drs["DgNo"] : drs["CUN"]));
                                    }
                                }
                                if (BatchS.Contains(dc.ColumnName))
                                {
                                    string Batch = string.Format(" and Batch='{0}'", dc.ColumnName);
                                    WHDT = dataGridView1.CurrentRow.Index == 0 || checkBox1.Checked ? string.Empty : string.Format(" InfusionDT like('{0}%')", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                                    foreach (DataRow drs in AllCount.Select(string.Format("{0}{1}{2}{3}", string.Format("DrugCode='{0}'", dr["DrugCode"]), Batch, string.IsNullOrEmpty(WHDT) ? WHDT : " and " + WHDT, string.IsNullOrEmpty(WHWC) ? WHWC : " and " + WHWC)))
                                    {
                                        all = all + Convert.ToInt32((radioButton6.Checked ? drs["DgNo"] : drs["CUN"]));
                                    }
                                }
                                if (Ages.Contains(dc.ColumnName))
                                {
                                    string Age = string.Format(" and Age='{0}'", dc.ColumnName);
                                    WHDT = dataGridView1.CurrentRow.Index == 0 || checkBox1.Checked ? string.Empty : string.Format(" InfusionDT like('{0}%')", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                                    foreach (DataRow drs in AllCount.Select(string.Format("{0}{1}{2}{3}", string.Format("DrugCode='{0}'", dr["DrugCode"]), Age, string.IsNullOrEmpty(WHDT) ? WHDT : " and " + WHDT, string.IsNullOrEmpty(WHWC) ? WHWC : " and " + WHWC)))
                                    {
                                        all = all + Convert.ToInt32((radioButton6.Checked ? drs["DgNo"] : drs["CUN"]));
                                    }
                                }
                                dr[dc] = all;
                            }
                        }
                    }
                }
                dataGridView3.DataSource = ShowCount;
                dataGridView3.Columns[0].Visible = false;
                dataGridView3.Columns[1].Width = 250;
                foreach (DataGridViewColumn dgvc in dataGridView3.Columns)
                {
                    if (dgvc.Index > 1)
                    {
                        dgvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            finally
            {
                isok = true;
                if(!string.IsNullOrEmpty(err))
                {
                    MessageBox.Show(err);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                ShowDate();
                ShowMainCount();
            }
            dataGridView1.Enabled = !checkBox1.Checked;
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                if (dataGridView2.CurrentCell.ColumnIndex > 0)
                {
                    SelectWard = "'" + dataGridView2.CurrentRow.Cells[1].Value.ToString() + "'";
                    ShowMainCount();
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                ShowMainCount();
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if(dataGridView3.Rows.Count>0)
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
                                    foreach (DataGridViewColumn dgvc in dataGridView3.Columns)
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
                                    foreach (DataGridViewRow dgvr in dataGridView3.Rows)
                                    {
                                        string columnValue = string.Empty;
                                        foreach (DataGridViewColumn dgvc in dataGridView3.Columns)
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView2_SizeChanged(object sender, EventArgs e)
        {
            if (dataGridView2.Height < (dataGridView2.Rows.Count + 1) * dataGridView2.ColumnHeadersHeight)
            {
                dataGridView2.Columns[2].Width = dataGridView2.Width - 45;
            }
            else
            {
                dataGridView2.Columns[2].Width = dataGridView2.Width - 28;
            }
        }
    }
}
