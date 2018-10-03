using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using PivasBatchMX;
using PivasBatchCommon;
using PIVAsCommon.Helper;

namespace PivasBatch
{
    public partial class ViewInfo : UserControl
    {
        public ViewInfo()
        {
            InitializeComponent();
        }

        DB_Help DB = new DB_Help();
        SelectSql select = new SelectSql();
        UpdateSql update = new UpdateSql();
        int tf = 0;
        int IsSame = 0;
        Pivas_setBatch batch = new Pivas_setBatch();

        string SelectColor = "";
        DataTable dt = new DataTable();
        /// <summary>
        /// 加载瓶签数据和处方，药品信息
        /// </summary>
        /// <param name="Wardcode"></param>
        /// <param name="dd"></param>
        /// <param name="Tf"></param>
        /// <param name="IsSame"></param>
        public void ShowInfo(string Wardcode, string dd, int Tf, int IsSame,string SelectStr,int LongOrtemp,string getdrugtype)
        {
            try
            {
                this.tf = Tf;
                Dgv_Info.DataSource = null;
                this.IsSame = IsSame;

                //label1.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff");
                 ds = DB.GetPIVAsDB(select.IVRecord(pvb.ward, dd, Tf, SelectStr, Wardcode, IsSame, LongOrtemp,getdrugtype));


                //label2.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff");
                if (ds==null||ds.Tables.Count==0)
                {
                    this.Dgv_Info.DataSource = null;
                    ((UserControlBatch)(this.Parent.Parent.Parent)).med1.clear();
                    return;
                }
               
                Dgv_Info.DataSource = ds.Tables[0];
                dt = ds.Tables[1];

                if (Dgv_Info.Rows.Count > 0)
                {
                    string PetCode = Dgv_Info.Rows[0].Cells["病人编码"].Value.ToString();
                    string[] a = Dgv_Info.Rows[0].Cells["病区"].Value.ToString().Split('(');
                    string wName = a[0]+ "  " + Dgv_Info.Rows[0].Cells["床号"].Value.ToString().Replace("床", "") + "床" + "  " + Dgv_Info.Rows[0].Cells["姓名"].Value.ToString();
                    ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Visible = true;
                    ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Wardcode = a[1].Substring(0,a[1].Length-1);
                    ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Groupno = Dgv_Info.Rows[0].Cells["组号"].Value.ToString();
                    ((UserControlBatch)(this.Parent.Parent.Parent)).med1.AddMed(PetCode, tf, IsSame, "", "",Dgv_Info.Rows[0].Cells["批次规则"].Value.ToString());
                    ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Label_Info.Text = wName;
                    ((UserControlBatch)(this.Parent.Parent.Parent)).med1.rows = 0;
                    ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Label_BedNo.Text = Dgv_Info.Rows[0].Cells["床号"].Value.ToString();
                    Dgv_Info.Columns["批次"].Width = 50;
                    Dgv_Info.Columns["姓名"].Width = 80;
                    Dgv_Info.Columns["床号"].Width = 50;
                    Dgv_Info.Columns["主药"].Width = 200;
                    Dgv_Info.Columns["溶媒"].Width = 200;
                    Dgv_Info.Columns["瓶签号"].Width = 150;
                    Dgv_Info.Columns["用药时间"].Width = 200;
                    Dgv_Info.Columns["TeamNumber"].Visible = false;
                    Dgv_Info.Columns["IVStatus"].Visible = false;
                }
                else 
                {
                    this.Dgv_Info.DataSource = null;
                    ((UserControlBatch)(this.Parent.Parent.Parent)).med1.clear();
                }
                //DataSet ds = db.GetPIVAsDB(select.IVRecordSetUp(ECode));
                ColorS();
                //label3.Text = DateTime.Now.ToString();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        /// <summary>
        /// 颜色赋值
        /// </summary>
        private void ColorS()
        {
            string patcode = "";
           // int Count = 0;
            
            DataRow[] dr = dt.Select(" PatCode='" + patcode + "'");
            for (int i = 0; i < Dgv_Info.Rows.Count; i++)
            {
                if (Dgv_Info.Rows[i].Cells["病人编码"].Value.ToString().CompareTo(patcode) != 0 || i == 0)
                {
                    patcode = Dgv_Info.Rows[i].Cells["病人编码"].Value.ToString();
                    dr=dt.Select(" PatCode='"+patcode+"'");
                }
                if ((Dgv_Info.Rows[i].Cells["是否改动"].Value != null && Dgv_Info.Rows[i].Cells["是否改动"].Value.ToString().Trim().CompareTo("有改动") == 0) || (dr.Length>0))
                {
                    //MessageBox.Show("1");
                    if (pvb.ChangeColords.Tables[0].Rows[0]["ViewColor1"] != null && pvb.ChangeColords.Tables[0].Rows[0]["ViewColor1"].ToString().Trim().Length!=0)
                    {
                        //MessageBox.Show("2");
                        Dgv_Info.Rows[i].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(pvb.ChangeColords.Tables[0].Rows[0]["ViewColor1"].ToString());
                        Dgv_Info.Rows[i].HeaderCell.Style.BackColor = System.Drawing.ColorTranslator.FromHtml(pvb.ChangeColords.Tables[0].Rows[0]["ViewColor1"].ToString());
                    }
                    patcode = Dgv_Info.Rows[i].Cells["病人编码"].Value.ToString();
                    dr = dt.Select(" PatCode='" + patcode + "'");
                }
                else
                {
                   // MessageBox.Show("3");
                   if (pvb.ChangeColords.Tables[0].Rows[0]["ViewColor2"] != null && pvb.ChangeColords.Tables[0].Rows[0]["ViewColor2"].ToString() .Trim().Length!=0)
                    {
                        //MessageBox.Show("4");
                        Dgv_Info.Rows[i].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(pvb.ChangeColords.Tables[0].Rows[0]["ViewColor2"].ToString());
                        Dgv_Info.Rows[i].HeaderCell.Style.BackColor = System.Drawing.ColorTranslator.FromHtml(pvb.ChangeColords.Tables[0].Rows[0]["ViewColor2"].ToString());
                        //  Dgv_Info.DefaultCellStyle.SelectionBackColor = Color.Purple;
                    }
                }
                if (pvb.ChangeColords.Tables[0].Rows[0]["SelectionColor1"] != null && pvb.ChangeColords.Tables[0].Rows[0]["SelectionColor1"].ToString().Trim().Length != 0)
                {
                    //MessageBox.Show("5");
                    SelectColor = pvb.ChangeColords.Tables[0].Rows[0]["SelectionColor1"].ToString();
                    Dgv_Info.Rows[i].DefaultCellStyle.SelectionBackColor = System.Drawing.ColorTranslator.FromHtml(pvb.ChangeColords.Tables[0].Rows[0]["SelectionColor1"].ToString());
                    Dgv_Info.Rows[i].HeaderCell.Style.BackColor = System.Drawing.ColorTranslator.FromHtml(pvb.ChangeColords.Tables[0].Rows[0]["SelectionColor1"].ToString());

                }
            }
        }

        /// <summary>
        /// 双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_Info_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                pvb.operate = true;

                if (pvb.IvBatchSaved == 1 && pvb.ChangeSendBatch == "0")
                {
                    MessageBox.Show("<已发送> 状态瓶签不可修改批次，如需修改，请修改配置", "批次提示", MessageBoxButtons.OK);
                    return;
                }
                if (pvb.IvBatchSaved == 2 && pvb.ChangePrintBatch == "0")
                {
                    MessageBox.Show("<已打印> 状态瓶签不可修改批次，如需修改，请修改配置", "批次提示", MessageBoxButtons.OK);
                    return;
                }

                    batch.Close();
                    batch = new Pivas_setBatch();
                    string LabelNo = Dgv_Info.Rows[e.RowIndex].Cells["瓶签号"].Value.ToString();
                    string BatchRule = Dgv_Info.Rows[e.RowIndex].Cells["批次规则"].Value.ToString();
                    string Teamter = Dgv_Info.Rows[e.RowIndex].Cells["TeamNumber"].Value.ToString();
                    string PetCode = Dgv_Info.Rows[e.RowIndex].Cells["病人编码"].Value.ToString();
                    string Batch = Dgv_Info.Rows[e.RowIndex].Cells["批次"].Value.ToString();
                    batch.ShowSetBatch(true, LabelNo, Teamter, BatchRule, null, PetCode, Batch,true);
                    batch.ChangeTextVal += new DelegateChangeTextVal(ChangeBatch);
                    batch.Show(this);
            }
        }

