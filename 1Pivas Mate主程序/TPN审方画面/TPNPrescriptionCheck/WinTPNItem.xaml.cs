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
using Xceed.Wpf.Toolkit;

namespace TPNReview
{
    /// <summary>
    /// TPNItemWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WinTPNItem : Window
    {
        private DataTable tblTPNItem = null;    //TPN项目
        private Dictionary<string, string> itemType = null; //项目类型<code, name>
        private Dictionary<string, string> itemName = null; //项目名称<code, name>

        public WinTPNItem()
        {
            InitializeComponent();

            this.itemType = new Dictionary<string, string>();
            this.itemType.Add("collect", "计算项目");
            this.itemType.Add("comp", "营养成分");
            this.itemType.Add("thing", "所含物");
            this.itemType.Add("check", "检查项目");
        }


        private void lstType()
        {
            lbType.Items.Add(new BLPublic.CodeNameItem("ALL", "<全部>"));
            foreach (string typeCode in this.itemType.Keys)
                lbType.Items.Add(new BLPublic.CodeNameItem(typeCode, this.itemType[typeCode]));


            this.tblTPNItem = new DataTable();
            if (!AppConst.db.GetRecordSet(SQL.SEL_TPNTEIM_ALL, ref this.tblTPNItem))
            {
                BLPublic.Dialogs.Error("加载TPN项目失败:" + AppConst.db.Error);
                return;
            }

            this.tblTPNItem.PrimaryKey = new DataColumn[] { this.tblTPNItem.Columns["TPNItemID"] };

            this.itemName = new Dictionary<string, string>();
            this.itemName.Add("patient.age", "患者年龄");
            this.itemName.Add("patient.sex", "患者性别");
            this.itemName.Add("recipe.capacity", "处方容量");
            this.itemName.Add("recipe.AA.capacity", "氨基酸容量");
            this.itemName.Add("recipe.sugar.capacity", "葡萄糖容量");
            this.itemName.Add("recipe.fat.capacity", "脂肪乳容量");

            foreach (DataRow row in this.tblTPNItem.Rows)
                this.itemName.Add(row["ItemCode"].ToString(), row["ItemName"].ToString());

            lbType.SelectedIndex = 0;
            //lstItems("ALL");
        }

        /// <summary>
        /// 格式化表达式，用项目名称代替项目编码
        /// </summary>
        /// <returns></returns>
        private string ftmExpress(string _exp)
        {
            if (string.IsNullOrWhiteSpace(_exp))
                return "";

            string expitem = "";
            string result = "";
            char[] chr = _exp.ToCharArray();
            int i = 0;
            int valStart = -1;

            while (i < chr.Length)
            {
                if ('[' == chr[i])
                {
                    result += chr[i];
                    valStart = i;
                }
                else if (']' == chr[i])
                {
                    if (0 <= valStart)
                    {
                        expitem = _exp.Substring(valStart + 1, i - valStart - 1);
                        if (this.itemName.ContainsKey(expitem))
                            result += this.itemName[expitem];
                        else
                            result += expitem;
                    }

                    result += chr[i];
                    valStart = -1;
                }
                else if (0 > valStart)
                    result += chr[i];

                i++;
            }

            return result;
        }

        private string getTypeName(string _typeCode)
        {
            if ("bodychk".Equals(_typeCode) || "lischk".Equals(_typeCode))
                _typeCode = "check";

            return this.itemType[_typeCode];
        }


        private void lstItems(string _type)
        {
            if ("ALL".Equals(_type))
            {
                lvItem.Items.Clear();
                txtNum.Text = "";
                tpnmonitor.TPNItem item = null;
                foreach (DataRow row in this.tblTPNItem.Rows)
                {
                    item = new tpnmonitor.TPNItem(Convert.ToInt32(row["TPNItemID"].ToString()),
                                        getTypeName(row["ItemType"].ToString()), row["ItemCode"].ToString(),
                                        row["ItemName"].ToString(), ftmExpress(row["Express"].ToString()),
                                        row["Unit"].ToString(),
                                        row.IsNull("SeqNo") ? -1 : Convert.ToInt32(row["SeqNo"].ToString()));
                    item.NormalValue = row["NormalValue"].ToString();
                    lvItem.Items.Add(item);
                }
                txtNum.Text = "项目数:" + lvItem.Items.Count.ToString();
            }
            else
            {
                string select = "";
                if ("check".Equals(_type))
                    select = "ItemType='bodychk' OR ItemType='lischk'";
                else
                    select = "ItemType='" + _type + "'";

                filterItem(select); 
            }
        }

        private void filterItem(string _condition)
        {
            lvItem.Items.Clear();
            txtNum.Text = "";

            tpnmonitor.TPNItem item = null;
            DataRow[] rows = this.tblTPNItem.Select(_condition); 
            foreach(DataRow row in rows)
            {
                item = new tpnmonitor.TPNItem(Convert.ToInt32(row["TPNItemID"].ToString()),
                                    getTypeName(row["ItemType"].ToString()), row["ItemCode"].ToString(),
                                    row["ItemName"].ToString(), ftmExpress(row["Express"].ToString()),
                                    row["Unit"].ToString(),
                                    row.IsNull("SeqNo") ? -1 : Convert.ToInt32(row["SeqNo"].ToString()));
                item.NormalValue = row["NormalValue"].ToString();
                lvItem.Items.Add(item);
            }

             /*   lvItem.Items.Add(new tpnmonitor.TPNItem(Convert.ToInt32(row["TPNItemID"].ToString()),
                                                getTypeName(row["ItemType"].ToString()), row["ItemCode"].ToString(),
                                                row["ItemName"].ToString(), ftmExpress(row["Express"].ToString()), 
                                                row["Unit"].ToString(),
                                                row.IsNull("SeqNo") ? -1 : Convert.ToInt32(row["SeqNo"].ToString())));
            */
            txtNum.Text = "项目数:" + lvItem.Items.Count.ToString();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            lstType();
        }

        private void lbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            if (null != lbType.SelectedItem) 
                lstItems(((BLPublic.CodeNameItem)lbType.SelectedItem).Code); 
        }

