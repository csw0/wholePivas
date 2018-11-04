using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AilongHisInterface.Message
{
    /// <summary>
    /// 一行药品信息实体
    /// </summary>
    public class MsgDrugRowInfo
    {
        /// <summary>
        /// 药品序号
        /// </summary>
        public string DrugIndex;

        /// <summary>
        /// 药品名称
        /// </summary>
        public string DrugName;

        /// <summary>
        /// 药品规格
        /// </summary>
        public string DrugSpec;

        /// <summary>
        /// 药品剂量
        /// </summary>
        public string DrugDose;

        /// <summary>
        /// 药品数量
        /// </summary>
        public string DrugCount;
    }
}
