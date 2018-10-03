using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PivasLabelCheckAll.common;
using PivasLabelCheckAll.dao;
using PivasLabelCheckAll.entity;
namespace PivasLabelCheckAll
{
    /// <summary>
    /// 所有病区用户控件
    /// </summary>
    public partial class ucDwards : UserControl
    {
        #region 属性
        private UCCommonCheck frmPacCHK;
        private seldb sel = new seldb();
        public List<Dward> wardSelList = new List<Dward>();//用来存储用户锁定的病区的集合
        public List<Dward> wardUnSelList = new List<Dward>();//用来存储用户未锁定的病区的集合
        #endregion

        


        #region 方法
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="wardsSel"></param>
        /// <param name="wardsUnsel"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        public ucDwards(UCCommonCheck frm, List<Dward> wardsSel, List<Dward> wardsUnsel)
        {
            InitializeComponent();
            wardSelList = wardsSel;
            wardUnSelList = wardsUnsel;
            frmPacCHK = frm;
        }

        /// <summary>
        /// 显示所有病区
        /// </summary>
        private void showDwards()
        {
            if (wardSelList.Count > 0)//有选中的病区的情况
            {
                for (int i = 0; i < wardSelList.Count; i++)
                {
                    Dward ward = new Dward();
                    ward = wardSelList[i];
                    ucDwardInfo wardInfo = new ucDwardInfo(ward);
                    wardInfo.autoSelected();
                    flpWards.Controls.Add(wardInfo);
                    //wardInfo.Width = (flpWards.Width - 30) / 5;
                   // wardInfo.Height = (flpWards.Height-60) / 8;
                }

                if (wardUnSelList.Count > 0)//存在没有选中的的病区的情况
                {
                    for (int i = 0; i < wardUnSelList.Count; i++)
                    {
                        Dward ward = new Dward();
                        ward = wardUnSelList[i];
                        ucDwardInfo wardInfo = new ucDwardInfo(ward);
                        wardInfo.antoUnSelected();
                        flpWards.Controls.Add(wardInfo);
                       // wardInfo.Width = (flpWards.Width - 30) / 5;
                     //   wardInfo.Height = (flpWards.Height - 60) / 8;
                    }
                }
            }
            else //加载全部病区
            {
                DataSet ds = new DataSet();
                //ds = sel.getAllDward();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        Dward ward = new Dward();
                        ward.WardCode = dr["WardCode"].ToString();
                        ward.WardName = dr["WardName"].ToString();
                        ward.WardSimName = dr["WardSimName"].ToString();
                        ward.WardArea = dr["WardArea"].ToString();
                        ucDwardInfo wardInfo = new ucDwardInfo(ward);
                        wardInfo.antoUnSelected();
                        cbSelAll.Checked = false;
                        flpWards.Controls.Add(wardInfo);
                        wardInfo.Width = (flpWards.Width - 30) / 5;
                        wardInfo.Height = (flpWards.Height - 60) / 8;
                    }
                }
            }
        }

        /// <summary>
        /// 保存选中的病区
        /// </summary>
        private void SavSelDwards()
        {
            frmPacCHK.wardSelList.Clear();//清空原有选中的病区
            frmPacCHK.wardUnSelList.Clear();//清空原有没选中的病区
            if (flpWards.Controls.Count > 0)
            {
                Dward Selward = null;
                foreach (Control c in flpWards.Controls)
                {
                    if (c is ucDwardInfo)
                    {
                        ucDwardInfo wardInfo = (ucDwardInfo)c;
                        Selward = wardInfo.ward;
                        if (wardInfo.flag == true)//选中的情况
                        {
                            frmPacCHK.wardSelList.Add(Selward);
                        }
                        else
                        {
                            frmPacCHK.wardUnSelList.Add(Selward);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 全选按钮变更方法
        /// </summary>
        private void SelCboxChanged()
        {

            if (flpWards.Controls.Count > 0)
            {
                foreach (Control c in flpWards.Controls)
                {
                    if (c is ucDwardInfo)
                    {
                        ucDwardInfo wardInfo = (ucDwardInfo)c;
                        if (cbSelAll.Checked == true)
                        {
                            wardInfo.autoSelected();
                        }
                        else
                        {
                            wardInfo.antoUnSelected();
                        }
                    }
                }
            }
        }

        #endregion



        #region 事件
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseWard_Click(object sender, EventArgs e)
        {
            SavSelDwards();//保存选中的病区
            frmPacCHK.showDgvLabelInfo(1);
            this.Dispose();
        }

        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucDwards_Load(object sender, EventArgs e)
        {
            showDwards();//显示所有的病区
        }


        /// <summary>
        /// 全选按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSelAll_CheckedChanged(object sender, EventArgs e)
        {
            SelCboxChanged();
            frmPacCHK.FocusAndSelectTB();
        }
        #endregion

        

        


        
        
    }
}
