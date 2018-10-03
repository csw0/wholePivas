using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PivasDrugCash
{
    internal sealed partial class DWardSelect : Form
    {
        private List<TreeNode> ls;
        private bool AllSelect = true;
        internal DWardSelect(ref List<TreeNode> ls)
        {
            this.ls = ls;
            InitializeComponent();
        }

        private void DWardSelect_Load(object sender, EventArgs e)
        {
            foreach (TreeNode tn in ls)
            {
                treeView1.Nodes.Add(tn);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (TreeNode tn in treeView1.Nodes)
            {
                tn.Checked = AllSelect;
            }
            AllSelect = !AllSelect;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            this.DialogResult = DialogResult.OK;
            this.Dispose(true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            this.DialogResult = DialogResult.Cancel;
            this.Dispose(true);
        }
    }
}
