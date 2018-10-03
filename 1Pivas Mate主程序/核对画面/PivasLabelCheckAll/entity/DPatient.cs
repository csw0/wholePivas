using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivasLabelCheckAll.entity
{
    /// <summary>
    /// 病人实体类
    /// </summary>
    public  class DPatient
    {
        private string pCode;

        public string PCode
        {
            get { return pCode; }
            set { pCode = value; }
        }

        private string pWardOld;

        public string PWardOld
        {
            get { return pWardOld; }
            set { pWardOld = value; }
        }

        private string pWardNew;

        public string PWardNew
        {
            get { return pWardNew; }
            set { pWardNew = value; }
        }

        private string pBedOld;

        public string PBedOld
        {
            get { return pBedOld; }
            set { pBedOld = value; }
        }

        private string pBedNew;

        public string PBedNew
        {
            get { return pBedNew; }
            set { pBedNew = value; }
        }

        private string pWardNameOld;

        public string PWardNameOld
        {
            get { return pWardNameOld; }
            set { pWardNameOld = value; }
        }

        private string pWardNameNew;

        public string PWardNameNew
        {
            get { return pWardNameNew; }
            set { pWardNameNew = value; }
        }

        private string patientName;

        public string PatientName
        {
            get { return patientName; }
            set { patientName = value; }
        }


    }
}
