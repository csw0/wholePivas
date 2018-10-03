using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FastReport;
using PIVAsCommon.Helper;
using PivasLimitDES;

namespace QrUserCode
{
    public partial class QrUserCode : Form
    {

        private DB_Help db;
        private string User;

        private Report report = new Report();
        private string value;
        private string DEmployeeID;
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MOVE = 0xF010;
        private const int HTCAPTION = 0x0002;
        DataTable demploy = new DataTable();
       

        public QrUserCode()
        {
            InitializeComponent();
        }
        public QrUserCode(string DEmployeeID)
        {
            InitializeComponent();
            User = DEmployeeID;
        }

        private void QrUserCode_Load(object sender, EventArgs e)
        {
            try
            {
                comboBox1.SelectedIndex = 0;
                db = new DB_Help();
                string ir = db.IniReadValuePivas("Printer", "EmpReportWe");
                comboBox2.SelectedIndex = string.IsNullOrEmpty(ir) ? 0 : Convert.ToInt32(ir);
                //if (db.GetPIVAsDB(string.Format("SELECT [LimitName] FROM [dbo].[ManageLimit] where [DEmployeeID]='{0}' and LimitName='QrUserCode'", User)).Tables[0].Rows.Count == 0)
                if (!GetPivasLimit.Instance.Limit(User, "QrUserCode"))
                {
                    this.Dispose(true);
                }
                string sql = "  select *from DEmployee";
               
                demploy = db.GetPIVAsDB(sql).Tables[0];
                listBox1.DataSource = demploy;
                listBox1.DisplayMember = "DEmployeeCode";
                listBox1.ValueMember = "DEmployeeCode";
                textBox1.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db.IniWriteValuePivas("Printer", "EmpReportWe", comboBox2.SelectedIndex.ToString());
            if (report.Pages.Count != 0)
            {
                string printerName = db.IniReadValuePivas("Printer", "LabelPrinter");
                using (PrintDocument printDocument = new PrintDocument())
                {
                    string Mprint = printDocument.PrinterSettings.PrinterName;
                    if (string.IsNullOrEmpty(printerName))
                    {
                        if (printDocument.PrinterSettings.IsValid)
                        {
                            report.PrintSettings.Printer = Mprint;
                            report.PrintSettings.ShowDialog = false;
                            if (previewControlFR.Print())
                            {
                                db.SetPIVAsDB(string.Format("UPDATE [dbo].[QRcodeLog]SET [DelDT] = GETDATE() WHERE [DelDT] is null and [DEmployeeID]='{0}'", DEmployeeID));
                                db.SetPIVAsDB(string.Format("INSERT INTO [dbo].[QRcodeLog]([DEmployeeID],[QRcode],[QRcodeDT])VALUES('{0}','{1}',GETDATE())", DEmployeeID, value));
                                if (comboBox1.Text != "1")
                                {
                                    int g = Convert.ToInt32(comboBox1.Text);
                                    for (int i = 1; i < g; i++)
                                    {
                                        previewControlFR.Print();
                                    }
                                }
                                MessageBox.Show("打印成功");
                            }
                            else
                                MessageBox.Show("打印失败");
                        }
                        else
                        {
                            MessageBox.Show("打印机配置为空且系统默认打印机不可用");
                        }
                    }
                    else
                    {
                        printDocument.PrinterSettings.PrinterName = printerName;
                        if (printDocument.PrinterSettings.IsValid)
                        {
                            report.PrintSettings.Printer = printerName;
                            report.PrintSettings.ShowDialog = false;
                            if (previewControlFR.Print())
                            {
                                db.SetPIVAsDB(string.Format("UPDATE [dbo].[QRcodeLog]SET [DelDT] = GETDATE() WHERE [DelDT] is null and [DEmployeeID]='{0}'", DEmployeeID));
                                db.SetPIVAsDB(string.Format("INSERT INTO [dbo].[QRcodeLog]([DEmployeeID],[QRcode],[QRcodeDT])VALUES('{0}','{1}',GETDATE())", DEmployeeID, value));
                                if (comboBox1.Text != "1")
                                {
                                    int g = Convert.ToInt32(comboBox1.Text);
                                    for (int i = 1; i < g; i++)
                                    {
                                        previewControlFR.Print();
                                    }
                                }
                                MessageBox.Show("打印成功");
                            }
                            else
                                MessageBox.Show("打印失败");
                        }
                        else
                        {
                            MessageBox.Show("当前配置的打印机不可用，若使用系统默认打印机，请将打印机配置为空");
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                MessageBox.Show("员工号不能为空");
            }
            else
            {
                using (DataSet ds = db.GetPIVAsDB(string.Format("SELECT DEmployeeID,DEmployeeName,getdate()  FROM [dbo].[DEmployee] where Pas='{1}' and AccountID='{0}'", textBox1.Text.Trim(), textBox2.Text.Trim())))
                {
                    if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    {
                        MessageBox.Show("用户名或密码输入有误");
                    }
                    else
                    {
                        using (Image img = new Bitmap(100, 100))
                        {
                            DEmployeeID = ds.Tables[0].Rows[0][0].ToString();
                            value = "7777" + DateTime.Now.Ticks;
                            new DotNetBarcode().QRWriteBar(value, -10, -10, 4, Graphics.FromImage(img));
                            report.Clear();
                            previewControlFR.Clear();
                            report.Preview = previewControlFR;
                            report.Load(".\\Crystal\\Usercode.frx");
                            string name = ds.Tables[0].Rows[0][1].ToString();
                            string DT = ds.Tables[0].Rows[0][2].ToString();
                            (report.FindObject("Text1") as FastReport.TextObject).Text = name + "  " + DT;
                            (report.FindObject("Text2") as FastReport.TextObject).Text = name + "  " + DT;
                            (report.FindObject("Picture1") as FastReport.PictureObject).Image = img;
                            (report.FindObject("Picture2") as FastReport.PictureObject).Image = img;
                            (report.FindObject("Barcode1") as FastReport.Barcode.BarcodeObject).Text = value;
                            (report.FindObject("Barcode2") as FastReport.Barcode.BarcodeObject).Text = value;
                            if (comboBox2.SelectedIndex == 0)
                            {
                                (report.FindObject("Barcode1") as FastReport.Barcode.BarcodeObject).Visible = true;
                                (report.FindObject("Barcode2") as FastReport.Barcode.BarcodeObject).Visible = true;
                                (report.FindObject("Picture1") as FastReport.PictureObject).Visible = false ;
                                (report.FindObject("Picture2") as FastReport.PictureObject).Visible = false;
                            }
                            else
                            {
                                (report.FindObject("Barcode1") as FastReport.Barcode.BarcodeObject).Visible = false;
                                (report.FindObject("Barcode2") as FastReport.Barcode.BarcodeObject).Visible = false;
                                (report.FindObject("Picture1") as FastReport.PictureObject).Visible = true;
                                (report.FindObject("Picture2") as FastReport.PictureObject).Visible = true;
                            }

                            report.Show();
                        }
                    }
                }
            }
        }

        private void QrUserCode_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataRow[] dr = demploy.Select("DEmployeeCode like '%" + textBox1.Text.Trim() + "%' or DEmployeeName like '%"+textBox1.Text+"%'");
            if (dr.Length > 0)
            {

                listBox1.DataSource = dr.CopyToDataTable(); 
                listBox1.DisplayMember = "DEmployeeCode";
                listBox1.ValueMember = "DEmployeeCode";
            }
            else
            {
                listBox1.DataSource=null;
            }
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (!this.listBox1.Focused && string.IsNullOrEmpty(this.textBox1.Text.Trim()))
            {
                this.listBox1.Visible = false;
            }
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            this.listBox1.Visible = true;
            if (e.KeyValue== 38 || e.KeyValue == 40)
            {
                this.listBox1.Focus();
            }
            if (e.KeyValue == 13&&listBox1.SelectedValue!=null)
            {
                textBox1.Text = listBox1.SelectedValue.ToString();
                this.listBox1.Visible = false;
            }
        }
        private void label5_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

       

        private void listBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.SelectedValue.ToString();
            this.listBox1.Visible = false;
        }

       

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                textBox1.Text = listBox1.SelectedValue.ToString();
                this.listBox1.Visible = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 1)
            {
                pictureBox1.Visible = true;
                pictureBox2.Visible = false;
            }
            else
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = true;
            }
        }

    
    }
}
