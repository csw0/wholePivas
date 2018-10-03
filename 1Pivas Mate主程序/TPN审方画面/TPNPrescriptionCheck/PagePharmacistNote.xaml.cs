using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes; 


namespace TPNReview
{
    /// <summary>
    /// PagePharmacistNote.xaml 的交互逻辑
    /// </summary>
    public partial class PagePharmacistNote : Page, IContentPage
    {
        private PatientModel patient = null;
        private Action onRefParent = null;
        private DataTable tblOpRecord = null;

        public PagePharmacistNote()
        {
            InitializeComponent();
             
            dpSDate.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01"));
            dpEDate.SelectedDate = DateTime.Now;
            //dpSDate.IsEnabled = false;
            //dpEDate.IsEnabled = false;

            dpSDate.SelectedDateChanged += dpDate_SelectedDateChanged;
            dpEDate.SelectedDateChanged += dpDate_SelectedDateChanged;

            cbbOpType.Items.Add(new BLPublic.CodeNameItem("all", "<全部>"));
            cbbOpType.Items.Add(new BLPublic.CodeNameItem("custody", "监护"));
            cbbOpType.Items.Add(new BLPublic.CodeNameItem("intervene", "干预"));
            //cbbOpType.Items.Add(new BLPublic.CodeNameItem("monitor", "审方"));
            cbbOpType.Items.Add(new BLPublic.CodeNameItem("note", "笔记"));
            cbbOpType.SelectedIndex = 0;
        }
        
        #region INTERFACE
        public void init(Action _refParent)
        {
            this.onRefParent = _refParent;
        }
        public void setPatient(PatientModel _pnt)
        {
            if (null == _pnt)
                return;

            this.patient = _pnt;

            if (true != cbAllPatient.IsChecked)
                loadRecord();
        }

        public void clear()
        {
            if (null != lvRecord.ItemsSource)
                lvRecord.ItemsSource = null; 
        }
        #endregion

        private void loadRecord()
        {
            if (null == tblOpRecord)
            {
                this.tblOpRecord = new DataTable();
                this.tblOpRecord.Columns.Add("TypeCode", typeof(string));
                this.tblOpRecord.Columns.Add("ID", typeof(int));
                this.tblOpRecord.Columns.Add("DeptName", typeof(string));
                this.tblOpRecord.Columns.Add("BedNo", typeof(string));
                this.tblOpRecord.Columns.Add("PatientName", typeof(string));
                this.tblOpRecord.Columns.Add("OpTime", typeof(DateTime)); 
                this.tblOpRecord.Columns.Add("Content", typeof(string));
                this.tblOpRecord.Columns.Add("Operater", typeof(string));
                this.tblOpRecord.PrimaryKey = new DataColumn[] { this.tblOpRecord.Columns["TypeCode"], 
                                                                 this.tblOpRecord.Columns["ID"] };
            }
            else
            {
                this.tblOpRecord.Clear();
                lvRecord.Items.Refresh();
            }
             
            ComClass.getAcountNameList(AppConst.db, ComClass.Acounts);

            loadCustody();
            loadIntervene();
            loadPMNote();

            this.tblOpRecord.DefaultView.Sort = "OpTime";
            lvRecord.ItemsSource = this.tblOpRecord.DefaultView;
        }

        /// <summary>
        /// 加载监护
        /// </summary>
        private void loadCustody()
        {
            string sql = "";
            if (true == cbAllPatient.IsChecked)
                sql = string.Format(SQL.SEL_CUSTODY_BYDATE, ((DateTime)dpSDate.SelectedDate).ToString("yyyy-MM-dd"),
                    ((DateTime)dpEDate.SelectedDate).AddDays(1).ToString("yyyy-MM-dd"));
            else if (null == this.patient)
                return;
            else
                sql = string.Format(SQL.SEL_CUSTODY_BYPNT2, this.patient.PatientCode);

            DataTable tbl = null;
            if (AppConst.db.GetRecordSet(sql, ref tbl))
            {
                BLPublic.BLDataReader bldr = new BLPublic.BLDataReader(tbl.CreateDataReader());
                string content = "";
                while (bldr.next())
                {
                    content = WinCustodyEdit.getObjectStr(bldr.getInt("CustodyID")) + ". " + bldr.getString("CustodyDesc");
                    if (!bldr.isNull("FinishTime"))
                        content += ". (已结束)";

                    this.tblOpRecord.Rows.Add(new object[] {"custody", bldr.getInt("CustodyID"), 
                        bldr.getString("DeptName"), bldr.getString("BedNo"), bldr.getString("PatientName"),
                        bldr.getDateTime("CustodyTime"), content, ComClass.getEmpName(bldr.getString("Custodyer")) });
                }

                bldr.close();
                tbl.Clear();
            }
            else
                BLPublic.Dialogs.Error("加载监护失败:" + AppConst.db.Error);
        }

