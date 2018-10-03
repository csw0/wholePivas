using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace PivasIVRPrint
{
    public partial class SetShowFunc : Form
    {
        private UserControlPrint piv;
        private ColorDialog cd = new ColorDialog();
        public SetShowFunc(UserControlPrint p)
        {
            piv = p;
            InitializeComponent();
            cd.AllowFullOpen = true;
            cd.FullOpen = true;
            cd.AnyColor = true;
            cd.SolidColorOnly = false;
            cd.CustomColors = new int[] { ColorTranslator.ToWin32(button3.BackColor), ColorTranslator.ToWin32(button6.BackColor), ColorTranslator.ToWin32(button5.BackColor), ColorTranslator.ToWin32(button7.BackColor) };
            foreach (string s in PrinterSettings.InstalledPrinters)
            {
                comboBox1.Items.Add(s);
                comboBox4.Items.Add(s);
            }
        }
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MOVE = 0xF010;
        private const int HTCAPTION = 0x0002;


        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void SetShowFunc_Load(object sender, EventArgs e)
        {
            if (piv.HasLoad)
            {
                comboBox2.SelectedIndex = UserControlPrint.PreviewMode;
                comboBox3.SelectedIndex = UserControlPrint.PrintNumber - 1;
                checkBox1.Checked = UserControlPrint.WardIdle;
                checkBox2.Checked = UserControlPrint.WardOpen;
                checkBox3.Checked = UserControlPrint.DrugList;
                checkBox4.Checked = UserControlPrint.Positions;
                comboBox5.SelectedIndex = UserControlPrint.PrintDrugCount;
                checkBox6.Checked = UserControlPrint.PrintOrderCount;
                checkBox7.Checked = UserControlPrint.UP2;
                checkBox8.Checked = UserControlPrint.Species;
                checkBox9.Checked = UserControlPrint.PrintOverCheck;
                checkBox10.Checked = UserControlPrint.CheckDrugLimit;
                comboBox1.SelectedIndex = !comboBox1.Items.Contains(piv.LabelNoPrint) ? 0 : comboBox1.Items.IndexOf(piv.LabelNoPrint);
                comboBox4.SelectedIndex = !comboBox4.Items.Contains(piv.CountPrint) ? 0 : comboBox4.Items.IndexOf(piv.CountPrint);
                comboBox7.SelectedIndex = comboBox7.Items.IndexOf(piv.NextDay.Split(':')[0]);
                comboBox8.SelectedIndex = comboBox8.Items.IndexOf(piv.NextDay.Split(':')[1]);
                button3.BackColor = UserControlPrint.NotPrint.ToArgb() == 0 ? button3.BackColor : UserControlPrint.NotPrint;
                button6.BackColor = UserControlPrint.NotPrintSelected.ToArgb() == 0 ? button6.BackColor : UserControlPrint.NotPrintSelected;
                button5.BackColor = UserControlPrint.Printed.ToArgb() == 0 ? button5.BackColor : UserControlPrint.Printed;
                button7.BackColor = UserControlPrint.PrintSelected.ToArgb() == 0 ? button7.BackColor : UserControlPrint.PrintSelected;
                button8.BackColor = UserControlPrint.HasCheck.ToArgb() == 0 ? button8.BackColor : UserControlPrint.HasCheck;
                button9.BackColor = UserControlPrint.HasCheckSelected.ToArgb() == 0 ? button9.BackColor : UserControlPrint.HasCheckSelected;
                this.StartPosition = FormStartPosition.Manual;
            }
            else
            {
                comboBox2.SelectedIndex =
                comboBox3.SelectedIndex = 
                comboBox5.SelectedIndex = 0;
                checkBox1.Checked =
                checkBox2.Checked =
                checkBox3.Checked =
                checkBox4.Checked =
                checkBox6.Checked =
                checkBox7.Checked =
                checkBox8.Checked =
                checkBox9.Checked = true;
                checkBox10.Checked = false;
                comboBox1.SelectedIndex = 0;
                comboBox4.SelectedIndex = 0;
                comboBox7.SelectedIndex = 12;
                comboBox8.SelectedIndex = 0;
                button1_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (piv.HasLoad)
                {
                    if (piv.dbHelp.SetPIVAsDB(string.Format("UPDATE [dbo].[PrintFormSet] SET [ChooseMode] ='{0}',[PreviewMode] ='{1}' ,[WardIdle] ='{2}' ,[WardOpen] ='{3}',[DrugList] = '{4}',[PrintNumber]='{5}',[PrintOrderCount]='{6}',[PrintDrugCount]='{7}',[Positions]='{8}',[Species]='{9}',[UP2]='{10}',NextDay='{11}',[NotPrint]='{12}',[NotPrintSelected]='{13}',[Printed]='{14}',[PrintSelected]='{15}',[PrintOverCheck]='{16}',[HasCheck]='{17}',[HasCheckSelected]='{18}',[CheckDrugLimit]='{19}' WHERE [DEmployeeID] = '{20}'", "0", comboBox2.SelectedIndex, checkBox1.Checked, checkBox2.Checked, checkBox3.Checked, comboBox3.SelectedIndex + 1, checkBox6.Checked, comboBox5.SelectedIndex, checkBox4.Checked, checkBox8.Checked, checkBox7.Checked, comboBox7.SelectedItem.ToString() + ":" + comboBox8.SelectedItem.ToString(), button3.BackColor.ToArgb(), button6.BackColor.ToArgb(), button5.BackColor.ToArgb(), button7.BackColor.ToArgb(), (checkBox9.Checked ? "1" : "0"), button8.BackColor.ToArgb(), button9.BackColor.ToArgb(), checkBox10.Checked, piv.userID)) > 0)
                    {
                        UserControlPrint.PreviewMode = comboBox2.SelectedIndex;
                        UserControlPrint.PrintNumber = comboBox3.SelectedIndex + 1;
                        UserControlPrint.WardIdle = checkBox1.Checked;
                        UserControlPrint.WardOpen = checkBox2.Checked;
                        UserControlPrint.DrugList = checkBox3.Checked;
                        UserControlPrint.Positions = checkBox4.Checked;
                        UserControlPrint.PrintDrugCount = comboBox5.SelectedIndex;
                        UserControlPrint.PrintOrderCount = checkBox6.Checked;
                        UserControlPrint.UP2 = checkBox7.Checked;
                        UserControlPrint.Species = checkBox8.Checked;
                        UserControlPrint.PrintOverCheck = checkBox9.Checked;
                        UserControlPrint.CheckDrugLimit = checkBox10.Checked;
                        UserControlPrint.NotPrint = button3.BackColor;
                        UserControlPrint.NotPrintSelected = button6.BackColor;
                        UserControlPrint.Printed = button5.BackColor;
                        UserControlPrint.PrintSelected = button7.BackColor;
                        UserControlPrint.HasCheck = button8.BackColor;
                        UserControlPrint.HasCheckSelected = button9.BackColor;
                        piv.LabelNoPrint = comboBox1.SelectedIndex == 0 ? string.Empty : comboBox1.SelectedItem.ToString();
                        piv.dbHelp.IniWriteValuePivas("Printer", "LabelPrinter", piv.LabelNoPrint);
                        piv.CountPrint = comboBox4.SelectedIndex == 0 ? string.Empty : comboBox4.SelectedItem.ToString();
                        piv.dbHelp.IniWriteValuePivas("Printer", "CountPrint", piv.CountPrint);
                        piv.NextDay = comboBox7.SelectedItem.ToString() + ":" + comboBox8.SelectedItem.ToString();
                        if (int.Parse(piv.dbHelp.GetPIVAsDB(string.Format("select DATEDIFF(MI,'{0}',CONVERT(varchar,GETDATE(),108))", piv.NextDay)).Tables[0].Rows[0][0].ToString()) >= 0)
                        {
                            piv.dateTimePicker1.Value = Convert.ToDateTime(DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-dd"));
                        }
                        else
                        {
                            piv.dateTimePicker1.Value = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                        }
                        piv.comboxch();
                        piv.PivasIVRP_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("设置失败");
                    }
                }
                else
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder(4096);
                    sb.Append("INSERT INTO [PrintFormSet] ");
                    sb.Append("([DEmployeeID],[ChooseMode],[PreviewMode],[WardIdle],[WardOpen],[DrugList],[PrintNumber],[PrintOrderCount],[PrintDrugCount],[Positions],[Species],[UP2],[NextDay],[NotPrint],[NotPrintSelected],[Printed],[PrintSelected],[PrintOverCheck],[HasCheck],[HasCheckSelected],[CheckDrugLimit])");
                    sb.Append(" VALUES ");
                    sb.AppendFormat("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}')", piv.userID, "0", comboBox2.SelectedIndex, checkBox1.Checked, checkBox2.Checked, checkBox3.Checked, comboBox3.SelectedIndex + 1, checkBox6.Checked, comboBox5.SelectedIndex, checkBox4.Checked, checkBox8.Checked, checkBox7.Checked, comboBox7.SelectedItem.ToString() + ":" + comboBox8.SelectedItem.ToString(), button3.BackColor.ToArgb(), button6.BackColor.ToArgb(), button5.BackColor.ToArgb(), button7.BackColor.ToArgb(), (checkBox9.Checked ? "1" : "0"), button8.BackColor.ToArgb(), button9.BackColor.ToArgb(), checkBox10.Checked);
                    if (piv.dbHelp.SetPIVAsDB(sb.ToString()) == 0)
                        MessageBox.Show("添加配置失败");
                    else
                    {
                        UserControlPrint.PreviewMode = comboBox2.SelectedIndex;
                        UserControlPrint.PrintNumber = comboBox3.SelectedIndex + 1;
                        UserControlPrint.WardIdle = checkBox1.Checked;
                        UserControlPrint.WardOpen = checkBox2.Checked;
                        UserControlPrint.DrugList = checkBox3.Checked;
                        UserControlPrint.Positions = checkBox4.Checked;
                        UserControlPrint.PrintDrugCount = comboBox5.SelectedIndex;
                        UserControlPrint.PrintOrderCount = checkBox6.Checked;
                        UserControlPrint.UP2 = checkBox7.Checked;
                        UserControlPrint.Species = checkBox8.Checked;
                        UserControlPrint.PrintOverCheck = checkBox9.Checked;
                        UserControlPrint.CheckDrugLimit = checkBox10.Checked;
                        UserControlPrint.NotPrint = button3.BackColor;
                        UserControlPrint.NotPrintSelected = button6.BackColor;
                        UserControlPrint.Printed = button5.BackColor;
                        UserControlPrint.PrintSelected = button7.BackColor;
                        UserControlPrint.HasCheck = button8.BackColor;
                        UserControlPrint.HasCheckSelected = button9.BackColor;
                        piv.NextDay = comboBox7.SelectedItem.ToString().Trim() + ":" + comboBox8.SelectedItem.ToString().Trim();
                        piv.HasLoad = true;
                        if (int.Parse(piv.dbHelp.GetPIVAsDB(string.Format("select DATEDIFF(MI,'{0}',CONVERT(varchar,GETDATE(),108))", piv.NextDay)).Tables[0].Rows[0][0].ToString()) >= 0)
                        {
                            piv.dateTimePicker1.Value = Convert.ToDateTime(DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-dd"));
                        }
                        else
                        {
                            piv.dateTimePicker1.Value = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                        }
                        piv.comboxch();
                        piv.PivasIVRP_Load(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Dispose(true);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            new BatchOrder().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cd.Color = button3.BackColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                button3.BackColor = cd.Color;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            cd.Color = button6.BackColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                button6.BackColor = cd.Color;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cd.Color = button5.BackColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                button5.BackColor = cd.Color;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            cd.Color = button7.BackColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                button7.BackColor = cd.Color;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            cd.Color = button8.BackColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                button8.BackColor = cd.Color;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            cd.Color = button9.BackColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                button9.BackColor = cd.Color;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            new HelpShow().ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            new DrugsColorSelect(piv).ShowDialog();
        }
    }
}
