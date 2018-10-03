using System;
using System.Collections.Generic;
using System.Text;

namespace PivasDDrug
{
    public class Select
    {
        /// <summary>
        /// 查询药品目录
        /// </summary>
        /// <param name="IsValid"></param>
        /// <returns></returns>
        public string DDurg(int IsValid, string DName)
        {
            //3、药品名称 //4、药品简称;//5、规格//6、剂量//7、剂量单位  //9 速查码 //10 单位制剂 
            StringBuilder str = new StringBuilder();
            str.Length = 0;
            str.Append(" SELECT  IsValid as 可见 ,DD.DrugCode as 药品编码,DD.DrugName as 药品名称,DD.DrugNameJC as 药品简称,");
            str.Append(" DD.Spec as 规格,");
            str.Append(" case when cast(DD.Dosage as float) > cast(cast(DD.Dosage as float) as INT) then cast(DD.Dosage as float)");
            str.Append(" else cast(cast(DD.Dosage as float) as int) end as 剂量,dm1.MetricCode AS 剂量单位,");
            str.Append(" cast(case when cast(DD.Dosage as float) > cast(cast(DD.Dosage as float) as INT) then cast(DD.Dosage as float)");
            str.Append(" else cast(cast(DD.Dosage as float) as int) end as varchar)+");
            str.Append("(case when dm1.MetricName is null then '' else dm1.MetricName end) AS 主药剂量,");
            str.Append("  cast(case when cast(DD.Capacity as float) > cast(cast(DD.Capacity as float) as INT) then cast(DD.Capacity as float)");
            str.Append(" else cast(cast(DD.Capacity as float) as int) end as varchar) +");
            str.Append(" (case when dm2.MetricName is null then '' else dm2.MetricName end) AS 溶媒剂量,DD.SpellCode as 速查码,");
            //str.Append(" 1 as 单位制剂");
            str.Append("  PortNo as 货柜号 ,(case StandardSpec when 'N/A' then UniversalName else UniversalName + '｜' + StandardSpec end)  单位制剂");
            str.Append("  ,cs.ConsumablesName as 药品耗材 ");
            str.Append("  ,convert(varchar, DD.TheDrugType ) as 药品属性 ");
            //str.Append("  ,case DD.Drugtype when 4 than '营养药'when 2 than '抗生素' when 3 than '化疗药' else '普通药'end as 药品属性 ");
            //str.Append(" DD.ProductName");
            str.Append(" from DDrug DD ");
            str.Append(" left OUTER join dbo.DMetric dm1 on dm1.MetricCode collate chinese_prc_cs_as=dd.DosageUnit");
            str.Append(" left OUTER join dbo.DMetric dm2 on dm2.MetricCode collate chinese_prc_cs_as=dd.CapacityUnit");
            str.Append(" LEFT OUTER JOIN KD0100.dbo.UniPreparation up ON DD.UniPreparationID = up.UniPreparationID ");
            str.Append(" LEFT OUTER JOIN KD0100.dbo.UniversalName un ON up.UniversalID = un.UniversalID ");
            str.Append("  LEFT OUTER JOIN dbo.DrugConsumables dc on dc.DrugCode=dd.DrugCode ");
            str.Append("  LEFT OUTER JOIN dbo.Consumables cs on dc.ConsumablesCode=cs.ConsumablesCode  ");
            str.Append(" WHERE 1=1 ");
            if (IsValid == 1)
            {
                str.Append(" AND IsValid =" + IsValid);
            }
            if (DName != "")
            {
                str.Append(" AND ((DD.SpellCode LIKE '%" + DName + "%') OR (DD.DrugName LIKE '%" + DName + "%') or (DD.DrugNameJC LIKE '%" + DName + "%') or (DD.DrugCode LIKE '%" + DName + "%')) ");
            }

            str.Append(" ORDER BY DD.NoName");
            return str.ToString();
        }


        /// <summary>
        /// 药品维护中查询数据
        /// </summary>
        /// <param name="DrugCode"></param>
        /// <returns></returns>
        public string DDurg(string DrugCode)
        {
            StringBuilder str = new StringBuilder();
            str.Length = 0;
            str.Append(" SELECT  DD.DrugCode as 药品编码,DD.SpellCode as 拼音码,DD.DrugName as 药品名称,DD.DrugNameJC as 药品简称,");
            str.Append(" DD.Spec as 规格, case when cast(DD.Dosage as float) > cast(cast(DD.Dosage as float) as INT) then cast(DD.Dosage as float)");
            str.Append(" else cast(cast(DD.Dosage as float)as int)  end  as 剂量,DosageUnit AS 剂量单位,");
            str.Append(" case when cast(DD.Capacity as float) > cast(cast(DD.Capacity as float) as INT) then cast(DD.Capacity as float)");
            str.Append(" else cast(cast(DD.Capacity as float) as int) end as 溶媒剂量,CapacityUnit as 溶媒剂量单位,UniPreparationID as 单位剂量,");
            str.Append(" UniPreparationID as  单位制剂,");
            str.Append(" BigUnit as 大包装单位,FormUnit as 小包装单位,Conversion as 小包装数量,");
            str.Append(" PreConfigure as 预配药, PiShi as 皮试,NotCompound as 不冲配,Precious as 贵重药,AsMajorDrug as 不作主药,");
            str.Append(" WithMenstruum as 自带溶媒,IsMenstruum as 溶媒, PortNo as '货柜号',Difficulty as 配液难度系数,");
            str.Append(" NoName as 优先级,Positions1 as 仓位1,Positions2 as 仓位2,");
            str.Append(" Species as 分类,ProductName as 商品名称, ");
            str.Append(" Concentration as 浓度监测,DifficultySF as 配置难度系数 ,");
            str.Append(" DD.TheDrugType as 药品属性 ");
            str.Append(" from DDrug DD ");
            str.Append(" where DrugCode='" + DrugCode + "'");
            str.Append(" ORDER BY DrugName");
            return str.ToString();
        }


