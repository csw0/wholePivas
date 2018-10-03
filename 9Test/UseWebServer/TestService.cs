using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UseWebServer.SR_CustomerHis;

namespace UseWebServer
{
    /// <summary>
    /// VS中对服务的叫法，早期是Web服务，目前都是服务引用
    /// 在引用时，高级选项可以选择
    /// </summary>
    public class TestService
    {
        private string str = String.Empty;
        public string testHelloW()
        {
            HISServiceSoapClient client = new HISServiceSoapClient();
            str =client.HelloWorld();
            return str;
        }
    }
}
