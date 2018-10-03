using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemDamageCount.Class
{
    class Item
    {

        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string drugcode;//药品代码

        public string Drugcode
        {
            get { return drugcode; }
            set { drugcode = value; }
        }
        private string drugname;//药品名称

        public string Drugname
        {
            get { return drugname; }
            set { drugname = value; }
        }
        private string spec;    //规格

        public string Spec
        {
            get { return spec; }
            set { spec = value; }
        }
        private int count;      //数量

        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        private string money;    //金额

        public string Money
        {
            get { return money; }
            set { money = value; }
        }
        private string reason;   //报损原因

        public string Reason
        {
            get { return reason; }
            set { reason = value; }
        }
        private string responsibilityid;//责任人ID

        public string Responsibilityid
        {
            get { return responsibilityid; }
            set { responsibilityid = value; }
        }
        private string responsibilityer;//责任人

        public string Responsibilityer
        {
            get { return responsibilityer; }
            set { responsibilityer = value; }
        }
        private string reportid;    //举报人ID

        public string Reportid
        {
            get { return reportid; }
            set { reportid = value; }
        }
        private string reporter;    //举报人

        public string Reporter
        {
            get { return reporter; }
            set { reporter = value; }
        }
        private string date;        //插入时间

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        private string damagetime;  //损坏时间

        public string Damagetime
        {
            get { return damagetime; }
            set { damagetime = value; }
        }
   
    }
}
