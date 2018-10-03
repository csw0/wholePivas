using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ScanPre
{
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
                lblDrugName.Text = R["DrugName"].ToString();
                if (R["PiShi"].ToString() == "True")
                    lblPiShi.Text = "YES";
                else
                    lblPiShi.Text = "NO";
                lblSpec.Text = R["Spec"].ToString();
                label2.Text = R["FregCode"].ToString();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblDrugName_Click(object sender, EventArgs e)
        {
            this.Parent.Parent.Focus();
        }
    }
}
