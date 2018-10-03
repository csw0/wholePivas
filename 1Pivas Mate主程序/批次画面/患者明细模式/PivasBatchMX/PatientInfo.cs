using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;
using PivasBatchCommon;
using PIVAsCommon.Helper;

namespace PivasBatchMX
{
    public partial class PatientInfo : UserControl
    {
        public PatientInfo()
        {
            InitializeComponent();
            DataSet ds = db.GetPIVAsDB(string.Format("SELECT * FROM [OrderFormSet] where  DEmployeeID='{0}'", pvb.DEmployeeID));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                refresh = ds.Tables[0].Rows[0]["Refresh"].ToString() == "1" ? true : false;
            }
            else
            {
                MessageBox.Show("请设置该员工的批次界面样式");
            }
        }

        public delegate void DelegateChangeTextValS(string TextVal, int tags, int Issame, string Labelno, string batch);
        // 2.定义委托事件  
        public event DelegateChangeTextValS ChangeTextVal;
        //病区Code
        string WardCode = string.Empty;
        DB_Help db = new DB_Help();
        UpdateSql update = new UpdateSql();
        SelectSql select = new SelectSql();
        string PatCode = string.Empty;
        //编辑状态（能否改批次）
        bool Smalledit = false;

        public string premode = "0";
        public DateTime datetime;
        public bool tag = false;
        int batchsaved = 0;
        //Pivasbatch pvb;
        float KSum = 0;
        DataSet DrugInfo = new DataSet();

        int DosageLimit = 50;
        string s1 = string.Empty;
        string sk = string.Empty;
        string ls1 = string.Empty;
        string lsk = string.Empty;

        public bool refresh = true; //修改批次后是否刷新界面

