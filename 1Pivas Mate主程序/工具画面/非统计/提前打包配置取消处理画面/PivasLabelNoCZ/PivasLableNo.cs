using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace PivasLabelNoCZ
{
    public partial class PivasLableNo : Form
    {
        private const int CS_DropSHADOW = 0x20000;
        private const int GCL_STYLE = (-26);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetClassLong(IntPtr hwnd, int nIndex);

        private DB_Help DB = new DB_Help();
        private string userID;
        private bool AllShow;
        private DataSet Tds = new DataSet();
        private DataSet DWard = new DataSet();
        private DataTable DWShow = new DataTable();
        private DataTable LabelNo = new DataTable();
        private DataTable LabelDetail = new DataTable();
        private string WardCodeS;
        private string LabelNoS;
        private string LabelSQL;
        private bool Lablechange;
        private bool TimeChange;
        private bool hasload;
        private int min = 0;
        private int max = 0;


        public PivasLableNo(string empCode,string exeType)
        {
            this.userID = empCode.Trim();
            InitializeComponent();
            try
            {
                comboBox3.Enabled = false;
                comboBox3.SelectedIndex = comboBox4.SelectedIndex = Int32.Parse(exeType);
                AllShow = Convert.ToBoolean(comboBox4.SelectedIndex);
                SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
                comboBox1.Items.Clear();
                comboBox1.Items.Add("全部区域");
                DWard = DB.GetPIVAsDB("SELECT * FROM DWard WHERE IsOpen=1 order by WardArea,WardSeqNo");
                if (DWard != null && DWard.Tables.Count > 0)
                {
                    foreach (DataRow dr in DWard.Tables[0].Rows)
                    {
                        if (!comboBox1.Items.Contains(dr["WardArea"].ToString().Trim()))
                        {
                            comboBox1.Items.Add(dr["WardArea"].ToString().Trim());
                        }
                    }
                }
                else
                {
                    MessageBox.Show("查询病区出错！！！");
                }
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
                DWShow = new DataTable();
                DWShow.Columns.Add("Checked", typeof(bool));
                DWShow.Columns.Add("WardSimName", typeof(string));
                DWShow.Columns.Add("WardCode", typeof(string));
                (dataGridView1.Columns["Checked"] as DataGridViewCheckBoxColumn).TrueValue = true;
                (dataGridView1.Columns["Checked"] as DataGridViewCheckBoxColumn).FalseValue = false;
                (dataGridView2.Columns["Check"] as DataGridViewCheckBoxColumn).TrueValue = true;
                (dataGridView2.Columns["Check"] as DataGridViewCheckBoxColumn).FalseValue = false;
                label1.Text =
                label2.Text =
                label3.Text =
                label4.Text =
                label5.Text =
                label6.Text = string.Empty;
                dateTimePicker1.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PivasLableNo_Load(object sender, EventArgs e)
        {
            dateTimePicker1_CloseUp(null, null);
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            NewRun();
        }

        private void TDS()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                if (!AllShow)
                {
                    sb.Append("SELECT CONVERT(bit,0) as [Check],i.LabelNo,i.WardName,i.WardCode,i.BedNo,i.PatName,i.Age,i.Sex,isnull(iu.CenterAct,0) CenterAct,i.FreqName,de.DEmployeeName as EName,iu.WardInsertDT,de2.DEmployeeName as CenterName,iu.CenterInsertDT,p.CaseID,i.Batch,i.InfusionDT,I.IVRecordID ,case IVStatus when 0 then '未打印' when 3 then '已打印' when 5 then '已排药' when 7 then '已进仓' when 9 then '已配置' when 11 then '已出仓' when 13 then '已打包' when 15 then '已签收' else '未知' end as IVStatus ");
                    sb.Append("FROM IVRecord I INNER JOIN Prescription p on i.PrescriptionID=p.PrescriptionID inner join [IVRecordUpdateWait] IU ON I.LabelNo=IU.WardLabelNo and DATEDIFF(DAY,'{0}',InfusionDT)>=0 and WardAct='{1}' ");
                    sb.Append("left join DEmployee de on de.DEmployeeID=iu.WardEmployeeID ");
                    sb.Append("left join DEmployee de2 on de2.DEmployeeID=iu.CenterEmployeeID order by WardCode,IVStatus,batch");
                }
                else
                {
                    sb.Append("SELECT CONVERT(bit,0) as [Check],i.LabelNo,i.WardName,i.WardCode,i.BedNo,i.PatName,i.Age,i.Sex,isnull(iu.CenterAct,0) CenterAct,i.FreqName,de.DEmployeeName as EName,iu.WardInsertDT,de2.DEmployeeName as CenterName,iu.CenterInsertDT,p.CaseID,i.Batch,i.InfusionDT,I.IVRecordID ,case IVStatus when 0 then '未打印' when 3 then '已打印' when 5 then '已排药' when 7 then '已进仓' when 9 then '已配置' when 11 then '已出仓' when 13 then '已打包' when 15 then '已签收' else '未知' end as IVStatus ");
                    sb.Append("FROM IVRecord I INNER JOIN Prescription p on i.PrescriptionID=p.PrescriptionID and DATEDIFF(DAY,'{0}',InfusionDT)>=0 left join (select * from [IVRecordUpdateWait] where WardAct='{1}') IU ON I.LabelNo=IU.WardLabelNo  ");
                    sb.Append("left join DEmployee de on de.DEmployeeID=iu.WardEmployeeID ");
                    sb.Append("left join DEmployee de2 on de2.DEmployeeID=iu.CenterEmployeeID order by WardCode,IVStatus,batch");
                }
                Tds = DB.GetPIVAsDB(string.Format(sb.ToString(), dateTimePicker1.Value.ToString("yyyy-MM-dd"), comboBox3.SelectedIndex));
                if (Tds != null && Tds.Tables.Count > 0)
                {
                    LabelNo = Tds.Tables[0].Clone();
                }
                else
                {
                    MessageBox.Show("查询瓶签记录出错！！！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            TimeChange = true;
        }

        private void NewRun()
        {
            try
            {
                using (DataTable dt = DWShow.Clone())
                {
                    if (DWard != null && DWard.Tables.Count > 0)
                    {
                        foreach (DataRow dr in DWard.Tables[0].Select(string.Format("{0} (WardName like ('%{1}%') or Spellcode like ('%{1}%'))", comboBox1.SelectedItem.ToString() == "全部区域" ? string.Empty : ("WardArea='" + comboBox1.SelectedItem.ToString() + "' and "), textBox1.ForeColor != Color.Black ? string.Empty : textBox1.Text.Trim().Replace("病区名/简拼", string.Empty)), "WardSeqNo asc"))
                        {
                            dt.Rows.Add(false, dr["WardSimName"].ToString(), dr["WardCode"].ToString());
                        }
                    }
                    string WardCodeS = string.Empty;
                    if (Tds != null && Tds.Tables.Count > 0)
                    {
                        foreach (DataRow dr in Tds.Tables[0].Rows)
                        {
                            if (!WardCodeS.Contains("'" + dr["WardCode"].ToString() + "'"))
                            {
                                WardCodeS = WardCodeS + "'" + dr["WardCode"].ToString() + "',";
                            }
                        }
                    }
                    DWShow.Rows.Clear();
                    WardCodeS = WardCodeS.TrimEnd(',');
                    if (!string.IsNullOrEmpty(WardCodeS))
                    {
                        foreach (DataRow dr in dt.Select(string.Format("WardCode in ({0})", WardCodeS)))
                        {
                            DWShow.Rows.Add(dr.ItemArray);
                        }
                    }
                    if (DWShow.Rows.Count == 0)
                    {
                        MessageBox.Show("当前条件下无病区数据！！！");
                    }
                    else
                    {
                        dataGridView1.DataSource = DWShow;
                    }
                    checkBox1.Checked = true;
                    checkBox1_MouseClick(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex >= 0)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[0].Value = !true.Equals(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    WardCodeS = string.Empty;
                    bool allcheck = true;
                    bool alluncheck = true;
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        if (true.Equals(dgvr.Cells["Checked"].Value))
                        {
                            WardCodeS = WardCodeS + "'" + dgvr.Cells["WardCode"].Value + "',";
                            alluncheck = false;
                        }
                        else
                        {
                            allcheck = false;
                        }
                    }
                    checkBox1.CheckState = allcheck ? CheckState.Checked : (alluncheck ? CheckState.Unchecked : CheckState.Indeterminate);
                    WardCodeS = WardCodeS.TrimEnd(',');
                    if (!string.IsNullOrEmpty(WardCodeS))
                    {
                        LabelSQL = string.Format("WardCode in ({0}) {1}", WardCodeS, comboBox2.SelectedIndex < 3 ? (" and CenterAct= " + comboBox2.SelectedIndex) : string.Empty);
                        ShowLabelNo();
                    }
                    else
                    {
                        TableClr();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                NewRun();
            }
        }


        private void ShowLabelNo()
        {
            try
            {
                string ivrecordID = string.Empty;
                if (!string.IsNullOrEmpty(WardCodeS))
                {
                    LabelNo.Rows.Clear();
                    foreach (DataRow dr in Tds.Tables[0].Select(LabelSQL))
                    {
                        LabelNo.Rows.Add(dr.ItemArray);
                        ivrecordID = ivrecordID + dr["IVRecordID"].ToString() + ",";
                    }
                    dataGridView2.DataSource = LabelNo;
                    foreach (DataGridViewRow dgvr in dataGridView2.Rows)
                    {
                        switch (dgvr.Cells["CenterAct"].Value.ToString())
                        {
                            case "1":
                                {
                                    dgvr.DefaultCellStyle.BackColor = Color.LightSalmon;
                                    dgvr.DefaultCellStyle.SelectionBackColor = Color.Tomato;
                                    break;
                                }
                            case "2":
                                {
                                    dgvr.DefaultCellStyle.BackColor = Color.Silver;
                                    dgvr.DefaultCellStyle.SelectionBackColor = Color.Gray;
                                    break;
                                }
                        }
                    }
                    Lablechange = false;
                    checkBox2.Checked = false;
                    label14.Text = dataGridView2.RowCount.ToString();
                    textBox3.Text = "0";
                    textBox4.Text = "0";
                    if (!string.IsNullOrEmpty(ivrecordID))
                    {
                        using (DataSet ds = DB.GetPIVAsDB(string.Format("select IVRecordID,DrugName,Spec,convert(varchar,Dosage)+DosageUnit as DosageS from IVRecordDetail where IVRecordID in ({0}) order by IVRecordID,Dosage desc", ivrecordID.TrimEnd(','))))
                        {
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                LabelDetail = ds.Tables[0];
                                ShowLableDetail();
                            }
                            else
                            {
                                MessageBox.Show("查询瓶签明细出错！！！");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("当前选择的病区无符合！！！");
                    }
                }
                else
                {
                    TableClr();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowLableDetail()
        {
            try
            {
                Lablechange = false;
                using (DataTable dt = LabelDetail.Clone())
                {
                    if (dataGridView2.SelectedRows.Count > 0)
                    {
                        foreach (DataRow dr in LabelDetail.Select("IVRecordID=" + dataGridView2.SelectedRows[0].Cells["IVRecordID"].Value))
                        {
                            dt.Rows.Add(dr.ItemArray);
                        }
                        dataGridView3.DataSource = dt;
                        StringBuilder sb = new StringBuilder();
                        sb.Append("select tp.CZ,tp.DT,de.DEmployeeCode as EID from (");
                        sb.Append("SELECT '签收' as CZ,[QSDT] as DT,[PCode] AS EID FROM [IVRecord_QS] where IVRecordID='{0}' and ScanCount=0 ");
                        sb.Append("union SELECT '打包' as CZ,[DBDT] as DT,[PCode] AS EID FROM [IVRecord_DB] where IVRecordID='{0}' and ScanCount=0 ");
                        sb.Append("union SELECT '出舱' as CZ,[CCDT] as DT,[PCode] AS EID FROM [IVRecord_CC] where IVRecordID='{0}' and ScanCount=0 ");
                        sb.Append("union SELECT '配置' as CZ,[PZDT] as DT,[PCode] AS EID FROM [IVRecord_PZ] where IVRecordID='{0}' and ScanCount=0 ");
                        sb.Append("union SELECT '进舱' as CZ,[JCDT] as DT,[PCode] AS EID FROM [IVRecord_JC] where IVRecordID='{0}' and ScanCount=0 ");
                        sb.Append("union SELECT '排药' as CZ,[PYDT] as DT,[PCode] AS EID FROM [IVRecord_PY] where IVRecordID='{0}' and ScanCount=0 ");
                        sb.Append("union SELECT '贴签' as CZ,[YSDT] as DT,[PCode] AS EID FROM [IVRecord_YS_ZJG] where IVRecordID='{0}' and ScanCount=0 ");
                        sb.Append("union SELECT '拿药' as CZ,[YPDT] as DT,[PCode] AS EID FROM [IVRecord_YP_ZJG] where IVRecordID='{0}' and ScanCount=0 ");
                        sb.Append("union SELECT '打印' as CZ,[PrintDT] as DT,[PrintCode] AS EID FROM [IVRecord_Print] where LabelNo='{0}' and PrintCount=0 ");
                        sb.Append("union SELECT '审方' as CZ,[CPDT] as DT ,CheckDCode as EID FROM [CPRecord] cp inner join IVRecord on cp.PrescriptionID = IVRecord.PrescriptionID and LabelNo='{0}' ");
                        sb.Append(") tp left join DEmployee de on tp.EID=de.DEmployeeID order by DT");
                        using (DataSet ds = DB.GetPIVAsDB(string.Format(sb.ToString(), dataGridView2.SelectedRows[0].Cells["Label"].Value)))
                        {
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                dataGridView4.DataSource = ds.Tables[0];
                            }
                        }
                        label1.Text = dataGridView2.SelectedRows[0].Cells["Age"].Value.ToString().Trim() + "岁";
                        label2.Text = dataGridView2.SelectedRows[0].Cells["Sex"].Value.ToString().Replace("1", "男").Replace("2", "女");
                        label3.Text = dataGridView2.SelectedRows[0].Cells["CaseID"].Value.ToString();
                        label4.Text = dataGridView2.SelectedRows[0].Cells["Batch"].Value.ToString();
                        label5.Text = dataGridView2.SelectedRows[0].Cells["InfusionDT"].Value.ToString();
                        label6.Text = dataGridView2.SelectedRows[0].Cells["FreqName"].Value.ToString();
                    }
                }
                hasload = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                LabelNoS = string.Empty;
                foreach (DataGridViewRow dgvr in dataGridView2.Rows)
                {
                    if (true.Equals(dgvr.Cells["check"].Value))
                    {
                        LabelNoS = LabelNoS + "'" + dgvr.Cells["Label"].Value + "',";
                    }
                }
                LabelNoS = LabelNoS.TrimEnd(',');
                if (!string.IsNullOrEmpty(LabelNoS))
                {
                    if (new Caption().ShowDialog() == DialogResult.Yes)
                    {
                        if (AllShow)
                        {
                            DB.SetPIVAsDB(string.Format("insert into IVRecordUpdateWait ([WardEmployeeID],[WardInsertDT],[WardLabelNo],[WardAct],[CenterAct]) select '{0}' as WardEmployeeID,GETDATE(),LabelNo,'{2}' as WardAct,'0' as CenterAct  from IVRecord i where LabelNo in ({1}) and not exists (select 'X' from IVRecordUpdateWait iu where i.LabelNo=iu.WardLabelNo and iu.WardAct='{2}')", userID, LabelNoS, comboBox3.SelectedIndex));
                        }
                        switch (comboBox3.SelectedIndex)
                        {
                            case 0:
                                {

                                    DB.SetPIVAsDB(string.Format("update IVRecord set PackAdvance=1,PackID='{1}',PackTime=getdate() where LabelNo in ({0})", LabelNoS, userID));
                                    DB.SetPIVAsDB(string.Format("update [IVRecordUpdateWait] set CenterAct=(select isnull(i.PackAdvance,0) from IVRecord i where i.LabelNo=WardLabelNo) ,CenterEmployeeID=(select i.PackID from IVRecord i where i.LabelNo=WardLabelNo) ,CenterInsertDT=GETDATE() where WardLabelNo in ({0}) and WardAct='{1}'", LabelNoS, comboBox3.SelectedIndex));
                                    TDS();
                                    MessageBox.Show("执行完成！！！");
                                    if (!string.IsNullOrEmpty(WardCodeS))
                                    {
                                        ShowLabelNo();
                                    }
                                    else
                                    {
                                        TableClr();
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    DB.SetPIVAsDB(string.Format("update IVRecord set LabelOverID='{1}' , LabelOverTime=getdate() , LabelOver='{2}' where LabelNo in ({0}) and  LabelOver>=0", LabelNoS, userID, (comboBox4.SelectedIndex == 0 ? "-1" : "-4")));
                                    DB.SetPIVAsDB(string.Format("update [IVRecordUpdateWait] set CenterAct=(select case when i.LabelOver<0 then 1 else CenterAct end  from IVRecord i where i.LabelNo=WardLabelNo) ,CenterEmployeeID=(select i.LabelOverID from IVRecord i where i.LabelNo=WardLabelNo) ,CenterInsertDT=GETDATE() where WardLabelNo in ({0}) and WardAct='{1}'", LabelNoS, comboBox3.SelectedIndex));
                                    TDS();
                                    MessageBox.Show("执行完成！！！");
                                    if (!string.IsNullOrEmpty(WardCodeS))
                                    {
                                        ShowLabelNo();
                                    }
                                    else
                                    {
                                        TableClr();
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    break;
                                }
                            default:
                                {
                                    MessageBox.Show("执行不明！！！");
                                    break;
                                }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("未选择任何数据");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBox1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                WardCodeS = string.Empty;
                if (checkBox1.Checked)
                {
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        dgvr.Cells["Checked"].Value = true;
                        WardCodeS = WardCodeS + "'" + dgvr.Cells["WardCode"].Value + "',";
                    }
                    WardCodeS = WardCodeS.TrimEnd(',');
                    if (!string.IsNullOrEmpty(WardCodeS))
                    {
                        LabelSQL = string.Format("WardCode in ({0}) {1}", WardCodeS, comboBox2.SelectedIndex < 3 ? (" and CenterAct=" + comboBox2.SelectedIndex) : string.Empty);
                        ShowLabelNo();
                    }
                    else
                    {
                        TableClr();
                    }
                }
                else
                {
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        dgvr.Cells["Checked"].Value = false;
                    }
                    TableClr();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0 && hasload)
            {
                Lablechange = true;
                ShowLableDetail();
            }
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == 0)
                {
                    if (dataGridView2.Rows[e.RowIndex].Cells["CenterAct"].Value.ToString() == "0")
                    {
                        dataGridView2.Rows[e.RowIndex].Cells[0].Value = !true.Equals(dataGridView2.Rows[e.RowIndex].Cells[0].Value);
                        bool allcheck = true;
                        bool alluncheck = true;
                        foreach (DataGridViewRow dgvr in dataGridView2.Rows)
                        {
                            if (dgvr.Cells["CenterAct"].Value.ToString() == "0")
                            {
                                if (true.Equals(dgvr.Cells["Check"].Value))
                                {
                                    alluncheck = false;
                                }
                                else
                                {
                                    allcheck = false;
                                }
                            }
                        }
                        checkBox2.CheckState = allcheck ? CheckState.Checked : (alluncheck ? CheckState.Unchecked : CheckState.Indeterminate);
                    }
                }
                if (Lablechange)
                {
                    ShowLableDetail();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void TableClr()
        {
            if (dataGridView2.DataSource != null)
            {
                (dataGridView2.DataSource as DataTable).Rows.Clear();
                label14.Text = string.Empty;
            }
            if (dataGridView3.DataSource != null)
            {
                (dataGridView3.DataSource as DataTable).Rows.Clear();
                (dataGridView4.DataSource as DataTable).Rows.Clear();
            }
            checkBox2.Checked = false;
            label1.Text =
                label2.Text =
                label3.Text =
                label4.Text =
                label5.Text =
                label6.Text = string.Empty;
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LabelSQL = string.Format("WardCode in ({0}) {1}", WardCodeS, comboBox2.SelectedIndex < 3 ? (" and CenterAct=" + comboBox2.SelectedIndex) : string.Empty);
            ShowLabelNo();
        }

        private void checkBox2_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (DataGridViewRow dgvr in dataGridView2.Rows)
            {
                if (dgvr.Cells["CenterAct"].Value.ToString() == "0")
                {
                    dgvr.Cells["Check"].Value = checkBox2.Checked;
                }
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(textBox3.Text.Trim()))
                {
                    textBox3.Text = "0";
                }
                try
                {
                    int i = Int32.Parse(textBox3.Text.Trim());
                    if (i > dataGridView2.RowCount || i < 0)
                    {
                        textBox3.Text = "0";
                        i = 0;
                    }
                    min = i;
                    if (i > max)
                    {
                        max = min;
                        textBox4.Text = max.ToString();
                    }
                }
                catch
                {
                    MessageBox.Show("输入的不是数字");
                    textBox3.Text = "0";
                }
                selt();
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(textBox4.Text.Trim()))
                {
                    textBox4.Text = "0";
                }
                try
                {
                    int i = Int32.Parse(textBox4.Text.Trim());
                    if (i > dataGridView2.RowCount || i < 0)
                    {
                        textBox4.Text = dataGridView2.RowCount.ToString();
                        i = dataGridView2.RowCount;
                    }
                    max = i;
                    if (i < min)
                    {
                        min = max;
                        textBox3.Text = min.ToString();
                    }
                }
                catch
                {
                    MessageBox.Show("输入的不是数字");
                    textBox4.Text = "0";
                }
                selt();
            }
        }
        private void selt()
        {
            try
            {
                foreach (DataGridViewRow dgvr in dataGridView2.Rows)
                {
                    if (dgvr.Cells["CenterAct"].Value.ToString() == "0")
                    {
                        dgvr.Cells["Check"].Value = (dgvr.Index < max && dgvr.Index >= min - 1);
                    }
                }
                if (min == 0 && max == 0)
                {
                    checkBox2.Checked = false;
                }
                else if (min > 0 || max < dataGridView2.RowCount)
                {
                    checkBox2.CheckState = CheckState.Indeterminate;
                }
                else
                {
                    checkBox2.CheckState = CheckState.Checked;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (!string.IsNullOrEmpty(textBox2.Text.Trim()) && textBox2.Text.Trim() != "瓶签/床号/患者/住院号/批次/用法")
                {
                    if (Tds != null && Tds.Tables.Count > 0)
                    {
                        LabelSQL = string.Format((string.IsNullOrEmpty(WardCodeS) ? string.Empty : string.Format("WardCode in ({0}) and ", WardCodeS)) + " (LabelNo like ('%{0}%') or BedNo like ('%{0}%') or PatName like ('%{0}%') or FreqName like ('%{0}%') or CaseID like ('%{0}%') or Batch like ('%{0}%'))", textBox2.Text.Trim());
                        if (Tds.Tables[0].Select(LabelSQL).Length > 0)
                        {
                            ShowLabelNo();
                        }
                        else
                        {
                            LabelSQL = string.Format("WardCode in ({0}) {1}", WardCodeS, comboBox2.SelectedIndex < 3 ? (" and CenterAct=" + comboBox2.SelectedIndex) : string.Empty);
                            MessageBox.Show("未找到数据！！！");
                        }
                    }
                }
                else
                {
                    LabelSQL = string.Format("WardCode in ({0}) {1}", WardCodeS, comboBox2.SelectedIndex < 3 ? (" and CenterAct=" + comboBox2.SelectedIndex) : string.Empty);
                    ShowLabelNo();
                }
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.ForeColor = Color.Black;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text.Trim()) || textBox2.Text.Trim() == "瓶签/床号/患者/住院号/批次/用法")
            {
                textBox2.Text = "瓶签/床号/患者/住院号/批次/用法";
                textBox2.ForeColor = Color.Silver;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Black;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()) || textBox1.Text.Trim() == "病区名/简拼")
            {
                textBox1.Text = "病区名/简拼";
                textBox1.ForeColor = Color.Silver;
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.SelectAll();
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.SelectAll();
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            if (TimeChange)
            {
                TimeChange = false;
                TDS();
                NewRun();
            }
        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dataGridView2.RowHeadersDefaultCellStyle.Font, new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dataGridView1.RowHeadersWidth, e.RowBounds.Height), dataGridView2.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                LabelNoS = string.Empty;
                foreach (DataGridViewRow dgvr in dataGridView2.Rows)
                {
                    if (true.Equals(dgvr.Cells["check"].Value))
                    {
                        LabelNoS = LabelNoS + "'" + dgvr.Cells["Label"].Value + "',";
                    }
                }
                LabelNoS = LabelNoS.TrimEnd(',');
                if (!string.IsNullOrEmpty(LabelNoS))
                {
                    if (new Caption().ShowDialog() == DialogResult.Yes)
                    {
                        if (AllShow)
                        {
                            DB.SetPIVAsDB(string.Format("insert into IVRecordUpdateWait ([WardEmployeeID],[WardInsertDT],[WardLabelNo],[WardAct],[CenterAct]) select '{0}' as WardEmployeeID,GETDATE(),LabelNo,'{2}' as WardAct,'0' as CenterAct from IVRecord i where LabelNo in ({1}) and not exists (select 'X' from IVRecordUpdateWait iu where i.LabelNo=iu.WardLabelNo and iu.WardAct='{2}')", userID, LabelNoS, comboBox3.SelectedIndex));
                        }
                        DB.SetPIVAsDB(string.Format("update [IVRecordUpdateWait] set CenterAct=2 ,CenterEmployeeID='{0}' ,CenterInsertDT=GETDATE() where WardLabelNo in ({1}) and WardAct='{2}'", userID, LabelNoS, comboBox3.SelectedIndex));
                        TDS();
                        MessageBox.Show("完成！！！");
                        if (!string.IsNullOrEmpty(WardCodeS))
                        {
                            ShowLabelNo();
                        }
                        else
                        {
                            TableClr();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("未选择任何数据");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            TDS();
            NewRun();  
        }

        private void comboBox4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            AllShow = Convert.ToBoolean(comboBox4.SelectedIndex);
            TDS();
            NewRun(); 
        }
    }
}
