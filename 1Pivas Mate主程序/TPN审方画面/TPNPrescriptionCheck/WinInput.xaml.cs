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
    /// InputWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WinInput : Window
    {
        private Func<string, bool> onInput = null;

        public WinInput()
        {
            InitializeComponent();
        }

        public void inputLong(string _title, string _tip, Func<string, bool> _onInput, string _def = "")
        {
            this.onInput = _onInput;
            this.Title = _title;
            this.Height += 100;
            this.txtTip.Text = _tip;
            this.txtInput.MaxLength = 800;
            this.txtInput.Height += 100;
            this.txtInput.Text = _def;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInput.Text.Trim()))
            {
                BLPublic.Dialogs.Alert("请输入内容");
                txtInput.Focus();
                return;
            }

            if (null != this.onInput)
                if (!this.onInput(txtInput.Text.Trim()))
                    return;

            this.DialogResult = true;
        }

        public static string getInput(Window _parent, string _title, string _tip, int _maxLen = 50)
        {
            return getInput(_parent, _title, _tip, "", _maxLen);
        }

        public static string getInput(Window _parent, string _title, string _tip, string _def = "", int _maxLen = 50)
        {
            WinInput win = new WinInput();
            win.Owner = _parent;
            win.Title = _title;
            win.txtTip.Text = _tip;
            win.txtInput.MaxLength = _maxLen;
            win.txtInput.Text = _def;

            if (true == win.ShowDialog())
                return win.txtInput.Text.Trim();
            else
                return null;
        }
    }
}
