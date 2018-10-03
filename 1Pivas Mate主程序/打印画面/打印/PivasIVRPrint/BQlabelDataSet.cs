using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using PrintBatchUpdate;
using UpdateIVDeskNo;

namespace PivasIVRPrint
{
    public partial class BQlabelDataSet : UserControl
    {
        private UserControlPrint piv;
        private bool locked = true;
        private string Useful1 = string.Empty;
        private string Useful2 = string.Empty;
        private string Useful3 = string.Empty;
        private string Useless1 = string.Empty;
        private string Useless2 = string.Empty;
        private string Useless3 = string.Empty;
        public BQlabelDataSet(UserControlPrint p)
        {
            piv = p;
            InitializeComponent();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == 1)
                {
                    dataGridView2.Rows[e.RowIndex].Cells["checkbox"].Value = !true.Equals(dataGridView2.Rows[e.RowIndex].Cells["checkbox"].Value);
                    bool ched = false;
                    bool state = true;
                    foreach (DataGridViewRow dr in dataGridView2.Rows)
                    {
                        if (true.Equals(dr.Cells["checkbox"].Value))
                            ched = true;
                        else
                            state = false;
                    }
                    piv.checkBox2.CheckState = ched ? (state ? CheckState.Checked : CheckState.Indeterminate) : CheckState.Unchecked;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void BQlabelDataSet_Load(object sender, EventArgs e)
        {
            locked = false;
            dataGridView2.DefaultCellStyle.BackColor = UserControlPrint.NotPrint;
            dataGridView2.DefaultCellStyle.SelectionBackColor = UserControlPrint.NotPrintSelected;
            if (dataGridView2.DataSource != null)
            {
                label1.Text = dataGridView2.RowCount.ToString();
                dataGridView2.Columns["index"].Width = label1.Width + 10;
                dataGridView2.Columns["index"].HeaderText = label1.Text;
                dataGridView2.Columns["瓶签号"].Width = 110;
                dataGridView2.Columns["患者"].Width = 80;
                dataGridView2.Columns["病区"].Width = 120;
                dataGridView2.Columns["批次"].Width = 50;
                dataGridView2.Columns["频次"].Width = 100;
                dataGridView2.Columns["床号"].Width = 60;
                dataGridView2.Columns["瓶签状态"].Width = 100;
                dataGridView2.Columns["主药"].Width = 180;
                dataGridView2.Columns["打印时间"].Width = 120;
                dataGridView2.Columns["打印人"].Width = 50;
                foreach (DataGridViewColumn dc in dataGridView2.Columns)
                {
                    //dc.SortMode = PivasIVRP.PreviewMode == 0 ? DataGridViewColumnSortMode.Automatic : DataGridViewColumnSortMode.NotSortable;
                    dc.Visible = (dc.Index <= dataGridView2.Columns["仓位"].Index);
                }
                dataGridView2.Columns["HIS瓶签"].Visible = (piv.dbHelp.GetPivasAllSet("第三方瓶签") == "1");
                bool check = false;
                bool unicheck = true;
                foreach (DataGridViewRow dgr in dataGridView2.Rows)
                {
                    int c = 0;
                    if (int.TryParse(dgr.Cells["Remark3"].Value.ToString().Trim(), out c) && c >= 10)
                    {
                        dgr.DefaultCellStyle.BackColor = UserControlPrint.HasCheck;
                        dgr.DefaultCellStyle.SelectionBackColor = UserControlPrint.HasCheckSelected;
                    }
                    if (dgr.Cells["IVStatus"].Value.ToString() != "0")
                    {
                        dgr.DefaultCellStyle.BackColor = UserControlPrint.Printed;
                        dgr.DefaultCellStyle.SelectionBackColor = UserControlPrint.PrintSelected;
                    }
                    dgr.Cells["checkbox"].Value = piv.checkBox2.Checked;
                    dgr.Cells["index"].Value = dgr.Index + 1;
                    check = check || true.Equals(dgr.Cells["checkbox"].Value);
                    unicheck = unicheck && true.Equals(dgr.Cells["checkbox"].Value);
                    switch (dgr.Cells["Remark3"].Value.ToString().Trim())
                    {
                        case "0":
                            {
                                dgr.Cells["瓶签状态"].Value = "未记账";
                                break;
                            }
                        case "1":
                            {
                                dgr.Cells["瓶签状态"].Value = piv.Useless1;
                                break;
                            }
                        case "2":
                            {
                                dgr.Cells["瓶签状态"].Value = "耗材未收";
                                break;
                            }
                        case "3":
                            {
                                dgr.Cells["瓶签状态"].Value = piv.Useless2;
                                break;
                            }
                        case "4":
                            {
                                dgr.Cells["瓶签状态"].Value = "配置费失败";
                                break;
                            }
                        case "5":
                            {
                                dgr.Cells["瓶签状态"].Value = "配置费成功";
                                break;
                            }
                        case "6":
                            {
                                dgr.Cells["瓶签状态"].Value = "扣库存成功";
                                break;
                            }
                        case "7":
                            {
                                dgr.Cells["瓶签状态"].Value = "扣库存失败";
                                break;
                            }
                        case "8":
                            {
                                dgr.Cells["瓶签状态"].Value = "打印记账失败";
                                break;
                            }
                        case "9":
                            {
                                dgr.Cells["瓶签状态"].Value = piv.Useless3;
                                break;
                            }
                        case "10":
                            {
                                dgr.Cells["瓶签状态"].Value = "可打印";
                                break;
                            }
                        case "12":
                            {
                                dgr.Cells["瓶签状态"].Value = "配置记账失败";
                                break;
                            }
                        case "13":
                            {
                                dgr.Cells["瓶签状态"].Value = "耗材未收";
                                break;
                            }
                        case "14":
                            {
                                dgr.Cells["瓶签状态"].Value = "耗材已收";
                                break;
                            }
                        case "15":
                            {
                                dgr.Cells["瓶签状态"].Value = "配置记账成功";
                                break;
                            }
                        case "16":
                            {
                                dgr.Cells["瓶签状态"].Value = "已记账";
                                break;
                            }
                        case "17":
                            {
                                dgr.Cells["瓶签状态"].Value = "扣库存成功";
                                break;
                            }
                        case "18":
                            {
                                dgr.Cells["瓶签状态"].Value = "扣库存失败";
                                break;
                            }

                        case "19":
                            {
                                dgr.Cells["瓶签状态"].Value = "配置费成功";
                                break;
                            }
                        case "20":
                            {
                                dgr.Cells["瓶签状态"].Value = "其他";
                                break;
                            }
                        case "21":
                            {
                                dgr.Cells["瓶签状态"].Value = "配置费失败";
                                break;
                            }
                        case "22":
                            {
                                dgr.Cells["瓶签状态"].Value = piv.Useful1;
                                break;
                            }
                        case "23":
                            {
                                dgr.Cells["瓶签状态"].Value = piv.Useful2;
                                break;
                            }
                        case "24":
                            {
                                dgr.Cells["瓶签状态"].Value = piv.Useful3;
                                break;
                            }

                        default:
                            {
                                dgr.Cells["瓶签状态"].Value = "未知";
                                break;
                            }
                    }
                }
                piv.checkBox2.CheckState = check ? (unicheck ? CheckState.Checked : CheckState.Indeterminate) : CheckState.Unchecked;
            }
            dataGridView2_SelectionChanged(sender, e);
        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == dataGridView2.Columns["批次"].Index)
                    {
                        using (BatchUpdate bu = new BatchUpdate(dataGridView2.Rows[e.RowIndex].Cells["瓶签号"].Value.ToString(), piv.userID))
                        {
                            if (bu.ShowDialog() == DialogResult.OK)
                            {
                                if (dataGridView2.Rows[e.RowIndex].Cells["批次"].Value.ToString() != bu.Return())
                                {
                                    dataGridView2.Rows[e.RowIndex].Cells["checkbox"].Value = false;
                                    dataGridView2.Rows[e.RowIndex].Cells["批次"].Value = bu.Return();
                                }
                            }
                        }
                    }

                    if (e.ColumnIndex == dataGridView2.Columns["仓位"].Index)
                    {
                        using (UpdateDeskNo bu = new UpdateDeskNo(dataGridView2.Rows[e.RowIndex].Cells["瓶签号"].Value.ToString(), piv.userID))
                        {
                            if (bu.ShowDialog() == DialogResult.OK)
                            {
                                if (dataGridView2.Rows[e.RowIndex].Cells["仓位"].Value.ToString() != bu.Return())
                                {
                                    dataGridView2.Rows[e.RowIndex].Cells["checkbox"].Value = false;
                                    dataGridView2.Rows[e.RowIndex].Cells["仓位"].Value = bu.Return();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!locked)
                {
                    locked = true;
                    if (dataGridView2.SelectedRows.Count > 0)
                    {
                        piv.selected = dataGridView2.SelectedRows[0].Cells["瓶签号"].Value.ToString();
                        if (UserControlPrint.PreviewMode == 0)
                        {
                            piv.BD.ds = piv.dbHelp.GetPIVAsDB(string.Format("SELECT * FROM ({0} where LabelNo = '{1}') V order by NoName,DosageUnit,Dosage", piv.GetLabelNoSql, piv.selected));
                            piv.BD.BQlabelDetail_Load(sender, e);
                        }
                        else
                        {
                            piv.BS.MShow(piv.selected);
                        }
                    }
                    locked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView2_Sorted(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgr in dataGridView2.Rows)
            {
                int c = 0;
                if (int.TryParse(dgr.Cells["Remark3"].Value.ToString().Trim(), out c) && c >= 10)
                {
                    dgr.DefaultCellStyle.BackColor = UserControlPrint.HasCheck;
                    dgr.DefaultCellStyle.SelectionBackColor = UserControlPrint.HasCheckSelected;
                }
                if (dgr.Cells["IVStatus"].Value.ToString() != "0")
                {
                    dgr.DefaultCellStyle.BackColor = UserControlPrint.Printed;
                    dgr.DefaultCellStyle.SelectionBackColor = UserControlPrint.PrintSelected;
                }
                dgr.Cells["checkbox"].Value = piv.checkBox2.Checked;
                dgr.Cells["index"].Value = dgr.Index + 1;
            }
            piv.checkBox2.CheckState = piv.checkBox2.Checked ? CheckState.Checked : CheckState.Unchecked;
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dataGridView2.Columns[e.ColumnIndex].Name == "床号" && e.RowIndex == -1)
                {
                    DataTable dt = dataGridView2.DataSource as DataTable;
                    if (!dt.Columns.Contains("SortBy"))
                    {
                        dt.Columns.Add("SortBy", typeof(double));
                        dataGridView2.Columns["SortBy"].Visible = false;
                    }
                    DataGridViewColumn newColumn = dataGridView2.Columns[e.ColumnIndex];
                    newColumn.SortMode = DataGridViewColumnSortMode.Programmatic;
                    foreach (DataGridViewRow dgvr in dataGridView2.Rows)
                    {
                        string strs = string.Empty;
                        if (!string.IsNullOrEmpty(dgvr.Cells[e.ColumnIndex].Value.ToString().Trim()))
                        {
                            string intg = string.Empty;
                            foreach (char c in dgvr.Cells[e.ColumnIndex].Value.ToString().Trim().TrimStart('+', '.').Replace("床", string.Empty).ToCharArray())
                            {
                                if (char.IsNumber(c))
                                {
                                    intg = intg + c;
                                }
                                else
                                {
                                    int ic = (int)c;
                                    switch (intg.Length)
                                    {
                                        case 2:
                                            {
                                                intg = "0" + intg;
                                                break;
                                            }
                                        case 1:
                                            {
                                                intg = "00" + intg;
                                                break;
                                            }
                                        default:
                                            {
                                                break;
                                            }
                                    }
                                    if (string.IsNullOrEmpty(intg))
                                    {
                                        strs = ((string.IsNullOrEmpty(strs) ? 0 : long.Parse(strs)) + ic).ToString();
                                    }
                                    else
                                    {
                                        strs = strs + intg + ic;
                                    }
                                    intg = string.Empty;
                                }
                            }
                            switch (intg.Length)
                            {
                                case 2:
                                    {
                                        intg = "0" + intg;
                                        break;
                                    }
                                case 1:
                                    {
                                        intg = "00" + intg;
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            }
                            int i = 0;
                            if (dgvr.Cells[e.ColumnIndex].Value.ToString().Trim().Contains("."))
                            {
                                i = 1;
                            }
                            if (dgvr.Cells[e.ColumnIndex].Value.ToString().Trim().Contains("+"))
                            {
                                i = 2;
                            }
                            strs = strs + "." + intg + i + dgvr.Cells[e.ColumnIndex].Value.ToString().Trim().Length;
                        }
                        dgvr.Cells["SortBy"].Value = string.IsNullOrEmpty(strs) ? "0" : strs;
                    }
                    if (newColumn.HeaderCell.SortGlyphDirection == SortOrder.Ascending)
                    {
                        dataGridView2.Sort(dataGridView2.Columns["SortBy"], ListSortDirection.Descending);
                        newColumn.HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                    else
                    {
                        dataGridView2.Sort(dataGridView2.Columns["SortBy"], ListSortDirection.Ascending);
                        newColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
