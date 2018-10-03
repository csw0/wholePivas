using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using PivasBatchCommon;
using PIVAsCommon.Helper;

namespace PivasBatchMX
{
    public delegate void DelegateChangeTextVal(string TextVal,string Team,string batchrule);    
    public partial class Pivas_setBatch : Form
    {
        public Pivas_setBatch()
        {
            InitializeComponent();
        }

          // 2.定义委托事件  
        public event DelegateChangeTextVal ChangeTextVal;  
        //旧批次
        public string BatchTags = string.Empty;
        public DB_Help DB = new DB_Help();
        public UpdateSql update = new UpdateSql();
        public SelectSql select = new SelectSql();
        public InsertSql insert = new InsertSql();
        BatchInfo bi;
        string LabelNo;
        //新批次里的字符
        string K_U = "";
        //旧批次的数字
        public string Team = "";
        string Pcode;
        //新批次
        string Batch;
        //bool reasonclick = false;

        private string reason = string.Empty;
        private string reasonDetail = string.Empty;

        private bool refresh; //修改批次后是否刷新界面

        /// <summary>
        /// 改批次原因LIST
        /// </summary>
        List<string> Data = new List<string>();
 

        public void Clear()
        {
       
             bi=null;
             LabelNo=null;
             K_U = null;
             Team = null;
             Pcode=null;
             Batch=null;
             BatchTags = null;
        }

