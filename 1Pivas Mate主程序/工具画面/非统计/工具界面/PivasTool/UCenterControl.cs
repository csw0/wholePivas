using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PivasTool
{
    public partial class UCenterControl : UserControl
    {
        protected internal UMaxControl um;
        protected internal string ToolsMinCategories;
        public UCenterControl(UMaxControl um, string ToolsMinCategories)
        {
            this.ToolsMinCategories = ToolsMinCategories;
            this.um=um;
            InitializeComponent();
        }

        private void UCenterControl_Load(object sender, EventArgs e)
        {
            Label_ToolsMinCategorie.Text = string.IsNullOrEmpty(ToolsMinCategories) ? "未分组" : ToolsMinCategories;
            UCenterControl_SizeChanged(sender,e);
        }

        private void UCenterControl_SizeChanged(object sender, EventArgs e)
        {
            DataSet ds = um.db.GetPIVAsDB(um.Ssql.GetToolsNameDetail(ToolsMinCategories));
            if (ds != null && ds.Tables.Count > 0)
            {
                UMinControl umc = new UMinControl();
                int count = 0;
                int h = 0;
                panel1.Controls.Clear();
                panel1.Height = umc.Height;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    count = count + 1;
                    umc = new UMinControl(dr, um);
                    if (count * umc.Width <= panel1.Width)
                    {
                        umc.Location = new Point((count - 1) * umc.Width, h);
                    }
                    else
                    {
                        h = h + umc.Height;
                        count = 1;
                        umc.Location = new Point((count - 1) * umc.Width, h);
                        panel1.Height = h + umc.Height;
                    }
                    panel1.Controls.Add(umc);
                }
            }
            this.Height = panel1.Location.Y + panel1.Height;
            ds.Dispose();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show();
            }
        }

        private void 年ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (um.lt)
            {
                ToolAdd ta = new ToolAdd(this);
                ta.Comb_MaxCategories.SelectedIndex = ta.Comb_MaxCategories.Items.IndexOf(um.ToolsMaxCategories);
                ta.Comb_MinCategories.SelectedIndex = ta.Comb_MinCategories.Items.IndexOf(ToolsMinCategories);
                ta.ShowDialog();
            }
            else
            {
                MessageBox.Show("您没有权限，请联系管理员");
            }
        }
    }
}
