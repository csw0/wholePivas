using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;


namespace ErrorRecord
{
    public partial class ErrorAdd : UserControl
    {
        public string UserID=string.Empty;
        protected internal DB_Help DB = new DB_Help();
        protected internal SplitContainer sp1;
        public ErrorAdd()
        {
            InitializeComponent();
        }
        public ErrorAdd(string ID)
        {
            InitializeComponent();
            UserID = ID;
        }

        private void ErrorAdd_Load(object sender, EventArgs e)
        {
            LordTapAdd();
            //ErrorCode();
            PreInformation p1 = new PreInformation();
            p1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(p1);
            //tag2_Lord();\
            if (UserID != string.Empty)
            {
                string str = "select DEmployeeName from DEmployee where DEmployeeID= '" + UserID + "'";
                DataSet ds = DB.GetPIVAsDB(str);
                textErrorName.Text = ds.Tables[0].Rows[0][0].ToString();
            }
        }

        private void LordTapAdd()
        {
            string str = "select distinct StatusCode,StatusName from ErrorRule";
            DataTable dt = DB.GetPIVAsDB(str).Tables[0];
            if (dt.Rows.Count != 0)
            {
                comboStatus.DataSource = dt;
                comboStatus.ValueMember = "StatusCode";
                comboStatus.DisplayMember = "StatusName";
                comboFind.DataSource = dt.Copy();
                comboFind.ValueMember = "StatusCode";
                comboFind.DisplayMember = "StatusName";

            }
            else
            {
                MessageBox.Show("规则列表为空，请维护规则后刷新！");
            }

        }
        private void LordType()
        {
            string strtype = "select distinct TypeCode,TypeName from ErrorRule where StatusName='"+comboStatus.Text+"'";
            DataTable dtype = DB.GetPIVAsDB(strtype).Tables[0];
            comboType.DataSource = dtype;
            comboType.ValueMember = "TypeCode";
            comboType.DisplayMember = "TypeName";


        }

