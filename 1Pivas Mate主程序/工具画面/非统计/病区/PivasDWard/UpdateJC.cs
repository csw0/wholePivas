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
    /// 简称更新
    /// </summary>
    public partial class UpdateJC : Form
    {
        public UpdateJC()
        {
            InitializeComponent();
        }
        updatedb update = new updatedb();
        seldb sel = new seldb();
        int y;
        DataGridViewRow dr;

        public UpdateJC(DataGridViewRow dr,int y)
        {
            InitializeComponent();
            this.dr = dr;
            this.y = y;
        }

        /// <summary>
        /// 简称更新画面初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateJC_Load(object sender, EventArgs e)
        {
           
            if (dr.Cells[y].Value.ToString() == string.Empty&&y==2)
            {
                textBox1.Text = dr.Cells["WardName"].Value.ToString();//取得当前病区使用的简称
            }
            else
            {
                textBox1.Text = dr.Cells[y].Value.ToString();//取得当前病区使用的简称
            }
        }

        /// <summary>
        /// 回车事件响应（更新数据，并赋值调用画面）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateJianChen();
            }
        }

        /// <summary>
        /// 简称更新
        /// </summary>
        private void UpdateJianChen()
        {
            PivasDWard form1 = (PivasDWard)this.Owner;
            dr.Cells[y].Value = textBox1.Text;

            update.updatjc(dr.Cells["WardSimName"].Value.ToString(), dr.Cells["Spellcode"].Value.ToString(), dr.Cells["WardCode"].Value.ToString());
            
            //form1.ddrefresh(rowDWard.i, rowDWard.k);
            
            this.Close();
        }

        
    }
}
