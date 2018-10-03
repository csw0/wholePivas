using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PivasRevPre
{
    public partial class DetailDrug : UserControl
    {
        public DetailDrug()
        {
            InitializeComponent();
        }

        public void setDrug(DataRow R)
        {
            try
            {
                lblDrugName.Text = R["DrugName"].ToString().Trim();
                if (R["PiShi"].ToString() == "True")
                    lblPiShi.Text = "皮试";
                else
                    lblPiShi.Text = "不皮试";
                lblSpec.Text = R["Spec"].ToString();
                label1.Text = ((float.Parse(R["Dosage"].ToString())).ToString()+R["DosageUnit"].ToString()).Trim();
                label2.Text = R["FregCode"].ToString();
                lblQuantity.Text = R["Quantity"].ToString();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblDrugName_Click(object sender, EventArgs e)
        {
            this.Parent.Focus();
        }
    }
}