        /// <summary>
        /// 修改批次
        /// </summary>
        /// <param name="Batch">批次 如：L-1-K</param>
        /// <param name="Team">teamnumber</param>
        /// <param name="batchrule">修改规则的描述</param>
        private void ChangeBatch(string Batch, string Team,string batchrule)
        {
            Dgv_Info.Rows[Dgv_Info.CurrentRow.Index].Cells["批次"].Value = Batch;
            Dgv_Info.Rows[Dgv_Info.CurrentRow.Index].Cells["批次规则"].Value = batchrule;
            if (Team.Length > 0)
            {
                Dgv_Info.Rows[Dgv_Info.CurrentRow.Index].Cells["TeamNumber"].Value = Team;
            }
            string PetCode = Dgv_Info.Rows[Dgv_Info.CurrentRow.Index].Cells["病人编码"].Value.ToString();
            string[] a = Dgv_Info.Rows[Dgv_Info.CurrentRow.Index].Cells["病区"].Value.ToString().Split('(');
            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Wardcode = a[1].Substring(0, a[1].Length - 1);
            string Naem = a[0] + "  " + Dgv_Info.Rows[Dgv_Info.CurrentRow.Index].Cells["床号"].Value.ToString().Trim().Replace("床", "") + "床" + "  " + Dgv_Info.Rows[Dgv_Info.CurrentRow.Index].Cells["姓名"].Value.ToString();
            string Groupno = Dgv_Info.Rows[Dgv_Info.CurrentRow.Index].Cells["组号"].Value.ToString();
            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Visible = true;
            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Label_Info.Text = Naem;
            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.rows = Dgv_Info.CurrentRow.Index;
            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Groupno = Groupno;
            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Label_BedNo.Text = Dgv_Info.Rows[Dgv_Info.CurrentRow.Index].Cells["床号"].Value.ToString().Trim();
            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.AddMed(PetCode, tf, IsSame, "", "",batchrule);

        }

