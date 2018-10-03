using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PivasIVRPrint
{
    public partial class BQlabel : UserControl
    {
        private UserControlPrint piv;
        private static string Labelno;
        private bool ischange;
        public BQlabel(UserControlPrint p,string labelno)
        {
            piv = p;
            Labelno=labelno;
            InitializeComponent();
        }

        private void BQlabel_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = piv.dbHelp.GetPIVAsDB(string.Format("SELECT * FROM ({0} where LabelNo = '{1}') V order by NoName,DosageUnit,Dosage", piv.GetLabelNoSql,Labelno));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    panel2.BackColor = (dr["IVStatus"].ToString() == "0") ? Color.LightGreen : Color.Silver;
                    label1.Text = dr["LabelNo"].ToString();
                    label2.Text = dr["PatName"].ToString();
                    label5.Text = dr["WardName"].ToString();
                    label7.Text = dr["Batch"].ToString();
                    label3.Text = dr["FreqCode"].ToString();
                    label4.Text = dr["BedNo"].ToString();
                    label6.Text = dr["StartDT"].ToString();
                    label8.Text = dr["PatientCode"].ToString();
                    label9.Text = dr["DEmployeeName"].ToString();
                    label10.Text = string.Empty;
                    checkBox1.Checked = piv.prints[Labelno];
                    flowLayoutPanel1.Height = (new BQDrugs()).Height * ds.Tables[0].Rows.Count;
                    panel2.Height = panel1.Height = flowLayoutPanel1.Location.Y + flowLayoutPanel1.Height;
                    this.Height = panel2.Height + 10;
                    foreach (DataRow drs in ds.Tables[0].Rows)
                    {
                        flowLayoutPanel1.Controls.Add(new BQDrugs(this, drs));
                    }
                    ds.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void BQlabel_Click(object sender, EventArgs e)
        {
            if (!this.Focused)
                this.Focus();
            //piv.index = this.Parent.Controls.IndexOf(this);
            //if (checkBox1.Checked)
            //    piv.selected = Labelno;
            //else
            //    piv.selected = "";
        }

        private void BQlabel_SizeChanged(object sender, EventArgs e)
        {
            foreach (BQDrugs c in flowLayoutPanel1.Controls)
            {
                c.Width = flowLayoutPanel1.Width;
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (ischange)
            {
                bool ched = false;
                bool state = true;
                piv.prints[Labelno] = checkBox1.Checked;
                foreach (var item in piv.prints)
                {
                    if (item.Value)
                        ched = true;
                    else
                        state = false;
                }
                piv.checkBox2.CheckState = ched ? (state ? CheckState.Checked : CheckState.Indeterminate) : CheckState.Unchecked;
            }
        }
        

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ischange = true;
        }

        private void BQlabel_Enter(object sender, EventArgs e)
        {
            pictureBox1.BackColor = (panel2.BackColor == Color.LightGreen) ? Color.SeaGreen : Color.Gray;
        }

        private void BQlabel_Leave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent;
        }
    }
}