        /// <summary>
        /// 加载干预
        /// </summary>
        private void loadIntervene()
        {
            string sql = "";
            if (true == cbAllPatient.IsChecked)
                sql = string.Format(SQL.SEL_INTERVENE_BYDATE, ((DateTime)dpSDate.SelectedDate).ToString("yyyy-MM-dd"),
                    ((DateTime)dpEDate.SelectedDate).AddDays(1).ToString("yyyy-MM-dd"));
            else if (null == this.patient)
                return;
            else
                sql = string.Format(SQL.SEL_INTERVENE_BYPNT2, this.patient.PatientCode);

            IDataReader dr = null;
            if (AppConst.db.GetRecordSet(sql, ref dr, true))
            {
                BLPublic.BLDataReader bldr = new BLPublic.BLDataReader(dr);
                string content = "";
                while (bldr.next())
                {
                    content = WinInterveneEdit.getObjectStr(bldr.getInt("InterveneID")) + ". " + bldr.getString("IntervenePlan");

                    this.tblOpRecord.Rows.Add(new object[] {"intervene", bldr.getInt("InterveneID"), 
                        bldr.getString("DeptName"), bldr.getString("BedNo"), bldr.getString("PatientName"),
                        bldr.getDateTime("InterveneTime"), content, ComClass.getEmpName(bldr.getString("Intervener")) });
                }
                dr.Close();
            }
            else
                BLPublic.Dialogs.Error("加载干预失败:" + AppConst.db.Error);
        }
        /// <summary>
        /// 新增笔记时刷新
        /// </summary>
        /// <param name="_content"></param>
        public void addNote(int _id, string _content)
        {
            this.tblOpRecord.Rows.Add(new object[] {"note", _id, this.patient.WardName,
                        this.patient.BedNo, this.patient.PatientName, DateTime.Now, 
                        _content, ComClass.getEmpName(AppConst.LoginEmpCode) });

            lvRecord.Items.Refresh();
        }
         
        /// <summary>
        /// 加载药师日记
        /// </summary>
        private void loadPMNote()
        { 
            string sql = "";
            if (true == cbAllPatient.IsChecked)
                sql = string.Format(SQL.SEL_PM_NOTE_WPNT_BYDATE, ((DateTime)dpSDate.SelectedDate).ToString("yyyy-MM-dd"),
                    ((DateTime)dpEDate.SelectedDate).AddDays(1).ToString("yyyy-MM-dd"));
            else if (null == this.patient)
                return;
            else
                sql = string.Format(SQL.SEL_PM_NOTE_WPNT_BYPNT, this.patient.PatientCode);

            IDataReader dr = null;
            if (AppConst.db.GetRecordSet(sql, ref dr, true))
            {
                BLPublic.BLDataReader bldr = new BLPublic.BLDataReader(dr);
                while (bldr.next())
                {
                    this.tblOpRecord.Rows.Add(new object[] {"note", bldr.getInt("NoteID"), 
                        bldr.getString("DeptName"), bldr.getString("BedNo"), bldr.getString("PatientName"),
                        bldr.getDateTime("NoteTime"), bldr.getString("NoteContent"), 
                        ComClass.getEmpName(bldr.getString("Noter")) });
                }
                dr.Close();
            }
            else
                BLPublic.Dialogs.Error("加载药师笔记失败:" + AppConst.db.Error); 
        }