        /// <summary>
        /// 单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_Info_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                                
                Application.DoEvents();
                pvb.operate = true;
                string PetCode = Dgv_Info.Rows[e.RowIndex].Cells["病人编码"].Value.ToString();


                if (e.ColumnIndex==3)
                {
                    batch.Dispose();
                    if (pvb.IvBatchSaved == 1 && pvb.ChangeSendBatch == "0")
                    {
                        MessageBox.Show("<已发送> 状态瓶签不可修改批次，如需修改，请修改配置", "批次提示", MessageBoxButtons.OK);
                        return;
                    }
                    if (pvb.IvBatchSaved == 2 && pvb.ChangePrintBatch == "0")
                    {
                        MessageBox.Show("<已打印> 状态瓶签不可修改批次，如需修改，请修改配置", "批次提示", MessageBoxButtons.OK);
                        return;
                    }
                        batch = new Pivas_setBatch();
                        string LabelNo = Dgv_Info.Rows[e.RowIndex].Cells["瓶签号"].Value.ToString();
                        string BatchRule = Dgv_Info.Rows[e.RowIndex].Cells["批次规则"].Value.ToString();
                        string Teamter = Dgv_Info.Rows[e.RowIndex].Cells["TeamNumber"].Value.ToString();
                        // string PetCode = Dgv_Info.Rows[e.RowIndex].Cells["病人编码"].Value.ToString();
                        string Batch = Dgv_Info.Rows[e.RowIndex].Cells["批次"].Value.ToString();
                        batch.ShowSetBatch(true, LabelNo, Teamter, BatchRule, null, PetCode, Batch,true);
                        batch.ChangeTextVal += new DelegateChangeTextVal(ChangeBatch);
                        batch.Show(this);
                    //}
                }

                string[] a = Dgv_Info.Rows[e.RowIndex].Cells["病区"].Value.ToString().Split('(');
                ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Wardcode = a[1].Substring(0, a[1].Length - 1);
                string Naem = a[0] + "  " + Dgv_Info.Rows[e.RowIndex].Cells["床号"].Value.ToString().Trim().Replace("床", "") + "床" + "  " + Dgv_Info.Rows[e.RowIndex].Cells["姓名"].Value.ToString();
                string Groupno = Dgv_Info.Rows[e.RowIndex].Cells["组号"].Value.ToString();
                ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Visible = true;

                ((UserControlBatch)(this.Parent.Parent.Parent)).med1.rows = e.RowIndex;
                ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Groupno = Groupno;


                ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Label_BedNo.Text = Dgv_Info.Rows[e.RowIndex].Cells["床号"].Value.ToString().Trim();
                ((UserControlBatch)(this.Parent.Parent.Parent)).med1.AddMed(PetCode, tf, IsSame, "", "", Dgv_Info.Rows[0].Cells["批次规则"].Value.ToString());
                ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Label_Info.Text = Naem;

