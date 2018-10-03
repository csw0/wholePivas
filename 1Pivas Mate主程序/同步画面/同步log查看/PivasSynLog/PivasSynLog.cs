using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace PivasSynLog
{
    public partial class PivasSynLog : Form
    {
        private static DB_Help db =new DB_Help();
        private static string SynCode;
        private bool isrun;
        private DataTable dt;
        public PivasSynLog()
        {
            InitializeComponent();
        }
        public PivasSynLog(string SynCd)
        {
            SynCode = SynCd; 
            InitializeComponent();
        }

        #region 窗体移动
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Thread th = new Thread(() => move(Location.X, Location.Y));
            th.IsBackground = true;
            isrun = true;
            th.Start();
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isrun = false;
        }
        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            isrun = false;
        }
        private void move(int X, int Y)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            Point p = MousePosition;
            while (isrun)
            {
                Location = new Point(X + MousePosition.X - p.X, Y + MousePosition.Y - p.Y);
            }
        }
        #endregion

        private void PivasSynLog_Load(object sender, EventArgs e)
        {
            try
            {
            a:
                {
                    textBox1.Text = string.Empty;
                    labelSynCount.Text = label20.Text = "0";
                    label6.Text = label7.Text = label9.Text = label11.Text = label13.Text = label15.Text = string.Empty;
                    dataGridView1.DataSource = dataGridView2.DataSource = null;
                    label2.Visible = !(comboBox2.Visible = string.IsNullOrEmpty(SynCode));
                    comboBox1.Items.Clear();
                    comboBox3.Items.Clear();
                    DataSet ds = new DataSet();
                    if (comboBox2.Visible)
                    {
                        comboBox2.Items.Clear();
                        ds = db.GetPIVAsDB("SELECT [SYnName] FROM dbo.SynSet order by SynCode");
                        if (ds == null || ds.Tables[0].Rows.Count == 0)
                        {
                            MessageBox.Show("未找到同步项请检查数据库信息");
                        }
                        else
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                comboBox2.Items.Add(dr[0].ToString());
                            }
                            comboBox2.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        ds = db.GetPIVAsDB(string.Format("SELECT [SYnName] FROM dbo.SynSet where SynCode='{0}'", SynCode));
                        if (ds == null || ds.Tables[0].Rows.Count == 0)
                        {
                            MessageBox.Show(SynCode + "--这种同步方式不存在");
                            SynCode = string.Empty;
                            goto a;
                        }
                        else
                        {
                            label2.Text = ds.Tables[0].Rows[0][0].ToString();
                            showTime();
                        }
                    }
                    ds.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void showTime()
        {
            DataSet ds = db.GetPIVAsDB(string.Format(
                "select distinct CONVERT(varchar,StartTime,111) StartTime from dbo.SynLog where [SynCode]='{0}' "
                +" order by StartTime desc", SynCode));
            comboBox1.Items.Clear();
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("未找到此同步项的同步数据,检查此项是否从未同步");
            }
            else
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    comboBox1.Items.Add(dr[0].ToString());
                }
                comboBox1.SelectedIndex = 0;
            }
            ds.Dispose();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SynCode = db.GetPIVAsDB(string.Format("SELECT [SynCode] FROM dbo.SynSet where SYnName='{0}'", comboBox2.SelectedItem.ToString())).Tables[0].Rows[0][0].ToString();
                showTime();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                showDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void showDetail()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT [StartTime],[StartUPTime],[EndUPTime],[EndTime],[SynData],[SynAct],[Success],[Schedule],[ScheduleTxt],ISNULL(DEmployeeName,'自动同步') [DEmployeeName] ");
            sb.Append("FROM [dbo].[SynLog] left join dbo.DEmployee on SynAct=DEmployeeID where SynID='{0}'");
            DataSet ds = db.GetPIVAsDB(string.Format(sb.ToString(), dataGridView1.SelectedRows[0].Cells["SynID"].Value));
            DataRow dr = ds.Tables[0].Rows[0];
            label6.Text = dr["DEmployeeName"].ToString();
            label7.Text = dr["ScheduleTxt"].ToString();
            if (Equals(label7.Text, "同步成功"))
            {
                label7.ForeColor = Color.Green;
            }
            else
            {
                label7.ForeColor = Color.Red;
            }
            label9.Text = dr["StartTime"].ToString();
            label13.Text = dr["EndTime"].ToString();
            label11.Text = dr["StartUPTime"].ToString();
            label15.Text = dr["EndUPTime"].ToString();
            if (string.IsNullOrEmpty(dr["SynData"].ToString()))
            {
                dataGridView2.DataSource = null;
                MessageBox.Show("此次同步没有任何数据");
            }
            else
            {
                DataSet dt = new DataSet();
                dt.ReadXml(new XmlTextReader(new StringReader(dr["SynData"].ToString())));
                dataGridView2.DataSource = dt.Tables[0];
                this.dt = dt.Tables[0];
                label20.Text = dataGridView2.RowCount.ToString();
                if (string.IsNullOrEmpty(textBox1.Text.Trim()))
                {
                    comboBox3.Items.Clear();
                    foreach (DataColumn dc in dt.Tables[0].Columns)
                    {
                        comboBox3.Items.Add(dc.ColumnName);
                    }
                    comboBox3.SelectedIndex = 0;
                }
                else
                {
                    btnSearch_Click(null, null);
                }
                dt.Dispose();
            }
            ds.Dispose();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = string.Empty;
                DataSet ds = db.GetPIVAsDB(string.Format("select COUNT(1) from dbo.SynLog where DATEDIFF(DAY,StartTime,'{0}')=0 and [SynCode]='{1}'", comboBox1.SelectedItem.ToString(), SynCode));
                labelSynCount.Text = ds.Tables[0].Rows[0][0].ToString();
                ds = db.GetPIVAsDB(string.Format("select StartTime as '开始时间',SynID from dbo.SynLog where DATEDIFF(DAY,StartTime,'{0}')=0 and [SynCode]='{1}' order by StartTime desc", comboBox1.SelectedItem.ToString(), SynCode));
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns["开始时间"].Width = 200;
                dataGridView1.Rows[0].Selected = true;
                showDetail();
                ds.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void labelMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                db.SetPIVAsDB(string.Format("DELETE FROM [dbo].[SynLog] WHERE SynCode='{0}' and DATEDIFF(DAY,StartTime,'{1}')=0", SynCode, comboBox1.SelectedItem.ToString()));
                MessageBox.Show("删除成功");
                PivasSynLog_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                showDetail();
            }
            else
            {
                DataRow[] drs = dt.Select(string.Format("{0} like ('%{1}%')", comboBox3.SelectedItem.ToString(), textBox1.Text));
                DataTable d = new DataTable();
                foreach (DataColumn dc in dt.Columns)
                {
                    d.Columns.Add(new DataColumn(dc.ColumnName));
                }
                foreach (DataRow dr in drs)
                {
                    d.Rows.Add(dr.ItemArray);
                }
                dataGridView2.DataSource = d;
            }
        }
    }
}
