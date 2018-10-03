using System;
using System.Collections.Generic;
using System.Linq;
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
    /// ListWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WinList : Window
    {
        private Action<object> onSelected = null;
        private object selItem = null;

        public WinList()
        {
            InitializeComponent();
        }

        public object SelObject { get { return this.selItem; } }

        public void init(List<BLPublic.CodeNameItem> _list, Action<object> _onSelected = null)
        {
            lbList.ItemsSource = _list;
            this.onSelected = _onSelected;

            if (null == this.onSelected)
                btnClose.Visibility = System.Windows.Visibility.Visible;
        }
        public void init(List<object> _list, Action<object> _onSelected = null)
        {
            lbList.ItemsSource = _list;
            this.onSelected = _onSelected;

            if (null == this.onSelected)
                btnClose.Visibility = System.Windows.Visibility.Visible;
        } 

        private void Window_Deactivated(object sender, EventArgs e)
        {
            if (null != this.onSelected) 
                this.Close();
        } 

        private void Window_Closed(object sender, EventArgs e)
        {
            if (true == this.DialogResult)
            {
                if (this.Topmost)
                    this.Topmost = false;

                if (null != this.Owner)
                    this.Owner.Activate();
            }
        }

        private void lbList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (null != lbList.SelectedItem)
            {
                this.selItem = lbList.SelectedItem;
                if (null != this.onSelected)
                    this.onSelected(this.selItem);

                else
                    this.DialogResult = true;

                //this.Close();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        { 
            this.Close();
        }
    }
}
