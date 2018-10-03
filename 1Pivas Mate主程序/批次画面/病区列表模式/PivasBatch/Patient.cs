using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using PIVAsCommon.Helper;
using PivasBatchCommon;

namespace PivasBatch
{
    public partial class Patient : UserControl
    {
        
        UserControlBatch batch = new UserControlBatch();
        SelectSql select = new SelectSql();
      //  DataSet ds = new DataSet();
        //病人Code
        string Pcode = string.Empty;
        //医生Code
        public string ECode = "";
        DB_Help db = new DB_Help();
        //病人有改动的索引
        int Sindex;

        DataSet ChangeColords;
        

        public Patient(UserControlBatch batch)
        {
            InitializeComponent();
            this.batch = batch;
        }

        public Patient()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 显示病人列表
        /// </summary>
        public void ShowData()
        {
            ChangeColords = db.GetPIVAsDB(select.IVRecordSetUp(ECode));
            if (Dgv_Patient.Rows.Count > 0)
            {
                Dgv_Patient.Rows[0].Selected = true;          
                    if (batch.Panel_Patient.Visible == true)
                    {
                        batch.Panel_Patient.AutoScroll = false;
                    }

                    NewMethod();
                SelectedInfo();
               
            }
            else
            {
                foreach (Control c in batch.Panel_BatchRule.Controls)
                {
                    c.Dispose();
                }
                GC.Collect();
            }
        }

        /// <summary>
        /// 赋是否病人处方有修改颜色
        /// </summary>
        public void NewMethod()
        {
            try
            {
                if (ChangeColords != null && ChangeColords.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < Dgv_Patient.Rows.Count; i++)
                    {

                        switch (Dgv_Patient.Rows[i].Cells["dgv_IsSame"].Value.ToString())
                        {
                            case "1":
                                Dgv_Patient.Rows[i].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(ChangeColords.Tables[0].Rows[0]["ChangeColor"].ToString());
                                Dgv_Patient.Rows[i].HeaderCell.Style.BackColor = System.Drawing.ColorTranslator.FromHtml(ChangeColords.Tables[0].Rows[0]["ChangeColor"].ToString());
                                break;
                        }
                    }
                }
                else
                {
                    foreach (DataGridViewRow dgr in Dgv_Patient.Rows)
                    {
                        DgvColor(dgr);
                    }              
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10001:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }

        /// <summary>
        /// 赋颜色
        /// </summary>
        /// <param name="dgr"></param>
        private void DgvColor(DataGridViewRow dgr)
        {
            //if (batch.Check_Ivstatus.Checked != true)
            //{
            dgr.DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#ecf5ff");
                dgr.DefaultCellStyle.SelectionBackColor = System.Drawing.ColorTranslator.FromHtml("#9bf2ff");

            //}
            //else
            //{
            //    dgr.DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#d9e1eb");
            //    dgr.DefaultCellStyle.SelectionBackColor = System.Drawing.ColorTranslator.FromHtml("#20e1fe");
            //}


        }


        private void Dgv_Patient_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {

                pvb.operate = true;
                if (e.RowIndex < 0)
                {
                    lieMing = Dgv_Patient.Columns[e.ColumnIndex].HeaderText;
                    return;
                }
                DataGridViewRow dgr = Dgv_Patient.CurrentRow;
                if (Dgv_Patient.SelectedRows != null)
                {
                    Pcode = Dgv_Patient.SelectedRows[0].Cells["dgv_PatCode"].Value.ToString();
                    //DgvColor(dgr);
                    SelectedInfo();

                    //switch (Dgv_Patient.Rows[e.RowIndex].Cells["dgv_IsSame"].Value.ToString())
                    //{
                    //    case "1":
                    //        Sindex = dgr.Index;
                    //        Dgv_Patient.Rows[Sindex].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(ChangeColords.Tables[0].Rows[0]["ChangeColor"].ToString());
                    //        Dgv_Patient.Rows[Sindex].HeaderCell.Style.BackColor = System.Drawing.ColorTranslator.FromHtml(ChangeColords.Tables[0].Rows[0]["ChangeColor"].ToString());
                    //        break;
                    //}
                    //if (dgr.Cells["IsSame"].Value.ToString() == "1")
                    //{
                    //    Sindex = dgr.Index;
                    //}
                }
                else
                {

                }


            }
            catch (System.Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10002:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }
        /// <summary>
        /// 显示具体信息
        /// </summary>
        private void SelectedInfo()
        {
            try
            {
                if (Dgv_Patient.Rows.Count > 0)
                {
                    //如果pcode不等于null的话，就显示选中病人的瓶签信息
                    if (Pcode != null)
                    {
                        batch.OnePatient(Dgv_Patient.CurrentRow.Cells["dgv_PatCode"].Value.ToString(), true);
                    }
                    else
                    {
                        batch.OnePatient(false);
                    }
                    batch.Panel_Patient.AutoScroll = false;
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10029:" + ex.ToString() + "   " + DateTime.Now.ToString() + "\r\n");
            }
          
        }

        private void Dgv_Patient_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            NewMethod();
        }

        private void Dgv_Patient_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode==Keys.Up||e.KeyCode==Keys.Down)
                {
                    pvb.operate = true;

                    DataGridViewRow dgr = Dgv_Patient.CurrentRow;
                    if (Dgv_Patient.SelectedRows != null)
                    {
                        Pcode = Dgv_Patient.SelectedRows[0].Cells["dgv_PatCode"].Value.ToString();
                        //DgvColor(dgr);
                        SelectedInfo();
                        switch (Dgv_Patient.CurrentRow.Cells["dgv_IsSame"].Value.ToString())
                        {
                            case "1":
                                Sindex = dgr.Index;
                                Dgv_Patient.Rows[Sindex].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(ChangeColords.Tables[0].Rows[0]["ChangeColor"].ToString());
                                Dgv_Patient.Rows[Sindex].HeaderCell.Style.BackColor = System.Drawing.ColorTranslator.FromHtml(ChangeColords.Tables[0].Rows[0]["ChangeColor"].ToString());
                                break;
                        }
                        //if (dgr.Cells["IsSame"].Value.ToString() == "1")
                        //{
                        //    Sindex = dgr.Index;
                        //}
                    }
                    //foreach (DataGridViewRow dgr1 in Dgv_Patient.Rows)
                    //{
                    //    DgvColor(dgr);
                    //}

                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10003:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }


