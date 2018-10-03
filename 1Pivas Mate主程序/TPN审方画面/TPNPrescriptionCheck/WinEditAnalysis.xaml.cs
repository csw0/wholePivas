using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TPNReview
{
    /// <summary>
    /// EditAnalysis.xaml 的交互逻辑
    /// </summary>
    public partial class WinEditAnalysis : Window
    {
        private string patientCode = null;
        private int editID = 0;
        private List<string> lstCheckItem = null;

        public WinEditAnalysis()
        {
            InitializeComponent();

            txtRecorder.Text = AppConst.LoginEmpCode;
            txtRecordTime.Text = DateTime.Now.ToString("yyyy-M-d H:m");

            this.lstCheckItem = new List<string>(8);
        }

        public void init(string _pcode, int _editID)
        {
            this.patientCode = _pcode;
            this.editID = _editID;
        }

        public int EditID { get { return this.editID; } }

        private PatientModel getPatientInfo()
        {
            if (string.IsNullOrWhiteSpace(this.patientCode))
                return null;

            PatientModel pnt = null;
            IDataReader dr = null;
            //体格检查
            if (AppConst.db.GetRecordSet(string.Format(SQL.SEL_PNTINFO, this.patientCode), ref dr))
            {
                BLPublic.BLDataReader blDR = new BLPublic.BLDataReader(dr);
                if (blDR.next())
                {
                    string ageStr = "";
                    if (!blDR.isNull("Birthday"))
                        ageStr = BLPublic.Utils.getAgeStr(blDR.getDateTime("Birthday"));

                    pnt = new PatientModel()
                    {
                        WardName = blDR.getString("DeptName"),
                        BedNo = blDR.getString("BedNo"),
                        HospitalNo = blDR.getString("HospitalNo"),
                        PatientName = blDR.getString("PatientName"),
                        Age = ageStr,
                        Sex = "f".Equals(blDR.getString("Sex")) ? "女" : "男"
                    };
                }

                blDR.close();
            }

            return pnt;
        }

        private int getScore(TextBlock _txt)
        {
            if (null == _txt)
                return 0;
            else if (string.IsNullOrWhiteSpace(_txt.Text))
                return 0;
            else
                return Convert.ToInt32(_txt.Text);
        }

        private void refScore()
        {
            int score = getScore(txtDisTotal) + getScore(txtYYTotal);
            if (true == cbAge.IsChecked)
                score++;

            if (0 < score)
            {
                txtTotal.Text = score.ToString();
                cbGt3.IsChecked = (3.0 <= score);
                cbLt3.IsChecked = false == cbGt3.IsChecked;
            }
            else
            {
                txtTotal.Text = "";
                cbGt3.IsChecked = false;
                cbLt3.IsChecked = false;
            }
        }

        private string check2Label(CheckBox _cb)
        {
            if ((null == _cb) || (false == _cb.IsChecked))
                return "";
            else
                return "√";
        }

        private bool checkItem(Panel _pnl, string _itemCode)
        { 
            CheckBox cb = null;
            foreach (UIElement el in _pnl.Children)
                if (el is CheckBox)
                {
                    cb = (CheckBox)el;
                    if ((null != cb.Tag) && cb.Tag.Equals(_itemCode))
                    {
                        cb.IsChecked = true;
                        return true;
                    }
                }

            return false;
        }

        private void checkItem(string _itemCode)
        {
            if(!checkItem(grdDisease, _itemCode))
                checkItem(grdNutrition, _itemCode); 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PatientModel pnt = getPatientInfo();
            if (null == pnt)
                return;

            if (0 < this.editID) 
            {
                IDataReader dr = null;
                BLPublic.BLDataReader blDR = null;
                if (AppConst.db.GetRecordSet(string.Format(SQL.SEL_RCD, this.editID), ref dr))
                {
                    blDR = new BLPublic.BLDataReader(dr);
                    if (blDR.next())
                    {
                        txtRecorder.Text = blDR.getString("EmployeeCode");
                        txtRecordTime.Text = blDR.getDateTime("RecordTime").ToString("yyyy-M-d H:m"); 
                    }

                    blDR.close();
                }

                if (AppConst.db.GetRecordSet(string.Format(SQL.SEL_RCDITEMS, this.editID), ref dr))
                {
                    blDR = new BLPublic.BLDataReader(dr);
                    string itemCode = "";
                    while (blDR.next())
                    {
                        itemCode = blDR.getString("DataItemCode").ToLower();

                        if ("patient.age".Equals(itemCode))
                            pnt.Age = blDR.getString("ItemValue");
                        else if ("patient.height".Equals(itemCode))
                            pnt.Height = blDR.getString("ItemValue");
                        else if ("patient.weight".Equals(itemCode))
                            pnt.Weight = blDR.getString("ItemValue");
                        else if ("bmi".Equals(itemCode))
                            pnt.BMI = blDR.getString("ItemValue");
                        else if ("albumin".Equals(itemCode))
                            pnt.Albumin = blDR.getString("ItemValue");
                        else if ("diagnose".Equals(itemCode))
                            pnt.Diagnose = blDR.getString("ItemValue");

                        else if ("remark".Equals(itemCode))
                            txtRemark.Text = blDR.getString("ItemValue");
                        else if ("disease.score".Equals(itemCode))
                            txtDisTotal.Text = blDR.getString("ItemValue");
                        else if ("nutrition.score".Equals(itemCode))
                            txtYYTotal.Text = blDR.getString("ItemValue");
                        else if ("age70".Equals(itemCode))
                            cbAge.IsChecked = "√".Equals(blDR.getString("ItemValue"));
                        else if ("gtscore3".Equals(itemCode))
                            cbGt3.IsChecked = "√".Equals(blDR.getString("ItemValue"));
                        else if ("ltscore3".Equals(itemCode))
                            cbLt3.IsChecked = "√".Equals(blDR.getString("ItemValue"));
                        else if ("total.score".Equals(itemCode))
                            txtTotal.Text = blDR.getString("ItemValue");
                        else
                            checkItem(itemCode);
                    }

                    blDR.close();
                }
            }

            grdPatient.DataContext = pnt;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.patientCode))
            {
                BLPublic.Dialogs.Alert("没有患者信息");
                return;
            }

            PatientModel pnt = (PatientModel)grdPatient.DataContext;
            string ID = "@RcdID";
            if (0 < this.editID)
                ID = this.editID.ToString();

            string sql = string.Format(SQL.ADD_RCD_ITEM, ID, "patient.age", pnt.Age);
            sql += string.Format(SQL.ADD_RCD_ITEM, ID, "patient.height", pnt.Height);
            sql += string.Format(SQL.ADD_RCD_ITEM, ID, "patient.weight", pnt.Weight);
            sql += string.Format(SQL.ADD_RCD_ITEM, ID, "BMI", pnt.BMI);
            sql += string.Format(SQL.ADD_RCD_ITEM, ID, "albumin", pnt.Albumin);
            sql += string.Format(SQL.ADD_RCD_ITEM, ID, "diagnose", pnt.Diagnose);
            sql += string.Format(SQL.ADD_RCD_ITEM, ID, "remark", txtRemark.Text);
            sql += string.Format(SQL.ADD_RCD_ITEM, ID, "disease.score", txtDisTotal.Text);
            sql += string.Format(SQL.ADD_RCD_ITEM, ID, "nutrition.score", txtYYTotal.Text);
            sql += string.Format(SQL.ADD_RCD_ITEM, ID, "total.score", txtTotal.Text);
            if (true == cbAge.IsChecked)
                sql += string.Format(SQL.ADD_RCD_ITEM, ID, "age70", check2Label(cbAge));
            if (true == cbGt3.IsChecked)
                sql += string.Format(SQL.ADD_RCD_ITEM, ID, "gtscore3", check2Label(cbGt3));
            if (true == cbLt3.IsChecked)
                sql += string.Format(SQL.ADD_RCD_ITEM, ID, "ltscore3", check2Label(cbLt3));

            foreach (string item in this.lstCheckItem)
                sql += string.Format(SQL.ADD_RCD_ITEM, ID, item, "√");


            if (0 < this.editID)
                sql = string.Format(SQL.DEL_RCD_ITEM_BYRCD, this.editID) + sql;
            else
                sql = string.Format(SQL.ADD_RCD, this.patientCode, AppConst.LoginEmpCode) + sql;

            if (AppConst.db.ExecSQL(sql))
            {
                this.DialogResult = true;
                this.Close();
            }
            else
                BLPublic.Dialogs.Error("保存评价失败:" + AppConst.db.Error);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender; 

            if ((cbAge != cb) && (null != cb.Tag))
            {
                foreach (UIElement el in ((Grid)cb.Parent).Children)
                    if (el is CheckBox)
                        if (el != cb)
                            ((CheckBox)el).IsChecked = false;

                string item = cb.Tag.ToString();

                if (!this.lstCheckItem.Contains(item))
                    this.lstCheckItem.Add(item);

                int p = item.IndexOf('.');
                if (0 < p)
                {
                    int s = Convert.ToInt32(item.Substring(p+1));
                    item = item.Substring(0, p);

                    if ("disease".Equals(item))
                        txtDisTotal.Text = (0 == s ? "" : s.ToString());
                    else if ("nutrition".Equals(item))
                        txtYYTotal.Text = (0 == s ? "" : s.ToString()); 
                }

            }

            refScore();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;

            if ((cbAge != cb) && (null != cb.Tag))
            {
                string item = cb.Tag.ToString();
                if (this.lstCheckItem.Contains(item))
                    this.lstCheckItem.Remove(item);

                int p = item.IndexOf('.');
                if (0 < p)
                { 
                    item = item.Substring(0, p);

                    if ("disease".Equals(item))
                        txtDisTotal.Text = "";
                    else if ("nutrition".Equals(item))
                        txtYYTotal.Text = ""; 
                }
            }

            refScore(); 
        }
    }
}
