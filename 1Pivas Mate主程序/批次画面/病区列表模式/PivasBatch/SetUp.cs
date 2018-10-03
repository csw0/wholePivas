using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PivasBatch;
using PivasBatchCommon;
using PIVAsCommon.Helper;
//using System.Windows.Controls;

namespace PivasBatch
{
    public partial class SetUp : Form
    {
        //用户Code
        string Uid = string.Empty;
        //用户Name
        string DEmployeeName = string.Empty;
        DB_Help db = new DB_Help();
        SelectSql select = new SelectSql();
        InsertSql inert = new InsertSql();
        UpdateSql update = new UpdateSql();
        
        //批次颜色
        string BColor = string.Empty;
        //字体颜色
        string TColor = string.Empty;
        DataSet ds = new DataSet();
        //批次选择的索引
        int Comb_Select = 0;
        
        //是否有改变   0没改变 1有改变
        int ts = 0;

        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion
        public SetUp(string UID, string DName)
        {
            Uid = UID;
            DEmployeeName = DName;
            InitializeComponent();
        }



        public void Clear()
        {
            Uid = null;
            DEmployeeName = null;
            BColor = null;
            TColor = null;
            ds.Dispose();
            Comb_Select = 0;
            //是否有改变   0没改变 1有改变
            ts = 0;
        }

        private void SetUp_Load(object sender, EventArgs e)
        {
            try
            {

                DataShow();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 浏览设置
        /// </summary>
        private void DataShow()
        {
            DataSet ds = db.GetPIVAsDB(select.IVRecordSetUp(Uid));
            if (ds.Tables[0].Rows.Count > 0)
            {
                //显示模式
                Comb_PreviewMode.SelectedIndex = int.Parse(ds.Tables[0].Rows[0]["PreviewMode"].ToString()) == 3 ? 1 : int.Parse(ds.Tables[0].Rows[0]["PreviewMode"].ToString());
                //显示排序条件
                Comb_LabelOrderBy.SelectedIndex = int.Parse(ds.Tables[0].Rows[0]["LabelOrderBy"].ToString());
                //有数据病区
                if (int.Parse(ds.Tables[0].Rows[0]["WardIdle"].ToString()) == 0)
                {
                    Check_WardIdle.Checked = true;
                }
                else
                {
                    Check_WardIdle.Checked = false;
                }
                //开放病区
                if (int.Parse(ds.Tables[0].Rows[0]["WardOpen"].ToString()) == 0)
                {
                    Check_WardOpen.Checked = true;
                }
                else
                {
                    Check_WardOpen.Checked = false;
                }
                //进入批次时生成瓶签
                if (int.Parse(ds.Tables[0].Rows[0]["AutoGetOrder"].ToString()) == 0)
                {
                    Check_AutoGetOrder.Checked = false;
                }
                else
                {
                    Check_AutoGetOrder.Checked = true;
                }
                //停留在此画面不操作
                if (ds.Tables[0].Rows[0]["TimeCount"] != null && ds.Tables[0].Rows[0]["TimeCount"].ToString().Trim().Length != 0)
                {
                    Com_TimeCount.Text = ds.Tables[0].Rows[0]["TimeCount"].ToString().Trim();
                }
                else 
                {
                    Com_TimeCount.SelectedIndex = 0;
                }

                //不计算空包
                if (int.Parse(ds.Tables[0].Rows[0]["IsPack"].ToString()) == 0)
                {
                    Check_Nothing.Checked = true;
                }
                else
                {
                    Check_Nothing.Checked = false;
                }
                //时间点，在这个时间点后显示第二天数据
                if (ds.Tables[0].Rows[0]["NextDay"] != null && ds.Tables[0].Rows[0]["NextDay"].ToString().Trim().Length != 0)
                {
                    string[] time = ds.Tables[0].Rows[0]["NextDay"].ToString().Split(',');
                    Cob_Hour.Text = time[0];
                    Cob_Month.Text = time[1];
                }
                else
                {
                    Cob_Hour.SelectedText = "18";
                    Cob_Month.SelectedText = "00";
                }
                if (ds.Tables[0].Columns.Contains("Refresh"))
                {
                    if (ds.Tables[0].Rows[0]["Refresh"].ToString() == "1")
                    {
                        check_refresh.Checked = true;
                    }
                }
                else
                {
                    MessageBox.Show("请在数据库里增加Refresh字段");
                }
            }
            else
            {
                db.SetPIVAsDB(inert.OrderFormSet(Uid));
                Comb_PreviewMode.SelectedItem = 0;
                Check_WardIdle.Checked = true;
                Check_WardOpen.Checked = true;
            }
            string dossage=db.IniReadValuePivas("PivasBatch", "AllDrugDossage");
            if (string.IsNullOrEmpty(dossage) || dossage == "0")
            {
                checkBox1.Checked = false;
            }
            else
            {
                checkBox1.Checked = true;
            }
            
            ds.Dispose();
        }

        #region

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            // Pen pen = new Pen(Color.Gray, 1);//颜色和粗细
            Pen pen = new Pen(System.Drawing.ColorTranslator.FromHtml("#4B6076"), 1);
            DrawRoundRect(e.Graphics, pen, 0, 0, panel2.Width - 1, panel2.Height - 1, 1);
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

        /// <summary>
        /// Tab转换时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                
                DataShow();
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                ColorShow();
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                ChangeColorShow();
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                PatientColorShow();
            }
        }

