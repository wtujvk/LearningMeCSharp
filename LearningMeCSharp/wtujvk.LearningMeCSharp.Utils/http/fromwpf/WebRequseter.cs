using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace wtujvk.LearningMeCSharp.Utils.http
{
    public class WebRequseter
    {
        public WebRequseter()
        {
         
        }

        public object RequseteWeb(string apiMapKey, string apiInterfaceName, string methodName, List<object> parameterList, string apiUrl = "")
        {
            return RequseteWeb(apiMapKey, apiInterfaceName, methodName, parameterList, null, apiUrl);
        }
        public object RequseteWeb(string apiMapKey, string apiInterfaceName, string methodName, List<object> parameterList, string[] types, string apiUrl = "")
        {
            System.GC.Collect();
            HttpWebResponse response = null;
            Stream streamResponse = null;
            HttpWebRequest request = null;
            try
            {
                MethodDC theContract = new MethodDC();
                theContract.ApiConfigKey = apiMapKey;
                theContract.InterfaceName = apiInterfaceName;
                theContract.MethodName = methodName;
                theContract.TypeArguments = types;

                List<ParameterDC> webParamerList = new List<ParameterDC>();
                foreach (object param in parameterList)
                {
                    ParameterDC aWebParamer = new ParameterDC();
                    aWebParamer.TypeQualifiedName = param.GetType().AssemblyQualifiedName;
                    aWebParamer.JsonValue =param.ToJson();
                    webParamerList.Add(aWebParamer);
                }
                theContract.ParameterList = webParamerList;

                string jsonContract = theContract.ToJson();;
                //TODO:加密或压缩对jsonContract,密文可根据日期变化。服务端验证密文 
                MethodDC vdf = jsonContract.FromJosn<MethodDC>();

                byte[] PostData = Encoding.UTF8.GetBytes(jsonContract);

                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                request = (HttpWebRequest)WebRequest.Create(apiUrl);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.GetRequestStream().Write(PostData, 0, PostData.Length);
                request.ContentType = "text/xml; charset=utf-8";
                ServicePoint currentServicePoint = request.ServicePoint;
                currentServicePoint.ConnectionLimit = int.MaxValue;//1000;

                response = (HttpWebResponse)request.GetResponse();
                streamResponse = response.GetResponseStream();

                string strResponse = string.Empty;
                //如果服务端采用了GZIP压缩,则先解压缩。
                if (response.ContentEncoding.ToLower().Contains("gzip"))
                {
                    using (GZipStream gz = new GZipStream(streamResponse, CompressionMode.Decompress))
                    {
                        using (StreamReader readerGzip = new StreamReader(gz, Encoding.UTF8))
                        {
                            strResponse = readerGzip.ReadToEnd();
                        }
                    }
                }
                else
                {
                    using (StreamReader streamRead = new StreamReader(streamResponse, Encoding.UTF8))
                    {
                        strResponse = streamRead.ReadToEnd();
                    }
                }

                //TODO:解密或解压缩对strResponse
                MethodDC reObj = strResponse.FromJosn<MethodDC>();
                watch.Stop();

                //写请求日志，用于跟踪请求的时间和数据大小。
                string writeWebRequestLog = System.Configuration.ConfigurationManager.AppSettings["WriteClientHttpWebRequestLog"];
                if (writeWebRequestLog == null) { writeWebRequestLog = "false"; }
                if (bool.Parse(writeWebRequestLog) && watch.ElapsedMilliseconds > 1000)
                {
                    byte[] responseData = Encoding.UTF8.GetBytes(strResponse);
                    var counter = watch.ElapsedMilliseconds;
                    string strRequestTimeLog = string.Format(" 接口：{0} 调用方法：{1} 请求响应时间：{2}(毫秒) 返回值大小：{3}Kb", apiInterfaceName, methodName, counter.ToString(), (responseData.Length / 1024.00).ToString("f0"));
                    WriteLog(strRequestTimeLog, null);
                }

                if (reObj.Result.HasException)
                {
                    if (reObj.Result.ExceptionState == ExceptionStateEn.InformationException)
                    {
                        LoggerFactory.Instance.Logger_Debug("消息："+reObj.Result.ExceptionMessage);
                        return null;
                    }
                    else if (reObj.Result.ExceptionState ==ExceptionStateEn.WarningException)
                    {
                        LoggerFactory.Instance.Logger_Warn("警告："+reObj.Result.ExceptionMessage);
                        return null;
                    }
                    else if (reObj.Result.ExceptionState ==  ExceptionStateEn.ErrorException)
                    {
                        LoggerFactory.Instance.Logger_Exception("错误："+reObj.Result.ExceptionMessage, () => { });
                        return null;
                    }
                    else
                    {
                        throw new Exception("Message: " + reObj.Result.ExceptionMessage + "\r\n"
                            + "Stack: " + reObj.Result.ExceptionStack);
                    }
                }
                else
                {
                    Type relType = Type.GetType(reObj.Result.TypeQualifiedName);

                    object relResult = null;
                    if (relType.Name.ToLower() != "void")
                    {
                        relResult = reObj.Result.JsonValue.FromJosn(relType);
                    }
                    return relResult;
                }
            }
            finally
            {
                if (streamResponse != null)
                {
                    streamResponse.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
        }

        private static void WriteLog(string msg, string logfile = null)
        {
            LoggerFactory.Instance.Logger_Exception(msg, () => { });
        }
    }
}