                //Dgv_Info.Rows[Dgv_Info.CurrentRow.Index].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(SelectColor);
                ColorS();
            }
            else
            {
                lieMing = Dgv_Info.Columns[e.ColumnIndex].HeaderText;
            }
        }

        private void Dgv_Info_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
         
            pvb.operate = true;
            ColorS();
        }
        /// <summary>
        /// 选中(2)。上一床(-1)。下一床(1)
        /// </summary>
        /// <param name="Groupno"></param>
        /// <param name="BedNo"></param>
        /// <param name="TopBottom"></param>
        public void SelectRow(string Groupno, string BedNo, int TopBottom, int rows)
        {
            ColorS();
            DataGridViewRow dr = new DataGridViewRow();
            int Selectrow = 0;
            int count = 0;
            if (TopBottom == 2)
            {
                //选中
                for (int i = rows; i < Dgv_Info.Rows.Count; i++)
                {
                    //if (i < Dgv_Info.Rows.Count)
                    //{
                        dr = Dgv_Info.Rows[i];

                        if (dr.Cells["组号"].Value.ToString().Trim() == Groupno.Trim())
                        {
                            Selectrow = i;
                            Dgv_Info.Rows[Selectrow].Selected = true;
                            //this.Dgv_Info.FirstDisplayedScrollingRowIndex = Selectrow;
                            Dgv_Info.Rows[i].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(SelectColor);
                            break;
                        }
                    //}
                }
            }
            else
            {
                //上一床
                if (TopBottom == -1)
                {
                    for (int i = rows - 1; i >= 0; i--)
                    {
                        dr = Dgv_Info.Rows[i];
                        if (dr.Cells["床号"].Value.ToString().Trim().CompareTo(BedNo.Trim()) != 0 && dr.Cells["组号"].Value.ToString().Trim().CompareTo(Groupno.Trim()) != 0 &&
                            dr.Cells["床号"].Value.ToString().Trim().CompareTo(Dgv_Info.Rows[i + 1].Cells["床号"].Value.ToString().Trim()) != 0 &&
                             dr.Cells["组号"].Value.ToString().Trim().CompareTo(Dgv_Info.Rows[i + 1].Cells["组号"].Value.ToString().Trim()) != 0
                            && count > 0)
                        {
                            Selectrow = i;
                            break;
                        }
                        else
                        {
                            count++;
                        }
                    }
                }
                else
                {
                    //下一床
                    for (int i = rows; i < Dgv_Info.Rows.Count; i++)
                    {
                        if (i < Dgv_Info.Rows.Count)
                        {
                            dr = Dgv_Info.Rows[i];
                            if (dr.Cells["床号"].Value.ToString().Trim() != BedNo.Trim())
                            {
                                Selectrow = i;
                                break;
                            }
                        }
                        else
                        {

                        }
                    }
                }

                //①如果是上一床。并且传递到Med控件里的行数大于要选中的行数的话
                //②如果是下一床。并且传递到Med控件里的行数小于于要选中的行数的话

                //如果没有上一床 或者是没有下一床了。
                if ((TopBottom == 1 && rows < Selectrow) || (TopBottom == -1 && rows > Selectrow))
                {
                    if (TopBottom == -1 && rows > Selectrow && count > 0 && Selectrow != 0)
                    {
                        Selectrow = Selectrow + 1;
                    }
                }
                else if (Selectrow >= 0)
                {
                    Selectrow = 0;

                }
                UpdateSql update = new UpdateSql();

                if (BedNo == Dgv_Info.Rows[Dgv_Info.Rows.Count - 1].Cells["床号"].Value.ToString() && TopBottom==1)
                {
                    DialogResult result = MessageBox.Show("所选病区批次均已排完，是否发送？", "确定发送当前列表的所有瓶签吗？ ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        int B = DB.SetPIVAsDB(update.IVRecordBatchSaved(pvb.datetime.ToString("yyyyMMdd"), "", pvb.ward, pvb.Choose));
                        if (B > 0)
                        {
                            ((UserControlBatch)(this.Parent.Parent.Parent)).ShowWard(0, false);
                            //((Pivasbatch)(this.Parent.Parent.Parent)).ClickWard(1, "");
                            ((UserControlBatch)(this.Parent.Parent.Parent)).ShowSelectWard();
                            
                        }
                    }
                }
                else
                {
                    Dgv_Info.Rows[Selectrow].Cells[0].Selected = true;
                    string[] a = Dgv_Info.Rows[Selectrow].Cells["病区"].Value.ToString().Split('(');
                    ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Wardcode = a[1].Substring(0, a[1].Length - 1);
                    //this.Dgv_Info.FirstDisplayedScrollingRowIndex = Selectrow;
                    Dgv_Info.Rows[Selectrow].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(SelectColor);
                    string PetCode = Dgv_Info.Rows[Selectrow].Cells["病人编码"].Value.ToString();
                    string Naem = a[0] + " "
                        + Dgv_Info.Rows[Selectrow].Cells["床号"].Value.ToString().Replace("床", "") + "床" + " "
                        + Dgv_Info.Rows[Selectrow].Cells["姓名"].Value.ToString();
                    ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Visible = true;
                    ((UserControlBatch)(this.Parent.Parent.Parent)).med1.AddMed(PetCode, tf, IsSame, "", "",Dgv_Info.Rows[0].Cells["批次规则"].Value.ToString());
                    ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Label_Info.Text = Naem;
                    ((UserControlBatch)(this.Parent.Parent.Parent)).med1.rows = Selectrow;
                    ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Label_BedNo.Text = Dgv_Info.Rows[Selectrow].Cells["床号"].Value.ToString();
                }
            }
        }

        private void Dgv_Info_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts = DataGridViewPaintParts.All ^ DataGridViewPaintParts.Focus;
        }

        private void Dgv_Info_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                pvb.operate = true;
                //MessageBox.Show(e.KeyValue.ToString());
                if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                {
                    ColorS();
                    Application.DoEvents();
                    
                    string PetCode = Dgv_Info.CurrentRow.Cells["病人编码"].Value.ToString();

                    string[] a = Dgv_Info.CurrentRow.Cells["病区"].Value.ToString().Split('(');
                    ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Wardcode = a[1].Substring(0, a[1].Length - 1);
                    string Naem = a[0] + "  " + Dgv_Info.CurrentRow.Cells["床号"].Value.ToString().Trim().Replace("床", "") + "床" + "  " + Dgv_Info.CurrentRow.Cells["姓名"].Value.ToString();
                    string Groupno = Dgv_Info.CurrentRow.Cells["组号"].Value.ToString();
                    ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Visible = true;

                    ((UserControlBatch)(this.Parent.Parent.Parent)).med1.rows = Dgv_Info.CurrentRow.Index;
                    ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Groupno = Groupno;


                    ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Label_BedNo.Text = Dgv_Info.CurrentRow.Cells["床号"].Value.ToString().Trim();
                    ((UserControlBatch)(this.Parent.Parent.Parent)).med1.AddMed(PetCode, tf, IsSame, "", "", Dgv_Info.Rows[0].Cells["批次规则"].Value.ToString());
                    ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Label_Info.Text = Naem;
                }
                if (pvb.fastChange)
                {
                    
                    if (e.KeyCode == Keys.K)
                    {
                        if (pvb.IvBatchSaved == 1 && pvb.ChangeSendBatch == "0")
                        {
                            MessageBox.Show("<已发送> 状态瓶签不可修改批次，如需修改，请修改配置", "批次提示", MessageBoxButtons.OK);
                            return;
                        }
                        if (pvb.IvBatchSaved == 2 && pvb.ChangePrintBatch == "0")
                        {
                            MessageBox.Show("<已打印> 状态瓶签不可修改批次，如需修改，请修改配置", "批次提示", MessageBoxButtons.OK);
                            return;
                        }


                        string oldbatch = "";
                        string newbatch = "";
                        string LabelNo = "";
                        string IVStatus = "";


                        IVStatus = Dgv_Info.CurrentRow.Cells["IVStatus"].Value.ToString();
                        LabelNo = Dgv_Info.CurrentRow.Cells["瓶签号"].Value.ToString();
                        oldbatch = Dgv_Info.CurrentRow.Cells["批次"].Value.ToString();
                        string batchrule = "";

                        if (oldbatch.IndexOf("K") > 0)
                        {
                            newbatch = oldbatch.Replace("-K", "");
                            newbatch += "#";
                        }
                        else
                        {
                            newbatch = oldbatch.Replace("#", "");
                            newbatch += "-K";
                        }

                        int i = DB.SetPIVAsDB(update.IVRecordBatch(LabelNo, oldbatch, newbatch, int.Parse(Dgv_Info.CurrentRow.Cells["TeamNumber"].Value.ToString()), pvb.DEmployeeName, ref batchrule));
                        if (i > 0)
                        {
                            Dgv_Info.CurrentRow.Cells["批次"].Value = newbatch;
                            Dgv_Info.CurrentRow.Cells["批次规则"].Value = batchrule;

                            string PetCode = Dgv_Info.CurrentRow.Cells["病人编码"].Value.ToString();

                            string[] a = Dgv_Info.CurrentRow.Cells["病区"].Value.ToString().Split('(');
                            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Wardcode = a[1].Substring(0, a[1].Length - 1);
                            string Naem = a[0] + "  " + Dgv_Info.CurrentRow.Cells["床号"].Value.ToString().Trim().Replace("床", "") + "床" + "  " + Dgv_Info.CurrentRow.Cells["姓名"].Value.ToString();
                            string Groupno = Dgv_Info.CurrentRow.Cells["组号"].Value.ToString();
                            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Visible = true;

                            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.rows = Dgv_Info.CurrentRow.Index;
                            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Groupno = Groupno;


                            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Label_BedNo.Text = Dgv_Info.CurrentRow.Cells["床号"].Value.ToString().Trim();
                            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.AddMed(PetCode, tf, IsSame, "", "", Dgv_Info.Rows[0].Cells["批次规则"].Value.ToString());
                            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Label_Info.Text = Naem;

                            DB.SetPIVAsDB(update.OrderChangelog(LabelNo, pvb.DEmployeeID, pvb.DEmployeeName, oldbatch, newbatch, IVStatus, "快速修改",""));
                            //NewMethod(batchtr, Team);
                        }
                    }
                    else if (e.KeyValue > 48 && e.KeyValue <= 57)
                    {
                        if (pvb.IvBatchSaved == 1 && pvb.ChangeSendBatch == "0")
                        {
                            MessageBox.Show("<已发送> 状态瓶签不可修改批次，如需修改，请修改配置", "批次提示", MessageBoxButtons.OK);
                            return;
                        }
                        if (pvb.IvBatchSaved == 2 && pvb.ChangePrintBatch == "0")
                        {
                            MessageBox.Show("<已打印> 状态瓶签不可修改批次，如需修改，请修改配置", "批次提示", MessageBoxButtons.OK);
                            return;
                        }


                        if (!OrderIsValid(e.KeyValue-48))
                        {
                            return;
                        }
                        string oldbatch = "";
                        string newbatch = "";
                        string LabelNo = "";
                        string IVStatus = "";

                        IVStatus = Dgv_Info.CurrentRow.Cells["IVStatus"].Value.ToString();
                        LabelNo = Dgv_Info.CurrentRow.Cells["瓶签号"].Value.ToString();
                        oldbatch = Dgv_Info.CurrentRow.Cells["批次"].Value.ToString();
                        string batchrule = "";

                        if (oldbatch.IndexOf("L") >= 0)
                        {
                            newbatch = "L-" + (e.KeyValue - 48).ToString();
                        }
                        else
                        {
                            newbatch = (e.KeyValue - 48).ToString();
                        }
                        if (oldbatch.IndexOf("K") > 0)
                        {
                            newbatch += "-K";
                        }
                        else
                        {
                            newbatch += "#";
                        }

                        int i = DB.SetPIVAsDB(update.IVRecordBatch(LabelNo, oldbatch, newbatch, e.KeyValue - 48, pvb.DEmployeeName, ref batchrule));
                        if (i > 0)
                        {
                            Dgv_Info.CurrentRow.Cells["批次"].Value = newbatch;
                            Dgv_Info.CurrentRow.Cells["批次规则"].Value = batchrule;
                            Dgv_Info.CurrentRow.Cells["TeamNumber"].Value = (e.KeyValue - 48).ToString();

                            string PetCode = Dgv_Info.CurrentRow.Cells["病人编码"].Value.ToString();

                            string[] a = Dgv_Info.CurrentRow.Cells["病区"].Value.ToString().Split('(');
                            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Wardcode = a[1].Substring(0, a[1].Length - 1);
                            string Naem = a[0] + "  " + Dgv_Info.CurrentRow.Cells["床号"].Value.ToString().Trim().Replace("床", "") + "床" + "  " + Dgv_Info.CurrentRow.Cells["姓名"].Value.ToString();
                            string Groupno = Dgv_Info.CurrentRow.Cells["组号"].Value.ToString();
                            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Visible = true;

                            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.rows = Dgv_Info.CurrentRow.Index;
                            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Groupno = Groupno;


                            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Label_BedNo.Text = Dgv_Info.CurrentRow.Cells["床号"].Value.ToString().Trim();
                            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.AddMed(PetCode, tf, IsSame, "", "", Dgv_Info.Rows[0].Cells["批次规则"].Value.ToString());
                            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Label_Info.Text = Naem;

                            DB.SetPIVAsDB(update.OrderChangelog(LabelNo, pvb.DEmployeeID, pvb.DEmployeeName, oldbatch, newbatch, IVStatus, "快速修改",""));
                            //NewMethod(batchtr, Team);
                        }
                    }
                    else if ((e.KeyValue > 96 && e.KeyValue <= 105))
                    {
                        if (pvb.IvBatchSaved == 1 && pvb.ChangeSendBatch == "0")
                        {
                            MessageBox.Show("<已发送> 状态瓶签不可修改批次，如需修改，请修改配置", "批次提示", MessageBoxButtons.OK);
                            return;
                        }
                        if (pvb.IvBatchSaved == 2 && pvb.ChangePrintBatch == "0")
                        {
                            MessageBox.Show("<已打印> 状态瓶签不可修改批次，如需修改，请修改配置", "批次提示", MessageBoxButtons.OK);
                            return;
                        }

                        if (!OrderIsValid(e.KeyValue-96))
                        {
                            return;
                        }
                        string oldbatch = "";
                        string newbatch = "";
                        string LabelNo = "";
                        string IVStatus = "";

                        IVStatus = Dgv_Info.CurrentRow.Cells["IVStatus"].Value.ToString();
                        LabelNo = Dgv_Info.CurrentRow.Cells["瓶签号"].Value.ToString();
                        oldbatch = Dgv_Info.CurrentRow.Cells["批次"].Value.ToString();
                        string batchrule = "";

                        if (oldbatch.IndexOf("L") >= 0)
                        {
                            newbatch = "L-" + (e.KeyValue - 96).ToString();
                        }
                        else
                        {
                            newbatch = (e.KeyValue - 96).ToString();
                        }
                        if (oldbatch.IndexOf("K") > 0)
                        {
                            newbatch += "-K";
                        }
                        else
                        {
                            newbatch += "#";
                        }

                        int i = DB.SetPIVAsDB(update.IVRecordBatch(LabelNo, oldbatch, newbatch, e.KeyValue - 96, pvb.DEmployeeName, ref batchrule));
                        if (i > 0)
                        {
                            Dgv_Info.CurrentRow.Cells["批次"].Value = newbatch;
                            Dgv_Info.CurrentRow.Cells["批次规则"].Value = batchrule;
                            Dgv_Info.CurrentRow.Cells["TeamNumber"].Value = (e.KeyValue - 96).ToString();

                            string PetCode = Dgv_Info.CurrentRow.Cells["病人编码"].Value.ToString();

                            string[] a = Dgv_Info.CurrentRow.Cells["病区"].Value.ToString().Split('(');
                            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Wardcode = a[1].Substring(0, a[1].Length - 1);
                            string Naem = a[0] + "  " + Dgv_Info.CurrentRow.Cells["床号"].Value.ToString().Trim().Replace("床", "") + "床" + "  " + Dgv_Info.CurrentRow.Cells["姓名"].Value.ToString();
                            string Groupno = Dgv_Info.CurrentRow.Cells["组号"].Value.ToString();
                            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Visible = true;

                            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.rows = Dgv_Info.CurrentRow.Index;
                            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Groupno = Groupno;


                            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Label_BedNo.Text = Dgv_Info.CurrentRow.Cells["床号"].Value.ToString().Trim();
                            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.AddMed(PetCode, tf, IsSame, "", "", Dgv_Info.Rows[0].Cells["批次规则"].Value.ToString());
                            ((UserControlBatch)(this.Parent.Parent.Parent)).med1.Label_Info.Text = Naem;

                            DB.SetPIVAsDB(update.OrderChangelog(LabelNo, pvb.DEmployeeID, pvb.DEmployeeName, oldbatch, newbatch, IVStatus, "快速修改",""));
                            //NewMethod(batchtr, Team);
                        }
                    }
                }
                    pvb.operate = true;
           
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool OrderIsValid(int order)
        {
            try
            {
                for (int i = 0; i < pvb.OrderID.Length;i++ )
                {
                    if (pvb.OrderID[i]==order)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 返回列表中患者的瓶签号
        /// </summary>
        /// <param name="PacCode">患者code</param>
        /// <returns></returns>
        public string patLabelNos(string PacCode)
        {
            try 
            {
                string LabelNos = "";
                for (int i = 0; i < Dgv_Info.RowCount; i++)
                {
                    if (Dgv_Info.Rows[i].Cells["病人编码"].Value.ToString()==PacCode)
                    {
                        LabelNos += Dgv_Info.Rows[i].Cells["瓶签号"].Value.ToString() + ",";
                    }
                }

                if (LabelNos.Length>0)
                {
                    LabelNos = LabelNos.Remove(LabelNos.Length - 1, 1);
                }

                return LabelNos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        #region 排序 --张衡2014-09-28新增床位号排序

        public static bool IsNumeric(string str)
        {

            if (str.Length > 10)
                return false;
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            byte[] bytestr = ascii.GetBytes(str);
            //if (bytestr[0] == 43)
            //    return true;
            foreach (byte c in bytestr)
            {
                if ((c < 48 || c > 57))
                {
                    return false;
                }
            }
            return true;
        }
        public class RowComparer : IComparer<DataRow>
        {
            public Dictionary<int, SortOrder> SortColumns { get; set; }

            private static int sortOrderModifier = 1;

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
                        value1 = x.ItemArray[key].ToString().TrimEnd('+');
                        if (value1.Contains('.'))
                        {
                            value1 = value1.TrimStart('.');
                        }
                        if (value1.Contains('床'))
                        {
                            value1.Replace("床", "");
                        }
                        value1 = value1.TrimStart('+');
                    }
                    if (y.ItemArray[key] == DBNull.Value)
                        value2 = "0";
                    else
                    {
                        value2 = y.ItemArray[key].ToString().TrimEnd('+');
                        if (value2.Contains('.'))
                        {
                            value2 = value2.TrimStart('.');
                        }
                        if (value2.Contains('床'))
                        {
                            value2.Replace("床", "");
                        }
                        value2 = value2.TrimStart('+');
                    }

                    if (IsNumeric(value1) && IsNumeric(value2))
                    {
                        if (int.Parse(value1) > int.Parse(value2))
                        {
                            compareResult = 1;
                        }
                        else if (int.Parse(value1) < int.Parse(value2))
                        {
                            compareResult = -1;
                        }
                        else
                        {
                            compareResult = 0;
                        }
                    }
                    else if (IsNumeric(value1))
                    {
                        compareResult = -1;

                    }
                    else if (IsNumeric(value2))
                    {
                        compareResult = 1;
                    }
                    else
                    {

                        compareResult = System.String.Compare(value1, value2);
                    }

                }
                return compareResult * sortOrderModifier;
            }
        }
        int m = 0;
        string lieMing;
        DataSet ds = new DataSet(); //存放病人信息
        private void Dgv_Info_Sorted(object sender, EventArgs e)
        {

            if (Dgv_Info != null && lieMing == "床号")
            {
                DataTable dt = ds.Tables[0];
                ListSortDirection direction = new ListSortDirection();
                Dictionary<int, SortOrder> sortColumns =
                   new Dictionary<int, SortOrder>();
                if (m == 0)
                {
                    RowComparer comp = new RowComparer(SortOrder.Ascending);
                    sortColumns.Add(0, SortOrder.Ascending);
                    direction = ListSortDirection.Ascending;
                    m = 1;

                    comp.SortColumns = sortColumns;
                    var query3 = dt.AsEnumerable().OrderBy(q => q, comp);
                    DataView dv3 = query3.AsDataView();
                    Dgv_Info.DataSource = dv3;

                }
                else
                {
                    RowComparer comp = new RowComparer(SortOrder.Descending);
                    sortColumns.Add(0, SortOrder.Descending);
                    direction = ListSortDirection.Descending;
                    m = 0;

                    comp.SortColumns = sortColumns;
                    var query3 = dt.AsEnumerable().OrderBy(q => q, comp);
                    DataView dv3 = query3.AsDataView();
                    Dgv_Info.DataSource = dv3;

                }
                Dgv_Info.Columns["床号"].HeaderCell.SortGlyphDirection = direction == ListSortDirection.Ascending ? SortOrder.Ascending : SortOrder.Descending;
            }

        }
        #endregion
    }
}
