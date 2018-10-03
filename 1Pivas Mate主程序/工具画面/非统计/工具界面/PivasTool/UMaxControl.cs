using PIVAsCommon;
using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;


/// <summary>
/// 2013年8月28日-由朱琳转交张望修改
/// 涉及表[Pivas2013].[dbo].[Tools]
/// 字段[ToolsMaxCategories]表示程序左边的大分类节点
/// 字段[ToolsMinCategories]表示程序大节点下各个小节点
/// 字段[ToolsName]标识小节点下各个工具名称
/// 字段[ToolsImgName]表示各个工具对应的图片
/// 字段[ToolsPath]表示各个工具对应的程序名字
/// </summary>
namespace PivasTool
{

    public partial class UMaxControl : UserControl, IMenuManager
    {
        protected internal DB_Help db = new DB_Help();
        protected internal SelectSql Ssql = new SelectSql();
        protected internal string ToolsMaxCategories = string.Empty;
        protected internal string userID = "3548";
        protected internal bool lt;

        public UMaxControl(string userID,string str,string str2)
        {
            this.userID = userID;
            InitializeComponent();
        }
        protected internal void UMaxControl_Load(object sender, EventArgs e)
        {

        }

        protected internal void ShowUCenter(string str)
        {
            this.ToolsMaxCategories = str;
            FPanel_Main.Controls.Clear();
            DataSet ds = db.GetPIVAsDB(Ssql.GetToolsMin(str.Equals("未分组") ? string.Empty : str));
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    UCenterControl uc = new UCenterControl(this, dr[0].ToString());
                    uc.Width = this.Width - FPanel_TreeStrip.Width - 20;
                    FPanel_Main.Controls.Add(uc);
                }
            }
            ds.Dispose();
        }

        private void UMaxControl_SizeChanged(object sender, EventArgs e)
        {
            foreach (Control control in FPanel_Main.Controls)
            {
                control.Width = this.Width - FPanel_TreeStrip.Width-20;
            }
        }

        #region 接口实现
        void IMenuManager.menuBeforeSelect()
        {
            try
            {
                
                lt = db.GetPIVAsDB(string.Format("SELECT [LimitName] FROM [dbo].[ManageLimit] where [DEmployeeID]='{0}' and LimitName='06001'", userID)).Tables[0].Rows.Count > 0;

                FPanel_TreeStrip.Controls.Clear();
                UTreeControl ut = new UTreeControl(this);
                ut.Label_TreeName.Text = "基础数据维护";
                FPanel_TreeStrip.Controls.Add(ut);
                ut.Panel_Tree_Click(null, null);
                ut = new UTreeControl(this);
                ut.Label_TreeName.Text = "系统运行日志";
                FPanel_TreeStrip.Controls.Add(ut);
                DataSet ds = db.GetPIVAsDB(Ssql.GetTools());
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (!dr[0].ToString().Trim().Equals("基础数据维护") && !dr[0].ToString().Trim().Equals("系统运行日志"))
                        {
                            ut = new UTreeControl(this);
                            ut.Label_TreeName.Text = string.IsNullOrEmpty(dr[0].ToString().Trim()) ? "未分组" : dr[0].ToString();
                            FPanel_TreeStrip.Controls.Add(ut);
                        }
                    }
                }
                ds.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void IMenuManager.menuAfterSelect()
        {
        }
        #endregion
    }
}
