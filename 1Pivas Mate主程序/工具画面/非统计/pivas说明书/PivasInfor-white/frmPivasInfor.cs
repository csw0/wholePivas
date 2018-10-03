using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PivasInfor.LoginInfor;
using PivasInfor.MainForm;
using PivasInfor.Syn;
using PivasInfor.RevPre;
using PivasInfor.Batch;
using PivasInfor.Printer;
using PivasInfor.Tools;
using PivasInfor.Check;

namespace PivasInfor
{
    public partial class frmPivasInfor : Form
    {
        public frmPivasInfor()
        {
            InitializeComponent();
        }
        private string showFrom = string.Empty; //显示哪个窗口
        private string userType = string.Empty;     //用户类型
        public frmPivasInfor(string a)
        {
            InitializeComponent();
            showFrom = a;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            userType = "0";
            comboBox1.SelectedIndex = 0;
            AddTreeView();
            treeView1.Nodes[0].ExpandAll();
            ChangeColor(showFrom);
        }
        private void AddTreeView()
        {
            TreeNode newNode1 = new TreeNode("Pivas Mate");
            treeView1.Nodes.Add(newNode1);


            TreeNode newNode11 = new TreeNode("登陆界面");
            newNode11.Name = "login";
            newNode1.Nodes.Add(newNode11);

            TreeNode newNode12 = new TreeNode("主画面");
            newNode12.Name = "MainForm";
            newNode1.Nodes.Add(newNode12);

            TreeNode newNode13 = new TreeNode("同步界面");
            newNode13.Name = "Label_Syn";
            newNode1.Nodes.Add(newNode13);

            TreeNode newNode14 = new TreeNode("审方界面");
            newNode14.Name = "Label_Checking";
            newNode1.Nodes.Add(newNode14);

            TreeNode newNode15 = new TreeNode("批次界面");
            newNode15.Name = "Label_Batch";
            newNode1.Nodes.Add(newNode15);

            TreeNode newNode16 = new TreeNode("打印界面");
            newNode16.Name = "Label_print";
            newNode1.Nodes.Add(newNode16);         

            TreeNode newNode17 = new TreeNode("核对界面");
            newNode17.Name  = "Label_Check";
            newNode1.Nodes.Add(newNode17);

            TreeNode newNode171 = new TreeNode("摆药核对");
            newNode171.Name = "BaiYaoCheck";
            newNode17.Nodes.Add(newNode171);

            TreeNode newNode172 = new TreeNode("其他核对");
            newNode172.Name = "PaiYaoCheck";
            newNode17.Nodes.Add(newNode172);


            TreeNode newNode2 = new TreeNode("Pivas Nurse");
            treeView1.Nodes.Add(newNode2);

            //TreeNode newNode20 = new TreeNode("其他核对");
            //newNode172.Name = "PaiYaoCheck";
            //newNode17.Nodes.Add(newNode172);
            //TreeNode newNode173 = new TreeNode("进仓核对");
            //newNode173.Name = "JinCangCheck";
            //newNode17.Nodes.Add(newNode173);

            //TreeNode newNode174 = new TreeNode("配置核对");
            //newNode174.Name = "PeiZhiCheck";
            //newNode17.Nodes.Add(newNode174);

            //TreeNode newNode175 = new TreeNode("出仓核对");
            //newNode175.Name = "ChuCangCheck";
            //newNode17.Nodes.Add(newNode175);

            //TreeNode newNode176 = new TreeNode("打包核对");
            //newNode176.Name= "DaBaoCheck";
            //newNode17.Nodes.Add(newNode176);

            TreeNode newNode18 = new TreeNode("工  具");
            newNode18.Name = "Label_Tool";
            newNode1.Nodes.Add(newNode18);

            TreeNode newNode181 = new TreeNode("批次规则");
            newNode181.Name = "BatchRule";
            newNode18.Nodes.Add(newNode181);

            TreeNode newNode182 = new TreeNode("药品维护");
            newNode182.Name = "DrugHelp";
            newNode18.Nodes.Add(newNode182);

            TreeNode newNode183 = new TreeNode("医院病区");
            newNode183.Name = "DwardHelp";
            newNode18.Nodes.Add(newNode183);

           

            //TreeNode newNode18 = new TreeNode("医嘱查询");

            //newNode1.Nodes.Add(newNode18);

            //TreeNode newNode19 = new TreeNode("瓶签查询");
            //newNode1.Nodes.Add(newNode19);

            //TreeNode newNode20 = new TreeNode("工具界面");
            //newNode1.Nodes.Add(newNode20);

            //TreeNode newNode21 = new TreeNode("常见问题");
            //newNode1.Nodes.Add(newNode21);


            //TreeNode newNode22 = new TreeNode("版本信息");
            //newNode1.Nodes.Add(newNode22);
        }
        private void AddForm(string addForm)
        {
            switch (addForm)
            {
                case "login":                
                    panel1.Controls.Clear();
                    Login l = new Login(userType);
                    panel1.Controls.Add(l);
                    break;
                case "MainForm":
                    panel1.Controls.Clear();
                    MainInfor mi = new MainInfor(userType);
                    panel1.Controls.Add(mi);
                    break;
                case "Label_Syn":              
                    panel1.Controls.Clear();
                    SynForm syn = new SynForm(userType);
                    panel1.Controls.Add(syn);
                    break;
                case "Label_Checking":                   
                    panel1.Controls.Clear();
                    RevpreForm RF = new RevpreForm(userType);
                    panel1.Controls.Add(RF);
                    break;
                case "Label_Batch":                 
                    panel1.Controls.Clear();
                    BatchForm bf = new BatchForm (userType);
                    panel1.Controls.Add(bf);
                    break;
                case "Label_print":                   
                    panel1.Controls.Clear();
                   PrintForm pf = new PrintForm(userType);
                    panel1.Controls.Add(pf);
                    break;
                case "Label_Check":
                    panel1.Controls.Clear();
                    LabelCheck lc = new LabelCheck(userType);
                    panel1.Controls.Add(lc);
                    break;
                case "Label_Tool":
                    panel1.Controls.Clear();
                    ToolForm tf = new ToolForm(userType);
                    panel1.Controls.Add(tf);
                    break;

                case "BatchRule":
                    panel1.Controls.Clear();
                    BatchRule br = new BatchRule(userType);
                    panel1.Controls.Add(br);
                    break;

                case "DrugHelp":
                    panel1.Controls.Clear();
                   DrugHelp dh = new DrugHelp(userType);
                    panel1.Controls.Add(dh);
                    break;

                case "DwardHelp":
                    panel1.Controls.Clear();
                   DwardHelp ddh = new DwardHelp(userType);
                    panel1.Controls.Add(ddh);
                    break;
                case "BaiYaoCheck":
                    panel1.Controls.Clear();
                    BaiYao BY = new BaiYao(userType);
                    panel1.Controls.Add(BY);
                    break;
                case "PaiYaoCheck":
                    panel1.Controls.Clear();
                    QiTa QT = new QiTa(userType);
                    panel1.Controls.Add(QT);
                    break;

                //case "CheckForm":
                //    ChangeColor(label15.Text);
                //    panel1.Controls.Clear();
                //    LabelCheck lc = new LabelCheck();
                //    panel1.Controls.Add(lc);
                //    break;
                //case "ScanPreForm":
                //    ChangeColor(label7.Text);
                //    panel1.Controls.Clear();
                //    ScanPre spe = new ScanPre();
                //    panel1.Controls.Add(spe);
                //    break;
                //case "LabelCXForm":
                //    ChangeColor(label8.Text);
                //    panel1.Controls.Clear();
                //    LabelCX lcx = new LabelCX();
                //    panel1.Controls.Add(lcx);
                //    break;
                //case "ToolsForm":
                //    ChangeColor(label5.Text);
                //    Tool t = new Tool();
                //    panel1.Controls.Add(t);
                //    break;
                //case "ProblemForm":
                //    ChangeColor(label10.Text);
                //    panel1.Controls.Clear();
                //    Problem pb = new Problem();
                //    panel1.Controls.Add(pb);
                //    break;
                //case "EdtionForm":
                //    ChangeColor(label9.Text);
                //    panel1.Controls.Clear();
                //    Edition ed = new Edition();
                //    panel1.Controls.Add(ed);
                //    break;
                default: break;

            }  
        }

      
      


