using PIVAsCommon.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace SetSyn
{
    public partial class USynMinSet : UserControl
    {
        protected internal static string SynCode;
        protected internal static string SynTimeCode;
        protected internal static string begin;
        protected internal static string end;
        protected internal static int space = -1;
        protected internal DB_Help db = new DB_Help();
        private USynMin uSynMin3;
        private USynMin uSynMin2;
        private USynMin uSynMin1;
        private USynMin uSynMin4;
        private USynMinSetOpen usso;


        public USynMinSet()
        {
            InitializeComponent();
        }
        public USynMinSet(string SC)
        {
            SynCode = SC;
            InitializeComponent();

        }

        private void USynMinSet_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SynCode.Trim()))
            {
                using (DataSet ds = db.GetPIVAsDB(string.Format("SELECT [SynTimeCode],[SynStarTime],[SynEndTime],[SyncSpaceTime] FROM [dbo].[SynSet] where [SynCode]='{0}'", SynCode).ToString()))
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        SynTimeCode = ds.Tables[0].Rows[0]["SynTimeCode"].ToString().Trim();
                        begin = ds.Tables[0].Rows[0]["SynStarTime"].ToString().Trim();
                        end = ds.Tables[0].Rows[0]["SynEndTime"].ToString().Trim();
                        int.TryParse(ds.Tables[0].Rows[0]["SyncSpaceTime"].ToString().Trim(), out space);
                    }
                }
                Create();
            }
        }
        private void Create()
        {
            usso = new USynMinSetOpen();
            uSynMin4 = new USynMin();
            uSynMin3 = new USynMin();
            uSynMin2 = new USynMin();
            uSynMin1 = new USynMin();

            usso.Location = new System.Drawing.Point(108, 53);
            usso.Name = "usso";
            usso.TabIndex = 4;
            usso.Visible = false;

            uSynMin4.BackColor = System.Drawing.Color.Transparent;
            uSynMin4.BackgroundImage = SetSyn.Properties.Resources.mulv13;
            uSynMin4.Location = new System.Drawing.Point(0, 165);
            uSynMin4.Margin = new System.Windows.Forms.Padding(0);
            uSynMin4.Name = "uSynMin4";
            uSynMin4.Size = new System.Drawing.Size(349, 55);
            uSynMin4.TabIndex = 3;
            uSynMin4.Tag = "4";

            uSynMin3.Location = new System.Drawing.Point(0, 110);
            uSynMin3.Margin = new System.Windows.Forms.Padding(0);
            uSynMin3.Name = "uSynMin3";
            uSynMin3.Size = new System.Drawing.Size(349, 55);
            uSynMin3.TabIndex = 2;
            uSynMin3.Tag = "3";

            uSynMin2.Location = new System.Drawing.Point(0, 55);
            uSynMin2.Margin = new System.Windows.Forms.Padding(0);
            uSynMin2.Name = "uSynMin2";
            uSynMin2.Size = new System.Drawing.Size(349, 55);
            uSynMin2.TabIndex = 1;
            uSynMin2.Tag = "2";

            uSynMin1.BackColor = System.Drawing.Color.Transparent;
            uSynMin1.BackgroundImage = SetSyn.Properties.Resources.mulv111;
            uSynMin1.Location = new System.Drawing.Point(0, 0);
            uSynMin1.Margin = new System.Windows.Forms.Padding(0);
            uSynMin1.Name = "uSynMin1";
            uSynMin1.Size = new System.Drawing.Size(349, 55);
            uSynMin1.TabIndex = 0;
            uSynMin1.Tag = "1";

            this.panel1.Controls.Add(this.usso);
            this.panel1.Controls.Add(this.uSynMin4);
            this.panel1.Controls.Add(this.uSynMin3);
            this.panel1.Controls.Add(this.uSynMin2);
            this.panel1.Controls.Add(this.uSynMin1);
        }
    }
}
