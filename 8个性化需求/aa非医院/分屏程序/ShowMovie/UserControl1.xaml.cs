using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShowMovie
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public sealed partial class ShowMV : UserControl
    {
        private Border br;
        private Grid gd;
        private List<string> ls = new List<string>();
        private int i = 0;
        private string AppPath = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
        private string MovPath = string.Empty;
        private bool isMax = false;
        private System.Data.DataSet DS = new System.Data.DataSet();
        public ShowMV(Border br, Grid gd)
        {
            this.br = br;
            this.gd = gd;
            InitializeComponent();
            MovPath = AppPath + "\\Mov";
            if (!Directory.Exists(MovPath))
            {
                Directory.CreateDirectory(MovPath);
            }
            bool err = false;
            if (File.Exists(AppPath + "\\ShowMov.Dat"))
            {
                try
                {
                    DS.ReadXml(AppPath + "\\ShowMov.Dat", System.Data.XmlReadMode.ReadSchema);
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
        }

        private void CheckPath(string p)
        {
            try
            {
                if (string.IsNullOrEmpty(p))
                {
                    p = MovPath;
                }
                if (Directory.Exists(p) && Directory.GetFiles(p, "*.mp4", SearchOption.TopDirectoryOnly).Length > 0)
                {
                    MovPath = p;
                    DS.Tables[0].Rows.Find(br.Name)[1] = p.Replace(AppPath, ".");
                    DS.WriteXml(AppPath + "\\ShowMov.Dat", System.Data.XmlWriteMode.WriteSchema);
                    ls.Clear();
                    foreach (string s in Directory.GetFiles(MovPath, "*.mp4", SearchOption.TopDirectoryOnly))
                    {
                        ls.Add(s);
                    }
                }
                else
                {
                    MessageBox.Show("目录中没有视频，请重新选择路径");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Bor1_Loaded(object sender, RoutedEventArgs e)
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
            mediaElement_MediaEnded(null, null);
        }

        private void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
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
                mediaElement.Source = new Uri(ls[i], UriKind.RelativeOrAbsolute);
            }
        }

        private void mediaElement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount==2)
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
        }

        private void Bor1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                using (System.Windows.Forms.FolderBrowserDialog FBD = new System.Windows.Forms.FolderBrowserDialog())
                {
                    FBD.SelectedPath = MovPath;
                    FBD.ShowNewFolderButton = false;
                    FBD.Description = "视频目录选择";
                    if (FBD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        DS = new System.Data.DataSet();
                        DS.ReadXml(AppPath + "\\ShowMov.Dat", System.Data.XmlReadMode.ReadSchema);
                        bool run = (ls.Count == 0);
                        CheckPath(FBD.SelectedPath);
                        if (run)
                        {
                            mediaElement_MediaEnded(null, null);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}