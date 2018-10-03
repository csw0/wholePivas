using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PivasFreqRule;
namespace DFregManage
{
    public partial class rowDFreg : UserControl
    {
        public rowDFreg()
        {
            InitializeComponent();
        }




        public delegate void DelegateChangeText();

        //public event DelegateChangeText ChangeTextVa;

        public static string  i ;
        seldb seldb = new seldb();
        updatedeletedb ud = new updatedeletedb();

        /// <summary>
        /// 初始化用法列表
        /// </summary>
        /// <param name="row">用法每行数据</param>
        /// <param name="isalter">是否修改</param>
        /// <param name="k"></param>
        public void add(DataRow row,bool isalter,string k)
        {
            label1.Tag = row["FreqCode"].ToString();
            label1.Text = row["FreqName"].ToString();
            label2.Text = row["IntervalDay"].ToString();
            label3.Text = row["TimesOfDay"].ToString();
            label4.Text = row["FreqCode"].ToString();
            label6.Text = row["UseTime"].ToString();
          
                Panel_delete .Visible = true;         
                Panel_delete.Click+=new EventHandler(Panel_delete_Click);
                
             
          
        }


        private void add()
        {
            DataSet ds= seldb.getDFregid(label1.Tag.ToString());
            DataRow row=ds.Tables[0].Rows[0];
             label1.Tag = row["FreqCode"].ToString();
            label1.Text = row["FreqName"].ToString();
            label2.Text = row["IntervalDay"].ToString();
            label3.Text = row["TimesOfDay"].ToString();
            label4.Text = row["FreqCode"].ToString();
            label6.Text = row["UseTime"].ToString();
            Panel_delete .Visible = true;
          
            Panel_delete.Click+=new EventHandler(Panel_delete_Click);
        }

        /// <summary>
        /// 修改间隔时间窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label2_Click(object sender, EventArgs e) 
        {
            Point p = new Point(Control.MousePosition.X, Control.MousePosition.Y);
            UpdateDelete updel = new UpdateDelete();
            i = label1.Tag.ToString();

            updel.ChangeTextVal += new UpdateDelete.DelegateChangeTextValS(add);
            updel.Location = p;
            updel.ShowDialog(this);
        }

        /// <summary>
        /// 删除用法事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel_delete_Click(object sender, EventArgs e) 
        {
            if (MessageBox.Show("是否删除?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                ud.deleteDFreq(label1.Tag.ToString());

                ((DFreg)this.Parent.Parent).refresh();

            }
        }

        /// <summary>
        /// 修改每日次数窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label3_Click(object sender, EventArgs e)
        {
            Point p = new Point(Control.MousePosition.X, Control.MousePosition.Y);
            Update_Times updete = new Update_Times();

            updete.ChangeTextVal += new Update_Times.DelegateChangeTextValS(add);
            i = label1.Tag.ToString();
            updete.Location = p;
            updete.ShowDialog(this);
        }

        /// <summary>
        /// 修改使用时间窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label6_Click(object sender, EventArgs e) 
        {
            i = label1.Tag.ToString();
            Freg_Rule freg_rule = new Freg_Rule();
            freg_rule.ChangeTextVal += new Freg_Rule.DelegateChangeTextValS(add);
            freg_rule.ShowDialog(this);
        }
    }
}
