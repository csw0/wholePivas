using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace PivasDESforEveryHis
{
    internal sealed partial class DesForm : Form
    {
        private SqlDataAdapter ODA;
        private SqlConnectionStringBuilder osb;
        private DataSet DS;
        private DataSet DSMaping;

        internal DesForm()
        {
            InitializeComponent();
            try
            {
                osb = new SqlConnectionStringBuilder();
                osb.DataSource = ".";
                osb.InitialCatalog = "PIVAS2014";
                osb.UserID = "laennec";
                osb.Password = "13816350872";
                using (SqlCommand oc = new SqlCommand())
                {
                    oc.Connection = new SqlConnection(osb.ConnectionString);
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("if not exists (SELECT 1 FROM sys.all_objects where type='U' AND name='PivasDesTabColNameMaping') begin Create table [PivasDesTabColNameMaping](LimitName VarChar(512) PRIMARY key,MapingName VarChar(512)) end ");
                    sb.AppendLine("if not exists (SELECT 1 FROM sys.all_objects where type='U' AND name='PivasDesForEveryHis') begin Create table [PivasDesForEveryHis](医院服务器ID VarChar(512) PRIMARY key,医院名称 VarChar(512),加密密文 TEXT) end ");
                    oc.CommandText = sb.ToString();
                    oc.Connection.Open();
                    oc.ExecuteNonQuery();
                    oc.Connection.Close();
                }
                if (!Directory.Exists(".\\DataSave\\"))
                {
                    Directory.CreateDirectory(".\\DataSave\\");
                }
                comboBox1.Items.Add("数据库");
                foreach (string s in Directory.GetFileSystemEntries(".\\DataSave"))
                {
                    comboBox1.Items.Add(s.Replace(".\\DataSave\\", string.Empty));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void NewDT()
        {
            try
            {
                DS = new DataSet();
                ODA = new SqlDataAdapter("select * from [PivasDesForEveryHis]", new SqlConnection(osb.ConnectionString));
                ODA.Fill(DS);
                if (DS != null && DS.Tables.Count > 0)
                {
                    DS.Tables[0].PrimaryKey = new DataColumn[] { DS.Tables[0].Columns[0] };
                    foreach (DataColumn dc in DS.Tables[0].Columns)
                    {
                        if (dc.DataType == typeof(bool))
                        {
                            dc.DefaultValue = false;
                        }
                        else
                        {
                            dc.DefaultValue = string.Empty;
                        }
                        dc.AllowDBNull = false;
                    }
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = DS.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void DesForm_Load(object sender, EventArgs e)
        {
            try
            {
                NewDT();
                DSMaping = new DataSet();
                using (SqlDataAdapter oda = new SqlDataAdapter("select * from [PivasDesTabColNameMaping]", new SqlConnection(osb.ConnectionString)))
                {
                    oda.Fill(DSMaping);
                    if (DSMaping != null && DSMaping.Tables.Count > 0)
                    {
                        DSMaping.Tables[0].PrimaryKey = new DataColumn[] { DSMaping.Tables[0].Columns[0] };
                        foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                        {
                            if (dgvc.Index > 2)
                            {
                                dgvc.HeaderText = DSMaping.Tables[0].Rows.Contains(dgvc.Name) ? DSMaping.Tables[0].Rows.Find(dgvc.Name)[1].ToString() : dgvc.Name;
                            }
                        }
                    }
                }

                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[2].ReadOnly = true;
                dataGridView1.Columns[0].Width = 250;
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.Columns[2].Width = 300;
                comboBox1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (new SqlCommandBuilder(ODA).DataAdapter.Update(DS) > 0)
                {
                    using (StringWriter sw = new StringWriter())
                    {
                        DS.WriteXml(sw, XmlWriteMode.WriteSchema);
                        File.AppendAllText(".\\DataSave\\" + DateTime.Now.ToString("yyyy-MM-dd HH_mm_ss") + ".XML", sw.ToString());
                    }
                    MessageBox.Show("保存成功！！！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void 删除此医院ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.RowCount > 0 && !dataGridView1.CurrentRow.IsNewRow)
                {
                    dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string RunEsOrDs(string ValueString, string Key, bool EsOrDs)
        {
            string RET = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(ValueString) && !string.IsNullOrEmpty(Key))
                {
                    Key = Key + Key.Length;
                    string k = string.Empty;
                    using (System.Security.Cryptography.MD5 md = System.Security.Cryptography.MD5.Create())
                    {
                        k = BitConverter.ToString(md.ComputeHash(Encoding.UTF8.GetBytes(Key))).Replace("-", string.Empty);
                    }
                    byte[] inputByteArray = EsOrDs ? System.Text.Encoding.UTF8.GetBytes(ValueString) : System.Convert.FromBase64String(ValueString);
                    byte[] rgbKey = System.Text.Encoding.UTF8.GetBytes(k.Substring(0, 8));
                    byte[] rgbIV = System.Text.Encoding.UTF8.GetBytes(k.Substring(k.Length - 8, 8));
                    using (System.Security.Cryptography.DES DCSP = System.Security.Cryptography.DES.Create())
                    {
                        using (System.IO.MemoryStream mStream = new System.IO.MemoryStream())
                        {
                            using (System.Security.Cryptography.CryptoStream cStream = new System.Security.Cryptography.CryptoStream(mStream, EsOrDs ? DCSP.CreateEncryptor(rgbKey, rgbIV) : DCSP.CreateDecryptor(rgbKey, rgbIV), System.Security.Cryptography.CryptoStreamMode.Write))
                            {
                                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                                cStream.FlushFinalBlock();
                                RET = EsOrDs ? System.Convert.ToBase64String(mStream.ToArray()) : System.Text.Encoding.UTF8.GetString(mStream.ToArray());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return RET;
        }

        private void 添加医院ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (new ADDHIS(ref DS).ShowDialog() == DialogResult.OK)
                {
                    foreach (DataRow dr in DS.Tables[0].Rows)
                    {
                        if (string.IsNullOrWhiteSpace(dr[2].ToString()))
                        {
                            using (DataTable dts = DS.Tables[0].Clone())
                            {
                                dts.TableName = "PW";
                                dts.Rows.Add(dr.ItemArray);
                                using (StringWriter sw = new StringWriter())
                                {
                                    using (DataSet ds = new DataSet())
                                    {
                                        ds.Tables.Add(dts);
                                        ds.WriteXml(sw, XmlWriteMode.WriteSchema);
                                    }
                                    dr[2] = RunEsOrDs(sw.ToString(), dr[0].ToString().Trim(), true);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetDataObject(dataGridView1.CurrentRow.Cells[2].Value);
                MessageBox.Show("已复制到剪切板！！！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)System.Windows.Forms.Keys.Enter)
                {
                    using (DataTable dt = DS.Tables[0].Clone())
                    {
                        foreach (DataRow dr in DS.Tables[0].Select(string.Format("医院名称 like ('%{0}%')", textBox1.Text.Trim())))
                        {
                            dt.Rows.Add(dr.ItemArray);
                        }
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                DataRow dr = DS.Tables[0].Rows.Find(dataGridView1.CurrentRow.Cells[0].Value);
                dr[2] = string.Empty;
                using (DataTable dts = DS.Tables[0].Clone())
                {
                    dts.TableName = "PW";
                    dts.Rows.Add(dr.ItemArray);
                    using (StringWriter sw = new StringWriter())
                    {
                        using (DataSet ds = new DataSet())
                        {
                            ds.Tables.Add(dts);
                            ds.WriteXml(sw, XmlWriteMode.WriteSchema);
                        }
                        dr[2] = RunEsOrDs(sw.ToString(), dr[0].ToString().Trim(), true);
                    }
                }
            }
            else
            {
                foreach (DataRow dr in DS.Tables[0].Rows)
                {
                    dr[2] = string.Empty;
                    using (DataTable dts = DS.Tables[0].Clone())
                    {
                        dts.TableName = "PW";
                        dts.Rows.Add(dr.ItemArray);
                        using (StringWriter sw = new StringWriter())
                        {
                            using (DataSet ds = new DataSet())
                            {
                                ds.Tables.Add(dts);
                                ds.WriteXml(sw, XmlWriteMode.WriteSchema);
                            }
                            dr[2] = RunEsOrDs(sw.ToString(), dr[0].ToString().Trim(), true);
                        }
                    }
                }
            }
            MessageBox.Show("已重新生成！！！");
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem.ToString() == "数据库")
                {
                    dataGridView1.DataSource = DS.Tables[0];
                    button2.Enabled = true;
                    button3.Enabled = true;
                    contextMenuStrip1.Enabled = true;
                }
                else
                {
                    button2.Enabled = false;
                    button3.Enabled = false;
                    contextMenuStrip1.Enabled = false;
                    using (DataSet ds = new DataSet())
                    {
                        ds.ReadXml(".\\DataSave\\" + comboBox1.SelectedItem.ToString(), XmlReadMode.ReadSchema);
                        dataGridView1.DataSource = ds.Tables[0];
                    }
                }
                if (DSMaping != null && DSMaping.Tables.Count > 0)
                {
                    foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                    {
                        if (dgvc.Index > 2)
                        {
                            dgvc.HeaderText = DSMaping.Tables[0].Rows.Contains(dgvc.Name) ? DSMaping.Tables[0].Rows.Find(dgvc.Name)[1].ToString() : dgvc.Name;
                        }
                    }
                    dataGridView1.Columns[0].ReadOnly = true;
                    dataGridView1.Columns[1].ReadOnly = true;
                    dataGridView1.Columns[2].ReadOnly = true;
                    dataGridView1.Columns[0].Width = 250;
                    dataGridView1.Columns[1].Width = 150;
                    dataGridView1.Columns[2].Width = 300;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
