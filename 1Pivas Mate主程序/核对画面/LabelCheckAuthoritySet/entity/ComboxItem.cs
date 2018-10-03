using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabelCheckAuthoritySet.entity
{
    public class ComboxItem
    {
        private string _id = "";
        private string _Name = "";

        /// <summary>
        /// 有参构造方法
        /// </summary>
        /// <param name="displaymember"></param>
        /// <param name="valuemember"></param>
        public ComboxItem(string displaymember, string valuemember)
        {
             _Name = displaymember;
             _id = valuemember;
        }

        public string ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }
    }
}
