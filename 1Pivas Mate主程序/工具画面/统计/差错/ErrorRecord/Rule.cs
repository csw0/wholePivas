using System;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace ErrorRecord
{
    public partial class Rule : Form
    {
        int bup = 0;
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MOVE = 0xF010;
        private const int HTCAPTION = 0x0002;
        private const int CS_DropSHADOW = 0x20000;
        private const int GCL_STYLE = (-26);
        DB_Help DB = new DB_Help();
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public Rule()
        {
            InitializeComponent();
        }

        private void Rule_Load(object sender, EventArgs e)
        {
            Dgv1_Lord();
        }
        private void Dgv1_Lord()
        {
            string str = "select distinct StatusCode '节点编号',StatusName '节点' from ErrorRule";
            DataTable dt = DB.GetPIVAsDB(str).Tables[0];
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = dt;
            dataGridView1.Columns["节点"].Width = 230;
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (e.RowIndex >= 0 || e.ColumnIndex >= 0)
                {
                    if (dataGridView1.Rows[e.RowIndex].Selected == false)
                    {
                        dataGridView1.ClearSelection();
                        dataGridView1.Rows[e.RowIndex].Selected = true;
                        dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    if (dataGridView1.SelectedRows.Count == 1)
                    {
                        contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                        //dataGridView1.CurrentRow.Selected = false;
                        //dataGridView1.Rows[e.RowIndex].Selected = true;
                        //dgv2_Lord();
                    }
                }
            }
            dgv2_Lord();
        }

        private void dgv2_Lord()
        {
            int i = dataGridView1.CurrentCell.RowIndex;
            string str = " select TypeCode '类型编号',TypeName '类型' from ErrorRule where StatusName='" + dataGridView1.Rows[i].Cells["节点"].Value + "' order by ID";
            DataTable dt = DB.GetPIVAsDB(str).Tables[0];
            textBox1.Text = dataGridView1.Rows[i].Cells["节点"].Value.ToString();
            dataGridView2.Columns.Clear();
            dataGridView2.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int p = dataGridView1.CurrentCell.RowIndex;
            if (dataGridView1.CurrentCell.OwningColumn.HeaderText == "修改")
            {
                string sql = "update ErrorRule set StatusName='" + dataGridView1.Rows[p].Cells["节点"].Value + "'where StatusCode='" + dataGridView1.Rows[p].Cells["节点编号"].Value + "'";
                DB.SetPIVAsDB(sql);
            }
            if (dataGridView1.CurrentCell.OwningColumn.HeaderText == "删除")
            {
                string sql = "delete from ErrorRule where StatusCode='" + dataGridView1.Rows[p].Cells["节点编号"].Value + "'";
                DB.SetPIVAsDB(sql);
                Dgv1_Lord();
            }
           // LordTapAdd();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int p = dataGridView1.CurrentCell.RowIndex;
            int i = dataGridView2.CurrentCell.RowIndex;
            if (dataGridView2.CurrentCell.OwningColumn.HeaderText == "修改")
            {
                string sql = "update ErrorRule set TypeName='" + dataGridView2.Rows[i].Cells["类型"].Value + "'where TypeCode='" + dataGridView2.Rows[i].Cells["类型编号"].Value + "' and StatusCode ='" + dataGridView1.Rows[p].Cells["节点编号"].Value + "'";
                DB.SetPIVAsDB(sql);
            }
            if (dataGridView2.CurrentCell.OwningColumn.HeaderText == "删除")
            {
                if (dataGridView2.Rows.Count == 1)
                {
                    DialogResult del = MessageBox.Show("这是该节点最后一个类型，删除将会导致该节点删除，是否删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                    if (del == DialogResult.OK)
                    {
                        string sql = "delete from ErrorRule where TypeCode='" + dataGridView2.Rows[i].Cells["类型编号"].Value + "' and StatusCode ='" + dataGridView1.Rows[p].Cells["节点编号"].Value + "'";
                        DB.SetPIVAsDB(sql);
                        dataGridView2.Rows.Remove(dataGridView2.Rows[i]);
                        Dgv1_Lord();
                    }

                }
                else
                {
                    string sql = "delete from ErrorRule where TypeCode='" + dataGridView2.Rows[i].Cells["类型编号"].Value + "' and StatusCode ='" + dataGridView1.Rows[p].Cells["节点编号"].Value + "'";
                    DB.SetPIVAsDB(sql);
                    dataGridView2.Rows.Remove(dataGridView2.Rows[i]);
                    Dgv1_Lord();
                }
            }
            //LordTapAdd();
        }

        private void ButtonAdd_Click()
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("节点不能为空！");
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("类型不能为空！");
            }
            else
            {
                string str = "select distinct StatusCode,isnull(MAX(TypeCode),0)+1 from ErrorRule where StatusName='" + textBox1.Text + "' group by StatusCode ";
                DataTable dt = DB.GetPIVAsDB(str).Tables[0];
                string sql;
                if (dt.Rows.Count == 0)
                {
                    sql = "insert into ErrorRule(StatusName,StatusCode,TypeCode,TypeName) select '" + textBox1.Text + "',MAX(statuscode)+1,'01','" + textBox2.Text + "' from ErrorRule";
                    DB.SetPIVAsDB(sql);
                    Dgv1_Lord();
                }
                else
                {
                    sql = "insert into ErrorRule Values ('" + textBox1.Text + "','" + dt.Rows[0][0] + "','" + dt.Rows[0][1] + "','" + textBox2.Text + "')";
                    DB.SetPIVAsDB(sql);
                    dgv2_Lord();
                }

            }
            //LordTapAdd();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                dgv2_Lord();
            }
        }

        private void Rule_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            Dispose();
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.Button == System.Windows.Forms.MouseButtons.Right)
            //{
            //    if (e.RowIndex >= 0 || e.ColumnIndex >= 0)
            //    {
            //        if (dataGridView1.Rows[e.RowIndex].Selected == false)
            //        {
            //            dataGridView1.ClearSelection();
            //            dataGridView1.Rows[e.RowIndex].Selected = true;
            //        }
            //        if (dataGridView1.SelectedRows.Count == 1)
            //        {
            //            contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
            //        }
            //    }
            //}
                
        }
        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            label2.Text = "类型：";
            button1.Text = "添 加";
            textBox1.ReadOnly = false;
            bup = 0;
            //button1.Click += new System.EventHandler(this.ButtonAdd_Click);
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int p = dataGridView1.CurrentRow.Index;
            textBox1.Text = dataGridView1.Rows[p].Cells["节点"].Value.ToString();
            label2.Text = " →";
            button1.Text = "修 改";
            textBox1.ReadOnly = true;
            bup = 1;
            //button1.Click += new System.EventHandler(this.dataGridView1_update);
            textBox2.Text = string.Empty;
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int p = dataGridView1.CurrentRow.Index;
            string sql = "delete from ErrorRule where StatusCode='" + dataGridView1.Rows[p]. Cells["节点编号"].Value + "'";
            DB.SetPIVAsDB(sql);
            Dgv1_Lord();
        }

        private void dataGridView1_update()
        {
            int p = dataGridView1.CurrentCell.RowIndex;
            string sql = "update ErrorRule set StatusName='" + textBox2.Text + "'where StatusCode='" + dataGridView1.Rows[p].Cells["节点编号"].Value + "'";
            DB.SetPIVAsDB(sql);
            Dgv1_Lord();
            
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (e.RowIndex >= 0 || e.ColumnIndex >= 0)
                {
                    if (dataGridView2.Rows[e.RowIndex].Selected == false)
                    {
                        dataGridView2.ClearSelection();
                        dataGridView2.Rows[e.RowIndex].Selected = true;
                        dataGridView2.CurrentCell = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        
                    }
                    if (dataGridView2.SelectedRows.Count == 1)
                    {
                        contextMenuStrip2.Show(MousePosition.X, MousePosition.Y);
                    }
                }
            }
        }

        private void 添加ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["节点"].Value.ToString();
            label2.Text = "类型：";
            button1.Text = "添 加";
            textBox1.ReadOnly = true;
            bup = 0;
           // button1.Click += new System.EventHandler(this.ButtonAdd_Click);
        }

        private void 修改ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int p = dataGridView2.CurrentRow.Index;
            textBox1.Text = dataGridView2.Rows[p].Cells["类型"].Value.ToString();
            label2.Text = " →";
            button1.Text = "修 改";
            textBox1.ReadOnly = true;
            bup = 2;
            //button1.Click += new System.EventHandler(this.dataGridView2_update);
            textBox2.Text = string.Empty;
        }

        private void dataGridView2_update()
        {
            int p = dataGridView1.CurrentCell.RowIndex;
            int q = dataGridView2.CurrentCell.RowIndex;
            string sql = "update ErrorRule set TypeName='" + textBox2.Text + "'where StatusCode='" + dataGridView1.Rows[p].Cells["节点编号"].Value + "' and TypeCode = '" + dataGridView2.Rows[q].Cells["类型编号"].Value + "'";
            DB.SetPIVAsDB(sql);
            dgv2_Lord();

        }

        private void 删除ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int p = dataGridView1.CurrentCell.RowIndex;
            int i = dataGridView2.CurrentCell.RowIndex;
            if (dataGridView2.Rows.Count == 1)
            {
                DialogResult del = MessageBox.Show("这是该节点最后一个类型，删除将会导致该节点删除，是否删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (del == DialogResult.OK)
                {
                    string sql = "delete from ErrorRule where TypeCode='" + dataGridView2.Rows[i].Cells["类型编号"].Value + "' and StatusCode ='" + dataGridView1.Rows[p].Cells["节点编号"].Value + "'";
                    DB.SetPIVAsDB(sql);
                    dataGridView2.Rows.Remove(dataGridView2.Rows[i]);
                    Dgv1_Lord();
                }

            }
            else
            {
                string sql = "delete from ErrorRule where TypeCode='" + dataGridView2.Rows[i].Cells["类型编号"].Value + "' and StatusCode ='" + dataGridView1.Rows[p].Cells["节点编号"].Value + "'";
                DB.SetPIVAsDB(sql);
                dataGridView2.Rows.Remove(dataGridView2.Rows[i]);
                dgv2_Lord();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (bup==1)
            {
                dataGridView1_update();
            }
            else if(bup==2)
            {
                dataGridView2_update();
            }
            else if (bup == 0)
            {
                ButtonAdd_Click();
            }
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            panel1.BackColor = System.Drawing.Color.OrangeRed;
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            panel1.BackColor = System.Drawing.Color.Transparent;
        }
        
    }
}
