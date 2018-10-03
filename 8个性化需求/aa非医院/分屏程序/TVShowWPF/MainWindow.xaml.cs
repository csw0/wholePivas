using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace TVShowWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    sealed partial class MainWindow : Window
    {
        internal VisualBrush brush = new VisualBrush();
        internal bool IsEdit = false;
        internal List<string> ls = new List<string>() { string.Empty, "图片", "视频", "PPT", "瓶签统计", "排班表" };

        private int MS = 0;
        private bool ShowSet = false;
        private bool ThRun = false;
        private float SHT = 0;
        private DataSet Config = new DataSet();
        private DispatcherTimer DST = new DispatcherTimer();
        private string AppPath = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
        public MainWindow()
        {
            InitializeComponent();
            Button_MS.Visibility = Visibility.Hidden;
            BorderSet.Visibility = Visibility.Hidden;
            DST.Interval = new TimeSpan(0, 0, 0, 0, 25);
            DST.Tick += new EventHandler(ShowORHidden);
            bool err = false;
            if (!Directory.Exists(AppPath + "\\BackGD"))
            {
                Directory.CreateDirectory(AppPath + "\\BackGD");
            }
            if (File.Exists(AppPath + "\\Config.Dat"))
            {
                try
                {
                    Config.ReadXml(AppPath + "\\Config.Dat", XmlReadMode.Auto);
                    Config.Tables[0].PrimaryKey = new DataColumn[] { Config.Tables[0].Columns[0] };
                    if (Config.Tables.Count == 0 || Config.Tables[0].Rows.Count < 8)
                    {
                        err = true;
                    }
                }
                catch
                {
                    File.Delete(AppPath + "\\Config.Dat");
                    err = true;
                }
            }
            else
            {
                err = true;
            }
            if (err)
            {
                Config.Namespace = "NewDS";
                DataTable DT = new DataTable();
                DT.TableName = "NewDT";
                DT.Columns.Add("Item", typeof(string));
                DT.Columns.Add("Value", typeof(string));
                DT.PrimaryKey = new DataColumn[] { DT.Columns[0] };
                DT.Rows.Add("BackGD", string.Empty);
                DT.Rows.Add("MS", "0");
                DT.Rows.Add("PassWord", "13816350872");
                DT.Rows.Add("Border11", string.Empty);
                DT.Rows.Add("Border21", string.Empty);
                DT.Rows.Add("Border31", string.Empty);
                DT.Rows.Add("Border41", string.Empty);
                DT.Rows.Add("Border51", string.Empty);
                Config.Tables.Add(DT);
                Config.WriteXml(AppPath + "\\Config.Dat", XmlWriteMode.WriteSchema);
            }
            string BGD = Config.Tables[0].Rows.Find("BackGD")[1].ToString();
            if (!string.IsNullOrEmpty(BGD) && File.Exists(AppPath + "\\BackGD\\" + BGD))
            {
                ImageBrush IB = new ImageBrush(new BitmapImage(new Uri(AppPath + "\\BackGD\\" + BGD, UriKind.RelativeOrAbsolute)));
                IB.Stretch = Stretch.UniformToFill;
                Bor2.Background = IB;
            }
            brush.Stretch = Stretch.Uniform;
            GD2.Background = brush;
        }

        private void MainForm_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (Config.Tables[0].Rows[1][1].ToString())
                {
                    case "0":
                        {
                            MS = 0;
                            break;
                        }
                    case "1":
                        {
                            MS = 1;
                            break;
                        }
                    case "2":
                        {
                            MS = 2;
                            break;
                        }
                    default:
                        {
                            MS = 0;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MS = 0;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MSChanged();
            }
        }

        private void MSChanged()
        {
            try
            {
                switch (MS)
                {
                    case 0:
                        {
                            PageOf3 pf = new PageOf3(this);
                            pf.RenderSize = Bor1.RenderSize;
                            Bor1.Children.Clear();
                            if (!IsEdit)
                            {
                                pf.Border11.Tag = Config.Tables[0].Rows.Find("Border11")[1];
                                pf.Border21.Tag = Config.Tables[0].Rows.Find("Border21")[1];
                                pf.Border31.Tag = Config.Tables[0].Rows.Find("Border31")[1];
                            }
                            SetChild(pf.Border11);
                            SetChild(pf.Border21);
                            SetChild(pf.Border31);
                            pf.Border11_MouseLeftButtonDown(null, null);
                            Bor1.Children.Add(pf);
                            break;
                        }
                    case 1:
                        {
                            PageOf4 pf = new PageOf4(this);
                            pf.RenderSize = Bor1.RenderSize;
                            Bor1.Children.Clear();
                            if (!IsEdit)
                            {
                                pf.Border11.Tag = Config.Tables[0].Rows.Find("Border11")[1];
                                pf.Border21.Tag = Config.Tables[0].Rows.Find("Border21")[1];
                                pf.Border31.Tag = Config.Tables[0].Rows.Find("Border31")[1];
                                pf.Border41.Tag = Config.Tables[0].Rows.Find("Border41")[1];
                            }
                            SetChild(pf.Border11);
                            SetChild(pf.Border21);
                            SetChild(pf.Border31);
                            SetChild(pf.Border41);
                            pf.Border11_MouseLeftButtonDown(null, null);
                            Bor1.Children.Add(pf);
                            break;
                        }
                    case 2:
                        {
                            PageOf5 pf = new PageOf5(this);
                            pf.RenderSize = Bor1.RenderSize;
                            Bor1.Children.Clear();
                            if (!IsEdit)
                            {
                                pf.Border11.Tag = Config.Tables[0].Rows.Find("Border11")[1];
                                pf.Border21.Tag = Config.Tables[0].Rows.Find("Border21")[1];
                                pf.Border31.Tag = Config.Tables[0].Rows.Find("Border31")[1];
                                pf.Border41.Tag = Config.Tables[0].Rows.Find("Border41")[1];
                                pf.Border51.Tag = Config.Tables[0].Rows.Find("Border51")[1];
                            }
                            SetChild(pf.Border11);
                            SetChild(pf.Border21);
                            SetChild(pf.Border31);
                            SetChild(pf.Border41);
                            SetChild(pf.Border51);
                            pf.Border11_MouseLeftButtonDown(null, null);
                            Bor1.Children.Add(pf);
                            break;
                        }
                    default: { break; }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetChild(Border br)
        {
            try
            {
                if (IsEdit)
                {
                    br.Child = null;
                    Label l1 = new Label();
                    l1.Content = string.IsNullOrEmpty(br.Tag.ToString()) ? "点击切换功能" : br.Tag.ToString();
                    l1.VerticalAlignment = VerticalAlignment.Center;
                    l1.HorizontalAlignment = HorizontalAlignment.Center;
                    l1.FontSize = 70;
                    l1.Foreground = new SolidColorBrush(Color.FromArgb(System.Drawing.Color.Silver.A, System.Drawing.Color.Silver.R, System.Drawing.Color.Silver.G, System.Drawing.Color.Silver.B));
                    br.Child = l1;
                }
                else
                {
                    br.Child = null;
                    switch (br.Tag.ToString())
                    {
                        case "图片":
                            {
                                ShowPics.ShowPic sp = new ShowPics.ShowPic(br, Bor1);
                                br.Child = sp;
                                break;
                            }
                        case "视频":
                            {
                                ShowMovie.ShowMV sm = new ShowMovie.ShowMV(br, Bor1);
                                br.Child = sm;
                                break;
                            }
                        case "PPT":
                            {
                                ShowPPT.ShowPT SP = new ShowPPT.ShowPT(br, Bor1);
                                br.Child = SP;
                                break;
                            }
                        case "瓶签统计":
                            {
                                TVShow.UserControl1 SP = new TVShow.UserControl1(br, Bor1);
                                br.Child = SP;
                                break;
                            }
                        case "排班表":
                            {
                                PaiBanTVShow.UserControl1 SP = new PaiBanTVShow.UserControl1(br, Bor1);
                                br.Child = SP;
                                break;
                            }
                        default: { break; }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!IsEdit)
                {
                    if (!new PassWord(Config.Tables[0].Rows.Find("PassWord")[1].ToString()).ShowDialog().GetValueOrDefault(false))
                    {
                        return;
                    }
                }
                IsEdit = !IsEdit;
                if (IsEdit)
                {
                    if(Bor1.Children.Count>1)
                    {
                        Bor1.Children.RemoveRange(1, Bor1.Children.Count - 1);
                    }
                    Button_MS.Visibility = Visibility.Visible;
                    switch (MS)
                    {
                        case 0:
                            {
                                PageOf3 pf = Bor1.Children[0] as PageOf3;
                                SetChild(pf.Border11);
                                SetChild(pf.Border21);
                                SetChild(pf.Border31);
                                pf.Border11_MouseLeftButtonDown(sender, null);
                                break;
                            }
                        case 1:
                            {
                                PageOf4 pf = Bor1.Children[0] as PageOf4;
                                SetChild(pf.Border11);
                                SetChild(pf.Border21);
                                SetChild(pf.Border31);
                                SetChild(pf.Border41);
                                pf.Border11_MouseLeftButtonDown(sender, null);
                                break;
                            }
                        case 2:
                            {
                                PageOf5 pf = Bor1.Children[0] as PageOf5;
                                SetChild(pf.Border11);
                                SetChild(pf.Border21);
                                SetChild(pf.Border31);
                                SetChild(pf.Border41);
                                SetChild(pf.Border51);
                                pf.Border11_MouseLeftButtonDown(sender, null);
                                break;
                            }
                        default: { break; }
                    }
                }
                else
                {
                    Button_MS.Visibility = Visibility.Hidden;
                    switch (MS)
                    {
                        case 0:
                            {
                                PageOf3 pf = Bor1.Children[0] as PageOf3;
                                pf.Border11.BorderThickness = new Thickness(0);
                                pf.Border21.BorderThickness = new Thickness(0);
                                pf.Border31.BorderThickness = new Thickness(0);
                                SetChild(pf.Border11);
                                SetChild(pf.Border21);
                                SetChild(pf.Border31);
                                Config.Tables[0].Rows.Find("MS")[1] = "0";
                                Config.Tables[0].Rows.Find("Border11")[1] = pf.Border11.Tag;
                                Config.Tables[0].Rows.Find("Border21")[1] = pf.Border21.Tag;
                                Config.Tables[0].Rows.Find("Border31")[1] = pf.Border31.Tag;
                                break;
                            }
                        case 1:
                            {
                                PageOf4 pf = Bor1.Children[0] as PageOf4;
                                pf.Border11.BorderThickness = new Thickness(0);
                                pf.Border21.BorderThickness = new Thickness(0);
                                pf.Border31.BorderThickness = new Thickness(0);
                                pf.Border41.BorderThickness = new Thickness(0);
                                SetChild(pf.Border11);
                                SetChild(pf.Border21);
                                SetChild(pf.Border31);
                                SetChild(pf.Border41);
                                Config.Tables[0].Rows.Find("MS")[1] = "1";
                                Config.Tables[0].Rows.Find("Border11")[1] = pf.Border11.Tag;
                                Config.Tables[0].Rows.Find("Border21")[1] = pf.Border21.Tag;
                                Config.Tables[0].Rows.Find("Border31")[1] = pf.Border31.Tag;
                                Config.Tables[0].Rows.Find("Border41")[1] = pf.Border41.Tag;
                                break;
                            }
                        case 2:
                            {
                                PageOf5 pf = Bor1.Children[0] as PageOf5;
                                pf.Border11.BorderThickness = new Thickness(0);
                                pf.Border21.BorderThickness = new Thickness(0);
                                pf.Border31.BorderThickness = new Thickness(0);
                                pf.Border41.BorderThickness = new Thickness(0);
                                pf.Border51.BorderThickness = new Thickness(0);
                                SetChild(pf.Border11);
                                SetChild(pf.Border21);
                                SetChild(pf.Border31);
                                SetChild(pf.Border41);
                                SetChild(pf.Border51);
                                Config.Tables[0].Rows.Find("MS")[1] = "2";
                                Config.Tables[0].Rows.Find("Border11")[1] = pf.Border11.Tag;
                                Config.Tables[0].Rows.Find("Border21")[1] = pf.Border21.Tag;
                                Config.Tables[0].Rows.Find("Border31")[1] = pf.Border31.Tag;
                                Config.Tables[0].Rows.Find("Border41")[1] = pf.Border41.Tag;
                                Config.Tables[0].Rows.Find("Border51")[1] = pf.Border51.Tag;
                                break;
                            }
                        default: { break; }
                    }
                    Config.WriteXml(AppPath + "\\Config.Dat", XmlWriteMode.WriteSchema);
                }
                button_BUJU.Content = IsEdit ? "完成" : "布局";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GD2_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ShowSet = true;
            if (!ThRun)
            {
                ThRun = true;
                SHT = 0.1f;
                DST.Start();
            }
        }

        private void GD2_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ShowSet = false;
            if (!ThRun)
            {
                ThRun = true;
                SHT = 0.9f;
                DST.Start();
            }
        }

        private void ShowORHidden(object sender, EventArgs e)
        {
            BorderSet.Opacity = SHT;
            if (SHT > 0 && SHT < 1)
            {
                BorderSet.Visibility = Visibility.Visible;
                if (ShowSet)
                {
                    SHT = SHT + 0.1f;
                }
                else
                {
                    SHT = SHT - 0.1f;
                }
            }
            else
            {
                if (SHT == 0)
                {
                    BorderSet.Visibility = Visibility.Hidden;
                }
                ThRun = false;
                DST.Stop();
            }
        }

        private void button_BEIJIN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (System.Windows.Forms.OpenFileDialog open = new System.Windows.Forms.OpenFileDialog())
                {
                    open.CheckFileExists = true;
                    open.CheckPathExists = true;
                    open.AddExtension = true;
                    open.Title = "选择背景图片";
                    open.Filter = "jpg文件|*.jpg|Png文件|*.png";
                    open.InitialDirectory = AppPath + "\\BackGD";
                    open.Multiselect = false;
                    if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (!open.FileName.Contains(AppPath + "\\BackGD\\"))
                        {
                            File.Copy(open.FileName, AppPath + "\\BackGD\\" + open.SafeFileName, true);
                        }
                        Config.Tables[0].Rows.Find("BackGD")[1] = open.SafeFileName;
                        ImageBrush IB = new ImageBrush(new BitmapImage(new Uri(AppPath + "\\BackGD\\" + open.SafeFileName, UriKind.RelativeOrAbsolute)));
                        IB.Stretch = Stretch.UniformToFill;
                        Bor2.Background = IB;
                        Config.WriteXml(AppPath + "\\Config.Dat", XmlWriteMode.WriteSchema);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_MS_Click(object sender, RoutedEventArgs e)
        {
            MS = MS < 2 ? MS + 1 : 0;
            MSChanged();
        }
    }
}
