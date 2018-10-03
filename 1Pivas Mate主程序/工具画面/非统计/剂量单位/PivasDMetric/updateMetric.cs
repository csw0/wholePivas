using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsDBhelp;

namespace DMetricManage
{
    public partial class updateMetric : Form
    {
        updatedb update=new updatedb();
        DB_Help db = new DB_Help();
        DataTable dt = new DataTable();
        private int a;
        private string newId;

        public updateMetric()
        {
            InitializeComponent();
            
        }
       

        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                FormMetric form1 = (FormMetric)this.Owner;
                update.updatdUnit(textBox1.Text, textBox2.Text, newId, rowMetric.Oldid, rowMetric.oldcode, rowMetric.oldname);
                //form1.refresh();
                form1.updateColumn(rowMetric.RName, textBox1.Text, textBox2.Text, comboBox1.Text);
                this.Close();    
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }

        private void updateDWard_Load(object sender, EventArgs e)
        {
            newId = rowMetric.Oldid;
            DataSet ds = new DataSet();       
            string str="select * from KD0100..DMetrologyUnit";
            string str1;
            try
            {
                textBox1.Text = rowMetric.oldcode;
                textBox2.Text = rowMetric.oldname;
                ds=db.GetPIVAsDB(str.ToString());
                dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str1 = dt.Rows[i]["ChineseName"].ToString() + "|" + dt.Rows[i]["EnglishName"].ToString();
                    comboBox1.Items.Add(str1);
                }
                comboBox1.Text = rowMetric.PName;
            }
            catch(Exception ex)
            {
                MessageBox.Show("错误：" + ex.Message);
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            a = comboBox1.SelectedIndex;
            newId = dt.Rows[a]["UnitID"].ToString();            
        }       
    }
}
