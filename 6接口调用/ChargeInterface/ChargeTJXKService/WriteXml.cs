using System.Data;
using System.Xml.Linq;

namespace ChargeInterface.ChargeTJXKService
{
    public class WriteXml
    {
        public static string set(DataSet ds, DataSet ds2)
        {
            DataTable recipeDT = ds.Tables[0];
            string provider = ds.Tables[1].Rows[0]["DEmployeeCode"].ToString();
            string num = ds.Tables[2].Rows[0]["NUM"].ToString();

            XElement body = new XElement("body");
            XElement billings = new XElement("billings");
            XElement billing = new XElement("billing");
            XElement dispensary_info = new XElement("dispensary_info", 
                new XAttribute("code", recipeDT.Rows[0]["dispensary_info"].ToString()), 
                new XAttribute("provider", recipeDT.Rows[0]["provider"].ToString()), 
                new XAttribute("stock_flag", recipeDT.Rows[0]["stock_flag"].ToString()), 
                new XAttribute("type", recipeDT.Rows[0]["type"].ToString()));

            XElement patient_info = new XElement("patient_info", new XAttribute("id", recipeDT.Rows[0]["patient_id"].ToString()),
                new XAttribute("visit_id", recipeDT.Rows[0]["visit_id"].ToString()), 
                new XAttribute("baby_no", recipeDT.Rows[0]["baby_no"].ToString()));

            XElement prescriptions = new XElement("prescriptions");
            for (int i = 0; i < recipeDT.Rows.Count; i++)
            {
                DataRow dr = recipeDT.Rows[i];
                #region 编辑XML信息
                //医嘱信息
                XElement prescription = new XElement("prescription", 
                    new XAttribute("preform_datetime", dr["preform_datetime"].ToString()),
                    new XAttribute("group_no", dr["group_no"].ToString()), 
                    new XAttribute("bar_code", dr["bar_code"].ToString()), 
                    new XAttribute("add_nurse", dr["add_nurse"].ToString()));

                //医嘱药品详情
                XElement detail = new XElement("detail", new XAttribute("order_no", dr["order_no"].ToString()),
                    new XAttribute("order_sub_no", dr["order_sub_no"].ToString()));
                XElement item_class = new XElement("item_class", dr["item_class"].ToString());
                XElement drug_code = new XElement("drug_code", dr["drug_code"].ToString());
                XElement drug_spec = new XElement("drug_spec", dr["drug_spec"].ToString());
                XElement firm_id = new XElement("firm_id", dr["firm_id"].ToString());
                XElement drug_units = new XElement("drug_units", dr["drug_units"].ToString());
                XElement dispense_amount = new XElement("dispense_amount", dr["dispense_amount"].ToString());
                XElement sina_dose = new XElement("sina_dose", dr["sina_dose"].ToString());
                detail.Add(item_class);
                detail.Add(drug_code);
                detail.Add(drug_spec);
                detail.Add(firm_id);
                detail.Add(drug_units);
                detail.Add(dispense_amount);
                detail.Add(sina_dose);
                #endregion
                //第一次直接添加节点
                if (0 == i)
                {
                    prescription.Add(detail);
                    prescriptions.Add(prescription);

                }
                //第二次开始需判断头节点是否相同，相同直接添加detail否则重新添加一个prescription
                else
                {
                    int flag = 0;//是否重复标识
                    foreach (var item in prescriptions.Elements("prescription"))
                    {
                        if (item.Attribute("preform_datetime").Value == prescription.Attribute("preform_datetime").Value && item.Attribute("group_no").Value == prescription.Attribute("group_no").Value && item.Attribute("bar_code").Value == prescription.Attribute("bar_code").Value && item.Attribute("add_nurse").Value == prescription.Attribute("add_nurse").Value)
                        {
                            item.Add(detail);
                            flag = 1;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    //不重复
                    if (0 == flag)
                    {
                        prescription.Add(detail);
                        prescriptions.Add(prescription);
                    }
                }
            }
            billing.Add(dispensary_info);
            billing.Add(patient_info);
            billing.Add(prescriptions);
            //
            body.Add(billings);
            DataTable recipeDT2 = ds2.Tables[0];
            string provider2 = ds2.Tables[1].Rows[0]["DEmployeeCode"].ToString();
            string num2 = ds2.Tables[2].Rows[0]["NUM"].ToString();
            //
            XElement billing2 = new XElement("billing");
            XElement dispensary_info2 = new XElement("dispensary_info", 
                new XAttribute("code", recipeDT2.Rows[0]["dispensary_info"].ToString()), 
                new XAttribute("provider", recipeDT2.Rows[0]["provider"].ToString()),
                new XAttribute("stock_flag", recipeDT2.Rows[0]["stock_flag"].ToString()), 
                new XAttribute("type", recipeDT2.Rows[0]["type"].ToString()));
            XElement patient_info2 = new XElement("patient_info", 
                new XAttribute("id", recipeDT2.Rows[0]["patient_id"].ToString()), 
                new XAttribute("visit_id", recipeDT.Rows[0]["visit_id"].ToString()), 
                new XAttribute("baby_no", recipeDT.Rows[0]["baby_no"].ToString()));
            XElement prescriptions2 = new XElement("prescriptions");
            for (int i = 0; i < recipeDT2.Rows.Count; i++)
            {
                DataRow dr2 = recipeDT2.Rows[i];
                #region 编辑XML信息
                //医嘱信息
                XElement prescription2 = new XElement("prescription", new XAttribute("preform_datetime", dr2["preform_datetime"].ToString()),
                    new XAttribute("group_no", dr2["group_no"].ToString()), new XAttribute("bar_code", dr2["bar_code"].ToString()), 
                    new XAttribute("add_nurse", dr2["add_nurse"].ToString()));

                //医嘱药品详情
                XElement detail2 = new XElement("detail", new XAttribute("order_no", dr2["order_no"].ToString()), 
                    new XAttribute("order_sub_no", dr2["order_sub_no"].ToString()));
                XElement item_class2 = new XElement("item_class", dr2["item_class"].ToString());
                XElement drug_code2 = new XElement("drug_code", dr2["drug_code"].ToString());
                XElement drug_spec2 = new XElement("drug_spec", dr2["drug_spec"].ToString());
                XElement firm_id2 = new XElement("firm_id", dr2["firm_id"].ToString());
                XElement drug_units2 = new XElement("drug_units", dr2["drug_units"].ToString());
                XElement dispense_amount2 = new XElement("dispense_amount", dr2["dispense_amount"].ToString());
                XElement sina_dose2 = new XElement("sina_dose", dr2["sina_dose"].ToString());
                detail2.Add(item_class2);
                detail2.Add(drug_code2);
                detail2.Add(drug_spec2);
                detail2.Add(firm_id2);
                detail2.Add(drug_units2);
                detail2.Add(dispense_amount2);
                detail2.Add(sina_dose2);
                #endregion
                //第一次直接添加节点
                if (0 == i)
                {
                    prescription2.Add(detail2);
                    prescriptions2.Add(prescription2);
                }
                //第二次开始需判断头节点是否相同，相同直接添加detail否则重新添加一个prescription
                else
                {
                    int flag2 = 0;//是否重复标识
                    foreach (var item2 in prescriptions2.Elements("prescription"))
                    {
                        if (item2.Attribute("preform_datetime").Value == prescription2.Attribute("preform_datetime").Value 
                            && item2.Attribute("group_no").Value == prescription2.Attribute("group_no").Value 
                            && item2.Attribute("bar_code").Value == prescription2.Attribute("bar_code").Value 
                            && item2.Attribute("add_nurse").Value == prescription2.Attribute("add_nurse").Value)
                        {
                            item2.Add(detail2);
                            flag2 = 1;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    //不重复
                    if (0 == flag2)
                    {
                        prescription2.Add(detail2);
                        prescriptions2.Add(prescription2);
                    }
                }
            }
            billing2.Add(dispensary_info2);
            billing2.Add(patient_info2);
            billing2.Add(prescriptions2);
            //
            billings.Add(billing);
            billings.Add(billing2);
            //
            return body.ToString();
        }

        public static string SetXmlStr(DataSet ds)
        {
            DataTable recipeDT = ds.Tables[0];
            string provider = ds.Tables[1].Rows[0]["DEmployeeCode"].ToString();
            string num = ds.Tables[2].Rows[0]["NUM"].ToString();

            XElement body = new XElement("body");
            XElement billings = new XElement("billings");
            XElement billing = new XElement("billing");
            XElement dispensary_info = new XElement("dispensary_info", new XAttribute("code", recipeDT.Rows[0]["dispensary_info"].ToString()), 
                new XAttribute("provider", recipeDT.Rows[0]["provider"].ToString()), 
                new XAttribute("stock_flag", recipeDT.Rows[0]["stock_flag"].ToString()), 
                new XAttribute("type", recipeDT.Rows[0]["type"].ToString()));

            XElement patient_info = new XElement("patient_info", new XAttribute("id", recipeDT.Rows[0]["patient_id"].ToString()), 
                new XAttribute("visit_id", recipeDT.Rows[0]["visit_id"].ToString()), 
                new XAttribute("baby_no", recipeDT.Rows[0]["baby_no"].ToString()));

            XElement prescriptions = new XElement("prescriptions");
            for (int i = 0; i < recipeDT.Rows.Count; i++)
            {
                DataRow dr = recipeDT.Rows[i];
                #region 编辑XML信息
                //医嘱信息
                XElement prescription = new XElement("prescription", new XAttribute("preform_datetime", dr["preform_datetime"].ToString()), 
                    new XAttribute("group_no", dr["group_no"].ToString()), new XAttribute("bar_code", dr["bar_code"].ToString()),
                    new XAttribute("add_nurse", dr["add_nurse"].ToString()));

                //医嘱药品详情
                XElement detail = new XElement("detail", new XAttribute("order_no", dr["order_no"].ToString()),
                    new XAttribute("order_sub_no", dr["order_sub_no"].ToString()));
                XElement item_class = new XElement("item_class", dr["item_class"].ToString());
                XElement drug_code = new XElement("drug_code", dr["drug_code"].ToString());
                XElement drug_spec = new XElement("drug_spec", dr["drug_spec"].ToString());
                XElement firm_id = new XElement("firm_id", dr["firm_id"].ToString());
                XElement drug_units = new XElement("drug_units", dr["drug_units"].ToString());
                XElement dispense_amount = new XElement("dispense_amount", dr["dispense_amount"].ToString());
                XElement sina_dose = new XElement("sina_dose", dr["sina_dose"].ToString());
                detail.Add(item_class);
                detail.Add(drug_code);
                detail.Add(drug_spec);
                detail.Add(firm_id);
                detail.Add(drug_units);
                detail.Add(dispense_amount);
                detail.Add(sina_dose);
                #endregion
                //第一次直接添加节点
                if (0 == i)
                {
                    prescription.Add(detail);
                    prescriptions.Add(prescription);
                }
                //第二次开始需判断头节点是否相同，相同直接添加detail否则重新添加一个prescription
                else
                {
                    int flag = 0;//是否重复标识
                    foreach (var item in prescriptions.Elements("prescription"))
                    {
                        if (item.Attribute("preform_datetime").Value == prescription.Attribute("preform_datetime").Value 
                            && item.Attribute("group_no").Value == prescription.Attribute("group_no").Value 
                            && item.Attribute("bar_code").Value == prescription.Attribute("bar_code").Value 
                            && item.Attribute("add_nurse").Value == prescription.Attribute("add_nurse").Value)
                        {
                            item.Add(detail);
                            flag = 1;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    //不重复
                    if (0 == flag)
                    {
                        prescription.Add(detail);
                        prescriptions.Add(prescription);
                    }
                }
            }

            billing.Add(dispensary_info);
            billing.Add(patient_info);
            billing.Add(prescriptions);
            //
            billings.Add(billing);
            //
            body.Add(billings);
            //
            return body.ToString();
        }
    }
}
