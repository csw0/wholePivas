using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using PivasBatchCommon;
using PIVAsCommon.Helper;

namespace PivasBatchMX
{
    public delegate void DelegateChangeText(string TextVal,string Tem,string batchrule);    
    public partial class SetBatch : UserControl
    {
    
        public BatchInfo bi;
        public string patcode;
       // Pivasbatch pvb;
        DB_Help db = new DB_Help();
        //瓶签号
        public string Labelno = string.Empty;
        //public string Batchtags = string.Empty;
        UpdateSql update = new UpdateSql();
        InsertSql insert = new InsertSql();
        //旧批次
        string Batch = string.Empty;
        //批次（纯数字）
        string Team;
        public event DelegateChangeText ChangeTextVal;

        public delegate string getReason();
        public delegate string GetReasonDetail();
        public event getReason get_Reason;
        public event GetReasonDetail get_ReasonDetail;
        bool refresh = true;

        public SetBatch(BatchInfo bi, string pcode,bool refresh)
        {
            this.refresh = refresh;
            this.bi = bi;
            patcode = pcode;

            InitializeComponent();
        }

        public void Clear()
        {
            bi = null;
            patcode = null;
            //pvb = null;

            Labelno = null;
            Batch = null;
            Team = null;
        }

        /// <summary>
        /// 选中数据
        /// </summary>
        /// <param name="LabelNo">瓶签号</param>
        /// <param name="BatchTags">U，K，L</param>
        /// <param name="tags">true，由病人列表模式进入</param>
        public void batchInfo(string TeamNumber, string LabelNo, string Batch, string Teamter,bool tags)
        {
            this.Batch = Batch;
            //this.pvb = pvb;
            this.Team = Teamter;
            Label_TeamNumber.Text = TeamNumber;
            if (TeamNumber == Teamter)
            {
                Panel_TeamNumber.BackgroundImage = (Image)PivasBatchMX.Properties.Resources.ResourceManager.GetObject("选中批次");
            }
            else
            {
                Panel_TeamNumber.BackgroundImage = null;
            }
            Labelno = LabelNo.Replace("瓶签号", "");
            //Batchtags = ((Pivasbatch)(this.Parent.Parent)).BatchTags;
        }

        /// <summary>
        /// 选择批次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetBatch_Click(object sender, EventArgs e)
        {
            string ReasonDetail = "";
            ReasonDetail = get_ReasonDetail()== "原因说明" ? "" : get_ReasonDetail();
            string reason = get_Reason();

            if (reason== "" || reason  == "修改原因")
            {
                MessageBox.Show("请填写修改原因");
                return;
            }

            if (reason  == "请选择修改原因")
            {
                reason = "未选择修改原因";
            }
            //string oldBatchtags = ((Pivas_setBatch)(this.Parent.Parent.Parent.Parent)).BatchTags;
            string oldBatchtags = this.Batch;
            string newBatchtags = "";
            //Team = ((Pivas_setBatch)(this.Parent.Parent.Parent.Parent)).Team;
            string L = "";
            string K = "";

            if (oldBatchtags.IndexOf("L-") < 0)
            {
                L = "";
            }
            else
            {
                L = "L-";
            }
            if (oldBatchtags.IndexOf("-K") < 0)
            {
                K = "#";
            }
            else
            {
                K = "-K";
            }

            newBatchtags = L + Label_TeamNumber.Text + K;
            //Batchtags = Batchtags.Replace(Team, Label_TeamNumber.Text);
            string empName = pvb.DEmployeeName;
            string emp = pvb.DEmployeeID;
            string IVStatus = "";
            string batchrule = "";
            try
            {
               
                int i = db.SetPIVAsDB(update.IVRecordBatch(Labelno, oldBatchtags, newBatchtags, int.Parse(Label_TeamNumber.Text), empName, ref batchrule));
                if (i > 0)
                {
                    try
                    {
                        db.SetPIVAsDB(insert.OrderChangelog(Labelno, emp, empName, Batch, newBatchtags, IVStatus, reason, ReasonDetail));
                    }
                    catch
                    {
                        MessageBox.Show("批次修改LOG插入出错，请检查！！！" + insert.OrderChangelog(Labelno, emp, empName, Batch, oldBatchtags, IVStatus, reason, ReasonDetail));
                    }


                    if (pvb.PreviewMode != "2" && pvb.PreviewMode != "3" &&refresh)
                    {
                        ((PatientInfo)(bi.Parent.Parent)).SetInfo(patcode);
                    }
                    else
                    {
                        ChangeTextVal(newBatchtags, Label_TeamNumber.Text, batchrule);
                    }
                    ((Pivas_setBatch)(this.Parent.Parent.Parent.Parent)).Close();
                }
                pvb.operate = true;
            }
            catch (Exception ex)
            {
                File.AppendAllText(Application.StartupPath + "\\Log\\PivasBatchMX" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "10014:" + ex.Message + "    " + DateTime.Now.ToString() + "\r\n");
            }
        }

        private void Label_TeamNumber_MouseMove(object sender, MouseEventArgs e)
        {
            Panel_TeamNumber.BackgroundImage = (Image)PivasBatchMX.Properties.Resources.ResourceManager.GetObject("选中批次");
        }

        private void Label_TeamNumber_MouseLeave(object sender, EventArgs e)
        {

            if (Team != Label_TeamNumber.Text)
            {
                Panel_TeamNumber.BackgroundImage = null;

            }
        }
    }
}