        private void ErrorCode()
        {
            StringBuilder str = new StringBuilder();
            str.Append("  select case LEN(statuscode) when 1 then '0'+convert(varchar,statuscode) else convert(varchar,statuscode) end as statuscode");
            str.Append(",case LEN(TypeCode) when 1 then '0'+convert(varchar,TypeCode) else convert(varchar,TypeCode) end as Typecode ");
            str.Append("from ErrorRule where statusname ='" + comboStatus.Text + "'and typename='" + comboType.Text + "'");
            //string str="select statuscode,typecode from errorrule where statusname ='"+comboStatus.Text+"'and typename='"+comboType.Text+"'";
            DataTable dt = DB.GetPIVAsDB(str.ToString()).Tables[0];
            //label3.Text="0"+dt.Rows[0][0].ToString()+"0"+dt.Rows[0][1].ToString+"";
            string str1 = "  select isnull(MAX(substring (ErrorCode,14,6)),'000000') from ErrorRecord where DATEDIFF(DD,findtime,'" + DateTime.Now.Date.ToString() + "')=0";
            DataSet ds = DB.GetPIVAsDB(str1);
            string s = ds.Tables[0].Rows[0][0].ToString();
            int num = Convert.ToInt32(s);
            num++;
            s = 'E' + DateTime.Now.Date.ToString("yyyyMMdd")  + dt.Rows[0][0].ToString()  + dt.Rows[0][1].ToString() +  num.ToString().PadLeft(6, '0');
            label2.Text = s;
        }
        private void comboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LordType();
            richTextDescribe.Text = string.Empty;
            richTextDescribe.Text = richTextDescribe.Text + comboStatus.Text + ";" + comboType.Text + ";";
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                ErrorCode();
                string strnm = "select DemployeeID from Demployee where DemployeeName = '" + textErrorName.Text + "'";
                DataSet ds = DB.GetPIVAsDB(strnm);
                if (textBox1.Text == "")
                {
                    MessageBox.Show("请输入瓶签号/医嘱号!");
                }
                else
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
                        if (ds == null || ds.Tables[0].Rows.Count == 0)
                        {
                            MessageBox.Show("差错人不存在!");
                        }
                        else
                        {

                            string stradd = "insert into ErrorRecord values ('" + label2.Text + "','" + textBox1.Text + "','" + richTextDescribe.Text + "','" + comboStatus.SelectedValue + "','" + comboFind.SelectedValue + "','" + ds.Tables[0].Rows[0][0].ToString() + "','" + UserID + "','" + dateTimePicker1.Value.ToString() + "','" + DateTime.Now.ToString() + "','" + comboType.SelectedValue + "')";
                            DB.SetPIVAsDB(stradd);
                            MessageBox.Show("保存成功！");
                        }
                    }
                }
            }

            catch (Exception ex)
            {
            }
        }

        private void textErrorName_TextChanged(object sender, EventArgs e)
        {
            if (!listBox1.Focused)
            {
                string sql = "select DemployeeID,DemployeeName from Demployee where DemployeeName like " + "'%" + this.textErrorName.Text + "%'";
                DataSet ds = DB.GetPIVAsDB(sql);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.listBox1.Visible = true;
                        this.listBox1.Items.Clear();
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            this.listBox1.Items.Add(ds.Tables[0].Rows[i][1].ToString());
                        }
                    }
                }
            }
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.listBox1.Items.Count > 0)
            {
                this.textErrorName.Text = this.listBox1.SelectedItem.ToString();
                //this.listBox1.Visible = false;
            }
        }

        private void textErrorName_Leave(object sender, EventArgs e)
        {
            if(!listBox1.Focused)
            {
            listBox1.Visible = false;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            comboStatus.Text = string.Empty;
            comboFind.Text = string.Empty;
            comboType.Text = string.Empty;
            richTextDescribe.Text = string.Empty;
            textErrorName.Text = string.Empty;
            textErrorName.Focus();
            textBox1.Focus();
        }

        private void comboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextDescribe.Text = string.Empty;
            richTextDescribe.Text = richTextDescribe.Text + comboStatus.Text + ";" + comboType.Text + ";";
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (textBox1.Text == "")
                {
                    PreInformation p1 = new PreInformation();
                    splitContainer1.Panel2.Controls.Add(p1);
                }
                else
                {
                    CheckPreID(textBox1.Text);
                }
            }
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
                if (dt1.Rows.Count == 0 || dt1 == null)
                {
                    MessageBox.Show("不存在此瓶签号/医嘱号");
                    textBox1.Controls.Clear();
                }
                else
                {
                    splitContainer1.Panel2.Controls.Clear();
                    PreInformation p = new PreInformation(dt1.Rows[0][0].ToString(),PreID);
                    splitContainer1.Panel2.Controls.Add(p);
                    p.Dock = System.Windows.Forms.DockStyle.Fill;
                }
            }
            else
            {
                splitContainer1.Panel2.Controls.Clear();
                PreInformation p = new PreInformation(dt.Rows[0][0].ToString());
                splitContainer1.Panel2.Controls.Add(p);
                p.Dock = System.Windows.Forms.DockStyle.Fill;
            }   
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            } 
        }

    

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Up)
            //{
            //    int idx = listBox1.SelectedIndex;
            //    if (idx == -1)
            //    {
            //        listBox1.SelectedItem = listBox1.Items[listBox1.Items.Count - 1];
            //    }
            //    else
            //    {
            //        if (idx == 0)
            //        {
            //            listBox1.SelectedItem = listBox1.Items[listBox1.Items.Count - 1];
            //            idx = listBox1.Items.Count;
            //        }
            //        listBox1.SelectedItem = listBox1.Items[idx - 1];
            //    }
            //}
            //else if (e.KeyCode == Keys.Down)
            //{
            //    int idx = listBox1.SelectedIndex;
            //    if (idx == -1)
            //    {
            //        listBox1.SelectedItem = listBox1.Items[0];
            //    }
            //    else
            //    {
            //        if (idx == listBox1.Items.Count - 1)
            //        {
            //            listBox1.SelectedItem = listBox1.Items[0];
            //        }
            //        else
            //        {
            //            listBox1.SelectedItem = listBox1.Items[idx + 1];
            //        }
            //    }
            //    idx = listBox1.SelectedIndex;
            //}
           if (e.KeyCode == Keys.Enter)
            {
                listBox1.Visible = false;
                textErrorName.Focus();
            }
        }

        private void textErrorName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textErrorName.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                //if (textErrorName.Text != "")
                //{
                    listBox1.Focus();

                    //listBox1.SelectedItem = listBox1.Items[0];
                //}
            }
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            this.textErrorName.Text = this.listBox1.SelectedItem.ToString();
            this.listBox1.Visible = false;
        }
    }
}
