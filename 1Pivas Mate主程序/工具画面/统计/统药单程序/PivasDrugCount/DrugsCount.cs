using System;
using System.Data;
using System.Windows.Forms;
using FastReport;
using PIVAsCommon.Helper;

namespace PivasDrugCount
{
    public partial class DrugsCount : Form
    {
        private Report rp = new Report();
        private Report rp2 = new Report();
        private string labs;
        private DB_Help db = new DB_Help();
        private string comboBox3;
        private string comboBox4;
        private string comboBox2;
        private string userID;

        public DrugsCount(string comboBox2, string comboBox3, string comboBox4, string labs, string userID)
        {
            this.labs = labs;
            this.comboBox2 = comboBox2;
            this.comboBox3 = comboBox3;
            this.comboBox4 = comboBox4;
            this.userID = userID;
            InitializeComponent();
            //if (!string.IsNullOrEmpty(piv.CountPrint))
            //{
            //    rp.PrintSettings.Printer = piv.CountPrint;
            //    rp2.PrintSettings.Printer = piv.CountPrint;
            //}
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
                (rp.FindObject("Text32") as FastReport.TextObject).Text = comboBox2;
                (rp.FindObject("Text33") as FastReport.TextObject).Text = comboBox3;
                (rp.FindObject("Text34") as FastReport.TextObject).Text = comboBox4;
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
                (rp2.FindObject("Text35") as FastReport.TextObject).Text = comboBox2;
                (rp2.FindObject("Text36") as FastReport.TextObject).Text = comboBox3;
                (rp2.FindObject("Text37") as FastReport.TextObject).Text = comboBox4;
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            if (this.checkBox1.Checked)
            {
                try
                {
                    rp.Preview = previewControlFR;
                    rp.Load(".\\Crystal\\DrugsCount.frx");
                    //MessageBox.Show(string.Format("SELECT d.PortNo,D.DrugCode,D.DrugName,D.DrugNameJC,D.Spec,d.FormUnit,SUM(DgNo)COUN FROM IVRecord iv inner join IVRecordDetail ivd on iv.IVRecordID=ivd.IVRecordID and labelno in ({0}) inner join DDrug d on ivd.DrugCode=d.DrugCode  GROUP BY D.DrugCode,D.DrugName,D.DrugNameJC,D.Spec,PortNo,d.FormUnit order by PortNo", labs));
                    ds = db.GetPIVAsDB(string.Format("SELECT d.PortNo,D.DrugCode,D.DrugName,D.DrugNameJC,D.Spec,d.FormUnit,SUM(DgNo)COUN FROM IVRecord iv inner join IVRecordDetail ivd on iv.IVRecordID=ivd.IVRecordID and labelno in ({0}) inner join DDrug d on ivd.DrugCode=d.DrugCode AND d.Precious <> 0  GROUP BY D.DrugCode,D.DrugName,D.DrugNameJC,D.Spec,PortNo,d.FormUnit order by PortNo", labs));
                    ds.Tables[0].TableName = "dat";
                    rp.RegisterData(ds, "ds");
                    (rp.FindObject("Data1") as FastReport.DataBand).DataSource = rp.GetDataSource("dat");
                    (rp.FindObject("Text32") as FastReport.TextObject).Text = comboBox2;
                    (rp.FindObject("Text33") as FastReport.TextObject).Text = comboBox3;
                    (rp.FindObject("Text34") as FastReport.TextObject).Text = comboBox4;
                    rp.Show();

                    rp2.Preview = previewControl1;
                    rp2.Load(".\\Crystal\\WDrugsCount.frx");
                    //MessageBox.Show(string.Format("SELECT WardName,d.PortNo,D.DrugCode,D.DrugName,D.DrugNameJC,D.Spec,d.FormUnit,SUM(DgNo)COUN FROM IVRecord iv inner join IVRecordDetail ivd on iv.IVRecordID=ivd.IVRecordID and labelno in ({0}) inner join DDrug d on ivd.DrugCode=d.DrugCode  GROUP BY iv.WardName,D.DrugCode,D.DrugName,D.DrugNameJC,D.Spec,PortNo,d.FormUnit order by WardName,PortNo", labs));
                    ds = db.GetPIVAsDB(string.Format("SELECT WardName,d.PortNo,D.DrugCode,D.DrugName,D.DrugNameJC,D.Spec,d.FormUnit,SUM(DgNo)COUN FROM IVRecord iv inner join IVRecordDetail ivd on iv.IVRecordID=ivd.IVRecordID and labelno in ({0}) inner join DDrug d on ivd.DrugCode=d.DrugCode AND d.Precious <> 0  GROUP BY iv.WardName,D.DrugCode,D.DrugName,D.DrugNameJC,D.Spec,PortNo,d.FormUnit order by WardName,PortNo", labs));
                    ds.Tables[0].TableName = "dat";
                    DataTable dt = db.GetPIVAsDB(string.Format("SELECT distinct WardName FROM IVRecord where labelno in ({0})", labs)).Tables[0].Copy();
                    dt.TableName = "wds";
                    ds.Tables.Add(dt);
                    rp2.RegisterData(ds, "ds");
                    (rp2.FindObject("Data2") as FastReport.DataBand).DataSource = rp2.GetDataSource("dat");
                    (rp2.FindObject("Data1") as FastReport.DataBand).DataSource = rp2.GetDataSource("wds");
                    (rp2.FindObject("Text35") as FastReport.TextObject).Text = comboBox2;
                    (rp2.FindObject("Text36") as FastReport.TextObject).Text = comboBox3;
                    (rp2.FindObject("Text37") as FastReport.TextObject).Text = comboBox4;
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
            else 
            {
                try
                {
                    rp.Preview = previewControlFR;
                    rp.Load(".\\Crystal\\DrugsCount.frx");
                    //MessageBox.Show(string.Format("SELECT d.PortNo,D.DrugCode,D.DrugName,D.DrugNameJC,D.Spec,d.FormUnit,SUM(DgNo)COUN FROM IVRecord iv inner join IVRecordDetail ivd on iv.IVRecordID=ivd.IVRecordID and labelno in ({0}) inner join DDrug d on ivd.DrugCode=d.DrugCode  GROUP BY D.DrugCode,D.DrugName,D.DrugNameJC,D.Spec,PortNo,d.FormUnit order by PortNo", labs));
                    ds = db.GetPIVAsDB(string.Format("SELECT d.PortNo,D.DrugCode,D.DrugName,D.DrugNameJC,D.Spec,d.FormUnit,SUM(DgNo)COUN FROM IVRecord iv inner join IVRecordDetail ivd on iv.IVRecordID=ivd.IVRecordID and labelno in ({0}) inner join DDrug d on ivd.DrugCode=d.DrugCode  GROUP BY D.DrugCode,D.DrugName,D.DrugNameJC,D.Spec,PortNo,d.FormUnit order by PortNo", labs));
                    ds.Tables[0].TableName = "dat";
                    rp.RegisterData(ds, "ds");
                    (rp.FindObject("Data1") as FastReport.DataBand).DataSource = rp.GetDataSource("dat");
                    (rp.FindObject("Text32") as FastReport.TextObject).Text = comboBox2;
                    (rp.FindObject("Text33") as FastReport.TextObject).Text = comboBox3;
                    (rp.FindObject("Text34") as FastReport.TextObject).Text = comboBox4;
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
                    (rp2.FindObject("Text35") as FastReport.TextObject).Text = comboBox2;
                    (rp2.FindObject("Text36") as FastReport.TextObject).Text = comboBox3;
                    (rp2.FindObject("Text37") as FastReport.TextObject).Text = comboBox4;
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
}
