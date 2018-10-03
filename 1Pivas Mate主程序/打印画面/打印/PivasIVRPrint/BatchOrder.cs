using PIVAsCommon.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PivasIVRPrint
{
    internal partial class BatchOrder : Form
    {
        private DB_Help db = new DB_Help();
        private bool canch;
        internal BatchOrder()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (treeView1.Nodes[0].ForeColor != Color.Silver)
            {
                sb.Append(string.Format("UPDATE [dbo].[BatchToOrder] SET [OrderBY] = '{0}' WHERE [Batch]='{1}'; ", treeView1.Nodes[0].Checked ? comboBox6.SelectedItem.ToString() : string.Empty, treeView1.Nodes[0].Text));
            }
            foreach (TreeNode tn in treeView1.Nodes[0].Nodes)
            {
                if (tn.ForeColor != Color.Silver)
                {
                    sb.Append(string.Format("UPDATE [dbo].[BatchToOrder] SET [OrderBY] = '{0}' WHERE [Batch]='{1}'; ", tn.Checked ? comboBox6.SelectedItem.ToString() : string.Empty, tn.Text));
                }
            }
            foreach (TreeNode tn in treeView1.Nodes[0].Nodes[0].Nodes)
            {
                if (tn.ForeColor != Color.Silver)
                {
                    sb.Append(string.Format("UPDATE [dbo].[BatchToOrder] SET [OrderBY] = '{0}' WHERE [Batch]='{1}'; ", tn.Checked ? comboBox6.SelectedItem.ToString() : string.Empty, tn.Text));
                }
            }
            foreach (TreeNode tn in treeView1.Nodes[0].Nodes[1].Nodes)
            {
                if (tn.ForeColor != Color.Silver)
                {
                    sb.Append(string.Format("UPDATE [dbo].[BatchToOrder] SET [OrderBY] = '{0}' WHERE [Batch]='{1}'; ", tn.Checked ? comboBox6.SelectedItem.ToString() : string.Empty, tn.Text));
                }
            }
            foreach (TreeNode tn in treeView1.Nodes[0].Nodes[2].Nodes)
            {
                if (tn.ForeColor != Color.Silver)
                {
                    sb.Append(string.Format("UPDATE [dbo].[BatchToOrder] SET [OrderBY] = '{0}' WHERE [Batch]='{1}'; ", tn.Checked ? comboBox6.SelectedItem.ToString() : string.Empty, tn.Text));
                }
            }
            if (!string.IsNullOrEmpty(sb.ToString()))
            {
                if (db.SetPIVAsDB(sb.ToString()) > 0)
                {
                    MessageBox.Show("保存成功");
                }
                else
                {
                    MessageBox.Show("保存失败");
                }
            }
            else
            {
                MessageBox.Show("无需保存");
            }
        }

        private void BatchOrder_Load(object sender, EventArgs e)
        {
            db.SetPIVAsDB("INSERT INTO dbo.BatchToOrder SELECT DISTINCT IV.Batch,'' FROM IVRecord IV LEFT JOIN BatchToOrder BT ON BT.Batch=IV.Batch WHERE BT.Batch IS NULL");
            using (DataSet ds = db.GetPIVAsDB("SELECT [Batch],[OrderBY] FROM [dbo].[BatchToOrder] order by [Batch]"))
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr[0].ToString().Contains("#") && !dr[0].ToString().Contains("所有") && !dr[0].ToString().Contains("L"))
                        {
                            foreach (TreeNode tn in treeView1.Nodes[0].Nodes[0].Nodes)
                            {
                                if (tn.Text == dr[0].ToString())
                                {
                                    goto a;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            treeView1.Nodes[0].Nodes[0].Nodes.Add(new TreeNode(dr[0].ToString()));
                        }
                        else if (dr[0].ToString().Contains("K") && !dr[0].ToString().Contains("所有") && !dr[0].ToString().Contains("L"))
                        {
                            foreach (TreeNode tn in treeView1.Nodes[0].Nodes[1].Nodes)
                            {
                                if (tn.Text == dr[0].ToString())
                                {
                                    goto a;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            treeView1.Nodes[0].Nodes[1].Nodes.Add(new TreeNode(dr[0].ToString()));
                        }
                        else if (dr[0].ToString().Contains("L") && !dr[0].ToString().Contains("所有"))
                        {
                            foreach (TreeNode tn in treeView1.Nodes[0].Nodes[2].Nodes)
                            {
                                if (tn.Text == dr[0].ToString())
                                {
                                    goto a;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            treeView1.Nodes[0].Nodes[2].Nodes.Add(new TreeNode(dr[0].ToString()));
                        }
                    a: { continue; }
                    }
                }
            }
            treeView1.ExpandAll();
            comboBox6.SelectedIndex = 0;
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (DataSet ds = db.GetPIVAsDB("SELECT [Batch],[OrderBY] FROM [dbo].[BatchToOrder] order by [Batch]"))
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    canch = true;
                    treeView1.Nodes[0].ForeColor = ds.Tables[0].Select(string.Format("Batch='{0}' and (OrderBY='{1}' or OrderBY='')", treeView1.Nodes[0].Text, comboBox6.SelectedItem.ToString())).Length > 0 ? Color.Black : Color.Silver;
                    treeView1.Nodes[0].Checked = ds.Tables[0].Select(string.Format("Batch='{0}' and OrderBY='{1}'", treeView1.Nodes[0].Text, comboBox6.SelectedItem.ToString())).Length > 0;

                    foreach (TreeNode tn in treeView1.Nodes[0].Nodes)
                    {
                        tn.ForeColor = ds.Tables[0].Select(string.Format("Batch='{0}' and (OrderBY='{1}' or OrderBY='')", tn.Text, comboBox6.SelectedItem.ToString())).Length > 0 ? Color.Black : Color.Silver;
                        tn.Checked = ds.Tables[0].Select(string.Format("Batch='{0}' and OrderBY='{1}'", tn.Text, comboBox6.SelectedItem.ToString())).Length > 0;
                    }
                    foreach (TreeNode tn in treeView1.Nodes[0].Nodes[0].Nodes)
                    {
                        tn.ForeColor = ds.Tables[0].Select(string.Format("Batch='{0}' and (OrderBY='{1}' or OrderBY='')", tn.Text, comboBox6.SelectedItem.ToString())).Length > 0 ? Color.Black : Color.Silver;
                        tn.Checked = ds.Tables[0].Select(string.Format("Batch='{0}' and OrderBY='{1}'", tn.Text, comboBox6.SelectedItem.ToString())).Length > 0;
                    }
                    foreach (TreeNode tn in treeView1.Nodes[0].Nodes[1].Nodes)
                    {
                        tn.ForeColor = ds.Tables[0].Select(string.Format("Batch='{0}' and (OrderBY='{1}' or OrderBY='')", tn.Text, comboBox6.SelectedItem.ToString())).Length > 0 ? Color.Black : Color.Silver;
                        tn.Checked = ds.Tables[0].Select(string.Format("Batch='{0}' and OrderBY='{1}'", tn.Text, comboBox6.SelectedItem.ToString())).Length > 0;
                    }
                    foreach (TreeNode tn in treeView1.Nodes[0].Nodes[2].Nodes)
                    {
                        tn.ForeColor = ds.Tables[0].Select(string.Format("Batch='{0}' and (OrderBY='{1}' or OrderBY='')", tn.Text, comboBox6.SelectedItem.ToString())).Length > 0 ? Color.Black : Color.Silver;
                        tn.Checked = ds.Tables[0].Select(string.Format("Batch='{0}' and OrderBY='{1}'", tn.Text, comboBox6.SelectedItem.ToString())).Length > 0;
                    }
                    canch = false;
                }
            }
        }

        private void treeView1_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (!canch)
            {
                e.Cancel = e.Node.ForeColor == Color.Silver;
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode tn in e.Node.Nodes)
            {
                tn.Checked = e.Node.Checked;
            }
        }

        private void 清空所有设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (db.SetPIVAsDB("UPDATE [dbo].[BatchToOrder] SET [OrderBY] = ''") > 0)
            {
                comboBox6_SelectedIndexChanged(sender, e);
                MessageBox.Show("已清空所有设置");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked || checkBox4.Checked)
                {
                    List<string> ls = new List<string>();
                    if (checkBox1.Checked)
                    {
                        ls.Add(checkBox1.Text);
                    }
                    if (checkBox2.Checked)
                    {
                        ls.Add(checkBox2.Text);
                    }
                    if (checkBox3.CheckState == CheckState.Checked)
                    {
                        ls.Add(checkBox3.Text);
                    }
                    else if (checkBox3.CheckState == CheckState.Indeterminate)
                    {
                        ls.Add("主药");
                    }
                    if (checkBox4.Checked)
                    {
                        ls.Add(checkBox4.Text);
                    }
                    comboBox6.Items.Clear();
                    List<List<string>> lls = new List<List<string>>();
                    foreach (string s in ls)
                    {
                        List<string>[] lst = lls.ToArray();
                        foreach (List<string> ss in lst)
                        {
                            foreach (string str in ss)
                            {
                                List<string> lss = new List<string>();
                                lss.AddRange(ss.ToArray());
                                lss.Insert(ss.IndexOf(str), s);
                                lls.Add(lss);
                            }
                            List<string> lsss = new List<string>();
                            lsss.AddRange(ss.ToArray());
                            lsss.Add(s);
                            lls.Add(lsss);
                        }
                        lst = lls.ToArray();
                        foreach (List<string> ss in lst)
                        {
                            if (ss.Count < ls.IndexOf(s) + 1)
                            {
                                lls.Remove(ss);
                            }
                        }
                        if (lls.Count == 0)
                        {
                            lls.Add(new List<string>() { s });
                        }
                    }
                    foreach (List<string> lts in lls)
                    {
                        string si = string.Empty;
                        foreach (string s in lts)
                        {
                            si = s + "," + si;
                        }
                        comboBox6.Items.Add(si.TrimEnd(','));
                    }
                    comboBox6.SelectedIndex = 0;
                }
                else
                {
                    checkBox1.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
