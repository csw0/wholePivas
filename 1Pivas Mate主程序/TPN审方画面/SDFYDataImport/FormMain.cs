using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms; 
using System.Xml;

namespace SDFYDataImport
{
    public partial class frmMain : Form
    {
        private DataSyn syn = null;

        public frmMain()
        {
            InitializeComponent();
            this.syn = new DataSyn(Application.StartupPath);
        }

        private void loadPatient()
        {
            showProcess("正在加载病人信息...");
            BLPublic.DBOperate db = new BLPublic.DBOperate(Application.StartupPath + @"\bl_server.lcf");
            if (!db.IsConnected)
            {
                showProcess("连接服务器失败:" + db.Error);
                return;
            }

            DataTable tblPatient = null;
            if (!db.GetRecordSet(DataSyn.SEL_ACTTPN_PATIENT, ref tblPatient))
            {
                showProcess("加载TPN患者失败:" + db.Error);
                return;
            }

            ListViewItem item = null;
            foreach (DataRow row in tblPatient.Rows)
            {
                item = lvPatient.Items.Add(row["DeptName"].ToString());
                item.SubItems.Add(row["BedNo"].ToString());
                item.SubItems.Add(row["PatientName"].ToString());
                item.SubItems.Add(row["HospitalNo"].ToString());
            }

            tblPatient.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            txtProc.Text = "开始同步"; 

            syn.OnProcess = onSynProcess;
            syn.synTPNPatientLab(cbSynByTime.Checked);

            button1.Enabled = true;
            txtProc.Text += " 同步结束";
        }

        private void onSynProcess(int _num, int _total)
        {
            showProcess(string.Format("{0}/{1}", _num, _total)); 
        }

        private void showProcess(string _txt)
        {
            txtProc.Text = _txt;
            txtProc.Refresh();
            Application.DoEvents();
        }

        private void button2_Click(object sender, EventArgs e)
        { 
            syn.cancel();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            syn.close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream(1024 * 1000);
            byte[] buff = Encoding.UTF8.GetBytes("<?xml version=\"1.0\" encoding=\"utf-8\" ?><!DOCTYPE ROWSET [<!ENTITY div \"&#47;\">]>");
            ms.Write(buff, 0, buff.Length);
            
            buff = Encoding.UTF8.GetBytes(textBox2.Text.Trim().ToString());
            ms.Write(buff, 0, buff.Length);

            ms.Position = 0;

            XmlTextReader xr4 = new XmlTextReader(ms);  //装载整个文档到内存
            xr4.WhitespaceHandling = WhitespaceHandling.All;    //设置如何处理空白节点
            while (xr4.Read())
            {
                txtProc.Text += (" Type: " + xr4.NodeType).PadRight(20);   //打印节点类型
                txtProc.Text += (" Name: " + xr4.Name).PadRight(18);   //打印节点名称
                txtProc.Text += " Value: " + xr4.Value;    //打印节点的值
                txtProc.Text += "\r\n";
                /*if (xr4.HasAttributes)   //如果当前节点有属性的话(非元素节点的此属性为null)
                {
                    int tmp = xr4.AttributeCount;
                    for (int i = 0; i < tmp; i++) //循环打印元素节点的属性
                    {
                        xr4.MoveToAttribute(i);  //移动到第n个属性上
                        textBox1.Text += (" Type: " + xr4.NodeType).PadRight(20)
                          + (" Name: " + xr4.Name).PadRight(16)
                          + (" value: " + xr4.Value.PadRight(10))
                          + (" AttributeCount: " + xr4.AttributeCount);
                        textBox1.Text += "\r\n";
                        xr4.MoveToElement();  //回到元素节点
                    }
                }*/
            }
            xr4.Close();  //关闭流对象
        }

        private void btnSyn_Click(object sender, EventArgs e)
        { 
            btnSyn.Enabled = false;
            txtProc.Text = "开始同步"; 

            syn.OnProcess = onSynProcess;
            syn.OnMessage = showProcess;

            foreach(ListViewItem item in lvPatient.Items)
                if (item.Checked)
                {
                    syn.synPatient(item.SubItems[3].Text, DateTime.Parse("2018-1-1"));
                    item.Checked = false;
                }

            btnSyn.Enabled = true;
            txtProc.Text += " 同步结束";
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            loadPatient();
        }
    }
}
