using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PIVAsDBhelp;

namespace Notice
{
    public partial class DrugChange : Form
    {
        public DrugChange()
        {
            InitializeComponent();
            listview1Init();
        }

        string ccode = "";

        private void button1_Click(object sender, EventArgs e)
        {
            InsertNoticeSet();
            this.Dispose();
            this.Close();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.richTextBox1.Visible = true;
            try
            {
                DB_Help db = new DB_Help();
                DataSet ds = db.GetPIVAsDB("SELECT Discription FROM Notice WHERE DrugCode = '" + ccode + "' AND (Discription IS NOT NULL OR Discription <> '')");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                    this.richTextBox1.Text = ds.Tables[0].Rows[0]["Discription"].ToString();
                else
                    this.richTextBox1.Text = "(字数限制在250字之内....)";
            }
            catch (Exception ex) { }
            this.panel1.Visible = true;
        }

        private void listview1Init()
        {
            listView1.View = View.Details;
            listView1.GridLines = false;
            listView1.FullRowSelect = true;

            ColumnHeader header1 = new ColumnHeader();
            ColumnHeader header2 = new ColumnHeader();
            ColumnHeader header3 = new ColumnHeader();
            header1.TextAlign = HorizontalAlignment.Center;
            header2.TextAlign = HorizontalAlignment.Center;
            header3.TextAlign = HorizontalAlignment.Center;
            header1.Text = "药品编码";
            header2.Text = "药品名称";
            header3.Text = "通知内容";
            header1.Width = 100;
            header2.Width = 249;
            header3.Width = 400;
            listView1.Columns.Add(header1);
            listView1.Columns.Add(header2);
            listView1.Columns.Add(header3);

            ImageList imagelist = new ImageList();
            imagelist.ImageSize = new Size(1, 20);
            listView1.SmallImageList = imagelist;
            NoticeDrug();
        }

        private void NoticeDrug()
        {
            try
            {
                string sql = "SELECT DrugCode, DrugName, Discription FROM Notice";
                //sql = string.Format(sql, wardcode);
                DB_Help db = new DB_Help();
                DataSet ds = db.GetPIVAsDB(sql);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int a = 0; a < ds.Tables[0].Rows.Count; a++)
                    {
                        ListViewItem lv1 = new ListViewItem(ds.Tables[0].Rows[a]["DrugCode"].ToString().Trim());
                        lv1.SubItems.Add(ds.Tables[0].Rows[a]["DrugName"].ToString().Trim());
                        lv1.SubItems.Add(ds.Tables[0].Rows[a]["Discription"].ToString().Trim());
                        listView1.Items.Add(lv1);
                    }
                }
            }
            catch (Exception e) { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            getDrug();
            this.checkedListBox1.Visible = true;
            this.panel1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (true == this.richTextBox1.Visible)
            {
                UpdateNotice(ccode);
                this.richTextBox1.Visible = false;
            }
            else
            {
                InsertNotic();
                this.checkedListBox1.Visible = false;
            }
            this.panel1.Visible = false;
            this.listView1.Clear();
            listview1Init();
        }

        private void getDrug()
        {
            this.checkedListBox1.Items.Clear();
            this.checkedListBox1.Items.Add("全选/取消");
            try
            {
                DB_Help dbHelp = new DB_Help();
                using (DataSet dataSet = dbHelp.GetPIVAsDB(string.Format("SELECT DrugName FROM DDrug WHERE IsValid = 1 AND DrugCode NOT IN (SELECT DrugCode FROM Notice)")))
                {
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                        {
                            this.checkedListBox1.Items.Add(dataRow[0]);
                        }
                    }
                }
            }
            catch (Exception e) { }
        }

        private void InsertNotic()
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (this.checkedListBox1.GetItemChecked(i))
                {
                    string drugName = this.checkedListBox1.GetItemText(this.checkedListBox1.Items[i]);
                    try
                    {
                        DB_Help db = new DB_Help();
                        DataSet ds = db.GetPIVAsDB("SELECT DISTINCT DrugCode, DrugName FROM DDrug WHERE DrugName = '" + drugName + "'");
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            for (int a = 0; a < ds.Tables[0].Rows.Count; a++)
                            {
                                db.GetPIVAsDB("INSERT INTO Notice VALUES ('" + ds.Tables[0].Rows[a]["DrugCode"] + "','" + ds.Tables[0].Rows[a]["DrugName"] + "','')");
                            }
                        }
                    }
                    catch (Exception e) { }
                }
            }
        }

        private void UpdateNotice(string drugCode)
        {
            try
            {
                DB_Help db = new DB_Help();
                string mess = "";
                if ("(字数限制在250字之内....)" != this.richTextBox1.Text)
                    mess = this.richTextBox1.Text;
                else
                    mess = "";
                if ("" != mess)
                    db.GetPIVAsDB("UPDATE Notice SET Discription = '" + mess + "' WHERE DrugCode = '" + drugCode + "'");
            }
            catch (Exception e) { }
        }

        private void InsertNoticeSet()
        {
            try
            {
                DB_Help db = new DB_Help();
                using (DataSet ds = db.GetPIVAsDB("SELECT DrugCode FROM Notice "))
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //DB_Help dd = new DB_Help();
                        DataSet dst = db.GetPIVAsDB("SELECT wardCode FROM NoticeSet WHERE DrugCode = '" + ds.Tables[0].Rows[i]["DrugCode"].ToString() + "'");
                        if (dst != null && dst.Tables[0].Rows.Count > 0)
                        {
                            for (int m = 0; m < dst.Tables[0].Rows.Count; m++)
                            {
                                db.GetPIVAsDB(" IF EXISTS (SELECT wardCode FROM NoticeSet WHERE wardCode = '" + dst.Tables[0].Rows[m]["wardCode"].ToString() + "')" +
                                              "  UPDATE NoticeSet SET status = 1 WHERE wardCode = '" + dst.Tables[0].Rows[m]["wardCode"].ToString() + "' AND DrugCode = '" + ds.Tables[0].Rows[i]["DrugCode"].ToString() + "'  " +
                                              "ELSE " +
                                              "INSERT INTO NoticeSet VALUES ('" + dst.Tables[0].Rows[m]["wardCode"].ToString() + "', '" + ds.Tables[0].Rows[i]["DrugCode"].ToString() + "', 0)");
                            }
                        }
                        else
                        {
                            DataSet dd = db.GetPIVAsDB("SELECT wardCode FROM DWard WHERE IsOpen = 1");
                            for (int n = 0; n < dd.Tables[0].Rows.Count; n++)
                            {
                                db.GetPIVAsDB("INSERT INTO NoticeSet VALUES ('" + dd.Tables[0].Rows[n]["wardCode"].ToString() + "', '" + ds.Tables[0].Rows[i]["DrugCode"].ToString() + "', 0)");
                            }
                        }
                    }
                }
            }
            catch (Exception e) { }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (0 != e.Index) return;
            for (int i = 1; i < this.checkedListBox1.Items.Count; i++)
            {
                this.checkedListBox1.SetItemChecked(i, (CheckState.Checked != e.CurrentValue));
            }
        }

        private void richTextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.richTextBox1.Text == "(字数限制在250字之内....)")
                this.richTextBox1.Text = "";
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            ccode = this.listView1.SelectedItems[0].Text;
        }
    }
}
