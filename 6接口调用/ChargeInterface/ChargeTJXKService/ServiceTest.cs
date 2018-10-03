using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Net;
using System.Web.Services.Description;

namespace ChargeInterface.ChargeTJXKService
{
    public class ServiceTest
    {
        #region InvokeWebService
        //动态调用web服务
        public static object InvokeWebService(string url, string methodname, object[] args)
        {
            return ServiceTest.InvokeWebService(url, null, methodname, args);
        }

        public static object InvokeWebService(string url, string classname, string methodname, object[] args)
        {
            string @namespace = "EnterpriseServerBase.WebService.DynamicWebCalling";
            if ((classname == null) || (classname == ""))
            {
                classname = ServiceTest.GetWsClassName(url);
            }

            try
            {
                //获取WSDL
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(url + "?WSDL");
                ServiceDescription sd = ServiceDescription.Read(stream);
                ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
                sdi.AddServiceDescription(sd, "", "");
                CodeNamespace cn = new CodeNamespace(@namespace);

                //生成客户端代理类代码
                CodeCompileUnit ccu = new CodeCompileUnit();
                ccu.Namespaces.Add(cn);
                sdi.Import(cn, ccu);

                #region csw 注释，因CreateCompiler已过期弃用
                //CSharpCodeProvider csc = new CSharpCodeProvider();
                //ICodeCompiler icc = csc.CreateCompiler();

                //设定编译参数
                //CompilerParameters cplist = new CompilerParameters();
                //cplist.GenerateExecutable = false;
                //cplist.GenerateInMemory = true;
                //cplist.ReferencedAssemblies.Add("System.dll");
                //cplist.ReferencedAssemblies.Add("System.XML.dll");
                //cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                //cplist.ReferencedAssemblies.Add("System.Data.dll");

                //编译代理类
                //CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
                #endregion

                #region csw修改，因CreateCompiler已过期弃用
                if (!CodeDomProvider.IsDefinedLanguage("CSharp"))
                    return "编译失败";
                CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");

                //设定编译参数
                CompilerParameters cplist = new CompilerParameters();
                cplist.GenerateExecutable = false;
                cplist.GenerateInMemory = true;
                cplist.ReferencedAssemblies.Add("System.dll");
                cplist.ReferencedAssemblies.Add("System.XML.dll");
                cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                cplist.ReferencedAssemblies.Add("System.Data.dll");

                CompilerResults cr = provider.CompileAssemblyFromDom(cplist, ccu);
                #endregion

                if (true == cr.Errors.HasErrors)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
                    {
                        sb.Append(ce.ToString());
                        sb.Append(System.Environment.NewLine);
                    }
                    throw new Exception(sb.ToString());
                }

                //生成代理实例，并调用方法
                System.Reflection.Assembly assembly = cr.CompiledAssembly;
                Type t = assembly.GetType(@namespace + "." + classname, true, true);
                object obj = Activator.CreateInstance(t);
                System.Reflection.MethodInfo mi = t.GetMethod(methodname);

                return mi.Invoke(obj, args);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message, new Exception(ex.InnerException.StackTrace));
            }
        }

        private static string GetWsClassName(string wsUrl)
        {
            string[] parts = wsUrl.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');

            return pps[0];
        }
        #endregion
    }
}
