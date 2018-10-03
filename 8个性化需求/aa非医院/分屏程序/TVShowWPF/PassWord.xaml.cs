using System.Windows;
using System.Windows.Controls;

namespace TVShowWPF
{
    /// <summary>
    /// PassWord.xaml 的交互逻辑
    /// </summary>
    public partial class PassWord : Window
    {
        private string value = string.Empty;

        public PassWord(string v)
        {
            this.value = v;
            InitializeComponent();
        }

        private void buttonCEL_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == value)
            {
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("密码错误！");
            }
        }

        private void buttonCL_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password.Length > 0)
            {
                passwordBox.Password = passwordBox.Password.Remove(passwordBox.Password.Length - 1);
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            passwordBox.Password = passwordBox.Password + (sender as Button).Content;
        }

        private void PW_Loaded(object sender, RoutedEventArgs e)
        {
            passwordBox.Focus();
        }
    }
}
