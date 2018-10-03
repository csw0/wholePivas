using PIVAsCommon.Helper;
using System;
using System.Windows.Forms;

namespace SetSyn
{
    public partial class DeleteTableNew : UserControl
    {
        public DeleteTableNew()
        {
            InitializeComponent();
        }

        DB_Help db = new DB_Help();
        string deletetable = string.Empty;
        private void Truncate_Click(object sender, EventArgs e)
        {
            //delete(false);
            for (int i = 1; i <= groupBox1.Controls.Count; i++)
            {
                try
                {
                    if ((groupBox1.Controls[("CB" + i)] as CheckBox).Checked)
                    {
                        db.SetPIVAsDB("delete from [" + groupBox1.Controls[("CB" + i)].Tag + "]");
                        //MessageBox.Show(groupBox1.Controls[("CB" + i)].Tag.ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            db.SetPIVAsDB("EXEC [dbo].[REBUILDINDEX]");
            MessageBox.Show("冗余数据删除成功！");
        }

        /// <summary>
        /// 清空数据库
        /// </summary>
        /// <param name="tal">是否进入二次清空，主要针对第一次清空不成功的数据表</param>
        public void delete(bool tal)
        {
            string str="delete from ";
            string str2 = "select 1 from ";
            if (!tal)
            {
                for (int i = 0; i < groupBox1.Controls.Count; i++)
                {
                    CheckBox check = (CheckBox)groupBox1.Controls[i];
                    if (check.Checked)
                    {
                        if (db.SetPIVAsDB(str + check.Name) <=0)
                        {
                            if (deletetable == string.Empty)
                            {
                                deletetable = check.Name;
                            }
                            else
                            {
                                deletetable += "," + check.Name;
                            }
                        }
                    }
                }
            }
            if (deletetable != string.Empty)
            {
                string[] deltable = deletetable.Split(',');
                deletetable = string.Empty;
                for (int j = 0; j < deltable.Length; j++)
                {
                    if (db.GetPIVAsDB(str2 + deltable[j]).Tables[0].Rows.Count > 0)
                    {
                        if (db.SetPIVAsDB(str + deltable[j]) <= 0)
                        {
                            if (deletetable == string.Empty)
                            {
                                deletetable = deltable[j];
                            }
                            else
                            {
                                deletetable += "," + deltable[j];
                            }
                        }
                    }
                }
                delete(true);
            }
            else
            {
                MessageBox.Show("冗余数据删除成功！");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void CB1_MouseClick(object sender, MouseEventArgs e)
        {
            if (sender is CheckBox)
            {
                bool all = true;
                bool unall = true;
                foreach (CheckBox check in groupBox1.Controls)
                {
                    if ((sender as CheckBox).Checked)
                    {
                        if (check.TabIndex < (sender as CheckBox).TabIndex)
                        {
                            check.Checked = true;
                        }
                    }
                    else
                    {
                        if (check.TabIndex > (sender as CheckBox).TabIndex)
                        {
                            check.Checked = false;
                        }
                    }
                    all = all && check.Checked;
                    unall = unall && !check.Checked;
                }
                checkBox1.CheckState = all ? CheckState.Checked : (unall ? CheckState.Unchecked : CheckState.Indeterminate);
            }
        }

        private void checkBox1_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (CheckBox check in groupBox1.Controls)
            {
                check.Checked = checkBox1.Checked;
            }
        }
    }
}
