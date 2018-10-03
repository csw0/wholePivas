using ScreenService.WCFS;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.Services;

namespace ScreenService
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summarygrgtb ..
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        protected static string connstr = ConfigurationManager.ConnectionStrings["SqlConnStr"].ConnectionString;
        private static int TimeSet = Convert.ToInt32(ConfigurationManager.AppSettings["TimeSet"].ToString());
        private static int SaveDay = Convert.ToInt32(ConfigurationManager.AppSettings["SaveDay"].ToString());
        private string MediaPath = ConfigurationManager.AppSettings["MediaPath"].ToString();
        DataSet PatientDS = new DataSet(); //存放临时瓶签信息
        DataSet DrugPZInfor = new DataSet();//存放配置信息

        [WebMethod(Description = "登陆")]
        public string Login(string IP, string port)
        {
            string a = string.Empty;
            if (TestDB())
            {
                if (IP == "")
                {
                    a = "<login empname=\"显示屏IP不能为空!\" empocc=\"\" position=\"\" />";
                }
                else
                {
                    string sql = " select DemployeeID from ScreenDetail where IP=@ip ";
                    DataSet ds1 = ExecuteDataTable(sql, new SqlParameter("@ip", IP));
                    if (ds1 == null || ds1.Tables[0].Rows.Count <= 0)
                    {
                        a = "";
                    }
                    else if (ds1.Tables[0].Rows.Count > 0)
                    {
                        string sql1 = " select * from ScreenDetail where IP=@ip ";
                        DataSet ds2 = ExecuteDataTable(sql1, new SqlParameter("@ip", IP));
                        a = "<login empname=\"" + ds2.Tables[0].Rows[0]["DemployeeName"].ToString() + "\" empocc=\"药师\" position=\""
                            + ds2.Tables[0].Rows[0]["DeskNo"].ToString() + "\" />";
                    }
                }
            }
            else
            {
                a = "<login empname=\"连接失败\" empocc=\"\" position=\"\" />";
            }
            return a;
        }

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <returns></returns>
        private Boolean TestDB()
        {
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter("select  top 1 AccountID  from DEmployee", connstr);
                DataSet DS = new DataSet();
                sda.Fill(DS);

                if (DS != null)
                {
                    return true;
                }
                else
                { return false; }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //20150825100112
        [WebMethod(Description = "瓶签信息")]
        public string LabelInfor(string IP, string port)
        {
            //DateTime dt11 = DateTime.Now;    
            string label = string.Empty;
            try
            {
                Service1Client s = new Service1Client();
                string labelinfor = s.GetData1(IP); ;
                DataSet DS = new DataSet();
                DS.ReadXml(new StringReader(labelinfor), XmlReadMode.Auto);
                if (DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                {
                    label += ConvertPatientToXML(DS.Tables[0]);
                    label += ConvertDrugToXML(DS.Tables[0]);
                    label += ConvertOtherToXML(DS.Tables[0]);
                    label += "<confecter>" + DS.Tables[0].Rows[0]["DemployeeName"].ToString() + "</confecter>";
                    label += "</label>";
                }
                else
                {
                    label = "<label/>";
                }
                return label;
            }
            catch (Exception ex)
            {
                return "<label/>";
            }

        }
        [WebMethod(Description = "测试能否得到数据")]
        public string Test(string ip)
        {
            Service1Client s = new Service1Client();
            return s.GetDrugTest();
        }

        [WebMethod(Description = "药品图片上传接口")]
        public string DrugpictruePost(string time)
        {
            string drugpic = string.Empty;
            string sql = " select DrugCode,DrugPicture  from DDrug where DATEDIFF(S,PicInsertDT,@time)>0 and DrugPicture is not null ";

            DataSet ds1 = ExecuteDataTable(sql, new SqlParameter("@time", time));
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds1.Tables[0].Rows[i];
                byte[] content = (byte[])dr["DrugPicture"];
                drugpic += "<drugpic code=\"" + dr["DrugCode"] + "\">";
                drugpic += Convert.ToBase64String(content) + "</drugpic>";

            }
            return drugpic;

        }

        [WebMethod(Description = "药品视频文件名下载接口")]
        public string[] FileNameDown(string a)
        {
            string filepath = MediaPath;
            if (Directory.Exists(filepath))
            {
                try
                {
                    string[] files = Directory.GetFiles(filepath);
                    return files;
                    //return string.Join("_",files);
                }
                catch (Exception ex)
                {
                    return new string[0];
                    //return "";
                }
            }
            else
            {
                return new string[0];
            }
        }

        [WebMethod(Description = "药品视频下载接口")]
        public byte[] MediaDown(string filename)
        {
            string filepath = filename;
            if (File.Exists(filepath))
            {
                try
                {
                    FileStream s = File.OpenRead(filepath);
                    return ConvertStreamToByteBuffer(s);
                }
                catch (Exception ex)
                {
                    return new byte[0];
                }
            }
            else
            {
                return new byte[0];
            }
        }
        [WebMethod(Description = "药品视频16进制下载接口")]
        public string Media16Down(string filename)
        {
            string filepath = filename;
            if (File.Exists(filepath))
            {
                try
                {
                    FileStream fs = File.OpenRead(filepath);
                    byte[] bytes = ConvertStreamToByteBuffer(fs);
                    string hexString = string.Empty;

                    if (bytes != null)
                    {
                        StringBuilder strB = new StringBuilder();
                        for (int i = 0; i < bytes.Length; i++)
                        {
                            strB.Append(bytes[i].ToString("X2"));
                        }

                        hexString = strB.ToString();

                    }
                    return hexString;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            else
            {
                return "";
            }

        }

        private byte[] ConvertStreamToByteBuffer(Stream s)
        {
            MemoryStream ms = new MemoryStream();
            int b;
            while ((b = s.ReadByte()) != -1)
            {
                ms.WriteByte((byte)b);
            }
            return ms.ToArray();
        }

        [WebMethod(Description = "大文件传输,start为开始位置, length不得大于2,147,483,647，为每次的获取长度")]
        public byte[] BigFileDown(string parh, long start, int length)
        {
            FileStream stream = new FileInfo(parh).OpenRead();
            byte[] buffer = new byte[length];
            if (start > stream.Length)
                return new byte[0];
            if (start + length > stream.Length)
            {
                stream.Position = start;
                //stream.Seek(start,SeekOrigin.Begin);
                stream.Read(buffer, 0, (int)(stream.Length - start));
            }
            else
            {
                stream.Position = start;
                stream.Read(buffer, 0, length);
            }
            return buffer;
        }
        /// <summary>
        /// 拼接病人信息
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private string ConvertPatientToXML(DataTable dt)
        {
            string xml = string.Empty;
            if (dt.Rows.Count > 0)
            {
                xml += "<label deptname=\"" + dt.Rows[0]["WardName"].ToString().Trim() + "\" ";
                xml += "bedno=\"" + dt.Rows[0]["BedNo"].ToString() + "\" ";
                xml += "patientname=\"" + dt.Rows[0]["PatName"].ToString().Trim() + "\" ";
                xml += "age=\"" + dt.Rows[0]["Age"].ToString().Trim() + "\" ";
                xml += "labelno=\"" + dt.Rows[0]["LabelNo"].ToString() + "\" > ";
            }
            else
            {
                xml += "<label deptname=\"\" ";
                xml += "bedno=\"\" ";
                xml += "patientname=\"\" ";
                xml += "age=\"\" ";
                xml += "labelno=\"\" >";

            }
            return xml;
        }

        /// <summary>
        /// 拼接药品信息
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private string ConvertDrugToXML(DataTable dt)
        {
            string xml = string.Empty;
            string drugcodes = string.Empty;
            xml += "<drugs>";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                xml += "<drug drugno=\"" + (i + 1).ToString().Trim() + "\" ";
                xml += "is_highrisk=\"" + "高危" + "\" ";
                xml += "drugcode=\"" + dt.Rows[i]["DrugCode"].ToString().Trim() + "\" ";
                xml += "drugname=\"" + dt.Rows[i]["DrugName"].ToString().Trim() + "\" ";
                xml += "drugspec=\"" + dt.Rows[i]["Spec"].ToString().Trim() + "\" ";
                xml += "dosage=\"" + dt.Rows[i]["Dosage"].ToString().Trim() + dt.Rows[i]["DosageUnit"].ToString() + "\" ";
                xml += "quantity=\"" + dt.Rows[i]["DgNo"].ToString() + "\"/> ";
                string upi = dt.Rows[i]["UniPreparationID"].ToString().Trim();
                if (!string.IsNullOrEmpty(upi))
                {
                    drugcodes += "'" + upi + "',";
                }
            }
            xml += "</drugs>";
            if (!string.IsNullOrEmpty(drugcodes))
            {
                Service1Client s = new Service1Client();
                string labelinfor = s.GetDrugData(drugcodes.TrimEnd(','));
                DataSet DS = new DataSet();
                DS.ReadXml(new StringReader(labelinfor), XmlReadMode.Auto);
                if (DS.Tables.Count > 0)
                    xml += ConvertPZToXML(DS.Tables[0]);
            }
            return xml;
        }

        /// <summary>
        /// 拼接配置信息
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private string ConvertPZToXML(DataTable dt)
        {
            string xml = string.Empty;

            xml += " <confects> <!-- 配置说明信息 -->";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                xml += " <confect>" + dt.Rows[i]["PreparationDesc"] + "</confect>";
            }
            xml += "</confects>";

            return xml;
        }

        /// <summary>
        /// 拼接扫描信息
        /// </summary>
        /// <returns></returns>
        private string ConvertOtherToXML(DataTable dt)
        {
            string xml = string.Empty;
            xml += "<text type=\"";
            if (dt.Rows[0]["Result"].ToString() == "1")
            {
                xml += "information\">";
            }
            else
            {
                xml += "error\">";
            }
            xml += dt.Rows[0]["Msg"] + "</text>";

            xml += "<confectnum>" + dt.Rows[0]["Count"] + "</confectnum>";
            return xml;
        }

        private DataSet GetDrugPic(string time)
        {
            string sql = " select DrugCode,DrugPicture  from DDrug where DATEDIFF(M,PicInsertDT,@time)>0 and DrugPicture is not null ";

            DataSet ds1 = ExecuteDataTable(sql, new SqlParameter("@time", time));
            return ds1;
        }

        private static DataSet ExecuteDataTable(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset);
                    conn.Close();
                    return dataset;
                }
            }
        }
        
        public DataTable XMLToDataTable(string strXMLPath)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.ReadXml(strXMLPath);
                return dt;
            }
            catch (Exception vErr)
            {
                return new DataTable();
            }
        }
    }
}