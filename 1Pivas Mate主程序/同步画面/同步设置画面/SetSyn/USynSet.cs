using PIVAsCommon.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SetSyn
{
    public partial class USynSet : UserControl
    {
        private string SynCode;
        private DB_Help db = new DB_Help();
        private UpdateSQL update = new UpdateSQL();
        private SelectSQL select = new SelectSQL();
        private DataSet ds = new DataSet();
        public USynSet(string syncode)
        {
            SynCode = syncode;
            InitializeComponent();
            Select(SynCode);
            uChange1.us = this;
        }

        private List<string> decry(DataSet ds, string Type)
        {
            List<string> ls = new List<string>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ls.Add(db.Decrypt(dr[Type].ToString()));
                }
            }
            return ls;
        }

        private void Select(string Code)
        {
            DataRow dr = db.GetPIVAsDB(select.Get_SynSet(Code)).Tables[0].Rows[0];
            Label_TypeText.Text = string.IsNullOrEmpty(dr["SynMode"].ToString()) ? "SQLSERVER" : dr["SynMode"].ToString();
            Txt_DataSource.Text = string.IsNullOrEmpty(dr["DataSource"].ToString()) ? "数据库地址" : db.Decrypt(dr["DataSource"].ToString());
            Txt_InitialCatalog.Text = string.IsNullOrEmpty(dr["InitialCatalog"].ToString()) ? "数据库名" : db.Decrypt(dr["InitialCatalog"].ToString());
            Txt_UserID.Text = string.IsNullOrEmpty(dr["UserID"].ToString()) ? "用户名" : db.Decrypt(dr["UserID"].ToString());
            Txt_Password.Text = string.IsNullOrEmpty(dr["Password"].ToString()) ? "密码" : db.Decrypt(dr["Password"].ToString());
            textBox1.Text = dr["Sql"].ToString().Trim();
        }

        public bool Update(string syncode)
        {
            string[] DataSource = new string[5];
            DataSource[0] = Label_TypeText.Text;
            DataSource[1] = db.Encrypt(Txt_DataSource.Text.Replace("数据库地址", ""));
            if (!Equals(Label_TypeText.Text, "Oracle"))
            {
                DataSource[2] = db.Encrypt(Txt_InitialCatalog.Text.Replace("数据库名", ""));
            }
            DataSource[3] = db.Encrypt(Txt_UserID.Text.Replace("用户名", ""));
            DataSource[4] = db.Encrypt(Txt_Password.Text.Replace("密码", ""));
            //MessageBox.Show(update.Get_SynSet(SynCode, DataSource, textBox1.Text));
            if (db.SetPIVAsDB(update.Get_SynSet(SynCode, DataSource, textBox1.Text)) > 0)
            {
                return true;
            }
            else
            {
                string ss = "添加或修改设置失败\r\nSQL为\r\n\r\n" + update.Get_SynSet(SynCode, DataSource, textBox1.Text);
                MessageBox.Show(ss, "失败");
                return false;
            }
        }

        private void Pic_TypeChange_Click(object sender, EventArgs e)
        {
            uChange1.Visible = !uChange1.Visible;
        }
        private void Pic_CopySQL_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(textBox1.Text);
            MessageBox.Show("复制成功");
        }
        private void ChangeColor(Control Txt)
        {
            Thread th = new Thread(() =>
            {
                while (Txt.Focused)
                {
                    continue;
                }
                if (string.IsNullOrEmpty(Txt.Text.Trim()))
                {
                    switch (Txt.Name)
                    {
                        case "Txt_DataSource":
                            {
                                Txt_DataSource.Text = "数据库地址";
                                Txt_DataSource.SelectAll();
                                break;
                            }
                        case "Txt_InitialCatalog":
                            {
                                Txt_InitialCatalog.Text = "数据库名";
                                Txt_InitialCatalog.SelectAll();
                                break;
                            }
                        case "Txt_UserID":
                            {
                                Txt_UserID.Text = "用户名";
                                Txt_UserID.SelectAll();
                                break;
                            }
                        case "Txt_Password":
                            {
                                Txt_Password.Text = "密码";
                                Txt_Password.SelectAll();
                                break;
                            }
                    }
                    Txt.ForeColor = Color.Silver;
                }
                else
                {
                    if (Equals(Txt.Text.Trim(), "数据库地址") || Equals(Txt.Text.Trim(), "数据库名") || Equals(Txt.Text.Trim(), "用户名") || Equals(Txt.Text.Trim(), "密码"))
                        Txt.ForeColor = Color.Silver;
                    else
                        Txt.ForeColor = Color.Black;
                }
            });
            th.IsBackground = true;
            th.Start();
        }
        private void Txt_DataSource_TextChanged(object sender, EventArgs e)
        {
            ChangeColor(Txt_DataSource);
        }

        private void Txt_InitialCatalog_TextChanged(object sender, EventArgs e)
        {
            ChangeColor(Txt_InitialCatalog);
        }

        private void Txt_UserID_TextChanged(object sender, EventArgs e)
        {
            ChangeColor(Txt_UserID);
        }

        private void Txt_Password_TextChanged(object sender, EventArgs e)
        {
            ChangeColor(Txt_Password);
        }

        private void Txt_DataSource_Click(object sender, EventArgs e)
        {
            Txt_DataSource.SelectAll();
        }

        private void Txt_InitialCatalog_Click(object sender, EventArgs e)
        {
            Txt_InitialCatalog.SelectAll();
        }

        private void Txt_UserID_Click(object sender, EventArgs e)
        {
            Txt_UserID.SelectAll();
        }

        private void Txt_Password_Click(object sender, EventArgs e)
        {
            Txt_Password.SelectAll();
        }
        private void Txt_DataSource_DropDown(object sender, EventArgs e)
        {
            Txt_DataSource.Items.Clear();
            ds = db.GetPIVAsDB(select.Get_SynSets("DataSource"));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Txt_DataSource.Items.AddRange(decry(ds, "DataSource").ToArray());
            }
        }

        private void Txt_InitialCatalog_DropDown(object sender, EventArgs e)
        {
            Txt_InitialCatalog.Items.Clear();
            ds = db.GetPIVAsDB(string.Format(select.Get_SynSets("InitialCatalog"), db.Encrypt(Txt_DataSource.Text)));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Txt_InitialCatalog.Items.AddRange(decry(ds, "InitialCatalog").ToArray());
            }
        }

        private void Txt_UserID_DropDown(object sender, EventArgs e)
        {
            Txt_UserID.Items.Clear();
            ds = db.GetPIVAsDB(string.Format(select.Get_SynSets("UserID"), db.Encrypt(Txt_DataSource.Text), db.Encrypt(Txt_InitialCatalog.Text)));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                Txt_UserID.Items.AddRange(decry(ds, "UserID").ToArray());
            }
        }

        private void Label_Test_Click(object sender, EventArgs e)
        {
            if (Label_TypeText.Text.ToUpper() == "SQLSERVER")
            {
                SqlConnectionStringBuilder buf = new SqlConnectionStringBuilder();
                buf.DataSource = Txt_DataSource.Text;
                buf.InitialCatalog = Txt_InitialCatalog.Text;
                buf.UserID = Txt_UserID.Text;
                buf.Password = Txt_Password.Text;
                Thread th = new Thread(() =>
                {
                    try
                    {
                        using (SqlConnection sql = new SqlConnection(buf.ConnectionString))
                        {
                            using (SqlCommand sqlcom = new SqlCommand(textBox1.Text, sql))
                            {
                                sqlcom.CommandTimeout = 5;
                                sqlcom.Connection.Open();
                                try
                                {
                                    using (DataSet ds = new DataSet())
                                    {
                                        using (SqlDataAdapter da = new SqlDataAdapter(sqlcom))
                                        {
                                            da.Fill(ds);
                                            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                                            {
                                                MessageBox.Show("执行sql返回的结果集为空");
                                            }
                                            else
                                            {
                                                MessageBox.Show("测试成功");
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("执行sql语句错误\n\n" + ex.Message);
                                }
                                sqlcom.Connection.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("连接数据库错误或超时\n\n" + ex.Message);
                    }
                });
                th.IsBackground = true;
                th.Start();
            }
            else if (Label_TypeText.Text.Trim().ToUpper() == "ORACLE")
            {
                OracleConnectionStringBuilder ocsb = new OracleConnectionStringBuilder();
                ocsb.DataSource = Txt_DataSource.Text;
                ocsb.UserID = Txt_UserID.Text;
                ocsb.Password = Txt_Password.Text;
                Thread th = new Thread(() =>
                {
                    try
                    {
                        using (OracleConnection oc = new OracleConnection(ocsb.ConnectionString))
                        {
                            using (OracleCommand ocd = new OracleCommand(textBox1.Text, oc))
                            {
                                ocd.Connection.Open();
                                try
                                {
                                    using (DataSet ds = new DataSet())
                                    {
                                        using (OracleDataAdapter oraDap = new OracleDataAdapter(ocd))
                                        {
                                            oraDap.Fill(ds);
                                            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                                            {
                                                MessageBox.Show("执行sql返回的结果集为空");
                                            }
                                            else
                                            {
                                                MessageBox.Show("测试成功");
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("执行sql语句错误\n\n" + ex.Message);
                                }
                                ocd.Connection.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("连接数据库错误或超时\n\n" + ex.Message);
                    }
                });
                th.IsBackground = true;
                th.Start();
            }
        }
    }
}
