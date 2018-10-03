using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using PIVAsCommon.Helper;
using PivasBatchCommon;

namespace PivasBatch
{
    public delegate void DelegateChangeTextS(string TextVal, int tags, int issame, string Labelno, string Batch,string batchrule);
    public partial class Med : UserControl
    {
        public Med()
        {
            InitializeComponent();
        }

        UpdateBatch ba;
        DB_Help DB = new DB_Help();
        SelectSql select = new SelectSql();
        //中间瓶签的行数据  选中的是哪张瓶签，上一床，下一床 有用到
        public int rows = 0;

        //选中瓶签的批次
        string Batch = string.Empty;
        //选中瓶签的组号
        public string Groupno = string.Empty;
        //没用
        int tags = 0;
        //存储病人Code
         string Patcode = string.Empty;
        //存储病区Code
         public string Wardcode = string.Empty;
        UpdateSql update = new UpdateSql();
        //public bool ShowF = false;
        //选中
        bool isSelect = false;
        //bool CheckedRows = false;
        //public event DelegateChangeTextS ChangeTextVal;

        string batchrule = "";

        /// <summary>
        /// 显示药品
        /// </summary>
        /// <param name="Patcode">病人ID</param>
        /// <param name="tags"></param>
        /// <param name="IsSame"></param>
        /// <param name="Labelno">瓶签号</param>
        /// <param name="Batch"></param>
        public void AddMed(string Patcode, int tags, int IsSame, string Labelno, string Batch, string batchrule)
        {
            if(Labelno.Trim().Length!=0)
            {
                this.batchrule = batchrule;
                this.Batch = Batch;
                //选中瓶签的瓶签号
                Label_Change.Text=Labelno;                
            }
            this.tags = tags;
            //病人ID
            this.Patcode = Patcode;
            this.Wardcode = pvb.ward;
            try
            {
                //显示所有瓶签的药品(右下角药品)
                ShowDD(tags, Patcode);

            }
            catch (System.Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10011:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }

        /// <summary>
        ///  去掉多余的空格。 剂量和单位合并
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="Patcode">病人ID</param>
        public void ShowDD(int tags, string Patcode)
        {
            //查询瓶签明细中的信息
            DataSet ds = DB.GetPIVAsDB(select.IVRecordDetail(Wardcode, pvb.datetime.ToString("yyyyMMdd"), tags, Patcode));
            //右下角规则：先根据病人取出患者的药品信息，医嘱，瓶签信息组成第一张表，取出组号，批次和批次号组成第二张表。
            //然后，按表一循环取出医嘱号，根据医嘱号取出表2的批次号，将所有批次号合并（批次合并规则：如果该医嘱生成了多了批次的瓶签，则将多个批次合并，放入表一的Batch字段，并取出当前瓶签批次的#，K，L，放入表一的Batchs字段。）
            //如果医嘱号与上一行相同，则直接将表一该行的Batch，Batchs设为与上一行相同，unit设为空。
            //最后，如果表一的该药品是溶媒则显示剂量。
            //循环完成之后，将表一的BatchS字段里的含有#，K，L的行分别取出放入三张表里按照医嘱号，批次号进行排序，然后将三张表拼接。
            string Group = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //剂量单位
                string D = ds.Tables[0].Rows[i]["Unit"].ToString().Trim();
                //批次
                string batch = "";

                //如果本行的组号跟Group变量中的组号不一致或者是第一次进入。则合并批次，剂量和单位
                if (ds.Tables[0].Rows[i]["GroupNo"].ToString().Trim().CompareTo(Group) != 0 || i == 0)
                {
                    //组号
                    Group = ds.Tables[0].Rows[i]["GroupNo"].ToString().Trim();

                    //根据组号查询出批次信息
                    DataRow[] dr = ds.Tables[1].Select(" GroupNo='" + Group + "'");

                    //把组号查出的数据的批次合并（如1#,2#）
                    for (int j = 0; j < dr.Length; j++)
                    {
                        if (batch == "")
                        {
                            batch = dr[j][1].ToString();
                        }
                        else
                        {
                            batch = batch + "," + dr[j][1].ToString();
                        }

                    }
                    //把本药品的批次放在Batch中
                    ds.Tables[0].Rows[i]["Batch"] = dr[0][2].ToString();
                    //把本药品的用量和单位合并成剂量

                    ds.Tables[0].Rows[i]["Unit"] = ds.Tables[0].Rows[i]["Dosage"].ToString().Trim() + D;
                    

                    //把合并好的批次放在批次中。（页面显示）
                    ds.Tables[0].Rows[i]["Batch1"] = batch;
                    //如果batch中的批次有多个的情况下
                    if (batch.IndexOf(",") >= 0)
                    {
                        //删除掉多余的批次，只留当前药品的批次
                        string BatchS = batch.Remove(batch.IndexOf(","));
                        //删除BatchS中的批次，只留（#，K，L）
                        if (batch.IndexOf("L") >= 0)
                        {

                            ds.Tables[0].Rows[i]["BatchS"] = "L";
                            //ds.Tables[0].Rows[i]["BatchS"] = batch.Replace(ds.Tables[0].Rows[i]["Batch"].ToString(), "").Replace("-", "");
                        }
                        else
                        {
                            ds.Tables[0].Rows[i]["BatchS"] = BatchS.Replace(ds.Tables[0].Rows[i]["Batch"].ToString(), "").Replace("-", "");
                        }
                       
                    }
                    else
                    {
                        //删除Batch中的批次，只留（#，K，L）
                        if (batch.IndexOf("L") >= 0)
                        {
                            ds.Tables[0].Rows[i]["BatchS"] = "L";
                            //ds.Tables[0].Rows[i]["BatchS"] = batch.Replace(ds.Tables[0].Rows[i]["Batch"].ToString(), "").Replace("-", "");
                        }
                        else
                        {
                            ds.Tables[0].Rows[i]["BatchS"] = batch.Replace(ds.Tables[0].Rows[i]["Batch"].ToString(), "").Replace("-", "");
                        }
                    }
                }
                else //否则
                {
                    ds.Tables[0].Rows[i]["Unit"] = "";
                    ds.Tables[0].Rows[i]["Batch1"] = "";
                    //获取上个药品的批次和批次单位
                    ds.Tables[0].Rows[i]["Batch"] = ds.Tables[0].Rows[i - 1]["Batch"].ToString();
                    ds.Tables[0].Rows[i]["BatchS"] = ds.Tables[0].Rows[i - 1]["BatchS"].ToString();
                }
                //string strImagePath = Application.StartupPath+"\\";
                //如果这个药品是溶媒的话。就显示剂量
                string dossage=DB.IniReadValuePivas("PivasBatch","AllDrugDossage");
                if (string.IsNullOrEmpty(dossage) || dossage == "0")
                {
                    if (ds.Tables[0].Rows[i]["IsMenstruum"].ToString() == "True")
                    {
                        ds.Tables[0].Rows[i]["Unit"] = ds.Tables[0].Rows[i]["Dosage"].ToString().Trim() + D;
                    }
                    else
                    {
                        ds.Tables[0].Rows[i]["Unit"] = "";
                    }
                }
                else
                {
                    ds.Tables[0].Rows[i]["Unit"] = ds.Tables[0].Rows[i]["Dosage"].ToString().Trim() + D;
                }
                ds.Tables[0].Rows[i]["DrugName1"] = ds.Tables[0].Rows[i]["DrugName1"].ToString().Trim();
            }





            df.ClearSelection();

            if (ds.Tables[0].Rows.Count<=0)
            {
                MessageBox.Show("瓶签明细查不到相关数据");
                return;
            }

            //#批次的数据
            DataTable dd = ds.Tables[0];
            //dd.Clear();
            //K批次的数据
            DataTable ddd = ds.Tables[0];
            //ddd.Clear();
            //L批次的数据
            DataTable dddd = ds.Tables[0];
            //dddd.Clear();
            dd=DataRowToDatatable(dd.Select("BatchS='#'"), dd);
            DataView dw = dd.DefaultView;
            dw.Sort = "batch ASC,GroupNo ASC";
            dd = dw.Table;
            ddd=DataRowToDatatable(ddd.Select("BatchS='K'"), ddd);
            dw = ddd.DefaultView;
            dw.Sort = "batch ASC,GroupNo ASC";
            ddd = dw.Table;

            dddd=DataRowToDatatable(dddd.Select("BatchS='L'"), dddd);
            dw = dddd.DefaultView;
            dw.Sort = "batch ASC,GroupNo ASC";
            dddd = dw.Table;

            //把三个Table合成一个Table
            DataTable All = new DataTable();
            All = dd;
            if (ddd.Rows.Count > 0)
            {
                All.Merge(ddd);
            }
            if (dddd.Rows.Count > 0)
            {
                All.Merge(dddd);
            }

            Dgv_Info.DataSource = All;


            //Groupno = Dgv_Info.Rows[0].Cells["GroupNo"].Value.ToString().Trim();
            //Dgv_Info.Columns["状态"].Width = 60;
            //Dgv_Info.Columns["药品"].Width = 210;
            //Dgv_Info.Columns["频次"].Width = 80;
            ////Dgv_Info.Columns[3].Visible = false;
            ////Dgv_Info.Columns["剂量"].Width = 60;
            //Dgv_Info.Columns["剂量"].Visible = false;
            //Dgv_Info.Columns["批次"].Width = 100;
            //Dgv_Info.Columns[7].Visible = false;    
            //Dgv_Info.Columns[8].Visible = false;
            //Dgv_Info.Columns[9].Visible = false;
            //Dgv_Info.Columns[10].Visible = false;
            //Dgv_Info.Columns[11].Visible = false;
            //Dgv_Info.Columns["BatchS"].Visible = false;
            //Dgv_Info.Columns[12].Width = 60;
            //Dgv_Info.Columns[13].Visible = false;
            //Dgv_Info.Columns[14].Visible = false;
            //Dgv_Info.Columns[15].Visible = false;
            //赋颜色
            ColorS();

            //如果点击进来有组号的话，根据组号显示信息，
            //否则根据查询出的数据的第一条的组号显示信息
            if (Groupno.Trim().Length > 0)
            {
                if (ds.Tables[0].Select(" GroupNo='" + Groupno + "'").Length > 0)
                {
                    SetInformation(Groupno);
                }
                else
                {
                    SetInformation(ds.Tables[0].Rows[0]["GroupNo"].ToString());
                }
            }
            else
            {
                SetInformation(ds.Tables[0].Rows[0]["GroupNo"].ToString());
            }
        
        }

