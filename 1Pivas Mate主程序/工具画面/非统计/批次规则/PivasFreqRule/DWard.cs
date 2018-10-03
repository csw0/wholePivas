using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PivasFreqRule
{
    /// <summary>
    /// 病区用户控件
    /// </summary>
    public partial class DWard : UserControl
    {
        public static string DWardCode;
        public static bool IsClick;
        public static Control DWardName;
        public string WardCode;    //保存当前部门的编号
        private Panel pnl;
        /// <summary>
        /// 有参构造方法
        /// </summary>
        /// <param name="WCode">当前部门编号</param>
        /// <param name="MR">所有部门集合对象</param>
        public DWard(String WCode,Panel P)
        {
            InitializeComponent();
            IsClick = false;
            WardCode = WCode;
            pnl = P;
        }

        /// <summary>
        /// 无参构造方法
        /// </summary>
        public DWard() {
            InitializeComponent();
            IsClick = false;
        }

        public void Show(DataRow Row)
        {
            Ward.Text = Row["WardName"].ToString();
            Code.Text = Row["WardCode"].ToString();
        }

        /// <summary>
        /// 病区单击事件(查找这个病区中所有的药剂信息)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ward_Click(object sender, EventArgs e)
        {
               
                DWardCode = Code.Text;
                IsClick = true;
                DWardName = this;
                this.Focus();
                if (PivasFreqRule.Choice == "medcine")
                {
                    ((MedcineRule)this.Parent.Parent).ShowMedcine(Code.Text);
                    ((MedcineRule)this.Parent.Parent).label5.Text = Ward.Text;
                }
                else if (PivasFreqRule.Choice == "volume")
                {
                    ((VolumeRule)this.Parent.Parent).ShowLimit(Code.Text);
                    ((VolumeRule)this.Parent.Parent).label2.Text = Ward.Text;
                }
                else if (PivasFreqRule.Choice == "warddrug")
                {
                    ((WardDrug)this.Parent.Parent).ShowLimit(Code.Text);
                    ((WardDrug)this.Parent.Parent).label5.Text = Ward.Text;
                    ((WardDrug)this.Parent.Parent).label5.Tag = Code.Text;
                }
           

            //选中后提亮背景色
            //MessageBox.Show("当前部门的编号是："+WardCode);
            WardChoiceBgColor();
            //未选中恢复背景色
            WardUnChoiceBgControl();
        }

        /// <summary>
        /// 选中时背景色高亮
        /// </summary>
        private void WardChoiceBgColor()
        {
            this.BackColor = Color.FromArgb(140, 140, 255);
        }


        /// <summary>
        /// 遍历控件,未选中背景色
        /// </summary>
        private void WardUnChoiceBgControl() {
            foreach (Control c in pnl.Controls)
            {
                if (c is DWard)
                {
                    DWard dWard = (DWard)c;
                    if (dWard.WardCode != WardCode)
                    {
                        dWard.BackColor = Color.White;
                    }
                }
            }  
        }

       
        //private void DWard_Enter(object sender, EventArgs e)
        //{
        //    this.BackColor = Color.FromArgb(140, 140, 255);
        //}

        
        //private void DWard_Leave(object sender, EventArgs e)
        //{
        //    this.BackColor = Color.White;
        //}
    }
}
