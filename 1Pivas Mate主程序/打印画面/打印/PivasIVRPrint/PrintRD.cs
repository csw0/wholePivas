using System;
using System.Windows.Forms;
using System.Data;

namespace PivasIVRPrint
{
    public partial class PrintRD : Form
    {
        private UserControlPrint printed;
        public PrintRD(UserControlPrint printed)
        {
            this.printed = printed;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printed.printed = true;
            printed.printDgT = checkBox2.Checked;
            printed.SendPrt = checkBox1.Checked;
            printed.WhyRePrint = panel1.Visible ? (comboBox1.SelectedItem.ToString() + "-" + textBox1.Text) : string.Empty;
            this.Dispose(true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            printed.printed = false;
            this.Dispose(true);
        }

        private void PrintRD_Load(object sender, EventArgs e)
        {
            checkBox2.Checked = printed.PrintRD > 1;
            checkBox2.Visible = printed.PrintRD > 0;
            panel1.Visible = printed.comboBox1.SelectedIndex > 0;
            this.Height = panel1.Visible ? 260 : 145;
            using (DataSet ds = printed.dbHelp.GetPIVAsDB("SELECT distinct [ErrParent] FROM [dbo].[DPrint_Err]"))
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        comboBox1.Items.Add(dr[0].ToString());
                    }
                }
                else
                {
                    comboBox1.Items.Add("打印故障/中断");
                    comboBox1.Items.Add("标签遗失/损坏");
                    comboBox1.Items.Add("溶媒不符");
                    comboBox1.Items.Add("药品不符");
                    comboBox1.Items.Add("药物数量不符");
                }
                if (!comboBox1.Items.Contains("其他"))
                {
                    comboBox1.Items.Add("其他");
                }
                comboBox1.SelectedIndex = 0;
            }
        }
    }
}
