using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace SetSyn
{
    public partial class DeleteTable : UserControl
    {
        private DB_Help db = new DB_Help();
        public DeleteTable()
        {
            InitializeComponent();
        }

        private void DeleteTable_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = db.GetPIVAsDB("select name as '表名' from sys.tables where name like'%IVRecord%' or name like '%Prescription%' or name like '%Log%' order by name");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    treeView1.Nodes.Add(dr[0].ToString(), dr[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
                foreach (TreeNode tn in treeView1.Nodes)
                {
                    tn.Checked = true;
                }
            else if(checkBox1.CheckState==CheckState.Unchecked)
                foreach (TreeNode tn in treeView1.Nodes)
                {
                    tn.Checked = false;
                }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            bool allcheck = true;
            bool alluncheck = false;
            foreach (TreeNode tn in treeView1.Nodes)
            {
                if (e.Node.Checked && Equals(tn.Text.Trim(), e.Node.Text.Trim() + "Detail"))
                    tn.Checked = true;
                if (!e.Node.Checked && Equals(tn.Text.Trim() + "Detail", e.Node.Text.Trim()))
                    tn.Checked = false;
                allcheck = tn.Checked && allcheck;
                alluncheck = tn.Checked || alluncheck;
            }
            if (allcheck)
            {
                checkBox1.CheckState = CheckState.Checked;
            }
            else
            {
                if (alluncheck)
                    checkBox1.CheckState = CheckState.Indeterminate;
                else
                    checkBox1.CheckState = CheckState.Unchecked;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Unchecked)
            {
                MessageBox.Show("未选择任何表");
            }
            else
            {
                if (treeView1.Nodes["Prescription"].Checked)
                {
                    if (MessageBox.Show("选中处方表将默认删除处方明细，瓶签，瓶签明细表", "确认删除", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        treeView1.Nodes["PrescriptionDetail"].Checked = true;
                        treeView1.Nodes["IVRecord"].Checked = true;
                        treeView1.Nodes["IVRecordDetail"].Checked = true;
                    }
                    else
                        treeView1.Nodes["Prescription"].Checked = false;
                }
                if (!treeView1.Nodes["PrescriptionDetail"].Checked || !treeView1.Nodes["IVRecord"].Checked || !treeView1.Nodes["IVRecordDetail"].Checked)
                {
                    treeView1.Nodes["Prescription"].Checked = false;
                }
                if (MessageBox.Show("确认删除选中表的信息?", "确认删除", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (TreeNode tn in treeView1.Nodes)
                    {
                        if (tn.Checked)
                        {
                            if (treeView1.Nodes.ContainsKey(tn.Text + "Detail"))
                                db.SetPIVAsDB(string.Format("delete from {0}", tn.Text + "Detail"));
                            if (Equals(tn.Text, "Prescription"))
                            {
                                db.SetPIVAsDB(string.Format("delete from IVRecordDetail"));
                                db.SetPIVAsDB(string.Format("delete from IVRecord"));
                            }
                            db.SetPIVAsDB(string.Format("delete from {0}", tn.Text));

                        }
                    }
                    MessageBox.Show("删除成功");
                }
            }
        }
    }
}
