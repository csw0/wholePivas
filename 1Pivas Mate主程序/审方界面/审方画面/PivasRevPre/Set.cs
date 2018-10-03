using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace PivasRevPre
{
    public partial class frmSet : Form
    {
        public frmSet()
        {
            InitializeComponent();
        }
        DB_Help DB = new DB_Help();
        public string O = "";
        public string E = "";
        public string C = "";
        public string M = "";
        public string s = "";
        public string all = "";
        public string Name = "";
        public string Confirmation = "";
        private string EMployeeID;

        public frmSet(string EMployeeID)
        {
            InitializeComponent();
            this.EMployeeID = EMployeeID;
        }

        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion

        /// <summary>
        /// 保存病区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblSave_Click(object sender, EventArgs e)
        {
            string str = "";
            try
            {
                if (cbbOpen.Checked)//是否仅显示开放病区
                    O = "0";
                else
                    O = "1";

                if (cbbEmpty.Checked)//是否仅显示有数据的病区
                    E = "0";
                else
                    E = "1";
                if (cbbComplete.Checked)//是否仅显示审方未完成的病区
                    C = "0";
                else
                    C = "1";
                if (cbbMode.SelectedIndex == 0)
                    M = "0";
                else
                    M = "1";
                if (CBConfrim.Checked)//是否不显示审方确认对话框
                    s = "0";
                else
                    s = "1";
                if (cbSelectAll.Checked)
                    all = "0";
                else
                    all = "1";
                if (cbNameList.Checked)
                    Name = "1";
                else
                    Name = "0";
                if (cbbConfirmation.Checked)
                {
                    Confirmation = "1";
                }
                else
                {
                    Confirmation = "0";
                }

                string RightColor = radioButton1.BackColor.R + "," + radioButton1.BackColor.G + "," + radioButton1.BackColor.B;
                string Level3Color=radioButton2.BackColor.R + "," + radioButton2.BackColor.G + "," + radioButton2.BackColor.B;
                string Level5Color = radioButton3.BackColor.R + "," + radioButton3.BackColor.G + "," + radioButton3.BackColor.B;
                string SelectedeColor = radioButton4.BackColor.R + "," + radioButton4.BackColor.G + "," + radioButton4.BackColor.B;
                str = "UPDATE RevPreFormSet SET WardIdle = " + E + ",WardOpen = " + O + ",RevPreOver = " +
                    C + ",PreviewMode = " + M + ",Confirm = " + s + ",SelectAll = " + all + ",NameList = " + Name +
                     ",RightColor= '" + RightColor + "',Level3Color= '" + Level3Color + "',Level5Color= '" + Level5Color + "' ,SelectedColor = '" + SelectedeColor + "'," +
                     "Confirmation = '" + Confirmation + "'";
                     //+ "'  WHERE DEmployeeID = " + EMployeeID;控制
                DB.SetPIVAsDB(str);
              //  MessageBox.Show(str);
                this.DialogResult = DialogResult.OK;
            }
            catch
            {
                MessageBox.Show("请检查RevPreFormSet表中是否缺少字段！！！" + str);
            }
        }

        /// <summary>
        /// 取消事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public void Set(string Empty, string Open, string Complete, string Mode, string Confrim, string selectAll, string NameList, string RightColor, string Level3Color, string Level5Color, string SelectedColor, string Confirmation)
        {
            if (Empty == "0")            
                cbbEmpty.Checked = true;                            
            else
                cbbEmpty.Checked = false;

            if (Open == "0")
                cbbOpen.Checked = true;
            else
                cbbOpen.Checked = false;
            if (Complete == "0")
                cbbComplete.Checked = true;
            else
                cbbComplete.Checked = false;
            if (Mode == "0")
                cbbMode.SelectedIndex = 0;
            else
                cbbMode.SelectedIndex = 1;
            if (Confrim == "0")
                CBConfrim.Checked = true;
            else
                CBConfrim.Checked = false;
            if (selectAll == "0")
                cbSelectAll.Checked = true;
            else
                cbSelectAll.Checked = false;
            if (NameList == "1")
                cbNameList.Checked = true;
            else
                cbNameList.Checked = false;
            if (Confirmation=="1")
            {
                cbbConfirmation.Checked = true;
            }
            else
            {
                cbbConfirmation.Checked = false;
            }


            radioButton1.BackColor = System.Drawing.ColorTranslator.FromHtml(RightColor);
            radioButton2.BackColor = System.Drawing.ColorTranslator.FromHtml(Level3Color);
            radioButton3.BackColor = System.Drawing.ColorTranslator.FromHtml(Level5Color);
            radioButton4.BackColor = System.Drawing.ColorTranslator.FromHtml(SelectedColor);
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        /// <summary>
        /// 单击选色区域的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label3_Click(object sender, EventArgs e)
        {
            Label lab = (Label)sender;
            if (radioButton1.Checked == true)
            { radioButton1.BackColor = lab.BackColor; }
            else if (radioButton2.Checked == true)
            { radioButton2.BackColor = lab.BackColor; }
            else if (radioButton3.Checked == true)
            { radioButton3.BackColor = lab.BackColor; }
            else
            { radioButton4.BackColor = lab.BackColor; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HelpForm Hf = new HelpForm();
            Hf.Show();
        }

        private void frmSet_Load(object sender, EventArgs e)
        {

        }
    }
}
