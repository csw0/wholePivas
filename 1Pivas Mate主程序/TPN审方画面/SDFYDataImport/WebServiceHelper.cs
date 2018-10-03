using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Services.Description;
using Microsoft.CSharp;

namespace SDFYDataImport
{
    public static class WebServiceHelper
    {
        /// <summary>  
        /// 动态调用WebService  
        /// </summary>  
        /// <param name="url">WebService地址</param>  
        /// <param name="methodname">方法名(模块名)</param>  
        /// <param name="args">参数列表,无参数为null</param>  
        /// <returns>object</returns>  
        public static object InvokeWebService(string url, string methodname, object[] args)
        {
            return InvokeWebService(url, null, methodname, args);
        }
        /// <summary>  
        /// 动态调用WebService  
        /// </summary>  
        /// <param name="url">WebService地址</param>  
        /// <param name="classname">类名</param>  
        /// <param name="methodname">方法名(模块名)</param>  
        /// <param name="args">参数列表</param>  
        /// <returns>object</returns>  
        public static object InvokeWebService(string url, string classname, string methodname, object[] args)
        {
            string @namespace = "WebService.webservice";
            if (classname == null || classname == "")
            {
                classname = WebServiceHelper.GetClassName(url);
            }


            WebClient wc = new WebClient();
            Stream stream = wc.OpenRead(url + "?WSDL");   //获取服务描述语言(WSDL)  
            ServiceDescription sd = ServiceDescription.Read(stream);    //通过直接从 Stream实例加载 XML 来初始化ServiceDescription类的实例。  
            ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();

            sdi.AddServiceDescription(sd, "", "");
            CodeNamespace cn = new CodeNamespace(@namespace);  //CodeNamespace表示命名空间声明。  
            //生成客户端代理类代码  
            CodeCompileUnit ccu = new CodeCompileUnit();
            ccu.Namespaces.Add(cn);
            sdi.Import(cn, ccu);

            //CSharpCodeProvider csc = new CSharpCodeProvider(); 
            //ICodeCompiler icc = csc.CreateCompiler();//取得C#程式码编译器的执行个体  
            CSharpCodeProvider icc = new CSharpCodeProvider();

            //设定编译器的参数  
            CompilerParameters cplist = new CompilerParameters();//创建编译器的参数实例  
            cplist.GenerateExecutable = false;
            cplist.GenerateInMemory = true;
            cplist.ReferencedAssemblies.Add("System.dll");
            cplist.ReferencedAssemblies.Add("System.XML.dll");
            cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
            cplist.ReferencedAssemblies.Add("System.Data.dll");
            //编译代理类  
            CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
            if (true == cr.Errors.HasErrors)
            {
                System.Text.StringBuilder sb = new StringBuilder();
                foreach (CompilerError ce in cr.Errors)
                {
                    sb.Append(ce.ToString());
                    sb.Append(System.Environment.NewLine);
                }
                throw new Exception(sb.ToString());
            }

            //生成代理实例,并调用方法  
            System.Reflection.Assembly assembly = cr.CompiledAssembly;
            Type t = assembly.GetType(@namespace + "." + classname, true, true);
            object obj = Activator.CreateInstance(t);
            System.Reflection.MethodInfo mi = t.GetMethod(methodname);//MethodInfo 的实例可以通过调用GetMethods或者Type对象或派生自Type的对象的GetMethod方法来获取，还可以通过调用表示泛型方法定义的 MethodInfo 的MakeGenericMethod方法来获取。  
             
            return mi.Invoke(obj, args);

        }
        private static string GetClassName(string url)
        {
            //假如URL为"http://localhost/InvokeService/Service1.asmx"  
            //最终的返回值为 Service1  
            string[] parts = url.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');
            return pps[0];
        }

    }  
}