        /// <summary>
        /// 病区列表模式设置
        /// </summary>
        private void PatientColorShow()
        {

            DataSet ds = db.GetPIVAsDB(select.IVRecordSetUp(Uid));
            //病人处方有改动
            if (ds.Tables[0].Rows[0]["ViewColor1"] != null && ds.Tables[0].Rows[0]["ViewColor1"].ToString().Trim().Length !=0 )
            {
                string bcolor = ds.Tables[0].Rows[0]["ViewColor1"].ToString();
                Label_view1.BackColor = System.Drawing.ColorTranslator.FromHtml(bcolor);
            }
            //病人处方无改动
            if (ds.Tables[0].Rows[0]["ViewColor2"] != null && ds.Tables[0].Rows[0]["ViewColor2"].ToString().Trim().Length != 0)
            {
                string bcolor = ds.Tables[0].Rows[0]["ViewColor2"].ToString();
                Label_view2.BackColor = System.Drawing.ColorTranslator.FromHtml(bcolor);
            }
            //药品1
            if (ds.Tables[0].Rows[0]["DrugColor1"] != null && ds.Tables[0].Rows[0]["DrugColor1"].ToString().Trim().Length != 0)
            {
                string bcolor = ds.Tables[0].Rows[0]["DrugColor1"].ToString();
                Label_drug1.BackColor = System.Drawing.ColorTranslator.FromHtml(bcolor);
            }
            //药品2
            if (ds.Tables[0].Rows[0]["DrugColor2"] != null && ds.Tables[0].Rows[0]["DrugColor2"].ToString().Trim().Length !=0)
            {
                string bcolor = ds.Tables[0].Rows[0]["DrugColor2"].ToString();
                Label_drug2.BackColor = System.Drawing.ColorTranslator.FromHtml(bcolor);
            } 
            //处方选中色
            if (ds.Tables[0].Rows[0]["SelectionColor1"] != null && ds.Tables[0].Rows[0]["SelectionColor1"].ToString().Trim().Length != 0)
            {
                string bcolor = ds.Tables[0].Rows[0]["SelectionColor1"].ToString();
                Label_SelectionColor1.BackColor = System.Drawing.ColorTranslator.FromHtml(bcolor);
            }
            //药品选中色
            if (ds.Tables[0].Rows[0]["SelectionColor2"] != null && ds.Tables[0].Rows[0]["SelectionColor2"].ToString().Trim().Length!=0)
            {
                string bcolor = ds.Tables[0].Rows[0]["SelectionColor2"].ToString();
                Label_SelectionColor2.BackColor = System.Drawing.ColorTranslator.FromHtml(bcolor);
            }
        }

        /// <summary>
        /// 批次有改动病人显示颜色
        /// </summary>
        private void ChangeColorShow()
        {
            DataSet ds = db.GetPIVAsDB(select.IVRecordSetUp(Uid));
            foreach (Label la in panel_ChangeColor.Controls)
            {
                string color = la.BackColor.R + "," + la.BackColor.G + "," + la.BackColor.B;
                //加边框
                if (color == ds.Tables[0].Rows[0]["ChangeColor"].ToString())
                {
                    la.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                }
                else
                {
                    la.BorderStyle = System.Windows.Forms.BorderStyle.None;
                }
            }
            ds.Dispose();
        }

