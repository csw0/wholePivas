using System;
using System.Windows.Forms;
using System.Text;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;
using PivasLimitDES;

namespace PrintBatchUpdate
{
    public partial class BatchUpdate : Form
    {
        private string LabelNO;
        private string UserID;
        private string Batch;
        DB_Help db = new DB_Help();
        private string Batchnum;//存放批次数字
        private string BatchStr;//存放批次数字后的字符
        private string BatchStr1;//存放批次数字前的字符
        private string oldBatch;//存放刚开始的批次
        private string IVStatus;
        string XiuGai = string.Empty;
        string Fs = string.Empty;
        string Dy = string.Empty;
        int  IVstatus = 0;



        private string reason = string.Empty;
        private string reasonDetail = string.Empty;
        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion
        private int a;   //计算listbox的宽度
        private  float Capacity;//容量
        public BatchUpdate(string labelno, string userid)
        {
            this.LabelNO = labelno;
            this.UserID = userid;
            InitializeComponent();
        }
        public string Return()
        {
            return Batch;
        }

        private void BatchUpdate_Load(object sender, EventArgs e)
        {

            if (GetPivasLimit.Instance.Limit(UserID, "PrintBatchUpdate"))
            {
                XiuGai = db.GetPivasAllSet("批次-修改-打印画面");
                if (XiuGai == "1")
                {
                    Fs = db.GetPivasAllSetValue2("批次-修改-打印画面");
                    Dy = db.GetPivasAllSetValue3("批次-修改-打印画面");
                }
                else
                {
                    Fs = "0";
                    Dy = "0";
                }
               
                DataSet Ds = db.GetPIVAsDB("select IVStatus from IVRecord where labelno = '" + LabelNO.Trim() + "'  ");

                if (Ds != null && Ds.Tables[0].Rows.Count > 0)
                {
                    IVstatus = int.Parse(Ds.Tables[0].Rows[0]["IVStatus"].ToString());
                }
                else
                {
                    MessageBox.Show("  瓶签号错误！！！ ");
                    this.Dispose();
                }

                if (XiuGai == "0")
                {
                    MessageBox.Show(" 打印画面瓶签不能修改批次，若要修改需要修改配置。（批次-修改-打印画面:value1）");
                    this.Dispose();
                }
                else if (Fs == "0" && XiuGai == "1")
                {
                    MessageBox.Show(" < 已发送 > 瓶签不能修改，若要修改需要修改配置。（批次-修改-打印画面:value2）");
                    this.Dispose();
                }
                else if (IVstatus >= 3 && Dy == "0" && XiuGai == "1")
                {
                    MessageBox.Show(" < 已打印 > 瓶签不能修改，若要修改需要修改配置。（批次-修改-打印画面:value3）");
                    this.Dispose();
                }
                else
                {
                    label1.Text = LabelNO;
                    addPici();
                    patientInfo();
                    newdgv1();
                    AddReason();
                    //listBox1.Visible = false;
                    textBox5.Visible = false;
                }
            }
            else
            {
                this.Dispose();
            }                 
        }
       

