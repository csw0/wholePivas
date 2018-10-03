using System;
using System.Data;
using System.Windows.Forms;

namespace PivasDESforEveryHis
{
    internal sealed partial class ADDHIS : Form
    {
        private DataTable DT;
        internal ADDHIS(ref DataSet dt)
        {
            DT = dt.Tables[0];
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose(true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text.Trim()) || string.IsNullOrWhiteSpace(textBox2.Text.Trim()))
            {
                MessageBox.Show("输入不能为空！！！");
            }
            else
            {
                if (DT.Rows.Contains(textBox1.Text.Trim()))
                {
                    MessageBox.Show("表中已包含此ID");
                }
                else
                {
                    DataRow dr = DT.NewRow();
                    dr[0] = textBox1.Text.Trim();
                    dr[1] = textBox2.Text.Trim();
                    DT.Rows.Add(dr);
                    this.DialogResult = DialogResult.OK;
                    this.Dispose(true);
                }
            }
        }
    }
}
