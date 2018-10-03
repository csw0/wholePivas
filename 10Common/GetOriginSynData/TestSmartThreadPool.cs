using Amib.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetOriginSynData
{
    public class TestSmartThreadPool
    {
        SmartThreadPool smartThreadPool = new SmartThreadPool(180*1000,1000);
        public void test()
        {
            smartThreadPool.Start();
            for (int i = 0; i < 1000; i++)
            {
                smartThreadPool.QueueWorkItem(handler, "序号" + i.ToString()+ ";");
            }
            smartThreadPool.Shutdown();
        }

        void handler(string s)
        {
            Console.Write(s);
        }
    }
}
