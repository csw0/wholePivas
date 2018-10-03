using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DFregManage;
using PIVAsCommon.Helper;
using PivasLimitDES;

namespace PivasFreqRule
{
    public partial class DFreg : UserControl
    {
        public DFreg()
        {
            InitializeComponent();
        }

         public DFreg(string ID)
        {
            InitializeComponent();
            UserID = ID;
        }


        public delegate void DelegateChangeTextS();

        //public event DelegateChangeTextS ChangeTextVal;

        DB_Help db = new DB_Help();    
        seldb sel = new seldb();
        updatedeletedb update = new updatedeletedb();
        string codeid;
        DataTable dt;
        DataTable dt1;
        bool isalter = true;
        private string UserID = string.Empty;

        private void PivasDFeg_Load(object sender, EventArgs e)
        {
            if (UserID == null || UserID == "")
            {
                UserID = "00000000000000000000000000000000000000000000000";
            }

            if (GetPivasLimit.Instance.Limit(UserID, "PivasDFreg"))
            {
                addcontrols();
                this.ActiveControl = this.panel2;
            }
            else
            {
                //MessageBox.Show("您没有操作权限，请与管理员联系！");
                // this.Dispose();
            }

     
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
        /// <summary>
        /// 加载界面
        /// </summary>
        private void addcontrols() 
        {
            dt = new DataTable();
            dt = sel.getDFreg().Tables[0];
            showrow(dt);
        }

        private void showrow(DataTable dt) 
        {
            for (int i = 0; i < dt.Rows.Count; i++) 
            {
                rowDFreg row = new rowDFreg();
                row.Parent =this;
                row.add(dt.Rows[i], isalter, i.ToString());
                row.Top = i * 33;
                panel2.Controls.Add(row);   
            }
        }

        /// <summary>
        /// 刷新界面
        /// </summary>
        public void refresh() 
        {
            panel2.Controls.Clear();
            addcontrols();
            this.ActiveControl = this.panel2;
        }

        /// <summary>
        /// 新增用法按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            addDFreg add=new addDFreg();

            add.ChangeTextVal +=new addDFreg.DelegateChangeTextValS(refresh);
            add.ShowDialog(this);
        }

        ///// <summary>
        ///// 修改按钮事件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    if (isalter == false)
        //    {
        //        isalter = true;
        //        //label_modify.Text= "完成";
        //        refresh();
        //    }
        //    else 
        //    {
        //        isalter = false;
        //       // label_modify.Text = "修改";
        //        refresh();
        //    }
        //}

        /// <summary>
        /// 更新批次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_Click(object sender, EventArgs e)
        {
            StringBuilder mrg=new StringBuilder();
            dt=sel.getDFreg().Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++) 
            {
                checkSubRule(i);
                string[] time = dt.Rows[i][4].ToString().Split('_');
                if (time.Length > 0)
                {
                    for (int j = 0; j < time.Length; j++)
                    {
                        if (sel.getOrderID(time[j]).Tables[0].Rows.Count > 0)
                        {
                            string order = sel.getOrderID(time[j]).Tables[0].Rows[0][0].ToString();
                            codeid = dt.Rows[i][0].ToString() + (j + 1);
                            update.updateFreqRule(time[j], order, dt.Rows[i][0].ToString(), codeid);
                        }
                        else
                        {
                            mrg.Append("找不到" + time[j] + "的批次" + "\n");
                        }
                    }
                }
            }
            MessageBox.Show("更新成功");
            if (mrg.ToString() != "")
            {
                MessageBox.Show(mrg.ToString(),"请到时间规则维护");
            }
        }


        /// <summary>
        /// 核对FreqSubRule并更新
        /// </summary>
        /// <param name="i"></param>
        private void checkSubRule(int i) 
        {
            dt1 = sel.getCount(dt.Rows[i]["FreqCode"].ToString()).Tables[0];
            int k = Convert.ToInt32(dt.Rows[i]["TimesOfDay"]) - Convert.ToInt32(dt1.Rows[0][0]);
            if (k > 0)
            {
                for (int j = 1; j <= Math.Abs(k); j++)
                {
                    codeid = dt.Rows[i][0].ToString() + (Convert.ToInt32(dt1.Rows[0][0]) + j);
                    update.intsertFreqRule(dt.Rows[i][0].ToString(), codeid);
                }
            }
            else if (k < 0)
            {
                for (int j = 0; j < Math.Abs(k); j++)
                {
                    codeid = dt.Rows[i][0].ToString() + (Convert.ToInt32(dt1.Rows[0][0]) - j);
                    update.deleteFreqRule(codeid);
                }
            }
        }
    }
}
