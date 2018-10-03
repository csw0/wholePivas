using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PivasLabelSelect;
using PIVAsCommon.Helper;

namespace ScanPre
{
    public partial class labelNo : Form
    {
        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion
        public string LabelNo = string.Empty;
        int rc = 0;
        DB_Help DB = new DB_Help();
        mySQL sql = new mySQL();
        public labelNo( string groupNo)
        {
            InitializeComponent();
            label13.Text = groupNo;       
        }

        public void setLabelNo()
        {

            StringBuilder str = new StringBuilder();
            str.Append("  SELECT distinct [Batch] as '批次' ,[FreqName] as '频次' ,InfusionDT as '用药时间',");
            str.Append(" [PrintDT] as '打印时间',[PrinterName] as '打印人' ,[PatCode] as '病人编号' ,[PatName] as '姓名'  ");
            str.Append(",[BedNo]as '床号',iv.WardName as '病区' ,a.DrugName as '主药' ,b.DrugName as '溶媒' ");
            str.Append("  ,case iv.IVStatus when 0 then '瓶签产生' when 1 then '批次产生' when 3 then '打印' when 5 then '排药核对' ");
            str.Append(" when 7 then '进仓核对' when 9 then '配置核对' when 11 then '出仓核对' when 13 then '打包核对' when 15 then '病区签收' end as '状态'");
            str.Append("  FROM [IVRecord] iv  left join  DDrug a on iv.MarjorDrug=a.DrugCode  ");
            str.Append("left join  DDrug b  on iv.Menstruum=b.DrugCode  ");
            str.Append(" left join DWard on iv.WardCode=DWard.WardCode   ");
            str.Append(" left join IVRecordDetail ivd on ivd.IVRecordID=iv.IVRecordID ");
            str.Append(" where iv.LabelNo= '");
            str.Append(dgv1.CurrentRow.Cells[0].Value.ToString());
            str.Append("' ");
            DataTable dt = DB.GetPIVAsDB(str.ToString()).Tables[0];
            tbName.Text = dt.Rows[0][6].ToString();
            textBox1.Text = dt.Rows[0][4].ToString();
            textBox2.Text = dt.Rows[0][7].ToString();
            textBox3.Text = dt.Rows[0][8].ToString();
            textBox4.Text = dt.Rows[0][0].ToString();
            textBox5.Text = dt.Rows[0][1].ToString();
            textBox6.Text = dt.Rows[0][9].ToString();
            textBox7.Text = dt.Rows[0][10].ToString();
            textBox8.Text = dt.Rows[0][2].ToString();
            textBox9.Text = dt.Rows[0][3].ToString();
            textBox10.Text = dt.Rows[0][11].ToString();
        }
        public void setDrug()
        {
            dgv2.Rows.Clear();
            StringBuilder str = new StringBuilder();
            str.Append(" select ivd.DrugName,ivd.Spec,ivd.Dosage,ivd.DosageUnit from IVRecordDetail ivd  ");
            str.Append("join IVRecord on ivd.IVRecordID=IVRecord.IVRecordID where LabelNo='");
            str.Append(dgv1.CurrentRow.Cells[0].Value.ToString());
            str.Append("'");
            DataTable dt = DB.GetPIVAsDB(str.ToString()).Tables[0];
          
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                float f = float.Parse(dt.Rows[i][2].ToString());
                dgv2.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), f, dt.Rows[i][3].ToString());
            }
                
                
            
        }
        public void getLabelNo(string groupNo)
        {
            StringBuilder str = new StringBuilder();
            str.Append("   select LabelNo from IVRecord iv  inner join Prescription p on iv.GroupNo=p.GroupNo where p.GroupNo='");
            str.Append(groupNo);
            str.Append("'");
            str.Append("   select count (LabelNo) from IVRecord iv  inner join Prescription p on iv.GroupNo=p.GroupNo where p.GroupNo='");
            str.Append(groupNo);
            str.Append("'");
            DataTable dt = DB.GetPIVAsDB(str.ToString()).Tables[0];
            DataTable dt1 = DB.GetPIVAsDB(str.ToString()).Tables[1];
            dgv1.DataSource = dt;
            if (dgv1.Rows.Count > 0)
            {
                setLabelNo();
                setDrug();
                ShowLabelCheck(dgv1.CurrentRow.Cells[0].Value.ToString());
            }
            label14.Text = dt1.Rows[0][0].ToString();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv1_Click(object sender, EventArgs e)
        {
            if (dgv1.Rows.Count > 0)
            {
                panel3.Controls.Clear();
                rc = 0;
                setLabelNo();
                setDrug();
                ShowLabelCheck(dgv1.CurrentRow.Cells[0].Value.ToString());
            }
        }
        private void Panel_Close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Panel_Close_MouseHover(object sender, EventArgs e)
        {
            Panel_Close.BackgroundImage = global::ScanPre.Properties.Resources._15;
        }

        private void Panel_Close_MouseLeave(object sender, EventArgs e)
        {
            Panel_Close.BackgroundImage = null;
            Panel_Close.BackColor = Color.Transparent;
            Panel_Close.BackgroundImage = global::ScanPre.Properties.Resources._21;
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void label13_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void labelNo_Load(object sender, EventArgs e)
        {
            getLabelNo(LabelNo);
        }

        private void ShowLabelCheck(string Label)
        {
            DataSet ds = DB.GetPIVAsDB(sql.msg(Label));
            for (int J = 0; J < ds.Tables[1].Rows.Count; J++)
            {
                CancelMessage msg = new CancelMessage();
                msg.setmsg("核对", ds.Tables[1].Rows[J]);
                msg.Top = rc * 20;
               panel3.Controls.Add(msg);
                rc++;
            }
            if (int.Parse(ds.Tables[0].Rows[0]["LabelOver"].ToString()) < 0)
            {
                CancelMessage msg = new CancelMessage();
                msg.setmsg("配置取消", ds.Tables[0].Rows[0]);
                msg.Top = rc * 20;
               panel3.Controls.Add(msg);
                rc++;
            }
            if (int.Parse(ds.Tables[0].Rows[0]["WardRetreat"].ToString()) > 0)
            {
                CancelMessage msg = new CancelMessage();
                msg.setmsg("退药", ds.Tables[0].Rows[0]);
                msg.Top = rc * 20;
               panel3.Controls.Add(msg);
                rc++;
            }

        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
    
    }
}
