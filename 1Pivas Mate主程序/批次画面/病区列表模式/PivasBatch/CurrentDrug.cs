using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PivasBatch
{
    /// <summary>
    /// 批次画面 处方药品信息
    /// </summary>
    public partial class CurrentDrug : UserControl
    {
        public CurrentDrug()
        {
            InitializeComponent();
        }


        public void setDrug(DataRow R)
        {
            try
            {
                //药品
                lblDrugName.Text = R["DrugName"].ToString().Trim();
                if (R["PiShi"].ToString() == "True")
                    lblPiShi.Text = "皮试";
                else
                    lblPiShi.Text = "--";
                //规格
                lblSpec.Text = R["Spec"].ToString();
                //剂量和剂量单位
                label2.Text = R["剂量"].ToString() + R["单位"].ToString().Trim();
            }
            catch (System.Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10005:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }

        private void lblSpec_Click(object sender, EventArgs e)
        {
            this.Parent.Focus();
        }
    }
}
