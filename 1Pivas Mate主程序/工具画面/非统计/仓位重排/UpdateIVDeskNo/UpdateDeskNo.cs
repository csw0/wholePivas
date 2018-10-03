using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace UpdateIVDeskNo
{
    public partial class UpdateDeskNo : Form
    {
        private string labelNo;//瓶签号
        private string UserId;//用户名
        DB_Help db = new DB_Help();
        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion

        private  string date="";
        public UpdateDeskNo()
        {
            InitializeComponent();
        }
        public UpdateDeskNo(string labelNo1, string user)
        {
            this.UserId = user;
            this.labelNo = labelNo1;
            InitializeComponent();
          
        }
        /// <summary>
        /// 权限
        /// </summary>
        /// <param name="DEmployeeID"></param>
        /// <param name="LimitName"></param>
        /// <returns></returns>
        public bool Limit(string DEmployeeID, string LimitName)
        {
            try
            {
                DataSet ds = new DataSet();
                string str = "select * from ManageLimit where DEMployeeID = " + DEmployeeID + " and LimitName = '" + LimitName + "'";
                ds = db.GetPIVAsDB(str);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                ds.Dispose();
                return false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void UpdateDeskNo_Load(object sender, EventArgs e)
        {
            if (UserId != null && Limit(UserId, "UpdateIVDeskNo"))
            {
                label1.Text = labelNo;
                getDeskDetail();
                getType();
            }
            else
            {
                MessageBox.Show("您没有操作权限，请与管理员联系！ " + UserId);
                this.DialogResult = DialogResult.No;
                this.Dispose();
            }
        }
        /// <summary>
        /// 得到当前瓶签的仓位号及药的种类
        /// </summary>
        private void  getType()
        {
            StringBuilder str = new StringBuilder();
            str.Append("select DeskNo, (Case DrugType when 1 then '普' when  2 then '抗' when 3 then '化' when 4  then '营' end) as 'Type'");
            str.Append("  from IVRecord left join Prescription p on p.PrescriptionID=IVRecord.PrescriptionID ");
            str.Append("where LabelNo='");
            str.Append(labelNo);
            str.Append("'");
            DataSet ds = db.GetPIVAsDB(str.ToString());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                label2.Text = ds.Tables[0].Rows[0]["Type"].ToString();
                label4.Text = ds.Tables[0].Rows[0]["DeskNo"].ToString();
                label6.Text = ds.Tables[0].Rows[0]["DeskNo"].ToString();
                StringBuilder str1 = new StringBuilder();
                str1.Append("select DeskNo from IVRecordDeskSet ");

                if (ds.Tables[0].Rows[0]["Type"].ToString() == "普")
                {
                    str1.Append(" where IsPTY='1' ");
                }
                else if (ds.Tables[0].Rows[0]["Type"].ToString() == "抗")
                {
                    str1.Append(" where IsKSS='1' ");
                }
                else if (ds.Tables[0].Rows[0]["Type"].ToString() == "化")
                {
                    str1.Append(" where IsHLY ='1'");
                }
                else if (ds.Tables[0].Rows[0]["Type"].ToString() == "营")
                {
                    str1.Append(" where IsYYY='1' ");
                }
                str1.Append(" and IsOpen='1'");

                DataSet ds1 = db.GetPIVAsDB(str1.ToString());
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        Button lb = new Button();
                        lb.Text = ds1.Tables[0].Rows[i]["DeskNo"].ToString();
                        lb.Size = new Size(72, 30);
                        //lb.Location = new Point(panel1.Left + i * 30, panel1.Top - panel1.Height + 20);
                        lb.Click += new EventHandler(buttenClick);
                        if (ds1.Tables[0].Rows[i]["DeskNo"].ToString() == label4.Text)
                        {
                            lb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(126)))), ((int)(((byte)(253)))));
                        }
                       ftp.Controls.Add(lb);
                    }
                }
            }

        }

        public void buttenClick(object sender, EventArgs e)
        {

            Button bu = (Button)sender;          
            foreach (Control o in ftp.Controls)
            {
                o.BackColor = Color.White;
            }
            bu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(126)))), ((int)(((byte)(253)))));
            label6.Text = bu.Text;

        }
     
        /// <summary>
        /// 等到各个仓位的详细情况
        /// </summary>
        private void getDeskDetail()
        {
            StringBuilder str = new StringBuilder();
            str.Append("select ivd.DeskNo,(select COUNT(*) from IVRecord where DeskNo=ivd.DeskNo and IVStatus<9) as 'Num' ");
            str.Append(",(select COUNT(*)from IVRecord where IVStatus>7 and DeskNo=ivd.DeskNo ) as 'Num1'  ");
            str.Append(",IsPTY,IsKSS,IsHLY,IsYYY,IsOpen from IVRecordDeskSet ivd  ");
            str.Append("left join IVRecord iv on ivd.DeskNo=iv.DeskNo  ");
            str.Append("group by IsPTY,IsKSS,IsHLY,IsYYY,IsOpen,ivd.DeskNo ");
            str.Append("order by ivd.DeskNo ");
            DataSet ds = db.GetPIVAsDB(str.ToString());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //ds.Tables[0].Columns.Add("Func",Type.GetType("System.String"));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string a="";
                    if (ds.Tables[0].Rows[i]["IsPTY"].ToString() == "True")
                    {
                        a += "普"+",";
                    }

                    if (ds.Tables[0].Rows[i]["IsKSS"].ToString() == "True")
                    {
                       a += "抗"+",";
                    }

                    if (ds.Tables[0].Rows[i]["IsHLY"].ToString() == "True")
                    {
                        a += "化"+",";
                    }

                    if (ds.Tables[0].Rows[i]["IsYYY"].ToString() == "True")
                    {
                        a += "营"+",";
                    }
                    dgv.Rows.Add(1);
                    if (ds.Tables[0].Rows[i]["IsOpen"].ToString() == "True")
                    {
                        dgv.Rows[i].Cells["IsOpen"].Value = "开启";
                    }
                    else
                    {
                        dgv.Rows[i].Cells["IsOpen"].Value = "关闭";
                        dgv.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                      //ds.Tables[0].Rows[i]["Func"] = a;
                    a = a.TrimEnd(',');
                      dgv.Rows[i].Cells["DeskNo"].Value=ds.Tables[0].Rows[i]["DeskNo"].ToString();
                       dgv.Rows[i].Cells["Function"].Value=a;
                       dgv.Rows[i].Cells["LabelNum"].Value=ds.Tables[0].Rows[i]["Num"].ToString();
                       dgv.Rows[i].Cells["Configure"].Value = ds.Tables[0].Rows[i]["Num1"].ToString();  
                }
                dgv.Rows[0].Selected = false;
                
            }
        }
        /// <summary>
        /// 更改仓位号，并记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
              if (label6.Text != label4.Text)
            {
                StringBuilder str = new StringBuilder();
                str.Append("update IVRecord set DeskNo='");
                str.Append(label6.Text);
                str.Append("' where LabelNo='");
                str.Append(labelNo);
                str.Append("'");

                int a = db.SetPIVAsDB(str.ToString());
                if (a == 1)
                {
                    date = label6.Text;
                    StringBuilder str1 = new StringBuilder();
                    str1.Append(" insert into DeskNoUpdateLog([DEmployeeID],[InsertDT],[LabelNo],[oldDeskNo],[newDeskNo]) ");
                    str1.Append("values('");
                    str1.Append(UserId);
                    str1.Append("',GETDATE(),'");
                    str1.Append(labelNo);
                    str1.Append("','");
                    str1.Append(label4.Text);
                    str1.Append("','");
                    str1.Append(label6.Text);
                    str1.Append("')");
                    int b = db.SetPIVAsDB(str1.ToString());
                    if (b == 1)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("插入记录表出错");
                        this.DialogResult = DialogResult.No;
                       this.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("修改仓位出错");
                    this.DialogResult = DialogResult.No;
                    this.Dispose();
                }

            }
            else
            {
                MessageBox.Show("未修改仓位");               
            }
        }

       public string Return()
        {
            return date;
          
        }
        private void button2_Click(object sender, EventArgs e)
       {
           this.DialogResult = DialogResult.No;
           this.Dispose();
         
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
    }
}
