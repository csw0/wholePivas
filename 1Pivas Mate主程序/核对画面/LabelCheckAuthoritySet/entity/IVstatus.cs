using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabelCheckAuthoritySet.entity
{
    /// <summary>
    /// 配置文件的权限名称和值的对象
    /// </summary>
    public class IVstatus
    {
        private string ivStatusCode;

        public string IvStatusCode
        {
            get { return ivStatusCode; }
            set { ivStatusCode = value; }
        }

        private string ivStatusName;

        public string IvStatusName
        {
            get { return ivStatusName; }
            set { ivStatusName = value; }
        }
    }
}
