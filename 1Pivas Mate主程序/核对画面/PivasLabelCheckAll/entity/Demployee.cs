using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PivasLabelCheckAll.entity
{
    /// <summary>
    /// 员工实体类
    /// </summary>
    public class Demployee
    {
        private string dEmployeeID;//员工编号
        public string DEmployeeID
        {
            get { return dEmployeeID; }
            set { dEmployeeID = value; }
        }

        private string qRcode;//员工二维码
        public string QRcode
        {
            get { return qRcode; }
            set { qRcode = value; }
        }

        private string qRcodeDT;
        public string QRcodeDT
        {
            get { return qRcodeDT; }
            set { qRcodeDT = value; }
        }

        private string delDT;
        public string DelDT
        {
            get { return delDT; }
            set { delDT = value; }
        }

        private string accountID;
        public string AccountID
        {
            get { return accountID; }
            set { accountID = value; }
        }

        private string pas;
        public string Pas
        {
            get { return pas; }
            set { pas = value; }
        }

        private string position;
        public string Position
        {
            get { return position; }
            set { position = value; }
        }

        private string dEmployeeCode;
        public string DEmployeeCode
        {
            get { return dEmployeeCode; }
            set { dEmployeeCode = value; }
        }

        private string demployeeName;
        public string DemployeeName
        {
            get { return demployeeName; }
            set { demployeeName = value; }
        }

        private string isValid;
        public string IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }

        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

    }
}
