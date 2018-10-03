using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DWardManage
{
    public partial class UpdateLM : Form
    {
        private DataGridViewRow dr;
        updatedb update = new updatedb();
        public UpdateLM(DataGridViewRow dr)
        {
            InitializeComponent();
            this.dr = dr;
        }

        private void UpdateLM_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Value = Convert.ToDateTime(dr.Cells["LimitSTTime"].Value.ToString());
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
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
            update.updateLM(dateTimePicker1.Value.ToString("HH:mm"), dr.Cells["WardCode"].Value.ToString());
            dr.Cells["LimitSTTime"].Value =this.dateTimePicker1.Value.ToString("HH:mm");
            this.Close();
        }

    }
}
