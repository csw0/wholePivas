using System;
using System.Data;
using System.Windows.Forms;
using FastReport;
using PIVAsCommon.Helper;

namespace PivasIVRPrint
{
    public partial class DrugsCount : Form
    {
        private Report rp = new Report();
        private Report rp2 = new Report();
        private string labs;
        private DB_Help db = new DB_Help();
        private UserControlPrint piv;
        public DrugsCount(UserControlPrint piv, string labs)
        {
            this.piv = piv;
            this.labs = labs;
            InitializeComponent();
            if (!string.IsNullOrEmpty(piv.CountPrint))
            {
                rp.PrintSettings.Printer = piv.CountPrint;
                rp2.PrintSettings.Printer = piv.CountPrint;
            }
        }

        private void DrugsCount_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            try
            {
                rp.Preview = previewControlFR;
                rp.Load(".\\Crystal\\DrugsCount.frx");
                //MessageBox.Show(string.Format("SELECT d.PortNo,D.DrugCode,D.DrugName,D.DrugNameJC,D.Spec,d.FormUnit,SUM(DgNo)COUN FROM IVRecord iv inner join IVRecordDetail ivd on iv.IVRecordID=ivd.IVRecordID and labelno in ({0}) inner join DDrug d on ivd.DrugCode=d.DrugCode  GROUP BY D.DrugCode,D.DrugName,D.DrugNameJC,D.Spec,PortNo,d.FormUnit order by PortNo", labs));
                ds = db.GetPIVAsDB(string.Format("SELECT d.PortNo,D.DrugCode,D.DrugName,D.DrugNameJC,D.Spec,d.FormUnit,SUM(DgNo)COUN FROM IVRecord iv inner join IVRecordDetail ivd on iv.IVRecordID=ivd.IVRecordID and labelno in ({0}) inner join DDrug d on ivd.DrugCode=d.DrugCode  GROUP BY D.DrugCode,D.DrugName,D.DrugNameJC,D.Spec,PortNo,d.FormUnit order by PortNo", labs));
                ds.Tables[0].TableName = "dat";
                rp.RegisterData(ds, "ds");
                (rp.FindObject("Data1") as FastReport.DataBand).DataSource = rp.GetDataSource("dat");
                (rp.FindObject("Text32") as FastReport.TextObject).Text = piv.label2.Visible ? piv.label2.Text : piv.comboBox2.SelectedItem.ToString();
                (rp.FindObject("Text33") as FastReport.TextObject).Text = piv.comboBox3.SelectedItem.ToString();
                (rp.FindObject("Text34") as FastReport.TextObject).Text = piv.comboBox4.SelectedItem.ToString();
                rp.Show();

                rp2.Preview = previewControl1;
                rp2.Load(".\\Crystal\\WDrugsCount.frx");
                //MessageBox.Show(string.Format("SELECT WardName,d.PortNo,D.DrugCode,D.DrugName,D.DrugNameJC,D.Spec,d.FormUnit,SUM(DgNo)COUN FROM IVRecord iv inner join IVRecordDetail ivd on iv.IVRecordID=ivd.IVRecordID and labelno in ({0}) inner join DDrug d on ivd.DrugCode=d.DrugCode  GROUP BY iv.WardName,D.DrugCode,D.DrugName,D.DrugNameJC,D.Spec,PortNo,d.FormUnit order by WardName,PortNo", labs));
                ds = db.GetPIVAsDB(string.Format("SELECT WardName,d.PortNo,D.DrugCode,D.DrugName,D.DrugNameJC,D.Spec,d.FormUnit,SUM(DgNo)COUN FROM IVRecord iv inner join IVRecordDetail ivd on iv.IVRecordID=ivd.IVRecordID and labelno in ({0}) inner join DDrug d on ivd.DrugCode=d.DrugCode  GROUP BY iv.WardName,D.DrugCode,D.DrugName,D.DrugNameJC,D.Spec,PortNo,d.FormUnit order by WardName,PortNo", labs));
                ds.Tables[0].TableName = "dat";
                DataTable dt = db.GetPIVAsDB(string.Format("SELECT distinct WardName FROM IVRecord where labelno in ({0})", labs)).Tables[0].Copy();
                dt.TableName = "wds";
                ds.Tables.Add(dt);
                rp2.RegisterData(ds, "ds");
                (rp2.FindObject("Data2") as FastReport.DataBand).DataSource = rp2.GetDataSource("dat");
                (rp2.FindObject("Data1") as FastReport.DataBand).DataSource = rp2.GetDataSource("wds");
                (rp2.FindObject("Text35") as FastReport.TextObject).Text = piv.label2.Visible ? piv.label2.Text : piv.comboBox2.SelectedItem.ToString();
                (rp2.FindObject("Text36") as FastReport.TextObject).Text = piv.comboBox3.SelectedItem.ToString();
                (rp2.FindObject("Text37") as FastReport.TextObject).Text = piv.comboBox4.SelectedItem.ToString();
                rp2.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ds.Dispose();
            }
        }
    }
}
