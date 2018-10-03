using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using PIVAsCommon.Helper;

namespace LibDrugManual
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 选中的是哪个set
        /// </summary>
        int i = 0;
      //  public  string UniPreparationID =string.Empty;
        public string UniPreparationID = "806";
        /// <summary>
        /// 内容改变是否改变文本框大小
        /// </summary>
        bool TextSizeChaged = false;
        DataSet ds;
        private void MainForm_Load(object sender, EventArgs e)
        {
            MessageBox.Show(UniPreparationID);
            if (UniPreparationID.Length > 0)
            {
                StringBuilder str = new StringBuilder();
                str.Append(" SELECT     SpecificationID, TypeID, UniversalID, SubTitle as 修改日期, DrugNames 药品名称,");
                str.Append(" Characters 性状, Pharmacology 药理毒理, Dynamics 药代动力学, AdapterSymtoms as '适应症/功能主治',");
                str.Append(" Usages 用法用量, SideEffects 不良反应, Taboos 禁忌, Notion 注意事项, WomenUsages 孕妇及哺乳期妇女用药,");
                str.Append(" ChildrenUsages 儿童用药,OldUsages 老年用药, Interactions 药物相互作用, Paranormal 药物过量, Specifications 规格,");
                str.Append(" Storage 贮藏, Package 包装, ValidityPeriod 有效期, ConfirmNumber 批准文号, Factory 生产企业, InputOrg, IsValid, Title,");
                //str.Append(" company FROM   kd0100.dbo.DrugSpecification ");
                str.Append(" company FROM   kd0102.dbo.DrugSpecification ");
                str.Append(" WHERE     UniversalID in (select UniversalID from  kd0100.dbo.UniPreparation where UniPreparationID='" + UniPreparationID + "')");
                str.Append(" order by TypeID");
                DB_Help db = new DB_Help();
               
                ds = db.GetPIVAsDB(str.ToString());

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    //加载tab名称
                    if (ds.Tables[0].Rows[0]["company"].ToString().Trim().Length > 0)
                    {
                        tabPage1.Text = ds.Tables[0].Rows[0]["company"].ToString();
                    }
                    else
                    {
                        if (ds.Tables[0].Rows[0]["TypeID"].ToString().Equals("1"))
                        {
                            tabPage1.Text = "标准说明书";
                        }
                    }
                    //加载tab
                    for (int j = 1; j < ds.Tables[0].Rows.Count && tabControl1.Controls.Count < ds.Tables[0].Rows.Count; j++)
                    {
                        TabPage tab = new TabPage(ds.Tables[0].Rows[j]["company"].ToString());
                        tabControl1.Controls.Add(tab);
                    }
                    //赋值内容
                    DataShow();
                }
                else
                {
                    Title.Parent = tabPage1;
                    Title.Text = "无药品说明书";
                    flowLayoutPanel1.Visible = false;
                }
            }
            else
            {
                Title.Parent = tabPage1;
                Title.Text = "未匹配此药品对应的制剂信息";
                flowLayoutPanel1.Visible = false;
            }
        }

        private void DataShow()
        {
              if (ds != null && ds.Tables[0].Rows.Count > i)
              {
                TextSizeChaged = false;
                Title.Text = ds.Tables[0].Rows[i]["Title"].ToString();
                LabelShow(SubTitle, ds.Tables[0].Rows[i]["修改日期"]);
                LabelShow(DrugNames, ds.Tables[0].Rows[i]["药品名称"]);
                LabelShow(Characters, ds.Tables[0].Rows[i]["性状"]);
                LabelShow(Pharmacology, ds.Tables[0].Rows[i]["药理毒理"]);
                LabelShow(Dynamics, ds.Tables[0].Rows[i]["药代动力学"]);
                LabelShow(AdapterSymtoms, ds.Tables[0].Rows[i]["适应症/功能主治"]);
                LabelShow(Usages, ds.Tables[0].Rows[i]["用法用量"]);
                LabelShow(SideEffects, ds.Tables[0].Rows[i]["不良反应"]);
                LabelShow(Taboos, ds.Tables[0].Rows[i]["禁忌"]);
                LabelShow(Notion, ds.Tables[0].Rows[i]["注意事项"]);
                LabelShow(WomenUsages, ds.Tables[0].Rows[i]["孕妇及哺乳期妇女用药"]);
                LabelShow(ChildrenUsages, ds.Tables[0].Rows[i]["儿童用药"]);
                LabelShow(OldUsages, ds.Tables[0].Rows[i]["老年用药"]);
                LabelShow(Interactions, ds.Tables[0].Rows[i]["药物相互作用"]);
                LabelShow(Paranormal, ds.Tables[0].Rows[i]["药物过量"]);
                LabelShow(Specifications, ds.Tables[0].Rows[i]["规格"]);
                LabelShow(Storage, ds.Tables[0].Rows[i]["贮藏"]);
                LabelShow(Package, ds.Tables[0].Rows[i]["包装"]);
                LabelShow(ValidityPeriod, ds.Tables[0].Rows[i]["有效期"]);
                LabelShow(ConfirmNumber, ds.Tables[0].Rows[i]["批准文号"]);
                LabelShow(Factory, ds.Tables[0].Rows[i]["生产企业"]);
                TextSizeChaged = true;
            }
        }

        /// <summary>
        /// 把内容放在label里
        /// </summary>
        /// <param name="lb">label控件</param>
        /// <param name="Labelstr">内容</param>
        private void LabelShow(TextBox lb, object Labelstr)
        {
            if (Labelstr != null && Labelstr.ToString().Trim() != "")
            {
                //textbox最小size
                lb.MaximumSize = new Size(flowLayoutPanel1.Width, this.Height);

                //System.Windows.Forms.HtmlDocument document =this.webBrowser1.Document;
               // string labstr = Labelstr.ToString().Trim().Replace("<br />", "\r\n");
                ////如果内容只有'回车换行'的话，隐藏此textbox
                //if (labstr.IndexOf("\r\n") == 0 && labstr.Length > 0)
                //{
                //    lb.Visible = true;
                //    labstr = labstr.Remove(0, 2);
                //    lb.Text = labstr;          
                //    SizeChage(lb);
                //}
                //else
                //{
                //    lb.Visible = false;
                //}'


              //  System.Web.HttpUtility.HtmlEncode();
                lb.Text = System.Web.HttpUtility.HtmlDecode(Labelstr.ToString());
                SizeChage(lb);
                lb.Visible = true;
               // lb.HtmlDecode
      
            }
            else
            {
                lb.Visible = false;
            }
        }

        /// <summary>
        /// tabcontrol切换选择项时，说明书内容变换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            i = tabControl1.SelectedIndex;
            flowLayoutPanel1.Parent = tabControl1.SelectedTab;
            DataShow();
        }

         /// <summary>
         ///控件大小随内容变更 
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void DrugNames_SizeChanged(object sender, EventArgs e)
        {
            TextBox tt = (TextBox)sender;
            if (TextSizeChaged)
            {
                SizeChage(tt);
            }
        }

        /// <summary>
        /// 设置控件大小
        /// </summary>
        /// <param name="tt"></param>
        private void SizeChage(TextBox tt)
        {
            int LineCount = tt.GetLineFromCharIndex(tt.Text.Length) + 1;
            tt.Height = LineCount * 15;
        }

        /// <summary>
        /// flowLayoutPanel1所有控件的大小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_Resize(object sender, EventArgs e)
        {
            TextSizeChaged = true;
            foreach (Control cc in flowLayoutPanel1.Controls)
            {
                cc.MaximumSize = new Size(this.Size.Width-50, this.Height);
                cc.Width = this.Size.Width -50;
            }
            flowLayoutPanel1.Width = this.Size.Width -50;
            TextSizeChaged = false;
        }

        /// <summary>
        /// 获取焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubTitle_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Focus();
        }
    }
}
