using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ShowPPT
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public sealed partial class ShowPT : UserControl
    {
        private Border br;
        private Grid gd;
        private string AppPath = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
        private string PPTPath = string.Empty;
        private bool isMax = false;
        private System.Data.DataSet DS = new System.Data.DataSet();
        public ShowPT(Border br, Grid gd)
        {
            this.br = br;
            this.gd = gd;
            InitializeComponent();
            if (!Directory.Exists(AppPath + "\\PPT"))
            {
                Directory.CreateDirectory(AppPath + "\\PPT");
            }
            bool err = false;
            if (File.Exists(AppPath + "\\ShowPPT.Dat"))
            {
                try
                {
                    DS.ReadXml(AppPath + "\\ShowPPT.Dat", System.Data.XmlReadMode.ReadSchema);
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
                    foreach (string s in Directory.GetFiles(AppPath + "\\PPT", "*.ppt", SearchOption.TopDirectoryOnly))
                    {
                        PPTPath = s;
                        break;
                    }
                }
                else
                {
                    PPTPath = p;
                }
                PPTPath = PPTPath.Replace(AppPath, ".");
                if (!string.IsNullOrEmpty(PPTPath) && File.Exists(PPTPath))
                {
                    FileInfo fi = new FileInfo(PPTPath);
                    string path = fi.DirectoryName + "\\" + fi.Name.Replace(fi.Extension, string.Empty);
                    bool err = false;
                    if (Directory.Exists(path))
                    {
                        if (Directory.GetFiles(path, "*.jpg", SearchOption.TopDirectoryOnly).Length == 0)
                        {
                            err = true;
                            Directory.Delete(path, true);
                        }
                    }
                    else
                    {
                        err = true;
                    }
                    if (err)
                    {
                        PowerPoint.Presentation pr = new PowerPoint.Application().Presentations.Open((PPTPath.StartsWith(".") ? AppPath + PPTPath.TrimStart('.') : PPTPath), PowerPoint.MsoTriState.msoTrue, PowerPoint.MsoTriState.msoFalse, PowerPoint.MsoTriState.msoFalse);
                        pr.SaveAs(path + ".jpg", PowerPoint.PpSaveAsFileType.ppSaveAsJPG, PowerPoint.MsoTriState.msoTrue);
                        pr.Close();
                    }
                    foreach (string s in Directory.GetFiles(path, "*.jpg", SearchOption.TopDirectoryOnly))
                    {
                        ImageBrush b = new ImageBrush();
                        b.ImageSource = new BitmapImage(new Uri(s, UriKind.RelativeOrAbsolute));
                        b.Stretch = Stretch.UniformToFill;
                        Bor1.Background = b;
                        break;
                    }
                    DS.Tables[0].Rows.Find(br.Name)[1] = PPTPath;
                    DS.WriteXml(AppPath + "\\ShowPPT.Dat", System.Data.XmlWriteMode.WriteSchema);
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
        }

        private void Bor1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ClickCount == 2 && !string.IsNullOrEmpty(PPTPath))
                {
                    PowerPoint.Application ap = new PowerPoint.Application();
                    PowerPoint.SlideShowSettings sss = ap.Presentations.Open((PPTPath.StartsWith(".") ? AppPath + PPTPath.TrimStart('.') : PPTPath), PowerPoint.MsoTriState.msoTrue, PowerPoint.MsoTriState.msoFalse, PowerPoint.MsoTriState.msoFalse).SlideShowSettings;
                    SetWindowPos(new IntPtr(ap.HWND), -1, 0, 0, 0, 0, 1 | 4);
                    sss.Run();
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);

        private void Bor1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                using (System.Windows.Forms.OpenFileDialog open = new System.Windows.Forms.OpenFileDialog())
                {
                    open.CheckFileExists = true;
                    open.CheckPathExists = true;
                    open.AddExtension = true;
                    open.Title = "选择PPT";
                    open.Filter = "ppt文件|*.ppt";
                    open.InitialDirectory = PPTPath;
                    open.Multiselect = false;
                    if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (!open.FileName.Contains(AppPath + "\\PPT\\"))
                        {
                            File.Copy(open.FileName, AppPath + "\\PPT\\" + open.SafeFileName, true);
                        }
                        DS = new System.Data.DataSet();
                        DS.ReadXml(AppPath + "\\ShowPPT.Dat", System.Data.XmlReadMode.ReadSchema);
                        CheckPath(open.FileName);
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
