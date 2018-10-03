using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PivasIVRPrint
{
    internal partial class BatchS : Form
    {
        private UserControlPrint piv;
        private int ShowItem = 0;
        private bool allsec;
        internal BatchS(UserControlPrint p,int i)
        {
            ShowItem = i;
            this.piv = p;
            InitializeComponent();
        }

        private void BatchS_Load(object sender, EventArgs e)
        {
            if (ShowItem == 0)
            {
                string[] bats = piv.BatS.Replace("'", string.Empty).Split(',');
                foreach (DataRow dr in piv.Batch.Rows)
                {
                    checkedListBox1.Items.Add(dr[0].ToString(), string.IsNullOrEmpty(piv.BatS) ? false : bats.Contains(dr[0].ToString()));
                }
            }
            else if (ShowItem == 1)
            {
                string[] usages = piv.UsageCodeS.Replace("'", string.Empty).Split(',');
                foreach (DataRow dr in piv.Usage.Rows)
                {
                    checkedListBox1.Items.Add(dr[0].ToString(), string.IsNullOrEmpty(piv.UsageCodeS) ? true : usages.Contains(dr[0].ToString()));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ShowItem == 0)
            {
                piv.BatS = string.Empty;
                foreach (string clb in checkedListBox1.CheckedItems)
                {
                    piv.BatS = piv.BatS + "'" + clb + "',";
                }
                piv.BatS = piv.BatS.TrimEnd(',');
                if (!string.IsNullOrEmpty(piv.BatS))
                {
                    piv.label2.Text = piv.BatS.Replace("'", string.Empty);
                    string OrderBY = string.Empty;
                    using (DataSet ds = piv.dbHelp.GetPIVAsDB(string.Format("SELECT max([OrderBY]) FROM [dbo].[BatchToOrder] where [Batch] = '{0}'", piv.label2.Text.Contains(",") ? piv.label2.Text.Split(',')[0] : piv.label2.Text)))
                    {
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            OrderBY = ds.Tables[0].Rows[0][0].ToString();
                        }
                    }
                    piv.comboBox6.SelectedIndex = string.IsNullOrEmpty(OrderBY) ? 0 : piv.comboBox6.Items.IndexOf(OrderBY);
                }
                piv.comboBox2.Visible = string.IsNullOrEmpty(piv.BatS);
                piv.label2.Visible = !piv.comboBox2.Visible;
                piv.PivasIVRP_Load(sender, e);
                this.Dispose(true);
            }
            else if (ShowItem == 1)
            {
                piv.UsageCodeS = string.Empty;
                if (checkedListBox1.CheckedItems.Count != checkedListBox1.Items.Count)
                {
                    foreach (string clb in checkedListBox1.CheckedItems)
                    {
                        piv.UsageCodeS = piv.UsageCodeS + "'" + clb + "',";
                    }
                    piv.UsageCodeS = piv.UsageCodeS.TrimEnd(',');
                }
                piv.PivasIVRP_Load(sender, e);
                this.Dispose(true);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            allsec = !allsec;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, allsec);
            }
        }
    }
}
