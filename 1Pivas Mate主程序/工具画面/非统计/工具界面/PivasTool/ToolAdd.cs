using PIVAsCommon.Helper;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace PivasTool
{
    public partial class ToolAdd : Form
    {
        protected internal UMinControl um = null;
        protected internal UMaxControl uma = null;
        private static InsertSql insert = new InsertSql();
        private static DB_Help db = new DB_Help();
        private string ImgPath = string.Empty;
        private static string URL = Application.StartupPath + "\\Img\\";
        public ToolAdd(UMinControl um)
        {
            this.um = um;
            this.uma = um.uma;
            InitializeComponent();
            DataSet ds = db.GetPIVAsDB("select distinct [ToolsMaxCategories] from [dbo].[Tools]");
            Comb_MaxCategories.Items.Clear();
            Comb_MinCategories.Items.Clear();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Comb_MaxCategories.Items.Add(dr[0].ToString());
            }
            ds = db.GetPIVAsDB("select distinct [ToolsMinCategories] from [dbo].[Tools]");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Comb_MinCategories.Items.Add(dr[0].ToString());
            }
        }
        public ToolAdd(UCenterControl uc)
        {
            this.uma = uc.um;
            InitializeComponent();
            DataSet ds = db.GetPIVAsDB("select distinct [ToolsMaxCategories] from [dbo].[Tools]");
            Comb_MaxCategories.Items.Clear();
            Comb_MinCategories.Items.Clear();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Comb_MaxCategories.Items.Add(dr[0].ToString());
            }
            ds = db.GetPIVAsDB("select distinct [ToolsMinCategories] from [dbo].[Tools]");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Comb_MinCategories.Items.Add(dr[0].ToString());
            }
        }
        private void Panel_YES_Click(object sender, EventArgs e)
        {
            //string[] add = new string[] { Txt_Name.Text, Txt_Version.Text, Comb_MaxCategories.Text, Comb_MinCategories.Text, Txt_ImgName.Text, Txt_Path.Text };
            Save();
            if (db.SetPIVAsDB(insert.SetTools(this)) > 0)
            {
                uma.ShowUCenter(uma.ToolsMaxCategories);
                MessageBox.Show("成功");
                this.Dispose(true);
            }
            else
            {
                MessageBox.Show("失败");
            }
        }
        /// <summary>
        /// 文件复制，用户自己选择是否覆盖
        /// </summary>
        /// <param name="file">要复制的文件路径</param>
        /// <param name="Rename">文件重命名</param>
        private void Save()
        {
            try
            {
                //复制文件到指定目录(PS：此次复制不修改文件名字)
                //（为true是覆盖同名文件）
                if (!string.IsNullOrEmpty(ImgPath))
                {
                    File.Copy(ImgPath, URL + Path.GetFileName(ImgPath), false);
                }
            }
            catch (Exception ioe)
            {
                //如果报错信息中有出现“已经存在”的字样，弹出消息提示框，提示用户是否覆盖源文件
                //if (ioe.Message.IndexOf("已经存在") >= 0)
                //{
                //    DialogResult dr = MessageBox.Show(ioe.Message + "是否覆盖", "复制文件时提示", MessageBoxButtons.YesNo,
                //        MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                //    if (dr == DialogResult.Yes)
                //    {
                //        //复制文件到指定目录(PS：此次复制覆盖同名文件)
                //        File.Copy(ImgPath, URL + Path.GetFileName(ImgPath), true);  //（为true是覆盖同名文件）
                //    }
                //}
                //else
                {
                    MessageBox.Show(ioe.Message, "复制文件时出错", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void ADDFile()
        {
            if (!Directory.Exists(URL))//若文件夹不存在则新建文件夹   
            {
                Directory.CreateDirectory(URL); //新建文件夹   
            }
        }

        private void Label_AddImgName_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "PNG Files (*.png)|*.png|All Files (*.*)|*.*";
            openFileDialog1.InitialDirectory = Application.StartupPath;
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImgPath = openFileDialog1.FileName;
                Txt_ImgName.Text = openFileDialog1.SafeFileName;
            }
        }

        private void Label_AddPath_Click(object sender, EventArgs e)
        {
            openFileDialog2.Filter = "exe (*.exe)|*.exe|All Files (*.*)|*.*";
            openFileDialog2.InitialDirectory = Application.StartupPath;
            if (this.openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                Txt_Path.Text = openFileDialog2.SafeFileName;
            }
        }

        private void ToolAdd_Load(object sender, EventArgs e)
        {
            ADDFile();
            if (um != null)
            {
                label1.Text = "修改工具";
                Txt_Name.Text = um.dr["ToolsName"].ToString();
                Txt_Version.Text = um.dr["ToolsVersion"].ToString();
                Txt_ImgName.Text = um.dr["ToolsImgName"].ToString();
                Txt_Path.Text = um.dr["ToolsPath"].ToString();
                Comb_MaxCategories.SelectedIndex = Comb_MaxCategories.Items.IndexOf(um.dr["ToolsMaxCategories"].ToString());
                Comb_MinCategories.SelectedIndex = Comb_MinCategories.Items.IndexOf(um.dr["ToolsMinCategories"].ToString());
            }
        }
        private void panel1_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }
    }
}
