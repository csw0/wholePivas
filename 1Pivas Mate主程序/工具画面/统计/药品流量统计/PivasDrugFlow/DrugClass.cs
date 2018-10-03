using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PivasDrugFlow
{
    public partial class DrugClass : Form
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
        private string ClassCode = string.Empty;
        private DB_Help DB = new DB_Help();

        public DrugClass(MainForm mf)
        {
            this.mf = mf;
            InitializeComponent();
        }

        private void DrugClass_Load(object sender, EventArgs e)
        {
            using (DataSet all = DB.GetPIVAsDB("SELECT distinct [MedicineClassID],[MedicineClassCode],[MedicineClassName],[ParentID] FROM [KD0100]..[MedicineClass]"))
            {
                if (all != null && all.Tables.Count > 0)
                {
                    TreeNode tn = new TreeNode("所有");
                    tn.Tag = "0";
                    treeView1.Nodes.Add(tn);
                    load(all.Tables[0], tn);
                    tn.Expand();
                }
            }
        }

        private void load(DataTable dt, TreeNode tn)
        {
            foreach (DataRow dr in dt.Select(string.Format("ParentID='{0}'", tn.Tag)))
            {
                TreeNode tns = new TreeNode(dr["MedicineClassName"].ToString().Trim());
                tns.Tag = dr["MedicineClassID"].ToString().Trim();
                tn.Nodes.Add(tns);
                load(dt, tns);
                tns.Checked = mf.DrugForClass.Contains("'" + tns.Tag.ToString() + "'");
            }
        }

        private void CheckTreeNode(TreeNode tn)
        {
            foreach (TreeNode tns in tn.Nodes)
            {
                if (tns.Checked)
                {
                    ClassCode = "'" + tns.Tag + "'," + ClassCode;
                }
                if (tns.Nodes.Count > 0)
                {
                    CheckTreeNode(tns);
                }
            }
        }

        private void checkTn(TreeNode tn)
        {
            foreach (TreeNode tns in tn.Nodes)
            {
                tns.Checked = tn.Checked;
                checkTn(tns);
            }
        }

        private void DrugClass_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mf.Drugs.Rows.Clear();
            if (treeView1.Nodes[0].Checked)
            {
                mf.DrugForClass = string.Empty;
            }
            else
            {
                CheckTreeNode(treeView1.Nodes[0]);
                mf.DrugForClass = ClassCode.TrimEnd(',');
            }
            this.Dispose();
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (!check)
            {
                check = true;
                TreeNode tn = e.Node.Parent;
                if (e.Node.Checked)
                {
                    if (tn != null)
                    {
                        bool all = true;
                        foreach (TreeNode tns in tn.Nodes)
                        {
                            if (!tns.Checked)
                            {
                                all = false;
                            }
                        }
                        tn.Checked = all;
                    }
                }
                else
                {
                    while (tn != null)
                    {
                        tn.Checked = false;
                        tn = tn.Parent;
                    }
                }
                checkTn(e.Node);
                check = false;
            }
        }
    }
}