        /// <summary>
        /// 改变颜色
        /// </summary>
        /// <param name="form"></param>
        private void ChangeColor(string form)
        {
            for (int i = 0; i < treeView1.Nodes[0].Nodes.Count; i++)
            {
                if (form == treeView1.Nodes[0].Nodes[i].Name.ToString())
                {
                    treeView1.Nodes[0].Nodes[i].ForeColor = Color.White;
                    treeView1.Nodes[0].Nodes[i].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
                    treeView1.SelectedNode = treeView1.Nodes[0].Nodes[i];
                }
                else
                {
                    treeView1.Nodes[0].Nodes[i].ForeColor = Color.Black;
                    treeView1.Nodes[0].Nodes[i].BackColor = Color.White;
                }
            }
            for (int i = 0; i < treeView1.Nodes[0].Nodes["Label_Tool"].Nodes.Count; i++)
            {
                if (form == treeView1.Nodes[0].Nodes["Label_Tool"].Nodes[i].Name.ToString())
                {
                    treeView1.Nodes[0].Nodes["Label_Tool"].Nodes[i].ForeColor = Color.White;
                    treeView1.Nodes[0].Nodes["Label_Tool"].Nodes[i].BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
                    treeView1.SelectedNode = treeView1.Nodes[0].Nodes["Label_Tool"].Nodes[i];
                }
                else
                {
                    treeView1.Nodes[0].Nodes["Label_Tool"].Nodes[i].ForeColor = Color.Black;
                    treeView1.Nodes[0].Nodes["Label_Tool"].Nodes[i].BackColor = Color.White;
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Name != null)
            {
                showFrom = e.Node.Name.ToString();
                ChangeColor(showFrom);
                AddForm(e.Node.Name.ToString());
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
               PassWord pw = new PassWord("1");

                DialogResult dd = pw.ShowDialog();
                if (dd == DialogResult.Yes)
                {
                    userType = "1";
                    AddForm(showFrom);

                }
                else
                {
                    comboBox1.SelectedIndex = 0;
                    userType = "0";
                    AddForm(showFrom); 
                }

            }
            else if (comboBox1.SelectedIndex == 2)
            {
                PassWord pw = new PassWord("2");

                DialogResult dd = pw.ShowDialog();
                if (dd == DialogResult.Yes)
                {
                    userType = "2";
                    AddForm(showFrom);

                }
                else
                {
                    comboBox1.SelectedIndex = 0;
                    userType = "0";
                    AddForm(showFrom); 
                }

            }
            else
            {
                userType = "0";
                AddForm(showFrom);              
            }
            ChangeColor(showFrom);
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            panel1.Focus();
        }
    }
}
