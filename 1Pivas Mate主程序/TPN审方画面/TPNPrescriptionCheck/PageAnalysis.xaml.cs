using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using FastReport;
using FormReport;

namespace TPNReview
{
    /// <summary>
    /// PageAnalysis.xaml 的交互逻辑
    /// </summary>
    public partial class PageAnalysis : Page, IContentPage
    {
        public const int ANALYSIS_DOCID = 22;


        private PatientModel patient = null;
        private Action onRefParent = null;
        private formReport frmRpt = null;
        private Report report = null; 

        public PageAnalysis()
        {
            InitializeComponent();

            this.frmRpt = new formReport();
            frmHostReport.Child = this.frmRpt; 
        }

        #region INTERFACE
        public void init(Action _refParent)
        {
            this.onRefParent = _refParent;
        }
        public void setPatient(PatientModel _pnt)
        {
            this.patient = _pnt;

            this.frmRpt.showReport(null);

            if (null != this.patient)
                listRecord(this.patient.PatientCode);
        }

        public void clear()
        {
            lvRecord.Items.Clear(); 
        }
        #endregion

        /// <summary>
        /// 加载患者评估记录
        /// </summary>
        /// <param name="_pntCode"></param>
        private void listRecord(string _patientCode)
        {
            lvRecord.Items.Clear();
            IDataReader dr = null;
            if (!AppConst.db.GetRecordSet(string.Format(SQL.SEL_ANALYSIS_RCD, _patientCode), ref dr))
            {
                BLPublic.Dialogs.Error("加载评价记录失败:" + AppConst.db.Error);
                return;
            }

            BLPublic.BLDataReader blDR = new BLPublic.BLDataReader(dr);
            while (blDR.next())
                lvRecord.Items.Add(new AnalysisModel() {
                    RecordID = blDR.getInt("RecordID"), 
                    TotalScore = blDR.getString("TotalScore"),
                    RecordTime = blDR.getDateTime("RecordTime").ToString("yyyy-MM-dd HH:mm"),
                    Recorder = blDR.getString("Recorder")
                });

            blDR.close();

            if (0 < lvRecord.Items.Count)
                lvRecord.SelectedIndex = lvRecord.Items.Count - 1;
        }

        /// <summary>
        /// 布尔型数据标识
        /// </summary>
        /// <param name="_cb"></param>
        /// <returns></returns>
        private string boolLabel(bool _bl)
        {
            return _bl ? "√" : "□"; 
        }

        /// <summary>
        /// 设置报表变量值
        /// </summary>
        private void setReportValue(string _name, string _value)
        {
            if (null == this.report.GetParameter(_name))
                this.report.SetParameterValue(_name, _value);
            else
                this.report.GetParameter(_name).Value = _value;
        }

        /// <summary>
        /// 显示评价记录
        /// </summary>
        /// <param name="_rcd"></param>
        private void showRecord(AnalysisModel _rcd)
        {
            IDataReader dr = null;
            BLPublic.BLDataReader blDR = null;
            if (null == this.report)
            {
                this.report = new Report();
                this.report.Load(@"report.frx");

                if (AppConst.db.GetRecordSet(SQL.SEL_ANALYSIS_DOCITEM, ref dr))
                {
                    blDR = new BLPublic.BLDataReader(dr);
                    while (blDR.next()) 
                        this.report.SetParameterValue(blDR.getString("DataItemCode").ToLower(), blDR.getString("DefaultValue")); 

                    blDR.close();
                }
                else
                {
                    BLPublic.Dialogs.Error("评价文档默认值失败:" + AppConst.db.Error); 
                    return;
                }

                this.report.SetParameterValue("isman", boolLabel(false));
                this.report.SetParameterValue("iswoman", boolLabel(false)); 
            }

            setReportValue("recorder", _rcd.Recorder);
            setReportValue("recordtime", _rcd.RecordTime);

            if (AppConst.db.GetRecordSet(string.Format(SQL.SEL_PNTINFO, this.patient.PatientCode), ref dr))
            {
                blDR = new BLPublic.BLDataReader(dr);
                if (blDR.next())
                {
                    string sex = blDR.getString("Sex");
                    this.report.GetParameter("patient.roomname").Value = blDR.getString("DeptName");
                    this.report.GetParameter("patient.bedno").Value = blDR.getString("BedNo");
                    this.report.GetParameter("patient.inhospitalno").Value = blDR.getString("HospitalNo");
                    this.report.GetParameter("patient.patientname").Value = blDR.getString("PatientName");
                    this.report.GetParameter("isman").Value = boolLabel("男".Equals(sex) || "m".Equals(sex));
                    this.report.GetParameter("iswoman").Value = boolLabel("女".Equals(sex) || "f".Equals(sex));
                }

                blDR.close();
            }

            if (AppConst.db.GetRecordSet(string.Format(SQL.SEL_RCDITEMS, _rcd.RecordID), ref dr))
            {
                blDR = new BLPublic.BLDataReader(dr); 
                while (blDR.next())
                    setReportValue(blDR.getString("DataItemCode").ToLower(), blDR.getString("ItemValue")); 

                blDR.close();
            }
             
            this.frmRpt.showReport(this.report);
        }

        private void lvRecord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null != lvRecord.SelectedItem)
                showRecord((AnalysisModel)lvRecord.SelectedItem); 
            
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            if (null == this.patient)
            {
                BLPublic.Dialogs.Alert("请选择患者.");
                return;
            }

            WinEditAnalysis edt = new WinEditAnalysis();
            edt.init(this.patient.PatientCode, 0);
            if (true == edt.ShowDialog())
                if (null != this.patient)
                    listRecord(this.patient.PatientCode);

            edt = null;
        }

        private void Mod_Click(object sender, RoutedEventArgs e)
        {
            if (null == lvRecord.SelectedItem)
            {
                BLPublic.Dialogs.Alert("请选评价记录.");
                lvRecord.Focus();
                return;
            }

            WinEditAnalysis edt = new WinEditAnalysis();
            edt.init(this.patient.PatientCode, ((AnalysisModel)lvRecord.SelectedItem).RecordID); 
            if (true == edt.ShowDialog())
                showRecord((AnalysisModel)lvRecord.SelectedItem);

            edt = null;
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            if (null == lvRecord.SelectedItem)
            {
                BLPublic.Dialogs.Alert("请选择评估记录.");
                lvRecord.Focus();
                return;
            }

            this.frmRpt.print();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            if (null == lvRecord.SelectedItem)
            {
                BLPublic.Dialogs.Alert("选择要删除记录.");
                lvRecord.Focus();
                return;
            }
            if (BLPublic.Dialogs.Yes != BLPublic.Dialogs.Ask("是否确定删除所选记录?")) 
                lvRecord.Focus();

            AnalysisModel rcd = (AnalysisModel)lvRecord.SelectedItem;

            if (AppConst.db.ExecSQL(string.Format(SQL.DEL_RCD + SQL.DEL_RCD_ITEM_BYRCD, rcd.RecordID)))
            {
                lvRecord.Items.RemoveAt(lvRecord.SelectedIndex);
                this.frmRpt.showReport(null);

                BLPublic.Dialogs.Info("删除成功.");
            }
            else
                BLPublic.Dialogs.Error("删除失败:" + AppConst.db.Error);
        }
    }
}
