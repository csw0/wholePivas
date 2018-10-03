using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PivasWebServiceInterface
{
    /// <summary>
    /// 同步HIS的基础信息
    /// </summary>
    public class SynBaseInfo
    {
        /// <summary>
        /// 同步剂量单位
        /// </summary>
        /// <returns></returns>
        public string getDosage()
        {
            DBClass class2 = new DBClass();
            DataSet ds = new DataSet();
            string sql = "SELECT distinct UNIT_CODE MetricCode ,UNIT_NAME  MetricName ,null UnitID  FROM   v_pivas_dosunit";
            ds = class2.getOraDB(sql);
            string str2 = "delete from DMetricTemp ";
            class2.SetDB(str2);
            XmlToDataSet set2 = new XmlToDataSet();
            WriteLog log = new WriteLog();
            string content = set2.getxml(ds);
            log.writeLog("Dosage_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), content);
            return content;
        }

        /// <summary>
        /// 同步药品
        /// </summary>
        /// <returns></returns>
        public string getDrug()
        {
            DBClass class2 = new DBClass();
            DataSet ds = new DataSet();
            string sql = "  SELECT distinct ccode  as DrugCode,cname  as DrugName, cname  as DrugNameJC,spec   as Spec, dos    as Dosage, dosu   as DosageUnit, null   Major,null   MajorUnit, dos    AS Capacity,  null   Conversion,capu as CapacityUnit,null   as Form,units  as FormUnit,null   as Conversion, spell  as SpellCode, 0      as UniPreparationID,0      IsMenstruum, 0      withmenstruum, 0      PreConfigure, pishi  as PiShi, 0      NotCompound,  0      Precious, null   AsMajorDrug,  null   BigUnit, portno PortNo,null   Symbol, null   IsValid, null   Difficulty, null   DifficultySF, null   Species, null   Positions1,null   Positions2,null   NoName,spec1   ProductName,null   Concentration, FIRM_ID   firm_id    FROM v_pivas_drugdict";
            ds = class2.getOraDB(sql);
            string str2 = "delete from DDrugTemp ";
            class2.SetDB(str2);
            XmlToDataSet set2 = new XmlToDataSet();
            WriteLog log = new WriteLog();
            string content = set2.getxml(ds);
            log.writeLog("Drug_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), content);
            return content;
        }

        /// <summary>
        /// 同步员工
        /// </summary>
        /// <returns></returns>
        public string getEmployee()
        {
            DBClass class2 = new DBClass();
            DataSet ds = new DataSet();
            string sql = " SELECT distinct  eno AccountID    ,null  Pas ,null  Position    ,eno DEmployeeCode  , ename DEmployeeName  ,1 IsValid  ,0 Type    FROM v_pivas_employee";
            ds = class2.getOraDB(sql);
            string str2 = "truncate table DEmployeeTemp ";
            class2.SetDB(str2);
            XmlToDataSet set2 = new XmlToDataSet();
            WriteLog log = new WriteLog();
            string content = set2.getxml(ds);
            log.writeLog("Employee_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), content);
            return content;
        }

        /// <summary>
        /// 同步频次
        /// </summary>
        /// <returns></returns>
        public string getFreq()
        {
            DBClass class2 = new DBClass();
            DataSet ds = new DataSet();
            string sql = " SELECT DISTINCT    REPLACE(fname, '''', '’') as  freqcode,      REPLACE(fname, '''', '’')  as freqname,     1 as timesofday ,     1 as intervalday , null as  UseTime FROM v_pivas_freq";
            ds = class2.getOraDB(sql);
            string str2 = "delete from DFreqTemp";
            class2.SetDB(str2);
            XmlToDataSet set2 = new XmlToDataSet();
            WriteLog log = new WriteLog();
            string content = set2.getxml(ds);
            log.writeLog("Freq_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), content);
            return content;
        }

        /// <summary>
        /// 同步患者
        /// </summary>
        /// <returns></returns>
        public string getPatient()
        {
            DBClass class2 = new DBClass();
            DataSet ds = new DataSet();
            string sql = "SELECT distinct  pcode PatCode   ,pname PatName,    CASE Sex WHEN '?D' THEN '1' ELSE '2'    END Sex                  ,     birthday  Birthday                  ,'0'Height                    ,'0'Weight  ,ward WardCode                    ,bed  BedNo                     ,1 PatStatus       ,trunc(months_between  (sysdate,birthday)/12) Age        ,null AgeSTR  ,0 UPdateStatu    FROM v_pivas_actpatient";
            ds = class2.getOraDB(sql);
            string str2 = "delete from PatientTemp ";
            class2.SetDB(str2);
            XmlToDataSet set2 = new XmlToDataSet();
            WriteLog log = new WriteLog();
            string content = set2.getxml(ds);
            log.writeLog("Patient_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), content);
            return content;
        }
        /// <summary>
        /// 同步医嘱
        /// </summary>
        /// <returns></returns>
        public string getPrecption()
        {
            DBClass class2 = new DBClass();
            DataSet ds = new DataSet();
            string sql = " select   distinct   hospital_no||visit_id||'Y'||parent_no||'Z'||act_order_no as RecipeNo,  hospital_no||visit_id||parent_no as GroupNo,   ward_code WardCode,  Doctor DoctorCode,  '' DrawerCode,        inpatient_no PatientCode,      hospital_no CaseID,      Bed_no BedNo,        UsageName UsageCode,      UsageName UsageName,     freq_code FreqCode,    charge_code DrugCode, drugname DrugName,       specification Spec,      dose Dosage,      dose_unit DosageUnit,      0 Quantity,     null QuantityUnit,     sysdate InceptDT,     start_time StartDT,   end_time EndDT,      TPN,     2 PDStatus,      0 PPause,     null Remark1,    null Remark2,'未记帐' as Remark3,     null Remark4,      null Remark5,   null Remark6,    remark Remark7,   null Remark8,    null Remark9,   null Remark10,   0 OldGroup,   0 OldRecipe,   0 OldStop  from v_pivas_yz";
            ds = class2.getOraDB(sql);
            string str2 = "delete from PrescriptionTemp";
            class2.SetDB(str2);
            XmlToDataSet set2 = new XmlToDataSet();
            WriteLog log = new WriteLog();
            string content = set2.getxml(ds);
            log.writeLog("Precption_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), content);
            return content;
        }
        /// <summary>
        /// 同步病区
        /// </summary>
        /// <returns></returns>
        public string getWard()
        {
            DBClass class2 = new DBClass();
            DataSet ds = new DataSet();
            XmlToDataSet set2 = new XmlToDataSet();
            string sql = " select   wcode as WardCode,   wname as WardName,    null as WardSimName,    null as WardSeqNo,null as WardArea,  null as IsOpen,  null as Spellcode  from v_pivas_ward";
            ds = class2.getOraDB(sql);
            string str2 = "delete from DWardTemp ";
            class2.SetDB(str2);
            WriteLog log = new WriteLog();
            string content = set2.getxml(ds);
            log.writeLog("Ward_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), content);
            return content;
        }

    }
}