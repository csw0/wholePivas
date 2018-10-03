using PIVAsCommon.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LibDrugManual
{
    public partial class RichFrom : Form
    {
        public RichFrom()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 选中的是哪个set
        /// </summary>
        int i = 0;
        //  public  string UniPreparationID =string.Empty;
        public string UniPreparationID = "75";
        /// <summary>
        /// 内容改变是否改变文本框大小
        /// </summary>
        bool TextSizeChaged = false;
        bool has = false;
        DataSet ds;

        StringBuilder htmlstr=new StringBuilder();
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void RichFrom_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(UniPreparationID);
             webBrowser1.DocumentText="";
             //webBrowser1.Document.Body.Id = "1";
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
                    DataShow();
                    //赋值内容
                    
                }
                else
                {
                    Title.Parent = tabPage1;
                    Title.Text = "无药品说明书";
                    flowLayoutPanel1.Visible = false;
                }
                has = true;
            }
        }

        private void DataShow()
        {

            if (ds != null && ds.Tables[0].Rows.Count > i)
            {
                TextSizeChaged = false;
                htmlstr = new StringBuilder();
                webBrowser1.DocumentText = string.Empty;
                Title.Text = ds.Tables[0].Rows[i]["Title"].ToString();
                LabelShow("修改日期", ds.Tables[0].Rows[i]["修改日期"]);
                LabelShow("药品名称", ds.Tables[0].Rows[i]["药品名称"]);
                LabelShow("性状", ds.Tables[0].Rows[i]["性状"]);
                LabelShow("药理毒理", ds.Tables[0].Rows[i]["药理毒理"]);
                LabelShow("药代动力学", ds.Tables[0].Rows[i]["药代动力学"]);
                LabelShow("适应症/功能主治", ds.Tables[0].Rows[i]["适应症/功能主治"]);
                LabelShow("用法用量", ds.Tables[0].Rows[i]["用法用量"]);
                LabelShow("不良反应", ds.Tables[0].Rows[i]["不良反应"]);
                LabelShow("禁忌", ds.Tables[0].Rows[i]["禁忌"]);
                LabelShow("注意事项", ds.Tables[0].Rows[i]["注意事项"]);
                LabelShow("孕妇及哺乳期妇女用药", ds.Tables[0].Rows[i]["孕妇及哺乳期妇女用药"]);
                LabelShow("儿童用药", ds.Tables[0].Rows[i]["儿童用药"]);
                LabelShow("老年用药", ds.Tables[0].Rows[i]["老年用药"]);
                LabelShow("药物相互作用", ds.Tables[0].Rows[i]["药物相互作用"]);
                LabelShow("药物过量", ds.Tables[0].Rows[i]["药物过量"]);
                LabelShow("规格", ds.Tables[0].Rows[i]["规格"]);
                LabelShow("贮藏", ds.Tables[0].Rows[i]["贮藏"]);
                LabelShow("包装", ds.Tables[0].Rows[i]["包装"]);
                LabelShow("有效期", ds.Tables[0].Rows[i]["有效期"]);
                LabelShow("批准文号", ds.Tables[0].Rows[i]["批准文号"]);
                LabelShow("生产企业", ds.Tables[0].Rows[i]["生产企业"]);
                TextSizeChaged = true;
                if (has)
                {
                    HtmlDocument hd = webBrowser1.Document.OpenNew(true);
                    hd.Write("<html style='font-family:宋体;font-size:16;background-color:white;'>" + htmlstr.ToString() + "</Html>");
                }
                else
                {
                    webBrowser1.DocumentText = "<html style='font-family:宋体;font-size:16;background-color:white;'>" + htmlstr.ToString() + "</Html>";
                }

            }
        }



        /// <summary>
        /// 把内容放在label里
        /// </summary>
        /// <param name="lb">label控件</param>
        /// <param name="Labelstr">内容</param>
        private void LabelShow(string lb, object Labelstr)
        {
            string strg = "<br /><span style='font-weight:bold;color:blue;Magin-top:50px'>" + "【"+lb +"】"+ "</span>";
            if (Labelstr != null && Labelstr.ToString().Trim() != "")
            {
                htmlstr.Append(strg+"<div>");
                htmlstr.Append(Labelstr.ToString().Replace("<br />", "</div><div style='margin-left:35px;line-height:20px'>"));
               // htmlstr.Append("</div>");
            }
            else
            {      
                   htmlstr.Append(strg+"<br />");
            }
            htmlstr.Append("</div>");
        }

        private void tabControl1_Resize(object sender, EventArgs e)
        {
            TextSizeChaged = true;
            foreach (Control cc in flowLayoutPanel1.Controls)
            {
                cc.Width = tabControl1.Width-20;
            }
            flowLayoutPanel1.Width = this.Size.Width - 50;
            flowLayoutPanel1.Height = tabControl1.Height - Title.Height - 20;
            webBrowser1.Height =tabControl1.Height - Title.Height-20;
            TextSizeChaged = false;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            i = tabControl1.SelectedIndex;
            flowLayoutPanel1.Parent = tabControl1.SelectedTab;
            DataShow();
        }

    }
}
