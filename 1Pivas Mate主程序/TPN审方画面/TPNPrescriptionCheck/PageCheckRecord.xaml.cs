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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TPNReview
{
    /// <summary>
    /// PageCheckRecord.xaml 的交互逻辑
    /// </summary>
    public partial class PageCheckRecord : Page, IContentPage
    {
        private PatientModel patient = null;
        private Action onRefParent = null;

        public PageCheckRecord()
        {
            InitializeComponent();
             
            //cbbChkType.Items.Add(new BLPublic.CodeNameItem("all", "<全部>"));
            cbbChkType.Items.Add(new BLPublic.CodeNameItem(WinAddCheck.CT_LISCHK, "实验室检查"));
            cbbChkType.Items.Add(new BLPublic.CodeNameItem(WinAddCheck.CT_BODYCHK, "体格检查"));
            //cbbChkType.Items.Add(new BLPublic.CodeNameItem("virus", "细菌检查"));

            cbbChkType.SelectedIndex = 0;

            dpSDate.SelectedDate = DateTime.Now;
            dpEDate.SelectedDate = DateTime.Now;
        }
        public void init(Action _refParent)
        {
            this.onRefParent = _refParent;
        }
        public void setPatient(PatientModel _pnt)
        {
            this.patient = _pnt;

            if (null != this.patient)
                listRecord(this.patient.PatientCode);
        }

        public void clear()
        {
            lvRecord.Items.Clear();
            lvItems.Items.Clear();
        }

        /// <summary>
        /// 获取检查类型
        /// </summary>
        /// <param name="_code">类型编码</param>
        /// <returns></returns>
        public static string getChkTypeName(string _code)
        {
            if (WinAddCheck.CT_LISCHK.Equals(_code))
                return "实验室检查";

            else if (WinAddCheck.CT_BODYCHK.Equals(_code))
                return "体格检查";

            else if (WinAddCheck.CT_VIRUSCHK.Equals(_code))
                return "细菌检查";

            else
                return "<未知>";
        }

        private void listRecord(string _pcode)
        {
            IDataReader dr = null;
            string sql = "";
            string type = WinAddCheck.CT_LISCHK;
            if (null != cbbChkType.SelectedItem)
                type = ((BLPublic.CodeNameItem)cbbChkType.SelectedItem).Code;

            sql = string.Format(SQL.SEL_CHKRCD_BYTYPE, _pcode, dpSDate.SelectedDate.Value.ToShortDateString(),
                                dpEDate.SelectedDate.Value.ToShortDateString(), type); 
            sql += " ORDER BY CheckTime"; 

            if (AppConst.db.GetRecordSet(sql, ref dr))
            {
                while (dr.Read())
                    lvRecord.Items.Add(new CheckRecord(dr["CheckRecordNo"].ToString(), dr["CheckType"].ToString(),
                        getChkTypeName(dr["CheckType"].ToString()), dr["CheckName"].ToString(), 
                        Convert.ToDateTime(dr["CheckTime"].ToString()), dr["CheckerCode"].ToString()));

                dr.Close();
            }
            else
                BLPublic.Dialogs.Error("加载检查记录失败:" + AppConst.db.Error); 
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            clear();
            if (null != this.patient)
                listRecord(this.patient.PatientCode);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (null == this.patient)
            {
                BLPublic.Dialogs.Alert("请选择患者");
                return;
            }

            WinAddCheck addChk = new WinAddCheck(); 
            addChk.init(this.patient.PatientCode);
            if (true == addChk.ShowDialog())
                if (null != this.patient)
                    listRecord(this.patient.PatientCode);
        }

        private void lvRecord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvItems.Items.Clear();
            if (null == lvRecord.SelectedItem)
                return;

            CheckRecord chkRcd = (CheckRecord)lvRecord.SelectedItem;
            string sql = "";

            if (WinAddCheck.CT_LISCHK.Equals(chkRcd.TypeCode))
                sql = SQL.SEL_LISCHK_BYNO;

            else if (WinAddCheck.CT_BODYCHK.Equals(chkRcd.TypeCode))
                sql = SQL.SEL_BODYCHK_BYNO;

            else if (WinAddCheck.CT_VIRUSCHK.Equals(chkRcd.TypeCode))
                sql = SQL.SEL_VIRUSCHK_BYNO;

            else
                return;

            sql = string.Format(sql, chkRcd.CheckTime.Substring(0, 10), chkRcd.RecordNo);

            IDataReader dr = null;
            int i = 1;
            
            if (AppConst.db.GetRecordSet(sql, ref dr))
            {
                while (dr.Read())
                    lvItems.Items.Add(new CheckResult(i++, dr["ResultID"].ToString(), dr["Code"].ToString(), 
                        dr["Name"].ToString(), dr["ResultValue"].ToString(), dr["ResultUnit"].ToString(),
                        dr["Domain"].ToString(), dr["ValueDrect"].ToString()));

                dr.Close();
            }
            else
                BLPublic.Dialogs.Error("加载检查内容失败:" + AppConst.db.Error); 
        }

        private void miDel_Click(object sender, RoutedEventArgs e)
        {
            if (null == lvRecord.SelectedItem)
            {
                BLPublic.Dialogs.Alert("请选择要删除的记录.");
                lvRecord.Focus();
                return;
            }

            CheckRecord chkRcd = (CheckRecord)lvRecord.SelectedItem;
            if (!"手工录入".Equals(chkRcd.CheckName))
            {
                BLPublic.Dialogs.Alert("非手工录入，不可删除.");
                lvRecord.Focus();
                return;
            }

            if (BLPublic.Dialogs.Yes != BLPublic.Dialogs.Ask2("是否确定删除所选记录?"))
                return;

            if (AppConst.db.ExecSQL(string.Format(SQL.DEL_CHK_RCD, chkRcd.RecordNo)))
            {
                BLPublic.Dialogs.Info("删除成功");
                lvRecord.Items.RemoveAt(lvRecord.SelectedIndex);
                lvItems.Items.Clear();
            }
            else
                BLPublic.Dialogs.Error("删除失败:" + AppConst.db.Error);
        }

        private void miDelRt_Click(object sender, RoutedEventArgs e)
        {
            if (null == lvRecord.SelectedItem)
            {
                BLPublic.Dialogs.Alert("请选择要删除的记录.");
                lvRecord.Focus();
                return;
            }

            if (null == lvItems.SelectedItem)
            {
                BLPublic.Dialogs.Alert("请选择要删除的记录.");
                lvItems.Focus();
                return;
            }

            CheckRecord chkRcd = (CheckRecord)lvRecord.SelectedItem;
            if (!"手工录入".Equals(chkRcd.CheckName))
            {
                BLPublic.Dialogs.Alert("非手工录入，不可删除.");
                lvRecord.Focus();
                return;
            }

            if (BLPublic.Dialogs.Yes != BLPublic.Dialogs.Ask2("是否确定删除所选记录?"))
                return;

            string sql = "";
            if (chkRcd.CheckType.Contains("体格检查"))
                sql = SQL.DEL_BODYCHK;
            else
                sql = SQL.DEL_LISCHK;

            if (AppConst.db.ExecSQL(string.Format(sql, chkRcd.RecordNo, ((CheckResult)lvItems.SelectedItem).ResultID)))
            {
                BLPublic.Dialogs.Info("删除成功");
                lvItems.Items.RemoveAt(lvItems.SelectedIndex);
            }
            else
                BLPublic.Dialogs.Error("删除失败:" + AppConst.db.Error);
        }
    }
}
