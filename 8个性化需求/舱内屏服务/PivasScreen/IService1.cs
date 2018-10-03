using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PivasScreen
{
    // 注意: 如果更改此处的接口名称 "IService1"，也必须更新 App.config 中对 "IService1" 的引用。
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData1(string ip);

        [OperationContract]
        string GetDrugData(string uip);
        [OperationContract]
        string GetDrugTest();

    }
}
