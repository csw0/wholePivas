using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;


namespace PivasCountForTV
{
    public partial class PivasCountForTV : Form
    {
        public PivasCountForTV()
        {
            InitializeComponent();
        }

        private PIVAsDBhelp.DB_Help db = new PIVAsDBhelp.DB_Help();
        private seldb sel = new seldb();
        private string SelectRule = string.Empty;
        DataTable dt = new DataTable();
        private int stay=0; //停留时间
        private int stay1 = 0;
        private void PivasCountForTV_Load(object sender, EventArgs e)
        {
            this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            string interval = db.IniReadValue("PivasTV", "RefreshItval");
            if (interval == "")
            {
                db.IniWriteValue("PivasTV", "RefreshItval", "60");
                timer2.Interval = 60000;
            }
            else
            {
                timer2.Interval = Convert.ToInt32(interval) * 1000;
            }

            string interval1 = db.IniReadValue("PivasTV", "GDItval");
            if (string.IsNullOrEmpty(interval1))
            {
                db.IniWriteValue("PivasTV", "GDItval", "2");
                GDtimer.Interval = 2000;
            }
            else
            {
                GDtimer.Interval = Convert.ToInt32(interval1) * 1000;
            }


            string batch = db.IniReadValue("PivasTV", "TVBatch");
            if (string.IsNullOrEmpty(batch))
            {
                db.IniWriteValue("PivasTV", "TVBatch", "0");
                lbBatch.Text = "0";
            }
            else
            {
                lbBatch.Text = batch;
            }

            string stayInterval = db.IniReadValue("PivasTV", "TVStay");
            if(string.IsNullOrEmpty(stayInterval))
            {
                db.IniWriteValue("PivasTV", "TVStay", "10");
                StayTimer.Interval = 10000;
            }
            else
            {
                stay = Convert.ToInt32(stayInterval);
                stay1 = stay;
            }

            try
            {
                sel.insertSet();
                dt = CheckMateId("电视机配置").Tables[0];
                showData();
            }
            catch
            {
                MessageBox.Show("读取配置失败，请检查数据库连接!");
            }


        }

        private void showData()
        {
            dataGridView1.DataSource = null;
            SelectRule = lbBatch.Text != "全部" ? "and TeamNumber in (" + lbBatch.Text + ") " : "";
            SelectRule += "and datediff(dd,infusionDT,getdate())=0";
            DataSet ds = count("IVRecord_PZ",SelectRule);
            if (ds != null)
            {
                ds.Tables[0].Columns.RemoveAt(0);
                dataGridView1.DataSource = ds.Tables[0];
                    label_title1_1.Text = ds.Tables[0].Columns[0].ColumnName.Contains("Column") ? "" : ds.Tables[0].Columns[0].ColumnName;
                    label_title1_2.Text = ds.Tables[0].Columns[1].ColumnName.Contains("Column") ? "" : ds.Tables[0].Columns[1].ColumnName;
                    label_title1_3.Text = ds.Tables[0].Columns[2].ColumnName.Contains("Column") ? "" : ds.Tables[0].Columns[2].ColumnName;
                    label_title1_4.Text = ds.Tables[0].Columns[3].ColumnName.Contains("Column") ? "" : ds.Tables[0].Columns[3].ColumnName;
                    label_title1_5.Text = ds.Tables[0].Columns[4].ColumnName.Contains("Column") ? "" : ds.Tables[0].Columns[4].ColumnName;
                    label_title1_6.Text = ds.Tables[0].Columns[5].ColumnName.Contains("Column") ? "" : ds.Tables[0].Columns[5].ColumnName;
                    DvgCellStyle();
            }
            if (panel_dd1.Height - label_title1_1.Height <= dataGridView1.Rows.GetRowsHeight(0))
            {
                timer2.Enabled = false;
                GDtimer.Enabled = true;

            }
            else
            {
                timer2.Enabled = true;
                GDtimer.Enabled = false;
            }
        }


