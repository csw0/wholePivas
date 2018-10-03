using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PivasLabelCheckAll.entity;

namespace PivasLabelCheckAll
{
    /// <summary>
    /// 病区用户控件
    /// </summary>
    public partial class ucDwardInfo : UserControl
    {

        #region 属性
        public  Dward ward;
        public Boolean flag = true;
        private Color choseColor = SystemColors.GradientActiveCaption;
        private Color usChoseColor =Color.White;
        #endregion


        #region  方法
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dr">数据库一条病区对象</param>
        public ucDwardInfo(Dward dWard)
        {
            InitializeComponent();
            ward = dWard;
        }

        /// <summary>
        /// 加载病区信息
        /// </summary>
        private void showDward()
        {
            lblWardName.Text = ward.WardSimName;
            tboxWardSimName.Text = ward.WardSimName;
            LblWardArea.Text = ward.WardArea;
        }

        /// <summary>
        /// 改变复选框的选中状态和背景色
        /// </summary>
        private void showChanged()
        {
            if (cbSelected.Checked == true)
            {
                cbSelected.Checked = false;
                this.BackColor = usChoseColor;
                flag = false;
            }
            else
            {
                cbSelected.Checked = true;
                this.BackColor = choseColor;
                flag = true;
            } 
        }

        /// <summary>
        /// 加载的时候自动勾选
        /// </summary>
        public void autoSelected()
        {
            cbSelected.Checked = true;
            this.BackColor = choseColor;
            flag = true;
        }

        /// <summary>
        /// 取现勾选
        /// </summary>
        public void antoUnSelected()
        {
            cbSelected.Checked = false;
            this.BackColor = usChoseColor;
            flag = false;
        }
        #endregion


        #region  事件
        /// <summary>
        /// 控件加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucDwardInfo_Load(object sender, EventArgs e)
        {
            showDward();
        }

        /// <summary>
        /// 单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucDwardInfo_Click(object sender, EventArgs e)
        {
            showChanged();
            //MessageBox.Show(ward.WardArea);
        }
        #endregion

        

      
    }
}
