using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivasLabelCheckAll.entity
{
    /// <summary>
    /// 瓶签实体类
    /// </summary>
    public class IVRecord
    {
        /// <summary>
        /// 频次
        /// </summary>
        private string batch;

        public string Batch
        {
            get { return batch; }
            set { batch = value; }
        }

        /// <summary>
        /// 瓶签当前的状态
        /// </summary>
        private string ivStatus;

        public string IvStatus
        {
            get { return ivStatus; }
            set { ivStatus = value; }
        }

        private string labelOver;

        public string LabelOver
        {
            get { return labelOver; }
            set { labelOver = value; }
        }

        private string wardRetreat;

        public string WardRetreat
        {
            get { return wardRetreat; }
            set { wardRetreat = value; }
        }
    }
}
