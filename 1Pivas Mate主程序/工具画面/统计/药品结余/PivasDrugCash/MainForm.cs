using PIVAsCommon.Helper;
using PivasLimitDES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace PivasDrugCash
{
    internal sealed partial  class MainForm : Form
    {
        private const int CS_DropSHADOW = 0x20000;
        private const int GCL_STYLE = (-26);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetClassLong(IntPtr hwnd, int nIndex);

        private string userID = string.Empty;
        private string WardCode = string.Empty;
        private string Batch = string.Empty;
        private string DrugType = string.Empty;
        private string DateByRd = string.Empty;

        private List<TreeNode> Batchs = new List<TreeNode>();
        private List<TreeNode> DWards = new List<TreeNode>();
        private DataTable DrugDB = new DataTable();
        private DateTime begindate = DateTime.Now;
        private DateTime enddate = DateTime.Now;
        private bool DateisChange = false;

        internal MainForm()
        {
            InitializeComponent();
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
        }
        internal MainForm(string userID)
        {
            this.userID = userID;
            InitializeComponent();
            SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);
        }

        private void ControlLoad()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" SELECT distinct Species FROM [dbo].[DDrug] where Species is not null and Species !=''");
            sb.AppendLine(" SELECT DrugCode,DrugName,Spec,SpellCode,NoName FROM DDrug");
            if (!GetPivasLimit.Instance.Limit(userID, "PivasDrugCash"))
            {
                MessageBox.Show("您没有权限，请联系管理员！");
                this.Dispose(true);
            }
            //MessageBox.Show(sb.ToString());
            using (DataSet ds = new DB_Help().GetPIVAsDB(sb.ToString()))
            {
                if (ds != null && ds.Tables.Count == 2)
                {
                    DrugDB = ds.Tables[1];
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        comboBox1.Items.Add(dr[0].ToString());
                    }
                }
                else
                {
                    this.Dispose(true);
                }
            }
            radioButton3.Checked = true;
            comboBox2.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            dateTimePicker1.Value = begindate;
            dateTimePicker2.Value = enddate;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                ControlLoad();
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
                if (comboBox2.SelectedIndex == 0 && radioButton3.Checked && (dateTimePicker2.Value - dateTimePicker1.Value).Days > 31)
                {
                    MessageBox.Show("按天统计，时间跨度不可超过31天！！！");
                    return;
                }
                string GetMoth = string.Empty;
                switch (comboBox2.SelectedIndex)
                {
                    case 0:
                        {
                            GetMoth = DateByRd;
                            break;
                        }
                    case 1:
                        {
                            GetMoth = "Ward";
                            break;
                        }
                    case 2:
                        {
                            GetMoth = "Batch";
                            break;
                        }
                }
                using (DataTable DatDB = new DataTable())
                {
                    //MessageBox.Show(string.Format("EXECUTE [dbo].[GetDrugCash] '{0}','{1}','{2}','{3}','{4}','{5}','{6}'", GetMoth, begindate, enddate, WardCode, Batch, DrugType, DateByRd));
                    using (DataSet ds = new DB_Help().GetPIVAsDB(string.Format("EXECUTE [dbo].[GetDrugCash] '{0}','{1}','{2}','{3}','{4}','{5}','{6}'", GetMoth, begindate, enddate, WardCode, Batch, DrugType, DateByRd)))
                    {
                        if (ds != null && ds.Tables.Count > 1)
                        {
                            DatDB.Columns.Add("药品名", typeof(string));
                            DatDB.Columns.Add("规格", typeof(string));
                            DatDB.Columns.Add("理论总用量", typeof(string));
                            DatDB.Columns.Add("实际总用量", typeof(string));
                            DatDB.Columns.Add("总结余", typeof(string));
                            if (comboBox2.SelectedIndex == 1)
                            {
                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    DataColumn dc = new DataColumn();
                                    dc.Caption = dr["WardName"].ToString().Trim();
                                    dc.ColumnName = dr["WardCode"].ToString().Trim();
                                    dc.DataType = typeof(string);
                                    DatDB.Columns.Add(dc);
                                }
                            }
                            else
                            {
                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    DatDB.Columns.Add(dr[0].ToString().Trim(), typeof(string));
                                }
                            }
                            foreach (DataRow dr in DrugDB.Select(string.Format("DrugName like ('%{0}%') or SpellCode like ('%{0}%')", textBox1.Text.Trim() == "药品名称/药品简拼" ? string.Empty : textBox1.Text.Trim()), "NoName desc"))
                            {
                                DataRow d = DatDB.NewRow();
                                float Qs = 0;
                                float LLal = 0;
                                float SJal = 0;
                                foreach (DataRow drs in ds.Tables[1].Select(string.Format("DrugCode='{0}'", dr["DrugCode"].ToString().Trim())))
                                {
                                    if (DatDB.Columns.Contains(drs[1].ToString().Trim()))
                                    {
                                        d[drs[1].ToString().Trim()] = drs[2];
                                        float QS = 0;
                                        float LLall = 0;
                                        float SJall = 0;
                                        float.TryParse(drs[2].ToString(), out QS);
                                        float.TryParse(drs[3].ToString(), out LLall);
                                        float.TryParse(drs[4].ToString(), out SJall);
                                        Qs = Qs + QS;
                                        LLal = LLal + LLall;
                                        SJal = SJal + SJall;
                                    }
                                }
                                if (Qs > 0 || LLal > 0 || SJal > 0)
                                {
                                    d["药品名"] = dr["DrugName"].ToString().Trim();
                                    d["规格"] = dr["Spec"].ToString().Trim();
                                    d["理论总用量"] = LLal;
                                    d["实际总用量"] = SJal;
                                    d["总结余"] = Qs;
                                    DatDB.Rows.Add(d);
                                }
                            }
                        }
                    }
                    if (DatDB.Rows.Count == 0)
                    {
                        MessageBox.Show("当前条件下没有药品结余！！！");
                    }
                    else
                    {
                        if (comboBox2.SelectedIndex == 1)
                        {
                            foreach(DataColumn dc in DatDB.Columns)
                            {
                                dc.ColumnName = dc.Caption;
                            }
                        }
                        else
                        {
                            foreach (DataColumn dc in DatDB.Columns)
                            {
                                dc.ColumnName = dc.ColumnName.Replace("#", "批").Replace("-", "_");
                            }
                        }
                        dataGridView1.Columns.Clear();
                        dataGridView1.DataSource = DatDB;
                        dataGridView1.Columns[0].Width = 200;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateChange();
            if (Batchs.Count <= 0)
            {
                MessageBox.Show("没有发现有药品结余的批次！！！");
                return;
            }
            if (new BatchSelect(ref Batchs).ShowDialog() == DialogResult.OK)
            {
                string vl = "''''";
                bool allselect = true;
                foreach (TreeNode tn in Batchs)
                {
                    if (tn.Checked)
                    {
                        vl = "''" + tn.Text + "''," + vl;
                    }
                    else
                    {
                        allselect = false;
                    }
                }
                if (allselect)
                {
                    Batch = string.Empty;
                }
                else
                {
                    Batch = string.Format(" and Batch in ({0}) ", vl.TrimEnd(','));
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("起始时间不能大于结束时间！！！");
                dateTimePicker1.Value = begindate;
            }
            else
            {
                begindate = dateTimePicker1.Value;
                DateisChange = true;
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("结束时间不能小于起始时间！！！");
                dateTimePicker2.Value = enddate;
            }
            else
            {
                enddate = dateTimePicker2.Value;
                DateisChange = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrugType = comboBox1.SelectedIndex == 0 ? string.Empty : (comboBox1.SelectedIndex > 4 ? string.Format(" and Species = ''{0}'' ", comboBox1.SelectedItem.ToString()) : string.Format(" and DrugType = {0} ", comboBox1.SelectedIndex));
        }

        private void DateChange()
        {
            if (DateisChange)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("SELECT distinct Batch FROM [dbo].[IVRecord] WHERE IVStatus>8 and DATEDIFF({0},InfusionDT,'{1}')>=0  and DATEDIFF({0},InfusionDT,'{2}')<=0 Order by Batch ", DateByRd, dateTimePicker2.Value.ToString("yyyy-MM-dd hh:mm:ss"), dateTimePicker1.Value.ToString("yyyy-MM-dd hh:mm:ss")));
                sb.AppendLine(string.Format("SELECT distinct d.WardCode,d.WardName,WardSeqNo FROM [IVRecord] iv inner join [DWard] d on iv.WardCode=d.WardCode and IVStatus>8 and DATEDIFF({0},InfusionDT,'{1}')>=0  and DATEDIFF({0},InfusionDT,'{2}')<=0 Order by WardSeqNo ", DateByRd, dateTimePicker2.Value.ToString("yyyy-MM-dd hh:mm:ss"), dateTimePicker1.Value.ToString("yyyy-MM-dd hh:mm:ss")));
                using (DataSet ds = new DB_Help().GetPIVAsDB(sb.ToString()))
                {
                    if (ds != null && ds.Tables.Count > 1)
                    {
                        Batchs = new List<TreeNode>();
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            TreeNode tn = new TreeNode();
                            tn.Checked = true;
                            tn.Text = dr["Batch"].ToString();
                            Batchs.Add(tn);
                        }
                        DWards = new List<TreeNode>();
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            TreeNode tn = new TreeNode();
                            tn.Checked = true;
                            tn.Text = dr["WardName"].ToString();
                            tn.Tag = dr["WardCode"].ToString();
                            DWards.Add(tn);
                        }

                    }
                }
                DateisChange = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            DateByRd = "Year";
            DateisChange = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            DateByRd = "Month";
            DateisChange = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            DateByRd = "Day";
            DateisChange = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DateChange();
            if (DWards.Count <= 0)
            {
                MessageBox.Show("没有发现有药品结余的病区！！！");
                return;
            }
            if (new DWardSelect(ref DWards).ShowDialog() == DialogResult.OK)
            {
                string vl = "''''";
                bool allselect = true;
                foreach (TreeNode tn in DWards)
                {
                    if (tn.Checked)
                    {
                        vl = "''" + tn.Tag + "''," + vl;
                    }
                    else
                    {
                        allselect = false;
                    }
                }
                if (allselect)
                {
                    WardCode = string.Empty;
                }
                else
                {
                    WardCode = string.Format(" and iv.WardCode in ({0}) ", vl.TrimEnd(','));
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, null);
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Black;
            textBox1.SelectAll();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                textBox1.ForeColor = Color.DarkGray;
                textBox1.Text = "药品名称/药品简拼";
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, new SolidBrush(dataGridView1.RowHeadersDefaultCellStyle.ForeColor), e.RowBounds.Location.X + (e.RowIndex < 9 ? 31 : (e.RowIndex < 99 ? 25 : 19)), e.RowBounds.Location.Y + 5);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                try
                {
                    using (SaveFileDialog saveFileDialog1 = new SaveFileDialog())
                    {
                        saveFileDialog1.AddExtension = true;
                        saveFileDialog1.DefaultExt = "xls";
                        saveFileDialog1.Filter = "Excel文件(*.xls)|*.xls";
                        saveFileDialog1.RestoreDirectory = true;
                        saveFileDialog1.FileName = DateTime.Now.ToString("药品结余 yyyy-MM-dd");
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            OleDbConnectionStringBuilder osb = new OleDbConnectionStringBuilder();
                            osb.Provider = "Microsoft.Jet.OLEDB.4.0";
                            osb.DataSource = saveFileDialog1.FileName;
                            osb.Add("Extended Properties", "Excel 8.0;HDR=YES;IMEX=2");

                            StringBuilder sb = new StringBuilder();
                            string TableName = DateTime.Now.ToString("hh_mm_ss");
                            sb.Append(string.Format("CREATE TABLE [{0}](", TableName));
                            using (DataTable DatDB = dataGridView1.DataSource as DataTable)
                            {
                                foreach (DataColumn dc in DatDB.Columns)
                                {
                                    sb.Append(string.Format("[{0}]{1},", dc.ColumnName, OleDbType.VarChar));
                                }
                                using (OleDbConnection cn = new OleDbConnection(osb.ConnectionString))
                                {
                                    using (OleDbCommand cmd = new OleDbCommand(sb.ToString().TrimEnd(',') + ")", cn))
                                    {
                                        cn.Open();
                                        cmd.ExecuteNonQuery();
                                        cn.Close();
                                    }
                                    using (OleDbDataAdapter oda = new OleDbDataAdapter(string.Format("select * from [{0}]", TableName), cn))
                                    {
                                        using (OleDbCommandBuilder cb = new OleDbCommandBuilder(oda))
                                        {
                                            cb.DataAdapter.Update(DatDB);
                                        }
                                    }
                                }
                            }
                            MessageBox.Show("保存成功");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("没有结余数据，勿需保存！！！");
            }
        }
    }
}
