using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PivasDrugFlow
{
    public partial class DrugSelect : Form
    {
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MOVE = 0xF010;
        private const int HTCAPTION = 0x0002;

        private string item = string.Empty;
        private bool isAllSelect = false;
        private DB_Help db = new DB_Help();
        private MainForm mf;
        private string sql = string.Empty;
        private DataTable DBDT = new DataTable();
        private DataTable ShowDT = new DataTable();
        private string SelVal = string.Empty;

        public DrugSelect(MainForm mf, string val)
        {
            this.mf = mf;
            this.item = val;
            InitializeComponent();
        }

        private void DrugSelect_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            }
        }

        private void DrugSelect_Load(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Empty;
                switch (item.Trim())
                {
                    case "药品选择":
                        {
                            sql = "SELECT convert(bit,0) as TF,[DrugName]+[Spec] as '药品名',[DrugCode],[SpellCode] FROM [dbo].[DDrug] where [IsValid] = '1'";
                            break;
                        }
                    case "成分选择":
                        {
                            sql = "SELECT convert(bit,0) as TF,[MedicineName] as '成分名称',[MedicineID],[SpellCode] FROM [KD0100]..[Medicine]";
                            break;
                        }
                }
                using (DataSet ds = db.GetPIVAsDB(sql))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DBDT = ds.Tables[0].Copy();
                        DBDT.PrimaryKey = new DataColumn[] { DBDT.Columns[2] };
                        foreach (DataRow dr in DBDT.Rows)
                        {
                            dr[0] = (item.Trim() == "药品选择" ? mf.Drugs.Rows.Contains(dr[2]) : mf.DrugForClass.Contains("'" + dr[2].ToString() + "'"));
                        }
                        ShowDT = DBDT.Clone();
                        ShowDT.PrimaryKey = new DataColumn[] { ShowDT.Columns[2] };
                    }
                }
                ShowVal();
                dataGridView1.DataSource = ShowDT;
                dataGridView1.Columns[0].HeaderText = string.Empty;
                dataGridView1.Columns[0].Width = 25;
                dataGridView1.Columns[1].Width = 280;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mf.Drugs.Rows.Clear();
            mf.DrugForClass = string.Empty;
            if (!isAllSelect || !string.IsNullOrEmpty(SelVal.Trim()))
            {
                bool isALL = true;
                string DrugClass = string.Empty;
                foreach (DataRow dr in ShowDT.Rows)
                {
                    if (Equals(dr[0], true))
                    {
                        if (item.Trim() == "药品选择")
                        {
                            mf.Drugs.Rows.Add(dr[2], dr[1]);
                        }
                        else
                        {
                            DrugClass = DrugClass + "'" + dr[2] + "',";
                        }
                    }
                    else
                    {
                        isALL = false;
                    }
                }
                if (isALL)
                {
                    if (string.IsNullOrEmpty(SelVal.Trim()))
                    {
                        mf.Drugs.Rows.Clear();
                    }
                }
                else
                {
                    mf.DrugForClass = DrugClass.TrimEnd(',');
                }
            }
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            isAllSelect = !isAllSelect;
            foreach (DataRow dr in ShowDT.Rows)
            {
                dr[0] = isAllSelect;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                button4_Click(sender, e);
            }
        }

        private void ShowVal()
        {
            try
            {
                SelVal = textBox1.Text;
                for (int i = ShowDT.Rows.Count - 1; i >= 0; i--)
                {
                    if (!Equals(ShowDT.Rows[i][0], true))
                    {
                        ShowDT.Rows.RemoveAt(i);
                    }
                }
                if (ShowDT.Rows.Count == 0)
                {
                    foreach (DataRow dr in DBDT.Select(string.IsNullOrEmpty(textBox1.Text.Trim()) ? string.Empty : string.Format("TF=1 or {0} like ('%{1}%') or {2} like ('%{1}%')", DBDT.Columns[1].ColumnName, textBox1.Text.Trim(), DBDT.Columns[3].ColumnName), "TF DESC"))
                    {
                        ShowDT.Rows.Add(dr.ItemArray);
                    }
                }
                else
                {
                    foreach (DataRow dr in DBDT.Select(string.IsNullOrEmpty(textBox1.Text.Trim()) ? string.Empty : string.Format("TF=1 or {0} like ('%{1}%') or {2} like ('%{1}%')", DBDT.Columns[1].ColumnName, textBox1.Text.Trim(), DBDT.Columns[3].ColumnName), "TF DESC"))
                    {
                        if (!ShowDT.Rows.Contains(dr[2]))
                        {
                            ShowDT.Rows.Add(dr.ItemArray);
                        }
                    }
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            ShowVal();
        }
    }
}