        /// <summary>
        /// 批次颜色设置初始化
        /// </summary>
        private void GetColor()
        {
            Panel_Selected.Controls.Clear();
            Panel_NotSelected.Controls.Clear();
            Panel_SelectColor.Controls.Clear();
            
            //把颜色分类
            for (int i = 0; i < Panel_Color.Controls.Count; i++)
            {
                
                Label colors = (Label)Panel_Color.Controls[i];
                //取出label的颜色
                string color = colors.BackColor.R + "," + colors.BackColor.G + "," + colors.BackColor.B;
                Label Cor = new Label();
                Cor.BackColor = colors.BackColor;
                Cor.Size = colors.Size;
                Cor.Margin = colors.Margin;
                Cor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                Cor.Click += new System.EventHandler(this.Label_Color_Click);
                Cor.Cursor = System.Windows.Forms.Cursors.Hand;
                //如果颜色在批次设置表里有的话。就加载在已选择，否则加载到未选择里
                if (ds.Tables[0].Select(" OrderColor='" + color + "'").Length > 0)
                {
                    DataRow[] dr = ds.Tables[0].Select(" OrderColor='" + color + "'");
                    if (dr.Length > 0)
                    {
                        // Label Cor = new Label();
                        Cor.Text = dr[0]["OrderID"].ToString();
                        Cor.Name = "N" + dr[0]["OrderID"].ToString();
                        Cor.ForeColor = System.Drawing.ColorTranslator.FromHtml(dr[0]["OrderTColor"].ToString());
                      
                        Panel_SelectColor.Controls.Add(Cor);
                    }
                    else
                    {
                        Cor.Text = "";
                        Panel_NotSelected.Controls.Add(Cor);
                    }
                }
                else
                {
                    Cor.Text = "";
                    Panel_NotSelected.Controls.Add(Cor);
                }
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                foreach (Label label in Panel_SelectColor.Controls)
                {
                    if (label.Text == ds.Tables[0].Rows[i]["OrderID"].ToString())
                    {
                        Panel_Selected.Controls.Add(label);
                    }
                }
            }

            //加载选择和未选择的panel位置
            if (Panel_NotSelected.Controls.Count > 0)
            {
                Panel_NotSelected.Height = Panel_NotSelected.Controls[Panel_NotSelected.Controls.Count - 1].Location.Y + Panel_NotSelected.Controls[Panel_NotSelected.Controls.Count - 1].Height + 10;
                Panel_NotSelected.Visible = true;
                label10.Visible = true;
                label3.Visible = true;
                label50.Location = new Point(label50.Location.X, Panel_NotSelected.Location.Y + Panel_NotSelected.Height);
                label49.Location = new Point(label49.Location.X, label50.Location.Y + label50.Height + 3);
                Panel_Selected.Location = new Point(Panel_Selected.Location.X, label49.Location.Y + label49.Height);
            }
            else
            {
                label10.Visible = false;
                label3.Visible = false;
                Panel_NotSelected.Visible = false;
            }

            if (Panel_Selected.Controls.Count > 0)
            {
                Panel_Selected.Height = Panel_Selected.Controls[Panel_Selected.Controls.Count - 1].Location.Y + Panel_Selected.Controls[Panel_Selected.Controls.Count - 1].Height;
                Panel_Selected.Visible = true;
                label49.Visible = true;
                label50.Visible = true;

            }
            else
            {
                Panel_Selected.Visible = false;
                label49.Visible = false;
                label50.Visible = false;
            }

        }

        /// <summary>
        /// 批次颜色选择
        /// </summary>
        private void ColorShow()
        {
            ds = db.GetPIVAsDB(select.IVRecordColor());
            GetColor();
            //加载批次
            if (ds.Tables[0].Rows.Count > 0)
            {
                Comb_OrderID.DataSource = ds.Tables[0];
                Comb_OrderID.DisplayMember = "OrderID";
                Comb_OrderID.SelectedIndex = Comb_Select;
            }
            
        }

