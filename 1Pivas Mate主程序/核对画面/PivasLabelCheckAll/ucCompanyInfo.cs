using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PivasLabelCheckAll
{
    public partial class ucCompanyInfo : UserControl
    {
       

        #region 属性
        #endregion


        #region 方法
        /// <summary>
        /// 构造方法
        /// </summary>
        public ucCompanyInfo()
        {
            InitializeComponent();
        }

        public void showCompanyInfo()
        { 
             string strInfo = "版权所有@上海市博龙智医科技股份有限公司";
             lblCompanyInfo.Text = strInfo;

             string strAdd = "上海市宝山区长江南路180号C区 318室";
             lblAddress.Text = strAdd;
        }
        #endregion


        #region 事件
        /// <summary>
        /// 页面加载事件,显示公司版权信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucCompanyInfo_Load(object sender, EventArgs e)
        {
            showCompanyInfo();
        }
        #endregion

    }
}
