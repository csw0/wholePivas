using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Design;
using System.Threading;
using System.IO;
using PIVAsCommon.Helper;

namespace ConsumablesStatic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckA();            
        }
        private string strConsumablesStaticIni = ".\\ConfigFile\\ConsumablesStatic.ini";
       
        private bool locked;

        private void tjToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] str = getSelectHC();
            this.panel1.Visible = true;
            this.textBox4.Text = str[0].ToString();
            this.textBox5.Text = str[1].ToString();
            this.textBox6.Text = str[2].ToString();
            this.textBox7.Text = str[3].ToString();
            this.textBox8.Text = str[4].ToString();
            this.textBox9.Text = str[5].ToString();
            this.textBox10.Text = str[6].ToString();
            this.textBox11.Text = str[7].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IniFileHelper.INIWriteValue(strConsumablesStaticIni,"ConsumablesName", "hc1", this.textBox4.Text);
            IniFileHelper.INIWriteValue(strConsumablesStaticIni, "ConsumablesName", "hc2", this.textBox5.Text);
            IniFileHelper.INIWriteValue(strConsumablesStaticIni, "ConsumablesName", "hc3", this.textBox6.Text);
            IniFileHelper.INIWriteValue(strConsumablesStaticIni, "ConsumablesName", "hc4", this.textBox7.Text);
            IniFileHelper.INIWriteValue(strConsumablesStaticIni, "ConsumablesName", "hc5", this.textBox8.Text);
            IniFileHelper.INIWriteValue(strConsumablesStaticIni, "ConsumablesName", "hc6", this.textBox9.Text);
            IniFileHelper.INIWriteValue(strConsumablesStaticIni, "ConsumablesName", "hc7", this.textBox10.Text);
            IniFileHelper.INIWriteValue(strConsumablesStaticIni, "ConsumablesName", "hc8", this.textBox11.Text);
            this.panel1.Visible = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string[] getSelectHC()
        {
            string hc1 = IniFileHelper.INIGetStringValue(strConsumablesStaticIni, "ConsumablesName", "hc1", string.Empty);
            string hc2 = IniFileHelper.INIGetStringValue(strConsumablesStaticIni, "ConsumablesName", "hc2", string.Empty);
            string hc3 = IniFileHelper.INIGetStringValue(strConsumablesStaticIni, "ConsumablesName", "hc3", string.Empty);
            string hc4 = IniFileHelper.INIGetStringValue(strConsumablesStaticIni, "ConsumablesName", "hc4", string.Empty);
            string hc5 = IniFileHelper.INIGetStringValue(strConsumablesStaticIni, "ConsumablesName", "hc5", string.Empty);
            string hc6 = IniFileHelper.INIGetStringValue(strConsumablesStaticIni, "ConsumablesName", "hc6", string.Empty);
            string hc7 = IniFileHelper.INIGetStringValue(strConsumablesStaticIni, "ConsumablesName", "hc7", string.Empty);
            string hc8 = IniFileHelper.INIGetStringValue(strConsumablesStaticIni, "ConsumablesName", "hc8", string.Empty);

            string PTY = IniFileHelper.INIGetStringValue(strConsumablesStaticIni, "ChargeItem", "PTY", string.Empty);
            string KSS = IniFileHelper.INIGetStringValue(strConsumablesStaticIni, "ChargeItem", "KSS", string.Empty);
            string HLY = IniFileHelper.INIGetStringValue(strConsumablesStaticIni, "ChargeItem", "HLY", string.Empty);
            string YYY = IniFileHelper.INIGetStringValue(strConsumablesStaticIni, "ChargeItem", "YYY", string.Empty);
            string PZL = IniFileHelper.INIGetStringValue(strConsumablesStaticIni, "ChargeItem", "PZL", string.Empty);
            string KZL = IniFileHelper.INIGetStringValue(strConsumablesStaticIni, "ChargeItem", "KZL", string.Empty);
            string HZL = IniFileHelper.INIGetStringValue(strConsumablesStaticIni, "ChargeItem", "HZL", string.Empty);
            string YZL = IniFileHelper.INIGetStringValue(strConsumablesStaticIni, "ChargeItem", "YZL", string.Empty);
            string XL = IniFileHelper.INIGetStringValue(strConsumablesStaticIni, "ChargeItem", "XL", string.Empty);
            string KW = IniFileHelper.INIGetStringValue(strConsumablesStaticIni, "ChargeItem", "KW", string.Empty);
            string XLZL = IniFileHelper.INIGetStringValue(strConsumablesStaticIni, "ChargeItem", "XLZL", string.Empty);
            string KWZL = IniFileHelper.INIGetStringValue(strConsumablesStaticIni, "ChargeItem", "KWZL", string.Empty);

            string[] str = { hc1, hc2, hc3, hc4, hc5, hc6, hc7, hc8, PTY, KSS, HLY, YYY, PZL, KZL, HZL, YZL, XL, KW, XLZL, KWZL };
            return str;
        }

        private string getStr(string date)
        {
            StringBuilder str = new StringBuilder();
            string[] st = getSelectHC();
            str.AppendLine("DECLARE @Static TABLE (ID INT IDENTITY, WN varchar(200) ");
            str.AppendLine(",PA varchar(20),PB varchar(20),PC varchar(20) ");
            str.AppendLine(",KA varchar(20),KB varchar(20),KC varchar(20) ");
            str.AppendLine(",HA varchar(20),HB varchar(20),HC varchar(20) ");
            str.AppendLine(",YA varchar(20),YB varchar(20),YC varchar(20) ");
            str.AppendLine(",KWA varchar(20),KWB varchar(20),KWC varchar(20) ");
            str.AppendLine(",XLA varchar(20),XLB varchar(20),XLC varchar(20) ");
            str.AppendLine(",HZ varchar(20),ZZ varchar(20),JZ varchar(20)) ");
            str.AppendLine("INSERT INTO @Static VALUES ('','','普通药','','','抗生素','','','化疗药','','','营养药','','','卡文','','','血滤','','','总计','') ");
            str.AppendLine("INSERT INTO @Static VALUES ('病区名','份数','材料','治疗费','份数','材料','治疗费','份数','材料','治疗费','份数','材料','治疗费','份数','材料','治疗费','份数','材料','治疗费','材料','治疗费','合计') ");
            str.AppendLine("INSERT INTO @Static ");
            str.AppendLine("select distinct isnull(a.wardName,'未知') as 病区名  ");
            str.AppendLine(",isnull([PA],'0') as [PA],isnull([PB],'0') as [PB],isnull([PC],'0') as [PC]");
            str.AppendLine(",isnull([KA],'0') as [KA],isnull([KB],'0') as [KB],isnull([KC],'0') as [KC]");
            str.AppendLine(",isnull([HA],'0') as [HA],isnull([HB],'0') as [HB],isnull([HC],'0') as [HC]");
            str.AppendLine(",isnull([YA],'0') as [YA],isnull([YB],'0') as [YB],isnull([YC],'0') as [YC]");
            str.AppendLine(",isnull([KWA],'0') as [KWA],isnull([KWB],'0') as [KWB],isnull([KWC],'0') as [KWC]");
            str.AppendLine(",isnull([XLA],'0') as [XLA],isnull([XLB],'0') as [XLB],isnull([XlC],'0') as [XLC]");
            str.AppendLine(",isnull([HZ],'0') as [HZ],isnull([ZZ],'0') as [ZZ],isnull([JZ],'0') as [JZ]");
            str.AppendLine("from ");
            str.AppendLine("(select distinct wardCode, wardName from DWard  where isOpen=1 ) a ");
            str.AppendLine("left join (select distinct i.WardCode,convert(varchar,COUNT(1)) as PA , convert(varchar,COUNT(1) * " + Convert.ToDouble(st[8]) + ") as PB, convert(varchar,COUNT(1) * " + Convert.ToDouble(st[12]) + ") as PC from  IVRecord i INNER JOIN Prescription p on i.PrescriptionID = p.PrescriptionID inner join IVRecord_DB db ON i.LabelNo = db.IVRecordID inner join DWard DE on DE.WardCode = i.WardCode where  (DBDT " + date + ") and p.DrugType = 1 group by i.WardCode) w on a.wardcode=w.wardcode ");
            str.AppendLine("left join (select distinct i.WardCode,convert(varchar,COUNT(1)) as KA , convert(varchar,COUNT(1) * " + Convert.ToDouble(st[9]) + ") as KB, convert(varchar,COUNT(1) * " + Convert.ToDouble(st[13]) + ") as KC from  IVRecord i INNER JOIN Prescription p on i.PrescriptionID = p.PrescriptionID inner join IVRecord_DB db ON i.LabelNo = db.IVRecordID inner join DWard DE on DE.WardCode = i.WardCode where  (DBDT " + date + ") and p.DrugType = 2 group by i.WardCode) x on a.wardcode=x.wardcode ");
            str.AppendLine("left join (select distinct i.WardCode,convert(varchar,COUNT(1)) as HA , convert(varchar,COUNT(1) * " + Convert.ToDouble(st[10]) + ") as HB, convert(varchar,COUNT(1) * " + Convert.ToDouble(st[14]) + ") as HC from IVRecord i INNER JOIN Prescription p on i.PrescriptionID = p.PrescriptionID inner join IVRecord_DB db ON i.LabelNo = db.IVRecordID inner join DWard DE on DE.WardCode = i.WardCode where  (DBDT " + date + ") and p.DrugType = 3 group by i.WardCode) y on a.wardcode=y.wardcode ");
            str.AppendLine("left join (select distinct i.WardCode,convert(varchar,COUNT(1)) as YA , convert(varchar,COUNT(1) * " + Convert.ToDouble(st[11]) + ") as YB, convert(varchar,COUNT(1) * " + Convert.ToDouble(st[15]) + ") as YC from IVRecord i INNER JOIN Prescription p on i.PrescriptionID = p.PrescriptionID inner join IVRecord_DB db ON i.LabelNo = db.IVRecordID inner join DWard DE on DE.WardCode = i.WardCode where  (DBDT " + date + ") and p.DrugType = 4 AND p.UsageCode <> '血液净化用' group by i.WardCode) z on a.wardcode=z.wardcode ");
            str.AppendLine("left join (select distinct i.WardCode,convert(varchar,COUNT(1)) as KWA , convert(varchar,COUNT(1) * " + Convert.ToDouble(st[16]) + ") as KWB, convert(varchar,COUNT(1) * " + Convert.ToDouble(st[18]) + ") as KWC from IVRecord i INNER JOIN Prescription p on i.PrescriptionID = p.PrescriptionID inner join IVRecord_DB db ON i.LabelNo = db.IVRecordID inner join DWard DE on DE.WardCode = i.WardCode where  (DBDT " + date + ") and p.DrugType = 0 group by i.WardCode) o on a.wardcode=o.wardcode ");
            str.AppendLine("left join (select distinct i.WardCode,convert(varchar,COUNT(1)) as XLA , convert(varchar,COUNT(1) * " + Convert.ToDouble(st[17]) + ") as XLB, convert(varchar,COUNT(1) * " + Convert.ToDouble(st[19]) + ") as XLC from IVRecord i INNER JOIN Prescription p on i.PrescriptionID = p.PrescriptionID inner join IVRecord_DB db ON i.LabelNo = db.IVRecordID inner join DWard DE on DE.WardCode = i.WardCode where  (DBDT " + date + ") and p.UsageCode = '血液净化用' group by i.WardCode) p on a.wardcode=p.wardcode ");
            str.AppendLine("left join (select distinct i.WardCode,convert(varchar,COUNT(1)) as HZ , convert(varchar,COUNT(1) * 2) as ZZ, convert(varchar,COUNT(1) * 3) as JZ from IVRecord i INNER JOIN Prescription p on i.PrescriptionID = p.PrescriptionID inner join IVRecord_DB db ON i.LabelNo = db.IVRecordID inner join DWard DE on DE.WardCode = i.WardCode where  (DBDT " + date + ") group by i.WardCode) m on a.wardcode=m.wardcode ");
            str.AppendLine(" UPDATE @Static SET HZ = CONVERT(float,[PB]) + CONVERT(float,[KB]) + CONVERT(float,[HB]) + CONVERT(float,[YB]) + CONVERT(float,[KWB]) + CONVERT(float,[XlB]), ZZ = CONVERT(float,[PC]) + CONVERT(float,[KC]) + CONVERT(float,[HC]) + CONVERT(float,[YC]) + CONVERT(float,[KWC]) + CONVERT(float,[XLC]) WHERE ID > 2");
            str.AppendLine(" UPDATE @Static SET JZ = CONVERT(float,[HZ]) + CONVERT(float,[ZZ]) WHERE ID > 2");
            str.AppendLine("  INSERT INTO @Static SELECT '<总计>' ");
            str.AppendLine(",CONVERT(varchar,SUM(CONVERT(int,[PA]))),CONVERT(varchar,SUM(CONVERT(float,[PB]))),CONVERT(varchar,SUM(CONVERT(float,[PC]))) ");
            str.AppendLine(",CONVERT(varchar,SUM(CONVERT(int,[KA]))),CONVERT(varchar,SUM(CONVERT(float,[KB]))),CONVERT(varchar,SUM(CONVERT(float,[KC]))) ");
            str.AppendLine(",CONVERT(varchar,SUM(CONVERT(int,[HA]))),CONVERT(varchar,SUM(CONVERT(float,[HB]))),CONVERT(varchar,SUM(CONVERT(float,[HC]))) ");
            str.AppendLine(",CONVERT(varchar,SUM(CONVERT(int,[YA]))),CONVERT(varchar,SUM(CONVERT(float,[YB]))),CONVERT(varchar,SUM(CONVERT(float,[YC]))) ");
            str.AppendLine(",CONVERT(varchar,SUM(CONVERT(int,[KWA]))),CONVERT(varchar,SUM(CONVERT(float,[KWB]))),CONVERT(varchar,SUM(CONVERT(float,[KWC]))) ");
            str.AppendLine(",CONVERT(varchar,SUM(CONVERT(int,[KWA]))),CONVERT(varchar,SUM(CONVERT(float,[KWB]))),CONVERT(varchar,SUM(CONVERT(float,[KWC]))) ");
            str.AppendLine(",CONVERT(varchar,SUM(CONVERT(float,[HZ]))),CONVERT(varchar,SUM(CONVERT(float,[ZZ]))),CONVERT(varchar,SUM(CONVERT(float,[JZ]))) ");
            str.AppendLine(" FROM @Static WHERE ID > 2");
            str.AppendLine(" SELECT * FROM @Static");
            return str.ToString();
        }

        private void CheckA()
        {
            if (this.szToolStripMenuItem.Checked)
            {
                this.ptToolStripMenuItem.Checked = true;
                this.kssToolStripMenuItem.Checked = true;
                this.hlToolStripMenuItem.Checked = true;
                this.tpnToolStripMenuItem.Checked = true;
                this.xlToolStripMenuItem.Checked = true;
                this.kwToolStripMenuItem.Checked = true;
            }
            else
            {
                this.ptToolStripMenuItem.Checked = false;
                this.kssToolStripMenuItem.Checked = false;
                this.hlToolStripMenuItem.Checked = false;
                this.tpnToolStripMenuItem.Checked = false;
                this.xlToolStripMenuItem.Checked = false;
                this.kwToolStripMenuItem.Checked = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] str = getSelectHC();
            this.checkBox1.Text = str[0].ToString();
            this.checkBox2.Text = str[1].ToString();
            this.checkBox3.Text = str[2].ToString();
            this.checkBox4.Text = str[3].ToString();
            this.checkBox5.Text = str[4].ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                this.textBox1.Visible = true;
                this.textBox20.Visible = true;
            }
            else
            {
                this.textBox1.Text = "";
                this.textBox20.Text = "";
                this.textBox1.Visible = false;
                this.textBox20.Visible = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox2.Checked)
            {
                this.label3.Visible = true;
                this.textBox2.Visible = true;
                this.label14.Visible = true;
                this.textBox19.Visible = true;
            }
            else
            {
                this.textBox2.Text = "";
                this.textBox19.Text = "";
                this.label3.Visible = false;
                this.textBox2.Visible = false;
                this.label14.Visible = false;
                this.textBox19.Visible = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox3.Checked)
            {
                this.label4.Visible = true;
                this.textBox3.Visible = true;
                this.label13.Visible = true;
                this.textBox18.Visible = true;
            }
            else
            {
                this.textBox3.Text = "";
                this.textBox18.Text = "";
                this.label4.Visible = false;
                this.textBox3.Visible = false;
                this.label13.Visible = false;
                this.textBox18.Visible = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox4.Checked)
            {
                this.label5.Visible = true;
                this.textBox12.Visible = true;
                this.label12.Visible = true;
                this.textBox17.Visible = true;
            }
            else
            {
                this.textBox12.Text = "";
                this.textBox17.Text = "";
                this.label5.Visible = false;
                this.textBox12.Visible = false;
                this.label12.Visible = false;
                this.textBox17.Visible = false;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox5.Checked)
            {
                this.label6.Visible = true;
                this.textBox13.Visible = true;
                this.label11.Visible = true;
                this.textBox16.Visible = true;
            }
            else
            {
                this.textBox13.Text = "";
                this.textBox16.Text = "";
                this.label6.Visible = false;
                this.textBox13.Visible = false;
                this.label11.Visible = false;
                this.textBox16.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.label7.Visible = true;
            this.label10.Visible = true;
            this.textBox14.Visible = true;
            this.textBox15.Visible = true;
            Double a = 0.0;
            Double b = 0.0;
            Double c = 0.0;
            Double d = 0.0;
            Double f = 0.0;
            Double o = 0.0;
            Double p = 0.0;
            Double q = 0.0;
            Double r = 0.0;
            Double s = 0.0;
            if ("" == this.textBox1.Text)
                a = 0.0;
            else
                a = Convert.ToDouble(this.textBox1.Text);
            if ("" == this.textBox2.Text)
                b = 0.0;
            else
                b = Convert.ToDouble(this.textBox2.Text);
            if ("" == this.textBox3.Text)
                c = 0.0;
            else
                c = Convert.ToDouble(this.textBox3.Text);
            if ("" == this.textBox12.Text)
                d = 0.0;
            else
                d = Convert.ToDouble(this.textBox12.Text);
            if ("" == this.textBox13.Text)
                f = 0.0;
            else
                f = Convert.ToDouble(this.textBox13.Text);
            this.textBox14.Text = (a + b + c + d + f).ToString();
            if (this.radioButton1.Checked)
                IniFileHelper.INIWriteValue(strConsumablesStaticIni,"ChargeItem", "PTY", this.textBox14.Text);
            else if (this.radioButton2.Checked)
                IniFileHelper.INIWriteValue(strConsumablesStaticIni, "ChargeItem", "KSS", this.textBox14.Text);
            else if (this.radioButton3.Checked)
                IniFileHelper.INIWriteValue(strConsumablesStaticIni, "ChargeItem", "HLY", this.textBox14.Text);
            else if (this.radioButton4.Checked)
                IniFileHelper.INIWriteValue(strConsumablesStaticIni, "ChargeItem", "YYY", this.textBox14.Text);
            else if (this.radioButton5.Checked)
                IniFileHelper.INIWriteValue(strConsumablesStaticIni, "ChargeItem", "XL", this.textBox14.Text);
            else
                IniFileHelper.INIWriteValue(strConsumablesStaticIni, "ChargeItem", "KW", this.textBox14.Text);

            if ("" == this.textBox20.Text)
                o = 0.0;
            else
                o = Convert.ToDouble(this.textBox20.Text);
            if ("" == this.textBox19.Text)
                p = 0.0;
            else
                p = Convert.ToDouble(this.textBox19.Text);
            if ("" == this.textBox18.Text)
                q = 0.0;
            else
                q = Convert.ToDouble(this.textBox18.Text);
            if ("" == this.textBox17.Text)
                r = 0.0;
            else
                r = Convert.ToDouble(this.textBox17.Text);
            if ("" == this.textBox16.Text)
                s = 0.0;
            else
                s = Convert.ToDouble(this.textBox16.Text);
            this.textBox15.Text = (o + p + q + r + s).ToString();
            if (this.radioButton1.Checked)
                IniFileHelper.INIWriteValue(strConsumablesStaticIni,"ChargeItem", "PZL", this.textBox14.Text);
            else if (this.radioButton2.Checked)
                IniFileHelper.INIWriteValue(strConsumablesStaticIni, "ChargeItem", "KZL", this.textBox14.Text);
            else if (this.radioButton3.Checked)
                IniFileHelper.INIWriteValue(strConsumablesStaticIni, "ChargeItem", "HZL", this.textBox14.Text);
            else if(this.radioButton4.Checked)
                IniFileHelper.INIWriteValue(strConsumablesStaticIni, "ChargeItem", "YZL", this.textBox14.Text);
            else if (this.radioButton5.Checked)
                IniFileHelper.INIWriteValue(strConsumablesStaticIni, "ChargeItem", "XLZL", this.textBox14.Text);
            else
                IniFileHelper.INIWriteValue(strConsumablesStaticIni, "ChargeItem", "KWZL", this.textBox14.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataBase db = new DataBase();
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Refresh();
            string date = "between '" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00.000") + "' and '" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59.000") + "'";
            string sel = getStr(date);

            if (!locked)
            {
                locked = true;
                Thread th = new Thread(() => { new WaitForm().ShowDialog(); });
                th.IsBackground = true;
                th.Start();
                try
                {
                    this.dataGridView1.DataSource = db.getCount(sel);
                    BackColors();
                }
                catch
                {

                }
                finally
                {
                    th.Abort();
                    locked = false;
                }
            }
        }

        private void BackColors()
        {
            this.dataGridView1.Columns[0].Visible = false;
            this.dataGridView1.Columns[1].Width = 180;
            this.dataGridView1.Columns[1].DefaultCellStyle.BackColor = Color.LightCyan;
            this.dataGridView1.Columns[2].DefaultCellStyle.BackColor = Color.Beige;
            this.dataGridView1.Columns[3].DefaultCellStyle.BackColor = Color.Beige;
            this.dataGridView1.Columns[4].DefaultCellStyle.BackColor = Color.Beige;
            this.dataGridView1.Columns[5].DefaultCellStyle.BackColor = Color.LightCyan;
            this.dataGridView1.Columns[6].DefaultCellStyle.BackColor = Color.LightCyan;
            this.dataGridView1.Columns[7].DefaultCellStyle.BackColor = Color.LightCyan;
            this.dataGridView1.Columns[8].DefaultCellStyle.BackColor = Color.HotPink;
            this.dataGridView1.Columns[9].DefaultCellStyle.BackColor = Color.HotPink;
            this.dataGridView1.Columns[10].DefaultCellStyle.BackColor = Color.HotPink;
            this.dataGridView1.Columns[11].DefaultCellStyle.BackColor = Color.LightGreen;
            this.dataGridView1.Columns[12].DefaultCellStyle.BackColor = Color.LightGreen;
            this.dataGridView1.Columns[13].DefaultCellStyle.BackColor = Color.LightGreen;
            this.dataGridView1.Columns[14].DefaultCellStyle.BackColor = Color.MistyRose;
            this.dataGridView1.Columns[15].DefaultCellStyle.BackColor = Color.MistyRose;
            this.dataGridView1.Columns[16].DefaultCellStyle.BackColor = Color.MistyRose;
            this.dataGridView1.Columns[17].DefaultCellStyle.BackColor = Color.PaleGreen;
            this.dataGridView1.Columns[18].DefaultCellStyle.BackColor = Color.PaleGreen;
            this.dataGridView1.Columns[19].DefaultCellStyle.BackColor = Color.PaleGreen;
            this.dataGridView1.Columns[20].DefaultCellStyle.BackColor = Color.LightCyan;
            this.dataGridView1.Columns[21].DefaultCellStyle.BackColor = Color.LightCoral;
            this.dataGridView1.Columns[22].DefaultCellStyle.BackColor = Color.LightBlue;
        }

        private void tcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void DataToExcel(DataGridView m_DataView)
        {
            SaveFileDialog kk = new SaveFileDialog();
            kk.Title = "工作量统计" + DateTime.Now.ToString("yyyMMdd");
            kk.Filter = "EXECL文件(*.xls) |*.xls |所有文件(*.*) |*.*";
            kk.FilterIndex = 1;
            if (kk.ShowDialog() == DialogResult.OK)
            {
                string FileName = kk.FileName;
                if (File.Exists(FileName))
                    File.Delete(FileName);
                FileStream objFileStream;
                StreamWriter objStreamWriter;
                string strLine = "";
                objFileStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write);
                objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);
                for (int i = 0; i < m_DataView.Columns.Count; i++)
                {
                    if (m_DataView.Columns[i].Visible == true)
                    {
                        strLine = strLine + m_DataView.Columns[i].HeaderText.ToString() + Convert.ToChar(9);
                    }
                }
                objStreamWriter.WriteLine(strLine);
                strLine = "";

                for (int i = 0; i < m_DataView.Rows.Count; i++)
                {
                    if (m_DataView.Columns[0].Visible == true)
                    {
                        if (m_DataView.Rows[i].Cells[0].Value == null)
                            strLine = strLine + " " + Convert.ToChar(9);
                        else
                            strLine = strLine + m_DataView.Rows[i].Cells[0].Value.ToString() + Convert.ToChar(9);
                    }
                    for (int j = 1; j < m_DataView.Columns.Count; j++)
                    {
                        if (m_DataView.Columns[j].Visible == true)
                        {
                            if (m_DataView.Rows[i].Cells[j].Value == null)
                                strLine = strLine + " " + Convert.ToChar(9);
                            else
                            {
                                string rowstr = "";
                                rowstr = m_DataView.Rows[i].Cells[j].Value.ToString();
                                if (rowstr.IndexOf("\r\n") > 0)
                                    rowstr = rowstr.Replace("\r\n", " ");
                                if (rowstr.IndexOf("\t") > 0)
                                    rowstr = rowstr.Replace("\t", " ");
                                strLine = strLine + rowstr + Convert.ToChar(9);
                            }
                        }
                    }
                    objStreamWriter.WriteLine(strLine);
                    strLine = "";
                }
                objStreamWriter.Close();
                objFileStream.Close();
                MessageBox.Show(this, "保存EXCEL成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataToExcel(this.dataGridView1);
        }
    }


    public partial class RowMergeView : DataGridView
    {
        #region 构造函数
        public RowMergeView()
        {

        }
        #endregion
        #region 重写的事件
        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: 在此处添加自定义绘制代码

            // 调用基类 OnPaint
            base.OnPaint(pe);
        }


        protected override void OnScroll(ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                ReDrawHead();
            }
            base.OnScroll(e);
        }
        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    DrawCell(e);
                }
                else
                {
                    //二维表头
                    if (e.RowIndex == -1)
                    {
                        if (SpanRows.ContainsKey(e.ColumnIndex)) //被合并的列
                        {
                            //画边框
                            Graphics g = e.Graphics;
                            e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);

                            int left = e.CellBounds.Left, top = e.CellBounds.Top + 2,
                            right = e.CellBounds.Right, bottom = e.CellBounds.Bottom;

                            switch (SpanRows[e.ColumnIndex].Position)
                            {
                                case 1:
                                    left += 2;
                                    break;
                                case 2:
                                    break;
                                case 3:
                                    right -= 2;
                                    break;
                            }

                            //画上半部分底色
                            g.FillRectangle(new SolidBrush(this._mergecolumnheaderbackcolor), left, top,
                            right - left, (bottom - top) / 2);

                            //画中线
                            g.DrawLine(new Pen(this.GridColor), left, (top + bottom) / 2,
                            right, (top + bottom) / 2);

                            //写小标题
                            StringFormat sf = new StringFormat();
                            sf.Alignment = StringAlignment.Center;
                            sf.LineAlignment = StringAlignment.Center;

                            g.DrawString(e.Value + "", e.CellStyle.Font, Brushes.Black,
                            new Rectangle(left, (top + bottom) / 2, right - left, (bottom - top) / 2), sf);
                            left = this.GetColumnDisplayRectangle(SpanRows[e.ColumnIndex].Left, true).Left - 2;

                            if (left < 0) left = this.GetCellDisplayRectangle(-1, -1, true).Width;
                            right = this.GetColumnDisplayRectangle(SpanRows[e.ColumnIndex].Right, true).Right - 2;
                            if (right < 0) right = this.Width;

                            g.DrawString(SpanRows[e.ColumnIndex].Text, e.CellStyle.Font, Brushes.Black,
                            new Rectangle(left, top, right - left, (bottom - top) / 2), sf);
                            e.Handled = true;
                        }
                    }
                }
                base.OnCellPainting(e);
            }
            catch
            { }
        }
        protected override void OnCellClick(DataGridViewCellEventArgs e)
        {
            base.OnCellClick(e);
        }
        #endregion
        #region 自定义方法
        /// <summary>
        /// 画单元格
        /// </summary>
        /// <param name="e"></param>
        private void DrawCell(DataGridViewCellPaintingEventArgs e)
        {
            if (e.CellStyle.Alignment == DataGridViewContentAlignment.NotSet)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            Brush gridBrush = new SolidBrush(this.GridColor);
            SolidBrush backBrush = new SolidBrush(e.CellStyle.BackColor);
            SolidBrush fontBrush = new SolidBrush(e.CellStyle.ForeColor);
            int cellwidth;
            //上面相同的行数
            int UpRows = 0;
            //下面相同的行数
            int DownRows = 0;
            //总行数
            int count = 0;
            if (this.MergeColumnNames.Contains(this.Columns[e.ColumnIndex].Name) && e.RowIndex != -1)
            {
                cellwidth = e.CellBounds.Width;
                Pen gridLinePen = new Pen(gridBrush);
                string curValue = e.Value == null ? "" : e.Value.ToString().Trim();
                string curSelected = this.CurrentRow.Cells[e.ColumnIndex].Value == null ? "" : this.CurrentRow.Cells[e.ColumnIndex].Value.ToString().Trim();
                if (!string.IsNullOrEmpty(curValue))
                {
                    #region 获取下面的行数
                    for (int i = e.RowIndex; i < this.Rows.Count; i++)
                    {
                        if (this.Rows[i].Cells[e.ColumnIndex].Value.ToString().Equals(curValue))
                        {
                            //this.Rows[i].Cells[e.ColumnIndex].Selected = this.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected;

                            DownRows++;
                            if (e.RowIndex != i)
                            {
                                cellwidth = cellwidth < this.Rows[i].Cells[e.ColumnIndex].Size.Width ? cellwidth : this.Rows[i].Cells[e.ColumnIndex].Size.Width;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion
                    #region 获取上面的行数
                    for (int i = e.RowIndex; i >= 0; i--)
                    {
                        if (this.Rows[i].Cells[e.ColumnIndex].Value.ToString().Equals(curValue))
                        {
                            //this.Rows[i].Cells[e.ColumnIndex].Selected = this.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected;
                            UpRows++;
                            if (e.RowIndex != i)
                            {
                                cellwidth = cellwidth < this.Rows[i].Cells[e.ColumnIndex].Size.Width ? cellwidth : this.Rows[i].Cells[e.ColumnIndex].Size.Width;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion
                    count = DownRows + UpRows - 1;
                    if (count < 2)
                    {
                        return;
                    }
                }
                if (this.Rows[e.RowIndex].Selected)
                {
                    backBrush.Color = e.CellStyle.SelectionBackColor;
                    fontBrush.Color = e.CellStyle.SelectionForeColor;
                }
                //以背景色填充
                e.Graphics.FillRectangle(backBrush, e.CellBounds);
                //画字符串
                PaintingFont(e, cellwidth, UpRows, DownRows, count);
                if (DownRows == 1)
                {
                    e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                    count = 0;
                }
                // 画右边线
                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);

                e.Handled = true;
            }
        }
        /// <summary>
        /// 画字符串
        /// </summary>
        /// <param name="e"></param>
        /// <param name="cellwidth"></param>
        /// <param name="UpRows"></param>
        /// <param name="DownRows"></param>
        /// <param name="count"></param>
        private void PaintingFont(System.Windows.Forms.DataGridViewCellPaintingEventArgs e, int cellwidth, int UpRows, int DownRows, int count)
        {
            SolidBrush fontBrush = new SolidBrush(e.CellStyle.ForeColor);
            int fontheight = (int)e.Graphics.MeasureString(e.Value.ToString(), e.CellStyle.Font).Height;
            int fontwidth = (int)e.Graphics.MeasureString(e.Value.ToString(), e.CellStyle.Font).Width;
            int cellheight = e.CellBounds.Height;

            if (e.CellStyle.Alignment == DataGridViewContentAlignment.BottomCenter)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + (cellwidth - fontwidth) / 2, e.CellBounds.Y + cellheight * DownRows - fontheight);
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.BottomLeft)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X, e.CellBounds.Y + cellheight * DownRows - fontheight);
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.BottomRight)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + cellwidth - fontwidth, e.CellBounds.Y + cellheight * DownRows - fontheight);
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.MiddleCenter)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + (cellwidth - fontwidth) / 2, e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2);
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.MiddleLeft)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X, e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2);
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.MiddleRight)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + cellwidth - fontwidth, e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2);
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.TopCenter)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + (cellwidth - fontwidth) / 2, e.CellBounds.Y - cellheight * (UpRows - 1));
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.TopLeft)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X, e.CellBounds.Y - cellheight * (UpRows - 1));
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.TopRight)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + cellwidth - fontwidth, e.CellBounds.Y - cellheight * (UpRows - 1));
            }
            else
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + (cellwidth - fontwidth) / 2, e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2);
            }
        }
        #endregion
        #region 属性
        /// <summary>
        /// 设置或获取合并列的集合
        /// </summary>
        [MergableProperty(false)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        [Localizable(true)]
        [Description("设置或获取合并列的集合"), Browsable(true), Category("单元格合并")]
        public List<string> MergeColumnNames
        {
            get
            {
                return _mergecolumnname;
            }
            set
            {
                _mergecolumnname = value;
            }
        }
        private List<string> _mergecolumnname = new List<string>();
        #endregion
        #region 二维表头
        private struct SpanInfo //表头信息
        {
            public SpanInfo(string Text, int Position, int Left, int Right)
            {
                this.Text = Text;
                this.Position = Position;
                this.Left = Left;
                this.Right = Right;
            }

            public string Text; //列主标题
            public int Position; //位置，1:左，2中，3右
            public int Left; //对应左行
            public int Right; //对应右行
        }



        private Dictionary<int, SpanInfo> SpanRows = new Dictionary<int, SpanInfo>();//需要2维表头的列
        /// <summary>
        /// 合并列
        /// </summary>
        /// <param name="ColIndex">列的索引</param>
        /// <param name="ColCount">需要合并的列数</param>
        /// <param name="Text">合并列后的文本</param>
        public void AddSpanHeader(int ColIndex, int ColCount, string Text)
        {
            if (ColCount < 2)
            {
                throw new Exception("行宽应大于等于2，合并1列无意义。");
            }
            //将这些列加入列表
            int Right = ColIndex + ColCount - 1; //同一大标题下的最后一列的索引
            SpanRows[ColIndex] = new SpanInfo(Text, 1, ColIndex, Right); //添加标题下的最左列
            SpanRows[Right] = new SpanInfo(Text, 3, ColIndex, Right); //添加该标题下的最右列
            for (int i = ColIndex + 1; i < Right; i++) //中间的列
            {
                SpanRows[i] = new SpanInfo(Text, 2, ColIndex, Right);
            }
        }
        /// <summary>
        /// 清除合并的列
        /// </summary>
        public void ClearSpanInfo()
        {
            SpanRows.Clear();
            //ReDrawHead();
        }

        //刷新显示表头
        public void ReDrawHead()
        {
            try
            {
                foreach (int si in SpanRows.Keys)
                {
                    this.Invalidate(this.GetCellDisplayRectangle(si, -1, true));
                }
            }
            catch { }
        }

        /// <summary>
        /// 二维表头的背景颜色
        /// </summary>
        [Description("二维表头的背景颜色"), Browsable(true), Category("二维表头")]
        public Color MergeColumnHeaderBackColor
        {
            get { return this._mergecolumnheaderbackcolor; }
            set { this._mergecolumnheaderbackcolor = value; }
        }
        private Color _mergecolumnheaderbackcolor = System.Drawing.SystemColors.Control;
        #endregion
    }
}