        #region  画边框

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Pen pen = new Pen(Color.Gray, 1);//颜色和粗细
            Pen pen = new Pen(System.Drawing.ColorTranslator.FromHtml("#4B6076"), 1);
            DrawRoundRect(e.Graphics, pen, 0, 0, panel1.Width - 1, panel1.Height - 1, 1);
            //绘制的图形，颜色和粗细，绘制图形的初始X，绘制图形的初始Y，绘制图形的宽度，绘制图形的高度，绘制图形的弧度
        }


        /// <summary>
        /// 划圆角边框
        /// </summary>
        /// <param name="g">绘制的图形</param>
        /// <param name="p">颜色和粗细</param>
        /// <param name="X">绘制图形的初始X</param>
        /// <param name="Y">绘制图形的初始Y</param>
        /// <param name="width">绘制图形的宽度</param>
        /// <param name="height">绘制图形的高度</param>
        /// <param name="radius">绘制图形的弧度</param>
        public void DrawRoundRect(Graphics g, Pen p, float X, float Y, float width, float height, float radius)
        {
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();

            gp.AddLine(X + radius, Y, X + width - (radius * 2), Y);

            gp.AddArc(X + width - (radius * 2), Y, radius * 2, radius * 2, 270, 90);

            gp.AddLine(X + width, Y + radius, X + width, Y + height - (radius * 2));

            gp.AddArc(X + width - (radius * 2), Y + height - (radius * 2), radius * 2, radius * 2, 0, 90);

            gp.AddLine(X + width - (radius * 2), Y + height, X + radius, Y + height);

            gp.AddArc(X, Y + height - (radius * 2), radius * 2, radius * 2, 90, 90);

            gp.AddLine(X, Y + height - (radius * 2), X, Y + radius);

            gp.AddArc(X, Y, radius * 2, radius * 2, 180, 90);

            gp.CloseFigure();

            g.DrawPath(p, gp);

            gp.Dispose();
            g.Dispose();
        }

        #endregion

     

        /// <summary>
        /// 病人详细数据显示
        /// </summary>
        /// <param name="Warcode">病区号 </param>
        /// <param name="Patcode">病人号 </param>
        /// <param name="dt"></param>
        /// <param name="s">是否已发送</param>
        /// <param name="tags">全是false</param>
        /// <param name="ClearS">是否清空</param>
        /// <param name="BatchSelect">批次筛选 SQL ，批次主画面传入</param>
        public void SetInfo(string Warcode, string Patcode, DateTime dt, int s, bool tags, bool ClearS,string s1,string sk,string ls1,string lsk)
        {
            GetDosageLimit();
            tag = tags;
            batchsaved = s;
            datetime = dt;
            WardCode = Warcode;
            PatCode = Patcode;
            this.s1 = s1;
            this.sk = sk;
            this.ls1 = ls1;
            this.lsk = lsk;
            string Sum = string.Empty;
            try
            {

                DrugInfo = db.GetPIVAsDB(select.IVRecordPatient(WardCode, dt.ToString("yyyyMMdd"), batchsaved, tags, Patcode, pvb.LabelOrderBy));
                if (DrugInfo == null || DrugInfo.Tables.Count == 0||DrugInfo.Tables[0].Rows.Count==0)
                {
                    return;
                }
                string sum1 = DrugInfo.Tables[0].Rows[0][0].ToString() + "袋";

                DataTable ddt = ChangeUnit(DrugInfo.Tables[1]);
                 ddt= DataRowToDatatable(ddt.Select(" (DosageUnit='ml' OR DosageUnit='l') and Dosage>" + DosageLimit + " "), ddt);
              
                //计算ml的液体量
                Sum = sum1 +" "+ (DosageCount(ddt, "ml") + DosageCount(ddt, "l") * 1000);
                //如果'袋'后面有数量的话
                if (Sum.IndexOf("袋") != Sum.Length - 1)
                {
                    Label_Sum.Text = Sum + "ml";
                }

                else
                {
                    Label_Sum.Text = Sum + "0ml";
                }
                //显示病人详细基础信息
                if (DrugInfo.Tables[1].Rows.Count > 0)
                {
                    if (DrugInfo.Tables[1].Rows[0]["IsSame"] == null || DrugInfo.Tables[1].Rows[0]["IsSame"].ToString().Trim().Length ==0 || DrugInfo.Tables[1].Rows[0]["IsSame"].ToString() == "False")
                    {
                        Label_IsSame.Visible = false;
                        label5.BackColor = Color.Transparent;
                    }
                    else
                    {
                        Label_IsSame.Visible = true;
                        label5.BackColor = System.Drawing.ColorTranslator.FromHtml("255, 119, 0");
                    }
                    //Label_GennerationBatch
                    Label_Name.Text = DrugInfo.Tables[1].Rows[0]["PatName"].ToString();
                    Label_Bed.Text = DrugInfo.Tables[1].Rows[0]["BedNo"].ToString().Trim().Replace("床", "") + "床";
                    Label_age.Text = DrugInfo.Tables[1].Rows[0]["age"].ToString() + DrugInfo.Tables[1].Rows[0]["AgeSTR"].ToString();
                    Label_Sex.Text = DrugInfo.Tables[1].Rows[0]["Sex"].ToString();
                    WardCode = "'" + DrugInfo.Tables[1].Rows[0]["WardCode"].ToString() + "'";
                    PatCode = DrugInfo.Tables[1].Rows[0]["PatCode"].ToString();
                    laWardName.Text = DrugInfo.Tables[1].Rows[0]["WardName"].ToString().Trim();
                    Label_PCode.Text = "病人编码：" + Patcode;
                    Show(Patcode);

                }
            }
            catch (Exception ee)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatchMX" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10004:" + ee.Message +"    " + DateTime.Now.ToString()+"\r\n");
           
            }
            DrugInfo.Dispose();
        }

        /// <summary>
        /// 显示病人药品和输液量
        /// </summary>
        /// <param name="Patcode"></param>
        public void SetInfo(string Patcode)
        {          
            GetDosageLimit();
            PatCode = Patcode;
            DataSet DrugInfo = new DataSet();
            this.Panel_Drug.Controls.Clear();
            string Sum = string.Empty;
            try
            {
                DrugInfo = db.GetPIVAsDB(select.IVRecordPatient(WardCode, datetime.ToString("yyyyMMdd"), batchsaved, tag, Patcode, pvb.LabelOrderBy));
                string sum1 = DrugInfo.Tables[0].Rows[0][0].ToString() + "袋";


                DataTable ddt = ChangeUnit(DrugInfo.Tables[1]);
                ddt = DataRowToDatatable(ddt.Select(" (DosageUnit='ml' OR DosageUnit='l') and Dosage>" + DosageLimit + " "), ddt);

                Sum = sum1 +( (DosageCount(ddt, "ml") + DosageCount(ddt, "l") * 1000)).ToString();
                if (Sum.IndexOf("袋") != Sum.Length - 1)
                {
                    Label_Sum.Text = Sum + "ml";
                }
                else
                {
                    Label_Sum.Text = Sum + "0ml";
                }
                if (DrugInfo.Tables[1].Rows.Count > 0)
                {
                    if (DrugInfo.Tables[1].Rows[0]["IsSame"] == null || DrugInfo.Tables[1].Rows[0]["IsSame"].ToString().Trim().Length == 0 || DrugInfo.Tables[1].Rows[0]["IsSame"].ToString() == "False")
                    {
                        Label_IsSame.Visible = false;
                        label5.BackColor = Color.Transparent;
                    }
                    else
                    {
                        Label_IsSame.Visible = true;
                        label5.BackColor = System.Drawing.ColorTranslator.FromHtml("255, 119, 0");
                    }
                    //Label_GennerationBatch
                    Label_Name.Text = DrugInfo.Tables[1].Rows[0]["PatName"].ToString().Trim();
                    Label_Bed.Text = DrugInfo.Tables[1].Rows[0]["BedNo"].ToString().Trim().Replace("床", "") + "床";
                    Label_age.Text = DrugInfo.Tables[1].Rows[0]["age"].ToString().Trim();
                    Label_Sex.Text = DrugInfo.Tables[1].Rows[0]["Sex"].ToString().Trim();
                    WardCode = "'" + DrugInfo.Tables[1].Rows[0]["WardCode"].ToString().Trim() + "'";
                    // string MaxSum = string.Empty;
                    laWardName.Text = DrugInfo.Tables[1].Rows[0]["WardName"].ToString().Trim();
                    PatCode = DrugInfo.Tables[1].Rows[0]["PatCode"].ToString().Trim();
                    Show(Patcode);

                }
            }
            catch (Exception ee)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatchMX" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10005:" + ee.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
            finally
            {
                DrugInfo.Dispose();
                pvb.operate = true;
            }
         
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Patcode"></param>
        private void Show(string Patcode)
        {
            try
            {
                GetDosageLimit();
                //批次。
                string TeamNumber = string.Empty;
                //瓶签
                string LabelNo = string.Empty;
                //数量
                object MinSum = string.Empty;
                // int KSum = string.Empty;
                DataTable Kdt = new DataTable();
                DataTable ddt = new DataTable();
                //药品数量（控件）
                int Mincount = 0;
                //输液量数量(控件)
                int count = 0;
                string TColor = "";
                string OrderColor = "";
                //是否有分割线
                bool druglineshow = false;
                DrugInfo = db.GetPIVAsDB(select.IVRecordPatient(WardCode, datetime.ToString("yyyyMMdd"), batchsaved, tag, Patcode, pvb.LabelOrderBy));
                DataTable DTable = DrugInfo.Tables[1].Copy();
                DTable = ChangeUnit(DTable);
                this.Panel_Drug.Controls.Clear();
                for (int i = 0; i < DTable.Rows.Count; i++)
                {

                    object Drugcount = "0";
                    druglineshow = false;
                    //药品数量
                    Drugcount = DTable.Compute("Count(LabelNo)", " LabelNo='" + DTable.Rows[i]["LabelNo"].ToString() + "'");
                    if ((int)Drugcount > 1)
                    {
                        druglineshow = false;
                    }
                    else
                    {
                        druglineshow = true;
                    }
                    //①如果保存批次等于Row[i]的批次
                    //②如果保存瓶签等于Row[i]的瓶签
                    if (TeamNumber == DTable.Rows[i]["TeamNumber"].ToString() && LabelNo == DTable.Rows[i]["LabelNo"].ToString())
                    {

                        if (i + 1 < DTable.Rows.Count)
                        {
                            //①如果批次和下一条数据不一样
                            //②或者瓶签和下一条数据不一样
                            if (TeamNumber != DTable.Rows[i + 1]["TeamNumber"].ToString() || LabelNo != DTable.Rows[i + 1]["LabelNo"].ToString())
                            {
                                //药品中的包括线显示
                                ChooseUserControls(DTable.Rows[i], 2, false, false, MinSum, OrderColor, TColor, true, druglineshow,refresh);
                                count++;
                            }
                            else
                            {
                                //药品中的包括线不显示
                                ChooseUserControls(DTable.Rows[i], 2, false, false, MinSum, OrderColor, TColor, false, druglineshow, refresh);
                                count++;
                            }

                        }
                        //如果是最后一条数据
                        else if (i + 1 == DTable.Rows.Count)
                        {
                            //包括线显示
                            ChooseUserControls(DTable.Rows[i], 2, false, false, MinSum, OrderColor, TColor, true, druglineshow, refresh);
                            count++;
                        }
                        else
                        {
                            ChooseUserControls(DTable.Rows[i], 2, false, false, MinSum, OrderColor, TColor, false, druglineshow, refresh);
                            count++;
                        }
                    }
                    else
                    {
                        //数据下的分割线是否显示
                        bool _Yes = true;
                        if (i == 0)
                        {
                            TeamNumber = DTable.Rows[i]["TeamNumber"].ToString();
                        }                  
                        if (TeamNumber != DTable.Rows[i]["TeamNumber"].ToString())
                        {
                            ddt = DataRowToDatatable(DTable.Select(" (DosageUnit='ml' or DosageUnit='l') and Dosage>" + DosageLimit + " and TeamNumber='" + TeamNumber + "'"), DTable);
                            //计算总液体量
                            MinSum = (DosageCount(ddt, "ml") + DosageCount(ddt, "l") * 1000);

                            Kdt = DataRowToDatatable(DTable.Select(" (DosageUnit='ml' or DosageUnit='l') and (Batch like '%K' or Batch like '%L')  and Dosage>" + DosageLimit + " and TeamNumber='" + TeamNumber + "'"), DTable);
                            KSum = 0;
                            //计算空包
                            KSum = (DosageCount(Kdt, "ml") + DosageCount(Kdt, "l") * 1000);
                            //加载输液总量控件
                            ChooseUserControls(DTable.Rows[i], 3, true, true, MinSum, OrderColor, TColor, false, druglineshow, refresh);
                            TeamNumber = DTable.Rows[i]["TeamNumber"].ToString();
                            Mincount++;
                            _Yes = false;
                        }



                        //加载药品控件
                        if (_Yes)
                        {
                            count++;
                            ChooseUserControls(DTable.Rows[i], 2, true, _Yes, MinSum, OrderColor, TColor, true, druglineshow, refresh);
                        }
                        else
                        {
                            if (i + 1 < DTable.Rows.Count)
                            {
                                if (TeamNumber != DTable.Rows[i + 1]["TeamNumber"].ToString() || LabelNo != DTable.Rows[i + 1]["LabelNo"].ToString())
                                {
                                    count++;
                                    ChooseUserControls(DTable.Rows[i], 2, true, _Yes, MinSum, OrderColor, TColor, true, druglineshow, refresh);
                                }
                                else
                                {
                                    count++;
                                    ChooseUserControls(DTable.Rows[i], 2, true, _Yes, MinSum, OrderColor, TColor, false, druglineshow, refresh);
                                }

                            }
                            else
                            {
                                count++;
                                ChooseUserControls(DTable.Rows[i], 2, true, _Yes, MinSum, OrderColor, TColor, false, druglineshow, refresh);
                            }
                        }
                        // count++;
                        LabelNo = DTable.Rows[i]["LabelNo"].ToString();
                    }
                    OrderColor = DTable.Rows[i]["OrderColor"].ToString();
                    TColor = DTable.Rows[i]["TColor"].ToString();
                }
              
                    TeamNumber = DTable.Rows[DTable.Rows.Count - 1]["TeamNumber"].ToString();
                    ddt = DataRowToDatatable(DTable.Select(" (DosageUnit='ml' or DosageUnit='l') and Dosage>" + DosageLimit + " and TeamNumber='" + TeamNumber + "'"), DTable);

                    MinSum = DosageCount(ddt, "ml") + DosageCount(ddt, "l") * 1000;

                    KSum = 0;
                    DataTable kkdt = new DataTable();
                    kkdt = DataRowToDatatable(DTable.Select(" (DosageUnit='ml' or DosageUnit='l') and (Batch like '%K' or Batch like '%L')  and Dosage>" + DosageLimit + " and TeamNumber='" + TeamNumber + "'"), DTable);

                    KSum = DosageCount(kkdt, "ml") + DosageCount(kkdt, "l") * 1000;
                    ChooseUserControls(DTable.Rows[DTable.Rows.Count - 1], 3, true, true, MinSum, OrderColor, TColor, false, druglineshow, refresh);
                    if (refresh)
                    {
                        Mincount++;
                    }
                    kkdt.Dispose();
                
                //信息的高度
             
                Panel_Drug.Height = 27 * count + Mincount * 29 + 30;
                Mincount = 0;
                this.Height = Panel_Drug.Height + panel1.Height - 20;

                
                Kdt.Dispose();
                ddt.Dispose();
                DTable.Dispose();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatchMX" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10001:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            
            }
        }

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
        /// 显示药品或批次总量
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="UserControlTape">2显示药品3显示单批次溶剂总量</param>
        /// <param name="tags">是否有批次信息</param>
        /// <param name="Lineshow">数据下的分割线是否显示</param>
        /// <param name="MinSum">剂量总量</param>
        /// <param name="OrderColor">颜色</param>
        /// <param name="TColor">字体颜色</param>
        /// <param name="line">药品中的包括线是否显示</param>
        /// <param name="all"> 是否为单个药，true是，false否</param>

        public void ChooseUserControls(DataRow dr, int UserControlTape, bool tags, bool Lineshow, object MinSum, string OrderColor, string TColor, bool line, bool all,bool refresh)
        {
            try
            {
                if (UserControlTape == 2)
                {
                    BatchInfo Batch = new BatchInfo();
                    Batch.Width = Panel_Drug.Width;
                    //Batch.SetInfo(dr, tags, Lineshow, line, all, premode, batchsaved, refresh);
                    Batch.SetInfo(dr, tags, true, line, all, premode, batchsaved,refresh);
                    Panel_Drug.Controls.Add(Batch);

                }
                else if(UserControlTape==3&& refresh)
                {                
                    DrugSum sun = new DrugSum();
                    sun.Width = Panel_Drug.Width ;
                    sun.SetInfo(OrderColor, TColor, MinSum.ToString(), KSum.ToString());
                    Panel_Drug.Controls.Add(sun);
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatchMX" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10015:" + ex.ToString() + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }


        /// <summary>
        /// 发送病人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_OnePGet_Click(object sender, EventArgs e)
        {
            try
            {
                if (tag != true)
                {
                    DialogResult result = MessageBox.Show("确定发送吗？", "确定发送此病人吗？ ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        db.SetPIVAsDB(update.IVRecordBatchSaved_OnePat(pvb.datetime.ToString("yyyyMMdd"), PatCode, WardCode,s1,sk,ls1,lsk));
                        //string LabelNos = "";

                        //for (int i = 0; i < Panel_Drug.Controls.Count;i++ )
                        //{
                        //    if (Panel_Drug.Controls[i] is BatchInfo)
                        //    {
                        //        LabelNos += ((BatchInfo)(Panel_Drug.Controls[i])).LabelNo + ",";
                        //    }
                        //}
                        //if (LabelNos.Length>0)
                        //{
                        //    LabelNos = LabelNos.Remove(LabelNos.Length - 1, 1);
                        //}


                        //db.SetPIVAsDB(update.IVRecordBatchSaved_OnePat(LabelNos, PatCode, WardCode));
                        int Count = int.Parse(Label_Sum.Text.ToString().Substring(0,Label_Sum.Text.IndexOf("袋")));
                        #region 陈松伟注释
                        //((Pivasbatch)(this.Parent.Parent)).ShowWard(Count);
                        #endregion 陈松伟注释

                        // ((Pivasbatch)(this.Parent.Parent)).ShowDrug(pvb.ward, false);
                    }
                }
                else
                {
                    MessageBox.Show("此瓶签已打印");
                }
                pvb.operate = true;
            }
            catch (System.Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatchMX" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10007:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }

        }

        private void Label_Smalledit_Click(object sender, EventArgs e)
        {
            Smalledit = true;
        }


        private void Panel_Drug_Resize(object sender, EventArgs e)
        {
            if (this.Width != 0 && this.Height != 0)
            {
                Foreach(Panel_Drug);
            }
        }

        private void Foreach(Control cn)
        {
            cn.Width = this.Width;
            foreach (Control j in cn.Controls)
            {
                if (j is BatchInfo)
                {
                    j.Width = this.Width - 10;
                }
                else
                {
                    j.Width = this.Width - 20;
                }
            }
        }

        /// <summary>
        /// 重排病人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_GennerationBatch_Click(object sender, EventArgs e)
        {

            if (db.GetPIVAsDB(select.PBatchTemp()) == null || db.GetPIVAsDB(select.PBatchTemp()).Tables[0].Rows.Count <= 0)
            {
                DialogResult result = MessageBox.Show("确定重排此病人的所有批次吗？", "确定重排此病人的所有批次吗？ ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    //timer1.Enabled = false;
                    string sward = "";
                    sward = pvb.ward.Replace("'", "");
                    Wait wait = new Wait(datetime, pvb.DEmployeeID, sward, PatCode, 1);
                    DialogResult rr = wait.ShowDialog();
                    if (rr == DialogResult.Cancel)
                    {
                        SetInfo(PatCode);
                    }
                    else
                    {
                        SetInfo(PatCode);
                    }
                }

            }
            else
            {
                MessageBox.Show("正在排批次，请稍候！");
            }
        }

        private void Label_GennerationBatch_MouseMove(object sender, MouseEventArgs e)
        {
            //Label Label_UKL = (Label)sender;
            // Label_UKL.BackgroundImage = (Image)PivasBatch.Properties.Resources.ResourceManager.GetObject("按钮");
        }

        private void Label_GennerationBatch_MouseLeave(object sender, EventArgs e)
        {
            // Label Label_UKL = (Label)sender;
            //Label_UKL.BackgroundImage = null;
        }

        /// <summary>
        /// 计算总量
        /// </summary>
        /// <param name="dt">数据集</param>
        /// <param name="DosageUnit">单位</param>
        private float DosageCount(DataTable dt, string DosageUnit)
        {
            if (dt.Compute("Sum(Dosage)", "DosageUnit='" + DosageUnit + "'") != null && dt.Compute("Sum(Dosage)", "DosageUnit='" + DosageUnit + "'").ToString().Trim().Length != 0)
            {
                return (float.Parse(dt.Compute("Sum(Dosage)", "DosageUnit='" + DosageUnit + "'").ToString()));
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取药品算入液体总量的dosage限制
        /// </summary>
        private void GetDosageLimit()
        {
            try
            {
                DosageLimit = Convert.ToInt32(db.GetPivasAllSet("批次-病人明细模式-液体总量计算"));
            }
            catch(Exception ex)
            {
                DosageLimit = 50;
                MessageBox.Show( ex.Message+ "请设置 批次-病人明细模式-液体总量计算");
            }
        }
        /// <summary>
        /// 转换药品的单位
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable ChangeUnit(DataTable dt)
        {
           
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["DosageUnit"].ToString() == "毫升" )
                {
                    dt.Rows[i]["DosageUnit"] = "ml";
            
                }
                else if (dt.Rows[i]["DosageUnit"].ToString() == "升" )
                {
                    dt.Rows[i]["DosageUnit"] = "l";
                }
                else
                {
                    dt.Rows[i]["DosageUnit"] = dt.Rows[i]["DosageUnit"].ToString().ToLower();
                }


                if (dt.Rows[i]["CapacityUnit"].ToString() == "毫升")
                {
                    dt.Rows[i]["CapacityUnit"] = "ml";
                }
                else if (dt.Rows[i]["CapacityUnit"].ToString() == "升")
                {
                    dt.Rows[i]["CapacityUnit"] = "l";           
                }
                else
                {
                    dt.Rows[i]["CapacityUnit"] = dt.Rows[i]["CapacityUnit"].ToString().ToLower();
                }
            }
           
            return dt;
        
        }
        
       

       

    }
}