        //把DataRow转换成Table
        public DataTable DataRowToDatatable(DataRow[] dr, DataTable DTable)
        {
            DataTable dd = DTable;
            dd = DTable.Copy();
            dd.Rows.Clear();
            foreach (DataRow row in dr)
            {
                dd.ImportRow(row);
            }
            return dd;
        }

        /// <summary>
        /// 选择一行找到这行选择详细信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_Info_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //时间重新计算
                pvb.operate = true;

                Groupno = Dgv_Info.Rows[e.RowIndex].Cells["colGroupNo"].Value.ToString().Trim();
                //重新着色
                ColorS();
                //重新加载处方信息
                SetInformation(Groupno);
                //找到瓶签datagridview中的那一行并选中
                ((ViewInfo)((SplitContainer)(this.Parent.Parent)).Panel1.Controls[0]).SelectRow(Groupno, Label_BedNo.Text.ToString(), 2, rows);
                try
                {
                    //关闭修改批次小界面
                    ba.Close();
                }
                catch 
                {}
                //加载批次修改界面

                if (Dgv_Info.Rows[e.RowIndex].Cells["colStatus"].Value.ToString().Trim() == "已发送" && pvb.ChangeSendBatch == "0")
                {
                    MessageBox.Show("<已发送> 状态瓶签不可修改批次，如需修改，请修改配置", "批次提示", MessageBoxButtons.OK);                                          
                        return;
                    
                }
                if (Dgv_Info.Rows[e.RowIndex].Cells["colStatus"].Value.ToString().Trim() == "已打印" && pvb.ChangePrintBatch == "0")
                {
                    MessageBox.Show("<已打印> 状态瓶签不可修改批次，如需修改，请修改配置", "批次提示", MessageBoxButtons.OK);
                    return;
                }

