using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DWardManage
{
    /// <summary>
    /// 排序号修改
    /// </summary>
    public partial class UpdatePxh : Form
    {
        public UpdatePxh()
        {
            InitializeComponent();
        }

        seldb sel=new seldb();
        updatedb update = new updatedb();
        DataGridViewRow dr;


        public UpdatePxh(DataGridViewRow dr) 
        {
            InitializeComponent();
            this.dr = dr;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdatePxh_Load(object sender, EventArgs e)
        {
            DataTable dt2 = new DataTable();
            dt2 = sel.getWardSeqNo().Tables[0];//取得左右在使用排序号
            comboBox1.DataSource = dt2;
            comboBox1.DisplayMember = "WardSeqNo";
            comboBox1.ValueMember= "WardSeqNo";

            comboBox1.Text = dr.Cells["WardSeqNo"].Value.ToString();//取得当前病区原始排序号
        }

        

        /// <summary>
        /// 回车时间响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdatePaiXu();
            }
        }

        /// <summary>
        /// 排序号更新
        /// </summary>
        private void UpdatePaiXu()
        {
            PivasDWard form1 = (PivasDWard)this.Owner;
            if (comboBox1.Text != "")
            {
                update.updatpxh(comboBox1.Text, dr.Cells["WardCode"].Value.ToString());

                dr.Cells["WardSeqNo"].Value = comboBox1.Text;
            }
            this.Close();
        }
     
        /// <summary>
        /// 被选中排序更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            PivasDWard form1 = (PivasDWard)this.Owner;
            if (comboBox1.Text != "")
            {
                update.updatpxh(comboBox1.SelectedValue.ToString(), dr.Cells["WardCode"].Value.ToString());
                dr.Cells["WardSeqNo"].Value = comboBox1.SelectedValue.ToString();
                //form1.ddrefresh(rowDWard.i, rowDWard.k);
            }
            this.Close();
        }

        

    }
}
