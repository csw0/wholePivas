using System;
using System.Windows.Forms;
using System.Threading;
using System.Data;
using System.Collections.Generic;
using PivasLabelCheckAll.entity;
using System.Drawing;

namespace PivasLabelCheckAll
{
    public partial class WaitForm : Form
    {
        private dao.seldb sel = new dao.seldb();
        private int c = 0;//当前扫描到的瓶签的序号
        private int s = 0; //统计核对成功的数量
        private int current = 0;//统计当前扫面瓶签的序号
        private UCCommonCheck PK = null;
        private int CountAll = 0;
        //private int CountNow = 0;
        //private bool Go = false;
        private bool IsBreak;

        public DataSet ds = new DataSet();//获取批量扫描需要扫的瓶签号集
        public DataTable dt = new DataTable();//获取批量扫描需要扫的瓶签号集
        private delegate void CtrUI(object obj);//控制控件变化的委托
        //线程对象
        Thread tWork;

        //退出标志
        //bool bExit = false;

        public WaitForm(UCCommonCheck check)
        {
            InitializeComponent();
            PK = check;
            CountAll = PK.piliangCount;//总数
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            label1.Text = label1.Text == "正在扫描中，请稍后..." ? "正在扫描中，请稍后." : (label1.Text == "正在扫描中，请稍后." ? "正在扫描中，请稍后.." : "正在扫描中，请稍后...");
            label3.Text = "当前第" + current.ToString() + "个，共" + listView1.Items.Count.ToString() + "个";

            if (current == listView1.Items.Count)
            {
                timer1.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                label1.Text = "扫描结束！";
                label2.Text = "...";
                label2.ForeColor = Color.Black;
                label3.Text = "成功" + s.ToString() + "个，共" + listView1.Items.Count.ToString() + "个";
            }
        }



        private void WaitForm_Load(object sender, EventArgs e)
        {
            this.IsBreak = PK._IsBreak;
            if (IsBreak == false)
            {
                button2.Visible = false;
            }
            else
            {
                button2.Visible = true;
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListViewItem m = new ListViewItem();
                    m.Text = dt.Rows[i]["瓶签号"].ToString();
                    m.SubItems.Add("未核对");
                    listView1.Items.Add(m);
                }
            }

            //object obj = new object[1] { c };
            Thread tWork = new Thread(new ParameterizedThreadStart(Work));
            //ThreadPool.QueueUserWorkItem(new WaitCallback(Work),obj);
            object obj = new object[1] { c };
            tWork.IsBackground = true;
            tWork.Start(obj);

        }

        void Work(object obj)
        {
            object[] newobj = obj as object[];
            int co = (int)newobj[0];
            for (int i = co; i < dt.Rows.Count; i++)
            {

                int a = PK.LabelsCheck(dt.Rows[i]["瓶签号"].ToString());
                if (a > 0)
                {
                    s = s + 1;
                    CtrUI ctrui = new CtrUI(Succ);
                    this.Invoke(ctrui, i);
                }
                else
                {
                    CtrUI ctrui = new CtrUI(Fail);
                    this.Invoke(ctrui, i);
                    this.c = i + 1;
                    if (IsBreak == true )
                    {
                        return;
                    }
                }
                current = current < dt.Rows.Count ? i + 1 : i;
                Thread.Sleep(1);
            }
        }

        //当工作结束
        private void WorkOver(object sender, EventArgs e)
        {
            Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PK.piliangFlag = false;
            this.Close();
        }

        private void Succ(object obj)
        {
            //object[] newobj = obj as object[];
            int i = (int)obj;
            label2.Text = PK.piliangBarcode.ToString() + "核对成功";
            label2.ForeColor = Color.Green;
            
            listView1.Items[i].Checked = true;
            listView1.Items[i].SubItems[1].Text="核对成功";
            listView1.Items[i].BackColor = Color.LightGreen;
        }

        private void Fail(object obj)
        {
            //object[] newobj = obj as object[];
            //int i = (int)newobj[0];
            int i = (int)obj;
            label2.Text = PK.piliangBarcode.ToString() + "核对失败";
            label2.ForeColor = Color.Red;
            listView1.Items[i].Checked = true;
            listView1.Items[i].SubItems[1].Text = "核对失败";
            listView1.Items[i].BackColor = Color.LightPink;
            DataTable dt1 = sel.getIVRecordDetail(listView1.Items[i].Text).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                ShowLabelDetail(dt1);
            }
            else
            {
                dgvDrugs.Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //object obj = new object[1] { c };
            Thread tWork = new Thread(new ParameterizedThreadStart(Work));
            //ThreadPool.QueueUserWorkItem(new WaitCallback(Work),obj);
            object obj = new object[1] { c };
            tWork.IsBackground = true;
            tWork.Start(obj);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dgvDrugs.Visible = false;
            Thread tWork = new Thread(Work);
            object obj = new object[1] { c };
            tWork.IsBackground = true;
            tWork.Start(obj);
        }

        public void ShowLabelDetail(DataTable dt1)
        {
            dgvDrugs.Visible = true;
            dgvDrugs.Rows.Clear();
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    dgvDrugs.Rows.Add(1);
                    dgvDrugs.Rows[i].Cells["DrugName"].Value = dt1.Rows[i]["DrugName"].ToString().Trim();
                    dgvDrugs.Rows[i].Cells["Spec"].Value = dt1.Rows[i]["Spec"].ToString().Trim();
                    dgvDrugs.Rows[i].Cells["Dosage"].Value = dt1.Rows[i]["Dosage"].ToString().Trim().TrimEnd('0').TrimEnd('.') + dt1.Rows[i]["DosageUnit"].ToString().Trim();
                }

            }
        }



    }
}
