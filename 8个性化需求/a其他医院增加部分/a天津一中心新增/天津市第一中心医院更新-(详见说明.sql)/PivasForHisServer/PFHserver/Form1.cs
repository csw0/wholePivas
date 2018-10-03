using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PIVAsDBhelp;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace PFHserver
{
    public partial class Form1 : Form
    {
        DB_Help db = new DB_Help();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.notifyIcon1.Visible = true;
                this.Hide();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                this.notifyIcon1.Visible = true;
                this.Hide();
                return;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.notifyIcon1.Visible = false;
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Focus();
        }

        private void xsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void tcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //listview1Init();

            //string sql = "EXEC p_baxter_pivas_pivano ";
            //try
            //{
            //    db.GetPIVAsDB(sql);
            //}
            //catch (Exception ex)
            //{  }
            //StringBuilder stringBuilder1 = new StringBuilder();
            //GetPrivateProfileString("Ward", "WardCode", "", stringBuilder1, 255, ".\\IMEQPIVAs.ini");
            //string wardcode = stringBuilder1.ToString();
            DB_Help db = new DB_Help();
            try
            {
                string sql = "SELECT * FROM IVRecord WHERE Remark6 LIKE '%发药计费成功%' AND PrinterName IS NOT NULL ";
                DataSet ds = db.GetPIVAsDB(sql);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    //if (this.WindowState == FormWindowState.Minimized)
                    //{
                    //    this.Show();
                    //    this.WindowState = FormWindowState.Normal;
                    //    Notice();
                    //}
                    //else
                    //    return;
                    db.GetPIVAsDB("UPDATE IVRecord SET Remark3 = 16, IVStatus = 13 WHERE Remark6 LIKE '%发药计费成功%' AND PrinterName IS NOT NULL AND DATEDIFF(DD, InsertDT, GETDATE()) = 0");
                }
            }
            catch (Exception ex) { }
            //syntoset();
        }

        private void listview1Init()
        {

            listView1.View = View.Details;
            listView1.GridLines = false;
            listView1.FullRowSelect = true;

            ColumnHeader header1 = new ColumnHeader();
            header1.Width = 240;

            listView1.Columns.Add(header1);
            

            ImageList imagelist = new ImageList();
            imagelist.ImageSize = new Size(1, 20);
            listView1.SmallImageList = imagelist;

            ListViewItem lv1 = new ListViewItem("处理时间: " + System.DateTime.Now);
            
            lv1.ForeColor = Color.Blue;
            this.listView1.Items.Add(lv1);
        }
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string Database, string key, string val, string filePath);
        //public string DataBse = System.Configuration.ConfigurationManager.AppSettings["Databse"].ToString();
        public int SetDB(string sql)
        {
            StringBuilder stringBuilder1 = new StringBuilder();
            GetPrivateProfileString("database", "db", "", stringBuilder1, 255, ".\\IMEQPIVAsHIS.ini");
            string DataBse = stringBuilder1.ToString();
            try
            {
                SqlConnection conn1 = new SqlConnection(DataBse);
                conn1.Open();
                SqlCommand cmd = new SqlCommand(sql, conn1);
                int i = cmd.ExecuteNonQuery();
                conn1.Close();
                return i;
            }
            catch
            {
                return 0;
            }
        }

        private void Notice()
        {
            StringBuilder stringBuilder1 = new StringBuilder();
            GetPrivateProfileString("Ward", "WardCode", "", stringBuilder1, 255, ".\\IMEQPIVAs.ini");
            string wardcode = stringBuilder1.ToString();
            this.richTextBox1.Text = "";
            DB_Help db = new DB_Help();
            string aa = "";
            try
            {
                string sql = "SELECT DrugName, Discription FROM NoticeSet ns INNER JOIN Notice n ON ns.DrugCode = n.DrugCode  WHERE ns.WardCode = '" + wardcode + "' AND ns.status = 0";
                DataSet ds = db.GetPIVAsDB(sql);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        aa += ds.Tables[0].Rows[i][0].ToString() + "_" + ds.Tables[0].Rows[i][1].ToString() + "\r\n";
                    }
                }
            }
            catch (Exception e) { }
            this.richTextBox1.Text = aa;
        }

        private void hadLook()
        {
            StringBuilder stringBuilder1 = new StringBuilder();
            GetPrivateProfileString("Ward", "WardCode", "", stringBuilder1, 255, ".\\IMEQPIVAs.ini");
            string wardcode = stringBuilder1.ToString();
            DB_Help db = new DB_Help();
            try
            {
                string sql1 = "SELECT ns.DrugCode FROM NoticeSet ns INNER JOIN Notice n ON ns.DrugCode = n.DrugCode  WHERE ns.WardCode = '" + wardcode + "' AND ns.status = 0";
                DataSet dataSet = db.GetPIVAsDB(sql1);
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    for (int m = 0; m < dataSet.Tables[0].Rows.Count; m++)
                    {
                        string sql2 = "UPDATE NoticeSet SET status = 1 WHERE WardCode = '" + wardcode + "' AND DrugCode = '" + dataSet.Tables[0].Rows[m][0].ToString() + "'";
                        DataSet ds = db.GetPIVAsDB(sql2);
                    }
                }

            }
            catch (Exception e) { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hadLook();
            this.WindowState = FormWindowState.Minimized;
        }

        private void syntoset()
        {
            DB_Help db = new DB_Help();
            try
            {
                string sql = "SELECT * FROM [IVRecordUpdateWait] WHERE CenterAct = 0";
                string sql2 = "update IVRecord set PackAdvance=1,PackID='1',PackTime=getdate() where LabelNo in (SELECT WardLabelNo FROM [IVRecordUpdateWait] WHERE CenterAct = 0)";
                string sql3 = "update [IVRecordUpdateWait] set CenterAct=(select isnull(i.PackAdvance,0) from IVRecord i where i.LabelNo=WardLabelNo) ,CenterEmployeeID=(select i.PackID from IVRecord i where i.LabelNo=WardLabelNo) ,CenterInsertDT=GETDATE() "+
                              "where WardLabelNo in (SELECT WardLabelNo FROM [IVRecordUpdateWait] WHERE CenterAct = 0) and WardAct='0'";
                DataSet dataSet = db.GetPIVAsDB(sql);
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    db.GetPIVAsDB(sql2);
                    db.GetPIVAsDB(sql3);
                }
            }
            catch (Exception e) { }
        }
    }


}
