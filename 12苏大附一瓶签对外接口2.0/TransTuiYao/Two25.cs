using System;
using System.Data;
using PIVAsDBhelp;
using System.Xml;
using System.IO;
using TransTuiYao.ServiceReference1;

namespace TransTuiYao
{
    class Two25
    {
        DB_Help DB = new DB_Help();
        ServiceReference1.HisServiceClient wls = new HisServiceClient();

        public void twoTwoFive_stop()
        {
            try
            {
                string str = " select distinct top 100 labelno,teamNumber from IVRecord(nolock) where labelover < 0 and DATEDIFF(dd,GETDATE(),InfusionDT) >= -1 and LabelNo not in (select LabelNo from IVRecordToTY) ";
                //string str = " select distinct top 100 labelno from IVRecord where labelover < 0 and (labelno like '20171010%' or labelno like '20171011%') and LabelNo not in (select LabelNo from IVRecordToTY) ";
                //string str = " select labelno from IVRecord where labelover < 0 and LabelNo in ('20171010102387') and LabelNo not in (select LabelNo from IVRecordToTY) ";
                DataSet ds = DB.GetPIVAsDB(str);
                if (ds.Tables.Count > 0 && !(ds == null) && ds.Tables[0].Rows.Count > 0)
                {
                    string sendstr = makestr(ds);
                    //调应webservice
                    string s = wls.HisTransData(sendstr);
                    DataSet dsxml = new DataSet();
                    StringReader read = new StringReader(s);
                    XmlTextReader readxml = new XmlTextReader(read);
                    dsxml.ReadXml(readxml);
                    DataTable dt1 = dsxml.Tables[0];
                    int i = int.Parse(dt1.Rows[0]["RETCODE"].ToString());
                    if (i == 1)
                    {
                        updateDB(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                //暂未处理，输出异常到log 
            }

        }

        //225 批次修改

        public void twoTwoFive_batchChanged()
        {
            try
            {
                string str = " select iv.LabelNo labelno,iv.TeamNumber teamNumber ,(case when Batch like '%#%' then 'J' else 'K' end) batchMark , batch from IVRecord(nolock) iv where DATEDIFF(dd,GETDATE(),InfusionDT) >= -1 and  exists (select 1 from TransBatch(nolock) tb  where tb.LabelNo = iv.LabelNo and tb.Batch <> iv.Batch) ";
                DataSet ds = DB.GetPIVAsDB(str);
                if (ds.Tables.Count > 0 && !(ds == null) && ds.Tables[0].Rows.Count > 0)
                {
                    string sendstr = makestr_225_false(ds);
                    //调应webservice
                    string s = wls.HisTransData(sendstr);
                    DataSet dsxml = new DataSet();
                    StringReader read = new StringReader(s);
                    XmlTextReader readxml = new XmlTextReader(read);
                    dsxml.ReadXml(readxml);
                    DataTable dt1 = dsxml.Tables[0];
                    int i = int.Parse(dt1.Rows[0]["RETCODE"].ToString());
                    if (i == 1)
                    {
                        updateDB_PC(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                //暂未处理，输出异常到log
            }
        }

        private string makestr(DataSet ds)
        {
            string wlhc = "";
            wlhc += "<ROOT>\n";
            wlhc += "<OPSYSTEM>HIS</OPSYSTEM>\n";
            wlhc += "<OPWINID></OPWINID>\n";
            wlhc += "<OPTYPE>225</OPTYPE>\n";
            wlhc += "<OPIP>192.168.0.101</OPIP>\n";
            wlhc += "<OPMANNO>112</OPMANNO>\n";
            wlhc += "<OPMANNAME>测试员</OPMANNAME>\n";
            foreach (DataRow r in ds.Tables[0].Rows)
            {

                wlhc += "<CONSIS_ORDER_MSTVW>\n";
                wlhc += "<ADVICE_CODE>";
                wlhc += r["labelno"].ToString().Trim();
                wlhc += "</ADVICE_CODE>\n";
                wlhc += "<ADVICE_BATCH>";
                wlhc += r["teamNumber"].ToString().Trim();
                wlhc += "</ADVICE_BATCH>\n";
                wlhc += "<ALLOWIND>N</ALLOWIND>\n";
                wlhc += "<STATUS>N</STATUS>\n";
                wlhc += "</CONSIS_ORDER_MSTVW>\n";
            }
            wlhc += "</ROOT>";
            return wlhc;
        }


        private string makestr_225_false(DataSet ds)
        {
            string wlhc = "";
            wlhc += "<ROOT>\n";
            wlhc += "<OPSYSTEM>HIS</OPSYSTEM>\n";
            wlhc += "<OPWINID></OPWINID>\n";
            wlhc += "<OPTYPE>225</OPTYPE>\n";
            wlhc += "<OPIP>192.168.0.101</OPIP>\n";
            wlhc += "<OPMANNO>112</OPMANNO>\n";
            wlhc += "<OPMANNAME>测试员</OPMANNAME>\n";
            foreach (DataRow r in ds.Tables[0].Rows)
            {

                wlhc += "<CONSIS_ORDER_MSTVW>\n";
                wlhc += "<ADVICE_CODE>";
                wlhc += r["labelno"].ToString().Trim();
                wlhc += "</ADVICE_CODE>\n";
                wlhc += "<ADVICE_BATCH>";
                wlhc += r["teamNumber"].ToString().Trim();
                wlhc += "</ADVICE_BATCH>\n";
                wlhc += "<ALLOWIND>" + r["batchMark"].ToString().Trim() + "</ALLOWIND>\n";
                wlhc += "<STATUS>N</STATUS>\n";
                wlhc += "</CONSIS_ORDER_MSTVW>\n";
            }
            wlhc += "</ROOT>";
            return wlhc;
        }

        private void updateDB(DataSet ds)
        {
            try
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    string ustr = " insert into IVRecordToTY(LabelNo,TYDT) values('" + r["labelno"].ToString().Trim() + "', GETDATE()) ";
                    DB.SetPIVAsDB(ustr);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void updateDB_PC(DataSet ds)
        {
            try
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    string ustr = " update TransBatch set Batch = '" + r["batch"].ToString().Trim() + "' where labelno = '" + r["labelno"].ToString().Trim() + "'";
                    DB.SetPIVAsDB(ustr);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
