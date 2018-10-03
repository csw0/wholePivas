using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PIVAsCommon.Helper;

namespace Damage
{
    public partial class Form2 : Form
    {
        private static Form2 formlnstance;

        public static Form2 instance
        {
            get {
                if (formlnstance == null)
                {
                    formlnstance = new Form2();
                }
                if(formlnstance.IsDisposed)
                {
                    formlnstance = new Form2();
                }
                return formlnstance;
            }
        }

        public static void ShowChildForm()
        {
            if (!instance.Visible)
            {
                instance.Show();
                instance.clean_combb();
            }
        }

        public Form2()
        {
            InitializeComponent();
            comb_zrr1_load();
            if(comboBox1.Text !=null && comboBox1.Text != "")
            { 
                comboBox1.Text = "";
            }

        }

        DB_Help DB = new DB_Help();
        //
        public void clean_combb()
        {
            comboBox1.Text = "";
        }

        //
        private DataSet sql_cx()
        {
            string time1 = dateTimePicker1.Value.ToShortDateString();
            string time2 = dateTimePicker2.Value.ToShortDateString();
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT dd.DrugName drugname,d.spec spec,quantity,d.amount amount ,damagetime,reason,de.DEmployeeName zrr,det.DEmployeeName tbr,nowtime ");
            sb.Append(" FROM Damage d left join DDrug dd on d.drugcode = dd.DrugCode ");
            sb.Append(" left join DEmployee de on d.zrrcode = de.DEmployeeCode ");
            sb.Append(" left join DEmployee det on d.tbrcode = det.DEmployeeCode ");
            sb.Append(" where damagetime between  '" + time1 + "' and dateadd(day,1,'" + time2 + "')");
            if ((comboBox1.Text != null) && (comboBox1.Text != ""))
            {
                sb.Append(" and d.zrrcode = '" + comboBox1.SelectedValue + "'");
            }
            sb.Append(" order by d.drugcode ");
            //MessageBox.Show((comboBox1.Text !=null).ToString());
            //MessageBox.Show((comboBox1.Text != "").ToString());

            DataSet ds = DB.GetPIVAsDB(sb.ToString());

            return ds;
        }

        private DataSet str_hz()
        {
            string time1 = dateTimePicker1.Value.ToShortDateString();
            string time2 = dateTimePicker2.Value.ToShortDateString();
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT dd.DrugName drugname,sum(quantity) squantity,sum(d.amount) samount    ");
            sb.Append(" FROM Damage d left join DDrug dd on d.drugcode = dd.DrugCode ");
            sb.Append(" where damagetime between  '" + time1 + "' and dateadd(day,1,'" + time2 + "')");
            if ((comboBox1.Text != null) && (comboBox1.Text != ""))
            {
                sb.Append(" and d.zrrcode = '" + comboBox1.SelectedValue + "'");
            }
            sb.Append(" group by dd.DrugName,d.drugcode order by d.drugcode  ");
            //MessageBox.Show((comboBox1.Text !=null).ToString());
            //MessageBox.Show((comboBox1.Text != "").ToString());

            DataSet ds = DB.GetPIVAsDB(sb.ToString());

            return ds; ;
        }

        private DataSet str_dy()
        {
            string time1 = dateTimePicker1.Value.ToShortDateString();
            string time2 = dateTimePicker2.Value.ToShortDateString();
            StringBuilder sb = new StringBuilder();
            sb.Append(" select CONVERT(varchar(10),damagetime,102) damagetime,d.DrugName drugname,dm.spec spec, ");
            sb.Append(" dm.quantity quantity,dm.batchNum batchnum,dm.xiaoqi xiaoqi,dm.amount amount,dm.reason reason ");
            sb.Append(" from Damage dm left join DDrug d on dm.DrugCode = d.DrugCode ");
            sb.Append(" where dm.damagetime between  '" + time1 + "' and dateadd(day,1,'" + time2 + "')");
            if ((comboBox1.Text != null) && (comboBox1.Text != ""))
            {
                sb.Append(" and d.zrrcode = '" + comboBox1.SelectedValue + "'");
            }
            sb.Append(" order by damagetime ");
            DataSet ds = DB.GetPIVAsDB(sb.ToString());
            return ds;
        }

        //combobox 责任人 初始化数据
        private void comb_zrr1_load()
        {
            try
            {
                comboBox1.Text = "";
                StringBuilder sb = new StringBuilder();
                sb.Append("select DEmployeeCode,DEmployeeName from DEmployee where type =1 ");
                DataTable dt = DB.GetPIVAsDB(sb.ToString()).Tables[0];
                comboBox1.DataSource = dt;
                if (dt != null && dt.Rows.Count > 0)
                {
                    comboBox1.DisplayMember = "DEmployeeName";
                    //MessageBox.Show(comb_zrr.SelectedValue);
                    comboBox1.ValueMember = "DEmployeeCode";
                    //comb_spec.ValueMember = dt.Rows[0].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //dataGridView1.Rows.Clear();
                dataGridView1.Visible = true;
                dataGridView2.Visible = false;
                DataTable dt = sql_cx().Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    dataGridView1.DataSource = dt;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Visible = false;
                dataGridView2.Visible = true;
                DataTable dt = str_hz().Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    dataGridView2.DataSource = dt;
                }
                else
                {
                    dataGridView2.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //private void printer(DataTable dt)
        //{
        //    previewControl1.Visible = true;
        //    report1.Clear();
        //    if (dt != null)
        //    {
        //        report1.Preview = previewControl1;
        //        report1.Load(".\\cctj_hz.frx");
        //        //report1.GetParameter("PrintDT").Value = DateTime.Now.ToString("");
        //        //report1.GetParameter("Total").Value = dt.Rows.Count;
        //        //report1.GetParameter("DemployName").Value = this.demployname;
        //        report1.RegisterData(dt, "dt");
        //        ((report1.FindObject("Data1")) as FastReport.DataBand).DataSource = report1.GetDataSource("dt");
        //        report1.Show();
        //    }
        //    else
        //    {
        //        previewControl1.Clear();

        //    }
        //}

        private void button3_Click(object sender, EventArgs e)
        {
            DataSet ds = str_dy();
            //DataTable dt = str_hz().Tables[0];
            //if (ds != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    printer(ds.Tables[0]);
            //}
            Form3 fm3 = new Form3(ds.Tables[0]);
            fm3.Show();
        }


        //public void Total(DataGridView dg)
        //{
        //    DataGridViewRow dgr = dg.Rows[dg.Rows.Count - 1];
        //    dgr.ReadOnly = true;
        //    dgr.Cells[0].Value = "合计";
        //    for (int i = 0; i < dg.Rows.Count - 1; i++)
        //    {
        //        dgr.Cells[2].Value = Convert.ToSingle(dgr.Cells[2].Value) + Convert.ToSingle(dg.Rows[i].Cells[2].Value);
        //    }
        //}  

    }
}
