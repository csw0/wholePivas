using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DWardManage
{
    /// <summary>
    /// 
    /// </summary>
    public partial class rowDWard : UserControl
    {
        public rowDWard()
        {
            InitializeComponent();
        }

        public static string  i ;
        seldb seldb = new seldb();
        public static string  k;
        bool iskf = true;

        /// <summary>
        /// 控件赋值
        /// </summary>
        /// <param name="row"></param>
        /// <param name="isalter"></param>
        /// <param name="k"></param>
        public void add(DataRow row,bool isalter,string  k)
        {
            //bool iskf = true;
            label1.Text = row["WardCode"].ToString();
            label2.Text = row["WardName"].ToString();
            label3.Text = row["WardSeqNo"].ToString();
            label4.Text = row["WardSimName"].ToString();
            label5.Text = row["WardArea"].ToString();
            textBox1.Text = row["Spellcode"].ToString();

            label8.Text = k.ToString();
     
           
            if (row["IsOpen"].ToString() =="True")
            {
                panel1.BackgroundImage = (Image)DWardManage.Properties.Resources.ResourceManager.GetObject("勾1");
                iskf = true; 
            }
            else
            {
                panel1.BackgroundImage = (Image)DWardManage.Properties.Resources.ResourceManager.GetObject("不选");
                iskf = false;
            }
            if (isalter == true) 
            {
                label3.Click+=new EventHandler(label3_Click);
                label4.Click += new EventHandler(label4_Click);
                label5.Click += new EventHandler(label5_Click);
                panel1.Click += new EventHandler(panel1_Click);
                label3.BackColor = Color.FromArgb(224, 224, 224);
                label4.BackColor = Color.FromArgb(224, 224, 224);
                label5.BackColor = Color.FromArgb(224, 224, 224);

            }         
        }

        /// <summary>
        /// 排序号修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label3_Click(object sender, EventArgs e) 
        {
            Point p = new Point(Control.MousePosition.X, Control.MousePosition.Y);
            UpdatePxh update = new UpdatePxh();
            i = label1.Text.ToString();
            k = label8.Text;
            update.Location = p;
            update.ShowDialog(this);
        }

        /// <summary>
        /// 简称更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label4_Click(object sender, EventArgs e)
        {
            Point p = new Point(Control.MousePosition.X, Control.MousePosition.Y);
            UpdateJC update = new UpdateJC();
            i = label1.Text.ToString();
            k = label8.Text;
            update.Location = p;
            update.ShowDialog(this);
        }

        /// <summary>
        /// 病区更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label5_Click(object sender, EventArgs e)
        {
            Point p = new Point(Control.MousePosition.X, Control.MousePosition.Y);
            UpdateFZ update = new UpdateFZ();
            i = label1.Text.ToString();
            k = label8.Text;
            update.Location = p;
            update.ShowDialog(this);
        }

        /// <summary>
        /// 病区是否开放勾选按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Click(object sender, EventArgs e) 
        {
            if (iskf == true)
            {
                panel1.BackgroundImage = (Image)DWardManage.Properties.Resources.ResourceManager.GetObject("不选");
                iskf = false;
                updatedb update = new updatedb();
                update.updatkf(iskf, label1.Text.ToString());
            }
            else
            {
                panel1.BackgroundImage = (Image)DWardManage.Properties.Resources.ResourceManager.GetObject("勾1");
                iskf = true;
                updatedb update = new updatedb();
                update.updatkf(iskf, label1.Text.ToString());
            }  
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            textBox1.ReadOnly = false;
            textBox1.BackColor = Color.FromArgb(224, 224, 224);
            textBox1.Text = textBox1.Text.Trim();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (!textBox1.ReadOnly)
            {
                textBox1.ReadOnly = true;
                updatedb update = new updatedb();
                update.updatSC(textBox1.Text.Trim().ToUpper(), label1.Text);
                textBox1.BackColor = Color.FromArgb(232, 239, 246);
                textBox1.Text = textBox1.Text.Trim().ToUpper();
            }
        }
          
    }
}