        #region 排序--张衡2014-09-28改

      
      
        public class RowComparer : IComparer<DataRow>
        {
            public Dictionary<int, SortOrder> SortColumns { get; set; }

            private static int sortOrderModifier = 1;

            public bool IsNumeric(string str)
            {
                if (str.Length > 10)
                    return false;
                System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
                byte[] bytestr = ascii.GetBytes(str);
                if (bytestr[0] == 43)
                    return true;
                foreach (byte c in bytestr)
                {
                    if ((c < 48 || c > 57))
                    {
                        return false;
                    }
                }
                return true;
            }
            private  int compareStr(string str1, string str2)
            {
                int result = 0;
                if (IsNumeric(str1) && IsNumeric(str2))
                {
                    if (int.Parse(str1) > int.Parse(str2))
                    {
                        result = 1;
                    }
                    else if (int.Parse(str1) < int.Parse(str2))
                    {
                        result = -1;
                    }
                    else
                    {
                        result = 0;
                    }
                }
                else if (IsNumeric(str1) && !IsNumeric(str2))
                {
                    result = -1;
                }
                else if (!IsNumeric(str1) && IsNumeric(str2))
                {
                    result = 1;
                }
                else
                {
                    result = string.Compare(str1, str2);
                }
                return result;
            }
            public RowComparer(SortOrder sortOrder)
            {
                if (sortOrder == SortOrder.Descending)
                {
                    sortOrderModifier = -1;
                }
                else if (sortOrder == SortOrder.Ascending)
                {
                    sortOrderModifier = 1;
                }
            }

