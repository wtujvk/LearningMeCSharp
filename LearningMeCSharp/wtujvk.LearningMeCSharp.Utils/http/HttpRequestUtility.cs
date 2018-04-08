using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.IO;

namespace wtujvk.LearningMeCSharp.Utils
{
   public class HttpRequestUtility
    {
        public string Method = "post";
        public string url;
        public Encoding encoding = Encoding.UTF8;

        /// <summary>
        /// 
        /// </summary>
        public HttpRequestUtility(string url)
        {
            this.url = url;
            Files = new Param();
            Params = new Param();
            Headers = new Param();
        }

        public class Param
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            public string this[string name]
            {
                get
                {
                    if (data.ContainsKey(name))
                    {
                        return data[name];
                    }
                    else
                    {
                        return null;
                    }
                }
                set
                {
                    if (data.ContainsKey(name))
                    {
                        data[name] = value;
                    }
                    else
                    {
                        data.Add(name, value);
                    }
                }
            }

            public string[] Keys
            {
                get
                {
                    return data.Keys.ToArray();
                }
            }
            //添加内容
            public void AddValue(KeyValuePair<string,string>ky)
            {
                if (!string.IsNullOrWhiteSpace(ky.Key) && !Keys.Any(c => c == ky.Key))
                {
                    data[ky.Key] = ky.Value;
                }
            }
        }
        /// <summary>
        /// 直接添加键值对
        /// </summary>
        /// <param name="dics"></param>
        /// <param name="isReplace"></param>
        public void AddDictionary(Dictionary<string,string>dics,bool isReplace=true)
        {
            if(dics!=null && dics.Count > 0)
            {
                foreach (var item in dics)
                {
                    var key0 = item.Key;
                    if (string.IsNullOrWhiteSpace(item.Key))
                    {
                        key0 = key0.Trim();
                        if (!Params.Keys.Any(c => c == key0))
                        {
                            this.Params.AddValue(item);
                        }
                    }
                }
            }
        }

        public Param Params;
        public Param Files;
        public Param Headers;
        public HttpWebResponse response;

        public string Response()
        {
            try
            {
                string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                byte[] endbytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "multipart/form-data; boundary=" + boundary;
                request.Method = "POST";
                request.KeepAlive = true;

                foreach (var item in Headers.Keys)
                {
                    request.Headers.Add(item, Headers[item]);
                }

                StringBuilder sb = new StringBuilder();

                using (Stream stream = request.GetRequestStream())
                {
                    //1.1 key/value
                    string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                    foreach (string name in Params.Keys)
                    {
                        stream.Write(boundarybytes, 0, boundarybytes.Length);
                        string formitem = string.Format(formdataTemplate, name, Params[name]);
                        byte[] formitembytes = encoding.GetBytes(formitem);
                        stream.Write(formitembytes, 0, formitembytes.Length);
                        sb.AppendFormat("{0}={1}&", name, Params[name]);
                    }
                    //1.2 file
                    string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                    foreach (var name in Files.Keys)
                    {
                        stream.Write(boundarybytes, 0, boundarybytes.Length);
                        string header = string.Format(headerTemplate, name, Path.GetFileName(Files[name]));
                        byte[] headerbytes = encoding.GetBytes(header);
                        stream.Write(headerbytes, 0, headerbytes.Length);
                        using (FileStream fileStream = new FileStream(Files[name], FileMode.Open))
                        {
                            byte[] buffer = new byte[fileStream.Length];
                            fileStream.Read(buffer, 0, buffer.Length);
                            stream.Write(buffer, 0, buffer.Length);
                        }
                    }
                    //1.3 form end
                    stream.Write(endbytes, 0, endbytes.Length);
                }
                string url2 = string.Empty;
                if (sb.Length > 0)
                {
                    url2 = url2 + "?" + sb.ToString().Substring(0, sb.ToString().Length - 1);
                }
                //2.WebResponse
                response = (HttpWebResponse)request.GetResponse();

                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    return stream.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex);
                return "failure:" + ex.Message;
            }
        }

        public T GetResult<T>()
        {
            var result = Response();
            return result.FromJosn<T>();
        }
    }
}
