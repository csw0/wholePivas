using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PivasBatchCommon;
using PIVAsCommon.Helper;

namespace PivasBatchMX
{
    public partial class BatchInfo : UserControl
    {  //瓶签号
        public string LabelNo;
        //批次规则
        public string BatchRule;
        //病人Code
        public string PetCode;
        //浏览模式
        public string premode;
        //批次
        public string Teamter;
        //public int MainWith = 0;
        // Pivasbatch pvb;
        //
        bool batchsaved = false;
        DB_Help db = new DB_Help();
        bool refresh = true; //修改批次后是否刷新界面
        public BatchInfo()
        {
            InitializeComponent();
        }
      
        /// <summary>
        /// 数据初始化
        /// </summary>
        /// <param name="dr">单条数据</param>
        /// <param name="tags">是否有批次信息</param>
        /// <param name="lineshow">数据下的分割线是否显示</param>
        /// <param name="bottomline">药品中的包括线是否显示</param>
        public void SetInfo(DataRow dr, bool tags, bool lineshow, bool bottomline, bool all, string premode, int batchsave,bool refresh)
        {
           // MessageBox.Show(this.Size.ToString());
            //this.pvb=pvb;
            this.premode = premode;
            this.refresh = refresh;
            try
            {
                if (tags)
                {
                     //批次
                        Label_Batch.Text = dr["Batch"].ToString();
                        //批次颜色（根据数据库中的OrderColor表）
                        Label_Batch.BackColor = System.Drawing.ColorTranslator.FromHtml(dr["OrderColor"].ToString());
                        Label_Batch.ForeColor = System.Drawing.ColorTranslator.FromHtml(dr["TColor"].ToString());
                  
                    //是否发送或者是否打印

                    //***********张建双2014-07-18********************//
                        try
                        {
                            if (dr["IVStatus"].ToString() == "已打印" && batchsave == 0)
                            {
                                batchsaved = false;
                                Label_BatchSaved.Text = "已打印";
                                Label_BatchSaved.ForeColor = Color.White;
                                Label_BatchSaved.BackColor = Color.Gray;
                            }
                            else if (dr["BatchSaved"].ToString() == "已发送" && batchsave == 0)
                            {
                                batchsaved = true;
                                Label_BatchSaved.Text = "已发送";
                                Label_BatchSaved.ForeColor = Color.White;
                                Label_BatchSaved.BackColor = Color.Gray;
                            }
                            else if (dr["IVStatus"].ToString() == "已打印")
                            {
                                batchsaved = false;
                                Label_BatchSaved.Text = "已打印";
                            }
                            else if (dr["BatchSaved"].ToString() == "已发送")
                            {
                                batchsaved = true;
                                Label_BatchSaved.Text = "已发送";
                            }
                            else
                            {
                                batchsaved = false;
                                Label_BatchSaved.Text = "未发送";
                            }
                        }
                        catch (Exception ex)
                        {
                            File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatchMX" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10023:" + ex.ToString() + "    " + DateTime.Now.ToString() + "\r\n");
                        }
                    //***********张建双2014-07-18********************//

                    //if (dr["IVStatus"].ToString() == "已打印") || dr["BatchSaved"].ToString() == "已发送")
                    //{
                    //    batchsaved = true;
                    //    if (dr["BatchSaved"].ToString() == "已发送")
                    //    {
                    //        Label_BatchSaved.Text = dr["IVStatus"].ToString();
                    //    }
                    //    else
                    //    {
                    //        Label_BatchSaved.Text = dr["BatchSaved"].ToString();
                    //    }
                    //}
                    //else 
                    //{
                    //    batchsaved = false;
                    //    Label_BatchSaved.Text = "已打印";
                    //}
                    try
                    {
                        //频次
                        Label_FreqName.Text = dr["FreqName"].ToString();
                        //药品名
                        Panel_DrugInfo_DrugName.Text = dr["DrugName"].ToString();
                        //药品名全
                        //this.toolTip1.SetToolTip(this.Panel_DrugInfo_DrugName, dr["DrugName"].ToString());
                        //剂量+剂量单位
                        if ((dr["Dosage"].ToString().Trim() == dr["Remark9"].ToString().Trim() && dr["DosageUnit"].ToString() == dr["CapacityUnit"].ToString()) || dr["Remark9"].ToString().Trim() == "0")
                        {
                            Panel_DrugInfo_DrugDosage.Text = dr["Dosage"].ToString() + dr["DosageUnit"].ToString();
                        }
                        else
                        {
                            Panel_DrugInfo_DrugDosage.Text = dr["Dosage"].ToString() + dr["DosageUnit"].ToString().Trim() + "("
                                + dr["Remark9"].ToString().Trim() + dr["CapacityUnit"].ToString() + ")";
                        }
                    }
                    catch (Exception ex)
                    {
                        File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatchMX" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10020:" + ex.ToString() + "    " + DateTime.Now.ToString() + "\r\n");
                    }
                    try
                    {
                        //规格
                        Panel_DrugInfo_DrugSpec.Text = dr["Spec"].ToString();
                        //批次规则
                        BatchRule = dr["BatchRule"].ToString();
                        //批次(纯数量)
                        Teamter = dr["TeamNumber"].ToString();
                        //瓶签号
                        LabelNo = dr["LabelNo"].ToString();
                        Label_LabelNo.Text = LabelNo;
                        //病人Code
                        PetCode = dr["PatCode"].ToString();
                        //组号
                        Label_GroupNo.Text = dr["GroupNo"].ToString();
                        //接受时间
                        Label_InsertDt.Text = dr["InsertDT"].ToString();
                        //药品分割线
                        LabelLine.Parent = this;
                        //匚 中的上面一横显示
                        Label_Linetop.Visible = true;
                        //药品分割线置顶
                        LabelLine.BringToFront();
                        //用法
                        laUsageCode.Text = dr["UsageName"].ToString();
                    }
                    catch (Exception ex)
                    {
                        File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatchMX" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10021:" + ex.ToString() + "    " + DateTime.Now.ToString() + "\r\n");
                    }
                }
                else
                {
                    try
                    {
                        //匚 中的下面一横
                        Label_Linebottom.Visible = bottomline;
                        //不显示重复信息(批次，频次，发送状态等)
                        Panel_Batch.Controls.Clear();
                        //this.Controls.Remove(Panel_Batch);
                        //药品
                        Panel_DrugInfo_DrugName.Text = dr["DrugName"].ToString().Trim();
                        //this.toolTip1.SetToolTip(this.Panel_DrugInfo_DrugName, dr["DrugName"].ToString().Trim());
                        Panel_DrugInfo_DrugDosage.Text = dr["Dosage"].ToString() + dr["DosageUnit"].ToString();
                        Panel_DrugInfo_DrugSpec.Text = dr["Spec"].ToString();
                    }
                    catch (Exception ex)
                    {
                        File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatchMX" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10022:" + ex.ToString() + "    " + DateTime.Now.ToString() + "\r\n");
                    }
                }
                try
                {
                    //如果是两个药品之间不显示分割线
                    if (!lineshow)
                    {

                        this.Controls.Remove(LabelLine);
                    }
                    else
                    {
                        LabelLine.Width = LabelLine.Width - 10;
                    }

                    //如果是一个药品的。匚 都显示
                    if (all)
                    {
                        Label_Linetop.Visible = true;
                        Label_Linebottom.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatchMX" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10024:" + ex.ToString() + "    " + DateTime.Now.ToString() + "\r\n");
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatchMX" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10008:" + ex.ToString() + "    " + DateTime.Now.ToString() + "\r\n");
              
            }
            ////定位
            if (this.Width != 0 && this.Height != 0)
            {
                try
                {
                    if (premode == "0")
                    {
                        if (this.Width > 524)
                        {
                            this.panel2.Visible = true;
                            Panel_DrugInfo.Width = this.Width - (this.panel2.Width + Label_FreqName.Location.X + Label_FreqName.Width);
                            Panel_DrugInfo.Location = new Point(Label_FreqName.Location.X + Label_FreqName.Width, Panel_DrugInfo.Location.Y);
                        }
                        else
                        {
                            this.panel2.Visible = false;
                            Panel_DrugInfo.Width = this.Width - ( Label_FreqName.Location.X + Label_FreqName.Width);
                            Panel_DrugInfo.Location = new Point(Label_FreqName.Location.X + Label_FreqName.Width, Panel_DrugInfo.Location.Y);
                        }
                    }
                    //else
                    //{
                    //    if (this.Width > 633)
                    //    {
                    //        Panel_DrugInfo.Width = this.Width - (laUsageCode.Location.X + laUsageCode.Width);
                    //        Panel_DrugInfo.Location = new Point(laUsageCode.Location.X + laUsageCode.Width, Panel_DrugInfo.Location.Y);
                    //    }
                    //    else
                    //    {
                    //        Panel_DrugInfo.Width = this.Width - (laUsageCode.Location.X + laUsageCode.Width);
                    //        Panel_DrugInfo.Location = new Point(laUsageCode.Location.X + laUsageCode.Width, Panel_DrugInfo.Location.Y);
                    //    }

                    //}

                }
                catch (System.Exception ex)
                {
                    File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatchMX" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10002:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");                
                }
            }
        }

        private void Label_Batch_Click(object sender, EventArgs e)
        {
            try
            {
                pvb.operate = true;

                if ((pvb.IvBatchSaved == 1 || Label_BatchSaved.Text == "已发送") && pvb.ChangeSendBatch == "0")
                {
                    MessageBox.Show("<已发送> 状态瓶签不可修改批次，如需修改，请修改配置", "批次提示", MessageBoxButtons.OK);
                    return;
                }
                if ((pvb.IvBatchSaved == 2 || Label_BatchSaved.Text == "已打印") && pvb.ChangePrintBatch == "0")
                {
                    MessageBox.Show("<已打印> 状态瓶签不可修改批次，如需修改，请修改配置", "批次提示", MessageBoxButtons.OK);
                    return;
                }
                DataSet dss = db.GetPIVAsDB(string.Format( "select *from IVRecord where LabelNo='{0}'",LabelNo));
                Pivas_setBatch batch = new Pivas_setBatch();
                string teamnum=dss.Tables[0].Rows[0]["TeamNumber"].ToString();
                string batchrule=dss.Tables[0].Rows[0]["BatchRule"].ToString();
                string br= dss.Tables[0].Rows[0]["Batch"].ToString();
                batch.ShowSetBatch(true, LabelNo,teamnum , batchrule, this, PetCode,br , refresh);
                batch.ShowDialog();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatchMX" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10012:" + ex.ToString() + "    " + DateTime.Now.ToString() + "\r\n");
            }
          
        }

        public void UpdateBatchShow(string Batch)
        {
            Label_Batch.Text = Batch;
        }


        #region  画边框

        private void Label_Batch_Paint(object sender, PaintEventArgs e)
        {
            // Pen pen = new Pen(Color.Gray, 1);//颜色和粗细
            Pen pen = new Pen(System.Drawing.ColorTranslator.FromHtml("#4B6076"), 1);
            DrawRoundRect(e.Graphics, pen, 0, 0, Label_Batch.Width - 1, Label_Batch.Height - 1, 3);
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
        }

        #endregion

        private void BatchInfo_SizeChanged(object sender, EventArgs e)
        {
            try
            {

                if (this.Width != 0 && this.Height != 0)
                {
                    if (premode == "0")
                    {
                        if (this.Width > 524)
                        {
                            this.panel2.Visible = true;
                            Panel_DrugInfo.Width = this.Width - (this.panel2.Width + Label_FreqName.Location.X + Label_FreqName.Width);
                            Panel_DrugInfo.Location = new Point(Label_FreqName.Location.X + Label_FreqName.Width, Panel_DrugInfo.Location.Y);
                        }
                        else
                        {
                            this.panel2.Visible = false;
                            Panel_DrugInfo.Width = this.Width - (Label_FreqName.Location.X + Label_FreqName.Width);
                            Panel_DrugInfo.Location = new Point(Label_FreqName.Location.X + Label_FreqName.Width, Panel_DrugInfo.Location.Y);
                        }
                    }
                    else
                    {
                        if (this.Width > 624)
                        {
                            Panel_DrugInfo.Width = this.Width - (laUsageCode.Location.X + laUsageCode.Width);
                            Panel_DrugInfo.Location = new Point(laUsageCode.Location.X + laUsageCode.Width, Panel_DrugInfo.Location.Y);

                        }
                        else
                        {
                            Panel_DrugInfo.Width = this.Width - (laUsageCode.Location.X + laUsageCode.Width);
                            Panel_DrugInfo.Location = new Point(laUsageCode.Location.X + laUsageCode.Width, Panel_DrugInfo.Location.Y);
                        }

                    }

                }

            }
            catch (System.Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatchMX" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10003:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            ((FlowLayoutPanel)(this.Parent.Parent.Parent)).Focus();
        }

       
    }
}
