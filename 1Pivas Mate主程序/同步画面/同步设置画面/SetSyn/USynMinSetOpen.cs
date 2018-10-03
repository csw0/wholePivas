using System;
using System.Data;
using System.Windows.Forms;

namespace SetSyn
{
    public partial class USynMinSetOpen : UserControl
    {
        protected internal USynMin usm;
        public USynMinSetOpen()
        {
            InitializeComponent();
        }
        public void USynMinSetOpen_Load(object sender, EventArgs e)
        {
            try
            {
                if (usm != null)
                {
                    switch (usm.Tag.ToString())
                    {
                        case "1":
                            {
                                dataGridView1.Enabled = true;
                                comboBox6.Enabled = false;
                                break;
                            }
                        case "2":
                            {
                                dataGridView1.Enabled = true;
                                comboBox6.Enabled = true;
                                break;
                            }
                        case "3":
                            {
                                dataGridView1.Enabled = false;
                                comboBox6.Enabled = true;
                                break;
                            }
                    }
                }
                if (!string.IsNullOrEmpty(USynMinSet.begin) && !string.IsNullOrEmpty(USynMinSet.end))
                {
                    using (DataTable dt = new DataTable())
                    {
                        dt.Columns.Add("begin", typeof(string));
                        dt.Columns.Add("end", typeof(string));
                        BeginTm.DataPropertyName = "begin";
                        BeginTm.DisplayMember = "begin";
                        BeginTm.ValueMember = "begin";

                        EndTm.DataPropertyName = "end";
                        EndTm.DisplayMember = "end";
                        EndTm.ValueMember = "end";
                        if (USynMinSet.begin.Contains(",") && USynMinSet.end.Contains(","))
                        {
                            string[] bs = USynMinSet.begin.Split(',');
                            string[] es = USynMinSet.end.Split(',');
                            if (bs.Length == es.Length)
                            {
                                if (usm.Tag.ToString() == "1")
                                {
                                    dt.Rows.Add(bs[0], es[0]);
                                }
                                else
                                {
                                    for (int i = 0; i < bs.Length; i++)
                                    {
                                        dt.Rows.Add(bs[i], es[i]);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!USynMinSet.begin.Contains(":"))
                            {
                                USynMinSet.begin = USynMinSet.begin + ":00";
                                USynMinSet.end = USynMinSet.end + ":00";
                            }
                            dt.Rows.Add(USynMinSet.begin, USynMinSet.end.Contains("24") ? USynMinSet.end.Replace("24", "23").Replace("00", "59") : USynMinSet.end);
                        }
                        dataGridView1.DataSource = dt;
                        dataGridView1.Refresh();
                    }
                }
                if (USynMinSet.space >= 0)
                    comboBox6.SelectedIndex = comboBox6.Items.IndexOf(USynMinSet.space.ToString());
                else
                    comboBox6.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.RowCount > 1)
            {
                if (e.RowIndex == 0)
                {
                    if (Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[0].Value) >= Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value))
                    {
                        if (e.ColumnIndex == 0)
                        {
                            dataGridView1.Rows[e.RowIndex].Cells[0].Value = BeginTm.Items[EndTm.Items.IndexOf(dataGridView1.Rows[e.RowIndex].Cells[1].Value)];
                        }
                        else
                        {
                            dataGridView1.Rows[e.RowIndex].Cells[1].Value = EndTm.Items[BeginTm.Items.IndexOf(dataGridView1.Rows[e.RowIndex].Cells[0].Value)];
                        }
                    }
                    else
                    {
                        if (Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value) > Convert.ToDateTime(dataGridView1.Rows[1].Cells[0].Value))
                        {
                            dataGridView1.Rows[e.RowIndex].Cells[1].Value = dataGridView1.Rows[1].Cells[0].Value;
                        }
                    }
                }
                else if (e.RowIndex == (dataGridView1.RowCount - 1))
                {
                    if (Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[0].Value) >= Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value))
                    {
                        if (e.ColumnIndex == 0)
                        {
                            dataGridView1.Rows[e.RowIndex].Cells[0].Value = BeginTm.Items[EndTm.Items.IndexOf(dataGridView1.Rows[e.RowIndex].Cells[1].Value)];
                        }
                        else
                        {
                            dataGridView1.Rows[e.RowIndex].Cells[1].Value = EndTm.Items[BeginTm.Items.IndexOf(dataGridView1.Rows[e.RowIndex].Cells[0].Value)];
                        }
                    }
                    else
                    {
                        if (Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[0].Value) < Convert.ToDateTime(dataGridView1.Rows[dataGridView1.RowCount - 2].Cells[1].Value))
                        {
                            dataGridView1.Rows[e.RowIndex].Cells[0].Value = dataGridView1.Rows[dataGridView1.RowCount - 2].Cells[1].Value;
                        }
                    }
                }
                else 
                {
                    if (e.ColumnIndex == 0)
                    {
                        if (Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[0].Value) < Convert.ToDateTime(dataGridView1.Rows[dataGridView1.Rows[e.RowIndex].Index - 1].Cells[1].Value))
                        {
                            dataGridView1.Rows[e.RowIndex].Cells[0].Value = dataGridView1.Rows[dataGridView1.Rows[e.RowIndex].Index - 1].Cells[1].Value;
                        }
                        else if (Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[0].Value) >= Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value))
                        {
                            dataGridView1.Rows[e.RowIndex].Cells[0].Value = BeginTm.Items[EndTm.Items.IndexOf(dataGridView1.Rows[e.RowIndex].Cells[1].Value)];
                        }
                    }
                    else
                    {
                        if (Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value) > Convert.ToDateTime(dataGridView1.Rows[dataGridView1.Rows[e.RowIndex].Index + 1].Cells[0].Value))
                        {
                            dataGridView1.Rows[e.RowIndex].Cells[1].Value = dataGridView1.Rows[dataGridView1.Rows[e.RowIndex].Index + 1].Cells[0].Value;
                        }
                        else if (Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[0].Value) >= Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value))
                        {
                            dataGridView1.Rows[e.RowIndex].Cells[1].Value = EndTm.Items[BeginTm.Items.IndexOf(dataGridView1.Rows[e.RowIndex].Cells[0].Value)];
                        }
                    }
                }
            }
            else
            {
                if (Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[0].Value) >= Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value))
                {
                    if (e.ColumnIndex == 0)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[0].Value = BeginTm.Items[EndTm.Items.IndexOf(dataGridView1.Rows[e.RowIndex].Cells[1].Value)];
                    }
                    else
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[1].Value = EndTm.Items[BeginTm.Items.IndexOf(dataGridView1.Rows[e.RowIndex].Cells[0].Value)];
                    }
                }
            }
        }


        protected internal void save()
        {
            USynMinSet.begin = string.Empty;
            USynMinSet.end = string.Empty;
            int.TryParse(comboBox6.SelectedItem.ToString(), out USynMinSet.space);
            foreach (DataGridViewRow dgvr in dataGridView1.Rows)
            {
                USynMinSet.begin = USynMinSet.begin + dgvr.Cells[0].Value + ",";
                USynMinSet.end = USynMinSet.end + dgvr.Cells[1].Value + ",";
            }
            USynMinSet.begin = USynMinSet.begin.TrimEnd(',');
            USynMinSet.end = USynMinSet.end.TrimEnd(',');
        }

        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usm.Tag.ToString() != "1")
            {
                using (DataTable dt = dataGridView1.DataSource as DataTable)
                {
                    DataRow dr = dt.NewRow();
                    if (dataGridView1.CurrentRow.Cells[0].Value.ToString() != "00:00")
                    {
                        if (dataGridView1.CurrentRow.Index == 0)
                        {
                            dr[0] = "00:00";
                            dr[1] = dataGridView1.CurrentRow.Cells[0].Value;
                            dt.Rows.InsertAt(dr, dataGridView1.CurrentRow.Index);
                        }
                        else
                        {
                            if (Convert.ToDateTime(dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[1].Value) < Convert.ToDateTime(dataGridView1.CurrentRow.Cells[0].Value))
                            {
                                dr[0] = dataGridView1.Rows[dataGridView1.CurrentRow.Index - 1].Cells[1].Value;
                                dr[1] = dataGridView1.CurrentRow.Cells[0].Value;
                                dt.Rows.InsertAt(dr, dataGridView1.CurrentRow.Index);
                            }
                            else
                            {
                                if (dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[1].Value.ToString() != EndTm.Items[EndTm.Items.Count - 1].ToString())
                                {
                                    dr[0] = dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[1].Value;
                                    dr[1] = EndTm.Items[EndTm.Items.Count - 1].ToString();
                                    dt.Rows.Add(dr);
                                }
                                else
                                {
                                    MessageBox.Show("在你当前选中的数据行前已无法插入数据,请先修改数据或选择其他数据行");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[1].Value.ToString() != EndTm.Items[EndTm.Items.Count - 1].ToString())
                        {
                            dr[0] = dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[1].Value;
                            dr[1] = EndTm.Items[EndTm.Items.Count - 1].ToString();
                            dt.Rows.Add(dr);
                        }
                        else
                        {
                            MessageBox.Show("在你当前选中的数据行前已无法插入数据,请先修改数据或选择其他数据行");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("每天一次同步只能有一条数据");
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 1)
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            }
            else
            {
                MessageBox.Show("保留至少一条数据");
            }
        }
    }
}
