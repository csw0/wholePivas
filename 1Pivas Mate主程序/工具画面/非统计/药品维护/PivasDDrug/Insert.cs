using System;
using System.Collections.Generic;
using System.Text;

namespace PivasDDrug
{
    public class Insert
    {
        public string MedDrugPlusCondition(string DrugCode, string[] PlusConditionID)
        {
            StringBuilder str = new StringBuilder();
            str.Append("  DELETE FROM MedDrugPlusCondition WHERE DrugID='" + DrugCode + "' ");
            str.Append(" INSERT INTO MedDrugPlusCondition(DrugID, PlusConditionID) ");
            int lent = str.Length;
            for (int i = 0; i < PlusConditionID.Length; i++)
            {
                if (PlusConditionID[i] != "")
                {
                    str.Append(" select '" + DrugCode + "', '" + PlusConditionID[i] + "'");
                }
                if (str.Length > lent)
                {
                    str.Append("union all");
                }
                lent = str.Length;
            }
            if (str.ToString().EndsWith("union all"))
            {
                str.Remove(str.Length - 9, 9);
            }
            if (str.ToString().IndexOf("select") > 0)
            {
                return str.ToString();
            }
            else
            {
                return "";
            }
            
        }

        public string PlusCondition(string DPlusConditionName)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" INSERT INTO DDrugPlusCondition(DrugPlusConditionName)");
            str.Append(" VALUES('" + DPlusConditionName + "')");
            return str.ToString();
        }

    }
}
