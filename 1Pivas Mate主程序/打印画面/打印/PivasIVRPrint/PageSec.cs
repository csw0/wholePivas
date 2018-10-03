using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PivasIVRPrint
{
    public partial class PageSec : Form
    {
        private UserControlPrint p;
        public PageSec(UserControlPrint p)
        {
            this.p = p;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int sl = 0;
            int el = 0;
            try
            {
                sl = Convert.ToInt32(textBox1.Text);
                el = Convert.ToInt32(textBox2.Text);
                if (sl >= p.stalimt && el <= p.endlimt && sl <= el)
                {
                    if (p.flowLayoutPanel2.HasChildren)
                    {
                        switch (UserControlPrint.PreviewMode)
                        {
                            case 0:
                                {
                                    BQlabelDataSet gdv = (BQlabelDataSet)p.flowLayoutPanel2.Controls[0];
                                    foreach (DataGridViewRow dgr in gdv.dataGridView2.Rows)
                                    {
                                        if (dgr.Index >= sl - 1 && dgr.Index < el)
                                        {
                                            dgr.Cells["checkbox"].Value = true;
                                        }
                                        else
                                        {
                                            dgr.Cells["checkbox"].Value = false;
                                        }
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    BQlabelDataSet gdv = (BQlabelDataSet)p.flowLayoutPanel2.Controls[0];
                                    foreach (DataGridViewRow dgr in gdv.dataGridView2.Rows)
                                    {
                                        if (dgr.Index >= sl - 1 && dgr.Index < el)
                                        {
                                            dgr.Cells["checkbox"].Value = true;
                                        }
                                        else
                                        {
                                            dgr.Cells["checkbox"].Value = false;
                                        }
                                    }
                                    break;
                                }
                            case 2:
                                List<string> ls = new List<string>(p.prints.Keys);
                                foreach (string key in ls)
                                {
                                    if (ls.IndexOf(key) >= sl - 1 && ls.IndexOf(key) < el)
                                    {
                                        p.prints[key] = true;
                                    }
                                    else
                                    {
                                        p.prints[key] = false;
                                    }
                                }
                                BQDetail bq = (BQDetail)p.flowLayoutPanel2.Controls[0];
                                foreach (BQlabel c in bq.panel1.Controls)
                                {
                                    c.checkBox1.Checked = p.prints[c.label1.Text];
                                }
                                break;
                        }
                        this.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("输入的数字不符合范围");
                    textBox1.Text = p.stalimt.ToString();
                    textBox2.Text = p.endlimt.ToString();
                }
            }
            catch
            {
                MessageBox.Show("请输入数字！");
            }
        }

        private void PageSec_Load(object sender, EventArgs e)
        {
            this.Location = new System.Drawing.Point(p.FindForm().Location.X + p.panelLeft.Width + p.button4.Location.X, p.FindForm().Location.Y + 100 + p.button4.Location.Y);
            textBox1.Text = p.stalimt.ToString();
            textBox2.Text = p.endlimt.ToString();
        }
    }
}
