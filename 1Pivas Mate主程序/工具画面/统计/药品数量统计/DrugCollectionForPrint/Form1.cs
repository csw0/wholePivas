using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DrugCollectionForPrint
{
    public partial class Form1 : Form
    {
        dao.sqldb sql = new DrugCollectionForPrint.dao.sqldb();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadStatus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!TimeCheck())
            {
                MessageBox.Show("请验证时间");
                return;
            }
            if (!StatusCheck())
            {
                MessageBox.Show("请验证状态");
                return;
            }
            LoadListView1();
        }

        /// <summary>
        /// 验证时间正确性
        /// </summary>
        /// <returns></returns>
        private bool TimeCheck()
        {
            bool Cando = false;

            if (dateTimePicker2.Value >= dateTimePicker1.Value)
            {
                Cando = true;
            }
            return Cando;
        }

        /// <summary>
        /// 验证状态正确性
        /// </summary>
        /// <returns></returns>
        private bool StatusCheck()
        {
            bool Cando = false;
            if (Convert.ToInt32(comboBox2 .SelectedValue)>=Convert.ToInt32(comboBox1.SelectedValue))
            {
                Cando = true;
            }
            return Cando;
        }

        /// <summary>
        /// 载入状态
        /// </summary>
        private void LoadStatus()
        {
            comboBox1.DataSource= sql.IVStatusSelect().Tables[0];
            comboBox1.DisplayMember = "CheckName";
            comboBox1.ValueMember = "CheckID";

            comboBox2.DataSource = sql.IVStatusSelect().Copy().Tables[0];
            comboBox2.DisplayMember = "CheckName";
            comboBox2.ValueMember = "CheckID";
        }

        /// <summary>
        /// 载入ListView1
        /// </summary>
        private void LoadListView1()
        {
            DataSet ds = new DataSet();

            ds = sql.DrugSelect(dateTimePicker1.Value.ToShortDateString(), dateTimePicker2.Value.ToShortDateString(), comboBox1.SelectedValue.ToString(), comboBox2.SelectedValue.ToString(), checkBox1.Checked,checkBox2.Checked);

            dataGridView1.DataSource = ds.Tables[0];
        }




        public void DataToExcel(DataGridView m_DataView)
        {
            SaveFileDialog kk = new SaveFileDialog();
            kk.Title = "工作量统计" + DateTime.Now.ToString("yyyMMdd");
            kk.Filter = "EXECL文件(*.xls) |*.xls |所有文件(*.*) |*.*";
            kk.FilterIndex = 1;
            if (kk.ShowDialog() == DialogResult.OK)
            {
                string FileName = kk.FileName;
                if (File.Exists(FileName))
                    File.Delete(FileName);
                FileStream objFileStream;
                StreamWriter objStreamWriter;
                string strLine = "";
                objFileStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write);
                objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);
                for (int i = 0; i < m_DataView.Columns.Count; i++)
                {
                    if (m_DataView.Columns[i].Visible == true)
                    {
                        strLine = strLine + m_DataView.Columns[i].HeaderText.ToString() + Convert.ToChar(9);
                    }
                }
                objStreamWriter.WriteLine(strLine);
                strLine = "";

                for (int i = 0; i < m_DataView.Rows.Count; i++)
                {
                    if (m_DataView.Columns[0].Visible == true)
                    {
                        if (m_DataView.Rows[i].Cells[0].Value == null)
                            strLine = strLine + " " + Convert.ToChar(9);
                        else
                            strLine = strLine + m_DataView.Rows[i].Cells[0].Value.ToString() + Convert.ToChar(9);
                    }
                    for (int j = 1; j < m_DataView.Columns.Count; j++)
                    {
                        if (m_DataView.Columns[j].Visible == true)
                        {
                            if (m_DataView.Rows[i].Cells[j].Value == null)
                                strLine = strLine + " " + Convert.ToChar(9);
                            else
                            {
                                string rowstr = "";
                                rowstr = m_DataView.Rows[i].Cells[j].Value.ToString();
                                if (rowstr.IndexOf("\r\n") > 0)
                                    rowstr = rowstr.Replace("\r\n", " ");
                                if (rowstr.IndexOf("\t") > 0)
                                    rowstr = rowstr.Replace("\t", " ");
                                strLine = strLine + rowstr + Convert.ToChar(9);
                            }
                        }
                    }
                    objStreamWriter.WriteLine(strLine);
                    strLine = "";
                }
                objStreamWriter.Close();
                objFileStream.Close();
                MessageBox.Show(this, "保存EXCEL成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataToExcel(dataGridView1);
        }




    }
}
