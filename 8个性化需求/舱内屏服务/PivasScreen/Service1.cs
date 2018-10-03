using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using System.Data;

namespace PivasScreen
{
    // 注意: 如果更改此处的类名 "Service1"，也必须更新 App.config 中对 "Service1" 的引用。
    public class Service1 : IService1
    {
        public string GetData1(string ip)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.Namespace = "NewDS";
                DataTable dt = PivasScreen.PatientDS.Tables[0].Clone();
                dt.TableName = "NewDT";
                foreach (DataRow dr in PivasScreen.PatientDS.Tables[0].Select(string.Format("IP='{0}'", ip)))
                {
                    dt.Rows.Add(dr.ItemArray);
                }
                ds.Tables.Add(dt);
                StringWriter sw = new StringWriter();
                ds.WriteXml(sw, XmlWriteMode.WriteSchema);
                return sw.ToString();
            }
            catch (Exception ex)
            {
                string stmp = this.GetType().Assembly.Location;
                stmp = stmp.Substring(0, stmp.LastIndexOf('\\'));//删除文件名     
                System.IO.File.AppendAllText(stmp + "\\PivasScreen.txt", ex.ToString() + "    " + DateTime.Now.ToString() + "\r\n");
                return ex.Message;
            }
        }
        public string GetDrugData(string uip)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.Namespace = "NewDS";
                DataTable dt = PivasScreen.DrugPZInfor.Tables[0].Clone();
                dt.TableName = "NewDT";
                foreach (DataRow dr in PivasScreen.DrugPZInfor.Tables[0].Select(string.Format("UniPreparationID in ('0'," + uip + ")")))
                {
                    dt.Rows.Add(dr.ItemArray);
                }
                dt = dt.DefaultView.ToTable(true, "PreparationDesc"); ; 
                ds.Tables.Add(dt);
                StringWriter sw = new StringWriter();
                ds.WriteXml(sw, XmlWriteMode.IgnoreSchema);
                return sw.ToString();
            }
            catch (Exception ex)
            {
                string stmp = this.GetType().Assembly.Location;
                stmp = stmp.Substring(0, stmp.LastIndexOf('\\'));//删除文件名     
                System.IO.File.AppendAllText(stmp + "\\PivasScreen.txt", ex.ToString() + "    " + DateTime.Now.ToString() + "\r\n");
                return ex.Message;
            }
        }

        public string GetDrugTest()
        {
            string labelno="所有IP：";
            foreach (DataRow dr in PivasScreen.PatientDS.Tables[0].Rows)
            {
                labelno += dr["IP"].ToString();
            }
            return labelno;
        }
    }
}
