using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;
using PivasLimitDES;

namespace DWardChange
{
    public partial class Form1 : Form
    {
        DB_Help DB = new DB_Help();
        SQL sql = new SQL();
        string Wards = string.Empty;
        private string UserID = string.Empty;
        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion

        public Form1()
        {
            InitializeComponent();
            UserID = "";
        }
        public Form1(string ID)
        {
            InitializeComponent();
            UserID = ID;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           if (UserID == null || UserID =="")
            {
                MessageBox.Show("  请从正常途径登录本画面！ ");
                this.Dispose();
            }
           else if (GetPivasLimit.Instance.Limit(UserID, "DWardChange"))
           {
               ShowlistBox1();
           }
           else
           { this.Dispose(); }
        }
        private void ShowlistBox1()
        {
            listBox1.Controls.Clear();
            DataTable dt = new DataTable();
            dt = DB.GetPIVAsDB(sql.SelectOLdDward()).Tables[0];
            if (dt != null || dt.Rows.Count != 0)
            {
                listBox1.ValueMember = "WardCode";
                listBox1.DisplayMember = "WardName";
                listBox1.DataSource = dt;
               
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox1.Text =((DataRowView)(this.listBox1.SelectedItem))["WardName"].ToString();

            Wards="'"+listBox1.SelectedValue.ToString()+"'";
          
            ShowGroup1();
            ShowGroup2();
            textBox2.Clear();
        }

        private void ShowGroup1()
        {
            listBox2.Controls.Clear();
            DataSet ds = new DataSet();
            ds = DB.GetPIVAsDB(sql.SelectNewDward(groupBox1.Text));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                button4.Enabled = true;

            }
            else
            {
                button4.Enabled = false;
                groupBox2.Text = "不存在新病区名，请添加";
                groupBox2.ForeColor = System.Drawing.Color.Red;
                checkBox1.ForeColor = System.Drawing.Color.Black;
                button3.ForeColor = System.Drawing.Color.Black;
                textBox1.Clear();
                textBox3.Clear();
                //ShowGroup2();
            }
            listBox2.DataSource = ds.Tables[0];
            listBox2.DisplayMember = "NewWardName";
            listBox2.ValueMember = "NewWardCode";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Wards = Wards + ",'" + ds.Tables[0].Rows[i][0].ToString() + "'";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = DB.GetPIVAsDB(sql.SelectNewCode(this.groupBox1.Text));
            if (ds == null || ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows[0][0].ToString() == "")
            {
                label1.Text = "DW" + this.listBox1.SelectedValue.ToString() + "000001";
            }
            else
            {
                int num = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                num++;
                label1.Text = "DW" + this.listBox1.SelectedValue.ToString() + num.ToString().PadLeft(6, '0');
            }
            if (textBox2.Text != "")
            {
                DataSet ds1 = new DataSet();
                ds1 = DB.GetPIVAsDB(sql.SelDWName(textBox2.Text));
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("新病区名重复！");
                    textBox2.Clear();
                }
                else
                {
                    DB.SetPIVAsDB(sql.AddNewDward(listBox1.SelectedValue.ToString(), groupBox1.Text, label1.Text, textBox2.Text));
                }
            }
            ShowGroup1();
            textBox2.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedValue != null)
            {
                DB.SetPIVAsDB(sql.DeleteNewDward(listBox2.SelectedValue.ToString()));
            }
            ShowGroup1();
            textBox2.Clear();
        }

        private void ShowGroup2()
        {
            string n = string.Empty;
            if (this.listBox2.SelectedValue != null)
            {
                if (this.listBox2.SelectedValue.ToString() == "System.Data.DataRowView")
                {
                    n = ((DataRowView)(this.listBox2.SelectedValue))["NewWardCode"].ToString();
                }
                else { n = this.listBox2.SelectedValue.ToString(); }
            }
            checkedListBox1.Controls.Clear();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
            DataSet ds = new DataSet();
            if (listBox2.SelectedValue != null && listBox2.SelectedValue.ToString() != "")
            {
                DataSet ds1 = new DataSet();
                ds1 = DB.GetPIVAsDB(sql.SelectOtherWDBeds(listBox1.SelectedValue.ToString(), n));

                string m = string.Empty;
                //if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                //{
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    if (ds1.Tables[0].Rows[i]["Beds"].ToString() != "")
                    {
                        if (m == "")
                        {
                            m = ds1.Tables[0].Rows[i]["Beds"].ToString();
                        }
                        else
                        {
                            m = m + ',' + ds1.Tables[0].Rows[i]["Beds"].ToString();
                        }
                    }
                }
                if (m == "") { m = "''"; }
                //}
                //else { m = "''"; }
                ds = DB.GetPIVAsDB(sql.SelectBeds(Wards, n, m, textBox1.Text, textBox3.Text));
            }
            else
            {
                ds = DB.GetPIVAsDB(sql.SelectBeds("''", "''", " ''", textBox1.Text, textBox3.Text));
            }
            if (ds.Tables[0] != null)
            {
                checkedListBox1.DataSource = ds.Tables[0];
                checkedListBox1.DisplayMember = "bedno";
                checkedListBox1.ValueMember = "bedno";
            }

            if (listBox2.SelectedValue != null && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
            {
                string[] s = ds.Tables[1].Rows[0][0].ToString().Split(new[] { "'" }, StringSplitOptions.None);
                foreach (string i in s)
                {
                    for (int j = 0; j < checkedListBox1.Items.Count; j++)
                    {
                        if (i == checkedListBox1.GetItemText(checkedListBox1.Items[j]).ToString())
                        {
                            checkedListBox1.SetItemChecked(j, true);
                        }
                    }

                }
                int m = 0;
                int c = 0;
                int d = ds.Tables[0].Rows.Count;
                if ((textBox1.Text != "" || textBox3.Text != "") && (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0))
                {
                    for (int i = 0; i < d; i++)
                    {
                        if (ds.Tables[0].Rows[i][0].ToString() == textBox1.Text)
                        {
                            m = i;
                        }   
                        if (textBox3.Text != "" && ds.Tables[0].Rows[i][0].ToString() == textBox3.Text)
                        {
                            c = i;
                        }
                    }
                    if ((textBox1.Text!=""&&textBox1.Text!=ds.Tables[0].Rows[0][0].ToString()&&m == 0)||(textBox3.Text!=""&&textBox3.Text!=ds.Tables[0].Rows[0][0].ToString()&&c==0))
                    {
                        MessageBox.Show("查不到所选项！");
                        textBox1.Text = "";
                        textBox3.Text = "";
                        m = 0; c = 0;

                    }       

                    if (m != 0 || c != 0 || textBox3.Text == ds.Tables[0].Rows[0][0].ToString())
                    {
                        for (int a = 0; a<d; a++)
                        {
                            if (a >= m && a <= c)
                            {
                                checkedListBox1.SetItemChecked(a, true);
                            }
                        }
                        textBox1.Text = "";
                        textBox3.Text = "";
                    }

                }
            }
        }
        

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox2.Text = ((DataRowView)(this.listBox2.SelectedItem))["NewWardName"].ToString();
            groupBox2.ForeColor = System.Drawing.Color.Black;
            checkBox1.Checked = false;
            textBox1.Clear();
            textBox3.Clear();
            ShowGroup2();
           
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count != checkedListBox1.Items.Count && checkedListBox1.CheckedItems.Count == 0)
            {
                checkBox1.CheckState = CheckState.Unchecked;

            }
            else if (checkedListBox1.CheckedItems.Count == checkedListBox1.Items.Count)
            {
                checkBox1.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox1.CheckState = CheckState.Indeterminate;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState==CheckState.Checked)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, true);
                }
            }
            else if (checkBox1.CheckState==CheckState.Unchecked)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string beds = string.Empty;
            string Bads = "''";
            for(int i=0;i<checkedListBox1.Items.Count;i++)
            {
                if (checkedListBox1.GetItemChecked(i) == true)
                {
                    if (beds == "")
                    {
                        beds = "''" + this.checkedListBox1.GetItemText(checkedListBox1.Items[i]).ToString() + "''";
                    }
                    else
                    {
                        beds = beds + ',' + "''" + this.checkedListBox1.GetItemText(checkedListBox1.Items[i]).ToString() + "''";
                    }
                }
                else
                {
                    if (Bads == "''")
                    {
                        Bads = "'" + this.checkedListBox1.GetItemText(checkedListBox1.Items[i]).ToString() + "'";
                    }
                    else
                    {
                        Bads = Bads + ',' + "'" + this.checkedListBox1.GetItemText(checkedListBox1.Items[i]).ToString() + "'";
                    }
                }
            }
            //if (beds != "" && beds != null)
            //{
                DB.SetPIVAsDB(sql.UpdateBeds(beds, listBox2.SelectedValue.ToString()));
                DB.SetPIVAsDB(sql.ReOldDward(Bads,listBox1.SelectedValue.ToString(),listBox2.SelectedValue.ToString()));
                MessageBox.Show("更新完成！");
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowGroup2();  
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);   
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void panel4_MouseLeave(object sender, EventArgs e)
        {
            this.panel4.BackgroundImage = null;
            this.panel4.BackColor = Color.Transparent;
            this.panel4.BackgroundImage = global::DWardChange.Properties.Resources._21;
        }

        private void panel4_MouseEnter(object sender, EventArgs e)
        {
            this.panel4.BackColor = Color.Red;
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        private void panel5_MouseEnter(object sender, EventArgs e)
        {
            this.panel5.BackgroundImage = global::DWardChange.Properties.Resources.最小化按下时按钮;
        }

        private void panel5_MouseLeave(object sender, EventArgs e)
        {
            this.panel5.BackgroundImage = null;
            this.panel5.BackColor = Color.Transparent;
            this.panel5.BackgroundImage = global::DWardChange.Properties.Resources.最小化;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "EXEC dbo.UpdateNewDWard_Prescription 3";
                DB.SetPIVAsDB(str);

                MessageBox.Show("同步成功");

            }
            catch
            {

            }     
        }
    }
}