        public void ShowSetBatch(bool tf, string LabelNo, string Teamter, string BatchRules, BatchInfo bi, string pcode, string batchstr,bool refresh)
        {
            try
            {
                this.refresh = refresh;
                //this.pivas = pivas;
                Off = false;
                //鼠标相对于屏幕的坐标
                Rectangle s = Screen.PrimaryScreen.WorkingArea;
                if (Control.MousePosition.Y + this.Height < s.Height)
                {
                    this.Location = new Point(Control.MousePosition.X, Control.MousePosition.Y);
                }
                else
                {
                    this.Location = new Point(Control.MousePosition.X, Control.MousePosition.Y - this.Height);
                }
                Panel_Batch.Tag = LabelNo;
                Fpanel_set.Controls.Clear();
                Team = Teamter;
                if (batchstr.Contains("L"))
                {
                    Label_L.BackColor = Color.Gold;
                    //Label_L.BackgroundImage = (Image)PivasBatchMX.Properties.Resources.ResourceManager.GetObject("K_L");
                    Label_L.Visible = true;             
                }
                else
                {
                    Label_L.Visible = false;

                  
                }
                if (batchstr.Contains("K"))
                {
                    Label_K.BackgroundImage = (Image)PivasBatchMX.Properties.Resources.ResourceManager.GetObject("K_L");
                }
                if (batchstr.Contains("#"))
                {
                    Label_.BackgroundImage = (Image)PivasBatchMX.Properties.Resources.ResourceManager.GetObject("K_L");
                }
                Pcode = pcode;            
                BatchTags = batchstr;
                this.bi = bi;
                this.LabelNo = LabelNo;
                DataSet ds = DB.GetPIVAsDB(select.DOrder());
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows.Count % 6) == 0)
                    {
                        Fpanel_set.Height = (ds.Tables[0].Rows.Count / 6) * 33;
                    }
                    else
                    {
                        Fpanel_set.Height = (ds.Tables[0].Rows.Count / 6 + 1) * 33;
                    }
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        SetBatch Set = new SetBatch(bi, pcode,refresh);
                        Set.batchInfo(ds.Tables[0].Rows[i]["OrderID"].ToString(), LabelNo, batchstr, Team, false);
                        Set.ChangeTextVal += new DelegateChangeText(NewMethod);
                        Set.get_Reason += new SetBatch.getReason(getReason);
                        Set.get_ReasonDetail += new SetBatch.GetReasonDetail(GetReasonDetail);
                        Fpanel_set.Controls.Add(Set);
                      
                    }
                    Label_BatchRule.Text = BatchRules;
                 }
                        

               
                X = this.Location.X;
                Y = this.Location.Y;
                Off = true;
                
            }
            catch (Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatchMX" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10010:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
              
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            pvb.operate = true;
            BatchTags = string.Empty;
        }

        private void Label_L_MouseMove(object sender, MouseEventArgs e)
        {
            Label Label_UKL = (Label)sender;
            if (!Label_UKL.Text.Equals("L"))
            {
                Label_UKL.BackgroundImage = (Image)PivasBatchMX.Properties.Resources.ResourceManager.GetObject("K_L");
            }
            else
            {
                Label_UKL.BackColor = Color.Gold;
            }
        }

        private void Label_L_MouseLeave(object sender, EventArgs e)
        {
            Label Label_UKL = (Label)sender;
            if (!K_U.Contains(Label_UKL.Text.Trim()) && !Label_UKL.Text.Equals("L"))
            {
                Label_UKL.BackgroundImage = null;
            }
            else
            {
                Label_UKL.BackColor = Color.PaleGoldenrod;
            }
        }

        /// <summary>
        /// 选择K L
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_L_Click(object sender, EventArgs e)
        {
           
            Label Label_UKL = (Label)sender;
            K_U = Label_UKL.Text;
            string empName = pvb.DEmployeeName;
            string emp = pvb.DEmployeeID;         
            string IVStatus = "";
            string batchrule = "";

            if (K_U != "L")
            {
                Batch =Team+(K_U.Equals("K")?"-K":"#");
            }
            else
            {
                Batch = BatchTags.IndexOf("L-") >= 0 ? BatchTags.Replace("L-", "") : "L-" + BatchTags;             
            }

            if (textBox1.Visible == true && (textBox1.Text == "" || textBox1.Text == "修改原因"))
            {
                MessageBox.Show("请填写修改原因");
                return;
            }

            if (comboBox1.Visible == true && comboBox1.SelectedItem.ToString() == "请选择修改原因")
            {
                reason = "未选择修改原因";
            }
            else if (comboBox1.Visible == true && comboBox1.SelectedItem.ToString()!= "请选择修改原因")
            {
                reason = comboBox1.SelectedItem.ToString();
            }
            else
            {
                reason = textBox1.Text;
            }
            if (textBox2.Text != "原因说明" && textBox2.Text.ToString() != "")
            {
                reasonDetail = textBox2.Text;
            }
            else
            {
                reasonDetail = null;
            }

            try
            {
                int i = DB.SetPIVAsDB(update.IVRecordBatch(LabelNo, BatchTags, this.Batch, int.Parse(Team), empName, ref batchrule));
                if (i > 0)
                {
                    DB.SetPIVAsDB(insert.OrderChangelog(LabelNo, emp, empName, Batch, this.Batch, IVStatus, reason, reasonDetail));
                    //this.Dispose();
                    NewMethod(this.Batch, Team, batchrule);
                }
                pvb.operate = true;
                this.Close();
            }
            catch (Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatchMX" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10009:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }


        /// <summary>
        /// 修改批次后刷新界面
        /// </summary>
        /// <param name="batchtr">批次</param>
        /// <param name="Team">teamnumber</param>
        /// <param name="batchrule">批次规则</param>
        private void NewMethod(string batchtr,string Team,string batchrule)
        {
            try
            {
                if (pvb.PreviewMode != "2" && pvb.PreviewMode != "3")
                {                
                    bi.Label_Batch.Text = batchtr;
                    DataSet ds = DB.GetPIVAsDB(string.Format("select *from OrderColor where OrderID='{0}'", Team));
                    bi.Label_Batch.BackColor = System.Drawing.ColorTranslator.FromHtml(ds.Tables[0].Rows[0]["OrderColor"].ToString());
                }
                else
                {
                    ChangeTextVal(batchtr, Team, batchrule);                
                }

            }
            catch (Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatchMX" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10011:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
            
            
        }

        #region  画边框

        private void FPanel_SetBatch_Paint(object sender, PaintEventArgs e)
        {
            // Pen pen = new Pen(Color.Gray, 1);//颜色和粗细
            Pen pen = new Pen(Color.Gray, 1);
            DrawRoundRect(e.Graphics, pen, 0, 0, FPanel_SetBatch.Width - 1, FPanel_SetBatch.Height - 1, 1);
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
        /// 界面初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pivas_setBatch_Load(object sender, EventArgs e)
        {
             DataSet ds = DB.GetPIVAsDB(select.DOrder());
             if (ds != null && ds.Tables[0].Rows.Count > 0)
             {
                 if ((ds.Tables[0].Rows.Count % 6) == 0)
                 {
                     Fpanel_set.Height = (ds.Tables[0].Rows.Count / 6) * 33;
                 }
                 else
                 {
                     Fpanel_set.Height = (ds.Tables[0].Rows.Count / 6+1) * 33;
                 }
             }


             ds.Dispose();
             comboBox1.IntegralHeight = false;
             textBox1.Visible = false;
             AddReason();
 
        }

        /// <summary>
        /// 添加原因类
        /// </summary>
        private void AddReason()
        {
            string sql = " select distinct  Reason  from OrderChangeLog  where Reason not like '%打印画面修改%'";
            DataSet ds = DB.GetPIVAsDB(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string reason = ds.Tables[0].Rows[i]["Reason"].ToString();

                    comboBox1.Items.Add(reason);
                }

            }
            comboBox1.SelectedIndex = 0;
        }


        int X;
        int Y;
        bool Off;
        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Off)
            {
                //||Control.MousePosition.X > X + this.Width || Control.MousePosition.Y < Y + this.Height
                Off = false;
                if ((Control.MousePosition.X < X || Control.MousePosition.Y < Y) || (Control.MousePosition.X > X + this.Width || Control.MousePosition.Y > Y + this.Height))
                {
                    //if (cbbReason.DroppedDown)
                    //{
                    //    return;
                    //}
                    this.Dispose();
                }
                Off = true;
            }
        }


        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion
        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            Off = false;
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            X = this.Location.X;
            Y = this.Location.Y;
            Off = true;
        }

        private string getReason()
        {
           
            if (comboBox1.Visible == true)
            {
                reason = comboBox1.SelectedItem.ToString();
            }
            else
            {
                reason = textBox1.Text; 
            }
            return reason;
        }

        private string GetReasonDetail()
        {
            if (textBox2.Text != "原因说明" && textBox2.Text.ToString() != "")
            {
                reasonDetail = textBox2.Text;
            }
            else
            {
                reasonDetail = null;
            }
            return reasonDetail;
        }

        private void Label_ClickClose_MouseHover(object sender, EventArgs e)
        {
            Label_ClickClose.BackColor = Color.Red;
        }

        private void Label_ClickClose_MouseLeave(object sender, EventArgs e)
        {
            Label_ClickClose.BackColor = Color.PaleTurquoise;
        }


    
        private void label4_Click(object sender, EventArgs e)
        {
            if (comboBox1.Visible == true)
            {
                comboBox1.Visible = false;
                textBox1.Visible = true;
            }
            else
            {
                comboBox1.Visible = true;
                textBox1.Visible = false;
            }

        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "原因说明")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;

            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "原因说明";
                textBox2.ForeColor = Color.Gray;

            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "修改原因";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "修改原因")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

      

     
        
    }
    
}


