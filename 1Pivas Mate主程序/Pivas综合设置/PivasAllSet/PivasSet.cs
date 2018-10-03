using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Text;
using System.Drawing;
using PIVAsCommon.Helper;

namespace PivasAllSet
{
    public partial class PivasSet : Form
    {
        private bool Pass;
        private DB_Help db = new DB_Help();
        //private DataTable dt = new DataTable();
        private SqlDataAdapter myDataAdapter;
        private string Sql;
        public PivasSet()
        {
            InitializeComponent();
        }
        protected internal void setPass(bool ps)
        {
            this.Pass = ps;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommandBuilder cb = new SqlCommandBuilder(myDataAdapter))
                {
                    myDataAdapter.InsertCommand = cb.GetInsertCommand(true);
                    myDataAdapter.UpdateCommand = cb.GetUpdateCommand(true);
                    myDataAdapter.DeleteCommand = cb.GetDeleteCommand(true);
                    if (myDataAdapter.Update(dataGridView1.DataSource as DataTable) >= 0)
                    {
                        MessageBox.Show("更新成功");
                        ShowMain(string .Empty);
                    }
                    else
                    {
                        MessageBox.Show("更新失败");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PivasSet_Load(object sender, EventArgs e)
        {
            new PassWord(this).ShowDialog();
            if (Pass)
            {
                SetDate();
                ShowMain(string.Empty);
            }
            else
            {
                this.Dispose();
            }
        }

        /// <summary>
        /// 一些基础数据插入。
        /// </summary>
        private void SetDate()
        {
            try
            {
                db.GetPivasAllSet("排批次-#");
                db.GetPivasAllSet("排批次-L#");
                db.GetPivasAllSet("排批次-K");
                db.GetPivasAllSet("排批次-LK");
                db.GetPivasAllSet("SaveLabelDay");
                db.GetPivasAllSet("同步-药品目录-画面显示");
                db.GetPivasAllSet("同步-病区-画面显示");
                db.GetPivasAllSet("同步-员工-画面显示");
                db.GetPivasAllSet("同步-剂量单位-画面显示");
                db.GetPivasAllSet("同步-频次-画面显示");
                db.GetPivasAllSet("同步-患者-画面显示");
                db.GetPivasAllSet("同步-医嘱-画面显示");
                db.GetPivasAllSet("同步-药单-画面显示");
                db.GetPivasAllSet("同步-患者身高体重-画面显示");
                db.GetPivasAllSet("同步-临床诊断-画面显示");
                db.GetPivasAllSet("同步-统药单-画面显示");
                db.GetPivasAllSet("第三方瓶签");
            }
            catch
            {
                MessageBox.Show("程序综合设置，基础数据插入维护出错。");
            }
        }


        private void ShowMain(string str)
        {
            try
            {
                string WhereStr = string.Empty;
                if (str != string.Empty)
                { WhereStr = " WHERE Pro LIKE '%" + str.Trim() + "%'   "; }
                Sql = "SELECT ID,Pro,Value,Remark,[Caption],Value2,Value3 FROM [PivasAllSet] " + WhereStr + " order by OrdBy,Pro   ";
                // dt = db.GetPIVAsDB(Sql).Tables[0];
                myDataAdapter = new SqlDataAdapter(Sql, new SqlConnection(db.DatebasePIVAsInfo()));//where Pro!='PassWord'
                using (DataTable dt = new DataTable())
                {
                    myDataAdapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Invalidate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "____" + Sql);
            }
        }

        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //dt.Rows.Add(dt.NewRow());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.RowCount > 0)
                {
                    dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            db.SetPIVAsDB("  delete from PivasAllSet  ");
            ShowMain(string.Empty);
            MessageBox.Show("清空数据成功");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ShowMain(string.Empty);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "")
            {
                ShowMain(textBox1.Text.Trim());
            }
            else
            {
                ShowMain(string.Empty);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string color1 = "58,0,29";          //审方三级
            string color2="255,128,128";        //审方五级
            string color3 = "137,255,192";      //审方通过
            string color4 = "128,128,128";      //审方选中
            //string color5 = "255,255,255";      //批次颜色设置，为选择
            string color6 = "0,192,0";          //批次颜色设置，已选择
            string color7 = "255,255,128";      //批次有改动病人显示颜色
            string color8 = "255,255,192";      //病区列表显示（有改动病人颜色）
            string color9 = "128,255,255";      //病区列表显示（无改动病人颜色）
            string color10 = "0,255,0";         //选中行颜色
            string color11 = "255,255,192";     //药瓶信息颜色一
            string color12 = "128,255,255";     //药瓶信息颜色二
            Color color13 = new Color();        //未打印
            color13= System.Drawing.Color.FromArgb(181, 251, 191);
            Color color14 = new Color();        //已打印
            color14 = System.Drawing.Color.FromArgb(228, 225, 194);
            Color color15 = new Color();        //未打印选中
            color15 = System.Drawing.Color.FromArgb(64, 0, 64);
            Color color16 = new Color();        //已打印选中
            color16 = System.Drawing.Color.FromArgb(0, 255, 255);
            Color color17 = new Color();        //不可打印HasCheck
            color17 = System.Drawing.Color.FromArgb(181, 251, 191);
            Color color18 = new Color();        //不可打印选中 HasCheckSelected
            color18 = System.Drawing.Color.FromArgb(64, 0, 64);


            StringBuilder str = new StringBuilder();
            str.Append("  update RevPreFormSet set Level3Color ='" + color1 + "',Level5Color='" + color2 + "',RightColor='" + color3 + "',SelectedColor='" + color4 + "' ");
            str.Append("  update OrderFormSet set ChangeColor='"+color7+"',ViewColor1='"+color8+"',ViewColor2='"+color9+"',DrugColor1='"+color11+"',DrugColor2='"+color12+"',SelectionColor1='"+color10+"'");
            str.Append("  update PrintFormSet set NotPrint='" + GetCustomColor(color13) + "',NotPrintSelected='" + GetCustomColor(color14) + "',Printed='" + GetCustomColor(color15) + "',PrintSelected='" + GetCustomColor(color16) + "',HasCheck='" + GetCustomColor(color17) + "',HasCheckSelected='" + GetCustomColor(color18) + "'");
            str.Append("  update OrderColor set OrderColor ='" + color6 + "' ");

            //MessageBox.Show(str.ToString());
            db.SetPIVAsDB(str.ToString());
        }

        uint GetCustomColor(Color color)
        {

            int nColor = color.ToArgb();

            int blue = nColor & 255;

            int green = nColor >> 8 & 255;

            int red = nColor >> 16 & 255;

            return Convert.ToUInt32(blue << 16 | green << 8 | red);

        }
    }
}
