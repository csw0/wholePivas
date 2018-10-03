using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PivasLabelCheckAll
{
    /// <summary>
    /// 瓶签信息用户控件
    /// </summary>
    public partial class ucLabelsInfo : UserControl
    {
        #region 属性
        public string CheckTable = string.Empty;
        public string CheckDt = string.Empty;
        public Color Adcolor;
        #endregion

        #region 方法
        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <param name="ds">瓶签信息的数据集</param>
        public ucLabelsInfo()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件
        /// <summary>
        /// 页面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucLabelsInfo_Load(object sender, EventArgs e)
        {
           
        }
        #endregion

        private void dgvLabelsInfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            LabelDetailInfo l = new LabelDetailInfo(dgvLabelsInfo.CurrentRow.Cells["瓶签号"].Value.ToString(), CheckTable,CheckDt);
            l.ShowDialog();

        }


        private void reColor()
        {
            if (dgvLabelsInfo.Rows.Count > 0)
            {
                for (int i = 0; i < dgvLabelsInfo.Rows.Count; i++)
                {
                    if (dgvLabelsInfo.Rows[i].Cells["提前打包"].Value.ToString() == "提前打包")
                    {
                        dgvLabelsInfo.Rows[i].DefaultCellStyle.BackColor = Adcolor;
                    }
                }
            }
        }

        private void dgvLabelsInfo_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            reColor();
        }

        public void dgvDT(DataTable dt)
        {
            dgvLabelsInfo.DataSource = dt;
            for (int i=0;i<dt.Rows.Count ;i++)
            {
                dgvLabelsInfo.Rows[i].Cells["主药"].Style.BackColor = Color.FromArgb(Convert.ToInt32(dgvLabelsInfo.Rows[i].Cells["主药颜色"].Value.ToString()));
                dgvLabelsInfo.Rows[i].Cells["溶媒"].Style.BackColor = Color.FromArgb(Convert.ToInt32(dgvLabelsInfo.Rows[i].Cells["溶媒颜色"].Value.ToString()));
            }
            dgvLabelsInfo.Columns["主药颜色"].Visible = false;
            dgvLabelsInfo.Columns["溶媒颜色"].Visible = false;
        }
    }
}
