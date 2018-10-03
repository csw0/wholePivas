using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSmartThreadPool
{
    public  class model
    {
        public model(int index , string content)
        {
            this.index = index;
            this.content = content;
        }
        private int index = -1;
        private string content = String.Empty;
        public string GetInfo()
        {
            return index.ToString() + ":" + content;
        }
    }
}
