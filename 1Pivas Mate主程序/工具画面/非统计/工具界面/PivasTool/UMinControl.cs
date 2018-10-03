using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace PivasTool
{
    public partial class UMinControl : UserControl
    {
        protected internal DataRow dr;
        protected internal UMaxControl uma = null;
        public UMinControl()
        {
            InitializeComponent();
        }
        public UMinControl(DataRow dr,UMaxControl uma)
        {
            this.uma = uma;
            this.dr = dr;
            InitializeComponent();
        }

        private void UMinControl_Load(object sender, EventArgs e)
        {
            Label_ToolsName.Text = dr["ToolsName"].ToString();
            if (!string.IsNullOrEmpty(dr["ToolsImgName"].ToString().Trim()))
            {
                if (dr["ToolsImgName"].ToString().Contains("."))
                {
                    if (File.Exists("./Img/" + dr["ToolsImgName"].ToString()))
                        pictureBox1.BackgroundImage = Image.FromFile("./Img/" + dr["ToolsImgName"].ToString());
                }
                else
                    pictureBox1.BackgroundImage = (Image)PivasTool.Properties.Resources.ResourceManager.GetObject(dr["ToolsImgName"].ToString());
            }
        }

        private void Clicks()
        {
            try
            {
                Process p = new Process();
                if (string.IsNullOrEmpty(dr["ToolsPath"].ToString().Trim()))
                {
                    MessageBox.Show("没有配置运行程序路径");
                }
                else
                {
                    if (dr["ToolsPath"].ToString().ToLower().Contains(".exe"))
                    {
                        p.StartInfo.FileName = dr["ToolsPath"].ToString();
                        p.StartInfo.Arguments = uma.userID;
                    }
                    else
                    {
                        p.StartInfo.FileName = dr["ToolsPath"].ToString() + ".exe";
                        p.StartInfo.Arguments = uma.userID;
                    }
                    p.StartInfo.WorkingDirectory = Application.StartupPath;
                    if (File.Exists("./" + p.StartInfo.FileName))
                    {
                        p.Start();
                    }
                    else
                    {
                        p.Dispose();
                        MessageBox.Show("调用的程序不存在");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            panel1.BackgroundImage = (Image)PivasTool.Properties.Resources.ResourceManager.GetObject("凸显");
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            panel1.BackgroundImage=null;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Clicks();
            }
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show();
            }
        }

        private void 删除此控件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (uma.lt)
            {
                DialogResult drt = MessageBox.Show("确认删除" + Label_ToolsName.Text + "工具", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (drt == DialogResult.Yes)
                {
                    if (new DB_Help().SetPIVAsDB(string.Format("delete from [dbo].[Tools] where ToolsID='{0}'", dr["ToolsID"].ToString())) > 0)
                    {
                        uma.ShowUCenter(uma.ToolsMaxCategories);
                    }
                }
            }
            else
            {
                MessageBox.Show("您没有权限，请联系管理员");
            }
        }

        private void 修改此控件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (uma.lt)
            {
                new ToolAdd(this).ShowDialog();
            }
            else
            {
                MessageBox.Show("您没有权限，请联系管理员");
            }
        }
    }
}
