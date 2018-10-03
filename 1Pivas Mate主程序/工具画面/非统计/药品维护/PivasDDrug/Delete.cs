using System;
using System.Collections.Generic;
using System.Text;

namespace PivasDDrug
{
    public class Delete
    {
        public string DDrugPlusCondition(string id)
        {
            StringBuilder str = new StringBuilder();
            str.Append(" delete from DDrugPlusCondition");
            str.Append(" where DrugPlusConditionID=" + id );
            return str.ToString();
        }
    }
}
