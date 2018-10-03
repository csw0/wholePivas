using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using DeskNo.Properties;
using PIVAsCommon.Helper;

namespace DeskNo
{
    public partial class DeskNoSort : Form
    {
        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion

        DataTable cangWeiDetail = new DataTable();
       
        DB_Help db = new DB_Help();
       public string UserID;//用户名
       public string labelNo1;//瓶签号

        public DeskNoSort()
        {
            InitializeComponent();
        }
    
        public DeskNoSort(string User)
        {          
                this.UserID = User;
                if (UserID != null && Limit(UserID, "DeskNo"))
                {
                    InitializeComponent();
                }
                else
                {
                    MessageBox.Show("您没有操作权限，请与管理员联系！");
                    this.Dispose();
                }              
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // string labels = "(";
           // string sql = "select LabelNo from IVRecord where DeskNo is null";
           //DataSet ds = db.GetPIVAsDB(sql);
           //if (ds != null && ds.Tables[0].Rows.Count > 0)
           //{
           //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
           //    {
           //        labels += "'" + ds.Tables[0].Rows[i]["LabelNo"].ToString()+"',";
           //    }
           //}
           //labels = labels.TrimEnd(',');
           //labels += ")";
           db.SetPIVAsDB("exec bl_AutoIVDeskNo '','"+UserID+"'");
           
        }



       

        private void GetCangWeiNum()
        {
            string sql = "SELECT [IsOpen], [DeskNo],[IsPTY],[IsKSS],[IsHLY],[IsYYY] ,'确认'  as 'a' FROM [IVRecordDeskSet] order by DeskNo";
            DataSet ds = db.GetPIVAsDB(sql);
        
            dgv.DataSource = ds.Tables[0];

        }
     
        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetCangWeiNum();
            cbState();
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
        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
 
        /// <summary>
        /// 全部选中
        /// </summary>
        private void cbState()
        {
            int a=0 ;
          
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
               
                if (dgv.Rows[i].Cells["IsOpen"].Value.ToString() == "True")
                {
                   a=a+1;
                }
            }
            if (a == 0)
            {
                checkBox1.CheckState = CheckState.Unchecked;
            }
            else if (a == dgv.Rows.Count)
            {
                checkBox1.CheckState = CheckState.Checked;               
            }
            else
            {
                checkBox1.CheckState = CheckState.Indeterminate;
            }
        }

      /// <summary>
      /// 更新全部
      /// </summary>
        private void update()
        {
         
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                StringBuilder str = new StringBuilder();
                str.Append("update IVRecordDeskSet set ");
                if (dgv.Rows[i].Cells["IsPTY"].Value.ToString() == "True")
                {
                    str.Append("IsPTY='1',");
                }
                else
                {
                    str.Append("IsPTY='0',");
                }
                if (dgv.Rows[i].Cells["IsKSS"].Value.ToString() == "True")
                {
                    str.Append("IsKSS='1',");
                }
                else
                {
                    str.Append("IsKSS='0',");
                }
                if (dgv.Rows[i].Cells["IsHLY"].Value.ToString() == "True")
                {
                    str.Append("IsHLY='1',");
                }
                else
                {
                    str.Append("IsHLY='0',");
                }
                if (dgv.Rows[i].Cells["IsYYY"].Value.ToString() == "True")
                {
                    str.Append("IsYYY='1',");
                }
                else
                {
                    str.Append("IsYYY='0',");
                }
                if (dgv.Rows[i].Cells["IsOpen"].Value.ToString() == "True")
                {
                    str.Append("IsOpen='1'");
                }
                else
                {
                    str.Append("IsOpen='0'");
                }
                str.Append(" where DeskNo='");
                str.Append(dgv.Rows[i].Cells["DeskNo"].Value.ToString());
                str.Append("'");
               
                db.SetPIVAsDB(str.ToString());

            }
            GetCangWeiNum();
        }
   /// <summary>
   /// 更新选中
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell.Value.ToString() == "True")
            {
                dgv.CurrentCell.Value = false; 
            }
            else if (dgv.CurrentCell.Value.ToString() == "False")
            {
                dgv.CurrentCell.Value = true;
            }
            else
            {
                return; 
            }
            StringBuilder str = new StringBuilder();
            str.Append("update IVRecordDeskSet set ");
            if (dgv.CurrentRow.Cells["IsPTY"].Value.ToString() == "True")
            {
                str.Append("IsPTY='1',");
            }
            else
            {
                str.Append("IsPTY='0',");
            }
            if (dgv.CurrentRow.Cells["IsKSS"].Value.ToString() == "True")
            {
                str.Append("IsKSS='1',");
            }
            else
            {
                str.Append("IsKSS='0',");
            }
            if (dgv.CurrentRow.Cells["IsHLY"].Value.ToString() == "True")
            {
                str.Append("IsHLY='1',");
            }
            else
            {
                str.Append("IsHLY='0',");
            }
            if (dgv.CurrentRow.Cells["IsYYY"].Value.ToString() == "True")
            {
                str.Append("IsYYY='1',");
            }
            else
            {
                str.Append("IsYYY='0',");
            }
            if (dgv.CurrentRow.Cells["IsOpen"].Value.ToString() == "True")
            {
                str.Append("IsOpen='1'");
            }
            else
            {
                str.Append("IsOpen='0'");
            }
            str.Append(" where DeskNo='");
            str.Append(dgv.CurrentRow.Cells["DeskNo"].Value.ToString());
            str.Append("'");

            int a = db.SetPIVAsDB(str.ToString());
            //if (a == 1)
            //{
            //    MessageBox.Show("更新成功");
            //}
            //else
            //{
            //    MessageBox.Show("更新失败");
            //}          
            cbState();
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            label1.Image=Resources._2;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.Image = Resources._21;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddDeskNo ad = new AddDeskNo();
            DialogResult dd = ad.ShowDialog();
            if (dd == DialogResult.Cancel)
            {
                GetCangWeiNum();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StringBuilder str = new StringBuilder();
            str.Append("delete from IVRecordDeskSet where DeskNo='");
            str.Append(dgv.CurrentRow.Cells["DeskNo"].Value.ToString());
            str.Append("'");
            int a = db.SetPIVAsDB(str.ToString());
            //if (a == 1)
            //{
            //    MessageBox.Show("删除成功");

            //}
            //else
            //{
            //    MessageBox.Show("删除失败");
            //}
            GetCangWeiNum();
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {

            if (checkBox1.Checked)
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    dgv.Rows[i].Cells["IsOpen"].Value = true;
                    update();

                }
            }
            else
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    dgv.Rows[i].Cells["IsOpen"].Value = false;
                    update();

                }
            }
        }


    }
}