            public int Compare(System.Data.DataRow x, System.Data.DataRow y)
            {
                int compareResult = 0;

                foreach (int key in SortColumns.Keys)
                {
                    string value1 = string.Empty;
                    string value2 = string.Empty;
                    //int compareResult;
                    // Check for nulls
                    if (x.ItemArray[key] == DBNull.Value)
                        value1 = "0";
                    else
                    {
                        //value1 = x.ItemArray[key].ToString().TrimEnd('+');
                        value1 = x.ItemArray[key].ToString();
                        if (value1.Contains('.'))
                        {
                            value1 = value1.TrimStart('.');
                        }
                        if (value1.Contains('床'))
                        {
                            value1.Replace("床", "");
                        }
                       //value1= value1.TrimStart('+');
                    }
                    if (y.ItemArray[key] == DBNull.Value)
                        value2 = "0";
                    else
                    {
                        //value2 = y.ItemArray[key].ToString().TrimEnd('+');
                        value2 = y.ItemArray[key].ToString();
                        if (value2.Contains('.'))
                        {
                            value2 = value2.TrimStart('.');
                        }
                        if (value2.Contains('床'))
                        {
                            value2.Replace("床", "");
                        }
                        //value2 = value2.TrimStart('+');
                    }



                    if (value1.Substring(0, 1) == "+" && value2.Substring(0, 1) == "+")
                    {
                        compareResult = compareStr(value1.TrimStart('+'), value2.TrimStart('+'));
                    }
                    else if (value1.Substring(value1.Length - 1) == "+" && value2.Substring(value2.Length - 1) == "+")
                    {
                        compareResult = compareStr(value1.TrimEnd('+'), value2.TrimEnd('+'));
                    }
                    else if ((value1.Substring(0, 1) == "+" && value2.Substring(0, 1) != "+"))
                    {
                        compareResult = -1;
                    }
                    else if ((value1.Substring(0, 1) != "+" && value2.Substring(0, 1) == "+") )
                    {
                        compareResult = 1;
                    }
                    else if (value1.Substring(value1.Length - 1) == "+" && IsNumeric(value2))
                    {
                        compareResult = -1; 
                    }
                    else if (IsNumeric(value1) && value2.Substring(value2.Length - 1)=="+")
                    {
                        compareResult = 1;
                    }
                    else if (value1.Substring(0, 1) != "+" && value2.Substring(0, 1) != "+")
                    {
                        compareResult = compareStr(value1, value2);
                    }
                
                }
                return compareResult * sortOrderModifier;
            }

        }
        
