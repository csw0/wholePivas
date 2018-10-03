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
    /// WinCustodyObj.xaml 的交互逻辑
    /// </summary>
    public partial class WinCustodyObj : Window
    {
        public WinCustodyObj()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载营养关联检查项目
        /// </summary>
        private void loadPNChkItem()
        {
            DataTable tbl = new DataTable();
            if (!AppConst.db.GetRecordSet(recipemonitorlist.SQL.SEL_PN_CHKITEM, ref tbl))
            {
                BLPublic.Dialogs.Error("加载检查项目失败:" + AppConst.db.Error);
                return;
            }

            BLPublic.BLDataReader bldr = new BLPublic.BLDataReader(tbl.CreateDataReader());
            while (bldr.next())
            {
                cbbChkItem.Items.Add(new BLPublic.IDCodeNameItem(bldr.getString("HISCheckItemCode"),
                    bldr.getString("HISCheckItemUnit"), bldr.getString("HISCheckItemName"))); 
            }
            bldr.close();
            tbl.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dpValDate.SelectedDate = DateTime.Today;

            loadPNChkItem();
        }

        private void cbbChkItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null == cbbChkItem.SelectedItem)
                return;

            txtUnit.Text = ((BLPublic.IDCodeNameItem)cbbChkItem.SelectedItem).Code;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (null == cbbChkItem.SelectedItem)
            {
                BLPublic.Dialogs.Alert("请选择检查项目");
                cbbChkItem.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtChkValue.Text))
            {
                BLPublic.Dialogs.Alert("请输入检查值");
                txtChkValue.Focus();
                return;
            }

            this.DialogResult = true;
            this.Close();
        }
    }
}
