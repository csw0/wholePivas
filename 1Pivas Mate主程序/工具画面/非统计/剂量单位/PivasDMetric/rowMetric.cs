using System;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PIVAsCommon.Helper;

namespace DMetricManage
{
    public partial class rowMetric : UserControl
    {
        DB_Help db = new DB_Help();
        DataTable dt;
        private int a;
        private string newId;
        private string unitstr = string.Empty;


        public rowMetric()
        {
            InitializeComponent();
        }


        public static string Oldid;
        public static string oldcode;
        public static string oldname;
        public static string PName;
        public static rowMetric RName;        
           
        public void add(DataRow row)//初始化一行计量单位
        {
            lblCode.Text = row["MetricCode"].ToString();
            lblName.Text = row["MetricName"].ToString();
            if ("" == row["ChineseName"].ToString() || "" == row["EnglishName"].ToString())
            {
                cbbPName.Text = row["ChineseName"].ToString() + row["EnglishName"].ToString();
                lblPName.Text = row["ChineseName"].ToString() + row["EnglishName"].ToString();
                unitstr = lblPName.Text;
            }
            else
            {
                cbbPName.Text = row["ChineseName"].ToString() + "|" + row["EnglishName"].ToString();
                lblPName.Text = row["ChineseName"].ToString() + "|" + row["EnglishName"].ToString();
                unitstr = lblPName.Text;
            }
            lblID.Text = row["UnitID"].ToString();
           // unitid = lblID.Text; 
        }

        private void delete_Click(object sender, EventArgs e)//删除剂量单位
        {
            try
            {
                deleteMetric form = new deleteMetric();

                Oldid = lblID.Text.ToString();
                oldcode = lblCode.Text.ToString();
                oldname = lblName.Text.ToString();
                PName = cbbPName.Text.ToString();
                RName = this;
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    this.Dispose();
                }
            
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void addstr(string str1,string str2,string str3,string str4)//界面显示增加计量单位
        {
            lblCode.Text = str1;
            lblName.Text = str2;
            lblPName.Text = str3;
            cbbPName.Text = str3;
            lblID.Text=str4;
        }

        public void updatestr(string str1, string str2, string str3)//更新计量单位画面显示
        {
            RName.lblCode.Text = str1;
            RName.lblName.Text = str2;
            RName.lblPName.Text = str3;
            RName.cbbPName.Text = str3;
        }

        public void show()//主画面单击修改时执行，使每一行变为可修改状态
        {
            delete.Visible = true;
            lblPName.Enabled = true;                      
            cbbPName.ForeColor = Color.FromArgb(0,0,0);
        }

        public void unshow()//主画面单击完成时执行，完成修改，变为不可修改状态
        {
            delete.Visible = false;
            cbbPName.Visible = false;
            lblPName.Visible = true;
            lblPName.Enabled = false;            
            cbbPName.ForeColor = Color.FromArgb(255,255,255);
        }


        public bool cbbPNameEnabled = false;
        private void SetCbbPName(DataTable dt)//单击时调用，初始化Pivas计量单位选框
        {
            string ss=string.Format("ChineseName+'|'+EnglishName");
            dt.Columns.Add("UnitName", typeof(string), ss);
            cbbPName.DataSource = dt;
            cbbPName.DisplayMember = "UnitName";
            cbbPName.ValueMember = "UnitID";
            if (lblPName.Text.Trim().Length < 0)
            {
                cbbPName.SelectedItem = 0;
            }
            else
            {
                cbbPName.Text = unitstr;
            }
            newId = cbbPName.SelectedValue.ToString();
            //updatdMetric(newId, Oldid, lblCode.Text, lblName.Text);
            cbbPNameEnabled = true;
            //lblPName.Text = cbbPName.Text;
            //lblID.Text = newId;
        }

        private void updatdMetric(string newID, string oldID, string oldcode, string oldname)//更新数据库的计量单位
        {
            StringBuilder str = new StringBuilder();
            str.Append("update DMetric set UnitID = " + newID + " ");
            str.Append("where  MetricCode = '" + oldcode + "' ");
            str.Append("and MetricName = '" + oldname + "'");
            if (oldID != "")
                str.Append(" and UnitID = " + oldID + "");
            try
            {
                db.SetPIVAsDB(str.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("错误：" + e.Message);
            }
        }

        private void cbbPName_SelectedIndexChanged(object sender, EventArgs e)//修改计量单位
        {
            try
            {
                a = cbbPName.SelectedIndex;
                newId = dt.Rows[a]["UnitID"].ToString();
                if (cbbPNameEnabled && cbbPName.Visible)
                {
                    updatdMetric(newId, Oldid, lblCode.Text, lblName.Text);
                    cbbPName.Visible = false;
                    cbbPName.Enabled = false;
                    lblPName.Visible = true;
                    cbbPNameEnabled = true;
                    cbbPNameEnabled = false;
                    unitstr = cbbPName.Text;
                }
                lblPName.Text = cbbPName.Text;
                lblID.Text = newId;
                if (cbbPName.Visible)
                {
                    lblPName.Visible = !cbbPName.Visible;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void label3_Click(object sender, EventArgs e)//单击要修改的计量单位，显示combobox备选框
        {
            if (rowMetric.RName != null)
            {
                rowMetric.RName.cbbPName.Visible = false;
                rowMetric.RName.lblPName.Visible = true;
                rowMetric.RName.cbbPNameEnabled = false;
            }
            lblPName.Visible = false;
            cbbPName.Visible = true;
            cbbPName.Enabled = true;
            rowMetric.RName = this;
        }

        private void rowMetric_Click(object sender, EventArgs e)//单击行事件，是之前未修改的combobox隐藏
        {
            if (rowMetric.RName != null)
            {
                rowMetric.RName.cbbPName.Visible = false;
         
                rowMetric.RName.lblPName.Visible = true;
                rowMetric.RName.cbbPNameEnabled = false;
            }
        }

        private void cbbPName_VisibleChanged(object sender, EventArgs e)
        {
            if(cbbPName.Visible)
            {
                Oldid = lblID.Text.ToString();
                string str = "select * from KD0100..DMetrologyUnit";
                DataSet ds = new DataSet();
                ds = db.GetPIVAsDB(str);
                dt = ds.Tables[0];
                SetCbbPName(dt);
            }
        }
    }
}
