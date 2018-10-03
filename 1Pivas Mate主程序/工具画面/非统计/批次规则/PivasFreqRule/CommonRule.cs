using System;
using System.Data;
using System.Windows.Forms;

namespace PivasFreqRule
{
    public partial class CommonRule : UserControl
    {
        public string RuleCode;
        seldb sel = new seldb();
        DataTable dt;
        updatedb update;
        public CommonRule()
        {
            InitializeComponent();
        }
        public CommonRule(string rulecode)
        {
            RuleCode = rulecode;
            InitializeComponent();
        }

        private void CommonRule_Load(object sender, EventArgs e)
        {
            WardShow();
             //DataShow();
        }

        private void DataShow()
        {
            if (Comb_Ward.Text.ToString().Trim().Equals("默认_一般规则"))
            {
                But_UseAll.Visible = true;
                Check_OnlyInse.Visible = true;
                But_Default.Visible = false;
            }
            else
            {
                But_UseAll.Visible = false;
                Check_OnlyInse.Visible = false;
                But_Default.Visible = true;
            }

            dt = new DataTable();
            dt = sel.getDFreq().Tables[0];
            panel_DFreq.Controls.Clear();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UDFreq udFreq = new UDFreq();
                    udFreq.Parent = this;
                    udFreq.rowDFReq(dt.Rows[i]);
                    //.rowDFReq(dt.Rows[i]);
                    udFreq.Top = 33 * i;
                    udFreq.Name = i.ToString();
                    panel_DFreq.Controls.Add(udFreq);

                }
                rowFReqRule(dt.Rows[0]["FreqCode"].ToString(), dt.Rows[0]["FreqName"].ToString());
            }
        }


        /// <summary>
        /// 2014-2-10 查询病区
        /// </summary>
        public void WardShow()
        {
            // Comb_Ward.Items.Add("默认_一般规则");
            DataSet ds = sel.getWard();
            DataRow dr = ds.Tables[0].NewRow();
            dr[0] = "";
            dr[1] = "默认_一般规则";
            ds.Tables[0].Rows.InsertAt(dr, 0);
            this.Comb_Ward.DataSource = ds.Tables[0];
            this.Comb_Ward.DisplayMember = "WardSimName";
            this.Comb_Ward.ValueMember = "WardCode";
           // this.Comb_Ward.SelectedValue = "0"; //在此处选择0值，即可显示请选择。
           // this.Comb_Ward.SelectedIndex = 0;
            Comb_Ward_SelectionChangeCommitted(null,null);
        }

        public void rowFReqRule(string code, string name)
        {

           
            dt = new DataTable();

            panel_Rule.Controls.Clear();

            DataSet ds = new DataSet();
            if (Comb_Ward.Text.Equals("默认_一般规则"))
            {
                ds = sel.getFreqRule(code);
            }
            else
            {
                ds = sel.getFreqRule(Comb_Ward.SelectedValue.ToString(), code);
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                row(code);
            }
            else
            {

                if (sel.getDFreq2(code).Tables[0].Rows[0][1].ToString() != "")
                {
                    int daytime = Convert.ToInt32(sel.getDFreq2(code).Tables[0].Rows[0][1]);
                    for (int i = 1; i < daytime + 1; i++)
                    {
                        string subcode = code + i;
                        string subname = name + i;
                        update = new updatedb();
                        update.insert(code, subcode, subname, Comb_Ward.SelectedValue.ToString().ToString());
                    }
                    row(code);
                }
            }

        }


        public void row(string code)
        {
            dt = new DataTable();
            if (Comb_Ward.Text.Equals("默认_一般规则"))
            {
                dt = sel.getFreqRule(code).Tables[0];
              }
            else
            {
                dt = sel.getFreqRule(Comb_Ward.SelectedValue.ToString().Trim(), code).Tables[0];
        
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                UDFreqRule udFreqRule = new UDFreqRule(); 
                if (Comb_Ward.Text.Equals("默认_一般规则"))
                {
                    udFreqRule.rowFreqRule(dt.Rows[i], "");
                }
                else
                {
                    udFreqRule.rowFreqRule(dt.Rows[i], Comb_Ward.SelectedValue.ToString());
                }
                udFreqRule.Top = 33 * i;
                udFreqRule.Name = i.ToString();
                panel_Rule.Controls.Add(udFreqRule);
            }
        }

      

        private void But_UseAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认应用到全部病区？", "确认", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;
            But_UseAll.Text= "正在应用中...";
            But_UseAll.Enabled = false;
            UseAll();
            But_UseAll.Enabled = true;
            But_UseAll.Text = "应用到所有病区";
        }

        /// <summary>
        /// 复制数据到detail表
        /// </summary>
        public void UseAll()
        {
            update = new updatedb();
            DataSet ds=sel.getWard();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (Check_OnlyInse.Checked)
                {
                    //插入增量数据
                    for (int i = 0; i < ds.Tables[0].Rows.Count - 1; i++)
                    {
                        //DataSet ds=.(Comb_Ward.DataSource);
                         update.insert(ds.Tables[0].Rows[i]["WardCode"].ToString(),1);
                    }
                }
                else
                {
                    //删除detail所有数据，并复制到detail表里
                    update.deleteFreqRuleDetail();
                    for (int i = 0; i < Comb_Ward.Items.Count - 1; i++)
                    {

                        update.insert(ds.Tables[0].Rows[i]["WardCode"].ToString(), 0);
                    }
                }
            }
        }

        private void But_Default_Click(object sender, EventArgs e)
        {
            update = new updatedb();
            But_Default.Text = "正在应用中...";
            But_Default.Enabled = false;
            update.insert(Comb_Ward.SelectedValue.ToString());
            //UseAll();
            But_Default.Enabled = true;
            But_Default.Text = "设置为默认";
        }

        private void Comb_Ward_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataShow();
        }

       

        
    }
}
