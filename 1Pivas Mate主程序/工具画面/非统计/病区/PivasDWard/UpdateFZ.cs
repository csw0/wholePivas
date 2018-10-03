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
    /// 病区区域更新
    /// </summary>
    public partial class UpdateFZ : Form
    {
        public UpdateFZ()
        {
            InitializeComponent();
        }

        seldb sel = new seldb();
        updatedb update = new updatedb();
        DataGridViewRow dr;

        public UpdateFZ(DataGridViewRow dr)
        {
            InitializeComponent();
            this.dr = dr;
        }

        /// <summary>
        /// 区域更新画面初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateFZ_Load(object sender, EventArgs e)
        {
            DataTable dt2 = new DataTable();
            dt2 = sel.getWardArea().Tables[0];//取得左右正在使用区域
            comboBox1.DataSource = dt2;
            comboBox1.DisplayMember = "WardArea";
            comboBox1.ValueMember = "WardArea";
            comboBox1.Text = dr.Cells["WardArea"].Value.ToString();//取得当前病区所属区域
              
        }

        /// <summary>
        /// 相应控件回车时间（更新数据）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateWardArea();
            }
        }

       /// <summary>
       /// 更新区域/并给调用画面赋值
       /// </summary>
        private void UpdateWardArea()
        {
            PivasDWard form1 = (PivasDWard)this.Owner;
            update.updatfz(comboBox1.Text, dr.Cells["WardCode"].Value.ToString());
            dr.Cells["WardArea"].Value = comboBox1.Text;
            this.Close();
        }

        /// <summary>
        /// 下拉框选择更新数据，并赋值调用画面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            PivasDWard form1 = (PivasDWard)this.Owner;
            update.updatfz(comboBox1.SelectedValue.ToString(), dr.Cells["WardCode"].Value.ToString());
            dr.Cells["WardArea"].Value = comboBox1.SelectedValue.ToString();
            this.Close();
        }

    }
}
