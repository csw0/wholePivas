using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabelCheckAuthoritySet.entity
{
    /// <summary>
    /// 权限列表对应的实体类
    /// </summary>
    public class Authority
    {
        private int seqNo;

        public int SeqNo
        {
            get { return seqNo; }
            set { seqNo = value; }
        }

        private string authorityName;

        public string AuthorityName
        {
            get { return authorityName; }
            set { authorityName = value; }
        }

        private string authorityArea;

        public string AuthorityArea
        {
            get { return authorityArea; }
            set { authorityArea = value; }
        }

        private string aUthorityLevel;

        public string AUthorityLevel
        {
            get { return aUthorityLevel; }
            set { aUthorityLevel = value; }
        }
    }
}