        ///// <summary>
        ///// 保存按钮
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void Btn_Yes_Click(object sender, EventArgs e)
        //{
        //    pvb.operate = true;
        //    pvb.PreviewMode = Comb_PreviewMode.SelectedIndex;
        //    pvb.WardIdle = Check_WardIdle.Checked == true ? 0 : 1;
        //    pvb.WardOpen = Check_WardOpen.Checked == true ? 0 : 1;
        //    this.Close();
        //}

        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            pvb.operate = true;
            pvb.PreviewMode = Comb_PreviewMode.SelectedIndex.ToString();
            pvb.WardIdle = Check_WardIdle.Checked == true ? 0 : 1;
            pvb.WardOpen = Check_WardOpen.Checked == true ? 0 : 1;
            if (ts != 0)
            {
                this.DialogResult = DialogResult.OK;
            }
            this.Close();
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            pvb.operate = true;
            //浏览模式
            int Previewmode = Comb_PreviewMode.SelectedIndex == 1 ? 3 : Comb_PreviewMode.SelectedIndex;
            //只显示有数据病区
            int WardIdle = Check_WardIdle.Checked == true ? 0 : 1;
            //只显示开放病区
            int WardOpen = Check_WardOpen.Checked == true ? 0 : 1;
            //进入画面是否自动生成瓶签
            int AutogetIVRecord = Check_AutoGetOrder.Checked == true ? 1 : 0;
            //排序
            int LabelOrderBy = Comb_LabelOrderBy.SelectedIndex;
            //时间点，在这个时间点后显示第二天数据
            string NextDay = Cob_Hour.Text + "," + Cob_Month.Text;
            //停留在此画面不操作
            int TimeCount = int.Parse(Com_TimeCount.Text);
            //不计算空包
            int IsPack = Check_Nothing.Checked == true ? 0: 1;
            //1：刷新，0不刷新
            int IsRefresh = check_refresh.Checked == true ? 1 : 0;

            int i = db.SetPIVAsDB(update.OrderFormSet(Uid, Previewmode, WardIdle, WardOpen, AutogetIVRecord, LabelOrderBy, NextDay,TimeCount,IsPack,Check_Allchange.Checked,IsRefresh));

            if (i > 0)
            {
                MessageBox.Show("保存成功");
                ds = db.GetPIVAsDB(select.IVRecordColor());
            }
        }

        /// <summary>
        /// 保存单个批次的颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_SaveBatch_Click(object sender, EventArgs e)
        {

            pvb.operate = true;
            Comb_Select = Comb_OrderID.SelectedIndex;
            BColor = Label_Batch2.BackColor.R + "," + Label_Batch2.BackColor.G + "," + Label_Batch2.BackColor.B;
            TColor = Label_Batch2.ForeColor.R + "," + Label_Batch2.ForeColor.G + "," + Label_Batch2.ForeColor.B;
            int i = db.SetPIVAsDB(update.OrderColor(Int16.Parse(Comb_OrderID.Text), BColor, TColor));

            if (i <= 0)
            {
                db.SetPIVAsDB(inert.OrderColor(Int16.Parse(Comb_OrderID.Text), BColor, TColor));
            }
            ColorShow();
            //编辑前批次颜色
            Label_Batch.BackColor = System.Drawing.ColorTranslator.FromHtml(BColor);
            //编辑后批次颜色
            Label_Batch2.BackColor = System.Drawing.ColorTranslator.FromHtml(BColor);
            //编辑前液体总量颜色
            Panel_Total.BackColor = System.Drawing.ColorTranslator.FromHtml(BColor);
            //编辑后液体总量颜色
            Panel_Total2.BackColor = System.Drawing.ColorTranslator.FromHtml(BColor);
            //编辑前批次字体颜色
            Label_Batch.ForeColor = System.Drawing.ColorTranslator.FromHtml(TColor);
            //编辑前'液体总量'的颜色
            Label_SY.ForeColor = System.Drawing.ColorTranslator.FromHtml(TColor);
            //编辑前液体总量的颜色
            Label_Total.ForeColor = System.Drawing.ColorTranslator.FromHtml(TColor);
            //编辑后批次字体颜色
            Label_Batch2.ForeColor = System.Drawing.ColorTranslator.FromHtml(TColor);
            //编辑后'液体总量'的颜色
            Label_SY2.ForeColor = System.Drawing.ColorTranslator.FromHtml(TColor);
            //编辑后液体总量的颜色
            Label_Total2.ForeColor = System.Drawing.ColorTranslator.FromHtml(TColor);
            ts = 1;
        }

