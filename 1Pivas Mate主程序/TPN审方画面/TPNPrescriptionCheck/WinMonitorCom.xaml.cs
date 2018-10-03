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
    /// WinMonitorCom.xaml 的交互逻辑
    /// </summary>
    public partial class WinMonitorCom : Window
    {
        private DataTable tblMonitor = null;

        public WinMonitorCom()
        {
            InitializeComponent(); 
        }

        private void loadMonitor()
        {
            if (null == this.tblMonitor)
                this.tblMonitor = new DataTable();
            else
                this.tblMonitor.Clear();

            if (!AppConst.db.GetRecordSet(SQL.SEL_COMCHECK, ref this.tblMonitor))
            {
                BLPublic.Dialogs.Error("加载通用审核失败:" + AppConst.db.Error);
                return;
            }

            this.tblMonitor.PrimaryKey = new DataColumn[] { this.tblMonitor.Columns["AlwayCheckID"] }; 
            grdMonitor.ItemsSource = this.tblMonitor.DefaultView;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadMonitor();
        }

        private void btnSelDrug_Click(object sender, RoutedEventArgs e)
        {
            WinListFind list = new WinListFind();
            list.Owner = this;
            list.Title = "药品";
            list.init(SQL.SEL_PREP,
                      delegate(object _sel)
                      {
                          if (null != _sel)
                          {
                              BLPublic.CodeNameItem item = (BLPublic.CodeNameItem)_sel;
                              DataRow r = this.tblMonitor.Rows.Find(((Button)sender).Tag.ToString());
                              if (null != r)
                              {
                                  r["UniPreparationID"] = item.Code;
                                  r["Drug"] = item.Name;
                              } 
                          }
                      });
            list.Show();
        }

        private void btnSelItem_Click(object sender, RoutedEventArgs e)
        {
            WinListFind list = new WinListFind();
            list.Owner = this;
            list.Title = "TPN项目";
            list.init(SQL.SEL_TPNTEIM_SEL,
                        delegate(object _sel)
                        {
                            if (null != _sel)
                            {
                                BLPublic.CodeNameItem item = (BLPublic.CodeNameItem)_sel;
                                DataRow r = this.tblMonitor.Rows.Find(((Button)sender).Tag.ToString());
                                if (null != r)
                                {
                                    r["TPNItemID"] = item.Code;
                                    r["ItemName"] = item.Name;
                                } 
                            }
                        });
            list.Show();  
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WinListFind list = new WinListFind();
            list.Owner = this;
            list.Title = "TPN项目";
            list.init(SQL.SEL_TPNTEIM_SEL,
                        delegate(object _sel)
                        {
                            if (null != _sel)
                            {
                                BLPublic.CodeNameItem item = (BLPublic.CodeNameItem)_sel;
                                this.tblMonitor.Rows.Add(new object[] {(this.tblMonitor.Rows.Count + 1) * -1, 
                                    item.Code, 0, "", false, false, false, 0, 
                                    AppConst.LoginEmpCode, DateTime.Now, item.Name, ""});
                            }
                        });
            list.Show();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (null == grdMonitor.SelectedItem)
            {
                BLPublic.Dialogs.Alert("选择要删除的审核.");
                grdMonitor.Focus();
                return;
            }

            DataRowView drv = (DataRowView)grdMonitor.SelectedItem;
            int ID = Convert.ToInt32(drv["AlwayCheckID"].ToString());
            if (0 >= ID)
                this.tblMonitor.Rows.RemoveAt(grdMonitor.SelectedIndex);

            else if (AppConst.db.ExecSQL(string.Format(SQL.DEL_COMCHECK, ID)))
                this.tblMonitor.Rows.RemoveAt(grdMonitor.SelectedIndex);
            
            else
                BLPublic.Dialogs.Error("删除失败:" + AppConst.db.Error);
        }

        private string getRowField(DataRow _r, string _field, string _def="")
        {
            if (null == _r)
                return "";
            else if (_r.IsNull(_field))
                return _def;
            else if (_r[_field] is bool)
                return BLPublic.Utils.bool2Bit((bool)_r[_field]).ToString();
            else
                return _r[_field].ToString();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            int ID = 0;
            foreach(DataRow r in this.tblMonitor.Rows)
            {
                ID = Convert.ToInt32(r["AlwayCheckID"].ToString());
                if (0 >= ID)
                {
                    if (AppConst.db.InsertAndGetId(string.Format(SQL.ADD_COMCHECK, r["TPNItemID"].ToString(),
                            getRowField(r, "UniPreparationID", "0"), r["NormalValue"].ToString(), 
                            getRowField(r, "RequireSugar", "0"), getRowField(r, "RequireAA", "0"), 
                            getRowField(r, "RequireFat", "0"), r["SeqNo"].ToString(), AppConst.LoginEmpCode), 
                            out ID))

                        r["AlwayCheckID"] = ID; 
                    else
                    {
                        BLPublic.Dialogs.Error("新增失败:" + AppConst.db.Error);
                        return;
                    }
                }
                else if (!AppConst.db.ExecSQL(string.Format(SQL.MOD_COMCHECK, r["TPNItemID"].ToString(),
                            getRowField(r, "UniPreparationID", "0"), r["NormalValue"].ToString(),
                            getRowField(r, "RequireSugar", "0"), getRowField(r, "RequireAA", "0"),
                            getRowField(r, "RequireFat", "0"), r["SeqNo"].ToString(), ID)))
                {
                    BLPublic.Dialogs.Error("更新失败:" + AppConst.db.Error);
                    return;
                }
            }

            grdMonitor.Items.Refresh();
            BLPublic.Dialogs.Info("保存成功");
        }
    }
}
