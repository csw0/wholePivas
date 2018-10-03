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
    /// MonitorSetWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WinMonitorSet : Window
    { 
        private List<ConditionModel> lstMntCdt = null;
        private List<BLPublic.CodeNameItem> lstTPNItems = null;
        private int selMntID = 0;

        public WinMonitorSet()
        {
            InitializeComponent();

            initComboBox();
        }

        private string ftmAddUnit(string _unit)
        {
            if (string.IsNullOrWhiteSpace(_unit))
                return "";
            else 
                return "(" + _unit + ")";
        }


        private void initComboBox()
        {
            IDataReader dr = null;
            if (AppConst.db.GetRecordSet(SQL.SEL_TPNMNT_TYPE, ref dr))
            {
                while (dr.Read())
                    cbbChkTyp.Items.Add(dr["ResultType"].ToString());

                dr.Close();
            }

            if (AppConst.db.GetRecordSet(SQL.SEL_TPNMNT_REF, ref dr))
            {
                while (dr.Read())
                    cbbRef.Items.Add(dr["MonitorRef"].ToString());

                dr.Close();
            }
        }

        private bool loadMonitorCdt()
        {
            if (null == this.lstMntCdt)
                this.lstMntCdt = new List<ConditionModel>();
            else
                this.lstMntCdt.Clear();

            IDataReader idr = null;
            if (!AppConst.db.GetRecordSet(SQL.SEL_TPNMNT_DIG, ref idr))
            {
                BLPublic.Dialogs.Error("加载审方条件失败:" + AppConst.db.Error);
                return false;
            }

            int index = 0;
            BLPublic.BLDataReader bldr = new BLPublic.BLDataReader(idr);
            ConditionModel cdt = null;

            while (bldr.next())
            {
                cdt = new ConditionModel(bldr.getInt("TPNMonitorID"), bldr.getString("ItemType"), bldr.getString("Code"),
                                         bldr.getString("Name"), "", bldr.getString("Condition"), bldr.getBool("IsIn"));
                cdt.Index = index;

                if ("tpn".Equals(cdt.ItemType) && cdt.ItemName.Contains("()"))
                    cdt.ItemName = cdt.ItemName.Substring(0, cdt.ItemName.IndexOf("()"));
                 
                this.lstMntCdt.Add(cdt);
                index++;
            }
            bldr.close();
            idr = null;

            DataTable tbl = new DataTable();
            //药品审方条件 
            if (AppConst.db.GetRecordSet(SQL.SEL_TPNMNT_DRUG, ref tbl))
            { 
                bldr = new BLPublic.BLDataReader(tbl.CreateDataReader());
                while (bldr.next())
                {
                    this.lstMntCdt.Add(new ConditionModel{
                                         Index = index,
                                         MonitorID = bldr.getInt("TPNMonitorID"),
                                         ItemType = bldr.getString("ItemType"),
                                         ItemCode = bldr.getString("Code"),
                                         ItemName = bldr.getString("Name"),
                                         ConditionType = (bldr.getBool("IsType2") ? "class" : "drug"),
                                         ConditionCode = bldr.getString("Code2"),
                                         Condition = bldr.getString("Condition"),
                                         IsIn = bldr.getBool("IsIn")
                                        });
                    index++;
                }
                bldr.close();
                tbl.Clear();
            }
            else
                BLPublic.Dialogs.Error("加载药品审方失败:" + AppConst.db.Error);

            return true;
        }

        private void loadTPNItems()
        {
            IDataReader dr = null;
            if (!AppConst.db.GetRecordSet(SQL.SEL_TPNTEIM2, ref dr))
            {
                BLPublic.Dialogs.Error("加载TPN项目失败:" + AppConst.db.Error);
                return;
            }

            if (null == this.lstTPNItems)
                this.lstTPNItems = new List<BLPublic.CodeNameItem>();

            BLPublic.BLDataReader bldr = new BLPublic.BLDataReader(dr);
            while (bldr.next())
                this.lstTPNItems.Add(new BLPublic.CodeNameItem(bldr.getString("TPNItemID"),
                                     bldr.getString("ItemName") + ftmAddUnit(bldr.getString("Unit"))));

            bldr.close(); 
        }

        private string getMonitorCdt(int _mntID)
        {
            string result = "";
            string link = "";
            foreach(ConditionModel c in this.lstMntCdt)
                if (_mntID == c.MonitorID)
                {
                    if (!string.IsNullOrWhiteSpace(result))
                        result += ",";

                    if ("icd".Equals(c.ItemType) || "tpnsaf".Equals(c.ItemType))
                        link = c.IsIn ? "有" : "无";

                    else if (c.IsDrug)
                        link = c.IsIn ? "⊂" : "⊄";

                    else
                        link = c.IsIn ? "∈" : "∉";

                    result += string.Format("{{{0} {1} {2}}}", c.ItemName, link, c.Condition);
                }

            return result;
        }

        private void loadMonitor()
        {
            IDataReader dr = null;
            if (!AppConst.db.GetRecordSet(SQL.SEL_TPNMNTSET, ref dr))
            {
                BLPublic.Dialogs.Error("加载审方记录失败:" + AppConst.db.Error);
                return;
            }

            while (dr.Read())
            {
                lvMonitor.Items.Add(new MonitorModel {
                                        MonitorID = dr.GetInt32(0),
                                        Condition = getMonitorCdt(dr.GetInt32(0)),
                                        ResultType = dr["ResultType"].ToString(),
                                        ResultDesc = dr["ResultDesc"].ToString(),
                                        Degress = dr["ResultLevel"].ToString() + "星",
                                        RefName = dr["MonitorRef"].ToString(),
                                        SetTime = dr["InputTime"].ToString(),
                                        Setter = dr["Inputer"].ToString(),
                                        IsUse = (bool)dr["IsUse"]
                                        });
            }

            dr.Close(); 
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            loadTPNItems();
            if (loadMonitorCdt())
                loadMonitor();

            clearInput();
        }

        private void addNullCondition()
        { 
            ConditionModel cdt = new ConditionModel(0, "", "", "", "", "", true);
            cdt.Index = lvCondition.Items.Count;
            lvCondition.Items.Add(cdt);
        }

        private void delCondition(int _indx)
        {
            ConditionModel sel = null;

            foreach (ConditionModel c in lvCondition.Items)
                if (c.Index == _indx)
                {
                    sel = c;
                    break;
                }

            if (null == sel)
                return;

            if (BLPublic.Dialogs.Yes != BLPublic.Dialogs.Ask2("是否确定删除?"))
                return;

            if ((0 > sel.MonitorID) && (sel == lvCondition.Items[lvCondition.Items.Count - 1])) //最后行不可删除
                return;
            else
                lvCondition.Items.Remove(sel);
        }

        private void setCondition(string _type, BLPublic.CodeNameItem _cnt, int _index, bool _isItem)
        {
            if (null == _cnt)
                return; 

            foreach (ConditionModel c in lvCondition.Items)
                if (c.Index == _index)
                {
                    if (_isItem)
                    {
                        c.ItemType = _type;
                        c.ItemCode = _cnt.Code;
                        c.ItemName = _cnt.Name;
                        if (c == lvCondition.Items[lvCondition.Items.Count - 1])
                            addNullCondition();
                    }
                    else
                    {
                        c.ConditionType = _type;
                        c.ConditionCode = _cnt.Code;
                        c.Condition = _cnt.Name;
                    }

                    break;
                }

            lvCondition.Items.Refresh();
        }

        private void clearInput()
        {
            this.selMntID = 0;
            lvCondition.Items.Clear();
            addNullCondition();

            cbbChkTyp.Text = "";
            rdo3Degress.IsChecked = true;
            txtResultDesc.Text = "结果描述";
            cbbRef.Text = "";
        }
         
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            clearInput();
        }

        private void Mod_Click(object sender, RoutedEventArgs e)
        {
            if (null == lvMonitor.SelectedItem)
            {
                BLPublic.Dialogs.Alert("选择要修改的审方记录.");
                lvMonitor.Focus();
                return;
            }

            MonitorModel mnt = (MonitorModel)lvMonitor.SelectedItem;
            this.selMntID = mnt.MonitorID;

            lvCondition.Items.Clear();
            foreach (ConditionModel c in this.lstMntCdt)
                if (this.selMntID == c.MonitorID)
                {
                    lvCondition.Items.Add(c);
                }

            addNullCondition();
             
            cbbChkTyp.Text = mnt.ResultType;
            txtResultDesc.Text = mnt.ResultDesc;
            cbbRef.Text = mnt.RefName;

            if (rdo5Degress.Content.ToString().Equals(mnt.Degress))
                rdo5Degress.IsChecked = true;
            else
                rdo3Degress.IsChecked = true;

        }

        private void Del_Click(object sender, RoutedEventArgs e)
        { 
            if (null == lvMonitor.SelectedItem)
            {
                BLPublic.Dialogs.Alert("选择要删除的审方记录.");
                lvMonitor.Focus();
                return;
            }

            if (BLPublic.Dialogs.Yes != BLPublic.Dialogs.Ask2("是否确定删除所选审方记录?"))
                return;
             
            MonitorModel mnt = (MonitorModel)lvMonitor.SelectedItem;
            if (!AppConst.db.ExecSQL(string.Format(SQL.DEL_TPNMNT, mnt.MonitorID)))
            {
                BLPublic.Dialogs.Error("删除审方记录失败:" + AppConst.db.Error);
                return;
            }
             
            lvMonitor.Items.Remove(mnt);
            if (this.selMntID == mnt.MonitorID)
                clearInput();

            BLPublic.Dialogs.Info("删除成功");
        }

        private void btnSelDrug_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            btn.ContextMenu.Tag = btn.Tag;
            btn.ContextMenu.IsOpen = true; 
        }

        private void btnOp_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if ("-".Equals(btn.Content))
            {
                delCondition(Convert.ToInt32(btn.Tag.ToString()));
                return;
            }
            else if (null == btn.Tag)
                return;

            if (10 < lvCondition.Items.Count)
            {
                BLPublic.Dialogs.Alert("条件不能超过10个.");
                return;
            }

            btn.ContextMenu.Tag = btn.Tag;
            btn.ContextMenu.IsOpen = true; 
        }

        private void miTPNItem_Click(object sender, RoutedEventArgs e)
        {
            WinList list = new WinList();
            list.Owner = this;
            list.init(this.lstTPNItems, delegate(object _sel)
                          {
                            if (null != _sel)
                                setCondition("tpn", (BLPublic.CodeNameItem)_sel, getMenuIndex(sender), true); 
                          });
            list.Show();
        }

        private void miICD_Click(object sender, RoutedEventArgs e)
        {
            WinListFind list = new WinListFind();
            list.Owner = this;
            list.Title = "诊断选择";
            list.init(SQL.SEL_DIG, 
                        delegate(object _sel)
                        {
                            if (null != _sel)
                                setCondition("icd", (BLPublic.CodeNameItem)_sel, getMenuIndex(sender), true);
                        });
            list.Show();
        }

        private void selDrug(int _index, bool _isItem)
        {
            WinListFind list = new WinListFind();
            list.Owner = this;
            list.Title = "药品";
            list.init(SQL.SEL_PREP, 
                      delegate(object _sel)
                    {
                        if (null != _sel)
                            setCondition("drug", (BLPublic.CodeNameItem)_sel, _index, _isItem);
                    });
            list.Show();
        }

        private void selDrugClass(int _index, bool _isItem)
        {
            WinListFind list = new WinListFind();
            list.Owner = this;
            list.Title = "药品分类";
            list.init(SQL.SEL_MEDCLASS, 
                        delegate(object _sel)
                        {
                            if (null != _sel)
                                setCondition("class", (BLPublic.CodeNameItem)_sel, _index, _isItem);
                        });
            list.Show();
        }

        private int getMenuIndex(object _mi)
        {
            return Convert.ToInt32(((ContextMenu)((MenuItem)_mi).Parent).Tag.ToString());
        }

        private void miDrug_Click(object sender, RoutedEventArgs e)
        {
            selDrug(getMenuIndex(sender), true); 
        }

        private void miDrugClass_Click(object sender, RoutedEventArgs e)
        {
            selDrugClass(getMenuIndex(sender), true);
        }

        private void miDrug2_Click(object sender, RoutedEventArgs e)
        {
            selDrug(getMenuIndex(sender), false);
        }

        private void miDrugClass2_Click(object sender, RoutedEventArgs e)
        {
            selDrugClass(getMenuIndex(sender), false);
        }

        private void miTPNType_Click(object sender, RoutedEventArgs e)
        {
            List<BLPublic.CodeNameItem> lstSAF = new List<BLPublic.CodeNameItem>();
            lstSAF.Add(new BLPublic.CodeNameItem("sugar", "葡萄糖"));
            lstSAF.Add(new BLPublic.CodeNameItem("AA", "氨基酸"));
            lstSAF.Add(new BLPublic.CodeNameItem("fat", "脂肪乳"));

            WinList list = new WinList();
            list.Owner = this;
            list.init(lstSAF, delegate(object _sel)
            {
                if (null != _sel)
                    setCondition("tpnsaf", (BLPublic.CodeNameItem)_sel, getMenuIndex(sender), true);
            });
            list.Show(); 
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (0 == lvCondition.Items.Count)
            {
                BLPublic.Dialogs.Alert("请输入审方条件.");
                lvCondition.Focus();
                return;
            }
            /*
            foreach (ConditionModel c in lvCondition.Items)
            {
                if (!string.IsNullOrWhiteSpace(c.ItemCode) && string.IsNullOrWhiteSpace(c.Condition))
                {
                    BLPublic.Dialogs.Alert("请输入" + c.ItemName + "审方条件.");
                    lvCondition.Focus();
                    return;
                }
            }*/
            
            if (string.IsNullOrWhiteSpace(cbbChkTyp.Text))
            {
                BLPublic.Dialogs.Alert("请选择或输入审方项目.");
                cbbChkTyp.Focus();
                return;
            }

            string rtDesc = txtResultDesc.Text.Trim();
            if (string.IsNullOrWhiteSpace(rtDesc) || "结果描述".Equals(rtDesc))
            {
                BLPublic.Dialogs.Alert("请输入结果描述.");
                txtResultDesc.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(cbbRef.Text))
            {
                BLPublic.Dialogs.Alert("请选择或输入参考文献.");
                cbbRef.Focus();
                return;
            }

            MonitorModel edtMnt = null;
            if (0 < this.selMntID) //修改记录
            {
                if (!AppConst.db.ExecSQL(string.Format(SQL.MOD_TPNMNT, cbbChkTyp.Text.Trim(),
                        (true == rdo3Degress.IsChecked ? 3 : 5), rtDesc, cbbRef.Text.Trim(), this.selMntID)))
                {
                    BLPublic.Dialogs.Error("修改审方记录失败:" + AppConst.db.Error);
                    return;
                }

                foreach (MonitorModel m in lvMonitor.Items)
                    if (this.selMntID == m.MonitorID)
                    {
                        edtMnt = m;
                        m.ResultType = cbbChkTyp.Text.Trim();
                        m.ResultDesc = rtDesc;
                        m.RefName = cbbRef.Text.Trim();
                        m.Degress = true == rdo3Degress.IsChecked ? rdo3Degress.Content.ToString() : rdo5Degress.Content.ToString();
                    }
            }
            else //增加记录
                if (!AppConst.db.InsertAndGetId(string.Format(SQL.ADD_TPNMNT, cbbChkTyp.Text.Trim(),
                            (true == rdo3Degress.IsChecked ? 3 : 5), rtDesc, cbbRef.Text.Trim(), AppConst.LoginEmpCode),
                     out this.selMntID))
                {
                    BLPublic.Dialogs.Error("增加审方记录失败:" + AppConst.db.Error);
                    return;
                }
                else
                {
                    edtMnt = new MonitorModel
                                {
                                    MonitorID = this.selMntID,
                                    Condition = "",
                                    ResultType = cbbChkTyp.Text.Trim(),
                                    ResultDesc = rtDesc,
                                    RefName = cbbRef.Text.Trim(),
                                    Degress = true == rdo3Degress.IsChecked ? rdo3Degress.Content.ToString() : rdo5Degress.Content.ToString(),
                                    SetTime = DateTime.Now.ToLongDateString(),
                                    Setter = AppConst.LoginEmpCode
                                };

                    lvMonitor.Items.Add(edtMnt);
                }
            

            string sql = string.Format(SQL.DEL_TPNMNT_CDT, this.selMntID.ToString());
            foreach (ConditionModel c in lvCondition.Items) 
                if (!string.IsNullOrWhiteSpace(c.ItemCode))
                {
                    sql += ";";
                    if ("tpn".Equals(c.ItemType))
                        sql += string.Format(SQL.ADD_TPNMNT_TPN, this.selMntID, c.ItemCode, c.Condition,
                                             BLPublic.Utils.bool2Bit(c.IsIn));

                    else if ("icd".Equals(c.ItemType))
                        sql += string.Format(SQL.ADD_TPNMNT_DIG, this.selMntID, c.ItemCode, c.ItemName,
                                             BLPublic.Utils.bool2Bit(c.IsIn));

                    else if ("drug".Equals(c.ItemType) || "class".Equals(c.ItemType))
                        sql += string.Format(SQL.ADD_TPNMNT_DRUG, this.selMntID, c.ItemCode, 
                                              BLPublic.Utils.bool2Bit("class".Equals(c.ItemType)),
                                              c.ConditionCode, BLPublic.Utils.bool2Bit("class".Equals(c.ConditionType)), 
                                              BLPublic.Utils.bool2Bit(c.IsIn));
                } 

            if (!AppConst.db.ExecSQL(sql))
            {
                BLPublic.Dialogs.Error("修改审方条件失败:" + AppConst.db.Error);
                return;
            }

            foreach (ConditionModel c in lvCondition.Items)
                if (!string.IsNullOrWhiteSpace(c.ItemCode))
                    if (0 == c.MonitorID)
                    {
                        c.MonitorID = this.selMntID;
                        this.lstMntCdt.Add(c);
                    }

            edtMnt.Condition = getMonitorCdt(this.selMntID);
            lvMonitor.Items.Refresh();

            BLPublic.Dialogs.Info("保存成功");

            if (0 > cbbChkTyp.SelectedIndex)
                cbbChkTyp.Items.Add(cbbChkTyp.Text);

            if (0 > cbbRef.SelectedIndex)
                cbbRef.Items.Add(cbbRef.Text);
             
            clearInput();
        }

        private void lvCondition_LostFocus(object sender, RoutedEventArgs e)
        {
            ((ListBox)sender).SelectedIndex = -1;
        }

        private void lvCondition_HeaderClick(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            if ("?".Equals(headerClicked.Column.Header.ToString()))
                BLPublic.Dialogs.Info("条件值关系\r\n\r\n" +
                                      "∈:勾选时   - TPN项目值在条件值值域里;\r\n" +
                                      "   不勾选时 - TPN项目值不在条件值值域里时.\r\n\r\n" +
                                      "有:勾选时   - 患者存在此诊断;\r\n" +
                                      "   不勾选时 - 患者不存在此诊断.\r\n\r\n" +
                                      "⊂:勾选时   - 两个药品必须一起使用;\r\n" +
                                      "   不勾选时 - 两个药品不能一起使用.\r\n");
        }

        private void lvMonitor_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (null != lvMonitor.SelectedItem)
                Mod_Click(sender, null);
        }

        private void cbUse_Checked(object sender, RoutedEventArgs e)
        {
            if (null == ((CheckBox)sender).Tag)
                return;

            int mntID = Convert.ToInt32(((CheckBox)sender).Tag.ToString());

            if (!AppConst.db.ExecSQL(string.Format(SQL.MOD_TPNMNT_USE, mntID, 1)))
                BLPublic.Dialogs.Error("启用审方失败:" + AppConst.db.Error);  
        }

        private void cbUse_Unchecked(object sender, RoutedEventArgs e)
        {
            if (null == ((CheckBox)sender).Tag)
                return;

            int mntID = Convert.ToInt32(((CheckBox)sender).Tag.ToString());

            if (!AppConst.db.ExecSQL(string.Format(SQL.MOD_TPNMNT_USE, mntID, 0))) 
                BLPublic.Dialogs.Error("停用审方失败:" + AppConst.db.Error);  
        }
    }
}
