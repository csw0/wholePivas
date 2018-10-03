using System;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PivasLabelSelect;
using PIVAsCommon.Helper;

namespace ErrorRecord
{
    public partial class ErrorAddOthers : UserControl
    {
        public int c = 0;
        public int b = 0;
        public string sEid = string.Empty;
        public string stext = string.Empty;
        public string sname = string.Empty;
        public string stime = string.Empty;
        DB_Help DB = new DB_Help();
        SQL sql = new SQL();
        public int rc = 0;
        public string UserID;
        public ErrorAddOthers()
        {
            InitializeComponent();
            
        }
        public ErrorAddOthers(string UserID)
        {
            InitializeComponent();
            this.UserID = UserID;
        }
        public ErrorAddOthers(string PreID,string UserID)
        {
            InitializeComponent();
            this.UserID = UserID;
            textBox1.Text = PreID;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (textBox1.Text == "")
                {
                    clean();
                    //PreInformation p1 = new PreInformation();
                    //panel1.Controls.Add(p1);
                }
                else
                {
                    CheckPreID(textBox1.Text);
                }
            }
        }
        private void addcol()
        {
            DataGridViewTextBoxColumn dr = new DataGridViewTextBoxColumn();
            dr.DataPropertyName = "LabelNo";
            dr.HeaderText = "瓶签号";
            dr.Name = "Column1";
            dr.Width = 120;
            dataGridView1.Columns.Add(dr);
        }

