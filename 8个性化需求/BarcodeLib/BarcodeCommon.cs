using System;
using System.Collections.Generic;
using System.Text;

namespace BarcodeLib
{
    abstract class BarcodeCommon
    {
        protected string Raw_Data = "";
        protected string Formatted_Data = "";
        
        public string RawData
        {
            get { return this.Raw_Data; }
        }

        public string FormattedData
        {
            get { return this.Formatted_Data; }
            set { this.Formatted_Data = value; }
        }
    }//BarcodeVariables abstract class
}//namespace
