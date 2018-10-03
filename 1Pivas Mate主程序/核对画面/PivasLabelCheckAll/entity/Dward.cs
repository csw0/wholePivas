using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivasLabelCheckAll.entity
{
    public class Dward
    {
        private string wardCode;
        private string wardName;
        private string wardSimName;
        private string wardArea;
        private string wardSpellCode;

        public string WardSpellCode
        {
            get { return wardSpellCode; }
            set { wardSpellCode = value; }
        }

        public string WardCode
        {
            get { return wardCode; }
            set { wardCode = value; }
        }
        

        public string WardName
        {
            get { return wardName; }
            set { wardName = value; }
        }

        public string WardSimName
        {
            get { return wardSimName; }
            set { wardSimName = value; }
        }

        public string WardArea
        {
            get { return wardArea; }
            set { wardArea = value; }
        }
    }
}