        public DataSet count(string record, string SelectRule)
        {
            string SelectRuleReturn = SelectRule + "and IVStatus>=3" + " and (LabelOver<0 or WardRetreat='1' or WardRetreat='2')";
            SelectRule = SelectRule + "and IVStatus>=3" + " and WardRetreat='0' and LabelOver>=0 ";
            int m = 0;
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append("select '' as 空");
            for (int i = 6; i < 12; i++)
            {       
                switch (dt.Rows[0][i].ToString())
                {
                    case "病区简称":
                        str.Append(",isnull(a.病区简称,0)病区简称");
                        m++;
                        break;
                    case "病区名称":
                        str.Append(",isnull(a.全部病区,0)病区名称");
                        m++;
                        break;
                    case "已核对":
                        str.Append(",isnull(b.已核对,0)已核对");
                        m++;
                        break;
                    case "打印":
                        str.Append(",isnull(c.已打印,0)已打印");
                        m++;
                        break;
                    case "已摆药":
                        str.Append(",isnull(b.已摆药,0)已摆药");
                        m++;
                        break;
                    case "已排药":
                        str.Append(",isnull(d.已排药,0)已排药");
                        m++;
                        break;
                    case "已进仓":
                        str.Append(",isnull(e.已进仓,0)已进仓");
                        m++;
                        break;
                    case "已配置":
                        str.Append(",isnull(f.已配置,0)已配置");
                        m++;
                        break;
                    case "已出仓":
                        str.Append(",isnull(g.已出仓,0)已出仓");
                        m++;
                        break;
                    case "已打包":
                        str.Append(",isnull(h.已打包,0)已打包");
                        m++;
                        break;
                    case "总共":
                        str.Append(",isnull(a.总数,0) 总数");
                        m++;
                        break;
                    case "未核对":
                        str.Append(",isnull(n.未核对,0) 未核对");
                        m++;
                        break;
                    case "空包":
                        str.Append(",isnull(i.空包,0)空包");
                        m++;
                        break;
                    case "临时":
                        str.Append(",isnull(j.临时,0)临时 ");
                        m++;
                        break;
                    case "已退药":
                        str.Append(",isnull(k.已退药,0)已退药 ");
                        m++;
                        break;
                    case "已签收":
                        str.Append(",isnull(m.已签收,0)已签收 ");
                        m++;
                        break;
                }
            }
            for (int i = 0; i < 6 - m; i++)
            {
                str.Append(",null");
            }
            str.Append(" from (select iv.WardName 全部病区,WardSimName 病区简称, COUNT(iv.WardCode)总数,iv.WardCode from IVRecord iv left join DWard d on d.WardCode=iv.WardCode  where 1=1 " + SelectRule + " ");
            //str.Append(" "+SelectRule+" ");
            //if (DWard != "")
            //    str.Append(" and WardCode<>'" + DWard + "'");
            //if (WardArea != "")
            //    str.Append(" and  WardCode  in (select WardCode from DWard where WardArea<>'" + WardArea + "') ");


            str.Append("group by iv.WardName,iv.WardCode,WardSimName)a");
            str.Append("  left join");
            str.Append(" (select WardName 全部病区,COUNT(WardCode)已摆药 from IVRecord a,IVRecod_YP_Check b ");
            str.Append(" where a.LabelNo=b.LabelNo and b.MenstruumCheck='1' and b.SolventCheck='1' " + SelectRule + "  group by WardName)b ");
            str.Append("  on a.全部病区=b.全部病区");
            str.Append(" left join  ");
            str.Append(" (select WardName 全部病区,COUNT(WardCode)未核对 from IVRecord a where 1=1 " + SelectRule + "");
            //str.Append(" and LabelNo not in(select IVRecordID from " + record + " where  ScanCount='0' and Invalid is null " + SelectRule + ")group by WardName) n");
            str.Append(" and LabelNo in((select LabelNo from IVRecord where 1=1 " + SelectRule + ") except (select IVRecordID from " + record + " where  ScanCount='0' and Invalid is null )) group by WardName) n");
            str.Append(" on a.全部病区=n.全部病区");
            str.Append(" left join ");
            str.Append(" (select WardName 全部病区,COUNT(WardCode)已打印 from IVRecord where IVStatus='3' " + SelectRule + "  group by WardName)c on a.全部病区= c.全部病区");
            str.Append(" left join(select WardName 全部病区,COUNT(WardCode)已排药 from IVRecord a,IVRecord_PY b ");
            str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0' and Invalid is null " + SelectRule + " group by WardName)d ");
            str.Append("  on a.全部病区=d.全部病区");
            str.Append(" left join(select WardName 全部病区,COUNT(WardCode)已进仓 from IVRecord a,IVRecord_JC b ");
            str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0' and Invalid is null " + SelectRule + " group by WardName)e ");
            str.Append("  on a.全部病区=e.全部病区");
            str.Append(" left join(select WardName 全部病区,COUNT(WardCode)已配置 from IVRecord a,IVRecord_PZ b ");
            str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0' and Invalid is null " + SelectRule + "  group by WardName)f ");
            str.Append("  on a.全部病区=f.全部病区");
            str.Append(" left join(select WardName 全部病区,COUNT(WardCode)已出仓 from IVRecord a,IVRecord_CC b ");
            str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0' and Invalid is null " + SelectRule + "  group by WardName)g ");
            str.Append("  on a.全部病区=g.全部病区");
            str.Append(" left join(select WardName 全部病区,COUNT(WardCode)已打包 from IVRecord a,IVRecord_DB b ");
            str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0' and Invalid is null " + SelectRule + " group by WardName)h ");
            str.Append("  on a.全部病区=h.全部病区");
            str.Append(" left join(select WardName 全部病区,COUNT(WardCode)已签收 from IVRecord a,IVRecord_QS b ");
            str.Append(" where a.LabelNo=b.IVrecordID and ScanCount='0' and Invalid is null " + SelectRule + " group by WardName)m ");
            str.Append("  on a.全部病区=m.全部病区");
            str.Append(" left join (select WardName 全部病区,COUNT(WardCode)空包 from IVRecord  where Batch like '%K%'  " + SelectRule + " Group by WardName)i on i.全部病区=a.全部病区");
            str.Append(" left join(select WardName 全部病区,COUNT(WardCode)临时 from IVRecord  where JustOne='1' " + SelectRule + " Group by WardName)j on j.全部病区=a.全部病区");
            str.Append(" left join (select WardName 全部病区,COUNT(WardCode)已退药 from IVRecord where 1=1 " + SelectRuleReturn + " group by WardName)k on a.全部病区=k.全部病区");
            str.Append(" order by a.WardCode");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }


