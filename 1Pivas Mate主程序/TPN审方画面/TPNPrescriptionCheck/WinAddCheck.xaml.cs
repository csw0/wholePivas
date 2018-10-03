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
    /// AddCheckWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WinAddCheck : Window
    {
        public const string CT_LISCHK = "lischk";
        public const string CT_BODYCHK = "bodychk";
        public const string CT_VIRUSCHK = "viruschk";

        private string patientCode = "";

        public WinAddCheck()
        {
            InitializeComponent();

            dpDate.SelectedDate = DateTime.Now;
            tpTime.Value = DateTime.Now;
            txtItem.Text = "";
            txtUnit.Text = "";
            txtChecker.Text = AppConst.LoginEmpCode;
        }

        public void init(string _pcode)
        {
            this.patientCode = _pcode;
        }

        private void listCheckItems()
        {
            lvItems.Items.Clear();

            IDataReader dr = null;
            string code = "";
            string temp = "";
            int i = -1;
            if (true == rdoBody.IsChecked)
                code = CT_BODYCHK;
            else
                code = CT_LISCHK;
             
            if (AppConst.db.GetRecordSet(string.Format(SQL.SEL_TPNTEIM_BYTYP, code), ref dr))
            {
                while (dr.Read())
                {
                    temp = dr["Express"].ToString();
                    if (string.IsNullOrWhiteSpace(temp) || !temp.Contains("[") || !temp.Contains("]"))
                        lvItems.Items.Add(new CheckItem("", dr["ItemName"].ToString() + " (未匹配)", dr["Unit"].ToString()));
                    else
                    {
                        i = temp.IndexOf("[");
                        temp = temp.Substring(++i);
                        i = temp.IndexOf("]");
                        temp = temp.Substring(0, i);
                        lvItems.Items.Add(new CheckItem(temp, dr["ItemName"].ToString(), dr["Unit"].ToString()));
                    } 
                }

                dr.Close();
            }
            else
                BLPublic.Dialogs.Error("加载检查项目失败:" + AppConst.db.Error);  
        }

        private void listCheckResult()
        {
            lvResult.Items.Clear();

            string itemCode = "";
            foreach (object obj in lvItems.Items)
            {
                if (!string.IsNullOrWhiteSpace(((CheckItem)obj).ItemCode))
                    itemCode += "'" + ((CheckItem)obj).ItemCode + "',";
            }

            if (string.IsNullOrWhiteSpace(itemCode))
                return;

            itemCode = itemCode.Substring(0, itemCode.Length - 1);

            IDataReader dr = null;
            string sql = "";
            if (true == rdoBody.IsChecked)
                sql = SQL.SEL_BODYCHK_BYITEMS;
            else
                sql = SQL.SEL_LISCHK_BYITEMS;

            sql = string.Format(sql, DateTime.Now.ToString("yyyy-MM-dd"), this.patientCode, itemCode);
            sql += " ORDER BY cr.CheckTime";
             
            //体格检查
            if (AppConst.db.GetRecordSet(sql, ref dr))
            { 
                while (dr.Read())
                {
                    lvResult.Items.Add(new CheckResult2(dr["CheckRecordNo"].ToString(), dr["ResultID"].ToString(), 
                                            dr["Code"].ToString(), dr["Name"].ToString(), dr["ResultValue"].ToString(),
                                            dr["ResultUnit"].ToString(), "", "")); 
                }

                dr.Close();
            }
            else
                BLPublic.Dialogs.Error("加载检查记录失败:" + AppConst.db.Error);  
        }


        private void Type_Click(object sender, RoutedEventArgs e)
        {
            foreach (object item in lvResult.Items)
                if (string.IsNullOrWhiteSpace(((CheckResult2)item).ResultID))
                {
                    if (BLPublic.Dialogs.Yes != BLPublic.Dialogs.Ask("当前列表里有未保存的新增记录，是否取消?"))
                        return;
                }

            listCheckItems();
            listCheckResult();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IDataReader dr = null;
            //体格检查
            if (AppConst.db.GetRecordSet(string.Format(SQL.SEL_PNTINFO, this.patientCode), ref dr))
            {
                if (dr.Read())
                    txtPatient.Text = dr["DeptName"].ToString() + "  " + dr["BedNo"].ToString().Trim() + "床  " +
                                      dr["PatientName"].ToString();
                else
                    txtPatient.Text = "信息丢失";

                dr.Close();
            }
            else
                txtPatient.Text = "读取失败:" + AppConst.db.Error;

            listCheckItems();
            listCheckResult();
        }

        private void lvItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (null == lvItems.SelectedItem)
                return;

            CheckItem item = (CheckItem)lvItems.SelectedItem;
            if (string.IsNullOrWhiteSpace(item.ItemCode))
            {
                BLPublic.Dialogs.Alert("请先匹配项目.(在主界面菜单\"TPN项目\"里操作)");
                lvItems.Focus();
                return;
            }

            txtItem.Text = item.ItemName;
            txtUnit.Text = item.Unit;
            txtItem.Tag = item.ItemCode;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (null == txtItem.Tag)
            {
                BLPublic.Dialogs.Alert("请选择检查项目.");
                lvItems.Focus();
                return;
            }

            if (DateTime.MinValue >= dpDate.SelectedDate.Value)
            {
                BLPublic.Dialogs.Alert("请选择检查日期.");
                dpDate.Focus();
                return;
            }

            if (DateTime.MinValue >= tpTime.Value)
            {
                BLPublic.Dialogs.Alert("请输入检查时间.");
                tpTime.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtValue.Text))
            {
                BLPublic.Dialogs.Alert("请输入检查结果.");
                txtValue.Focus();
                return;
            }

            lvResult.Items.Add(new CheckResult2("", "", txtItem.Tag.ToString(), txtItem.Text, txtValue.Text.Trim(),
                                                txtUnit.Text, "", "")); 
            txtItem.Text = "";
            txtValue.Text = "";
            txtUnit.Text = "";
            txtItem.Tag = null;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string chkTyp = ""; 
            string dt = dpDate.SelectedDate.Value.ToShortDateString();
            string addSQL = "";
            string sql = "";
            CheckResult2 chkRT = null;

            dt += " " + tpTime.Text + ":00";
            if (true == rdoBody.IsChecked)
            {
                addSQL = SQL.ADD_PNTBODYCHK;
                chkTyp = CT_BODYCHK;
            }
            else
            {
                addSQL = SQL.ADD_PNTLISCHK;
                chkTyp = CT_LISCHK;
            }

            foreach (object item in lvResult.Items)
            {
                chkRT = (CheckResult2)item;
                if (string.IsNullOrWhiteSpace(chkRT.ResultID))
                    sql += string.Format(addSQL, chkRT.ItemCode, chkRT.ItemName, chkRT.Value, chkRT.Unit, dt);
            }

            if (string.IsNullOrWhiteSpace(sql))
            {
                BLPublic.Dialogs.Info("保存成功");
                return;
            }

            sql = string.Format(SQL.ADD_PNTCHK_1, chkTyp) +
                  string.Format(SQL.ADD_CHK_RCD, this.patientCode, chkTyp, "手工录入", AppConst.LoginEmpCode, dt) +
                  sql;
             
            if (AppConst.db.ExecSQL(sql))
            {
                BLPublic.Dialogs.Info("保存成功");
                listCheckResult();
            }
            else
                BLPublic.Dialogs.Error("添加失败:" + AppConst.db.Error);
        }

        private void DelRt_Click(object sender, RoutedEventArgs e)
        {
            if (null == lvResult.SelectedItem)
            {
                BLPublic.Dialogs.Alert("请选择要删除的检查结果.");
                lvResult.Focus();
                return;
            }

            CheckResult2 item = (CheckResult2)lvResult.SelectedItem;
            if (BLPublic.Dialogs.Yes != BLPublic.Dialogs.Ask2("是否确定删除所选记录?"))
                return;

            string sql = ""; 
            if (true == rdoBody.IsChecked)
                sql = SQL.DEL_BODYCHK;
            else
                sql = SQL.DEL_LISCHK;

            if (AppConst.db.ExecSQL(string.Format(sql, item.CheckRecordNo, item.ResultID)))
            { 
                 BLPublic.Dialogs.Info("删除成功");
                 lvResult.Items.RemoveAt(lvResult.SelectedIndex);
            }
            else
                BLPublic.Dialogs.Error("删除失败:" + AppConst.db.Error);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;

            foreach (object item in lvResult.Items)
                if (string.IsNullOrWhiteSpace(((CheckResult2)item).ResultID))
                {
                    if (BLPublic.Dialogs.Yes != BLPublic.Dialogs.Ask("存在未保存的新增记录，是否确定关闭?"))
                        return;
                }

            e.Cancel = false;
        }
    }
}