        private void CheckPreID(string PreID)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" select PrescriptionID  from Prescription where PrescriptionID= '" + PreID + "' ");
            str.Append(" select PrescriptionID from IVRecord where LabelNo='" + PreID + "'");
            DataTable dt = DB.GetPIVAsDB(str.ToString()).Tables[0];
            DataTable dt1 = DB.GetPIVAsDB(str.ToString()).Tables[1];
            if (dt.Rows.Count == 0 || dt == null)
            {
                if(dt1.Rows.Count == 0 || dt1 == null)
                {
                    MessageBox.Show("不存在此瓶签号/医嘱号");
                    textBox1.Controls.Clear();
                    clean();
                }
                else
                {
                    dataGridView1.DataSource = null;
                    //dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();
                    addcol();
                    dataGridView1.Rows.Add(textBox1.Text);
                    dataGridView1.Visible = true;
                    panel1.Controls.Clear();
                    rc = 0;
                    b = 0;
                    c = 0;
                    label4.Text = dt1.Rows[0][0].ToString();
                    ShowLabelCheck(dt1.Rows[0][0].ToString(),PreID);
                    label5.Visible = true;
                    panel1.Visible = true;
                }
            }
            else
            {
                this.panel1.Controls.Clear();
                rc = 0;
                b = 0;
                c = 0;
                dataGridView1.Controls.Clear();
                dataGridView1.Columns.Clear();
                addcol();
                string str1 = "select distinct labelno from ivrecord where prescriptionid = '" + PreID + "'";
                DataTable dt2 = DB.GetPIVAsDB(str1).Tables[0];
                dataGridView1.DataSource = dt2;
                dataGridView1.Visible = true;
                label4.Text = PreID;
                ShowLabelCheck(PreID,dt2.Rows[0][0].ToString());
                label5.Visible = true;
                panel1.Visible = true;
                
            }
        }

        private void ShowLabelCheck(string PreID,string Label)
        {
            if(PreID!="")
            {
                DataSet ds1 = DB.GetPIVAsDB(sql.Getsf(PreID));
                CancelMessage msg = new CancelMessage(this);
                msg.setmsg("审方", ds1.Tables[0].Rows[0]);
                msg.Top = rc * 25;
                panel1.Controls.Add(msg);
                rc++;
            }
            DataSet ds = DB.GetPIVAsDB(sql.msg(Label));
            for (int J = 0; J < ds.Tables[1].Rows.Count; J++)
            {

                CancelMessage msg = new CancelMessage(this);
                msg.setmsg("核对", ds.Tables[1].Rows[J]);
                msg.Top = rc * 25;
                panel1.Controls.Add(msg);
                rc++;
            }
            if (int.Parse(ds.Tables[0].Rows[0]["LabelOver"].ToString()) < 0)
            {
                CancelMessage msg = new CancelMessage(this);
                msg.setmsg("配置取消", ds.Tables[0].Rows[0]);
                msg.Top = rc * 25;
                panel1.Controls.Add(msg);
                rc++;
            }
            if (int.Parse(ds.Tables[0].Rows[0]["WardRetreat"].ToString()) > 0)
            {
                CancelMessage msg = new CancelMessage(this);
                msg.setmsg("退药", ds.Tables[0].Rows[0]);
                msg.Top = rc * 25;
                panel1.Controls.Add(msg);
                rc++;
            }

        }
        private void buttonSave_Click(object sender, EventArgs e)
        { 
            StringBuilder str = new StringBuilder();
            str.Append(" select PrescriptionID  from Prescription where PrescriptionID= '" + textBox1.Text + "' ");
            str.Append(" select PrescriptionID from IVRecord where LabelNo='" + textBox1.Text + "'");
            DataTable dt = DB.GetPIVAsDB(str.ToString()).Tables[0];
            DataTable dt1 = DB.GetPIVAsDB(str.ToString()).Tables[1];
            if ((dt.Rows.Count == 0 || dt == null) && (dt1.Rows.Count == 0 || dt1 == null))
            {

                MessageBox.Show("不存在此瓶签号/医嘱号");
                textBox1.Controls.Clear();
            }

            else
            {
                GetCtl();
            }
        }

        private void ErrorAddOthers_Load(object sender, EventArgs e)
        {
            this.MouseWheel += PanelMouseWheel;
            string str = "select distinct StatusCode,StatusName from ErrorRule";
            DataTable dt = DB.GetPIVAsDB(str).Tables[0];
            if (dt.Rows.Count != 0)
            {
                comboFind.DataSource = dt;
                comboFind.ValueMember = "StatusCode";
                comboFind.DisplayMember = "StatusName";
            }
            else
            {
                MessageBox.Show("规则列表为空，请维护规则后刷新！");
            }

            if (textBox1.Text != "")
            {
                CheckPreID(textBox1.Text);
            }
        }


        private void GetCtl() 
        {
            foreach (Control msg in panel1.Controls) 
            {
                
                if (msg is CancelMessage) 
                {
                    if (((CancelMessage)msg).checkBox1.Checked)
                    {
                        string l = ((CancelMessage)msg).lb_name.Text.Substring(0, ((CancelMessage)msg).lb_name.Text.LastIndexOf(":"));
                        StringBuilder str = new StringBuilder();
                        str.Append ( "  select case LEN(statuscode) when 1 then '0'+convert(varchar,statuscode) else convert(varchar,statuscode) end as statuscode");
                        str.Append(",case LEN(TypeCode) when 1 then '0'+convert(varchar,TypeCode) else convert(varchar,TypeCode) end as Typecode ");
                        str.Append("from ErrorRule where statusname ='" + l + "'and typename='" + ((CancelMessage)msg).comboBox1.Text + "'");
                        DataTable dt = DB.GetPIVAsDB(str.ToString()).Tables[0];
                        string str1 = "  select isnull(MAX(substring (ErrorCode,14,6)),'000000') from ErrorRecord where DATEDIFF(DD,findtime,'" + DateTime.Now.Date.ToString() + "')=0";
                        DataSet ds = DB.GetPIVAsDB(str1);
                        string s = ds.Tables[0].Rows[0][0].ToString();
                        int num = Convert.ToInt32(s);
                        num++;
                        s = 'E' + DateTime.Now.Date.ToString("yyyyMMdd") + dt.Rows[0][0].ToString() + dt.Rows[0][1].ToString() + num.ToString().PadLeft(6, '0');

                        string stradd = "insert into ErrorRecord values ('" + s + "','" + textBox1.Text + "','" + ((CancelMessage)msg).textBox1.Text + "','" + dt.Rows[0][0].ToString() + "','" + comboFind.SelectedValue + "','" + ((CancelMessage)msg).label2.Text + "','" + UserID + "','" + ((CancelMessage)msg).lb_time.Text + "','" + DateTime.Now.ToString() + "','" + ((CancelMessage)msg).comboBox1.SelectedValue + "')";
                        DB.SetPIVAsDB(stradd);
                    }
                }
            }
            MessageBox.Show("成功保存"+c+"个记录！");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        ///gundongfangfa
        ///
        private void PanelMouseWheel(object sender, MouseEventArgs e)
        {
            Point MousePoint = new Point(e.X,e.Y);
            MousePoint.Offset(this.Location.X, this.Location.Y);
            if (panel1.RectangleToScreen(panel1.DisplayRectangle).Contains(MousePoint))
            {
                panel1.AutoScrollPosition = new Point(0, panel1.VerticalScroll.Value - e.Delta);
            }

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            clean();
        }

        public void Topchange()
        {
           // int b = 0;
            rc = 0;
            foreach (Control msg in panel1.Controls)
            {

                if (msg is CancelMessage)
                {
                    if (((CancelMessage)msg).checkBox1.Checked)
                    {
                        if (rc == 0) { b = 0; }
                        msg.Top = rc * 25 + b;
                        msg.Height = 50;
                        rc++;
                        b = b + 35;
                    }
                    else
                    {
                        if (rc == 0) { b = 0; }
                        msg.Top = rc * 25 + b;
                        msg.Height = 25;
                        rc++;
                    }
                    
                }
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                panel1.Controls.Clear();
                rc = 0;
                b = 0;
                c = 0;
                ShowLabelCheck(label4.Text,dataGridView1.CurrentRow.Cells[0].Value.ToString());
            }
        }
        private void clean()
        {
            textBox1.Text = string.Empty;
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            panel1.Controls.Clear();
            dataGridView1.Visible = false;
            panel1.Visible = false;
            label5.Visible = false;
            rc = 0;
            b = 0;
            c = 0;
        }
        
        
    }
}
