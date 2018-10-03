using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PivasBatchCommon;

namespace PivasBatch
{
    public partial class Ward : UserControl
    {
        public Ward()
        {
            InitializeComponent();
        }
        public string WardName;
        public string WardCode;
        UserControlBatch PivasB;


        /// <summary>
        /// 初始化并赋值
        /// </summary>
        /// <param name="R"></param>
        /// <param name="IvBatchSaved"></param>
        /// <param name="pivas"></param>
        public void SetWard(DataRow R, int IvBatchSaved,UserControlBatch pivas)
        {
            try
            {
                this.PivasB = pivas;
                lblWardName.Text = R["WardName"].ToString();
                if (!lblWardName.Text.Trim().Contains("全部"))
                {
                    lblWardCode.Text = R["WardCode"].ToString();
                }
                else
                {
                    lblWardCode.Text = "0";
                }
                    if (IvBatchSaved != 2)
                {
                    this.Controls.Remove(Label_NotGet);
                    Label_Total.Text = R["UnCheckCount"].ToString()+ "/" + (R["TotalCount"].ToString().Trim().Length>0?R["TotalCount"].ToString():"0");
                }
                else
                {
                    Label_NotGet.Text = "";
                    Label_Total.Text = R["TotalCount"].ToString();
                }
               
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Word_Click(object sender, EventArgs e)
        {
            pvb.operate = true;
            WardClick();
        }

        /// <summary>
        /// 点击病区调用
        /// </summary>
        public void WardClick()
        {
            WardCode = lblWardCode.Text;

            foreach (Ward a in this.Parent.Controls)
            {
                a.BackgroundImage = null;
                a.ForeColor = Color.Black;
            }
            this.BackgroundImage = (Image)PivasBatch.Properties.Resources.ResourceManager.GetObject("病区选中");
            this.ForeColor = Color.White;
            this.Parent.Focus();
            PivasB.ShowDrug(this.lblWardCode.Text, true);

        }
    }
}
