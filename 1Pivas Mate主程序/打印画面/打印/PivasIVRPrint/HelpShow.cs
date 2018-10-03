using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace PivasIVRPrint
{
    public partial class HelpShow : Form
    {
        private DataSet ds = new DataSet();
        public HelpShow()
        {
            InitializeComponent();
        }

        private void HelpShow_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (File.Exists(".\\ConfigFile\\HelpShow.Dat"))
            {
                dt.ReadXml(".\\ConfigFile\\HelpShow.Dat");
            }
            else
            {
                dt.Columns.Add("报表字段名", typeof(string));
                dt.Columns.Add("自定义注释", typeof(string));
                dt.Rows.Add("ouvia.brdm", string.Empty);
                dt.Rows.Add("ouvia.brxm", string.Empty);
                dt.Rows.Add("ouvia.zwhm", string.Empty);
                dt.Rows.Add("ouvia.LabelNo", string.Empty);
                dt.Rows.Add("ouvia.num", string.Empty);
                dt.Rows.Add("ouvia.ewm", string.Empty);
                dt.Rows.Add("ouvia.WardName", string.Empty);
                dt.Rows.Add("ouvia.WardSimName", string.Empty);
                dt.Rows.Add("ouvia.Batch", string.Empty);
                dt.Rows.Add("ouvia.Age", string.Empty);
                dt.Rows.Add("ouvia.AgeSTR", string.Empty);
                dt.Rows.Add("ouvia.CaseID", string.Empty);
                dt.Rows.Add("ouvia.FreqCode", string.Empty);
                dt.Rows.Add("ouvia.UsageCode", string.Empty);
                dt.Rows.Add("ouvia.ArrDrugUserCode", string.Empty);
                dt.Rows.Add("ouvia.PZDrugUserCode", string.Empty);
                dt.Rows.Add("ouvia.PackDrugUserCode", string.Empty);
                dt.Rows.Add("ouvia.DrugAC", string.Empty);
                dt.Rows.Add("ouvia.tod", string.Empty);
                dt.Rows.Add("ouvia.FreqNum", string.Empty);
                dt.Rows.Add("ouvia.longdt", string.Empty);
                dt.Rows.Add("ouvia.sex", string.Empty);
                dt.Rows.Add("ouvia.CpUser", string.Empty);
                dt.Rows.Add("ouvia.Remark1", string.Empty);
                dt.Rows.Add("ouvia.Remark2", string.Empty);
                dt.Rows.Add("ouvia.Remark3", string.Empty);
                dt.Rows.Add("ouvia.Remark4", string.Empty);
                dt.Rows.Add("ouvia.Remark5", string.Empty);
                dt.Rows.Add("ouvia.Remark6", string.Empty);
                dt.Rows.Add("ouvia.DeskNo", string.Empty);
                dt.Rows.Add("ouvia.IVD", string.Empty);
                dt.Rows.Add("Med.ypmc", string.Empty);
                dt.Rows.Add("Med.spec", string.Empty);
                dt.Rows.Add("Med.dose", string.Empty);
                dt.Rows.Add("Med.unit", string.Empty);
                dt.Rows.Add("Med.FormUnit", string.Empty);
                dt.Rows.Add("Med.Remark7", string.Empty);
                dt.Rows.Add("Med.Remark8", string.Empty);
                dt.Rows.Add("Med.Remark9", string.Empty);
                dt.Rows.Add("Med.Remark10", string.Empty);
                dt.Rows.Add("Med.PiShi", string.Empty);
            }
            ds.Tables.Add(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 200;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(".\\ConfigFile\\HelpShow.Dat"))
                {
                    File.Delete(".\\ConfigFile\\HelpShow.Dat");
                }
                ds.Tables[0].WriteXml(".\\ConfigFile\\HelpShow.Dat", XmlWriteMode.WriteSchema);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Dispose(true);
            }
        }
    }
}
