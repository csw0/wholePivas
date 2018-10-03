using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Damage
{
    public partial class Form1 : Form
    {
        DB_Help DB = new DB_Help();
        string Drugcode = string.Empty;

        public Form1()
        {
            InitializeComponent();
            comb_zrr_load();
            comb_tbr_load();
            comb_reason_load();
        }

        //DataGridView 加载数据
        private void dgv_drug_loda()
        {
            string txtStr = txt_drug.Text.ToString();
            txtStr = txtStr.Replace(" ", "");
            StringBuilder sb = new StringBuilder();
            sb.Append(" select top 6 REPLACE(d.DrugName,' ','') DrugName ,d.DrugCode DrugCode from ddrug d ");
            sb.Append(" left join DDrugColor dc on d.DrugCode = dc.DrugCode ");
            sb.Append(" where d.IsValid = 1 ");
            sb.Append(" and ( d.SpellCode like '%" + txtStr + "%' ");
            sb.Append(" or d.DrugName like'%" + txtStr + "%' ) ");
            sb.Append(" order by d.DrugName,d.DrugCode ");
            DataSet ds = DB.GetPIVAsDB(sb.ToString());
            dgv_drug.DataSource = ds.Tables[0];
            dgv_drug.Columns[1].Visible = false;
        }

        //combobox spec 初始化数据
        private void comb_spec_load()
        {
            comb_spec.Text = "";
            string txtStr = txt_drug.Text.ToString();
            txtStr = txtStr.Replace(" ", "");
            StringBuilder sb = new StringBuilder();
            sb.Append(" select top 1 spec from DDrug where drugcode like '%" + Drugcode + "%' ");
            DataTable dt = DB.GetPIVAsDB(sb.ToString()).Tables[0];
            comb_spec.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
            comb_spec.DisplayMember = "spec";
            //comb_spec.ValueMember = dt.Rows[0].ToString();

            }
        }

        //combobox reason 初始化数据
        private void comb_reason_load()
        {
            comb_reason.Text = "";
            StringBuilder sb = new StringBuilder();
            sb.Append(" select reason from DamageReason ");
            DataTable dt = DB.GetPIVAsDB(sb.ToString()).Tables[0];
            comb_reason.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                comb_reason.DisplayMember = "reason";
                //comb_spec.ValueMember = dt.Rows[0].ToString();

            }
        }

        //combobox 责任人 初始化数据
        private void comb_zrr_load()
        {
            comb_zrr.Text = "";
            string txtStr = txt_drug.Text.ToString();
            txtStr = txtStr.Replace(" ", "");
            StringBuilder sb = new StringBuilder();
            sb.Append("select DEmployeeCode,DEmployeeName from DEmployee where type =1 ");
            DataTable dt = DB.GetPIVAsDB(sb.ToString()).Tables[0];
            comb_zrr.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                comb_zrr.DisplayMember = "DEmployeeName";
                //MessageBox.Show(comb_zrr.SelectedValue);
                comb_zrr.ValueMember = "DEmployeeCode";
                //comb_spec.ValueMember = dt.Rows[0].ToString();
            }
        }

        //combobox 填报人 初始化数据
        private void comb_tbr_load()
        {
            comb_tbr.Text = "";
            string txtStr = txt_drug.Text.ToString();
            txtStr = txtStr.Replace(" ", "");
            StringBuilder sb = new StringBuilder();
            sb.Append("select DEmployeeCode,DEmployeeName from DEmployee where type =1 ");
            DataTable dt = DB.GetPIVAsDB(sb.ToString()).Tables[0];
            comb_tbr.DataSource = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                comb_tbr.DisplayMember = "DEmployeeName";
                comb_tbr.ValueMember = "DEmployeeCode";
                //comb_spec.ValueMember = dt.Rows[0].ToString();
            }
        }

        //textbox 单价 初始化数据
        private void txt_price_load()
        {
            txt_price.Text = "";
            string txtStr = txt_drug.Text.ToString();
            txtStr = txtStr.Replace(" ", "");
            StringBuilder sb = new StringBuilder();
            sb.Append(" select top 1 Amount from DDrug where drugcode like '%" + Drugcode + "%' ");
            DataTable dt = DB.GetPIVAsDB(sb.ToString()).Tables[0];
            if(dt != null && dt.Rows.Count > 0)
            {
                txt_price.Text = dt.Rows[0]["Amount"].ToString().Trim();
            }
        }

        //textbox amount 初始化数据
        private void txt_amount_load()
        {
            txt_amount.Text = "";
            float result = Convert.ToSingle(this.txt_price.Text) * Convert.ToSingle(this.tex_count.Text);
            txt_amount.Text = result.ToString();
        }

        private void txt_drug_TextChanged(object sender, EventArgs e)
        {
            if (txt_drug.Text != "")
            {
                txt_drug.BackColor = System.Drawing.Color.White;
                dgv_drug.Visible = true;
                dgv_drug_loda();
                comb_spec_load();
                txt_price_load();
            }
            else
            {
                dgv_drug.Visible = false;
            }
        }

        private void dgv_drug_DoubleClick(object sender, EventArgs e)
        {
            if (!(dgv_drug.Rows.Count == 0))
            {
                Drugcode = dgv_drug.Rows[dgv_drug.CurrentRow.Index].Cells["DrugCode"].Value.ToString();
                txt_drug.Text = dgv_drug.Rows[dgv_drug.CurrentRow.Index].Cells["Drugname"].Value.ToString();
                dgv_drug.Visible = false;
            }

        }

        //只允许数字
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tex_count_TextChanged(object sender, EventArgs e)
        {
            if (!(tex_count.Text == "") && !(txt_price.Text == ""))
            {
                txt_amount_load();
            }
        }

        private void txt_price_TextChanged(object sender, EventArgs e)
        {
            //MessageBox.Show((tex_count.Text == "").ToString());
            //MessageBox.Show((txt_price.Text == "").ToString());
            //MessageBox.Show((!(tex_count.Text == null) && !(txt_price.Text == null)).ToString());
            if (!(tex_count.Text == "")&&!(txt_price.Text == ""))
            {
                txt_amount_load();
            }
        }

        private void bt_ok_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_drug.Text == null || txt_drug.Text == "")
                {
                    //lb_notice.Text = "药品不能为空";
                    //lb_notice.BackColor = Color.Red;
                    MessageBox.Show("请输入药品名称");
                    txt_drug.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
                }
                else
                {
                    if (comb_reason.Text == null || comb_reason.Text == "")
                    {
                        MessageBox.Show("请输入报损原因");
                        comb_reason.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
                    }
                    else
                    {
                        if (tex_count.Text == null || tex_count.Text == "")
                        {
                            MessageBox.Show("请输入药品数量");
                            tex_count.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
                        }
                        else {
                            if (comb_zrr.Text == null || comb_zrr.Text == "")
                            {
                                MessageBox.Show("请填写责任人");
                            }
                            else
                            {
                                if (comb_tbr.Text == null || comb_tbr.Text == "")
                                {
                                    MessageBox.Show("请填写填报人");
                                }
                                else
                                {
                                    string spec = comb_spec.Text.ToString();
                                    string quantity = tex_count.Text.ToString();
                                    string price = txt_price.Text.ToString();
                                    string amount = txt_amount.Text.ToString();
                                    string damagetime = timepick_bs.Value.ToShortDateString();
                                    string reason = comb_reason.Text.ToString();
                                    string tbrcode = comb_tbr.SelectedValue.ToString();
                                    string zrrcode = comb_zrr.SelectedValue.ToString();
                                    string nowtime = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
                                    string batchNum = txt_ph.Text.ToString();
                                    string xiaoqi = txt_xq.Text.ToString();

                                    string str = " insert into Damage values ";
                                    str += "( (select case when  MAX(id) is null then 1 else MAX(id)+1 end from Damage), ";
                                    str += " '" + Drugcode + "' ";
                                    str += " ,'" + spec + "' ";
                                    str += " ,'" + quantity + "' ";
                                    str += " ,'" + price + "' ";
                                    str += " ,'" + amount + "' ";
                                    str += " ,'" + damagetime + "' ";
                                    str += " ,'" + reason + "' ";
                                    str += " ,'" + tbrcode + "' ";
                                    str += " ,'" + zrrcode + "' ";
                                    str += " ,'" + nowtime + "' ";
                                    str += " ,'" + batchNum + "' ";
                                    str += " ,'" + xiaoqi + "') ";

                                    string result = DB.SetPIVAsDB(str) == 1 ? "提交成功" : "提交失败";
                                    //string result = DB.SetPIVAsDB(str).ToString();
                                    MessageBox.Show(result);
                                    txt_drug.Text = "";
                                    Drugcode = string.Empty;
                                    comb_spec.Text = "";
                                    tex_count.Text = "";
                                    txt_price.Text = "";
                                    txt_amount.Text = "";
                                    comb_reason.Text = "";
                                    comb_tbr.Text = "";
                                    comb_zrr.Text = "";
                                    txt_xq.Text = "";
                                    txt_ph.Text = "";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void bt_cx_Click(object sender, EventArgs e)
        {

            //if (!Form2.instance.Visible)
            //{
            //    Form2.instance.Show();
            //    Form2.instance.clean_combb();
            //}
            //Form2 fm = new Form2();
            //fm.Show();
            Form2.ShowChildForm();
            //fm.clean_combb();
        }


        private void comb_reason_Enter(object sender, EventArgs e)
        {
            bt_reason.Visible = true;
        }

        private void comb_reason_Leave(object sender, EventArgs e)
        {
            //bt_reason.Visible = false;
        }

        private void bt_reason_Click(object sender, EventArgs e)
        {
            Form4.ShowChildForm4();
            comb_reason_load();
        }

        private void comb_spec_Enter(object sender, EventArgs e)
        {
            bt_reason.Visible = false;
        }

        private void comb_tbr_Enter(object sender, EventArgs e)
        {
            bt_reason.Visible = false;
        }

        private void comb_zrr_Enter(object sender, EventArgs e)
        {
            bt_reason.Visible = false;
        }

        private void tex_count_Enter(object sender, EventArgs e)
        {
            bt_reason.Visible = false;
        }

        private void timepick_bs_Enter(object sender, EventArgs e)
        {
            bt_reason.Visible = false;
        }

        private void txt_amount_Enter(object sender, EventArgs e)
        {
            bt_reason.Visible = false;
        }

        private void txt_drug_Enter(object sender, EventArgs e)
        {
            bt_reason.Visible = false;
        }

        private void txt_ph_Enter(object sender, EventArgs e)
        {
            bt_reason.Visible = false;
        }

        private void txt_price_Enter(object sender, EventArgs e)
        {
            bt_reason.Visible = false;
        }

    }
}
