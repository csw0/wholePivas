using System;
using System.Windows.Forms;
using System.IO;
using PivasBatchCommon;
using PIVAsCommon.Helper;

namespace PivasBatchMX
{
    public partial class DrugSum : UserControl
    {
        //Pivasbatch pvb;
        DB_Help db = new DB_Help();
        
        public DrugSum()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 液体总量
        /// </summary>
        /// <param name="OrderColor">批次颜色</param>
        /// <param name="TColor">批次字体颜色</param>
        /// <param name="MinSum">总量</param>
        /// <param name="KSum">空包</param>
        public void SetInfo(string OrderColor,string TColor, string MinSum,string KSum)
        {
            try
            {
                Panel_Total.BackColor = System.Drawing.ColorTranslator.FromHtml(OrderColor);
                Panel_Total.ForeColor = System.Drawing.ColorTranslator.FromHtml(TColor);
                //if(1==1)//
                //如果设置空包计入液体总量的话
                if (pvb.IsPack == 1)
                {
                    if (MinSum.IndexOf("袋") != MinSum.Length - 1)
                    {
                        label_Sum.Text = MinSum + "ml";
                    }
                    else
                    {
                        label_Sum.Text = MinSum + "0ml";
                    }
                    //label_Sum.Text = label_Sum.Text +"(空包："+KSum;
                    if (KSum.Trim().Length != 0 && KSum != "0")
                    {
                        label2.Text = "(空包：" + KSum + "ml)";
                    }
                    else
                    {
                        label2.Visible = false;
                    }
                }
                else
                {
                    if (MinSum.IndexOf("袋") != MinSum.Length - 1)
                    {
                        label_Sum.Text = (int.Parse(MinSum) - int.Parse(KSum)) + "ml";
                    }
                    else
                    {
                        label_Sum.Text = (int.Parse(MinSum) - int.Parse(KSum)) + "0ml";
                    }

                    if (KSum.Trim().Length != 0 && KSum != "0")
                    {
                        label2.Text = "(空包：" + KSum + "ml)";
                    }
                    else
                    {
                        label2.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatchMX" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10013:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
  
            }
          
        }

        private void Label___Click(object sender, EventArgs e)
        {
            ((FlowLayoutPanel)(this.Parent.Parent.Parent)).Focus();
        }
    }
}
