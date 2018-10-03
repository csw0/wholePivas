using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TVShowWPF
{
    /// <summary>
    /// PageOf3.xaml 的交互逻辑
    /// </summary>
    sealed partial class PageOf3 : UserControl
    {
        private MainWindow mw;
        private int sed = 0;
        public PageOf3(MainWindow mw)
        {
            this.mw = mw;
            InitializeComponent();
            Border11.Background = mw.brush;
            Border21.Background = mw.brush;
            Border31.Background = mw.brush;
            Border11.Tag = string.Empty;
            Border21.Tag = string.Empty;
            Border31.Tag = string.Empty;
        }

        internal void Border11_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mw.IsEdit)
            {
                if (Border11.BorderThickness.Top == 5)
                {
                    sed = sed < mw.ls.Count - 1 ? sed + 1 : 1;
                    (Border11.Child as Label).Content = mw.ls[sed];
                    Border11.Tag = mw.ls[sed];
                }
                else
                {
                    Border11.BorderThickness = new Thickness(5);
                    Border21.BorderThickness = new Thickness(0);
                    Border31.BorderThickness = new Thickness(0);
                    sed = mw.ls.IndexOf(Border11.Tag.ToString());
                }
            }
        }

        private void Border21_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mw.IsEdit)
            {
                if (Border21.BorderThickness.Top == 5)
                {
                    sed = sed < mw.ls.Count - 1 ? sed + 1 : 1;
                    (Border21.Child as Label).Content = mw.ls[sed];
                    Border21.Tag = mw.ls[sed];
                }
                else
                {
                    Border11.BorderThickness = new Thickness(0);
                    Border21.BorderThickness = new Thickness(5);
                    Border31.BorderThickness = new Thickness(0);
                    sed = mw.ls.IndexOf(Border21.Tag.ToString());
                }
            }
        }

        private void Border31_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mw.IsEdit)
            {
                if (Border31.BorderThickness.Top == 5)
                {
                    sed = sed < mw.ls.Count - 1 ? sed + 1 : 1;
                    (Border31.Child as Label).Content = mw.ls[sed];
                    Border31.Tag = mw.ls[sed];
                }
                else
                {
                    Border11.BorderThickness = new Thickness(0);
                    Border21.BorderThickness = new Thickness(0);
                    Border31.BorderThickness = new Thickness(5);
                    sed = mw.ls.IndexOf(Border31.Tag.ToString());
                }
            }
        }
    }
}
