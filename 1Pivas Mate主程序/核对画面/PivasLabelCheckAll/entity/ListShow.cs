using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivasLabelCheckAll.entity
{
    class ListShow
    {
        private string _LabelNo;
        private string _IsCheck;

        public string LabelNo
        {
            get { return _LabelNo; }
            set { _LabelNo = value; }
        }

        public string IsCheck
        {
            get { return _IsCheck; }
            set { _IsCheck = value; }
        }
    }
}
