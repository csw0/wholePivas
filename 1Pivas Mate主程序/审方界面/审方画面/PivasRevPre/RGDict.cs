using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace PivasRevPre
{
    public partial class RGDict : Form
    {

        public static DB_Help db = new DB_Help();
        private string UserID = string.Empty;
        public string CurrentID = string.Empty;

        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion

        public RGDict()
        {
            InitializeComponent();
        }

        public RGDict(string UserID)
        {
            InitializeComponent();
            this.UserID = UserID;
        }

        private void CPResultRGDict_Load(object sender, EventArgs e)
        {
            ShowDict();
            
        }

        private void ShowDict() 
        {
            listView1.Items.Clear();
            DataSet ds = new DataSet();
            ds = db.GetPIVAsDB("select * from CPResultRGDict ");
            if (ds ==null || ds.Tables[0].Rows.Count <= 0)
                return;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++) 
            {
                ListViewItem lvi = new ListViewItem();
                
                lvi.Text = ds.Tables[0].Rows[i]["ID"].ToString();
                lvi.SubItems.Add(ds.Tables[0].Rows[i]["Name"].ToString());
                listView1.Items.Add(lvi);
            }
            ImageList image = new ImageList();
            image.ImageSize = new Size(1, 30);
            listView1.SmallImageList = image;
            listView1.Items[0].Selected = true;
            CurrentID = listView1.SelectedItems[0].Text.ToString();
            ShowDictDetail(listView1.SelectedItems[0].Text.ToString());
        }



        private void ShowDictDetail(string id) 
        {
           
            listView2.Items.Clear();
            DataSet ds = new DataSet();
            ds = db.GetPIVAsDB("select * from CPResultRGDictDetail where [DictID]="+id);
            if (ds==null || ds.Tables[0].Rows.Count <= 0)
                return;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = ds.Tables[0].Rows[i]["ID"].ToString();
                lvi.SubItems.Add(ds.Tables[0].Rows[i]["Describe"].ToString());
                
                listView2.Items.Add(lvi);
            }
            ImageList image = new ImageList();
            image.ImageSize = new Size(1, 25);
            listView2.SmallImageList = image;
            listView2.Items[0].Selected = true;
        }

      

        private void listView1_Click(object sender, EventArgs e)
        {
            CurrentID = listView1.SelectedItems[0].Text.ToString();

            for (int i = 0; i < listView1.Items.Count; i++) 
            {
                listView1.Items[i].BackColor = Color.White;
                listView1.Items[i].ForeColor = Color.Black;
            }
            listView1.SelectedItems[0].BackColor = Color.FromArgb(36, 94, 220);
            listView1.SelectedItems[0].ForeColor = Color.White;


                ShowDictDetail(listView1.SelectedItems[0].Text.ToString());
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
                return;
            string sql = "delete CPResultRGDict where ID=" + listView1.SelectedItems[0].Text;
            sql += " delete CPResultRGDictDetail where [DictID]=" + listView1.SelectedItems[0].Text;
            db.SetPIVAsDB(sql);
            ShowDict();
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count==0)
            {
               // MessageBox.Show("请选择！");
                return;
            }
            AddDict AUD = new AddDict("1", listView1.SelectedItems[0],CurrentID);
            AUD.ShowDialog();
            if (AUD.DialogResult == DialogResult.OK)
            {
                ShowDict();
            }
        }

        private void 增加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddDict AUD = new AddDict("1",null,CurrentID);
            AUD.ShowDialog();
            if (AUD.DialogResult == DialogResult.OK) 
            {
                ShowDict();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddDict AUD = new AddDict("2", null,CurrentID);
            AUD.ShowDialog();
            if (AUD.DialogResult == DialogResult.OK)
            {
                ShowDictDetail(CurrentID);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count == 0)
            {
               // MessageBox.Show("请选择！");
                return;
            }
            AddDict AUD = new AddDict("2", listView2.SelectedItems[0],CurrentID);
            AUD.ShowDialog();
            if (AUD.DialogResult == DialogResult.OK)
            {
                ShowDictDetail(CurrentID);
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count <= 0)
                return;
           
            string  sql = " delete CPResultRGDictDetail where [ID]=" + listView2.SelectedItems[0].Text;
            db.SetPIVAsDB(sql);
            ShowDictDetail(CurrentID);
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            label2.BackColor = Color.FromArgb(157, 185, 235);
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.BackColor = Color.FromArgb(0, 84, 227);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


       

       


        


    }
}