        public DataTable dvtodt(DataGridView dv)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            for (int i = 0; i < dv.Columns.Count; i++)
            {
                dc = new DataColumn();
                dc.ColumnName = dv.Columns[i].DataPropertyName.ToString();
                dt.Columns.Add(dc);
            }
            for (int j = 0; j < dv.Rows.Count; j++)
            {
                DataRow dr = dt.NewRow();
                for (int x = 0; x < dv.Columns.Count; x++)
                {
                    dr[x] = dv.Rows[j].Cells[x].Value;
                }
                dt.Rows.Add(dr);
            }
            dt.Columns.Remove("Column1");
            return dt;
        }
        int m = 0;
        string lieMing;
        private void Dgv_Patient_Sorted(object sender, EventArgs e)
        {

            if (Dgv_Patient != null && lieMing == "床号")
            {
                #region 第一版排序
                ListSortDirection direction = new ListSortDirection();
                DataTable dt = dvtodt(Dgv_Patient);
                Dictionary<int, SortOrder> sortColumns =
                   new Dictionary<int, SortOrder>();
                if (m == 0)
                {
                    RowComparer comp = new RowComparer(SortOrder.Ascending);
                    sortColumns.Add(1, SortOrder.Ascending);
                    direction = ListSortDirection.Ascending;
                    m = 1;

                    comp.SortColumns = sortColumns;
                    var query3 = dt.AsEnumerable().OrderBy(q => q, comp);
                    //DataView dv3 = query3.AsDataView();
                    DataTable dv3 = query3.CopyToDataTable();
                    Dgv_Patient.Rows.Clear();
                    for (int i = 0; i < dv3.Rows.Count; i++)
                    {
                        Dgv_Patient.Rows.Add(1);
                        Dgv_Patient.Rows[i].Cells["dgv_PatName"].Value = dv3.Rows[i]["Column2"].ToString();
                        Dgv_Patient.Rows[i].Cells["dgv_BedNo"].Value = dv3.Rows[i]["Column3"].ToString();
                        Dgv_Patient.Rows[i].Cells["dgv_PatCode"].Value = dv3.Rows[i]["Column4"].ToString();
                        Dgv_Patient.Rows[i].Cells["dgv_UnCheckCount"].Value = dv3.Rows[i]["Column5"].ToString();
                        Dgv_Patient.Rows[i].Cells["dgv_TotalCount"].Value = dv3.Rows[i]["Column6"].ToString();
                        Dgv_Patient.Rows[i].Cells["dgv_BatchSaved"].Value = dv3.Rows[i]["Column7"].ToString();
                        Dgv_Patient.Rows[i].Cells["dgv_IsSame"].Value = dv3.Rows[i]["Column8"].ToString();
                        Dgv_Patient.Rows[i].Cells["IsOpen"].Value = dv3.Rows[i]["Column9"].ToString();
                        Dgv_Patient.Rows[i].Cells["WardCode"].Value = dv3.Rows[i]["Column10"].ToString();

                    }
                    //Dgv_Patient.DataSource = dv3;

                }
                else
                {
                    RowComparer comp = new RowComparer(SortOrder.Descending);
                    sortColumns.Add(1, SortOrder.Descending);
                    direction = ListSortDirection.Descending;
                    m = 0;

                    comp.SortColumns = sortColumns;
                    var query3 = dt.AsEnumerable().OrderBy(q => q, comp);
                    DataTable dv3 = query3.CopyToDataTable();
                    Dgv_Patient.Rows.Clear();
                    for (int i = 0; i < dv3.Rows.Count; i++)
                    {
                        Dgv_Patient.Rows.Add(1);
                        Dgv_Patient.Rows[i].Cells["dgv_PatName"].Value = dv3.Rows[i]["Column2"].ToString();
                        Dgv_Patient.Rows[i].Cells["dgv_BedNo"].Value = dv3.Rows[i]["Column3"].ToString();
                        Dgv_Patient.Rows[i].Cells["dgv_PatCode"].Value = dv3.Rows[i]["Column4"].ToString();
                        Dgv_Patient.Rows[i].Cells["dgv_UnCheckCount"].Value = dv3.Rows[i]["Column5"].ToString();
                        Dgv_Patient.Rows[i].Cells["dgv_TotalCount"].Value = dv3.Rows[i]["Column6"].ToString();
                        Dgv_Patient.Rows[i].Cells["dgv_BatchSaved"].Value = dv3.Rows[i]["Column7"].ToString();
                        Dgv_Patient.Rows[i].Cells["dgv_IsSame"].Value = dv3.Rows[i]["Column8"].ToString();
                        Dgv_Patient.Rows[i].Cells["IsOpen"].Value = dv3.Rows[i]["Column9"].ToString();
                        Dgv_Patient.Rows[i].Cells["WardCode"].Value = dv3.Rows[i]["Column10"].ToString();

                    }
                    //Dgv_Patient.DataSource = dv3;

                }
                Dgv_Patient.Columns["dgv_BedNo"].HeaderCell.SortGlyphDirection = direction == ListSortDirection.Ascending ? SortOrder.Ascending : SortOrder.Descending;
                #endregion

                //#region 第二版排序
                //DataTable dt = dvtodt(Dgv_Patient);
                //DataTable dt1 = dt.Copy();
                //DataTable dt2 = dt.Copy();
                //DataTable dt3 = dt.Copy();
                //DataTable dt4 = dt.Copy();
                //dt1.Clear();
                //dt2.Clear();
                //dt3.Clear();
                //dt4.Clear();
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    if (dt.Rows[i]["Columns3"].ToString().Substring(0, 1) == "+")
                //    {
                //        dt1.Rows.Add(dt.Rows[i]);
                       
                //    }
                //    else if (dt.Rows[i]["Columns3"].ToString().Substring(dt.Rows[i]["Columns3"].ToString().Length - 1, 1) == "+")
                //    {
                //        dt2.Rows.Add(dt.Rows[i]);
                      
                //    }
                //    else if (IsNumeric(dt.Rows[i]["Columns3"].ToString()))
                //    {
                //        dt3.Rows.Add(dt.Rows[i]);
                //    }
                //    else
                //    {
                //        dt4.Rows.Add(dt.Rows[i]);
                //    }

                //}


                //#endregion
            }
        }
        #endregion

      

       
    }
}
