using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using LabelCheckAuthoritySet.entity;

namespace LabelCheckAuthoritySet.common
{
    /// <summary>
    /// 公共方法类
    /// </summary>
    public class CommonUtil
    {
        #region 属性
        private XmlDocument xmlDoc = new XmlDocument();
        private string path = "ConfigFile\\LabelAuthorityXml.xml";
        private List<IVstatus> statusList = null;
        #endregion

        /// <summary>
        /// 设置显示的配置页面
        /// </summary>
        /// <param name="IVstatus">当前页面下瓶签修改状态</param>
        /// <returns></returns>
        public string LabelSetName(string IVstatus)
        {
            string name = "";
            xmlDoc.Load(path);
            XmlElement root = xmlDoc.DocumentElement;//获取根节点
            XmlNodeList nodes = root.GetElementsByTagName("Operation");

            foreach (XmlNode node in nodes)
            {
                string value = ((XmlElement)node).GetAttribute("value");
                if (value == IVstatus)
                {
                    XmlNodeList subNameNodes = ((XmlElement)node).GetElementsByTagName("name");  //获取age子XmlElement集合
                    if (subNameNodes.Count == 1)
                    {
                        name = subNameNodes[0].InnerText; 
                    }
                }
            }
            return name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<IVstatus> GetIVStatusList()
        {
            statusList = new List<IVstatus>();
            xmlDoc.Load(path);
            XmlElement root = xmlDoc.DocumentElement;//获取根节点
            XmlNodeList nodes = root.GetElementsByTagName("Operation");
            IVstatus status = null;
            foreach (XmlNode node in nodes)
            {
                status = new IVstatus();
                status.IvStatusCode = ((XmlElement)node).GetAttribute("value");
                XmlNodeList subNameNodes = ((XmlElement)node).GetElementsByTagName("name");
                if (subNameNodes.Count == 1)
                {
                    status.IvStatusName = subNameNodes[0].InnerText;
                }
                statusList.Add(status);
            }
            return statusList;
        }
    }
}