        /// <summary>
        /// 药品位
        /// </summary>
        /// <returns></returns>
        public string DMetric()
        {
            StringBuilder str = new StringBuilder(255);
            str.Length = 0;
            str.Append(" Select MetricName,MetricCode from DMetric");
            str.Append(" Select distinct Species from DDrug where Species!=''");
            str.AppendFormat(" select DeskNo as '进仓号' from  IVRecordDeskSet where DeskNo is not null and DeskNo!='' and isopen='1' ");
           // str.Append(" Select distinct  D.进仓号");
           // str.Append(" from (select distinct Positions1 进仓号 from DDrug union");
           // str.Append(" select distinct Positions2 进仓号 from DDrug) D where D.进仓号!=''");
            return str.ToString();
        }

        /// <summary>
        /// 附加条件字典表
        /// </summary>
        /// <returns></returns>
        public string DDrugPlusCondition(string DrugID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("select DrugPlusConditionID AS [ID], DrugPlusConditionName AS [Name] ,DrugID from DDrugPlusCondition left join MedDrugPlusCondition");
            str.Append(" on DrugPlusConditionID =MedDrugPlusCondition.PlusConditionID and MedDrugPlusCondition.DrugID='" + DrugID + "' order by DrugID desc,ID asc");
            //str.Append("SELECT DrugPlusConditionID AS [ID], DrugPlusConditionName AS [Name] FROM DDrugPlusCondition  ");
            if (DrugID.Trim() != "")
            {
                str.Append(" select PlusConditionID from MedDrugPlusCondition where DrugID='" + DrugID + "'");

            }
            return str.ToString();
        }


        /// <summary>
        /// 浓度限制
        /// </summary>
        /// <returns></returns>
        public string DrugTiter(string DrugID)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" SELECT [DrugCode],case when cast([MinValue] as float) > cast(cast([MinValue] as float) as INT) then cast([MinValue] as float)");
            str.Append(" else cast(cast([MinValue] as float) as int) end [MinValue],");
            str.Append(" case when cast([LessLevel] as float) > cast(cast([LessLevel] as float) as INT) then cast([LessLevel] as float)");
            str.Append(" else cast(cast([LessLevel] as float) as int) end [LessLevel],");
            str.Append(" case when cast([LargeLevel] as float) > cast(cast([LargeLevel] as float) as INT) then cast([LargeLevel] as float)");
            str.Append(" else cast(cast([LargeLevel] as float) as int) end [LargeLevel],");
            str.Append(" case when cast([MaxValue] as float) > cast(cast([MaxValue] as float) as INT) then cast([MaxValue] as float)");
            str.Append(" else cast(cast([MaxValue] as float) as int) end [MaxValue],");
            str.Append(" [LessResult],[LargeResult] FROM DrugTiter  ");
            str.Append(" where DrugCode='" + DrugID + "'");
            return str.ToString();
        }



        /// <summary>
        /// 单位制剂
        /// </summary>
        /// <returns></returns>
        public string UniPreparation()
        {
            StringBuilder str = new StringBuilder();
            str.Append(" select UniPreparationID as ID, UniversalName as 通用名,StandardSpec 规格,EnglishName 英文名, SpellCode, ");
            str.Append(" (case StandardSpec when 'N/A' then UniversalName else UniversalName + '｜' + StandardSpec end) NameAndSpec");
            str.Append(" from KD0100.dbo.UniPreparation UP LEFT OUTER JOIN KD0100.dbo.UniversalName UN ON");
            str.Append(" UP.UniversalID = UN.UniversalID ");
            str.Append(" where UniversalName is not null ");
            return str.ToString();
        }






        /// <summary>
        ///  单位制剂(单个名字查询)
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public string UniPreparation(string Name)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" select UniPreparationID as ID, UniversalName as 通用名,StandardSpec 规格,EnglishName 英文名, SpellCode, ");
            str.Append(" (case StandardSpec when 'N/A' then UniversalName else UniversalName + '｜' + StandardSpec end) NameAndSpec");
            str.Append(" from KD0100.dbo.UniPreparation UP LEFT OUTER JOIN KD0100.dbo.UniversalName UN ON");
            str.Append(" UP.UniversalID = UN.UniversalID ");
            str.Append(" where (UniversalName is not null and UniversalName like '%" + Name + "%' )");
            str.Append("  OR UniPreparationID = '" + Name + "' ");
            return str.ToString();
        }
        /// <summary>
        /// 耗材查询
        /// </summary>
        /// <returns></returns>
        public string Consumables()
        {
            StringBuilder str = new StringBuilder();
            str.Append(" select ConsumablesName as 耗材名称,ConsumablesCode as 耗材编码 from Consumables  ");
            str.Append(" ");
            str.Append("  ");          
            return str.ToString();
        }
        /// <summary>
        /// 单个耗材查询
        /// </summary>
        /// <param name="DrugCode"></param>
        /// <returns></returns>
        public string Consumables(string DrugCode)
        {
            StringBuilder str = new StringBuilder();
            str.Append("  select cs.ConsumablesName,cs.ConsumablesCode from DrugConsumables dc");
            str.Append(" left join Consumables cs on dc.ConsumablesCode=cs.ConsumablesCode ");
            str.Append(" where dc.DrugCode='");
            str.Append(DrugCode);
            str.Append("'  ");
            return str.ToString();
        }
    }
}
