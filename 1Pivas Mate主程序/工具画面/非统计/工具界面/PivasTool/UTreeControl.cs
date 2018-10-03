using System;
using System.Drawing;
using System.Windows.Forms;

namespace PivasTool
{
    public partial class UTreeControl : UserControl
    {
        protected internal UMaxControl um;
        public UTreeControl(UMaxControl um)
        {
            this.um = um;
            InitializeComponent();
        }
        protected internal void Panel_Tree_Click(object sender, EventArgs e)
        {
            foreach (UTreeControl c in this.Parent.Controls)
            {
                c.BackgroundImage = (Image)PivasTool.Properties.Resources.ResourceManager.GetObject("菜单项");
                c.Label_TreeName.ForeColor = Color.FromArgb(64, 64, 64);
            }
            this.BackgroundImage = (Image)PivasTool.Properties.Resources.ResourceManager.GetObject("菜单项1");
            Label_TreeName.ForeColor = Color.White;
            um.ShowUCenter(Label_TreeName.Text);
        }
    }
}