        /// <summary>
        /// 添加批次控件
        /// </summary>
        private void addPici()
        {
            StringBuilder str = new StringBuilder();
            str.Append(" select OrderID from DOrder ");
            str.Append(" where IsValid=1");
            str.Append(" order by OrderID");
            DataTable dt = db.GetPIVAsDB(str.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Button lb = new Button();
                    lb.Text = dt.Rows[i][0].ToString();
                    lb.Size = new Size(35, 30);
                    lb.Location = new Point(panel1.Left + i * 30, panel1.Top - panel1.Height + 20);
                    lb.Click += new EventHandler(buttenClick);
                    panel1.Controls.Add(lb);
                }
            }
        }
        /// <summary>
        /// 批次中的数字按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void buttenClick(object sender, EventArgs e)
        {

            Button bu = (Button)sender;
            Batchnum = bu.Text;
            foreach (Control o in panel1.Controls)
            {
                o.BackColor = Color.White;
            }
            bu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(126)))), ((int)(((byte)(253)))));
            label6.Text =BatchStr1+ Batchnum + BatchStr;

        }
        /// <summary>
        /// 点击#按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            button2.BackColor = Color.White;
            button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(126)))), ((int)(((byte)(253)))));
            BatchStr = "#";
            label6.Text =BatchStr1+ Batchnum + BatchStr;
        }
        /// <summary>
        /// 点击k按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.White;
            button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(126)))), ((int)(((byte)(253)))));
            BatchStr = "-K";
            label6.Text =BatchStr1+ Batchnum + BatchStr;
        }

       
        /// <summary>
        /// 刷新瓶签表
        /// </summary>
        private void newdgv1()
        {
            string labelNo1 = "";
            string labelNo2 = "";
            
            StringBuilder str = new StringBuilder();
            str.Append("select distinct LabelNo,Batch,ivd.DrugName,CONVERT(varchar(50), CONVERT(float, Dosage)) as 'Dosage',ivd.DosageUnit,IVStatus,TeamNumber ");
            str.Append(" from IVRecord left join IVRecordDetail ivd on ivd.IVRecordID=IVRecord.IVRecordID ");
            str.Append(" where PatCode=(select PatCode from IVRecord where LabelNo='");
            str.Append(LabelNO);
            str.Append("') and DATEDIFF(dd,(select InfusionDT from IVRecord where LabelNo='");
            str.Append(LabelNO);
            str.Append("'), InfusionDT)=0");
            str.Append(" order by TeamNumber,Batch,LabelNo");
            DataTable dt = db.GetPIVAsDB(str.ToString()).Tables[0];
            totleCapacity(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
             
                labelNo2 = dt.Rows[i][0].ToString();
                dgvDrugsInfo.Rows.Add(1);
                dgvDrugsInfo.Rows[0].Selected = false;
                if (i == 0)//第一行的情况，直接添加
                {

                    dgvDrugsInfo.Rows[i].Cells["Column1"].Value = dt.Rows[i]["LabelNo"].ToString();
                    dgvDrugsInfo.Rows[i].Cells["Column2"].Value = dt.Rows[i]["Batch"].ToString();
                    dgvDrugsInfo.Rows[i].Cells["Column3"].Value = dt.Rows[i]["DrugName"].ToString();
                    dgvDrugsInfo.Rows[i].Cells["Column4"].Value = dt.Rows[i]["Dosage"].ToString() + dt.Rows[i]["DosageUnit"].ToString();           
                }
                else
                {
                    if (labelNo1 == labelNo2)
                    {
                        dgvDrugsInfo.Rows[i].Cells["Column1"].Value="";
                        dgvDrugsInfo.Rows[i].Cells["Column2"].Value = "";
                        dgvDrugsInfo.Rows[i].Cells["Column3"].Value = dt.Rows[i]["DrugName"].ToString();
                        dgvDrugsInfo.Rows[i].Cells["Column4"].Value = dt.Rows[i]["Dosage"].ToString() + dt.Rows[i]["DosageUnit"].ToString();   
                    }
                    else
                    {
                        dgvDrugsInfo.Rows[i].Cells["Column1"].Value = dt.Rows[i]["LabelNo"].ToString();
                        dgvDrugsInfo.Rows[i].Cells["Column2"].Value = dt.Rows[i]["Batch"].ToString();
                        dgvDrugsInfo.Rows[i].Cells["Column3"].Value = dt.Rows[i]["DrugName"].ToString();
                        dgvDrugsInfo.Rows[i].Cells["Column4"].Value = dt.Rows[i]["Dosage"].ToString() + dt.Rows[i]["DosageUnit"].ToString();    
                       
                    }
                }                                  
                    if (dt.Rows[i][0].ToString() == LabelNO)
                    {
                        selectPici(dt.Rows[i]["Batch"].ToString());
                        Batch = dt.Rows[i]["Batch"].ToString();
                        oldBatch = dt.Rows[i]["Batch"].ToString();
                        label3.Text = dt.Rows[i]["Batch"].ToString();
                        label6.Text = dt.Rows[i]["Batch"].ToString();
                        dgvDrugsInfo.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
                        IVStatus = dt.Rows[i]["IVStatus"].ToString();
                    }
                    labelNo1 = labelNo2;            
            }
           

        }
        private int selectnum(string str)
        {
            int a;
            string num = null;
            foreach (char item in str)
            {
                if (item >= 48 && item <= 58)
                {
                    num += item;
                }
            }
            a = Convert.ToInt32(num);
            return a;
        }
        private void totleCapacity(DataTable dt )
        {
            int a, b;
            string labelText = null;
            a = Convert.ToInt32( dt.Rows[0]["TeamNumber"].ToString());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                b = Convert.ToInt32(dt.Rows[i]["TeamNumber"].ToString());
                if (a == b)
                {
                    if (dt.Rows[i]["DosageUnit"].ToString() == "ml")
                    {
                        float f = float.Parse(dt.Rows[i]["Dosage"].ToString());
                        if (f >=50)
                        {
                            Capacity += f;
                        }
                    }                   
                }
                else
                {
                    if (Capacity != 0)
                    {
                        labelText += a + "批: " + Capacity + "ml   ";
                    }
                    Capacity = 0;
                    if (dt.Rows[i]["DosageUnit"].ToString() == "ml")
                    {
                        float f = float.Parse(dt.Rows[i]["Dosage"].ToString());
                        if (f >= 50)
                        {
                            Capacity += f;
                        }
                    }      
                }
              
                if (i == dt.Rows.Count-1 && Capacity != 0.0)
                {
                    
                    labelText += b + "批: " + Capacity + "ml   ";
                    Capacity = 0;
                }            
                a = b;
            }
            label4.Text = labelText;
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {            
            this.DialogResult = DialogResult.OK;
            this.Dispose();           
        }

        /// <summary>
        /// 设置批次中各控件颜色
        /// </summary>
        /// <param name="strBatch"></param>
        private void selectPici(string strBatch)
        {
            string num = null;
            if (strBatch.Contains("L"))
            {
                label_L.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
                label_L.Visible = true;
                BatchStr1 = "L-";
            }
            else
            {
                label_L.Visible = false;
            }
            foreach (char item in strBatch)
            {            
                if (item >= 48 && item <= 58)
                {
                    num += item;
                }
                if (item=='K')
                {
                    button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(126)))), ((int)(((byte)(253)))));
                    BatchStr = "-K";
                }
                if (item=='#')
                {
                    button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(126)))), ((int)(((byte)(253)))));
                    BatchStr = "#";
                }
            }
            foreach (Control o in panel1.Controls)
            { 
                if(o.Text==num)
                o.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(126)))), ((int)(((byte)(253)))));
            }
            Batchnum = num;
        }
        /// <summary>
        /// 添加原因类
        /// </summary>
        private void AddReason()
        {
            string sql = " select distinct  Reason  from OrderChangeLog  where Reason like '%打印画面修改%'";
            DataSet ds = db.GetPIVAsDB(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string reason = ds.Tables[0].Rows[i]["Reason"].ToString();
                    reason = reason.Substring(0, reason.Length - 8);
                    comboBox1.Items.Add(reason);
                }
                
            }
            comboBox1.SelectedIndex = 0;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            if (comboBox1.Visible == true)
            {
                comboBox1.Visible = false;
                textBox5.Visible = true;
            }
            else
            {
                comboBox1.Visible = true;
                textBox5.Visible = false;
            }

        }

        /// <summary>
        /// 保存修改批次，并将修改保存到数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (label6.Text == label3.Text)
            {
                MessageBox.Show("您未修改批次");
                return;
            }
            
            if (textBox5.Visible==true &&( textBox5.Text == "" || textBox5.Text == "修改原因"))
            {
                MessageBox.Show("请填写修改原因");
                return;
            }
            
            if (comboBox1.Visible == true&&comboBox1.SelectedItem.ToString() == "请选择修改原因")
            {
                MessageBox.Show("请选择修改原因");
                return;
            }
            if (textBox6.Text != "原因说明" && textBox6.Text.ToString() != "")
            {
                reasonDetail = textBox6.Text;
            }
            else
            {
                reasonDetail = null; 
            }
            if (comboBox1.Visible == true)
            {
                reason = comboBox1.SelectedItem.ToString() + "_ 打印画面修改";
            }
            else
            {
                reason = textBox5.Text +"_ 打印画面修改";
            }
                Batch = BatchStr1 + Batchnum + BatchStr;
                insertDB();
                updateDB();
                this.DialogResult = DialogResult.OK;
                this.Dispose();
          
        }
        /// <summary>
        /// 更新数据库批次和修改规则
        /// </summary>
        private void updateDB()
        {

            StringBuilder str = new StringBuilder();
            string ss = "修改人：" + serchEmployee(UserID) + ",由" + oldBatch + "修改为" + Batch + ",修改时间为："+DateTime.Now.ToString();
            str.Append(" update IVRecord set ");
            str.Append(" batch='");
            str.Append(Batch);
            str.Append("', TeamNumber='");
            str.Append(Batchnum);
            str.Append("', BatchRule='");
            str.Append(ss);          
            str.Append("' where LabelNo='");
            str.Append(LabelNO);
            str.Append("'");
            db.SetPIVAsDB(str.ToString());
        }
       /// <summary>
       /// 查找修改人
       /// </summary>
       /// <param name="userID"></param>
       /// <returns></returns>
        private string  serchEmployee(string userID )
        {
            string username;
            StringBuilder str = new StringBuilder();
            str.Append("select DEmployeeName from DEmployee where DEmployeeID='");
            str.Append(userID);
            str.Append("'");
            DataTable dt = db.GetPIVAsDB(str.ToString()).Tables[0];
            username = dt.Rows[0][0].ToString();
            return username;
        }
        /// <summary>
        /// 更新批次修改记录表
        /// </summary>
        private void insertDB()
        {
            StringBuilder str = new StringBuilder();
            str.Append(" insert  into OrderChangelog(LabelNo,ChangeDT,DEmployeeID,DEmployeeName,old,new,IVStatus,Reason,ReasonDetail)");
            str.Append(" values('");
            str.Append(LabelNO);
            str.Append("',");
            str.Append("getdate()");
            str.Append(",'");
            str.Append(UserID);
            str.Append("','");
            str.Append(serchEmployee(UserID));
            str.Append("','");
            str.Append(oldBatch);
            str.Append("','");
            str.Append(Batch);
            str.Append("','");
            str.Append(IVStatus);
            str.Append("','");
            str.Append(reason);
            str.Append("','");
            str.Append(reasonDetail);
            str.Append("')");
            int a=db.SetPIVAsDB(str.ToString());
            if (a == -1)
            {
                MessageBox.Show("记录未插入成功");
                return;
            }
        }

      
        /// <summary>
        /// 病人信息，病区，床号，姓名，年龄，性别
        /// </summary>
        private void patientInfo()
        {
            StringBuilder str = new StringBuilder();
            str.Append("select DWard.WardSimName,IVRecord.PatName,IVRecord.BedNo,p.Age,IVRecord.Sex,p.AgeSTR,p.Birthday from IVRecord ");
            str.Append("left join DWard on DWard.WardCode=IVRecord.WardCode ");
            str.Append(" left join Patient p on IVRecord.PatCode=p.PatCode ");
            str.Append("where LabelNo='");
            str.Append(LabelNO);
            str.Append("'");
            DataTable dt = db.GetPIVAsDB(str.ToString()).Tables[0];
         
           if (dt.Rows.Count > 0)
            {
                string a = dt.Rows[0]["WardSimName"].ToString() + "(" + dt.Rows[0]["BedNo"].ToString() + "床)";
                int b = a.Length;
                if (a.Length < 12)
                {
                    textBox4.Text = a;
                }
                else
                {
                    textBox4.Text = a.Substring(0, 10) + "\r\n" + a.Substring(10);
                }
                textBox1.Text = dt.Rows[0]["PatName"].ToString();

                textBox2.Text = dt.Rows[0]["Age"].ToString() + dt.Rows[0]["AgeSTR"].ToString();
               
                if (dt.Rows[0]["Sex"].ToString().Trim() == "1")
                {
                    textBox3.Text = "男";
                }
                else if (dt.Rows[0]["Sex"].ToString().Trim() == "2")
                {
                    textBox3.Text = "女";
                }
                else
                {
                    textBox3.Text = "其他"; 
                }
            }
        }
    
       
        
        /// <summary>
        /// 计算含汉字的字符串长度
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int len(string str)
        {
            System.Text.ASCIIEncoding n = new System.Text.ASCIIEncoding();
            byte[] b = n.GetBytes(str);
            int length = 0;                          // l 为字符串的实际长度
            for (int i = 0; i <= b.Length - 1; i++)
            {
                if (b[i] == 63)             //判断是否为汉字或全脚符号
                {
                    length++;
                }
                length++;
            }
            return length;
        }
       
        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "修改原因")
            {
                textBox5.Text = "";
                textBox5.ForeColor = Color.Black;

            }

        }
        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = "修改原因";
                textBox5.ForeColor = Color.Gray;

            }
        }
        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (textBox6.Text == "原因说明")
            {
                textBox6.Text = "";
                textBox6.ForeColor = Color.Black;

            }

        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                textBox6.Text = "原因说明";
                textBox6.ForeColor = Color.Gray;

            }


        }

        #region 窗体移动代码
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void dgvDrugsInfo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void label3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void label6_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void label5_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void label4_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        #endregion

     
      

    




    }
}
