using PIVAsCommon;
using System;
using UseWebServer.SR_CustomerHis;
using UseWebServer.SR_Transfer;

namespace UseWebServer
{
    /// <summary>
    /// VS中对服务的叫法，早期是Web服务，目前都是服务引用
    /// 在引用时，高级选项可以选择
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            InternalLogger.Log.Info("测试程序开启...");
            Console.WriteLine("输入任何键，开始测试操作");
            Console.ReadLine();

            #region 测试函数
            testTrans();
            #endregion
            Console.Write("输入任何键，结束测试操作");
            Console.ReadLine();
        }

        /// <summary>
        /// 服务的配置文件App.config需要，如果别人引用此工程或Dll，需要复制App.config的内容到其配置文件中
        /// 使用别人已发布的服务，会生成两个endpoint.分别是TranslatorWebServiceSoap12和TranslatorWebServiceSoap
        /// 原因是.Net支持两种绑定（soap和soap1.2），soap较为通用，可以删除soap1.2；使用
        /// </summary>
        /// <returns></returns>
        static void testHelloW()
        {
            HISServiceSoapClient client = new HISServiceSoapClient();
            string s=  client.HelloWorld();
            Console.WriteLine(s);
        }

        /// <summary>
        /// 中英翻译
        /// </summary>
        /// <returns></returns>
        static void testTrans()
        {
            //多个绑定时，可以删除配置文件中的多余绑定，也可以在使用参数指定
            TranslatorWebServiceSoapClient client = new TranslatorWebServiceSoapClient("TranslatorWebServiceSoap");
            //TranslatorWebServiceSoapClient client = new TranslatorWebServiceSoapClient();
            string[] rtn = client.getEnCnTwoWayTranslator("对不起");
            if (rtn!= null)
            {
                foreach (var item in rtn)
                {
                    Console.WriteLine(item);
                } 
            }
        }
    }
}