        private void lvItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnSet.IsEnabled = false;
            if (null != lvItem.SelectedItem)
                btnSet.IsEnabled = "检查项目".Equals(((tpnmonitor.TPNItem)lvItem.SelectedItem).Type);
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        { 
            if (string.IsNullOrWhiteSpace(txtKey.Text))
                lstItems("ALL");
            else
                filterItem("ItemName LIKE '%" + txtKey.Text.Trim() + "%'"); 
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (null == lbType.SelectedItem)
                lstItems("ALL");
            else
                lstItems(((BLPublic.CodeNameItem)lbType.SelectedItem).Code); 
        }

        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            if (null == lvItem.SelectedItem)
            {
                BLPublic.Dialogs.Alert("请选择要设置的项目.");
                return;
            }

            tpnmonitor.TPNItem item = (tpnmonitor.TPNItem)lvItem.SelectedItem;

            string input = WinInput.getInput(this, "TPN项目关联", "请输入关联的检查项目编码", item.Value);
            if (null == input)
                return;

            if (!string.IsNullOrWhiteSpace(input))
                input = "[" + input + "]";

            if (AppConst.db.ExecSQL(string.Format(SQL.SET_TPNTEIM_EXP, input, item.ID)))
            { 
                DataRow row = this.tblTPNItem.Rows.Find(item.ID);
                if (null != row)
                    row["Express"] = input;

                item.Value = input;
                lvItem.Items.Refresh();
                BLPublic.Dialogs.Info("设置成功.");
            }
            else
                BLPublic.Dialogs.Error("设置项目关联内容失败:" + AppConst.db.Error);
        }

        private void ByteUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            IntegerUpDown ud = (IntegerUpDown)sender;

            if (!AppConst.db.ExecSQL(string.Format(SQL.MOD_TPNTEIM_SEQ, ud.Value, ud.Tag.ToString())))
                BLPublic.Dialogs.Error("更新显示序号失败:" + AppConst.db.Error);
            else
            {
                DataRow row = this.tblTPNItem.Rows.Find(ud.Tag.ToString());
                if (null != row)
                    row["SeqNo"] = ud.Value;
            }
        } 

        private void TextBox_Changed(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;

            if (!AppConst.db.ExecSQL(string.Format(SQL.MOD_TPNTEIM_NORVAL, txt.Text.Trim(), txt.Tag.ToString())))
                BLPublic.Dialogs.Error("更新正常值失败:" + AppConst.db.Error);
            else
            {
                DataRow row = this.tblTPNItem.Rows.Find(txt.Tag.ToString());
                if (null != row)
                    row["NormalValue"] = txt.Text.Trim();
            }
        }
    }
}
