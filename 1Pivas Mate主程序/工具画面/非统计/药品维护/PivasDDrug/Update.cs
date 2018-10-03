using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PivasDDrug
{
    public class Update
    {

        public string DDurg(int IsValid, string DrugCode)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" update DDrug set");
            str.Append(" IsValid="+IsValid);
            str.Append(" where DrugCode='" + DrugCode + "'");
            return str.ToString();
        }

        public string DDurg(object[] ins, string DrugCode)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" update DDrug set");
            str.Append(" Spellcode='" + ins[0] + "',");
            str.Append(" Spec='" + ins[1] + "',");
            if ((decimal)ins[2] != 0)
            {
                str.Append(" Dosage=" + ins[2] + ",");
            }
            str.Append(" DosageUnit='" + ins[3] + "',");

         
            str.Append(" Capacity=" + ins[4] + ",");
            str.Append(" CapacityUnit='" + ins[5] + "',");
  
            if (ins[6] != null)
            { str.Append(" UniPreparationID='" + ins[6] + "',"); }
            else
            { str.Append(" UniPreparationID= null,"); }

 
            str.Append(" FormUnit='" + ins[7] + "',");
            if ((Int16)ins[8] != 0)
            {
                str.Append(" Conversion=" + ins[8] + ",");
            }
            str.Append(" BigUnit='" + ins[9] + "',");
            str.Append(" PortNo='" + ins[10] + "',");
            str.Append(" Withmenstruum='" + ins[11] + "',");
            str.Append(" PreConfigure='" + ins[12] + "',");
            str.Append(" PiShi='" + ins[13] + "',");
            str.Append(" NotCompound='" + ins[14] + "',");
            str.Append(" Precious='" + ins[15] + "',");
            str.Append(" AsMajorDrug='" + ins[16] + "',");
            str.Append(" DrugNameJC='" + ins[20] + "',");
            if ((decimal)ins[17] != 0)
            {
                str.Append(" Major=" + ins[17] + ",");

            }
            str.Append(" MajorUnit='" + ins[18] + "',");
            str.Append(" Difficulty=" + ins[19]+",");
            str.Append(string.Format(" Species='{0}',", ins[21]));
            str.Append(string.Format(" Positions1='{0}',", ins[22]));
            str.Append(string.Format(" Positions2='{0}',", ins[23]));
            str.Append(string.Format(" IsMenstruum='{0}',", ins[25]));
            str.Append(string.Format(" NoName='{0}',", ins[24]));
            str.Append(string.Format(" ProductName='{0}',",ins[26]));
            str.Append(string.Format(" Concentration='{0}',", ins[27]));
            str.Append(string.Format(" DifficultySF='{0}'", ins[28]));
            str.Append(string.Format(" ,TheDrugType='{0}'", ins[29]));

            if (str.ToString().EndsWith(","))
            {
                str.Remove(str.Length - 1, 1);
            }
            str.Append("   where DrugCode='" + DrugCode + "'");
         // MessageBox.Show(str.ToString());
            return str.ToString();
            
        }

        public string DDrugPlusCondition(string name, string id)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" update DDrugPlusCondition set");
            str.Append(" DrugPlusConditionName='" + name + "'");
            str.Append(" where DrugPlusConditionID=" + id);
            return str.ToString();
        }
        /// <summary>
        /// 耗材更新
        /// </summary>
        /// <param name="ConsumablesCode"></param>
        /// <param name="DrugCode"></param>
        /// <returns></returns>
        public string DrugConsumables(string ConsumablesCode, string DrugCode)
        {
            StringBuilder str = new StringBuilder();
            str.Append("if exists(select *from DrugConsumables where DrugCode='");
            str.Append(DrugCode + "')");
            str.Append(" update DrugConsumables set ConsumablesCode='"+ConsumablesCode);
            str.Append("' where DrugCode='"+DrugCode +"' ");
            str.Append("else  insert into DrugConsumables(ConsumablesCode,DrugCode) values('");
            str.Append(ConsumablesCode);
            str.Append("','");
            str.Append(DrugCode);
            str.Append("')  ");
            return str.ToString();
        }
        public string DrugConsumables(string DrugCode)
        {
            StringBuilder str = new StringBuilder();
            str.Append("if exists(select *from DrugConsumables where DrugCode='");
            str.Append(DrugCode + "')");
            str.Append("delete from DrugConsumables where DrugCode='");
            str.Append(DrugCode);
            str.Append("'");
            return str.ToString();
        }
    }
}
