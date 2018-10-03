using PIVAsCommon.Helper;
using PivasLimitDES;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WorkStatic
{
    internal sealed partial class Form1 : Form
    {
        private string UserID = string.Empty;
        private string UsageCodes = string.Empty;
        private string DrugType = string.Empty;
        private DB_Help db = new DB_Help();
        private StringBuilder sb = new StringBuilder();
        private List<string> ls = new List<string>();
        private DataTable Ldt = new DataTable();//员工表
        private DataTable WardorDEmp = new DataTable();
        private DataTable Usages = new DataTable();
        private DataTable DrugTypes = new DataTable();
        private DataTable CountItem = new DataTable();
        private DataTable MainDT = new DataTable();//显示在界面上的主表信息
        private DateTime beginTime = DateTime.Now;
        private DateTime endTime = DateTime.Now;
        private bool HadLoad = false;
        internal bool Hadrun = false;


        internal Form1()
        {
            InitializeComponent();
        }

        internal Form1(string ID)
        {
            this.UserID = ID;
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now.Date.AddDays(1 - DateTime.Now.Day);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (GetPivasLimit.Instance.Limit(UserID, "PivasWorkFlow"))
                {
                    panel1.Tag = string.Empty;
                    Usages = db.GetPIVAsDB("select distinct Convert(bit,1) bt,UsageCode from Prescription").Tables[0];
                    DrugTypes.Columns.Add("bt", typeof(bool));
                    DrugTypes.Columns.Add("DrugTypeC", typeof(string));
                    DrugTypes.Columns.Add("DrugTypeN", typeof(int));
                    DrugTypes.Rows.Add(true, "普通药", 1);
                    DrugTypes.Rows.Add(true, "抗生素", 2);
                    DrugTypes.Rows.Add(true, "化疗药", 3);
                    DrugTypes.Rows.Add(true, "营养药", 4);
                    DrugTypes.Rows.Add(true, "中药", 5);
                    try
                    {
                        if (File.Exists(".\\ConfigFile\\CountItem.Dat"))
                        {
                            CountItem.ReadXml(".\\ConfigFile\\CountItem.Dat");
                            goto contines;
                        }
                        else
                        {
                            goto ca;
                        }
                    }
                    catch
                    {
                        //MessageBox.Show(ex.Message);
                        goto ca;
                    }
                ca:
                    {
                        CountItem.TableName = "CountItems";
                        CountItem.Columns.Add("bt", typeof(bool));
                        CountItem.Columns.Add("CountItem", typeof(string));
                        CountItem.Columns.Add("SQLBYDemp", typeof(string));
                        CountItem.Columns.Add("SQLBYWard", typeof(string));
                        CountItem.PrimaryKey = new DataColumn[] { CountItem.Columns["CountItem"] };

                        string SQLBYDemp = "select CheckDCode DEmployeeID,COUNT(distinct p.PrescriptionID) as [审方(医嘱数量)] from Prescription p inner join CPRecord cp on cp.PrescriptionID = p.PrescriptionID and DATEDIFF(DAY,'{0}',CheckDT) Between 0 and {1} {2} {3} group by CheckDCode ";
                        string SQLBYWard = "select WardCode,              COUNT(distinct p.PrescriptionID) as [审方(医嘱数量)] from Prescription p inner join CPRecord cp on cp.PrescriptionID = p.PrescriptionID and DATEDIFF(DAY,'{0}',CheckDT) Between 0 and {1} {2} {3} group by WardCode ";
                        CountItem.Rows.Add(true, "审方(医嘱数量)", SQLBYDemp, SQLBYWard);

                        SQLBYDemp = "select CheckDCode DEmployeeID,SUM(isnull(difficultysf,0)) as [审方(难度系数)] from Prescription p inner join CPRecord cp on cp.PrescriptionID = p.PrescriptionID inner join PrescriptionDetail pd on pd.PrescriptionID = p.PrescriptionID inner join DDrug d on d.DrugCode = pd.DrugCode and DATEDIFF(DAY,'{0}',CheckDT) Between 0 and {1} {2} {3} group by CheckDCode ";
                        SQLBYWard = "select WardCode,              SUM(isnull(difficultysf,0)) as [审方(难度系数)] from Prescription p inner join CPRecord cp on cp.PrescriptionID = p.PrescriptionID inner join PrescriptionDetail pd on pd.PrescriptionID = p.PrescriptionID inner join DDrug d on d.DrugCode = pd.DrugCode and DATEDIFF(DAY,'{0}',CheckDT) Between 0 and {1} {2} {3} group by WardCode ";
                        CountItem.Rows.Add(false, "审方(难度系数)", SQLBYDemp, SQLBYWard);

                        SQLBYDemp = "select DEmployeeID,COUNT(1) as[改批次(瓶签数)] from OrderChangeLog oc inner join IVRecord iv on iv.LabelNo =oc.LabelNo inner join Prescription p on p.PrescriptionID = iv.PrescriptionID and DATEDIFF(DAY,'{0}',ChangeDT) Between 0 and {1} {2} {3} group by DEmployeeID ";
                        SQLBYWard = "select iv.WardCode,COUNT(1) as[改批次(瓶签数)] from OrderChangeLog oc inner join IVRecord iv on iv.LabelNo =oc.LabelNo inner join Prescription p on p.PrescriptionID = iv.PrescriptionID and DATEDIFF(DAY,'{0}',ChangeDT) Between 0 and {1} {2} {3} group by iv.WardCode ";
                        CountItem.Rows.Add(true, "改批次(瓶签数)", SQLBYDemp, SQLBYWard);

                        SQLBYDemp = "select DEmployeeID,COUNT(distinct iv.PatCode) as[改批次(患者数)] from OrderChangeLog oc inner join IVRecord iv on iv.LabelNo =oc.LabelNo inner join Prescription p on p.PrescriptionID = iv.PrescriptionID and DATEDIFF(DAY,'{0}',ChangeDT) Between 0 and {1} {2} {3} group by DEmployeeID ";
                        SQLBYWard = "select iv.WardCode,COUNT(distinct iv.PatCode) as[改批次(患者数)] from OrderChangeLog oc inner join IVRecord iv on iv.LabelNo =oc.LabelNo inner join Prescription p on p.PrescriptionID = iv.PrescriptionID and DATEDIFF(DAY,'{0}',ChangeDT) Between 0 and {1} {2} {3} group by iv.WardCode ";
                        CountItem.Rows.Add(false, "改批次(患者数)", SQLBYDemp, SQLBYWard);

                        SQLBYDemp = "select PCode DEmployeeID,COUNT(1)[摆水] from IVRecord_YP_ZJG YP inner join IVRecord iv on iv.LabelNo = yp.IVRecordID inner join Prescription p on p.PrescriptionID = iv.PrescriptionID and ScanCount=0 and DATEDIFF(DAY,'{0}',YPDT) Between 0 and {1} {2} {3} group by PCode ";
                        SQLBYWard = "select iv.WardCode,      COUNT(1)[摆水] from IVRecord_YP_ZJG YP inner join IVRecord iv on iv.LabelNo = yp.IVRecordID inner join Prescription p on p.PrescriptionID = iv.PrescriptionID and ScanCount=0 and DATEDIFF(DAY,'{0}',YPDT) Between 0 and {1} {2} {3} group by iv.WardCode  ";
                        CountItem.Rows.Add(true, "摆水", SQLBYDemp, SQLBYWard);

                        SQLBYDemp = "select PCode DEmployeeID,COUNT(1)[摆药] from IVRecord_YS_ZJG YP inner join IVRecord iv on iv.LabelNo = yp.IVRecordID inner join Prescription p on p.PrescriptionID = iv.PrescriptionID and ScanCount=0 and DATEDIFF(DAY,'{0}',YSDT) Between 0 and {1} {2} {3} group by PCode ";
                        SQLBYWard = "select iv.WardCode,      COUNT(1)[摆药] from IVRecord_YS_ZJG YP inner join IVRecord iv on iv.LabelNo = yp.IVRecordID inner join Prescription p on p.PrescriptionID = iv.PrescriptionID and ScanCount=0 and DATEDIFF(DAY,'{0}',YSDT) Between 0 and {1} {2} {3} group by iv.WardCode ";
                        CountItem.Rows.Add(true, "摆药", SQLBYDemp, SQLBYWard);

                        SQLBYDemp = "select PCode DEmployeeID,COUNT(1)[排药] from IVRecord_PY YP inner join IVRecord iv on iv.LabelNo = yp.IVRecordID inner join Prescription p on p.PrescriptionID = iv.PrescriptionID and ScanCount=0 and DATEDIFF(DAY,'{0}',PYDT) Between 0 and {1} {2} {3} group by PCode ";
                        SQLBYWard = "select iv.WardCode,      COUNT(1)[排药] from IVRecord_PY YP inner join IVRecord iv on iv.LabelNo = yp.IVRecordID inner join Prescription p on p.PrescriptionID = iv.PrescriptionID and ScanCount=0 and DATEDIFF(DAY,'{0}',PYDT) Between 0 and {1} {2} {3} group by iv.WardCode ";
                        CountItem.Rows.Add(false, "排药", SQLBYDemp, SQLBYWard);

                        SQLBYDemp = "select PCode DEmployeeID,COUNT(1)[进仓] from IVRecord_JC YP inner join IVRecord iv on iv.LabelNo = yp.IVRecordID inner join Prescription p on p.PrescriptionID = iv.PrescriptionID and ScanCount=0 and DATEDIFF(DAY,'{0}',JCDT) Between 0 and {1} {2} {3} group by PCode ";
                        SQLBYWard = "select iv.WardCode,      COUNT(1)[进仓] from IVRecord_JC YP inner join IVRecord iv on iv.LabelNo = yp.IVRecordID inner join Prescription p on p.PrescriptionID = iv.PrescriptionID and ScanCount=0 and DATEDIFF(DAY,'{0}',JCDT) Between 0 and {1} {2} {3} group by iv.WardCode ";
                        CountItem.Rows.Add(false, "进仓", SQLBYDemp, SQLBYWard);

                        SQLBYDemp = "select PCode DEmployeeID,COUNT(1)[配置(非空)] from IVRecord_PZ YP inner join IVRecord iv on iv.LabelNo = yp.IVRecordID inner join Prescription p on p.PrescriptionID = iv.PrescriptionID and ScanCount=0 and Batch like '%#' and DATEDIFF(DAY,'{0}',PZDT) Between 0 and {1} {2} {3} group by PCode ";
                        SQLBYWard = "select iv.WardCode,      COUNT(1)[配置(非空)] from IVRecord_PZ YP inner join IVRecord iv on iv.LabelNo = yp.IVRecordID inner join Prescription p on p.PrescriptionID = iv.PrescriptionID and ScanCount=0 and Batch like '%#' and DATEDIFF(DAY,'{0}',PZDT) Between 0 and {1} {2} {3} group by iv.WardCode ";
                        CountItem.Rows.Add(true, "配置(非空)", SQLBYDemp, SQLBYWard);

                        SQLBYDemp = "select PCode DEmployeeID,COUNT(1)[配置(空包)] from IVRecord_PZ YP inner join IVRecord iv on iv.LabelNo = yp.IVRecordID inner join Prescription p on p.PrescriptionID = iv.PrescriptionID and ScanCount=0 and Batch like '%K' and DATEDIFF(DAY,'{0}',PZDT) Between 0 and {1} {2} {3} group by PCode ";
                        SQLBYWard = "select iv.WardCode,      COUNT(1)[配置(空包)] from IVRecord_PZ YP inner join IVRecord iv on iv.LabelNo = yp.IVRecordID inner join Prescription p on p.PrescriptionID = iv.PrescriptionID and ScanCount=0 and Batch like '%K' and DATEDIFF(DAY,'{0}',PZDT) Between 0 and {1} {2} {3} group by iv.WardCode ";
                        CountItem.Rows.Add(true, "配置(空包)", SQLBYDemp, SQLBYWard);

                        SQLBYDemp = "select PCode DEmployeeID,COUNT(1)[出仓] from IVRecord_CC YP inner join IVRecord iv on iv.LabelNo = yp.IVRecordID inner join Prescription p on p.PrescriptionID = iv.PrescriptionID and ScanCount=0 and DATEDIFF(DAY,'{0}',CCDT) Between 0 and {1} {2} {3} group by PCode ";
                        SQLBYWard = "select iv.WardCode,      COUNT(1)[出仓] from IVRecord_CC YP inner join IVRecord iv on iv.LabelNo = yp.IVRecordID inner join Prescription p on p.PrescriptionID = iv.PrescriptionID and ScanCount=0 and DATEDIFF(DAY,'{0}',CCDT) Between 0 and {1} {2} {3} group by iv.WardCode ";
                        CountItem.Rows.Add(false, "出仓", SQLBYDemp, SQLBYWard);

                        SQLBYDemp = "select PCode DEmployeeID,COUNT(1)[打包] from IVRecord_DB YP inner join IVRecord iv on iv.LabelNo = yp.IVRecordID inner join Prescription p on p.PrescriptionID = iv.PrescriptionID and ScanCount=0 and DATEDIFF(DAY,'{0}',DBDT) Between 0 and {1} {2} {3} group by PCode ";
                        SQLBYWard = "select iv.WardCode,      COUNT(1)[打包] from IVRecord_DB YP inner join IVRecord iv on iv.LabelNo = yp.IVRecordID inner join Prescription p on p.PrescriptionID = iv.PrescriptionID and ScanCount=0 and DATEDIFF(DAY,'{0}',DBDT) Between 0 and {1} {2} {3} group by iv.WardCode ";
                        CountItem.Rows.Add(true, "打包", SQLBYDemp, SQLBYWard);

                        SQLBYDemp = "select LabelOverID DEmployeeID,COUNT(1) as [配置取消] from IVRecord iv inner join Prescription p on p.PrescriptionID = iv.PrescriptionID and LabelOver < 0 AND LabelOverID!='' AND DATEDIFF(DAY,'{0}',LabelOverTime) Between 0 and {1} {2} {3} group by LabelOverID ";
                        SQLBYWard = "select iv.WardCode,            COUNT(1) as [配置取消] from IVRecord iv inner join Prescription p on p.PrescriptionID = iv.PrescriptionID and LabelOver < 0 AND LabelOverID!='' AND DATEDIFF(DAY,'{0}',LabelOverTime) Between 0 and {1} {2} {3} group by iv.WardCode ";
                        CountItem.Rows.Add(true, "配置取消", SQLBYDemp, SQLBYWard);
                    }
                contines:
                    MainDT.Columns.Add("员工", typeof(string));
                    MainDT.Columns.Add("DEmployeeID", typeof(string));
                    MainDT.Columns.Add("病区", typeof(string));
                    MainDT.Columns.Add("WardCode", typeof(string));
                    dataGridView1.DataSource = MainDT;
                    dataGridView1.Columns["DEmployeeID"].Visible = false;
                    dataGridView1.Columns["WardCode"].Visible = false;
                    comboBox1.SelectedIndex = 0;
                    radioButton1.Checked = true;

                    sb = new StringBuilder();
                    ls = new List<string>();
                    foreach (DataRow dr in CountItem.Rows)
                    {
                        MainDT.Columns.Add(dr[1].ToString(), typeof(string));
                        dataGridView1.Columns[dr[1].ToString()].Visible = true.Equals(dr[0]);
                        if (true.Equals(dr[0]))
                        {
                            sb.AppendLine(radioButton2.Checked ? dr[3].ToString() : dr[2].ToString());
                            ls.Add(dr[1].ToString());
                        }
                    }
                    HadLoad = true;
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            this.button1.BackColor = System.Drawing.Color.FromArgb(65, 202, 179);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            this.button1.BackColor = System.Drawing.Color.Transparent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataToExcel(this.dataGridView1);
        }

        private void DataToExcel(DataGridView dgv)
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
                                foreach (DataGridViewColumn dgvc in dgv.Columns)
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
                                foreach (DataGridViewRow dgvr in dgv.Rows)
                                {
                                    string columnValue = string.Empty;
                                    foreach (DataGridViewColumn dgvc in dgv.Columns)
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
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }



        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("该功能会产生额外的费用，确定使用么？");
            MessageBox.Show("如果使用，请联系厂家商务");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Enabled =
            checkBox2.Enabled =
            comboBox1.Visible =
            radioButton1.Checked;
            checkBox1.Checked =
            radioButton1.Checked;

            dataGridView1.Columns["员工"].Visible = !radioButton2.Checked;
            dataGridView1.Columns["病区"].Visible = radioButton2.Checked;

            MainDT.Rows.Clear();
        }

        private void GetDWardorDEmp()
        {
            try
            {
                string sql = string.Empty;
                dataGridView2.Columns[1].HeaderText = "全部员工";
                if (checkBox1.Checked && checkBox2.Checked)
                {
                    sql = string.Format("select CONVERT(bit,1) as bt,{0} as '员工',DEmployeeID from [DEmployee] where IsValid=1 and [Type] in (1,2)", comboBox1.SelectedIndex == 0 ? "DEmployeeName" : (comboBox1.SelectedIndex == 1 ? "DEmployeeCode" : "DEmployeeCode+'-'+DEmployeeName"));
                }
                else if (checkBox1.Checked)
                {
                    sql = string.Format("select CONVERT(bit,1) as bt,{0} as '员工',DEmployeeID from [DEmployee] where IsValid=1 and [Type] = 1 ", comboBox1.SelectedIndex == 0 ? "DEmployeeName" : (comboBox1.SelectedIndex == 1 ? "DEmployeeCode" : "DEmployeeCode+'-'+DEmployeeName"));
                }
                else if (checkBox2.Checked)
                {
                    sql = string.Format("select CONVERT(bit,1) as bt,{0} as '员工',DEmployeeID from [DEmployee] where IsValid=1 and [Type] = 2 ", comboBox1.SelectedIndex == 0 ? "DEmployeeName" : (comboBox1.SelectedIndex == 1 ? "DEmployeeCode" : "DEmployeeCode+'-'+DEmployeeName"));
                }
                else
                {
                    dataGridView2.Columns[1].HeaderText = "全部病区";
                    sql = "SELECT CONVERT(bit,1) as bt,WardSimName as '病区',WardCode FROM DWard where IsOpen=1";
                }
                WardorDEmp = db.GetPIVAsDB(sql).Tables[0];
                WardorDEmp.PrimaryKey = new DataColumn[] { WardorDEmp.Columns[2] };
                foreach (DataGridViewColumn dgvc in dataGridView2.Columns)
                {
                    dgvc.DataPropertyName = WardorDEmp.Columns[dgvc.Index].ColumnName;
                }
                Ldt = WardorDEmp.Copy();
                dataGridView2.DataSource = Ldt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Enabled)
            {
                if (!checkBox1.Checked && !checkBox2.Checked)
                {
                    checkBox1.Checked = true;
                }
            }
            GetDWardorDEmp();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Enabled)
            {
                if (!checkBox1.Checked && !checkBox2.Checked)
                {
                    checkBox2.Checked = true;
                }
            }
            GetDWardorDEmp();
        }

        private void checkBox3_Click(object sender, EventArgs e)
        {
            foreach (DataRow dr in Ldt.Rows)
            {
                dr[0] = checkBox3.Checked;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                Ldt.Rows[e.RowIndex][0] = !true.Equals(Ldt.Rows[e.RowIndex][0]);
                bool ched = false;
                bool state = true;
                foreach (DataRow dr in Ldt.Rows)
                {
                    if (true.Equals(dr[0]))
                    {
                        ched = true;
                    }
                    else
                    {
                        state = false;
                    }
                }
                checkBox3.CheckState = ched ? (state ? CheckState.Checked : CheckState.Indeterminate) : CheckState.Unchecked;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(panel1.Tag.ToString()))
            {
                panel1.Height = 140;
                panel1.Visible = true;
                panel1.Tag = button4.Tag;
                bool ched = false;
                bool state = true;
                foreach (DataRow dr in DrugTypes.Rows)
                {
                    if (true.Equals(dr[0]))
                    {
                        ched = true;
                    }
                    else
                    {
                        state = false;
                    }
                }
                checkBox4.CheckState = ched ? (state ? CheckState.Checked : CheckState.Indeterminate) : CheckState.Unchecked;
                panel1.Location = new System.Drawing.Point(button4.Location.X, panel3.Height);
                dataGridView3.DataSource = DrugTypes;
                dataGridView3.Columns[0].Width = 20;
                dataGridView3.Columns[1].Width = 116;
                dataGridView3.Columns[2].Visible = false;
            }
            else
            {
                if (panel1.Tag == button4.Tag)
                {
                    switch (checkBox4.CheckState)
                    {
                        case CheckState.Checked:
                            {
                                DrugType = string.Empty;
                                break;
                            }
                        case CheckState.Indeterminate:
                            {
                                DrugType = string.Empty;
                                foreach (DataRow dr in DrugTypes.Rows)
                                {
                                    if (true.Equals(dr[0]))
                                    {
                                        DrugType = dr[2] + "," + DrugType;
                                    }
                                }
                                if (!string.IsNullOrEmpty(DrugType))
                                {
                                    DrugType = " and p.DrugType in (" + DrugType.TrimEnd(',') + ") ";
                                }
                                break;
                            }
                        case CheckState.Unchecked:
                            {
                                MessageBox.Show("请选择至少一种类型！");
                                return;
                            }
                    }
                    panel1.Visible = false;
                    panel1.Tag = string.Empty;
                    panel1.Height = 190;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(panel1.Tag.ToString()))
            {
                panel1.Height = 140;
                panel1.Visible = true;
                panel1.Tag = button5.Tag;
                bool ched = false;
                bool state = true;
                foreach (DataRow dr in Usages.Rows)
                {
                    if (true.Equals(dr[0]))
                    {
                        ched = true;
                    }
                    else
                    {
                        state = false;
                    }
                }
                checkBox4.CheckState = ched ? (state ? CheckState.Checked : CheckState.Indeterminate) : CheckState.Unchecked;
                panel1.Location = new System.Drawing.Point(button5.Location.X, panel3.Height);
                dataGridView3.DataSource = Usages;
                dataGridView3.Columns[0].Width = 20;
                dataGridView3.Columns[1].Width = 116;
            }
            else
            {
                if (panel1.Tag == button5.Tag)
                {
                    switch (checkBox4.CheckState)
                    {
                        case CheckState.Checked:
                            {
                                UsageCodes = string.Empty;
                                break;
                            }
                        case CheckState.Indeterminate:
                            {
                                UsageCodes = string.Empty;
                                foreach (DataRow dr in Usages.Rows)
                                {
                                    if (true.Equals(dr[0]))
                                    {
                                        UsageCodes = "'" + dr[1] + "'," + UsageCodes;
                                    }
                                }
                                if (!string.IsNullOrEmpty(UsageCodes))
                                {
                                    UsageCodes = " and p.UsageCode in (" + UsageCodes.TrimEnd(',') + ") ";
                                }
                                break;
                            }
                        case CheckState.Unchecked:
                            {
                                MessageBox.Show("请选择至少一种用药途径！");
                                return;
                            }
                    }
                    panel1.Visible = false;
                    panel1.Tag = string.Empty;
                    panel1.Height = 190;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(panel1.Tag.ToString()))
            {
                panel1.Visible = true;
                panel1.Tag = button3.Tag;
                bool ched = false;
                bool state = true;
                foreach (DataRow dr in CountItem.Rows)
                {
                    if (true.Equals(dr[0]))
                    {
                        ched = true;
                    }
                    else
                    {
                        state = false;
                    }
                }
                checkBox4.CheckState = ched ? (state ? CheckState.Checked : CheckState.Indeterminate) : CheckState.Unchecked;
                panel1.Location = new System.Drawing.Point(button3.Location.X, panel3.Height);
                dataGridView3.DataSource = CountItem;
                dataGridView3.Columns[0].Width = 20;
                dataGridView3.Columns[1].Width = 116;
                dataGridView3.Columns[2].Visible = false;
                dataGridView3.Columns[3].Visible = false;
            }
            else
            {
                if (panel1.Tag == button3.Tag)
                {
                    switch (checkBox4.CheckState)
                    {
                        case CheckState.Unchecked:
                            {
                                MessageBox.Show("请选择至少一种工种！");
                                return;
                            }
                        default:
                            {
                                sb = new StringBuilder();
                                ls = new List<string>();
                                foreach (DataRow dr in CountItem.Rows)
                                {
                                    dataGridView1.Columns[dr[1].ToString()].Visible = true.Equals(dr[0]);
                                    if (true.Equals(dr[0]))
                                    {
                                        sb.AppendLine(radioButton2.Checked ? dr[3].ToString() : dr[2].ToString());
                                        ls.Add(dr[1].ToString());
                                    }
                                }
                                CountItem.WriteXml(".\\ConfigFile\\CountItem.Dat", XmlWriteMode.WriteSchema, true);
                                break;
                            }
                    }
                    panel1.Visible = false;
                    panel1.Tag = string.Empty;
                }
            }
        }

        private void checkBox4_Click(object sender, EventArgs e)
        {
            DataTable dt = dataGridView3.DataSource as DataTable;
            foreach (DataRow dr in dt.Rows)
            {
                dr[0] = checkBox4.Checked;
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                DataTable dt = dataGridView3.DataSource as DataTable;
                dt.Rows[e.RowIndex][0] = !true.Equals(dt.Rows[e.RowIndex][0]);
                bool ched = false;
                bool state = true;
                foreach (DataRow dr in dt.Rows)
                {
                    if (true.Equals(dr[0]))
                    {
                        ched = true;
                    }
                    else
                    {
                        state = false;
                    }
                }
                checkBox4.CheckState = ched ? (state ? CheckState.Checked : CheckState.Indeterminate) : CheckState.Unchecked;
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

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!File.Exists(".\\ConfigFile\\Notice.txt"))
                {
                    File.AppendAllText(".\\ConfigFile\\Notice.txt", string.Empty);
                }
                System.Diagnostics.Process.Start(".\\ConfigFile\\Notice.txt");
                if (File.Exists(".\\htmlword.CHM"))
                {
                    System.Diagnostics.Process.Start(".\\htmlword.CHM");
                }
            }
            catch
            {
                MessageBox.Show("没有安装office软件或找不到文件");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (panel1.Visible)
                {
                    switch (panel1.Tag.ToString())
                    {
                        case "CountItem":
                            {
                                button3_Click(sender, e);
                                break;
                            }
                        case "DrugType":
                            {
                                button4_Click(sender, e);
                                break;
                            }
                        case "Usage":
                            {
                                button5_Click(sender, e);
                                break;
                            }
                        default: { break; }
                    }
                }
                if (!panel1.Visible)
                {
                    Hadrun = true;
                    Thread th = new Thread(() => { using (WaitForm wf = new WaitForm(this)) { wf.ShowDialog(); } });
                    th.IsBackground = true;
                    th.Start();
                    MainDT.Rows.Clear();
                    string tempStr = string.Format(sb.ToString(), beginTime.ToString("yyyy-MM-dd"),
                        (endTime - beginTime).Days, DrugType, UsageCodes);

                    String temp = string.Format(sb.ToString(), beginTime.ToString("yyyy-MM-dd"),
                        (endTime - beginTime).Days, DrugType, UsageCodes);
                    using (DataSet ds = db.GetPIVAsDB(string.Format(sb.ToString(), beginTime.ToString("yyyy-MM-dd"),
                        (endTime - beginTime).Days, DrugType, UsageCodes)))
                    {
                        for (int i = 0; i < ds.Tables.Count; i++)
                        {
                            ds.Tables[i].TableName = ls[i];
                            ds.Tables[i].PrimaryKey = new DataColumn[] { ds.Tables[i].Columns[0] };
                        }
                        if (radioButton2.Checked)
                        {
                            foreach (DataRow dr in Ldt.Rows)
                            {
                                if (true.Equals(dr[0]))
                                {
                                    DataRow drs = MainDT.NewRow();
                                    drs["病区"] = dr["病区"];
                                    drs["WardCode"] = dr["WardCode"];
                                    bool has = false;
                                    foreach (DataTable dt in ds.Tables)
                                    {
                                        if (dt.Rows.Contains(dr["WardCode"]))
                                        {
                                            drs[dt.TableName] = dt.Rows.Find(dr["WardCode"])[1].ToString();
                                            has = true;
                                        }
                                        else
                                        {
                                            drs[dt.TableName] = 0;
                                        }
                                    }
                                    if (has)
                                    {
                                        MainDT.Rows.Add(drs);
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (DataRow dr in Ldt.Rows)
                            {
                                if (true.Equals(dr[0]))
                                {
                                    DataRow drs = MainDT.NewRow();
                                    drs["员工"] = dr["员工"];
                                    drs["DEmployeeID"] = dr["DEmployeeID"];
                                    bool has = false;
                                    foreach (DataTable dt in ds.Tables)
                                    {
                                        if (dt.Rows.Contains(dr["DEmployeeID"]))
                                        {
                                            drs[dt.TableName] = dt.Rows.Find(dr["DEmployeeID"])[1].ToString();
                                            has = true;
                                        }
                                        else
                                        {
                                            drs[dt.TableName] = 0;
                                        }
                                    }
                                    if (has)
                                    {
                                        MainDT.Rows.Add(drs);
                                    }
                                }
                            }
                        }
                        DataRow ZJ = MainDT.NewRow();
                        foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                        {
                            if (dgvc.Visible)
                            {
                                if (dgvc.Index < 4)
                                {
                                    ZJ[dgvc.Index] = "<总计>";
                                }
                                else
                                {
                                    int i = 0;
                                    foreach (DataRow dr in MainDT.Rows)
                                    {
                                        i += Convert.ToInt32(dr[dgvc.Index]);
                                    }
                                    ZJ[dgvc.Index] = i;
                                }
                            }
                        }
                        MainDT.Rows.Add(ZJ);

                    }
                    Hadrun = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            sb = new StringBuilder();
            foreach (DataRow dr in CountItem.Rows)
            {
                if (true.Equals(dr[0]))
                {
                    sb.AppendLine(radioButton2.Checked ? dr[3].ToString() : dr[2].ToString());
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HadLoad)
            {
                GetDWardorDEmp();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Ldt.Rows.Clear();
                foreach (DataRow dr in WardorDEmp.Select(string.Format("{0} like ('%{1}%')", WardorDEmp.Columns[1].ColumnName, textBox1.Text.Trim())))
                {
                    Ldt.Rows.Add(dr.ItemArray);
                }
            }
        }
    }
}
