using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;


namespace PivasRevPre
{
    public partial class AllDrug : UserControl
    {
        public AllDrug()
        {
            InitializeComponent();
        }

        public void SetName(DataRow R)
        {
            try
            {
                lblMetric.Text = T(R["Dosage"].ToString()) + R["DosageUnit"].ToString();
                lblName.Text = R["DrugName"].ToString();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblName_Click(object sender, EventArgs e)
        {
            this.Parent.Parent.Focus();
        }


        private string T(string temp)
        {
            while (true)
            {
                if (temp == "")
                    break;
                if (temp[temp.Length - 1] == '0')
                    temp = temp.Remove(temp.Length - 1);
                else
                    break;
            }

            if (temp != ""&&temp[temp.Length - 1] == '.')
            {
                temp = temp.Remove(temp.Length - 1);
            }
            return temp;
        }
    }
}