        private void btnMod_Click(object sender, RoutedEventArgs e)
        {
            if (null == lvRecord.SelectedItem)
            {
                BLPublic.Dialogs.Alert("请选择要修改的记录.");
                lvRecord.Focus();
                return;
            }

            DataRowView dv = (DataRowView)lvRecord.SelectedItem;
            string typeCode = dv["TypeCode"].ToString();

            if ("note".Equals(typeCode))
            {
                WinInput input = new WinInput();
                input.Owner = AppConst.winMain;
                input.inputLong("笔记", "修改笔记", (_txt) =>
                    {
                        if (AppConst.db.ExecSQL(string.Format(SQL.MOD_PM_NOTE, BLPublic.DBOperate.ACS(_txt), 
                                                dv["ID"].ToString())))
                        {
                            dv["Content"] = _txt;
                            lvRecord.Items.Refresh();
                            return true;
                        }
                        else
                            BLPublic.Dialogs.Error("保存笔记失败:" + AppConst.db.Error);
                        return false;
                    },
                    dv["Content"].ToString());

                input.ShowDialog();
                input = null;
            }
            else if ("custody".Equals(typeCode))
            {
                if (dv["Content"].ToString().Contains("已完成"))
                {
                    BLPublic.Dialogs.Alert("已完成监护，不可修改.");
                    return;
                }

                WinCustodyEdit win = new WinCustodyEdit();
                win.Owner = AppConst.winMain;
                win.EditID = Convert.ToInt32(dv["ID"].ToString());
                win.OnEnd = (isOK) =>
                    {
                        if (isOK)
                        {
                            dv["Content"] = win.getObjectStr() + "." + win.txtCustodyDesc.Text.Trim();
                            lvRecord.Items.Refresh();
                        }
                    };

                win.Topmost = true;
                win.Show();
            }
            else if ("intervene".Equals(typeCode))
            {
                WinInterveneEdit win = new WinInterveneEdit();
                win.Owner = AppConst.winMain;
                win.EditID = Convert.ToInt32(dv["ID"].ToString());
                win.OnEnd = (isOK) =>
                {
                    if (isOK)
                    {
                        dv["Content"] = win.getObjectStr() + "." + win.txtInterveneDesc.Text.Trim();
                        lvRecord.Items.Refresh();
                    }
                };

                win.Topmost = true;
                win.Show();
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (null == lvRecord.SelectedItem)
            {
                BLPublic.Dialogs.Alert("请选择要删除的记录.");
                lvRecord.Focus();
                return;
            }

            if (BLPublic.Dialogs.Yes != BLPublic.Dialogs.Ask("是否确定删除所选记录?"))
                return;

            DataRowView dv = (DataRowView)lvRecord.SelectedItem;
            string typeCode = dv["TypeCode"].ToString();
            string sql = "";
            if ("note".Equals(typeCode))
                sql = string.Format(SQL.DEL_PM_NOTE, dv["ID"].ToString());
            else if ("custody".Equals(typeCode))
            {
                sql = string.Format(SQL.DEL_CUSTODY_BYID, dv["ID"].ToString());
                sql += "; ";
                sql += string.Format(SQL.DEL_CUSTODY_OBJ, dv["ID"].ToString());
            }
            else if ("intervene".Equals(typeCode))
            {
                sql = string.Format(SQL.DEL_INTERVENE, dv["ID"].ToString());
                sql += "; ";
                sql += string.Format(SQL.DEL_INTERVENE_OBJ, dv["ID"].ToString());
            }
            else
                return;

            if (AppConst.db.ExecSQL(sql))
            {
                DataRow r = this.tblOpRecord.Rows.Find(new object[]{typeCode, dv["ID"]});
                if (null != r)
                    this.tblOpRecord.Rows.Remove(r); 
                lvRecord.Items.Refresh();

                BLPublic.Dialogs.Info("删除成功");
            }
            else
                BLPublic.Dialogs.Error("删除失败:" + AppConst.db.Error);
        }

        private void miFinish_Click(object sender, RoutedEventArgs e)
        { 
            if (null == lvRecord.SelectedItem)
            {
                BLPublic.Dialogs.Alert("请选择要完成的监护.");
                lvRecord.Focus();
                return;
            }

            if (BLPublic.Dialogs.Yes != BLPublic.Dialogs.Ask("是否确定结束监护?"))
                return;
            
            DataRowView dv = (DataRowView)lvRecord.SelectedItem;
            if (AppConst.db.ExecSQL(string.Format(SQL.FINISH_CUSTODY, dv["ID"].ToString(), AppConst.LoginEmpCode, "")))
            {
                BLPublic.Dialogs.Info("完成成功");
                dv["Content"] = dv["Content"].ToString() + ". (已结束)";
            }
            else
                BLPublic.Dialogs.Error("完成失败:" + AppConst.db.Error);
        }
         

        private void cbAllPatient_Click(object sender, RoutedEventArgs e)
        {
            dpSDate.IsEnabled = true == cbAllPatient.IsChecked;
            dpEDate.IsEnabled = true == cbAllPatient.IsChecked;

            loadRecord();
        }

        private void dpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            loadRecord();
        }

        private void lvNotes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (null != lvRecord.SelectedItem)
                btnMod_Click(sender, null);
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            /*Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                MessageBox.Show("Excel is not properly installed!!");
                return;
            }


            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xlWorkSheet.Cells[1, 1] = "ID";
            xlWorkSheet.Cells[1, 2] = "Name";
            xlWorkSheet.Cells[2, 1] = "1";
            xlWorkSheet.Cells[2, 2] = "One";
            xlWorkSheet.Cells[3, 1] = "2";
            xlWorkSheet.Cells[3, 2] = "Two";


            xlWorkBook.SaveAs("d:\\csharp-Excel.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);*/
        }

        private void cmRecord_Opened(object sender, RoutedEventArgs e)
        {
            /*if (null == lvRecord.SelectedItem)
                return;

            DataRowView dv = (DataRowView)lvRecord.SelectedItem;
            string typeCode = dv["TypeCode"].ToString();
            if ("custody".Equals(typeCode))
            {
                if (!dv["Content"].ToString().Contains("完成"))
                    miFinish.Visibility = System.Windows.Visibility.Visible;
            }
            else
                miFinish.Visibility = System.Windows.Visibility.Collapsed; */
        }
    }
}
