using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace ShowPics
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public sealed partial class ShowPic : UserControl
    {
        private Border br;
        private Grid gd;
        private DispatcherTimer dt = new DispatcherTimer();
        private List<string> ls = new List<string>();
        private int i = 0;
        private string AppPath = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
        private string PicPath = string.Empty;
        private bool isMax = false;
        private System.Data.DataSet DS = new System.Data.DataSet();

        private bool ShowSet = false;
        private bool HasRun = false;
        private float SHT = 0;
        private DispatcherTimer DST = new DispatcherTimer();
        public ShowPic(Border br, Grid gd)
        {
            this.br = br;
            this.gd = gd;
            InitializeComponent();
            BorLP.Opacity = 0f;
            ImageBrush b = new ImageBrush();
            b.Stretch = Stretch.Uniform;
            b.ImageSource = BitmapToImage(Properties.Resources.ret);
            BorRet.Background = b;
            b = new ImageBrush();
            b.Stretch = Stretch.Uniform;
            b.ImageSource = BitmapToImage(Properties.Resources.next);
            BorNxt.Background = b;

            dt.Interval = new TimeSpan(0, 0, 5);
            dt.Tick += new EventHandler(dtrun);
            DST.Interval = new TimeSpan(0, 0, 0, 0, 25);
            DST.Tick += new EventHandler(ShowORHidden);

            PicPath = AppPath + "\\Pics";
            if (!Directory.Exists(PicPath))
            {
                Directory.CreateDirectory(PicPath);
            }
            bool err = false;
            if (File.Exists(AppPath + "\\ShowPic.Dat"))
            {
                try
                {
                    DS.ReadXml(AppPath + "\\ShowPic.Dat", System.Data.XmlReadMode.ReadSchema);
                    DS.Tables[0].PrimaryKey = new System.Data.DataColumn[] { DS.Tables[0].Columns[0] };
                    if (!DS.Tables[0].Rows.Contains(br.Name))
                    {
                        DS.Tables[0].Rows.Add(br.Name, string.Empty);
                    }
                }
                catch
                {
                    err = true;
                }
            }
            else
            {
                err = true;
            }
            if (err)
            {
                DS.Namespace = "NewDS";
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.TableName = "NewDT";
                dt.Columns.Add("Border", typeof(string));
                dt.Columns.Add("Path", typeof(string));
                dt.PrimaryKey = new System.Data.DataColumn[] { dt.Columns[0] };
                dt.Rows.Add(br.Name, string.Empty);
                DS.Tables.Add(dt);
            }
            CheckPath(DS.Tables[0].Rows.Find(br.Name)[1].ToString());
            dtrun(null, null);
            BorPly_MouseLeftButtonDown(null, null);
        }

        private BitmapImage BitmapToImage(System.Drawing.Bitmap bitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, bitmap.RawFormat);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
            return bitmapImage;
        }


        private void CheckPath(string p)
        {
            try
            {
                if (string.IsNullOrEmpty(p))
                {
                    p = PicPath;
                }
                if (Directory.Exists(p) && Directory.GetFiles(p, "*.jpg", SearchOption.TopDirectoryOnly).Length > 0)
                {
                    PicPath = p;
                    DS.Tables[0].Rows.Find(br.Name)[1] = p.Replace(AppPath, ".");
                    DS.WriteXml(AppPath + "\\ShowPic.Dat", System.Data.XmlWriteMode.WriteSchema);
                    ls.Clear();
                    foreach (string s in Directory.GetFiles(PicPath, "*.jpg", SearchOption.TopDirectoryOnly))
                    {
                        ls.Add(s);
                    }
                }
                else
                {
                    MessageBox.Show("目录中没有图片，请重新选择路径");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (isMax)
            {
                this.Height = gd.RenderSize.Height;
                this.Width = gd.RenderSize.Width;
            }
            else
            {
                this.Height = br.RenderSize.Height;
                this.Width = br.RenderSize.Width;
                br.Child = this;
            }
            ShowPic1.HorizontalAlignment = HorizontalAlignment.Stretch;
            ShowPic1.VerticalAlignment = VerticalAlignment.Stretch;
            DST.Start();
        }

        private void dtrun(object sender, EventArgs e)
        {
            if (ls.Count > 0)
            {
                if (i + 1 < ls.Count)
                {
                    i = i + 1;
                }
                else
                {
                    i = 0;
                }
                ImageBrush b = new ImageBrush();
                b.ImageSource = new BitmapImage(new Uri(ls[i], UriKind.RelativeOrAbsolute));
                b.Stretch = Stretch.UniformToFill;
                Bor1.Background = b;
            }
        }

        private void label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                isMax = !isMax;
                if (isMax)
                {
                    br.Child = null;
                    this.RenderSize = gd.RenderSize;
                    gd.Children.Add(this);
                }
                else
                {
                    gd.Children.Remove(this);
                    this.RenderSize = br.RenderSize;
                    br.Child = this;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                using (System.Windows.Forms.FolderBrowserDialog FBD = new System.Windows.Forms.FolderBrowserDialog())
                {
                    FBD.SelectedPath = PicPath;
                    FBD.ShowNewFolderButton = false;
                    FBD.Description = "图片目录选择";
                    if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        DS = new System.Data.DataSet();
                        DS.ReadXml(AppPath + "\\ShowPic.Dat", System.Data.XmlReadMode.ReadSchema);
                        CheckPath(FBD.SelectedPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowORHidden(object sender, EventArgs e)
        {
            ShowSet = GridShow.IsMouseOver;
            BorLP.Opacity = SHT;
            if (ShowSet)
            {
                if (SHT < 1)
                {
                    SHT = SHT + 0.1f;
                }
            }
            else
            {
                if (SHT > 0)
                {
                    SHT = SHT - 0.1f;
                }
            }
        }

        private void BorNxt_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ls.Count > 0)
            {
                if (i + 1 < ls.Count)
                {
                    i = i + 1;
                }
                else
                {
                    i = 0;
                }
                ImageBrush b = new ImageBrush();
                b.ImageSource = new BitmapImage(new Uri(ls[i], UriKind.RelativeOrAbsolute));
                b.Stretch = Stretch.UniformToFill;
                Bor1.Background = b;
            }
        }

        private void BorPly_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ImageBrush b = new ImageBrush();
            if (HasRun)
            {
                b.ImageSource = BitmapToImage(Properties.Resources.play);
                dt.Stop();
                HasRun = false;
            }
            else
            {
                b.ImageSource = BitmapToImage(Properties.Resources.stop);
                dt.Start();
                HasRun = true;
            }
            b.Stretch = Stretch.Uniform;
            BorPly.Background = b;
        }

        private void BorRet_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ls.Count > 0)
            {
                if (i - 1 > -1)
                {
                    i = i - 1;
                }
                else
                {
                    i = ls.Count - 1;
                }
                ImageBrush b = new ImageBrush();
                b.ImageSource = new BitmapImage(new Uri(ls[i], UriKind.RelativeOrAbsolute));
                b.Stretch = Stretch.UniformToFill;
                Bor1.Background = b;
            }
        }
    }
}
