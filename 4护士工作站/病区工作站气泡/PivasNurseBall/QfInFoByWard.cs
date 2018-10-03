using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace PivasNurseBall
{
    public partial class QfInFoByWard : Form
    {
        public string wardCode;
        public QfInFoByWard( string ward )
        {
            
            this.MaximizeBox = false;
            wardCode = ward;
            InitializeComponent();
            beginDT.Text = DateTime.Now.Date.ToString();
            endDT.Text = DateTime.Now.AddDays(2).Date.ToString();
            listview1Init();
            listview2Init();

            QfInfo(ward, 1, "", DateTime.Now.Date.ToString(), DateTime.Now.AddDays(2).Date.ToString());
          
            QfInfo(ward, 2, "", DateTime.Now.Date.ToString(), DateTime.Now.AddDays(2).Date.ToString());
           
            QfInfo(ward, 3, "(ALL)", DateTime.Now.Date.ToString(), DateTime.Now.AddDays(2).Date.ToString());
            labelInfo();
            showNotice();

        }

        private void listview1Init()
        {

            listView1.View = View.Details;
            listView1.GridLines = false;
            listView1.FullRowSelect = true;

            ColumnHeader header1 = new ColumnHeader();
            ColumnHeader header2 = new ColumnHeader();
            ColumnHeader header3 = new ColumnHeader();


            header1.Text = "床号";
            header2.Text = "病人";
            header3.Text = "数量";


            header1.Width = 70;
            header2.Width = 90;
            header3.Width = 45;


            listView1.Columns.Add(header1);
            listView1.Columns.Add(header2);
            listView1.Columns.Add(header3);
        }
        private void listview2Init()
        {

            listView2.View = View.Details;
            listView2.GridLines = false;
            listView2.FullRowSelect = true;

            ColumnHeader header1 = new ColumnHeader();
            ColumnHeader header2 = new ColumnHeader();
            ColumnHeader header3 = new ColumnHeader();
            ColumnHeader header4 = new ColumnHeader();
            ColumnHeader header5 = new ColumnHeader();
            


            header1.Text = "床号";
            header2.Text = "病人姓名";
            header3.Text = "瓶签号";
            header4.Text = "频次";
            header5.Text = "用药时间";



            header1.Width = 65;
            header2.Width = 96;
            header3.Width = 135;
            header4.Width = 60;
            header5.Width = 170;


            listView2.Columns.Add(header1);
            listView2.Columns.Add(header2);
            listView2.Columns.Add(header3);
            listView2.Columns.Add(header4);
            listView2.Columns.Add(header5);
        }

        private void listview3Init()
        {

            listView3.View = View.Details;
            listView3.GridLines = false;
            listView3.FullRowSelect = true;

            ColumnHeader header1 = new ColumnHeader();
            ColumnHeader header2 = new ColumnHeader();
            ColumnHeader header3 = new ColumnHeader();


            header1.Text = "药品";
            header2.Text = "规格";
            header3.Text = "剂量";


            header1.Width = 180;
            header2.Width = 95;
            header3.Width = 85;


            listView3.Columns.Add(header1);
            listView3.Columns.Add(header2);
            listView3.Columns.Add(header3);
        }

        private void labelInfo()
        {
            label8.Text = "床号";
            label6.Text = "性别";
            label15.Text = "年龄";
            label17.Text = "病人编码";
            label18.Text = "频次";
            label11.Text = "时间";
            label13.Text = "批次";
           // label8.Text = "床号";


        }

        private void QfInfo(string wardcode,int i,string patcode ,string begindt ,string enddt)
        {
            try
            {
                string sql = "exec QfInfoSelect '{0}',{1},'{2}','{3}','{4}'";
                sql = string.Format(sql, wardcode, i, patcode, begindt, enddt);
                DB_Help db = new DB_Help();
                DataSet ds = db.GetPIVAsDB(sql);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int a = 0; a < ds.Tables[0].Rows.Count; a++)
                    {
                        if (i == 1)
                        {
                            label10.Text = ds.Tables[0].Rows[0][0].ToString();
                            label2.Text = ds.Tables[0].Rows[0][1].ToString();
                            label20.Text = ds.Tables[0].Rows[0][2].ToString();

                        }
                        else if (i == 2)
                        {

                            ListViewItem lv1 = new ListViewItem(ds.Tables[0].Rows[a][0].ToString().Trim());
                            lv1.SubItems.Add(ds.Tables[0].Rows[a][1].ToString().Trim());
                            lv1.SubItems.Add(ds.Tables[0].Rows[a][2].ToString().Trim());
                            listView1.Items.Add(lv1);
                        }
                        else if (i == 3)
                        {

                            ListViewItem lv1 = new ListViewItem(ds.Tables[0].Rows[a][0].ToString().Trim());
                            lv1.SubItems.Add(ds.Tables[0].Rows[a][1].ToString().Trim());
                            lv1.SubItems.Add(ds.Tables[0].Rows[a][2].ToString().Trim());
                            lv1.SubItems.Add(ds.Tables[0].Rows[a][3].ToString().Trim());
                            lv1.SubItems.Add(ds.Tables[0].Rows[a][4].ToString().Trim());
                            listView2.Items.Add(lv1);
                        }
                        else
                        {

                            ListViewItem lv1 = new ListViewItem(ds.Tables[0].Rows[a][0].ToString().Trim());
                            lv1.SubItems.Add(ds.Tables[0].Rows[a][1].ToString().Trim());
                            lv1.SubItems.Add(ds.Tables[0].Rows[a][2].ToString().Trim());
                            listView3.Items.Add(lv1);
                        }

                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            listView2.Clear();
            listview2Init();
            try
            {
                string PatSelect = listView1.SelectedItems[0].Text;
                QfInfo(wardCode, 3, PatSelect, beginDT.Text, endDT.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
           

        }

        private void listView2_Click(object sender, EventArgs e)
        {
            listView3.Clear();
            listview3Init();
            string sql = "select DrugName ,Spec ,iv.WardName ,pt.BedNo ,case pt.Sex when '1' then '男' else '女' end sex  ,pt.Age ,pt.AgeSTR ,iv.PatCode, " +
                               "iv.Batch ,Convert(float,id.Dosage) Dosage,id.DosageUnit ,CONVERT (varchar(100),iv.InfusionDT,24) InfusionDT,iv.FreqCode   " +
                               "from IVRecord iv inner join IVRecordDetail id on iv.IVRecordID =id.IVRecordID " +
                               "inner join Patient pt on pt.PatCode =iv.PatCode where LabelNo =";
           
            try
            {
                string labelno = listView2.FocusedItem.SubItems[2].Text;
                DB_Help db = new DB_Help();
                DataSet ds = db.GetPIVAsDB(sql+labelno);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    label8.Text = ds.Tables[0].Rows[0]["BedNo"].ToString();
                    label6.Text = ds.Tables[0].Rows[0]["sex"].ToString();
                    label15.Text = ds.Tables[0].Rows[0]["age"].ToString() + ds.Tables[0].Rows[0]["AgeSTR"].ToString();
                    label17.Text = ds.Tables[0].Rows[0]["PatCode"].ToString();
                    label18.Text = ds.Tables[0].Rows[0]["FreqCode"].ToString();
                    label11.Text = ds.Tables[0].Rows[0]["InfusionDT"].ToString();
                    label13.Text = ds.Tables[0].Rows[0]["Batch"].ToString();
                    for (int a=0;a <ds.Tables[0].Rows.Count;a++)
                    {
                        ListViewItem lv1 = new ListViewItem(ds.Tables[0].Rows[a]["DrugName"].ToString().Trim());
                        lv1.SubItems.Add(ds.Tables[0].Rows[a]["Spec"].ToString().Trim());
                        lv1.SubItems.Add(ds.Tables[0].Rows[a]["Dosage"].ToString().Trim() + ds.Tables[0].Rows[a]["DosageUnit"].ToString().Trim());
                        listView3.Items.Add(lv1);
                             
                    }
                }

            }
            catch (Exception)
            {
                
                throw;
            }


            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QfInfo(wardCode, 1, "", beginDT.Text, endDT.Text);
            listView1.Clear();
            listview1Init();
            QfInfo(wardCode, 2, "", beginDT.Text, endDT.Text);
            listView2.Clear();
            listview2Init();
            QfInfo(wardCode, 3, "(ALL)", beginDT.Text, endDT.Text);
            labelInfo();
            listView3.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
        }

        private DataSet getDrug()
        {
            DB_Help db = new DB_Help();
            DataSet ds = null;
            try 
            {
                ds = db.GetPIVAsDB("SELECT DrugName, Spec, firm_id FROM DDrug WHERE isStop = 1");
            }
            catch { }
            return ds;
        }

        private void showNotice()
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Refresh();
            this.dataGridView1.DataSource = getDrug().Tables[0];
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
            DB_Help db = new DB_Help();
            try 
            {
                DataSet ds = db.GetPIVAsDB("SELECT DrugCode FROM NoticeSet WHERE WardCode = '" + wardCode + "' AND status = 0");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int a = 0; a < ds.Tables[0].Rows.Count; a++)
                    {
                        ListViewItem lv1 = new ListViewItem(ds.Tables[0].Rows[a]["DrugName"].ToString().Trim());
                        lv1.SubItems.Add(ds.Tables[0].Rows[a]["Spec"].ToString().Trim());
                        lv1.SubItems.Add(ds.Tables[0].Rows[a]["Dosage"].ToString().Trim() + ds.Tables[0].Rows[a]["DosageUnit"].ToString().Trim());
                        listView3.Items.Add(lv1);

                    }
                    this.panel2.Visible = true;
                }
            }
            catch (Exception ex) { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.panel2.Visible = false;
        }
    }
}
