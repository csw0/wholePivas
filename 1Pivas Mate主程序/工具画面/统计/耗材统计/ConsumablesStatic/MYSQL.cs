using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsumablesStatic
{
    class MYSQL
    {
        public string GetPZ()
        {
            StringBuilder str = new StringBuilder();
            str.Append("select RuleId, cbr.ConsumablesCode,c.ConsumablesName+'('+c.Spec+')' ConsumablesName , ");
            str.Append("(case cbr.DrugType when 1 then '普通药'  when 2 then '抗生素' when 3 then '化疗药' when 4 then '营养药' end)drugtype ");
            str.Append(",cbr.ConsumablesQuantity+cbr.QuantityUnit cbrq from ConsumablesRule cbr ");
            str.Append("left join Consumables c on c.ConsumablesCode=cbr.ConsumablesCode ");
            str.Append("order by cbr.DrugType ");
            return str.ToString();
        }
        public string GetSpecialPZ()
        {
            StringBuilder str = new StringBuilder();
            str.Append("select RuleId,ConsumablesName+'('+c.Spec+')' ConsumablesName,DrugName+'('+d.Spec+')' DrugName,SpecQuantity,SpecQuantityUnit   from ConsumablesRule_Spec crs ");
            str.Append("left join Consumables c on c.ConsumablesCode=crs.ConsumablesCode ");
            str.Append("left join DDrug d on d.DrugCode=crs.DrugCode ");
            str.Append(" order by RuleId ");
            str.Append(" ");
            str.Append(" ");
            str.Append(" ");
            str.Append(" ");
            str.Append(" ");
            return str.ToString();
        }
    }
}
