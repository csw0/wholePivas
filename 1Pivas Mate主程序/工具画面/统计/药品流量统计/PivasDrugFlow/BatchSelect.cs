using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PivasDrugFlow
{
    public partial class BatchSelect : Form
    {
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MOVE = 0xF010;
        private const int HTCAPTION = 0x0002;

        private MainForm mf;
        private bool check;
        private string InPutTX = string.Empty;
        private DB_Help DB = new DB_Help();
        public BatchSelect(MainForm mf, string val)
        {
            this.mf = mf;
            InPutTX = val;
            InitializeComponent();
        }

        private void BatchSelect_Load(object sender, EventArgs e)
        {
            if (InPutTX == "批次选择")
            {
                using (DataSet ds = DB.GetPIVAsDB(string.Format("SELECT Batch,TeamNumber FROM [dbo].[IVRecord] where DATEDIFF(DAY,InfusionDT,'{0}')<=0 and DATEDIFF(DAY,InfusionDT,'{1}')>=0 and BatchSaved=1 and LabelOver>-1 {2} GROUP BY Batch,TeamNumber order by Batch", mf.dateTimePicker1.Value.ToString("yyyy-MM-dd"), mf.dateTimePicker2.Value.ToString("yyyy-MM-dd"), mf.checkBox3.Checked ? string.Empty : " and IVStatus>3 ")))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        button3.Enabled = true;
                        treeView1.CheckBoxes = true;
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            TreeNode tn = new TreeNode(dr[0].ToString().Trim());
                            tn.Checked = mf.SelectBatch.Contains("" + dr[0].ToString() + "");
                            treeView1.Nodes.Add(tn);
                            if (!comboBox1.Items.Contains("所有" + dr[1].ToString().Trim() + "批"))
                            {
                                comboBox1.Items.Add("所有" + dr[1].ToString().Trim() + "批");
                            }
                        }
                    }
                    else
                    {
                        TreeNode tn = new TreeNode("此时间段内无瓶签数据");
                        treeView1.Nodes.Add(tn);
                        treeView1.CheckBoxes = false;
                        button3.Enabled = false;
                    }
                }
                comboBox1.Visible = true;
            }
            else if (InPutTX == "年龄选择")
            {
                button3.Enabled = true;
                treeView1.CheckBoxes = true;
                foreach (string i in new string[] { "新生儿", "婴儿", "幼儿", "儿童", "成人", "老年" })
                {
                    TreeNode tn = new TreeNode(i.ToString());
                    tn.Checked = mf.SelectAge.Contains("" + i.ToString() + "");
                    treeView1.Nodes.Add(tn);
                }
                comboBox1.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string temp = string.Empty;
            foreach (TreeNode tn in treeView1.Nodes)
            {
                if (tn.Checked)
                {
                    temp = temp + tn.Text + ",";
                }
            }
            if (InPutTX == "批次选择")
                mf.SelectBatch = temp;
            else if (InPutTX == "年龄选择")
                mf.SelectAge = temp;
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            check = !check;
            foreach (TreeNode tn in treeView1.Nodes)
            {
                tn.Checked = check;
            }
        }

        private void BatchSelect_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    {
                        foreach (TreeNode tn in treeView1.Nodes)
                        {
                            tn.Checked = false;
                        }
                        break;
                    }
                case 1:
                    {
                        foreach (TreeNode tn in treeView1.Nodes)
                        {
                            if (tn.Text.Contains("#"))
                            {
                                tn.Checked = true;
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        foreach (TreeNode tn in treeView1.Nodes)
                        {
                            if (tn.Text.Contains("K"))
                            {
                                tn.Checked = true;
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        foreach (TreeNode tn in treeView1.Nodes)
                        {
                            if (tn.Text.Contains("L"))
                            {
                                tn.Checked = true;
                            }
                        }
                        break;
                    }
                default:
                    {
                        foreach (TreeNode tn in treeView1.Nodes)
                        {
                            if (tn.Text.Contains(comboBox1.SelectedItem.ToString().Replace("所有", string.Empty).Replace("批", string.Empty)))
                            {
                                tn.Checked = true;
                            }
                        }
                        break;
                    }
            }
        }
    }
}