                ba = new UpdateBatch(Dgv_Info.Rows[e.RowIndex].Cells["colStatus"].Value.ToString().Trim());
                //ba = new UpdateBatch();
                ba.sert(Groupno);
                ba.patcode = Patcode;
                ba.tags = tags;
                ba.IsSame = 0;
                ba.ChangeTextVal += new DelegateChangeTextValS(AddMed);
                ba.Show(this);
            }
        }

        /// <summary>
        /// 上一床，下一床
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LLabel_Bottom_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dgv_Info != null && Dgv_Info.Rows.Count>0)
                {
                    //刷新界面停留时间
                    pvb.operate = true;
                    string Groupno = Dgv_Info.Rows[0].Cells["colGroupNo"].Value.ToString().Trim();
                    //用来判断的值（上一床，下一床，选中）
                    int TopBottom = int.Parse(((LinkLabel)sender).Tag.ToString());
                    //调用SelectRow
                    ((ViewInfo)((SplitContainer)(this.Parent.Parent)).Panel1.Controls[0]).SelectRow(Groupno, Label_BedNo.Text.ToString(), TopBottom, rows);
                }
            }
            catch (Exception ee)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10006:" + ee.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }

        /// <summary>
        /// 颜色
        /// </summary>
        private void ColorS()
        {
            //去掉默认选中
            //if (Dgv_Info.Rows.Count>0)
            if (Dgv_Info.SelectedRows.Count > 0)
            {
                Dgv_Info.Rows[0].Selected = false;
            }
            string groupno = "";
            //用来隔开每个处方。每组处方加1，奇数组处方是一个颜色，偶数组处方是另一个颜色
            int Count = 0;
            //取颜色
            DataSet ChangeColords = DB.GetPIVAsDB(select.IVRecordSetUp(pvb.DEmployeeID));
            for (int i = 0; i < Dgv_Info.Rows.Count; i++)
            {

                if (ChangeColords.Tables[0].Rows[0]["DrugColor1"] != null && ChangeColords.Tables[0].Rows[0]["DrugColor1"].ToString().Trim().Length != 0)
                {
                    Dgv_Info.Rows[i].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(ChangeColords.Tables[0].Rows[0]["DrugColor1"].ToString());
                    Dgv_Info.Rows[i].HeaderCell.Style.BackColor = System.Drawing.ColorTranslator.FromHtml(ChangeColords.Tables[0].Rows[0]["DrugColor1"].ToString());
                    //Dgv_Info.DefaultCellStyle.SelectionBackColor = Color.Purple;
                }
                if (Dgv_Info.Rows[i].Cells["colGroupNo"].Value.ToString().CompareTo(groupno) != 0 || i == 0)
                {
                    groupno = Dgv_Info.Rows[i].Cells["colGroupNo"].Value.ToString();
                    Count++;
                }
                
                
                if (Count % 2 != 0)
                {
                    if (ChangeColords.Tables[0].Rows[0]["DrugColor2"] != null && ChangeColords.Tables[0].Rows[0]["DrugColor2"].ToString().Trim().Length !=0)
                    {
                        Dgv_Info.Rows[i].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(ChangeColords.Tables[0].Rows[0]["DrugColor2"].ToString());
                        Dgv_Info.Rows[i].HeaderCell.Style.BackColor = System.Drawing.ColorTranslator.FromHtml(ChangeColords.Tables[0].Rows[0]["DrugColor2"].ToString());
                       // Dgv_Info.DefaultCellStyle.SelectionBackColor = Color.Red;                        
                    }
                    groupno = Dgv_Info.Rows[i].Cells["colGroupNo"].Value.ToString();
                }
                if (Dgv_Info.Rows[i].Cells["colGroupNo"].Value.ToString().CompareTo(Groupno) == 0)
                {
                    if (!isSelect)
                    {
                        isSelect = true;

                    }//==Select
                    if (ChangeColords.Tables[0].Rows[0]["SelectionColor2"] != null && ChangeColords.Tables[0].Rows[0]["SelectionColor2"].ToString().Trim().Length !=0)
                    {
                        Dgv_Info.Rows[i].DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(ChangeColords.Tables[0].Rows[0]["SelectionColor2"].ToString());
                        Dgv_Info.Rows[i].DefaultCellStyle.SelectionBackColor = System.Drawing.ColorTranslator.FromHtml(ChangeColords.Tables[0].Rows[0]["SelectionColor2"].ToString());
                        Dgv_Info.Rows[i].HeaderCell.Style.BackColor = System.Drawing.ColorTranslator.FromHtml(ChangeColords.Tables[0].Rows[0]["SelectionColor2"].ToString());

                    }
                    Dgv_Info.Rows[i].Selected = isSelect;

                    if ( isSelect==true)
                    {
                        this.Dgv_Info.FirstDisplayedScrollingRowIndex = i;
                    }
                }
            }
        }


        /// <summary>
        /// 清空信息
        /// </summary>
        public void clear()
        {
            lblBedNo.Text = "";
            lblCaseID.Text = "";
            lblDoctor.Text = "";
            lblEndDT.Text = "";
            lblSex.Text = "";
            lblStartDT.Text = "";
            lblWard.Text = "";
            lblPatient.Text = "";
            lblWeight.Text = "";
            lblAge.Text = "";
            lblBatch.Text = "";
            laUName.Text = "";
//            Dgv_Info.DataSource = null;
            pnlInfo.Controls.Clear();
            Label_Info.Text = "";
            Label_BedNo.Text = "";
            Label_Change.Text = "";
            //dataGridView1.Rows.Remove()
            while(dgvCurrentDrug.Rows.Count>0)
            {
                dgvCurrentDrug.Rows.RemoveAt(0);
            }

            while (Dgv_Info.Rows.Count > 0)
            {
                Dgv_Info.Rows.RemoveAt(0);
            }
        }

        /// <summary>
        /// 设置信息
        /// </summary>
        /// <param name="R"></param>
        private void setPerson(DataRow R)
        {
            try
            {
                //床号
                lblBedNo.Text = R["BedNo"].ToString().Trim();
                //HIS医嘱号
                lblCaseID.Text = R["CaseID"].ToString().Trim();
                //医生名称+医生Code
                lblDoctor.Text = R["DocName"].ToString().Trim()+ "(" + R["DoctorCode"].ToString().Trim() + ")";
                //医嘱停止时间
                lblEndDT.Text = R["EndDT"].ToString().Trim();
                //性别
                string sex = R["Sex"].ToString().ToLower().Trim();
                if ("2" == sex)
                    lblSex.Text = "女";
                else if ("1" == sex)
                    lblSex.Text = "男";
                else
                    lblSex.Text = "其他";
                //开始时间
                lblStartDT.Text = R["StartDT"].ToString().Trim();
                //病区名称
                lblWard.Text = R["WardName"].ToString().Trim();
                //病人名称
                lblPatient.Text = R["PatName"].ToString().Trim();
                //体重
                lblWeight.Text = R["Weight"].ToString().Trim();
                //年龄
                lblAge.Text = R["年龄"].ToString().Trim();
                //用药途径
                laUName.Text = R["UsageName"].ToString().Trim();
                //频次
                lblBatch.Text = R["FreqName"].ToString().Trim();
                //发送按钮
                if (lblPatient.Text.Trim().Length>0 && pvb.IvBatchSaved==0)
                {
                    Panel_set.Visible = true;
                }
                else
                {
                    Panel_set.Visible = false;
                }
            }
            catch (System.Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10008:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }

        /// <summary>
        /// 设置当前处方所对药品
        /// </summary>
        /// <param name="dt"></param>
        private void setCurrentDrug(DataTable dt)
        {
            dgvCurrentDrug.Rows.Clear();
            //pnlInfo.Controls.Clear();
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    CurrentDrug Drug = new CurrentDrug();
            //    Drug.setDrug(dt.Rows[i]);
            //    pnlInfo.Controls.Add(Drug);
            //}
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgvCurrentDrug.Rows.Add(1);
                dgvCurrentDrug.Rows[i].Cells["colDrugName"].Value = dt.Rows[i]["DrugName"].ToString();
                dgvCurrentDrug.Rows[i].Cells["colSpec"].Value = dt.Rows[i]["Spec"].ToString();
                float f = 0;
                float.TryParse(dt.Rows[i]["Remark9"].ToString(), out f);
                if ((dt.Rows[i]["剂量"].ToString().Trim() == dt.Rows[i]["Remark9"].ToString().Trim() && dt.Rows[i]["CapacityUnit"].ToString() == dt.Rows[i]["单位"].ToString()) || dt.Rows[i]["Remark9"].ToString().Trim() == "0")
                {
                    dgvCurrentDrug.Rows[i].Cells["coldDosage"].Value = dt.Rows[i]["剂量"].ToString() + dt.Rows[i]["单位"].ToString();
                }
                else
                {
                    dgvCurrentDrug.Rows[i].Cells["coldDosage"].Value = dt.Rows[i]["剂量"].ToString() + dt.Rows[i]["单位"].ToString()
                        + "(" + dt.Rows[i]["Remark9"].ToString().Trim() + dt.Rows[i]["CapacityUnit"].ToString() + ")";
                }
                //dgvCurrentDrug.Rows[i].Cells["colUnit"].Value = dt.Rows[i]["单位"].ToString();
                dgvCurrentDrug.Rows[i].Cells["colDgNo"].Value = dt.Rows[i]["DgNo"].ToString();
                dgvCurrentDrug.Rows[i].Cells["colPishi"].Value = dt.Rows[i]["PiShi"].ToString();
                dgvCurrentDrug.Rows[i].Cells["remark7"].Value = dt.Rows[i]["Remark7"].ToString();
                dgvCurrentDrug.Rows[i].Cells["UniPreparationID"].Value = dt.Rows[i]["UniPreparationID"].ToString();
            }
                //dgvCurrentDrug.DataSource = dt;

            for (int i = 6; i < dgvCurrentDrug.ColumnCount; i++)
            {
                dgvCurrentDrug.Columns[i].Visible = false;
            }
          
        }


        /// <summary>
        /// 设置显示信息
        /// </summary>
        /// <param name="Groupno"></param>
        public void SetInformation(string Groupno)
        {
            try
            {
                string str = select.INFO(Groupno);

                DataTable dtinfo = new DataTable();

                DataSet ds = DB.GetPIVAsDB(str);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dtinfo = ds.Tables[0];
                    
                    setPerson(dtinfo.Rows[0]);
                    
                    //setCurrentDrug(dtinfo);
                    setCurrentDrug(ds.Tables[1]);
                }
            }
            catch (System.Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10009:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }

        /// <summary>
        /// 发送单个病人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel_set_Click(object sender, EventArgs e)
        {
            pvb.operate = true;
            DialogResult result = MessageBox.Show("确定发送此病人吗？", "确定发送此病人吗？ ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                //int Count = DB.SetPIVAsDB(update.IVRecordBatchSaved_OnePat(pvb.datetime.ToString("yyyyMMdd"), Patcode, Wardcode));
                string labelNos = "";
                labelNos = ((UserControlBatch)(this.Parent.Parent.Parent)).dgv_Info.patLabelNos(Patcode);
                string s1 = ((UserControlBatch)(this.Parent.Parent.Parent)).S1;
                string sk = ((UserControlBatch)(this.Parent.Parent.Parent)).SK;
                string ls1 = ((UserControlBatch)(this.Parent.Parent.Parent)).LS1;
                string lsk = ((UserControlBatch)(this.Parent.Parent.Parent)).LSK;
                int Count = DB.SetPIVAsDB(update.IVRecordBatchSaved_OnePat(labelNos, Patcode, Wardcode,s1,sk,ls1,lsk));
                 //刷新界面
                  ((UserControlBatch)(this.Parent.Parent.Parent)).ShowWard(Count);
                 //发送病人之后，留中间的第一张瓶签的组号
                 //if()
                 // string Groupno = Dgv_Info.Rows[0].Cells["组"].Value.ToString().Trim();
            }
        }

        /// <summary>
        /// 批次有更改的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_Change_TextChanged(object sender, EventArgs e)
        {
            if (Label_Change.Text.Trim().Length >0)
            {
                for (int i = 0; i < (((UserControlBatch)(this.Parent.Parent.Parent)).dgv_Info.Dgv_Info).Rows.Count; i++)
                {
                    if (((UserControlBatch)(this.Parent.Parent.Parent)).dgv_Info.Dgv_Info.Rows[i].Cells["瓶签号"].Value.ToString() == Label_Change.Text.Trim().ToString())
                    {
                        ((UserControlBatch)(this.Parent.Parent.Parent)).dgv_Info.Dgv_Info.Rows[i].Cells["批次"].Value = Batch;

                        ((UserControlBatch)(this.Parent.Parent.Parent)).dgv_Info.Dgv_Info.Rows[i].Cells["批次规则"].Value = batchrule;

                        ((UserControlBatch)(this.Parent.Parent.Parent)).dgv_Info.Dgv_Info.Rows[i].Cells["TeamNumber"].Value = Batch.Replace("-", "").Replace("#", "").Replace("K", "").Replace("L", "");
                    }
                }
                Label_Change.Text = "";   // ((Pivasbatch)(this.Parent.Parent.Parent)).dgv_Info.Dgv_Info. = true;
            }
        }

        /// <summary>
        /// 点击列的排序时。赋颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_Info_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            pvb.operate = true;
            ColorS();
        }

        /// <summary>
        /// 病人名称+病区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_Info_TextChanged(object sender, EventArgs e)
        {
            if (Label_Info.Text.IndexOf("\r") < 0) 
            {
                Label_Info.AutoSize = true;
            }
            
        }


        private void Label_Info_AutoSizeChanged(object sender, EventArgs e)
        {
            try
            {
                //如果病人信息的长度大于16的话。 换行
                int cout = Label_Info.Text.Trim().Substring(Label_Info.Text.Trim().IndexOf("床"), Label_Info.Text.Trim().Length - Label_Info.Text.Trim().IndexOf("床")).Length;
                //if (Label_Info.Text.Trim().Length > 16)
                //{
                //    Label_Info.Text = Label_Info.Text.Trim().Replace("床", "床\r");
                //}
               
            }
            catch { }
            Label_Info.AutoSize = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try 
            {
                if (e.ColumnIndex==0)
                {
                    LibDrugManual.RichFrom mf = new LibDrugManual.RichFrom();
                    string UniPreID = dgvCurrentDrug.CurrentRow.Cells["UniPreparationID"].Value.ToString();
                    mf.UniPreparationID = UniPreID;//UniPreID 是Drug表中的UniPreparationID 字段
                    mf.ShowDialog();
                }                
            }
            catch(Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10010:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }

        private void Med_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (splitContainer2.Width > 305)
                {

                    splitContainer2.SplitterDistance = splitContainer2.Width - 305;
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatch" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10030:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }

       

      


   
       
    }
}
