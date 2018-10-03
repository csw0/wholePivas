using PIVAsCommon;
using PIVAsCommon.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace PivasIVRPrint
{
    public partial class UserControlPrint : UserControl, IMenuManager
    {
        protected internal static int PrintNumber;
        protected internal static int PreviewMode;
        protected internal static bool WardIdle;
        protected internal static bool WardOpen;
        protected internal static bool DrugList;
        protected internal static bool PrintOrderCount;
        protected internal static int PrintDrugCount;
        protected internal static bool Positions;
        protected internal static bool Species;
        protected internal static bool UP2;
        protected internal static bool PrintOverCheck;
        protected internal static bool CheckDrugLimit;
        protected internal static Color NotPrint;
        protected internal static Color NotPrintSelected;
        protected internal static Color Printed;
        protected internal static Color PrintSelected;
        protected internal static Color HasCheck;
        protected internal static Color HasCheckSelected;
        protected internal string LabelNoPrint = string.Empty;
        protected internal string CountPrint = string.Empty;
        protected internal string NextDay = string.Empty;
        protected internal string ad = string.Empty;
        protected internal string userID = string.Empty;
        protected internal string selected = string.Empty;
        protected internal string DrugCodes = string.Empty;
        protected internal string textValue = string.Empty;
        protected internal string OrderID = string.Empty;
        protected internal string isJustOne = string.Empty;
        protected internal string ByUsageCode = string.Empty;
        protected internal string Specs = string.Empty;
        protected internal string Position = string.Empty;
        protected internal string ArrDrugUserCode = string.Empty;
        protected internal string PZDrugUserCode = string.Empty;
        protected internal string PackDrugUserCode = string.Empty;
        protected internal string NowST = string.Empty;
        protected internal string BatS = string.Empty;
        protected internal string UsageCodeS = string.Empty;
        protected internal string showprint = string.Empty;
        protected internal string GetLabelNoSql = string.Empty;
        protected internal bool HasLoad = false;
        protected internal bool printed = false;
        protected internal bool SendPrt = true;
        protected internal bool printDgT = true;
        protected internal Dictionary<string, bool> prints;
        protected internal BQlabelDetail BD;
        protected internal BQlabelShow BS;
        protected internal DB_Help dbHelp = new DB_Help();
        protected internal int stalimt;
        protected internal int endlimt;
        protected internal int PrintRD;
        protected internal List<string> Menstruum = new List<string>();
        protected internal string WhyRePrint;
        protected internal DataSet DrugColor = new DataSet();
        protected internal DataTable Usage = new DataTable();
        protected internal DataTable Batch = new DataTable();
        private bool ischange;
        private bool ischange2;
        private bool timechage;
        private int combox1;
        private int combox2;
        private int combox3;
        private int combox4;
        private int combox5;
        private int combox6;
        private int combox7;
        private string MustSelectUser = string.Empty;
        private string WardCodes = string.Empty;
        private string IsUseCheck = string.Empty;
        private DataTable lt = new DataTable();
        private DataTable DDrug = new DataTable();
        private DataTable Drugs = new DataTable();
        private SqlDataAdapter sda;
        public string Useful1 = string.Empty;
        public string Useful2 = string.Empty;
        public string Useful3 = string.Empty;
        public string Useless1 = string.Empty;
        public string Useless2 = string.Empty;
        public string Useless3 = string.Empty;

        public UserControlPrint(string ID, string str, string str2)
        {
            StringBuilder SB = new StringBuilder();
            SB.AppendLine("IF NOT EXISTS(SELECT 1 FROM[dbo].[PivasAllSet] WHERE Pro = '打印-瓶签视图') BEGIN ");
            SB.AppendLine("ALTER TABLE [dbo].[PivasAllSet] ALTER COLUMN [Value] NVARCHAR(4000) ");
            SB.AppendLine("INSERT INTO [dbo].[PivasAllSet] VALUES('打印-瓶签视图','{0}','瓶签界面查询sql自定义可以加表连接，加字段，加条件，可修改as前的字段值，不可修改as后的别名，不能删除已用表和字段','','--','--',(SELECT MAX(OrdBy)+1 FROM [PivasAllSet])) END ");
            SB.AppendLine("SELECT [Value] FROM [dbo].[PivasAllSet] WHERE Pro='打印-瓶签视图' ");
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT ");
            SQL.AppendLine("iv.Batch,");
            SQL.AppendLine("iv.LabelNo,");
            SQL.AppendLine("iv.InfusionDT,");
            SQL.AppendLine("iv.IVStatus,");
            SQL.AppendLine("iv.FreqCode,");
            SQL.AppendLine("convert(varchar,iv.PrintDT,120)PrintDT,");
            SQL.AppendLine("iv.PrinterName,");
            SQL.AppendLine("iv.TeamNumber,");
            SQL.AppendLine("iv.MarjorDrug,");
            SQL.AppendLine("iv.Menstruum,");
            SQL.AppendLine("iv.FreqNum,");
            SQL.AppendLine("iv.Sex,");
            SQL.AppendLine("iv.Age,");
            SQL.AppendLine("iv.IVRecordID,");
            SQL.AppendLine("isnull(pat.PatName,iv.PatName) PatName,");
            SQL.AppendLine("isnull(pat.BedNo,iv.BedNo) BedNo,");
            SQL.AppendLine("iv.DeskNo,");
            SQL.AppendLine("iv.Remark1,");
            SQL.AppendLine("iv.PrintDT as Remark2,");
            SQL.AppendLine("iv.Remark3,");
            SQL.AppendLine("DrugType Remark4,");
            SQL.AppendLine("iv.Remark5,");
            SQL.AppendLine("iv.Remark6,");
            SQL.AppendLine("ivd.Remark7,");
            SQL.AppendLine("ivd.Remark8,");
            SQL.AppendLine("case ivd.DosageUnit when ''ml'' then ivd.Dosage when ''l'' then(1000 * ivd.Dosage) else isnull(dd.Capacity,0) * pd.Quantity end Remark9,");
            SQL.AppendLine("ivd.Remark10,");
            SQL.AppendLine("ivd.DosageUnit,");
            SQL.AppendLine("ivd.Dosage,");
            SQL.AppendLine("ivd.Spec,");
            SQL.AppendLine("ivd.DrugName,");
            SQL.AppendLine("ivd.DrugCode,");
            SQL.AppendLine("pre.PatientCode,");
            SQL.AppendLine("pre.CaseID,");
            SQL.AppendLine("pre.UsageCode,");
            SQL.AppendLine("pre.FregCode,");
            SQL.AppendLine("pre.DrugCount,");
            SQL.AppendLine("pre.DrugType,");
            SQL.AppendLine("isnull(pat.WardCode,iv.WardCode)WardCode,");
            SQL.AppendLine("DgNO DosageSum,");
            SQL.AppendLine("Quantity DosageSums,");
            SQL.AppendLine("isnull((select DEmployeeCode from DEmployee where DEmployeeID =(SELECT max(cp.CheckDCode) FROM CPRecord cp where cp.PrescriptionID = iv.PrescriptionID)),''1'')as CpUser,");
            SQL.AppendLine("(SELECT DEmployeeName FROM DEmployee WHERE DEmployeeCode = DoctorCode) DEmployeeName,");
            SQL.AppendLine("(SELECT TimesOfDay FROM DFreq AS df WHERE df.FreqCode = PRE.FregCode) AS tod,");
            SQL.AppendLine("pd.StartDT,");
            SQL.AppendLine("pd.EndDT,");
            SQL.AppendLine("dd.Species,");
            SQL.AppendLine("dd.PiShi,");
            SQL.AppendLine("ISNULL(QuantityUnit, dd.FormUnit) as FormUnit,");
            SQL.AppendLine("dd.IsMenstruum,");
            SQL.AppendLine("dd.NoName,");
            SQL.AppendLine("dw.WardName AS WardName,");
            SQL.AppendLine("dw.WardSimName,");
            SQL.AppendLine("pat.AgeSTR,");
            SQL.AppendLine("pat.Weight ");
            SQL.AppendLine("FROM IVRecordDetail AS ivd INNER JOIN IVRecord as iv on iv.IVRecordID = ivd.IVRecordID AND BatchSaved = 1 AND LabelOver > case when IVStatus = 0 then - 1 else -3 end ");
            SQL.AppendLine("LEFT JOIN Prescription as pre ON pre.PrescriptionID = iv.PrescriptionID ");
            SQL.AppendLine("LEFT JOIN PrescriptionDetail AS pd ON pd.RecipeNo = ivd.RecipeNo ");
            SQL.AppendLine("LEFT JOIN Patient AS pat ON pat.PatCode = iv.PatCode ");
            SQL.AppendLine("LEFT JOIN DWard AS dw ON dw.WardCode = pat.WardCode ");
            SQL.AppendLine("LEFT JOIN DDrug AS dd ON dd.DrugCode = ivd.DrugCode ");
            try
            {
                using (DataSet DS = dbHelp.GetPIVAsDB(string.Format(SB.ToString(), SQL.ToString())))
                {
                    if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                    {
                        GetLabelNoSql = DS.Tables[0].Rows[0][0].ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (string.IsNullOrEmpty(GetLabelNoSql))
            {
                GetLabelNoSql = SQL.ToString().Replace("''", "'");
            }
            StringBuilder sb = new StringBuilder(2048);
            sb.AppendLine("IF(OBJECT_ID('DDrugColor') is null) BEGIN ");
            sb.AppendLine("CREATE TABLE [dbo].[DDrugColor]([DrugCode] [varchar](50) NOT NULL,[DrugName] [varchar](128) NOT NULL,[Spec] [varchar](50) NULL,[SpellCode] [varchar](64) NULL,[DrugColor] [int] NOT NULL, ");
            sb.AppendLine(" CONSTRAINT [PK_DDrugColor] PRIMARY KEY CLUSTERED ([DrugCode] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ");
            sb.AppendLine("ALTER TABLE [dbo].[DDrugColor] ADD  CONSTRAINT [DF_DDrugColor_DrugColor]  DEFAULT ((0)) FOR [DrugColor] END ");
            sb.AppendLine(string.Format(" INSERT INTO [DDrugColor] SELECT DrugCode,DrugName,Spec,SpellCode,{0} FROM DDrug D WHERE NOT EXISTS(SELECT 'X' FROM [DDrugColor] DC WHERE DC.DrugCode=D.DrugCode) ", Color.White.ToArgb()));
            dbHelp.SetPIVAsDB(sb.ToString());
            DDrug = dbHelp.GetPIVAsDB("SELECT [DrugCode],[DrugName],[SpellCode] FROM [dbo].[DDrug]").Tables[0];
            DDrug.PrimaryKey = new DataColumn[] { DDrug.Columns[0] };
            sda = new SqlDataAdapter(string.Format("SELECT * FROM [DDrugColor] order by case when DrugColor={0} then 10000000 else DrugColor end , DrugName", Color.White.ToArgb()), new SqlConnection(dbHelp.DatebasePIVAsInfo()));
            sda.Fill(DrugColor);
            if (DrugColor != null && DrugColor.Tables.Count > 0 && DrugColor.Tables[0].Rows.Count > 0)
            {
                DrugColor.Tables[0].PrimaryKey = new DataColumn[] { DrugColor.Tables[0].Columns[0] };
            }
            PreviewMode = 0;
            userID = ID;
            InitializeComponent();
            firstrun();
        }

        private void firstrun()
        {
            comboBox1.SelectedIndex = 0;
            if (dbHelp.GetPivasAllSet("打印-瓶签打印状态下拉框-内容显示") == "1")
            {
                if (!comboBox1.Items.Contains("全部瓶签"))
                    comboBox1.Items.Add("全部瓶签");
            }
            else
            {
                comboBox1.Items.Remove("全部瓶签");
            }
            if (!HasLoad)
            {
                using (DataSet ds = dbHelp.GetPIVAsDB(string.Format("SELECT * FROM [dbo].[PrintFormSet] where DEmployeeID ='{0}'", userID)))
                {
                    lt = dbHelp.GetPIVAsDB(string.Format("SELECT [LimitName] FROM [dbo].[ManageLimit] where [DEmployeeID]='{0}'", userID)).Tables[0];
                    button2.Visible = dbHelp.GetPivasAllSet("打印-界面设置").Trim() == "1";
                    button3.Visible = dbHelp.GetPivasAllSet("打印-统药单按钮") == "1";
                    button6.Visible = dbHelp.GetPivasAllSet("打印-计费按钮") == "1";
                    button8.Visible = dbHelp.GetPivasAllSet("打印-发药机按钮") == "1";
                    button4.Visible = dbHelp.GetPivasAllSet("打印-页面自选-画面显示") == "1";
                    button7.Visible = dbHelp.GetPivasAllSet("打印-重排仓位按钮") == "1";
                    int.TryParse(dbHelp.GetPivasAllSet("打印-打印确认-统药单设置"), out PrintRD);
                    checkBox9.Visible =
                    label4.Visible =
                    comboBox10.Visible =
                    comboBox11.Visible =
                    comboBox9.Visible = dbHelp.GetPivasAllSet("打印-姓名选择-瓶签显示") == "1";
                    string lb4 = dbHelp.GetPivasAllSetValue2("打印-姓名选择-瓶签显示");
                    MustSelectUser = dbHelp.GetPivasAllSetValue3("打印-姓名选择-瓶签显示");
                    label4.Text = lb4.Length > 7 ? lb4 : "排药-配置-打包：";
                    textBox1.Visible = dbHelp.GetPivasAllSet("打印-瓶签模糊查询-画面显示") == "1";
                    IsUseCheck = dbHelp.GetPivasAllSet("打印-打印确认-打印完成后调用画面");
                    LabelNoPrint = string.IsNullOrEmpty(dbHelp.IniReadValuePivas("Printer", "LabelPrinter")) ? string.Empty : dbHelp.IniReadValuePivas("Printer", "LabelPrinter");
                    CountPrint = string.IsNullOrEmpty(dbHelp.IniReadValuePivas("Printer", "CountPrint")) ? string.Empty : dbHelp.IniReadValuePivas("Printer", "CountPrint");

                    Useful1 = dbHelp.GetPivasAllSet("打印-瓶签查询-瓶签状态显示配置-可打印").ToString() == "" ? "未配置" : dbHelp.GetPivasAllSet("打印-瓶签查询-瓶签状态显示配置-可打印").ToString();
                    Useful2 = dbHelp.GetPivasAllSetValue2("打印-瓶签查询-瓶签状态显示配置-可打印").ToString() == "" ? "未配置" : dbHelp.GetPivasAllSetValue2("打印-瓶签查询-瓶签状态显示配置-可打印").ToString();
                    Useful3 = dbHelp.GetPivasAllSetValue3("打印-瓶签查询-瓶签状态显示配置-可打印").ToString() == "" ? "未配置" : dbHelp.GetPivasAllSetValue3("打印-瓶签查询-瓶签状态显示配置-可打印").ToString();
                    Useless1 = dbHelp.GetPivasAllSet("打印-瓶签查询-瓶签状态显示配置-不可打印").ToString() == "" ? "未配置" : dbHelp.GetPivasAllSet("打印-瓶签查询-瓶签状态显示配置-不可打印").ToString();
                    Useless2 = dbHelp.GetPivasAllSetValue2("打印-瓶签查询-瓶签状态显示配置-不可打印").ToString() == "" ? "未配置" : dbHelp.GetPivasAllSetValue2("打印-瓶签查询-瓶签状态显示配置-不可打印").ToString();
                    Useless3 = dbHelp.GetPivasAllSetValue3("打印-瓶签查询-瓶签状态显示配置-不可打印").ToString() == "" ? "未配置" : dbHelp.GetPivasAllSetValue3("打印-瓶签查询-瓶签状态显示配置-不可打印").ToString();


                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        int.TryParse(dr["PreviewMode"].ToString(), out PreviewMode);
                        WardIdle = ds.Tables[0].Columns.Contains("WardIdle") ? true.Equals(dr["WardIdle"]) : false;
                        WardOpen = ds.Tables[0].Columns.Contains("WardOpen") ? true.Equals(dr["WardOpen"]) : false;
                        DrugList = ds.Tables[0].Columns.Contains("DrugList") ? true.Equals(dr["DrugList"]) : false;
                        int.TryParse(dr["PrintNumber"].ToString(), out PrintNumber);
                        PrintOrderCount = ds.Tables[0].Columns.Contains("PrintOrderCount") ? true.Equals(dr["PrintOrderCount"]) : false;
                        int.TryParse(dr["PrintDrugCount"].ToString(), out PrintDrugCount);
                        Positions = ds.Tables[0].Columns.Contains("Positions") ? true.Equals(dr["Positions"]) : false;
                        Species = ds.Tables[0].Columns.Contains("Species") ? true.Equals(dr["Species"]) : false;
                        UP2 = ds.Tables[0].Columns.Contains("UP2") ? true.Equals(dr["UP2"]) : false;
                        CheckDrugLimit = ds.Tables[0].Columns.Contains("CheckDrugLimit") ? true.Equals(dr["CheckDrugLimit"]) : false;
                        PrintOverCheck = ds.Tables[0].Columns.Contains("PrintOverCheck") ? true.Equals(dr["PrintOverCheck"]) : false;
                        NextDay = dr["NextDay"].ToString();
                        NotPrint = ds.Tables[0].Columns.Contains("NotPrint") ? Color.FromArgb(Convert.ToInt32(dr["NotPrint"])) : Color.WhiteSmoke;
                        NotPrintSelected = ds.Tables[0].Columns.Contains("NotPrintSelected") ? Color.FromArgb(Convert.ToInt32(dr["NotPrintSelected"])) : Color.Blue;
                        Printed = ds.Tables[0].Columns.Contains("Printed") ? Color.FromArgb(Convert.ToInt32(dr["Printed"])) : Color.Gray;
                        PrintSelected = ds.Tables[0].Columns.Contains("PrintSelected") ? Color.FromArgb(Convert.ToInt32(dr["PrintSelected"])) : Color.Silver;
                        HasCheck = ds.Tables[0].Columns.Contains("HasCheck") ? Color.FromArgb(Convert.ToInt32(dr["HasCheck"])) : Color.LightGreen;
                        HasCheckSelected = ds.Tables[0].Columns.Contains("HasCheckSelected") ? Color.FromArgb(Convert.ToInt32(dr["HasCheckSelected"])) : Color.Green;
                        if (int.Parse(dbHelp.GetPIVAsDB(string.Format("select DATEDIFF(MI,'{0}',CONVERT(varchar,GETDATE(),108))", NextDay)).Tables[0].Rows[0][0].ToString()) >= 0)
                        {
                            dateTimePicker1.Value = Convert.ToDateTime(DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-dd"));
                        }
                        else
                        {
                            dateTimePicker1.Value = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                        }
                        HasLoad = true;
                        comboxch();
                    }
                    else
                    {
                        new SetShowFunc(this).ShowDialog();
                    }
                }
            }
        }

        protected internal void comboxch()
        {
            comboBox2.Items.Clear();
            comboBox2.Items.Add("所有批次(#,K,L)");
            comboBox4.Items.Clear();
            comboBox4.Items.Add("全部种类");
            comboBox4.Items.Add("普通药类");
            comboBox4.Items.Add("抗生素类");
            comboBox4.Items.Add("化疗药类");
            comboBox4.Items.Add("营养药类");
            comboBox4.Items.Add("中(成)药类");
            comboBox5.Items.Clear();
            comboBox5.Items.Add("所有仓位");
            comboBox6.Items.Clear();
            comboBox7.Items.Clear();
            comboBox7.Items.Add("全部病区组");
            comboBox9.Items.Clear();
            comboBox9.Items.Add(string.Empty);
            comboBox10.Items.Clear();
            comboBox10.Items.Add(string.Empty);
            comboBox11.Items.Clear();
            comboBox11.Items.Add(string.Empty);

            StringBuilder sb = new StringBuilder(1024);
            sb.AppendLine("SELECT DISTINCT OrderBY    FROM BatchToOrder WHERE OrderBY !='' ");
            sb.AppendLine("SELECT DISTINCT Species    FROM IVRecord IV INNER JOIN IVRecordDetail IVD ON IV.IVRecordID=IVD.IVRecordID AND LabelOver  > case when IVStatus=0 then -1 else -3 end AND BatchSaved = 1 AND DATEDIFF(DAY,InfusionDT,'{0}')=0 LEFT JOIN DDrug DD ON DD.DrugCode=IVD.DrugCode ");
            sb.AppendLine("SELECT DISTINCT DeskNo     FROM IVRecord WHERE LabelOver > case when IVStatus=0 then -1 else -3 end AND BatchSaved = 1 AND DATEDIFF(DAY,InfusionDT,'{0}')=0 ");
            sb.AppendLine("SELECT DISTINCT UsageCode  FROM IVRecord IV INNER JOIN Prescription P ON IV.PrescriptionID=P.PrescriptionID AND LabelOver  > case when IVStatus=0 then -1 else -3 end AND BatchSaved = 1 AND DATEDIFF(DAY,InfusionDT,'{0}')=0 order by UsageCode");
            sb.AppendLine("SELECT DISTINCT DEmployeeCode,DEmployeeName FROM DEmployee where Type=1 ");
            sb.AppendLine("SELECT DISTINCT WardArea   FROM DWard    WHERE WardArea !='' ");
            sb.AppendLine("SELECT DISTINCT TeamNumber FROM IVRecord WHERE LabelOver > case when IVStatus=0 then -1 else -3 end AND BatchSaved = 1 AND DATEDIFF(DAY,InfusionDT,'{0}')=0 order by TeamNumber ");
            sb.AppendLine("SELECT Batch               FROM IVRecord WHERE LabelOver > case when IVStatus=0 then -1 else -3 end AND BatchSaved = 1 AND DATEDIFF(DAY,InfusionDT,'{0}')=0 GROUP BY Batch order by SUBSTRING([Batch],LEN([Batch]),1),[Batch] ");

            using (DataSet ds = dbHelp.GetPIVAsDB(string.Format(sb.ToString(), dateTimePicker1.Value.ToString("yyyy-MM-dd"))))
            {
                if (ds != null)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (!string.IsNullOrEmpty(dr[0].ToString().Trim()))
                            comboBox6.Items.Add(dr[0].ToString());
                    }
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        if (!string.IsNullOrEmpty(dr[0].ToString().Trim()))
                            comboBox4.Items.Add(dr[0]);
                    }
                    foreach (DataRow dr in ds.Tables[2].Rows)
                    {
                        if (!string.IsNullOrEmpty(dr[0].ToString().Trim()))
                            comboBox5.Items.Add(dr[0]);
                    }
                    Usage = ds.Tables[3];
                    Usage.PrimaryKey = new DataColumn[] { Usage.Columns[0] };
                    foreach (DataRow dr in ds.Tables[4].Rows)
                    {
                        comboBox9.Items.Add(dr["DEmployeeCode"] + "-" + dr["DEmployeeName"]);
                        comboBox10.Items.Add(dr["DEmployeeCode"] + "-" + dr["DEmployeeName"]);
                        comboBox11.Items.Add(dr["DEmployeeCode"] + "-" + dr["DEmployeeName"]);
                    }
                    foreach (DataRow dr in ds.Tables[5].Rows)
                    {
                        comboBox7.Items.Add(dr[0].ToString());
                    }
                    foreach (DataRow dr in ds.Tables[6].Rows)
                    {
                        if (!string.IsNullOrEmpty(dr[0].ToString().Trim()))
                            comboBox2.Items.Add(dr[0]);
                    }
                    Batch = ds.Tables[7];
                    Batch.PrimaryKey = new DataColumn[] { Batch.Columns[0] };
                    if (Batch.Select("Batch like('%#')").Length > 0)
                    {
                        comboBox2.Items.Add("所有#");
                        foreach (DataRow dr in Batch.Select("Batch like('%#')"))
                        {
                            if (!string.IsNullOrEmpty(dr[0].ToString().Trim()))
                                comboBox2.Items.Add(dr[0]);
                        }
                    }
                    if (Batch.Select("Batch like('%K')").Length > 0)
                    {
                        comboBox2.Items.Add("所有K");
                        foreach (DataRow dr in Batch.Select("Batch like('%K')"))
                        {
                            if (!string.IsNullOrEmpty(dr[0].ToString().Trim()))
                                comboBox2.Items.Add(dr[0]);
                        }
                    }
                    if (Batch.Select("Batch like('L%')").Length > 0)
                    {
                        comboBox2.Items.Add("所有L");
                        foreach (DataRow dr in Batch.Select("Batch like('L%')"))
                        {
                            if (!string.IsNullOrEmpty(dr[0].ToString().Trim()))
                                comboBox2.Items.Add(dr[0]);
                        }
                    }
                }
            }
            if (comboBox6.Items.Count == 0)
            {
                comboBox6.Items.Add("病区,批次,主药,用量,溶媒");
            }
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
            comboBox7.SelectedIndex = 0;
            UsageCodeS = string.Empty;
            BatS = string.Empty;
            comboBox2.Visible = true;
            label2.Visible = false;

            if (checkBox9.Checked)
            {
                comboBox9.SelectedIndex = 0;
                comboBox10.SelectedIndex = 0;
                comboBox11.SelectedIndex = 0;
            }
            else
            {
                comboBox9.SelectedIndex = comboBox9.Items.IndexOf(ArrDrugUserCode);
                comboBox10.SelectedIndex = comboBox10.Items.IndexOf(PZDrugUserCode);
                comboBox11.SelectedIndex = comboBox11.Items.IndexOf(PackDrugUserCode);
            }
            new ToolTip().SetToolTip(button5, "打印");
            new ToolTip().SetToolTip(button2, "设置");
        }
        public void PivasIVRP_Load(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// 根据显示模式显示数据
        /// </summary>
        private void showLabelNO()
        {
            switch (PreviewMode)
            {
                case 0:
                    panel3.Visible = true;//瓶签详细内容（容器）
                    flowLayoutPanel2.Width = panelPrint.Width - panelLeft.Width - panel3.Width;//瓶签列表控件宽度设置
                    if (panel3.Controls.Count == 0 || !(panel3.Controls[0] is BQlabelDetail))
                    {
                        panel3.Controls.Clear();
                        BD = new BQlabelDetail(new DataSet());
                        //BD.Size = panel3.Size;
                        BD.Dock = DockStyle.Fill;
                        panel3.Controls.Add(BD);
                    }
                    //BD.BQlabelDetail_Load(null, null);
                    getLabelno();
                    break;
                case 1:
                    panel3.Visible = true;//瓶签详细内容（容器）
                    flowLayoutPanel2.Width = panelPrint.Width - panelLeft.Width - panel3.Width;//瓶签列表控件宽度设
                    if (panel3.Controls.Count == 0 || !(panel3.Controls[0] is BQlabelShow))
                    {
                        panel3.Controls.Clear();
                        BS = new BQlabelShow(this);
                        //BS.Size = panel3.Size;
                        BS.Dock = DockStyle.Fill;
                        panel3.Controls.Add(BS);
                    }
                    //BS.BQlabelShow_Load(null, null);
                    getLabelno();
                    break;

                case 2:
                    panel3.Visible = false;//瓶签详细内容（容器）,不显示
                    flowLayoutPanel2.Width = panelPrint.Width - panelLeft.Width;//详细显示宽度设置
                    getLabelno();
                    break;

                case 3:
                    if (flowLayoutPanel2.HasChildren)
                        flowLayoutPanel2.Controls[0].Dispose();

                    panel3.Visible = checkBox2.Visible = false;
                    flowLayoutPanel2.Width = panelPrint.Width - panelLeft.Width;//预览控件宽度设置
                    if (dataGridView1.RowCount > 0)
                    {

                        DrugCodes = "";//保存被选药品变量
                        foreach (DataGridViewRow dg in dataGridView1.Rows)
                        {
                            //生成药品条件
                            if (true.Equals(dg.Cells["Column2"].Value))
                            {
                                DrugCodes = "'" + dg.Cells["DrugCode"].Value.ToString() + "'," + DrugCodes;
                            }
                        }

                        if (!string.IsNullOrEmpty(DrugCodes))
                        {
                            printlabel pr = new printlabel(this, WardCodes);
                            pr.Size = flowLayoutPanel2.Size;
                            flowLayoutPanel2.Controls.Add(pr);
                        }
                    }
                    break;
            }
        }

        private void getLabelno()
        {
            if (flowLayoutPanel2.HasChildren)
                flowLayoutPanel2.Controls[0].Dispose();
            checkBox2.Visible = true;
            checkBox2.CheckState = CheckState.Checked;
            if (dataGridView1.RowCount == 0)
            {
                //模糊查询时走此逻辑
                if (!string.IsNullOrEmpty(textValue))
                {
                    DataSet ds = dbHelp.GetPIVAsDB(getsql("getLabelnoS"));
                    if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    {
                        MessageBox.Show("没有瓶签信息");
                    }
                    else
                    {
                        if (PreviewMode == 0)
                        {
                            BQlabelDataSet gdv = new BQlabelDataSet(this);
                            gdv.dataGridView2.DataSource = ds.Tables[0];
                            gdv.Size = flowLayoutPanel2.Size;
                            flowLayoutPanel2.Controls.Add(gdv);
                            stalimt = 0;
                            endlimt = ds.Tables[0].Rows.Count;

                        }
                        else if (PreviewMode == 1)
                        {
                            BQlabelDataSet gdv = new BQlabelDataSet(this);
                            gdv.dataGridView2.DataSource = ds.Tables[0];
                            gdv.Size = flowLayoutPanel2.Size;
                            flowLayoutPanel2.Controls.Add(gdv);
                            stalimt = 0;
                            endlimt = ds.Tables[0].Rows.Count;

                        }
                        else
                        {
                            BQDetail bd = new BQDetail(this, ds);
                            bd.Size = flowLayoutPanel2.Size;
                            flowLayoutPanel2.Controls.Add(bd);
                            stalimt = 0;
                            endlimt = ds.Tables[0].Rows.Count;
                        }
                    }
                    ds.Dispose();
                }
            }
            else
            {
                DrugCodes = "";//保存被选药品变量
                foreach (DataGridViewRow dg in dataGridView1.Rows)
                {
                    if (true.Equals(dg.Cells["Column2"].Value))
                    {
                        DrugCodes = "'" + dg.Cells["DrugCode"].Value + "'," + DrugCodes;
                    }
                }

                if (!string.IsNullOrEmpty(DrugCodes))
                {
                    DataSet ds = dbHelp.GetPIVAsDB(getsql("getLabelno"));
                    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        if (PreviewMode == 0)
                        {
                            //列表模式

                            BQlabelDataSet gdv = new BQlabelDataSet(this);
                            gdv.dataGridView2.DataSource = ds.Tables[0];
                            gdv.Size = flowLayoutPanel2.Size;
                            flowLayoutPanel2.Controls.Add(gdv);
                            stalimt = 0;
                            endlimt = ds.Tables[0].Rows.Count;

                        }
                        else if (PreviewMode == 1)
                        {
                            BQlabelDataSet gdv = new BQlabelDataSet(this);
                            gdv.dataGridView2.DataSource = ds.Tables[0];
                            gdv.Size = flowLayoutPanel2.Size;
                            flowLayoutPanel2.Controls.Add(gdv);
                            stalimt = 0;
                            endlimt = ds.Tables[0].Rows.Count;

                        }
                        else
                        {
                            //明细模式
                            BQDetail bd = new BQDetail(this, ds);
                            bd.Size = flowLayoutPanel2.Size;
                            flowLayoutPanel2.Controls.Add(bd);
                            stalimt = 0;
                            endlimt = ds.Tables[0].Rows.Count;
                        }
                    }
                    ds.Dispose();
                }
            }
        }

        private void flowLayoutPanel2_SizeChanged(object sender, EventArgs e)
        {
            foreach (Control c in flowLayoutPanel2.Controls)
            {
                c.Size = flowLayoutPanel2.Size;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if ((lt.Select("LimitName='04001'").Length > 0 && comboBox1.SelectedIndex == 0) || (lt.Select("LimitName='04002'").Length > 0))
            {
                if (flowLayoutPanel2.HasChildren)
                {
                    string[] msu = string.IsNullOrEmpty(MustSelectUser) ? "0-0-0".Split('-') : MustSelectUser.Split('-');
                    if (msu.Length == 3)
                    {
                        if (msu[0].Trim() == "1" && string.IsNullOrEmpty(comboBox9.Text.Trim()))
                        {
                            MessageBox.Show("请选择“" + label4.Text.Split('-')[0] + "”员工！");
                            return;
                        }
                        if (msu[1].Trim() == "1" && string.IsNullOrEmpty(comboBox11.Text.Trim()))
                        {
                            MessageBox.Show("请选择“" + label4.Text.Split('-')[1] + "”员工！");
                            return;
                        }
                        if (msu[2].Trim() == "1" && string.IsNullOrEmpty(comboBox10.Text.Trim()))
                        {
                            MessageBox.Show("请选择“" + label4.Text.Split('-')[2] + "”员工！");
                            return;
                        }
                    }

                    if (PreviewMode == 3)
                    {
                        if (dbHelp.GetPivasAllSet("打印-打印按钮-预览模式下") == "0")
                        {
                            MessageBox.Show("预览模式下不可打印！！！");
                            return;
                        }
                    }
                    new PrintRD(this).ShowDialog();
                    if (printed)
                    {
                        NowST = "开始打印";
                        Thread th = new Thread(() => { new Abut(this).ShowDialog(); });
                        th.IsBackground = true;
                        th.Start();
                        print();
                        if (checkBox9.Checked)
                        {
                            comboBox9.SelectedIndex = 0;
                            comboBox10.SelectedIndex = 0;
                            comboBox11.SelectedIndex = 0;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("您没有权限，请联系管理员！");
            }
            //print();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lt.Select("LimitName='04003'").Length > 0)
            {
                new SetShowFunc(this).ShowDialog();
            }
            else
            {
                MessageBox.Show("您没有权限，请联系管理员！");
            }
        }

        public string getsql(string MOD)
        {
            string sql = null;
            switch (MOD)
            {
                case "2":
                    sql = string.Format("select convert(bit,1) as BOL,IVD.[DrugCode],IVD.[DrugName], COUNT(DISTINCT LabelNo) coun FROM IVRecord iv INNER JOIN Prescription P ON IV.PrescriptionID=P.PrescriptionID AND LabelOver > case when IVStatus=0 then -1 else -3 end  and BatchSaved = 1 and DATEDIFF(DAY,InfusionDT,'{0}')=0 " + (checkBox3.CheckState == CheckState.Checked ? string.Empty : string.Format(" and IV.WardCode in ({0}) ", WardCodes.TrimEnd(','))) + " LEFT JOIN IVRecordDetail IVD ON IV.IVRecordID=IVD.IVRecordID LEFT JOIN DDrug DD ON IVD.DrugCode=DD.DrugCode WHERE IVStatus" + ad + OrderID + isJustOne + Specs + Position + ByUsageCode + " group by IVD.[DrugCode],IVD.[DrugName],[IsMenstruum] order by [IsMenstruum],coun desc ", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                    break;
                case "getLabelno":
                    sql = string.Format("SELECT distinct WardName as '病区',BedNo as '床号',PatName as'患者',(select DrugName from [dbo].DDrug where DrugCode=MarjorDrug) as '主药',LabelNo as'瓶签号', Remark6 as 'HIS瓶签', Batch as'批次',FregCode as '频次',PrintDT as '打印时间',[PrinterName] as '打印人' ,[CpUser] as '审方人' ,remark3 as '瓶签状态',DeskNo as '仓位',remark3,IVStatus , '' as space,TeamNumber,Batch,MarjorDrug,Menstruum,(SELECT max([DgNo]) FROM [dbo].[IVRecordDetail] where IVRecordID =v.IVRecordID and DrugCode=MarjorDrug) mds,DrugCount FROM ({0} where {1} " + (checkBox3.CheckState == CheckState.Checked ? string.Empty : string.Format(" IV.WardCode in ({0}) and ", WardCodes.TrimEnd(','))) + " DATEDIFF(DAY,InfusionDT,'{2}')=0 and IVStatus" + ad + OrderID + isJustOne + Specs + Position + ByUsageCode + ") v order by {3},DrugCount ", GetLabelNoSql, checkBox1.CheckState == CheckState.Checked ? "" : " IVD.DrugCode in (" + DrugCodes.TrimEnd(',') + ") and ", dateTimePicker1.Value.ToString("yyyy-MM-dd"), showprint.Replace("溶媒", "Menstruum").Replace("批次", "Batch").Replace("主药", "MarjorDrug").Replace("病区", "WardName").Replace("用量", "mds"));
                    break;
                case "getLabelnoS":
                    sql = string.Format("SELECT distinct WardName as '病区',BedNo as '床号',PatName as'患者',(select DrugName from [dbo].DDrug where DrugCode=MarjorDrug) as '主药',LabelNo as'瓶签号', Remark6 as 'HIS瓶签', Batch as'批次',FregCode as '频次',PrintDT as '打印时间',[PrinterName] as '打印人' ,[CpUser] as '审方人' ,remark3 as '瓶签状态',DeskNo as '仓位',remark3,IVStatus , '' as space,TeamNumber,Batch,MarjorDrug,Menstruum,(SELECT max([DgNo]) FROM [dbo].[IVRecordDetail] where IVRecordID =v.IVRecordID and DrugCode=MarjorDrug) mds,DrugCount FROM ({0} where (LabelNo like '%{1}%' or IV.Remark6 like '%{1}%' or PatientCode like '%{1}%' or pat.BedNo like '%{1}%' or PAT.PatName like '%{1}%') and DATEDIFF(DAY,InfusionDT,'{2}')=0 and IVStatus" + ad + OrderID + isJustOne + Specs + Position + ByUsageCode + " ) v  order by {3},DrugCount ", GetLabelNoSql, textValue, dateTimePicker1.Value.ToString("yyyy-MM-dd"), showprint.Replace("溶媒", "Menstruum").Replace("批次", "Batch").Replace("主药", "MarjorDrug").Replace("病区", "WardName").Replace("用量", "mds"));
                    break;
                case "getWardAreaS":
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT [WardCode],[WardSimName],[WardSeqNo] FROM [DWard] dw where " + (comboBox7.SelectedIndex == 0 ? string.Empty : "WardArea='" + comboBox7.SelectedItem.ToString() + "' and ") + (string.IsNullOrEmpty(textBox2.Text.Replace("病区名/简拼", string.Empty).Trim()) ? string.Empty : ("(Spellcode like ('%" + textBox2.Text.Trim() + "%') or WardName like ('%" + textBox2.Text.Trim() + "%')) and ")) + " IsOpen>='{1}' ");
                    sb.AppendLine("SELECT IV.WardCode,COUNT (distinct LabelNo) coun    FROM IVRecord iv,Prescription P,IVRecordDetail IVD,DDrug DD where IV.PrescriptionID=P.PrescriptionID AND IV.IVRecordID=IVD.IVRecordID AND IVD.DrugCode=DD.DrugCode and LabelOver > case when IVStatus=0 then -1 else -3 end  and BatchSaved = 1 and IVStatus" + ad + OrderID + isJustOne + Specs + Position + ByUsageCode + " and  DATEDIFF(DAY,InfusionDT,'{0}')=0 GROUP BY IV.WardCode");
                    sb.AppendLine("SELECT IV.WardCode,COUNT (distinct LabelNo) allcoun FROM IVRecord iv where LabelOver > case when IVStatus=0 then -1 else -3 end  and BatchSaved = 1 and DATEDIFF(DAY,InfusionDT,'{0}')=0 GROUP BY WardCode");
                    sql = sb.ToString();
                    break;
            }
            //MessageBox.Show(sql);
            return sql;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (panel5.Visible)
            {
                panel5.Location = new Point(-30, panel5.Location.Y);
                label1.Text = ">";
                panel5.Visible = false;
            }
            else
            {
                if (dataGridView1.RowCount == 0)
                {
                    MessageBox.Show("没有药品信息");
                }
                else
                {
                    panel5.Location = new Point(panelLeft.Width, panel5.Location.Y);
                    label1.Text = "<";
                    panel5.Visible = true;
                    if (DrugColor.Tables.Count > 0 && DrugColor.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                        {
                            dgvr.DefaultCellStyle.BackColor = Color.FromArgb(Convert.ToInt32(DrugColor.Tables[0].Rows.Find(dgvr.Cells["DrugCode"].Value)["DrugColor"]));
                        }
                    }
                }
            }
            panel5.Refresh();
        }

        public void run()
        {
            WardCodes = string.Empty;
            foreach (DataGridViewRow dgvr in dataGridView2.Rows)
            {
                if (Equals(dgvr.Cells[0].Value, true))
                {
                    WardCodes = WardCodes + "'" + dgvr.Cells["wcode"].Value + "',";
                }
            }

            if (string.IsNullOrEmpty(WardCodes))
            {
                if (flowLayoutPanel2.HasChildren)
                    flowLayoutPanel2.Controls[0].Dispose();
                if (panel5.Visible)
                {
                    panel5.Visible = false;
                    panel5.Location = new Point(-30, panel5.Location.Y);
                    label1.Text = ">";
                    panel5.Refresh();
                }
                if (dataGridView1.DataSource != null)
                {
                    textBox3.Text = string.Empty;
                    (dataGridView1.DataSource as DataTable).Rows.Clear();
                }
                if (panel3.Visible)
                {
                    BD.ds = new DataSet();
                    BD.BQlabelDetail_Load(null, null);
                    BS.MShow(string.Empty);
                }
            }
            else
            {
                //药品选择内容生成
                textBox3.Text = string.Empty;
                textBox3_TextChanged(null, null);
                //生成瓶签数据
                showLabelNO();
            }
        }

        private void panel3_SizeChanged(object sender, EventArgs e)
        {
            foreach (Control c in panel3.Controls)
            {
                c.Size = panel3.Size;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ischange = true;
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (ischange)
            {
                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    dr.Cells["Column2"].Value = checkBox1.Checked;
                }
                if (!string.IsNullOrEmpty(textBox3.Text.Trim()) && checkBox1.Checked)
                {
                    checkBox1.CheckState = CheckState.Indeterminate;
                }
                ischange = false;
                showLabelNO();
            }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            ischange2 = true;
        }
        private void checkBox2_Click(object sender, EventArgs e)
        {
            if (ischange2)
            {
                if (flowLayoutPanel2.HasChildren)
                {
                    switch (PreviewMode)
                    {
                        case 0:
                            {
                                BQlabelDataSet gdv = (BQlabelDataSet)flowLayoutPanel2.Controls[0];
                                foreach (DataGridViewRow dgr in gdv.dataGridView2.Rows)
                                {
                                    dgr.Cells["checkbox"].Value = checkBox2.Checked;
                                }
                                break;
                            }
                        case 1:
                            {
                                BQlabelDataSet gdv = (BQlabelDataSet)flowLayoutPanel2.Controls[0];
                                foreach (DataGridViewRow dgr in gdv.dataGridView2.Rows)
                                {
                                    dgr.Cells["checkbox"].Value = checkBox2.Checked;
                                }
                                break;
                            }
                        case 2:
                            List<string> ls = new List<string>(prints.Keys);
                            foreach (string key in ls)
                            {
                                prints[key] = checkBox2.Checked;
                            }
                            BQDetail bq = (BQDetail)flowLayoutPanel2.Controls[0];
                            foreach (BQlabel c in bq.panel1.Controls)
                            {
                                c.checkBox1.Checked = checkBox2.Checked;
                            }
                            break;
                    }
                }
                ischange2 = false;
            }
        }
        public void print()
        {
            try
            {
                lock (this)
                {
                    if (flowLayoutPanel2.HasChildren)
                    {
                        string TimeID = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "-" + userID;
                        string LongDt = DateTime.Now.Ticks.ToString();
                        string label = string.Empty;
                        string LesLabel = string.Empty;
                        List<string> sls = new List<string>();
                        Menstruum.Clear();
                        switch (PreviewMode)
                        {
                            case 0:
                                {
                                    BQlabelDataSet gdv = ((BQlabelDataSet)flowLayoutPanel2.Controls[0]);
                                    selected = string.Empty;
                                    List<string> ls = new List<string>();
                                    foreach (DataGridViewRow dgr in gdv.dataGridView2.Rows)
                                    {
                                        int Remark3 = 0;
                                        int.TryParse(dgr.Cells["Remark3"].Value.ToString(), out Remark3);
                                        if (true.Equals(dgr.Cells["checkbox"].Value) && Remark3 >= 10)
                                        {
                                            ls.Add(dgr.Cells["瓶签号"].Value.ToString());
                                            selected = "'" + dgr.Cells["瓶签号"].Value + "'," + selected;
                                            if (!Menstruum.Contains(dgr.Cells["批次"].Value + "," + dgr.Cells["Menstruum"].Value))
                                                Menstruum.Add(dgr.Cells["批次"].Value + "," + dgr.Cells["Menstruum"].Value);
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(selected))
                                    {
                                        selected = selected.TrimEnd(',');
                                        if (CheckDrugLimit)
                                        {
                                            using (DataSet CanPrint = dbHelp.GetPIVAsDB(string.Format("EXEC [dbo].[bl_IVRCheckDgNoLimit] '{0}',''", selected.Replace("'", "''"))))
                                            {
                                                if (CanPrint != null && CanPrint.Tables.Count > 0)
                                                {
                                                    if (CanPrint.Tables[0].Rows.Count > 0)
                                                    {
                                                        StringBuilder mesg = new StringBuilder(4096);
                                                        foreach (DataRow dr in CanPrint.Tables[0].Rows)
                                                        {
                                                            mesg.AppendLine("病区：" + dr["WardName"].ToString().Trim());
                                                            mesg.AppendLine("对药品：" + dr["DrugName"].ToString().Trim() + " 已超控量上限 " + dr["LimitDgNoForWard"].ToString());
                                                        }
                                                        NowST = "打印完成";
                                                        MessageBox.Show(mesg.ToString());
                                                        break;
                                                    }
                                                }
                                                if (CanPrint != null && CanPrint.Tables.Count > 1)
                                                {
                                                    if (CanPrint.Tables[1].Rows.Count > 0)
                                                    {
                                                        StringBuilder mesg = new StringBuilder(4096);
                                                        foreach (DataRow dr in CanPrint.Tables[1].Rows)
                                                        {
                                                            mesg.AppendLine("医生工号：" + dr["DoctorCode"].ToString().Trim());
                                                            mesg.AppendLine("对药品：" + dr["DrugName"].ToString().Trim() + " 已超控量上限 " + dr["LimitDgNoForDoctor"].ToString());
                                                        }
                                                        NowST = "打印完成";
                                                        MessageBox.Show(mesg.ToString());
                                                        break;
                                                    }
                                                }
                                            }
                                        }



                                        printlabel prl = new printlabel(this, WardCodes);
                                        if (prl.PrintOne(TimeID, LongDt, ref ls))
                                        {
                                            if (printDgT && SendPrt)
                                            {
                                                if (PrintOrderCount)
                                                    new printlabel(this, WardCodes).PrintBathSum("9999" + LongDt);
                                                if (PrintDrugCount > 0)
                                                    new printlabel(this, WardCodes).PrintDrugSum("8888" + LongDt);
                                            }
                                            if (!PrintOverCheck)
                                            {
                                                MessageBox.Show("打印完成");
                                            }
                                        }
                                        NowST = "打印完成";
                                        PivasIVRP_Load(null, null);
                                    }
                                    else
                                    {
                                        NowST = "打印完成";
                                        MessageBox.Show("没有瓶签需要打印");
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    BQlabelDataSet gdv = ((BQlabelDataSet)flowLayoutPanel2.Controls[0]);
                                    selected = string.Empty;
                                    List<string> ls = new List<string>();
                                    foreach (DataGridViewRow dgr in gdv.dataGridView2.Rows)
                                    {
                                        int Remark3 = 0;
                                        int.TryParse(dgr.Cells["Remark3"].Value.ToString(), out Remark3);
                                        if (true.Equals(dgr.Cells["checkbox"].Value) && Remark3 >= 10)
                                        {
                                            ls.Add(dgr.Cells["瓶签号"].Value.ToString());
                                            selected = "'" + dgr.Cells["瓶签号"].Value + "'," + selected;
                                            if (!Menstruum.Contains(dgr.Cells["批次"].Value + "," + dgr.Cells["Menstruum"].Value))
                                                Menstruum.Add(dgr.Cells["批次"].Value + "," + dgr.Cells["Menstruum"].Value);
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(selected))
                                    {
                                        selected = selected.TrimEnd(',');
                                        if (CheckDrugLimit)
                                        {
                                            using (DataSet CanPrint = dbHelp.GetPIVAsDB(string.Format("EXEC [dbo].[bl_IVRCheckDgNoLimit] '{0}',''", selected.Replace("'", "''"))))
                                            {
                                                if (CanPrint != null && CanPrint.Tables.Count > 0)
                                                {
                                                    if (CanPrint.Tables[0].Rows.Count > 0)
                                                    {
                                                        StringBuilder mesg = new StringBuilder(4096);
                                                        foreach (DataRow dr in CanPrint.Tables[0].Rows)
                                                        {
                                                            mesg.AppendLine("病区：" + dr["WardName"].ToString().Trim());
                                                            mesg.AppendLine("对药品：" + dr["DrugName"].ToString().Trim() + " 已超控量上限 " + dr["LimitDgNoForWard"].ToString());
                                                        }
                                                        NowST = "打印完成";
                                                        MessageBox.Show(mesg.ToString());
                                                        break;
                                                    }
                                                }
                                                if (CanPrint != null && CanPrint.Tables.Count > 1)
                                                {
                                                    if (CanPrint.Tables[1].Rows.Count > 0)
                                                    {
                                                        StringBuilder mesg = new StringBuilder(4096);
                                                        foreach (DataRow dr in CanPrint.Tables[1].Rows)
                                                        {
                                                            mesg.AppendLine("医生工号：" + dr["DoctorCode"].ToString().Trim());
                                                            mesg.AppendLine("对药品：" + dr["DrugName"].ToString().Trim() + " 已超控量上限 " + dr["LimitDgNoForDoctor"].ToString());
                                                        }
                                                        NowST = "打印完成";
                                                        MessageBox.Show(mesg.ToString());
                                                        break;
                                                    }
                                                }
                                            }
                                        }

                                        printlabel prl = new printlabel(this, WardCodes);
                                        if (prl.PrintOne(TimeID, LongDt, ref ls))
                                        {
                                            if (printDgT && SendPrt)
                                            {
                                                if (PrintOrderCount)
                                                    new printlabel(this, WardCodes).PrintBathSum("9999" + LongDt);
                                                if (PrintDrugCount > 0)
                                                    new printlabel(this, WardCodes).PrintDrugSum("8888" + LongDt);
                                            }
                                            if (!PrintOverCheck)
                                            {
                                                MessageBox.Show("打印完成");
                                            }
                                        }
                                        NowST = "打印完成";
                                        PivasIVRP_Load(null, null);
                                    }
                                    else
                                    {
                                        NowST = "打印完成";
                                        MessageBox.Show("没有瓶签需要打印");
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    if (((BQDetail)flowLayoutPanel2.Controls[0]).panel1.HasChildren)
                                    {
                                        selected = string.Empty;
                                        List<string> ls = new List<string>();
                                        foreach (var item in prints)
                                        {
                                            if (item.Value)
                                            {
                                                ls.Add(item.Key);
                                                selected = "'" + item.Key + "'," + selected;
                                            }
                                        }
                                        if (!string.IsNullOrEmpty(selected))
                                        {
                                            selected = selected.TrimEnd(',');
                                            if (CheckDrugLimit)
                                            {
                                                using (DataSet CanPrint = dbHelp.GetPIVAsDB(string.Format("EXEC [dbo].[bl_IVRCheckDgNoLimit] '{0}',''", selected.Replace("'", "''"))))
                                                {
                                                    if (CanPrint != null && CanPrint.Tables.Count > 0)
                                                    {
                                                        if (CanPrint.Tables[0].Rows.Count > 0)
                                                        {
                                                            StringBuilder mesg = new StringBuilder(4096);
                                                            foreach (DataRow dr in CanPrint.Tables[0].Rows)
                                                            {
                                                                mesg.AppendLine("病区：" + dr["WardName"].ToString().Trim());
                                                                mesg.AppendLine("对药品：" + dr["DrugName"].ToString().Trim() + " 已超控量上限 " + dr["LimitDgNoForWard"].ToString());
                                                            }
                                                            NowST = "打印完成";
                                                            MessageBox.Show(mesg.ToString());
                                                            break;
                                                        }
                                                    }
                                                    if (CanPrint != null && CanPrint.Tables.Count > 1)
                                                    {
                                                        if (CanPrint.Tables[1].Rows.Count > 0)
                                                        {
                                                            StringBuilder mesg = new StringBuilder(4096);
                                                            foreach (DataRow dr in CanPrint.Tables[1].Rows)
                                                            {
                                                                mesg.AppendLine("医生工号：" + dr["DoctorCode"].ToString().Trim());
                                                                mesg.AppendLine("对药品：" + dr["DrugName"].ToString().Trim() + " 已超控量上限 " + dr["LimitDgNoForDoctor"].ToString());
                                                            }
                                                            NowST = "打印完成";
                                                            MessageBox.Show(mesg.ToString());
                                                            break;
                                                        }
                                                    }
                                                }
                                            }

                                            printlabel prl = new printlabel(this, WardCodes);
                                            if (prl.PrintOne(TimeID, LongDt, ref ls))
                                            {
                                                if (printDgT && SendPrt)
                                                {
                                                    if (PrintOrderCount)
                                                        new printlabel(this, WardCodes).PrintBathSum("9999" + LongDt);
                                                    if (PrintDrugCount > 0)
                                                        new printlabel(this, WardCodes).PrintDrugSum("8888" + LongDt);
                                                }
                                                if (!PrintOverCheck)
                                                {
                                                    MessageBox.Show("打印完成");
                                                }
                                            }
                                            NowST = "打印完成";
                                            PivasIVRP_Load(null, null);
                                        }
                                        else
                                        {
                                            NowST = "打印完成";
                                            MessageBox.Show("没有瓶签需要打印");
                                        }
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    if (((printlabel)flowLayoutPanel2.Controls[0]).btnPrint_Click(TimeID, LongDt))
                                    {
                                        if (printDgT && SendPrt)
                                        {
                                            if (PrintOrderCount)
                                                new printlabel(this, WardCodes).PrintBathSum("9999" + LongDt);
                                            if (PrintDrugCount > 0)
                                                new printlabel(this, WardCodes).PrintDrugSum("8888" + LongDt);
                                        }
                                        NowST = "打印完成";
                                        PivasIVRP_Load(null, null);
                                        if (!PrintOverCheck)
                                        {
                                            MessageBox.Show("瓶签打印完成");
                                        }
                                    }
                                    else
                                    {
                                        NowST = "打印完成";
                                        PivasIVRP_Load(null, null);
                                        MessageBox.Show("没有瓶签");
                                    }
                                    break;
                                }
                        }
                    }
                }
                if (IsUseCheck != "0")
                {
                    UseCheck u = new UseCheck(userID, IsUseCheck);
                    u.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Black;
            if (textBox1.Text == "床号/患者/编号/瓶签号")
            {
                textBox1.Text = string.Empty;
            }
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.DarkGray;
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                textBox1.Text = "床号/患者/编号/瓶签号";
                textValue = string.Empty;
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ((char)13))
            {
                DataSet ds = new DataSet();
                if (string.IsNullOrEmpty(textBox1.Text.Trim()))
                {
                    textValue = "";
                    textBox1.Text = "床号/患者/编号/瓶签号";
                    if (!string.IsNullOrEmpty(WardCodes))
                    {
                        textBox3.Text = string.Empty;
                        textBox3_TextChanged(sender, e);
                        showLabelNO();
                    }
                }
                else if (textBox1.Text.Trim() != "床号/患者/编号/瓶签号")
                {
                    textValue = textBox1.Text.Trim();
                    if (PreviewMode == 3)
                    {
                        if (flowLayoutPanel2.HasChildren)
                            flowLayoutPanel2.Controls[0].Dispose();
                        printlabel pr = new printlabel(this, WardCodes);
                        pr.Size = flowLayoutPanel2.Size;
                        flowLayoutPanel2.Controls.Add(pr);
                    }
                    else
                    {
                        if (dataGridView1.DataSource != null)
                        {
                            textBox3.Text = string.Empty;
                            (dataGridView1.DataSource as DataTable).Rows.Clear();
                        }
                        getLabelno();
                    }
                }
                else
                {
                    textValue = "";
                    if (!string.IsNullOrEmpty(WardCodes))
                    {
                        textBox3.Text = string.Empty;
                        textBox3_TextChanged(sender, e);
                        showLabelNO();
                    }
                }
                ds.Dispose();
            }
        }
        private void dateTimePicker1_DropDown(object sender, EventArgs e)
        {
            HasLoad = false;
        }
        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            HasLoad = true;
            if (timechage)
            {
                comboxch();
                PivasIVRP_Load(sender, e);
            }
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            timechage = true;
        }
        /// <summary>
        /// 全选按钮，目前不用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            WardCodes = string.Empty;
            foreach (DataGridViewRow dgrv in dataGridView2.Rows)
            {
                dgrv.Cells[0].Value = checkBox3.Checked;
                if (checkBox3.Checked)
                    WardCodes = "'" + dgrv.Cells["wcode"].Value + "'," + WardCodes;
            }


            //判断病区列表有被选择
            if (string.IsNullOrEmpty(WardCodes))
            {
                //未有被选择
                if (flowLayoutPanel2.HasChildren)
                    flowLayoutPanel2.Controls[0].Dispose();
                if (dataGridView1.DataSource != null)
                {
                    textBox3.Text = string.Empty;
                    (dataGridView1.DataSource as DataTable).Rows.Clear();
                }
            }
            else
            {
                textBox3.Text = string.Empty;
                textBox3_TextChanged(sender, e);
                //生成瓶签数据
                showLabelNO();
            }
        }
        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (combox2 != comboBox2.SelectedIndex)
            {
                string OrderBY = string.Empty;
                using (DataSet ds = dbHelp.GetPIVAsDB(string.Format("SELECT max([OrderBY]) FROM [dbo].[BatchToOrder] where [Batch] = '{0}'", comboBox2.SelectedItem.ToString())))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        OrderBY = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
                if (!string.IsNullOrEmpty(OrderBY) && !comboBox6.Items.Contains(OrderBY))
                {
                    comboBox6.Items.Add(OrderBY);
                }
                comboBox6.SelectedIndex = string.IsNullOrEmpty(OrderBY) ? 0 : comboBox6.Items.IndexOf(OrderBY);
                PivasIVRP_Load(sender, e);
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            combox2 = comboBox2.SelectedIndex;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            combox1 = comboBox1.SelectedIndex;
        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (combox1 != comboBox1.SelectedIndex)
                PivasIVRP_Load(sender, e);
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            combox3 = comboBox3.SelectedIndex;
        }
        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (combox3 != comboBox3.SelectedIndex)
                PivasIVRP_Load(sender, e);
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            combox4 = comboBox4.SelectedIndex;
        }
        private void comboBox4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (combox4 != comboBox4.SelectedIndex)
                PivasIVRP_Load(sender, e);
        }
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            combox5 = comboBox5.SelectedIndex;
        }
        private void comboBox5_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (combox5 != comboBox5.SelectedIndex)
                PivasIVRP_Load(sender, e);
        }
        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            combox7 = comboBox7.SelectedIndex;
        }
        private void comboBox7_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (combox7 != comboBox7.SelectedIndex)
                PivasIVRP_Load(sender, e);
        }
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            showprint = comboBox6.SelectedItem.ToString();
            combox6 = comboBox6.SelectedIndex;
        }
        private void comboBox6_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (combox6 != comboBox6.SelectedIndex)
            {
                showprint = comboBox6.SelectedItem.ToString();
                showLabelNO();
            }
        }
        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArrDrugUserCode = comboBox9.SelectedItem.ToString();
        }
        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            PZDrugUserCode = comboBox11.SelectedItem.ToString();
        }
        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            PackDrugUserCode = comboBox10.SelectedItem.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            new BatchS(this, 0).ShowDialog();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (flowLayoutPanel2.HasChildren)
                {
                    string labs = string.Empty;
                    switch (PreviewMode)
                    {
                        case 0:
                            {
                                BQlabelDataSet gdv = ((BQlabelDataSet)flowLayoutPanel2.Controls[0]);
                                foreach (DataGridViewRow dgr in gdv.dataGridView2.Rows)
                                {
                                    if (true.Equals(dgr.Cells["checkbox"].Value))
                                    {
                                        labs = "'" + dgr.Cells["瓶签号"].Value + "'," + labs;
                                    }
                                }
                                if (!string.IsNullOrEmpty(labs))
                                {
                                    new PivasDrugCount.DrugsCount((label2.Visible ? label2.Text : comboBox2.SelectedItem.ToString()), comboBox3.SelectedItem.ToString(), comboBox4.SelectedItem.ToString(), labs.TrimEnd(','), userID).ShowDialog();
                                    //new DrugsCount(this, labs.TrimEnd(',')).ShowDialog();
                                }
                                break;
                            }
                        case 1:
                            {
                                BQlabelDataSet gdv = ((BQlabelDataSet)flowLayoutPanel2.Controls[0]);
                                foreach (DataGridViewRow dgr in gdv.dataGridView2.Rows)
                                {
                                    if (true.Equals(dgr.Cells["checkbox"].Value))
                                    {
                                        labs = "'" + dgr.Cells["瓶签号"].Value + "'," + labs;
                                    }
                                }
                                if (!string.IsNullOrEmpty(labs))
                                {
                                    new PivasDrugCount.DrugsCount((label2.Visible ? label2.Text : comboBox2.SelectedItem.ToString()), comboBox3.SelectedItem.ToString(), comboBox4.SelectedItem.ToString(), labs.TrimEnd(','), userID).ShowDialog();
                                    //new DrugsCount(this, labs.TrimEnd(',')).ShowDialog();
                                }
                                break;
                            }
                        case 2:
                            {
                                if (((BQDetail)flowLayoutPanel2.Controls[0]).panel1.HasChildren)
                                {
                                    foreach (var item in prints)
                                    {
                                        if (item.Value)
                                        {
                                            labs = "'" + item.Key + "'," + labs;
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(labs))
                                    {
                                        new PivasDrugCount.DrugsCount((label2.Visible ? label2.Text : comboBox2.SelectedItem.ToString()), comboBox3.SelectedItem.ToString(), comboBox4.SelectedItem.ToString(), labs.TrimEnd(','), userID).ShowDialog();
                                        //new DrugsCount(this, labs.TrimEnd(',')).ShowDialog();
                                    }
                                }
                                break;
                            }
                        case 3:
                            {
                                if (!string.IsNullOrEmpty(WardCodes))
                                {
                                    DataSet dt = dbHelp.GetPIVAsDB(string.Format("SELECT distinct LabelNo FROM IVRecord iv INNER JOIN Prescription P ON IV.PrescriptionID=P.PrescriptionID AND LabelOver > case when IVStatus=0 then -1 else -3 end  and BatchSaved = 1 and DATEDIFF(DAY,InfusionDT,'{0}')=0 " + (checkBox3.CheckState == CheckState.Checked ? string.Empty : string.Format(" and IV.WardCode in ({0}) ", WardCodes.TrimEnd(','))) + " LEFT JOIN IVRecordDetail IVD ON IV.IVRecordID=IVD.IVRecordID LEFT JOIN DDrug DD ON IVD.DrugCode=DD.DrugCode  WHERE [IVStatus]" + ad + OrderID + isJustOne + Specs + Position + ByUsageCode + " {1}", dateTimePicker1.Value.ToString("yyyy-MM-dd"), checkBox1.CheckState == CheckState.Checked ? "" : " and IVD.DrugCode in (" + DrugCodes.TrimEnd(',') + ")"));
                                    foreach (DataRow dr in dt.Tables[0].Rows)
                                    {
                                        labs = "'" + dr[0].ToString().Trim() + "'," + labs;
                                    }
                                    if (!string.IsNullOrEmpty(labs))
                                    {
                                        new PivasDrugCount.DrugsCount((label2.Visible ? label2.Text : comboBox2.SelectedItem.ToString()), comboBox3.SelectedItem.ToString(), comboBox4.SelectedItem.ToString(), labs.TrimEnd(','), userID).ShowDialog();
                                        //new DrugsCount(this, labs.TrimEnd(',')).ShowDialog();
                                    }
                                }
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            new PageSec(this).ShowDialog();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel2.HasChildren)
                if (MessageBox.Show("确认发药!!!", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    switch (PreviewMode)
                    {
                        case 0:
                            {
                                BQlabelDataSet gdv = ((BQlabelDataSet)flowLayoutPanel2.Controls[0]);
                                selected = string.Empty;
                                foreach (DataGridViewRow dgr in gdv.dataGridView2.Rows)
                                {
                                    if (true.Equals(dgr.Cells["checkbox"].Value))
                                    {
                                        selected = "'" + dgr.Cells["瓶签号"].Value + "'," + selected;
                                    }
                                }
                                if (!string.IsNullOrEmpty(selected))
                                {
                                    selected = selected.TrimEnd(',');
                                    if (dbHelp.SetPIVAsDB(string.Format("UPDATE [IVRecord] SET Remark4='已发药' where LabelNo in ({0})", selected)) > 0)
                                    {
                                        MessageBox.Show("发药成功");
                                    }
                                    else
                                    {
                                        MessageBox.Show("发药失败");
                                    }
                                }
                                break;
                            }
                        case 1:
                            {
                                BQlabelDataSet gdv = ((BQlabelDataSet)flowLayoutPanel2.Controls[0]);
                                selected = string.Empty;
                                foreach (DataGridViewRow dgr in gdv.dataGridView2.Rows)
                                {
                                    if (true.Equals(dgr.Cells["checkbox"].Value))
                                    {
                                        selected = "'" + dgr.Cells["瓶签号"].Value + "'," + selected;
                                    }
                                }
                                if (!string.IsNullOrEmpty(selected))
                                {
                                    selected = selected.TrimEnd(',');
                                    if (dbHelp.SetPIVAsDB(string.Format("UPDATE [IVRecord] SET Remark4='已发药' where LabelNo in ({0})", selected)) > 0)
                                    {
                                        MessageBox.Show("发药成功");
                                    }
                                    else
                                    {
                                        MessageBox.Show("发药失败");
                                    }
                                }
                                break;
                            }
                        case 2:
                            {
                                if (((BQDetail)flowLayoutPanel2.Controls[0]).panel1.HasChildren)
                                {
                                    selected = string.Empty;
                                    foreach (var item in prints)
                                    {
                                        if (item.Value)
                                        {
                                            selected = "'" + item.Key + "'," + selected;
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(selected))
                                    {
                                        selected = selected.TrimEnd(',');
                                        if (dbHelp.SetPIVAsDB(string.Format("UPDATE [IVRecord] SET Remark4='已发药' where LabelNo in ({0})", selected)) > 0)
                                        {
                                            MessageBox.Show("发药成功");
                                        }
                                        else
                                        {
                                            MessageBox.Show("发药失败");
                                        }
                                    }
                                }
                                break;
                            }
                        default:
                            {
                                MessageBox.Show("该模式不支持发药！！！");
                                break;
                            }
                    }
                }
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                dataGridView2.Rows[e.RowIndex].Cells["check"].Value = !true.Equals(dataGridView2.Rows[e.RowIndex].Cells["check"].Value);
                WardCodes = string.Empty;
                bool ched = false;
                bool state = true;
                foreach (DataGridViewRow dgvr in dataGridView2.Rows)
                {
                    if (Equals(dgvr.Cells[0].Value, true))
                    {
                        WardCodes = "'" + dgvr.Cells["wcode"].Value + "'," + WardCodes;
                        ched = true;
                    }
                    else
                    {
                        state = false;
                    }
                }
                checkBox3.CheckState = ched ? (state ? CheckState.Checked : CheckState.Indeterminate) : CheckState.Unchecked;


                //判断病区列表有被选择
                if (string.IsNullOrEmpty(WardCodes))
                {
                    //未有被选择
                    if (flowLayoutPanel2.HasChildren)
                        flowLayoutPanel2.Controls[0].Dispose();
                    if (dataGridView1.DataSource != null)
                    {
                        textBox3.Text = string.Empty;
                        (dataGridView1.DataSource as DataTable).Rows.Clear();
                    }
                }
                else
                {
                    textBox3.Text = string.Empty;
                    textBox3_TextChanged(sender, e);
                    //生成瓶签数据
                    showLabelNO();
                }
            }
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ((char)13))
            {
                PivasIVRP_Load(sender, e);
            }
        }
        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.SelectAll();
        }
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text.Trim()))
            {
                textBox2.Text = "病区名/简拼";
            }
            textBox2.ForeColor = Color.Silver;
        }
        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.ForeColor = Color.Black;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (flowLayoutPanel2.HasChildren && flowLayoutPanel2.Controls[0] is BQlabelDataSet)
                {
                    BQlabelDataSet gdv = flowLayoutPanel2.Controls[0] as BQlabelDataSet;
                    List<string> ls = new List<string>();
                    foreach (DataGridViewRow dgr in gdv.dataGridView2.Rows)
                    {
                        int Remark3 = 10;
                        int.TryParse(dgr.Cells["Remark3"].Value.ToString().Trim(), out Remark3);
                        if (true.Equals(dgr.Cells["checkbox"].Value) && Remark3 < 10 && !ls.Contains(dgr.Cells["瓶签号"].Value.ToString().Trim()))
                        {
                            ls.Add(dgr.Cells["瓶签号"].Value.ToString().Trim());
                        }
                    }
                    if (ls.Count > 0)
                    {
                        if (new ToHIS(ls, userID).ShowDialog() == DialogResult.OK)
                        {
                            MessageBox.Show("完成");
                        }
                        else
                        {
                            MessageBox.Show("失败");
                        }
                        getLabelno();
                    }
                    else
                    {
                        MessageBox.Show("没有需要计费的瓶签");
                    }
                }
                else
                {
                    MessageBox.Show("没有数据");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel2.HasChildren)
                if (MessageBox.Show("确认重排仓位!!!", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    string labelnos = string.Empty;
                    switch (PreviewMode)
                    {
                        case 0:
                            {
                                BQlabelDataSet gdv = ((BQlabelDataSet)flowLayoutPanel2.Controls[0]);
                                foreach (DataGridViewRow dgr in gdv.dataGridView2.Rows)
                                {
                                    if (true.Equals(dgr.Cells["checkbox"].Value))
                                    {
                                        labelnos = "'" + dgr.Cells["瓶签号"].Value.ToString() + "'," + labelnos;
                                    }
                                }
                                if (string.IsNullOrEmpty(labelnos))
                                {
                                    MessageBox.Show("未选择瓶签");
                                }
                                else
                                {
                                    NowST = "开始重排！！！";
                                    Thread th = new Thread(() => { new Abut(this).ShowDialog(); });
                                    th.IsBackground = true;
                                    th.Start();
                                    dbHelp.SetPIVAsDB(string.Format("EXEC dbo.AutoIVDeskNo '{0}','{1}','{2}'", labelnos.TrimEnd(',').Replace("'", "''"), userID, dateTimePicker1.Value.ToString("yyyy-MM-dd")));
                                    NowST = "重排完成！！！";
                                    using (DataSet ds = dbHelp.GetPIVAsDB(string.Format("select distinct DeskNo  FROM IVRecord WHERE LabelOver  > case when IVStatus=0 then -1 else -3 end AND BatchSaved = 1 AND DATEDIFF(DAY,InfusionDT,'{0}')=0 ", dateTimePicker1.Value.ToString("yyyy-MM-dd"))))
                                    {
                                        string cb5 = comboBox5.SelectedItem.ToString();
                                        comboBox5.Items.Clear();
                                        comboBox5.Items.Add("所有仓位");
                                        foreach (DataRow dr in ds.Tables[0].Rows)
                                        {
                                            if (!string.IsNullOrEmpty(dr[0].ToString().Trim()))
                                                comboBox5.Items.Add(dr[0]);
                                        }
                                        comboBox5.SelectedIndex = comboBox5.Items.Contains(cb5) ? comboBox5.Items.IndexOf(cb5) : 0;
                                    }
                                    MessageBox.Show("重排成功");
                                    getLabelno();
                                }
                                break;
                            }
                        case 1:
                            {
                                BQlabelDataSet gdv = ((BQlabelDataSet)flowLayoutPanel2.Controls[0]);
                                foreach (DataGridViewRow dgr in gdv.dataGridView2.Rows)
                                {
                                    if (true.Equals(dgr.Cells["checkbox"].Value))
                                    {
                                        labelnos = "'" + dgr.Cells["瓶签号"].Value.ToString() + "'," + labelnos;
                                    }
                                }
                                if (string.IsNullOrEmpty(labelnos))
                                {
                                    MessageBox.Show("未选择瓶签");
                                }
                                else
                                {
                                    NowST = "开始重排！！！";
                                    Thread th = new Thread(() => { new Abut(this).ShowDialog(); });
                                    th.IsBackground = true;
                                    th.Start();
                                    dbHelp.SetPIVAsDB(string.Format("EXEC dbo.AutoIVDeskNo '{0}','{1}','{2}'", labelnos.TrimEnd(',').Replace("'", "''"), userID, dateTimePicker1.Value.ToString("yyyy-MM-dd")));
                                    NowST = "重排完成！！！";
                                    using (DataSet ds = dbHelp.GetPIVAsDB(string.Format("select distinct DeskNo  FROM IVRecord WHERE LabelOver  > case when IVStatus=0 then -1 else -3 end AND BatchSaved = 1 AND DATEDIFF(DAY,InfusionDT,'{0}')=0 ", dateTimePicker1.Value.ToString("yyyy-MM-dd"))))
                                    {
                                        string cb5 = comboBox5.SelectedItem.ToString();
                                        comboBox5.Items.Clear();
                                        comboBox5.Items.Add("所有仓位");
                                        foreach (DataRow dr in ds.Tables[0].Rows)
                                        {
                                            if (!string.IsNullOrEmpty(dr[0].ToString().Trim()))
                                                comboBox5.Items.Add(dr[0]);
                                        }
                                        comboBox5.SelectedIndex = comboBox5.Items.Contains(cb5) ? comboBox5.Items.IndexOf(cb5) : 0;
                                    }
                                    MessageBox.Show("重排成功");
                                    getLabelno();
                                }
                                break;
                            }
                        case 2:
                            {
                                if (((BQDetail)flowLayoutPanel2.Controls[0]).panel1.HasChildren)
                                {
                                    foreach (var item in prints)
                                    {
                                        if (item.Value)
                                        {
                                            labelnos = "'" + item.Key + "'," + labelnos;
                                        }
                                    }
                                    if (string.IsNullOrEmpty(labelnos))
                                    {
                                        MessageBox.Show("未选择瓶签");
                                    }
                                    else
                                    {
                                        NowST = "开始重排！！！";
                                        Thread th = new Thread(() => { new Abut(this).ShowDialog(); });
                                        th.IsBackground = true;
                                        th.Start();
                                        dbHelp.SetPIVAsDB(string.Format("EXEC dbo.AutoIVDeskNo '{0}','{1}','{2}'", labelnos.TrimEnd(',').Replace("'", "''"), userID, dateTimePicker1.Value.ToString("yyyy-MM-dd")));
                                        NowST = "重排完成！！！";
                                        using (DataSet ds = dbHelp.GetPIVAsDB(string.Format("select distinct DeskNo  FROM IVRecord WHERE LabelOver  > case when IVStatus=0 then -1 else -3 end AND BatchSaved = 1 AND DATEDIFF(DAY,InfusionDT,'{0}')=0 ", dateTimePicker1.Value.ToString("yyyy-MM-dd"))))
                                        {
                                            string cb5 = comboBox5.SelectedItem.ToString();
                                            comboBox5.Items.Clear();
                                            comboBox5.Items.Add("所有仓位");
                                            foreach (DataRow dr in ds.Tables[0].Rows)
                                            {
                                                if (!string.IsNullOrEmpty(dr[0].ToString().Trim()))
                                                    comboBox5.Items.Add(dr[0]);
                                            }
                                            comboBox5.SelectedIndex = comboBox5.Items.Contains(cb5) ? comboBox5.Items.IndexOf(cb5) : 0;
                                        }
                                        MessageBox.Show("重排成功");
                                        getLabelno();
                                    }
                                }
                                break;
                            }
                        default:
                            {
                                MessageBox.Show("该模式不支持重排！！！");
                                break;
                            }
                    }
                }
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (DataSet ds = dbHelp.GetPIVAsDB(getsql("2")))
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        Drugs = ds.Tables[0];
                    }
                }
                using (DataTable dt = Drugs.Copy())
                {
                    if (!string.IsNullOrEmpty(textBox3.Text.Trim()))
                    {
                        DataRow[] drs = DDrug.Select(string.Format("DrugName not like ('%{0}%') and SpellCode not like ('%{0}%')", textBox3.Text.Trim().ToUpper()));
                        foreach (DataRow dr in drs)
                        {
                            DataRow[] rdrs = dt.Select(string.Format("DrugCode='{0}'", dr["DrugCode"]));
                            foreach (DataRow rdr in rdrs)
                            {
                                dt.Rows.Remove(rdr);
                            }
                        }
                    }
                    dataGridView1.DataSource = dt;
                }
                dataGridView1.Columns["DrugCode"].Visible = false;
                dataGridView1.Columns["DrugName"].Width = 160;
                checkBox1.CheckState = string.IsNullOrEmpty(textBox3.Text.Trim()) ? CheckState.Checked : CheckState.Indeterminate;
                if (DrugColor.Tables.Count > 0 && DrugColor.Tables[0].Rows.Count > 0)
                {
                    foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                    {
                        dgvr.DefaultCellStyle.BackColor = Color.FromArgb(Convert.ToInt32(DrugColor.Tables[0].Rows.Find(dgvr.Cells["DrugCode"].Value)["DrugColor"]));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                showLabelNO();
            }
        }
        protected internal void SaveDrugColor()
        {
            try
            {
                if (DrugColor.Tables.Count > 0 && DrugColor.Tables[0].Rows.Count > 0)
                {
                    if (DrugColor.HasChanges(DataRowState.Modified))
                    {
                        using (SqlCommandBuilder scb = new SqlCommandBuilder(sda))
                        {
                            scb.GetUpdateCommand();
                            scb.DataAdapter.Update(DrugColor.GetChanges(DataRowState.Modified));
                            DrugColor.AcceptChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    using (ColorDialog cd = new ColorDialog())
                    {
                        cd.Color = dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor;
                        if (cd.ShowDialog() == DialogResult.OK)
                        {
                            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = cd.Color;
                            DrugColor.Tables[0].Rows.Find(dataGridView1.Rows[e.RowIndex].Cells["DrugCode"].Value)["DrugColor"] = cd.Color.ToArgb();
                            SaveDrugColor();
                        }
                    }
                }
                else if (e.Button == MouseButtons.Left)
                {
                    dataGridView1.Rows[e.RowIndex].Cells["Column2"].Value = !true.Equals(dataGridView1.Rows[e.RowIndex].Cells["Column2"].Value);
                    bool ched = false;
                    bool state = true;
                    foreach (DataGridViewRow dr in dataGridView1.Rows)
                    {
                        if (true.Equals(dr.Cells["Column2"].Value))
                            ched = true;
                        else
                            state = false;
                    }
                    if (string.IsNullOrEmpty(textBox3.Text.Trim()))
                    {
                        checkBox1.CheckState = ched ? (state ? CheckState.Checked : CheckState.Indeterminate) : CheckState.Unchecked;
                    }
                    else
                    {
                        checkBox1.CheckState = ched ? CheckState.Indeterminate : CheckState.Unchecked;
                    }
                    showLabelNO();
                }
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            new BatchS(this, 1).ShowDialog();
        }

        #region 接口实现
        void IMenuManager.menuBeforeSelect()
        {
            if (HasLoad)
            {
                try
                {
                    comboBox3.Visible = UP2;
                    comboBox4.Visible = Species;
                    comboBox5.Visible = Positions;
                    if (dataGridView1.DataSource != null)
                    {
                        textBox3.Text = string.Empty;
                        (dataGridView1.DataSource as DataTable).Rows.Clear();
                    }
                    else
                    {
                        (dataGridView1.Columns[0] as DataGridViewCheckBoxColumn).TrueValue = true;
                    }
                    textValue = WardCodes = DrugCodes = string.Empty;
                    showLabelNO();
                    panel5.Visible = false;
                    panel5.Location = new Point(-30, panel5.Location.Y);
                    panel5.Refresh();
                    label1.Text = ">";
                    label1.Visible = DrugList;
                    panelPrint.Refresh();
                    ad = (comboBox1.SelectedIndex == 2) ? ">-1 " : ((comboBox1.SelectedIndex == 1) ? ">0 " : "=0 ");
                    if (!string.IsNullOrEmpty(BatS))
                    {
                        OrderID = BatS.Contains(",") ? string.Format(" and Batch in({0}) ", BatS) : string.Format(" and Batch ={0} ", BatS);
                    }
                    else
                    {
                        if (comboBox2.SelectedIndex == 0)
                        {
                            OrderID = string.Empty;
                        }
                        else if (comboBox2.SelectedItem.ToString() == "所有K")
                        {
                            OrderID = " and Batch like('%K')";
                        }
                        else if (comboBox2.SelectedItem.ToString() == "所有#")
                        {
                            OrderID = " and Batch like('%#')";
                        }
                        else if (comboBox2.SelectedItem.ToString() == "所有L")
                        {
                            OrderID = " and Batch like('L%')";
                        }
                        else if (comboBox2.SelectedItem.ToString().ToUpper().Contains("K") || comboBox2.SelectedItem.ToString().Contains("#") || comboBox2.SelectedItem.ToString().ToUpper().Contains("L"))
                        {
                            OrderID = " and Batch ='" + comboBox2.SelectedItem.ToString() + "'";
                        }
                        else
                        {
                            OrderID = " and TeamNumber=" + comboBox2.SelectedItem.ToString() + "";
                        }
                    }
                    if (comboBox3.Visible)
                    {
                        switch (comboBox3.SelectedIndex)
                        {
                            case 0:
                                {
                                    isJustOne = string.Empty;
                                    break;
                                }
                            case 1:
                                {
                                    isJustOne = " and [DrugCount]=1 ";
                                    break;
                                }
                            case 2:
                                {
                                    isJustOne = " and [DrugCount]=2 ";
                                    break;
                                }
                            case 3:
                                {
                                    isJustOne = " and [DrugCount]=3 ";
                                    break;
                                }
                            default:
                                {
                                    isJustOne = " and [DrugCount]>" + (string.IsNullOrEmpty(dbHelp.GetPivasAllSet("打印-多药数量设置")) ? "2" : dbHelp.GetPivasAllSet("打印-多药数量设置"));
                                    break;
                                }
                        }
                    }
                    else
                    {
                        isJustOne = string.Empty;
                    }

                    ByUsageCode = string.IsNullOrEmpty(UsageCodeS) ? string.Empty
                        : (UsageCodeS.Contains(",") ? string.Format(" and UsageCode in({0}) ", UsageCodeS)
                        : string.Format(" and UsageCode={0} ", UsageCodeS));

                    Specs = (comboBox4.SelectedIndex == 0 || !comboBox4.Visible) ? string.Empty
                        : (comboBox4.SelectedIndex > 5 ? (" and Species='" + comboBox4.SelectedItem.ToString() + "'")
                        : (" and DrugType=" + comboBox4.SelectedIndex));
                    Position = (comboBox5.SelectedIndex == 0 || !comboBox5.Visible) ? string.Empty : (string.Format(" and DeskNo='{0}' ", comboBox5.SelectedItem.ToString()));
                    using (DataSet ds = dbHelp.GetPIVAsDB(string.Format(getsql("getWardAreaS"), dateTimePicker1.Value.ToString("yyyy-MM-dd"), WardOpen)))
                    {
                        if (ds != null && ds.Tables.Count == 3)
                        {
                            ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns[0] };
                            ds.Tables[1].PrimaryKey = new DataColumn[] { ds.Tables[1].Columns[0] };
                            ds.Tables[2].PrimaryKey = new DataColumn[] { ds.Tables[2].Columns[0] };
                            using (DataTable DT = new DataTable())
                            {
                                DT.Columns.Add("WardCode", typeof(string));
                                DT.Columns.Add("WardSimName", typeof(string));
                                DT.Columns.Add("coun", typeof(int));
                                DT.Columns.Add("allcoun", typeof(int));
                                DT.Columns.Add("WardSeqNo", typeof(int));
                                DT.PrimaryKey = new DataColumn[] { DT.Columns[0] };
                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    if (ds.Tables[2].Rows.Contains(dr["WardCode"].ToString()))
                                    {
                                        int allcoun = 0;
                                        int.TryParse(ds.Tables[2].Rows.Find(dr["WardCode"].ToString())["allcoun"].ToString(), out allcoun);
                                        int coun = 0;
                                        if (ds.Tables[1].Rows.Contains(dr["WardCode"].ToString()))
                                        {
                                            int.TryParse(ds.Tables[1].Rows.Find(dr["WardCode"].ToString())["coun"].ToString(), out coun);
                                        }
                                        int val = WardIdle ? 0 : -1;
                                        if (coun > val)
                                        {
                                            DT.Rows.Add(dr["WardCode"], dr["WardSimName"], coun, allcoun, dr["WardSeqNo"]);
                                        }
                                    }
                                }
                                DataTable DWS = new DataTable();
                                DWS.Columns.Add("WardCode", typeof(string));
                                DWS.Columns.Add("WardSimName", typeof(string));
                                DWS.Columns.Add("coun", typeof(int));
                                DWS.Columns.Add("allcoun", typeof(int));
                                DWS.PrimaryKey = new DataColumn[] { DWS.Columns[0] };
                                foreach (DataRow dr in DT.Select(string.Empty, "[WardSeqNo] asc,coun desc,allcoun desc"))
                                {
                                    DWS.Rows.Add(dr["WardCode"], dr["WardSimName"], dr["coun"], dr["allcoun"]);
                                }
                                dataGridView2.DataSource = DWS;
                                if (DWS.Rows.Count > 0)
                                {
                                    checkBox3.Enabled = true;
                                    foreach (DataGridViewRow dgvr in dataGridView2.Rows)
                                    {
                                        dgvr.Cells[0].Value = false;
                                    }
                                }
                                else
                                {
                                    checkBox3.Enabled = false;
                                    using (ShowMessage sm = new ShowMessage("没有符合此条件的数据"))
                                    {
                                        sm.ShowDialog();
                                    }
                                }
                            }
                            checkBox3.Checked = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            timechage = false;
        }

        void IMenuManager.menuAfterSelect()
        {
        }
        #endregion
    }
}
