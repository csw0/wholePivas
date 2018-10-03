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
    /// ListFindWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WinListFind : Window
    {
        private DataTable tblContent = null;
        private Action<object> onSelected = null; 
        private string SQL = null;

        public WinListFind()
        {
            InitializeComponent();
        }

        public void init(string _SQL, Action<object> _onSelected)
        {
            this.SQL = _SQL;
            this.onSelected = _onSelected;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BLPublic.Utils.setTimeout(500, () => { Dispatcher.Invoke(new Action(() => { loadContent(); }), null); });
        }

        private void loadContent()
        {
            this.tblContent = new DataTable();

            if (!AppConst.db.GetRecordSet(this.SQL, ref this.tblContent))
            {
                BLPublic.Dialogs.Error("加载内容失败:" + AppConst.db.Error);
                return;
            }

            listContent("");
        }

        private void listContent(string _key)
        {
            if (string.IsNullOrWhiteSpace(_key))
                this.tblContent.DefaultView.RowFilter = "";
            else
            {
                string flt = "(Name LIKE '%{0}%')";
                if (this.tblContent.Columns.Contains("QryCode"))
                    flt += " OR (QryCode LIKE '%{0}%')";
                 
                this.tblContent.DefaultView.RowFilter = string.Format(flt, _key);
            }

            lvContent.ItemsSource = this.tblContent.DefaultView; 
        }

        private void doSelected(object _selItem)
        {
            if (null == this.onSelected)
                return;

            DataRow row = ((DataRowView)_selItem).Row; 

            this.onSelected(new BLPublic.CodeNameItem(row["Code"].ToString(), row["Name"].ToString()));
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            listContent(txtKey.Text.Trim());
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (null == lvContent.SelectedItem)
            {
                BLPublic.Dialogs.Alert("请选择内容");
                lvContent.Focus();
                return;
            }
            else
            {
                doSelected(lvContent.SelectedItem);
                this.Close();
            }
        }

        private void Content_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (null != lvContent.SelectedItem)
            {
                doSelected(lvContent.SelectedItem);
                this.Close();
            }
        }

        private void txtKey_KeyUp(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
                listContent(txtKey.Text.Trim());
        }
    } 
}
