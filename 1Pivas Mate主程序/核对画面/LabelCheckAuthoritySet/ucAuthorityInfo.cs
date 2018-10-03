using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LabelCheckAuthoritySet.common;
using LabelCheckAuthoritySet.entity;
using LabelCheckAuthoritySet.service;


namespace LabelCheckAuthoritySet
{
    /// <summary>
    /// 单个权限
    /// </summary>
    public partial class ucAuthorityInfo : UserControl
    {
        private Authority Auth;
        private CommonUtil commonUtil = new CommonUtil();
        private AuthoritySetService service = new AuthoritySetService();

        public ucAuthorityInfo(Authority auth)
        {
            Auth = auth;
            InitializeComponent();
        }

        /// <summary>
        /// 显示权限
        /// </summary>
        /// <param name="auth"></param>
        private void ShowRuler(Authority auth)
        {
            string name1 = commonUtil.LabelSetName(auth.AUthorityLevel);
            string name2 = commonUtil.LabelSetName(auth.AuthorityArea);
            string msg = "";
            if (auth.AuthorityName.Trim() == "All")
            {
                msg = name1 + "之后 允许" + name2;

                btnModify.Visible = false;
                btnDelete.Visible = false;
            }
            else
            {
                string otherRule = auth.AuthorityName;
                string[] Rules = otherRule.Split(';');
                if (Rules[0].Equals("UL"))
                {
                    msg += "非临时瓶签，";
                }
                else if (Rules[0].Equals("L"))
                {
                    msg += "临时瓶签，";
                }
                else
                {
                    msg += "非临时和临时瓶签，";
                }

                if (Rules[1].Equals("UST"))
                {
                    msg += "非临时医嘱，";
                }
                else if (Rules[1].Equals("ST"))
                {
                    msg += "临时医嘱，";
                }
                else
                {
                    msg += "非临时和临时医嘱，";
                }

                if (Rules[2].Equals("K"))
                {
                    msg += "K包，";
                }
                else if (Rules[2].Equals("#"))
                {
                    msg += "#包，";
                }
                else
                {
                    msg += "K和#包，";
                }

                msg += "第" + Rules[3] +"批次 ，";
                msg += name1 + " 之后允许" + name2;
            }
            lblRuler.Text = msg;
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="auth"></param>
        private void DeleteRule(Authority auth)
        {
           DialogResult dr = MessageBox.Show("确认删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question); 
            if (dr == DialogResult.OK) 
            {
                bool flag = service.DeleteOtherService(auth);
                if (flag)
                {
                    MessageBox.Show("操作成功！");
                    ((frmLabelAuthoritySet)this.Parent.Parent.Parent).FindAllAuthority(); 
                }
                else
                {
                    MessageBox.Show("操作失败！");
                }
            }
        }

        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucAuthorityInfo_Load(object sender, EventArgs e)
        {
            lblSeqno.Text = Auth.SeqNo.ToString();
            //lblRuler.Text = Auth.AuthorityName;
            ShowRuler(Auth);
        }

        /// <summary>
        /// 删除当前规则
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteRule(Auth);
        }

        /// <summary>
        /// 修改当前规则
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModify_Click(object sender, EventArgs e)
        {
            ((frmLabelAuthoritySet)this.Parent.Parent.Parent).UpdateRule(Auth);
        }
    }
}
