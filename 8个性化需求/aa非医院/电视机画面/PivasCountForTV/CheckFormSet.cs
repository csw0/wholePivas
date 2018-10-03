using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PivasCountForTV
{
    public partial class CheckFormSet : Form
    {
        public CheckFormSet()
        {
            InitializeComponent();
        }
        public CheckFormSet(string batch,string interval,string gdinterval,string stayTime)
        {
            InitializeComponent();
            this.Batch = batch;
            this.interval = interval;
            this.GDinterval = gdinterval;
            this.stay = stayTime;
        }
        seldb sel = new seldb();
        PIVAsDBhelp.DB_Help db = new PIVAsDBhelp.DB_Help();
        DataTable dt = new DataTable();
        DataTable dt_cm = new DataTable();
        
        public delegate void childclose(string Batch,string interval,string GDinterval,string stay1);
        public event childclose closefather;
        string label_color;
        string CheckName="电视机配置";
        string Batch = string.Empty;
        string interval = string.Empty;
        string GDinterval = string.Empty;
        string stay = string.Empty;
        private void CheckFormSet_Load(object sender, EventArgs e)
        {
            dt=sel.getCheckFormSet().Tables[0];
        
            if (dt.Rows.Count > 0) 
            {
     
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Label lb = new Label();
                    lb.Text = dt.Rows[i][1].ToString();
                    if (dt.Rows[i]["Vesting"].ToString() == "False")
                    {
                        lb.Enabled = false;
                    }
                    else if (dt.Rows[i]["IsOpen"].ToString() == "True")
                    {
                        lb.Enabled = true;
                    }
                    lb.Top = 25 * i;
                    lb.Width = 140;
                    lb.Height = 25;
                    lb.AutoSize = false;
                    lb.TextAlign=System.Drawing.ContentAlignment.MiddleCenter;
                    panel1.Controls.Add(lb);
                    CheckBox cb = new CheckBox();
                    cb.Name = dt.Rows[i][1].ToString();
                    cb.AutoSize = true;
                    cb.Top = 25 * i+5;
                    cb.Left = 10;
                    if (dt.Rows[i]["Vesting"].ToString() == "False")
                    {
                        cb.Enabled = false;
                        

                    }
                    if (dt.Rows[i]["IsOpen"].ToString() == "True")
                    {
                        cb.Checked = true;
                    }
                    panel1.Controls.Add(cb);
                    
                }
              
            }
            ToFront();
            CheckClick();
            Bangding("电视机配置");
          
            BandingBatch();
            //cbBatch.Text = Batch == "全部" ? "<全部>" : Batch+"批";
            textBox1.Text = interval;
            textBox2.Text = this.GDinterval;
            textBox3.Text = this.stay;
        }

        /// <summary>
        /// 确定按钮，保存设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            foreach(Control c   in panel1.Controls )
            {
                if (c is CheckBox)
                {
                    sel.updateCheckFormSet(((CheckBox)c).Checked, c.Name);
                }
 
            }
            save();
            this.Close();
            
        }


        private void BandingBatch()
        {
            panel7.Controls.Clear();
            try{
            DataTable dt = new DataTable();
            dt = db.GetPIVAsDB("select (convert(varchar,orderid)+'批') as OrderName,convert(varchar,orderid) as orderid from dorder").Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CheckBox cb = new CheckBox();
                cb.Text = dt.Rows[i]["OrderName"].ToString();
                cb.Tag = dt.Rows[i]["orderid"].ToString();
                cb.Size = new Size(50, 20);
                cb.Click += new EventHandler(cb_Click);
                if (i > 3)
                {
                    cb.Location = new Point(55, 5 + (i-4) * 25);
                }
                else
                {
                    cb.Location = new Point(5, 5 + i * 25);
                }
                if (Batch.Contains(cb.Tag.ToString()))
                {
                    cb.Checked = true;
                }
                panel7.Controls.Add(cb);
            }
           
            }
            catch(Exception ex){
                MessageBox.Show("批次载入错误,请检查数据库连接！");
            }
            
        }
        private void cb_Click(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb.Checked)
            {
                cb.Checked = true;
            }
            else
            {
                cb.Checked = false;
            }
            GetBatch();

        }
        private void GetBatch()
        {
            Batch = string.Empty;
            foreach (CheckBox cb in panel7.Controls)
            {
                if (cb.Checked)
                {
                    Batch += cb.Tag.ToString()+",";
                }
            
            }
            Batch = Batch.TrimEnd(',');      
        }
    

        private void CheckFormSet_FormClosed(object sender, FormClosedEventArgs e)
        {
            closefather(Batch, textBox1.Text,textBox2.Text,textBox3.Text);
        }

        private void Bangding(string CheckName) 
        {
            dt_cm = sel.CheckMateId(CheckName).Tables[0];
            if (dt_cm.Rows.Count > 0)
            {
               // comboBox1.Text = dt_cm.Rows[0]["Content1"].ToString();
                comboBox2.Text = dt_cm.Rows[0]["Content2"].ToString();
                comboBox3.Text = dt_cm.Rows[0]["Content3"].ToString();
                comboBox4.Text = dt_cm.Rows[0]["Content4"].ToString();
                comboBox5.Text = dt_cm.Rows[0]["Content5"].ToString();
                comboBox6.Text = dt_cm.Rows[0]["Content6"].ToString();
                Color1.BackColor = ColorTranslator.FromHtml(dt_cm.Rows[0]["Color1"].ToString());
                Color2.BackColor = ColorTranslator.FromHtml(dt_cm.Rows[0]["Color2"].ToString());
                Color3.BackColor = ColorTranslator.FromHtml(dt_cm.Rows[0]["Color3"].ToString());
                Color4.BackColor = ColorTranslator.FromHtml(dt_cm.Rows[0]["Color4"].ToString());
                Color5.BackColor = ColorTranslator.FromHtml(dt_cm.Rows[0]["Color5"].ToString());
                Color6.BackColor = ColorTranslator.FromHtml(dt_cm.Rows[0]["Color6"].ToString());
                if (dt_cm.Rows[0]["NextDay"].ToString().Length >=5)
                {
                    comboBox7.Text = dt_cm.Rows[0]["NextDay"].ToString().Substring(0, 2);
                    comboBox8.Text = dt_cm.Rows[0]["NextDay"].ToString().Substring(3, 2);
                }
                else
                {
                    comboBox7.Text = "";
                    comboBox8.Text = "";
                }
            }
        }

        private void CheckClick() 
        {
            foreach (Control c in panel1.Controls)
            {
                if (c is Label)
                {
                    c.Click+=new EventHandler(c_Click);
                    c.MouseHover+=new EventHandler(c_MouseHover);
                    c.MouseLeave+=new EventHandler(c_MouseLeave);
                    
                }
                if (c.Text == "电视机配置") 
                {
                    c.BackColor = Color.White;
                }
                

            }
        }

        private void c_Click(object sender, EventArgs e)
        {
            
            save();
            Label lb = (Label)sender;
            foreach (Control c in panel1.Controls) 
            {
                if (c is CheckBox&&c.Name==lb.Text) 
                {
                    if (((CheckBox)c).Checked == true)
                    {
                        Bangding(lb.Text);
                        CheckName = lb.Text;
                        AllWhite();
                        lb.BackColor = Color.White;
                        panel4.Visible = true;
                    }
                    else 
                    {
                        panel4.Visible = false;
                        AllWhite();
                        lb.BackColor = Color.White;
                    }
                }
            }
           
           
           
        }
      
        private void Label_Color_Click(object sender, EventArgs e)
        {
            Label Label_UColor = (Label)sender;
            //BColor = Label_UColor.BackColor.R + "," + Label_UColor.BackColor.G + "," + Label_UColor.BackColor.B;
            //Label_Batch2.BackColor = System.Drawing.ColorTranslator.FromHtml(BColor);
            //Panel_Total2.BackColor = System.Drawing.ColorTranslator.FromHtml(BColor);   
            if (label_color == "Color1")
            {
                Color1.BackColor = Label_UColor.BackColor;
            }
            else if (label_color == "Color2") 
            {
                Color2.BackColor = Label_UColor.BackColor;
            }
            else if (label_color == "Color3")
            {
                Color3.BackColor = Label_UColor.BackColor;
            }
            else if (label_color == "Color4")
            {
                Color4.BackColor = Label_UColor.BackColor;
            }
            else if (label_color == "Color5")
            {
                Color5.BackColor = Label_UColor.BackColor;
            }
            else if(label_color == "Color6")
            {
                Color6.BackColor = Label_UColor.BackColor;
            }
            panel5.Visible = false;
        }

        private void color_click(object sender, EventArgs e) 
        {
            Button Label_Color = (Button)sender;
            label_color = Label_Color.Name;
            if (panel5.Visible == true)
            {
                panel5.Visible = false;
            }
            else 
            {
                panel5.Visible = true;
            }
            //ColorDialog color = new ColorDialog();
            //if (color.ShowDialog() == DialogResult.OK) 
            //{
            //    Label_Color.BackColor = color.Color;
            //}
            
        }
        /// <summary>
        /// 保存设置
        /// </summary>
        private void save() 
        {

            string color1=Color1.BackColor.R + "," + Color1.BackColor.G + "," + Color1.BackColor.B;
            string color2=Color2.BackColor.R + "," + Color2.BackColor.G + "," + Color2.BackColor.B;
            string color3=Color3.BackColor.R + "," + Color3.BackColor.G + "," + Color3.BackColor.B;
            string color4=Color4.BackColor.R + "," + Color4.BackColor.G + "," + Color4.BackColor.B;
            string color5=Color5.BackColor.R + "," + Color5.BackColor.G + "," + Color5.BackColor.B;
            string color6=Color6.BackColor.R + "," + Color6.BackColor.G + "," + Color6.BackColor.B;
            string NextDay=comboBox7.Text+":"+comboBox8.Text;
            sel.updateCombox(CheckName,comboBox1.Text,comboBox2.Text,comboBox3.Text,comboBox4.Text,comboBox5.Text,comboBox6.Text , color1, color2, color3, color4, color5,color6,NextDay);
              
            db.IniWriteValue("PivasTV", "RefreshItval", textBox1.Text);
            db.IniWriteValue("PivasTV", "GDItval", textBox2.Text);
            db.IniWriteValue("PivasTV", "TVBatch", this.Batch);
            db.IniWriteValue("PivasTV", "TVStay", textBox3.Text);
        }

        private void c_MouseHover(object sender, EventArgs e) 
        {
            Label pl = (Label)sender;
            if (pl.BackColor != Color.White) 
            {
                pl.BackColor = Color.FromArgb(192, 192, 255);
            }
        }
        private void c_MouseLeave(object sender, EventArgs e) 
        {
            Label pl = (Label)sender;
            if (pl.BackColor != Color.White)
            {
                pl.BackColor = Color.FromArgb(224, 224, 224);
            }
        }
        private void AllWhite() 
        {
            foreach (Control c in panel1.Controls)
            {
                if (c is Label)
                {
                    c.BackColor = Color.FromArgb(224, 224, 224);
                }
            }
        }

        private void ToFront() 
        {
            foreach(Control c in panel1.Controls )
            {
                if (c is CheckBox)
                {
                    c.BringToFront();
                }
            }
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

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "<空>")
            {
                comboBox3.Text = "<空>";
                comboBox4.Text = "<空>";
                comboBox5.Text = "<空>";
                comboBox6.Text = "<空>";
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
                comboBox5.Enabled = false;
                comboBox6.Enabled = false;
            }
            else 
            {

                comboBox3.Enabled = true;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text == "<空>")
            {
                comboBox4.Text = "<空>";
                comboBox5.Text = "<空>";
                comboBox6.Text = "<空>";
                comboBox4.Enabled = false;
                comboBox5.Enabled = false;
                comboBox6.Enabled = false;
            }
            else
            {
                comboBox4.Enabled = true;
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text == "<空>")
            {
                
                comboBox5.Text = "<空>";
                comboBox6.Text = "<空>";       
                comboBox5.Enabled = false;
                comboBox6.Enabled = false;
            }
            else
            { 
                comboBox5.Enabled = true;
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text == "<空>")
            {

                comboBox5.Text = "<空>";
                comboBox6.Text = "<空>";
              
                comboBox6.Enabled = false;
            }
            else
            {
                comboBox6.Enabled = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
     

     

        
        

  }
}
