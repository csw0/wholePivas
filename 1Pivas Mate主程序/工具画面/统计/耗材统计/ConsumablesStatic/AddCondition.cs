using PIVAsCommon.Helper;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ConsumablesStatic
{
    public partial class AddCondition : Form
    {
        DB_Help db = new DB_Help();
        public AddCondition()
        {
            InitializeComponent();
        }

        private void AddCondition_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(db.IniReadValuePivas("ConsumableIsCharge", "ChargeValue")))
            {
                db.IniWriteValuePivas("ConsumableIsCharge", "ChargeValue", "-1");
            }
            else
            {
                textBox1.Text = db.IniReadValuePivas("ConsumableIsCharge", "ChargeValue");
            }
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            db.IniWriteValuePivas("ConsumableIsCharge", "ChargeValue",textBox1.Text );
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
