using AilongHisInterface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestAilong
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("按回车键，测试手牌扫描...");
            Console.ReadLine();
            TestLabel("7777635760891459623461");//手牌编号

            Console.WriteLine("按回车键，测试瓶签扫描...");
            Console.ReadLine();
            TestLabel("20180509103404");//这里使用今天和明天的瓶签，不然返回值为空

            Console.WriteLine("按回车键，结束测试关闭程序...");
            Console.ReadLine();
        }

        /// <summary>
        /// 测试瓶签
        /// </summary>
        static void TestLabel(string labelNo)
        {
            try
            {
                string result = string.Empty;
                bool success = AilongCharge.Instance.GetResultByLabelNo(labelNo, out result);

                //调用接口成功，且返回值非空
                if (success && result != string.Empty)
                {
                    //打印结果返回的结果,
                    //举例1：{"EmployeeName":"王玮","Status":1,"MsgType":1}
                    //举例2：{"WardName":"A东15区骨外科","PatientName":"梅耀华","LabelNo":"20180509103404","EmployeeName":"王玮","Drugs":[{"DrugIndex":"1","DrugName":"氯化钠注射液","DrugSpec":"0.9% 250毫升/软","DrugDose":"250","DrugCount":"1"},{"DrugIndex":"2","DrugName":"美索巴莫注射液","DrugSpec":"1克 10毫升/支","DrugDose":"1","DrugCount":"1"}],"ChargeMessage":"计费成功","ChargeResult":1,"MsgType":2}
                    Console.WriteLine("扫描瓶签" + labelNo + "的结果是:" + result);

                    //将字符串解析出有效信息（也可使用JToken代替JObject）
                    JObject obj = (JObject)JsonConvert.DeserializeObject(result);

                    //解析出结果类型，1=扫描的手牌，返回的是登录信息；2=扫描的是瓶签，返回的是计费结果
                    int msgType = Int32.Parse(obj["MsgType"].ToString());

                    //根据msgType的类型，做进一步处理
                    if (msgType == 1)//返回的是登录结果
                    {
                        string employeeName = obj["EmployeeName"].ToString();//解析出扫描人的名字
                        int status = Int32.Parse(obj["Status"].ToString());//解析出扫描人当前登录状态；//0未登录，1已登陆

                        if (status == 0) //0未登录
                            Console.WriteLine("药师" + employeeName + "退出登录，结束冲配作业");
                        else if(status == 1)
                            Console.WriteLine("药师" + employeeName + "登录成功，开始冲配作业");
                    }
                    else if (msgType == 2)//返回的是计费结果
                    {
                        string WardName = obj["WardName"].ToString();//解析出病区名称
                        string PatientName = obj["PatientName"].ToString();//解析出病人的名字
                        string LabelNo = obj["LabelNo"].ToString();//解析扫描的瓶签号
                        string EmployeeName = obj["EmployeeName"].ToString();//解析扫描人的名字

                        string ChargeMessage = obj["ChargeMessage"].ToString();//解析计费结果的文字描述；例如，计费成功、计费失败、计费出错等
                        int ChargeResult = Int32.Parse(obj["ChargeResult"].ToString());//0=计费失败，1=计费成功

                        if (ChargeResult == 1) //计费成功
                        {
                            Console.WriteLine(LabelNo + "计费成功");
                            
                            //计费成功后，若想要药品信息，可以按照如下解析出来
                            JArray drugs = JArray.Parse(obj["Drugs"].ToString());//解析出该瓶签下所有药品信息，是数组存储的
                            //遍历数组，将所有药品解析出来
                            foreach (var drug in drugs)
                            {
                                string drugIndex = drug["DrugIndex"].ToString();//解析药品排序的序号
                                string drugName = drug["DrugName"].ToString();//解析药品的名称
                                string drugSpec = drug["DrugSpec"].ToString();//解析药品的规格型号
                                string drugDose = drug["DrugDose"].ToString();//解析药品的剂量
                                string drugCount = drug["DrugCount"].ToString();//解析药品的数量
                                Console.WriteLine("第" + drugIndex + "个药品信息是:" + "drugName=" + drugName + "drugSpec=" + drugSpec
                                    + "drugDose=" + drugDose + "drugCount=" + drugCount);
                            }
                        } 
                        else
                            Console.WriteLine("瓶签计费不成功，请不要冲配");
                    }                   
                }
                else
                    Console.WriteLine("瓶签计费不成功，请不要冲配");
            }
            catch (Exception ex)
            {
                Console.WriteLine("测试出错:" + ex.Message);
            }
        }
    }
}
