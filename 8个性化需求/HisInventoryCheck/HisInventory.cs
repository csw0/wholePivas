using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HisInventoryCheck
{
    public class HisInventory
    {   ///drugCodes是由多个药品编号组成
        public DataTable GetHis(string drugCodes)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("drugcode");
            dt.Columns.Add("hiscount");
            //根据药品编号获得His的库存
            return dt;
        }

    }
}