        /// <summary>
        /// 清除某个批次的颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            pvb.operate = true;
            db.SetPIVAsDB(update.OrderColor(Int16.Parse(Comb_OrderID.Text), "255,255,255", "0,0,0"));
            Comb_Select = Comb_OrderID.SelectedIndex;
            ColorShow();
            BColor = Label_Batch2.BackColor.R + "," + Label_Batch2.BackColor.G + "," + Label_Batch2.BackColor.B;
            TColor = Label_Batch2.ForeColor.R + "," + Label_Batch2.ForeColor.G + "," + Label_Batch2.ForeColor.B;
        }

        /// <summary>
        /// 点击色块，赋值到预览区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_Color_Click(object sender, EventArgs e)
        {
            pvb.operate = true;
            Label Label_UColor = (Label)sender;
            //如果选中的颜色不在未选择里。
            //去掉已选择里的选中边框
            if (Label_UColor.Parent != Panel_NotSelected)
            {
                foreach (Label la in Panel_Selected.Controls)
                {
                    la.BorderStyle = System.Windows.Forms.BorderStyle.None;
                }
            }
            //去掉未选择里的选中边框
            foreach (Label la in Panel_NotSelected.Controls)
            {
                la.BorderStyle = System.Windows.Forms.BorderStyle.None;
            }

            //把选中的label加边框
            Label_UColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            //如果选择的颜色在未选择里
            //把颜色复制到预览区域
            if (Label_UColor.Parent == Panel_NotSelected)
            {
                BColor = Label_UColor.BackColor.R + "," + Label_UColor.BackColor.G + "," + Label_UColor.BackColor.B;
                Label_Batch2.BackColor = System.Drawing.ColorTranslator.FromHtml(BColor);
                Panel_Total2.BackColor = System.Drawing.ColorTranslator.FromHtml(BColor);
            }
            //否则根据颜色中的批次选中到批次
            else
            {
                Comb_OrderID.SelectedIndex = int.Parse(Label_UColor.Text) - 1;
            }

        }

        /// <summary>
        /// 批次选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Comb_OrderID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pvb.operate = true;
                if (Comb_OrderID.SelectedIndex != null)
                {

                    if (ds.Tables[0].Rows[Comb_OrderID.SelectedIndex]["OrderColor"] != null && ds.Tables[0].Rows[Comb_OrderID.SelectedIndex]["OrderColor"].ToString().Trim().Length != 0)
                    {
                        string bcolor = ds.Tables[0].Rows[Comb_OrderID.SelectedIndex]["OrderColor"].ToString();
                        Label_Batch.BackColor = System.Drawing.ColorTranslator.FromHtml(bcolor);
                        Label_Batch2.BackColor = System.Drawing.ColorTranslator.FromHtml(bcolor);

                        Label_Batch.Text = Comb_OrderID.Text + "#";
                        Label_Batch2.Text = Comb_OrderID.Text + "#";

                        Panel_Total.BackColor = System.Drawing.ColorTranslator.FromHtml(bcolor);
                        Panel_Total2.BackColor = System.Drawing.ColorTranslator.FromHtml(bcolor);
                        foreach (Label la in Panel_Selected.Controls)
                        {
                            string cc = la.BackColor.R + "," + la.BackColor.G + "," + la.BackColor.B;
                            if (cc == ds.Tables[0].Rows[Comb_OrderID.SelectedIndex]["OrderColor"].ToString())
                            {
                                la.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                            }
                        }
                    }
                    else
                    {
                        Label_Batch.BackColor = Color.White;
                        Label_Batch2.BackColor = Color.White;
                        Panel_Total.BackColor = Color.White;
                        Panel_Total2.BackColor = Color.White;
                    }
                    if (ds.Tables[0].Rows[Comb_OrderID.SelectedIndex]["OrderColor"] != null && ds.Tables[0].Rows[Comb_OrderID.SelectedIndex]["OrderTColor"].ToString().Trim().Length !=0)
                    {
                        string color = ds.Tables[0].Rows[Comb_OrderID.SelectedIndex]["OrderTColor"].ToString();
                        Label_Batch.ForeColor = System.Drawing.ColorTranslator.FromHtml(color);
                        Label_SY.ForeColor = System.Drawing.ColorTranslator.FromHtml(color);
                        Label_Total.ForeColor = System.Drawing.ColorTranslator.FromHtml(color);
                        Label_Batch2.ForeColor = System.Drawing.ColorTranslator.FromHtml(color);
                        Label_SY2.ForeColor = System.Drawing.ColorTranslator.FromHtml(color);
                        Label_Total2.ForeColor = System.Drawing.ColorTranslator.FromHtml(color);
                    }
                    else
                    {
                        Label_Batch.ForeColor = System.Drawing.Color.Black;
                        Label_SY.ForeColor = System.Drawing.Color.Black;
                        Label_Total.ForeColor = System.Drawing.Color.Black;
                        Label_Batch2.ForeColor = System.Drawing.Color.Black;
                        Label_SY2.ForeColor = System.Drawing.Color.Black;
                        Label_Total2.ForeColor = System.Drawing.Color.Black;
                    }
                }
                else
                {
                    Label_Batch.BackColor = Color.White;
                    Label_Batch2.BackColor = Color.White;
                    Panel_Total.BackColor = Color.White;
                    Panel_Total2.BackColor = Color.White;
                    Label_Batch.ForeColor = System.Drawing.Color.Black;
                    Label_SY.ForeColor = System.Drawing.Color.Black;
                    Label_Total.ForeColor = System.Drawing.Color.Black;
                    Label_Batch2.ForeColor = System.Drawing.Color.Black;
                    Label_SY2.ForeColor = System.Drawing.Color.Black;
                    Label_Total2.ForeColor = System.Drawing.Color.Black;

                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 给字体赋颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_Color2_Click(object sender, EventArgs e)
        {

            pvb.operate = true;
            Label Label_UColor = (Label)sender;
            TColor = Label_UColor.BackColor.R + "," + Label_UColor.BackColor.G + "," + Label_UColor.BackColor.B;
            Label_SY2.ForeColor = System.Drawing.ColorTranslator.FromHtml(TColor);
            Label_Batch2.ForeColor = System.Drawing.ColorTranslator.FromHtml(TColor);
            Label_Total2.ForeColor = System.Drawing.ColorTranslator.FromHtml(TColor);
        }

        private void label35_MouseDown(object sender, MouseEventArgs e)
        {

            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        /// <summary>
        /// 批次有改动病人显示颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label73_MouseClick(object sender, MouseEventArgs e)
        {
            pvb.operate = true;
            Label Label_UColor = (Label)sender;
            foreach (Label la in panel_ChangeColor.Controls)
            {
                la.BorderStyle = System.Windows.Forms.BorderStyle.None;
            }

            Label_UColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panel_ChangeColor.Tag = Label_UColor.BackColor.R + "," + Label_UColor.BackColor.G + "," + Label_UColor.BackColor.B;
        }

        /// <summary>
        /// 保存有改动病人颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_SaveChangeColor_Click(object sender, EventArgs e)
        {

            pvb.operate = true;
            if (panel_ChangeColor.Tag != null && panel_ChangeColor.Tag.ToString().Trim() != "255,255,255")
            {
                int d = db.SetPIVAsDB(update.OrderFormSet(Uid, panel_ChangeColor.Tag.ToString(),Check_Allchange.Checked));
                ts = 1;
                if (d > 0)
                {
                    MessageBox.Show("保存成功");
                    this.DialogResult = DialogResult.OK;

                }
            }
        }

        /// <summary>
        ///病人有无改动颜色，点击选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_IsSame_Click(object sender, EventArgs e)
        {

            pvb.operate = true;
            Label Label_view = (Label)sender;
            if (Label_view.Tag.ToString() == "1")
            {
                db.SetPIVAsDB(update.OrderColor(Uid, "ViewColor1", Panel_Changeshow.Tag.ToString(),Check_Allchange.Checked));
                Label_view1.BackColor = System.Drawing.ColorTranslator.FromHtml(Panel_Changeshow.Tag.ToString());
                ts = 1;
            }
            else if (Label_view.Tag.ToString() == "2")
            {
                db.SetPIVAsDB(update.OrderColor(Uid, "ViewColor2", Panel_Changeshow.Tag.ToString(),Check_Allchange.Checked));
                Label_view2.BackColor = System.Drawing.ColorTranslator.FromHtml(Panel_Changeshow.Tag.ToString());
                ts = 1;
            }
            else
            {
                db.SetPIVAsDB(update.OrderColor(Uid, "SelectionColor1", Panel_Changeshow.Tag.ToString(),Check_Allchange.Checked));
                Label_SelectionColor1.BackColor = System.Drawing.ColorTranslator.FromHtml(Panel_Changeshow.Tag.ToString());
                ts = 1;
            }
            Panel_Changeshow.Visible = false;
        }

        /// <summary>
        /// 有无改动病人颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void view_Click(object sender, EventArgs e)
        {

            pvb.operate = true;
            Label Label_view = (Label)sender;
            string ViewColor = Label_view.BackColor.R + "," + Label_view.BackColor.G + "," + Label_view.BackColor.B;

            if ((Panel_IsSamed.Location.Y + Panel_IsSamed.Height) - (Label_view.Location.Y + Label_view.Height) >= Panel_DrugShow.Height+10)
            {
                Panel_Changeshow.Location = new Point(Label_view.Location.X, Label_view.Location.Y+10);
            }
            else
            {
                Panel_Changeshow.Location = new Point(Label_view.Location.X, Label_view.Location.Y - Panel_DrugShow.Height+10);
            }
            Panel_Changeshow.Tag = ViewColor;
            //Panel_Changeshow.Parent = this;
            Panel_Changeshow.Visible = true;
            Panel_DrugShow.Visible = false;
            Panel_Changeshow.BringToFront();
        }

        /// <summary>
        /// 药品信息颜色，点击选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label91_Click(object sender, EventArgs e)
        {

            pvb.operate = true;
            Label Label_view = (Label)sender;
            if (Label_view.Tag.ToString() == "1")
            {
                db.SetPIVAsDB(update.OrderColor(Uid, "DrugColor1", Panel_DrugShow.Tag.ToString(),Check_Allchange.Checked));
                Label_drug1.BackColor = System.Drawing.ColorTranslator.FromHtml(Panel_DrugShow.Tag.ToString());
                ts = 1;
            }
            else if (Label_view.Tag.ToString() == "2")
            {
                db.SetPIVAsDB(update.OrderColor(Uid, "DrugColor2", Panel_DrugShow.Tag.ToString(),Check_Allchange.Checked));
                Label_drug2.BackColor = System.Drawing.ColorTranslator.FromHtml(Panel_DrugShow.Tag.ToString());
                ts = 1;
            }
            else
            {
                db.SetPIVAsDB(update.OrderColor(Uid, "SelectionColor2", Panel_DrugShow.Tag.ToString(),Check_Allchange.Checked));
                Label_SelectionColor2.BackColor = System.Drawing.ColorTranslator.FromHtml(Panel_DrugShow.Tag.ToString());
                ts = 1;
            }
            Panel_DrugShow.Visible = false;
        }

        /// <summary>
        /// 药品信息颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Drug_Click(object sender, EventArgs e)
        {

            pvb.operate = true;
            Label Label_Drug= (Label)sender;
            string ViewColor = Label_Drug.BackColor.R + "," + Label_Drug.BackColor.G + "," + Label_Drug.BackColor.B;
            if ((Panel_Drug.Location.Y + Panel_Drug.Height) - (Label_Drug.Location.Y + Label_Drug.Height) >= Panel_DrugShow.Height+10)
            {
                Panel_DrugShow.Location = new Point(Label_Drug.Location.X, Label_Drug.Location.Y+10);
            }
            else
            {
                Panel_DrugShow.Location = new Point(Label_Drug.Location.X, Label_Drug.Location.Y-Panel_DrugShow.Height+10);
            }
            Panel_DrugShow.Tag = ViewColor;
            Panel_DrugShow.Visible = true;
            Panel_Changeshow.Visible = false;
          //  Panel_DrugShow.Parent = this;
            Panel_DrugShow.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                pvb.operate = true;
                frmProcedure f = new frmProcedure();
                f.ShowDialog();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                db.IniWriteValuePivas("PivasBatch", "AllDrugDossage", "1");
            }
            else
            {
                db.IniWriteValuePivas("PivasBatch", "AllDrugDossage", "0");
            }
           
        }

     
    }
}
