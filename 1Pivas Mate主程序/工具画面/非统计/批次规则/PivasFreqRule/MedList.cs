using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using PIVAsCommon.Helper;

namespace PivasFreqRule
{
    public partial class MedList : Form
    {
        DB_Help DB = new DB_Help();
        DataSet DS = new DataSet();
        DataTable DT = new DataTable();
        private string MName;
        private string MCode;
        private int IsMedClass;


        public static string MeID;
        public static int IsClass;
        public static string MeName;
        public static string MeCode;
        public static string MeWardCode;
        public static string MeSeqNo;
        

        public MedList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 药品页面加载事件，初始化药物列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MedList_Load(object sender, EventArgs e)
        {
            //string str1 = "update DrugMain..MedClass set ParentCode = '0' where ParentNo = 0;" +
            //            " update dmm1 set dmm1.ParentCode = dmm2.MedClassCode" +
            //            " from DrugMain..MedClass dmm1, DrugMain..MedClass dmm2" +
            //            " where dmm1.ParentNo = dmm2.MedClassID" +
            //            " and dmm1.ParentNo <> 0";
            //string str = "select ParentCode,MedClassCode,MedClassName from DrugMain..MedClass Order By MedClassCode";

            string str = "  SELECT MC.ParentID ,MC.MedicineClassCode,MC.MedicineClassName ,MC.MedicineClassID FROM KD0100..MedicineClass AS MC Order By MedicineClassID ";

            try
            {
                //DB.SetPIVAsDB(str1);
                DS = DB.GetPIVAsDB(str);
                DT = DS.Tables[0];
                listNode();//显示根节点
                listChildNode(tvMedlist.Nodes[0]);//遍历子节点
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 显示根节点
        /// </summary>
        private void listNode()
        {
            DataRow[] R = DT.Select("ParentID = 0");
            foreach (DataRow r in R)
            {
                TreeNode node = new TreeNode();
                node.Text = r["MedicineClassName"].ToString();
                node.Name = r["MedicineClassID"].ToString();
                node.Tag = 1;
                tvMedlist.Nodes.Add(node);
            }
        }

        /// <summary>
        /// 显示子节点
        /// </summary>
        /// <param name="ParentNode">父节点</param>
        private void listChildNode(TreeNode ParentNode)
        {
            if (ParentNode==null)
                return;

            DataRow[] R = DT.Select("ParentID = '" + ParentNode.Name + "'");

            foreach (DataRow r in R)
            {
                TreeNode node = new TreeNode();
                node.Text = r["MedicineClassName"].ToString();
                node.Name = r["MedicineClassID"].ToString();
                node.Tag = 1;
                ParentNode.Nodes.Add(node);                
            }

            listChildNode(ParentNode.FirstNode);
            listChildNode(ParentNode.NextNode);
        }

        /// <summary>
        /// 节点展开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvMedlist_AfterExpand(object sender, TreeViewEventArgs e)
        {
            try
            {
                tvMedlist.SelectedNode = e.Node;
                StringBuilder str = new StringBuilder();
                str.Append(" select distinct MC.MedicineClassID,MC.MedicineClassCode ,M.MedicineID,M.MedicineName, MC.MedicineClassName ,MC.ParentID ");
                str.Append(" FROM KD0100..MedicineClass AS MC ");
                str.Append("inner join KD0100..[MedicineClass-Medicine] AS MM ON MM.MedicineClassID = MC.MedicineClassID ");
                str.Append("inner join KD0100..[Medicine] AS M ON M.MedicineID = MM.MedicineID ");
               // str.Append("WHERE MC.MedicineClassCode= '" + tvMedlist.SelectedNode.Name + "' AND MC.IsMasterClass = 1 ORDER BY M.MedicineGeneralName");
                str.Append("WHERE MC.MedicineClassID= '" + tvMedlist.SelectedNode.Name + "' ORDER BY MC.MedicineClassID");
                DS = DB.GetPIVAsDB(str.ToString());

                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
                {
                    TreeNode node = new TreeNode();
                    node.Text = DS.Tables[0].Rows[i]["MedicineName"].ToString();
                    node.Name = DS.Tables[0].Rows[i]["MedicineID"].ToString();
                    node.Tag = 0;
                    tvMedlist.SelectedNode.Nodes.Add(node);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            string SeqNo;            

            if(MedcineRule.status=="add")
            {
                if (MCode != null && MCode.Trim ()!=string.Empty)
                {
                    SeqNo = Convert.ToString(Convert.ToInt64(MedcineRule.SeqNo) + 1);
                    AddMedcineRule(Convert.ToBoolean(IsMedClass), MCode, MName, MedcineRule.WardCode, SeqNo);

                    IsClass = IsMedClass;
                    MeCode = MCode;
                    MeName = MName;
                    MeWardCode = MedcineRule.WardCode;
                    MeSeqNo = SeqNo;
                }
                else
                {
                    MessageBox.Show("  您没选择任何一项  ！！！");
                }
            
            }
        }

        //private void UpdateMedcineRule(string MedCode,string MedName,string ID,bool IsMedClass)
        //{
        //    string str,str1;
        //    try
        //    {
        //        str = "DELETE OrderMPRuleSub WHERE MPRuleID = " + ID +
        //            " UPDATE OrderMPRule SET IsMedClass = '" + IsMedClass + "', MedCode = '" + MedCode + "', " +
        //            "MedName = '" + MedName + "'  WHERE MPRuleID = " + ID;
        //        DB.SetPIVAsDB(str);


        //        if (IsMedClass)
        //        {
        //            AddMPRuleSub(ID, MedCode);
        //        } 
        //        else
        //        {
        //            str1 = "INSERT OrderMPRuleSub (MPRuleID, MedCode) VALUES (" + ID + ",'" + MedCode + "')";
        //            DB.SetPIVAsDB(str1);
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }

        //}

        /// <summary>
        /// 确定按钮方法
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Code"></param>
        //private void AddMPRuleSub(string ID,string Code)
        //{
        //    string str = "INSERT OrderMPRuleSub (MPRuleID, MedCode)" +
        //                " SELECT " + ID + " AS MPRuleID, MedCode FROM [MedClass-Med] WHERE MedClassCode = '" + Code + "' ;" +
        //                " SELECT MedicineClassCode FROM KD0100..MedicineClass WHERE ParentID = '" + Code + "' ";
        //    try
        //    {
        //        DataSet D = new DataSet();
        //        D = DB.GetPIVAsDB(str);
        //        DataTable T = D.Tables[0];
        //        for (int i = 0; i < T.Rows.Count;i++ )
        //        {
        //            string s = T.Rows[i]["MedicineClassCode"].ToString();
        //            if (s == "")
        //                continue;
        //            AddMPRuleSub(ID, s);
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //lvMedList.Items.Clear();
                string strSearch="'%"+txtSearch.Text+"%'";
                string str = "SELECT MedicineName AS [Name], MedicineID AS [Code] FROM KD0100..[Medicine] " +
                    "WHERE SpellCode LIKE " + strSearch + " OR [MedicineName] LIKE " + strSearch + " ORDER BY [MedicineName]";
                DS = DB.GetPIVAsDB(str);
                dgvMedlist.DataSource = DS.Tables[0];
                dgvMedlist.Columns[0].Width = 180;
                dgvMedlist.Columns[1].Width = 60;                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Down)
            {
                dgvMedlist.Select();
            }
        }


  /// <summary>
  /// 单击节点事件
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
        private void tvMedlist_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                tvMedlist.SelectedNode = e.Node;
                MName = tvMedlist.SelectedNode.Text;
                IsMedClass = (int)tvMedlist.SelectedNode.Tag;
                if (IsMedClass == 1)
                {
                    DataRow[] R = DT.Select("MedicineClassID = '" + tvMedlist.SelectedNode.Name + "'");
                    MCode = R[0]["MedicineClassCode"].ToString();
                }
                else
                {
                    MCode = tvMedlist.SelectedNode.Name;
                }
             
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 新增添加的药品详细信息
        /// </summary>
        /// <param name="IsMedClass"></param>
        /// <param name="MedCode"></param>
        /// <param name="MedName"></param>
        /// <param name="WardCode"></param>
        /// <param name="SeqNo"></param>
        private void AddMedcineRule(bool IsMedClass,string MedCode,string MedName,string WardCode,string SeqNo )
        {
            //新增药品前需要查询添加数据的唯一性
            DataSet das = null;
            string sql = "";
            try
            {
                if (int.Parse(SeqNo) <= 150000000)
                {
                     sql = "select * from OrderMPRule where WardCode = '" + WardCode + "'  and MedCode = '" + MedCode + "'  and SeqNo <= " + SeqNo;
                    
                }
                else
                {
                     sql = "select * from OrderMPRule where WardCode = '" + WardCode + "' and MedCode = '" + MedCode + "'  and SeqNo > "+150000000+" and SeqNo <= " + SeqNo;
                }
                if (MedCode.Trim() != string.Empty)
                {
                    das = DB.GetPIVAsDB(sql);
                }
            }
            catch (System.Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            try
            {
                if (das.Tables[0].Rows.Count > 0)
                {
                    //MessageBox.Show("This medcine is added!");
                    return;
                }
                else
                {

                    string ID;
                    string str = "INSERT OrderMPRule (WardCode, IsMedClass, MedCode, MedName, SeqNo) VALUES ('" +
                                WardCode + "','" + IsMedClass + "','" + MedCode + "','" + MedName + "'," + SeqNo + ");" +
                                "SELECT IDENT_CURRENT('OrderMPRule') AS MPRuleID";
                    DS = DB.GetPIVAsDB(str);
                    ID = DS.Tables[0].Rows[0]["MPRuleID"].ToString();
                    MeID = ID;

                    //if (IsMedClass)
                    //{
                    //AddMPRuleSub(ID, MedCode);
                    //} 
                    //else
                    //{
                    str = "INSERT OrderMPRuleSub (MPRuleID, MedCode) VALUES (" + ID + ",'" + MedCode + "')";
                    DB.SetPIVAsDB(str);
                    //}
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMedlist_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter && MedcineRule.status == "add")
                {
                    string SeqNo;
                    SeqNo = Convert.ToString(Convert.ToInt64(MedcineRule.SeqNo) + 1);
                    MCode = dgvMedlist.Rows[dgvMedlist.CurrentRow.Index-1].Cells["Code"].Value.ToString();
                    MName = dgvMedlist.Rows[dgvMedlist.CurrentRow.Index-1].Cells["Name"].Value.ToString();
                    AddMedcineRule(false, MCode, MName, MedcineRule.WardCode, SeqNo);

                    IsClass = 0;
                    MeCode = MCode;
                    MeName = MName;
                    MeWardCode = MedcineRule.WardCode;
                    MeSeqNo = SeqNo;
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
                //if (e.KeyChar == (char)Keys.Enter && MedcineRow.status == "change")
                //{
                //    MCode = dgvMedlist.Rows[dgvMedlist.CurrentRow.Index-1].Cells["Code"].Value.ToString();
                //    MName = dgvMedlist.Rows[dgvMedlist.CurrentRow.Index-1].Cells["Name"].Value.ToString();
                //    //UpdateMedcineRule(MCode, MName, MedcineRow.MedID, false);
                    
                //    MeCode = MCode;
                //    MeName = MName;
                //    this.DialogResult = DialogResult.Yes;
                //    this.Close();
                //}
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 搜索界面，确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK1_Click(object sender, EventArgs e)
        {
            string SeqNo;

            if (MedcineRule.status == "add")
            {
                SeqNo = Convert.ToString(Convert.ToInt64(MedcineRule.SeqNo) + 1);
                MCode = dgvMedlist.Rows[dgvMedlist.CurrentRow.Index].Cells["Code"].Value.ToString();
                MName = dgvMedlist.Rows[dgvMedlist.CurrentRow.Index].Cells["Name"].Value.ToString();
                AddMedcineRule(false, MCode, MName, MedcineRule.WardCode, SeqNo);

                IsClass = 0;
                MeCode = MCode;
                MeName = MName;
                MeWardCode = MedcineRule.WardCode;
                MeSeqNo = SeqNo;
            }

            //if (MedcineRow.status == "change")
            //{
            //    MCode = dgvMedlist.Rows[dgvMedlist.CurrentRow.Index].Cells["Code"].Value.ToString();
            //    MName = dgvMedlist.Rows[dgvMedlist.CurrentRow.Index].Cells["Name"].Value.ToString();
            //    //UpdateMedcineRule(MCode, MName, MedcineRow.MedID, false);
            //}
        }

        /// <summary>
        /// 树状图取消按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 搜索模块取消按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel1_Click(object sender, EventArgs e)
        {

        }

        #region    移动窗体用到的变量
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        #endregion

      

  

    }
}
