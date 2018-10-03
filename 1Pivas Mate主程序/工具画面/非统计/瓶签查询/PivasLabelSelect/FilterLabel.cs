using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PivasLabelSelect
{
    public partial class FilterLabel : UserControl
    {
        public FilterLabel()
        {
            InitializeComponent();
        }


        public delegate void DelegateChangeTextS(int Chose,FilterLabel ff,bool checd);
        public event DelegateChangeTextS ChangeTextVal;
        public string Str=string.Empty;

        private void FilterLabel_Load(object sender, EventArgs e)
        {
 
        }
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="Labeltext">显示选择项</param>
        /// <param name="ChoseTag">是否仅此状态</param>
        /// <param name="Choseindex">选择总条件</param>
        public void ShowData(string Labeltext, bool ChoseTag, int Choseindex)
        {
            int width = 5;
            label1.Tag = Choseindex;


            if (Str == string.Empty)
            {
               

                label1.Text = Labeltext;
            }
            else
            {
                if (Labeltext.Contains("全部") || Labeltext.Contains("临时") || Labeltext.Contains("长期"))
                    label1.Text = Labeltext;
                else
                    label1.Text = label1.Text + ":" + Labeltext.Replace("批次:", "").Trim();
            }

            Str =label1.Text ;
            width = label1.Width + width;
            if (Choseindex!=0)
            {
                flowLayoutPanel1.Controls.Remove(checkBox1);
                
                
            }
            else
            {

                checkBox1.Checked = ChoseTag;
                width = checkBox1.Width + width;
            }

     
            width = label2.Width + width;
            this.Width = width + 20;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            ChangeTextVal(Convert.ToInt32(label1.Tag),this,true);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ChangeTextVal(Convert.ToInt32(label1.Tag),this,false);
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            label2.Image = global::PivasLabelSelect.Properties.Resources._5;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.Image = global::PivasLabelSelect.Properties.Resources._6;
        }
    }
}