        public DataSet CheckMateId(string CheckName)
        {
            DataSet ds = new DataSet();
            StringBuilder str = new StringBuilder();
            str.Append("select * from PivasCheckFormSet");
            str.Append(" where CheckName='" + CheckName + "' ");
            ds = db.GetPIVAsDB(str.ToString());
            return ds;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled =timer2.Enabled=GDtimer.Enabled= false;
            CheckFormSet set = new CheckFormSet(lbBatch.Text,(timer2.Interval/1000).ToString(),(GDtimer.Interval/1000).ToString(),stay1.ToString());
            set.closefather+=new CheckFormSet.childclose(set_closefather);
            set.ShowDialog();
        }

        private void set_closefather(string Batch,string interval,string GDinterval,string staytime) 
        {
            lbBatch.Text = Batch;
            
            timer2.Interval =Convert.ToInt32( interval)*1000;
            GDtimer.Interval = Convert.ToInt32(GDinterval) * 1000;
            stay = Convert.ToInt32(staytime);
            stay1 = stay;
            timer1.Enabled = true;
            //timer2.Enabled = true;
            //GDtimer.Enabled = true;
            try
            {
                dt = CheckMateId("电视机配置").Tables[0];
                showData();
                
            }
            catch
            {
                MessageBox.Show("读取配置失败，请检查数据库连接!");
            }

        }

        private void TitleChange()
        {
           
                label_title1_3.Text = dt.Rows[0]["Content2"].ToString();
                label_title1_5.Text = dt.Rows[0]["Content3"].ToString();
                label_title1_4.Text = dt.Rows[0]["Content4"].ToString();
                label_title1_2.Text = dt.Rows[0]["Content5"].ToString();
                label_title1_6.Text = dt.Rows[0]["Content6"].ToString();
                label_title1_1.Text = dt.Rows[0]["Content1"].ToString();
                //label_title2_1.Text = dt_cm.Rows[0]["Content1"].ToString();
                //CheckTitle();
        }

        private void CheckTitle()
        {
            foreach (Label c in panel_title1.Controls)
            {
                if (c.Text == "已核对")
                {
                    c.Text = "已配";
                }
                if (c.Text == "未核对")
                {
                    c.Text = "未配";
                }
                //if (c.Text == "前核对")
                //{
                //    c.Text = LastCheckName_C;
                //} 
                if (c.Text == "<空>")
                {
                    c.Visible = false;
                }
            }
        }

        private void DvgCellStyle()
        {
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[0].FillWeight = 40;
            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns[0].DefaultCellStyle.Font = new Font("黑体", 20);
            for (int i = 1; i <6; i++)
            {
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView1.Columns[i].FillWeight = 22;
                //dataGridView1.Columns[i].Width = 120;
                dataGridView1.Columns[i].DefaultCellStyle.Font = new Font("黑体", 32);
                dataGridView1.Columns[i].DefaultCellStyle.ForeColor = ColorTranslator.FromHtml(dt.Rows[0]["Color" + i].ToString());
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //int a = panel_dd1.Height;
            //int b = label_title1_1.Height;
            //int c = dataGridView1.Height;
            //int d = dataGridView1.Rows.GetRowsHeight(0);

            
                showData();
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
       
        private void GDtimer_Tick(object sender, EventArgs e)
        {
            if (panel_dd1.Height - label_title1_1.Height <= dataGridView1.Rows.GetRowsHeight(0)+10)
            {
                dataGridView1.Rows.RemoveAt(0);
            }
            else
            {
              GDtimer.Enabled=false;
              StayTimer.Enabled = true;                
            }
           
        }

        private void StayTimer_Tick(object sender, EventArgs e)
        {
            stay--;
            if (stay == 0)
            {
                showData();
                StayTimer.Enabled = false;
                stay = stay1;
            }
        }

        


    }
}
