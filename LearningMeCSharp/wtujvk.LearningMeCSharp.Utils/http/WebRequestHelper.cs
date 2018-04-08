﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Collections.Specialized;

namespace wtujvk.LearningMeCSharp.Utils
{
    /// <summary>
    /// ** 描述：模拟HTTP POST GET请求并获取数据
    /// 使用：WebRequestHelper wr = new WebRequestHelper();
    /// //GET
    /// var html= ws.HttpGet("http://WeiChat.HMIS.net");
    /// //带参GET
    /// var paras=new Dictionary<string, string>() ;
    /// paras.Add("name","skx");
    /// paras.Add("id", "100");
    /// var html2 = ws.HttpGet("http://WeiChat.HMIS.net",paras );
    /// //POST
    /// var postHtml= ws.HttpPost("http://WeiChat.HMIS.net", paras);
    /// //post and file
    /// var postHtml2 = ws.HttpUploadFile("http://WeiChat.HMIS.net", "文件地址可以是数组", paras);
    /// </summary>
    public class WebRequestHelper
    {
        /// <summary>
        /// 设置cookie
        /// </summary>
        private CookieContainer cookie;

        public CookieContainer Cookie
        {
            get { return cookie; }
            set { cookie = value; }
        }

        /// <summary>
        /// 是否允许重定向
        /// </summary>
        private bool allowAutoRedirect = true;
        /// <summary>
        /// 是否允许重定向(默认:true)
        /// </summary>
        public bool AllowAutoRedirect
        {
            get { return allowAutoRedirect; }
            set { allowAutoRedirect = value; }
        }

        /// <summary>
        /// contentType
        /// </summary>
        private string contentType = "application/x-www-form-urlencoded";
        /// <summary>
        /// 设置contentType(默认:application/x-www-form-urlencoded)
        /// </summary>
        public string ContentType
        {
            get { return contentType; }
            set { contentType = value; }
        }

        /// <summary>
        /// accept
        /// </summary>
        private string accept = "*/*";
        /// <summary>
        /// 设置accept(默认:*/*)
        /// </summary>
        public string Accept
        {
            get { return accept; }
            set { accept = value; }
        }

        /// <summary>
        /// 过期时间（默认：30000）
        /// </summary>
        private int timeOut = 30000;

        public int TimeOut
        {
            get { return timeOut; }
            set { timeOut = value; }
        }

        private string userAgent;
        /// <summary>
        /// 
        /// </summary>
        public string UserAgent
        {
            get { return userAgent; }
            set { userAgent = value; }
        }
        private string acceptLanguage;
        /// <summary>
        /// 接受的语言
        /// </summary>
        public string AcceptLanguage
        {
            get { return acceptLanguage; }
            set { acceptLanguage = value; }
        }
        /// <summary>
        /// 处理POST请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postdata"></param>
        /// <returns></returns>
        public string HttpPost(string url, string postdata)
        {
            var request = CreateWebRequest(url);
            request.Method = "POST";
            if (!string.IsNullOrWhiteSpace(postdata))
            {
                var bytesToPost = Encoding.UTF8.GetBytes(postdata);
                request.ContentLength = bytesToPost.Length;
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bytesToPost, 0, bytesToPost.Length);
                    requestStream.Close();
                }
            }
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    var result = sr.ReadToEnd();
                    return result;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public T HttpPost<T>(string url, object obj)
        {
            var resultStr = HttpPost(url, Jsoner.SerializeObject(obj));
            return Jsoner.DeserializeObject<T>(resultStr);
        }
        public T HttpPost<T>(string url, object obj, out string result, Func<string, string> serializeStrFunc = null)
        {
            var str = Jsoner.SerializeObject(obj);
            if (serializeStrFunc != null)
            {
                str = serializeStrFunc(str);
            }
            result = HttpPost(url, str);
            return Jsoner.DeserializeObject<T>(result);
        }
        /// <summary>
        /// post请求返回html ----2017-05-03修改
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postDataStr"></param>
        /// <returns></returns>
        public string HttpPost(string url, Dictionary<string, string> postdata)
        {
            string postDataStr = null;
            if (postdata != null && postdata.Count > 0)
            {
                postDataStr = string.Join("&", postdata.Select(it => it.Key + "=" + it.Value));
            }
            return HttpPost(url, postDataStr);
        }
        /// <summary>
        /// get请求获取返回的html
        /// </summary>
        /// <param name="url">无参URL</param>
        /// <param name="querydata">参数</param>
        /// <returns></returns>
        public string HttpGet(string url, Dictionary<string, string> querydata)
        {
            if (querydata != null && querydata.Count > 0)
            {
                url += "?" + string.Join("&", querydata.Select(it => it.Key + "=" + it.Value));
            }
            return HttpGet(url);
        }
        /// <summary>
        /// get请求获取返回的html
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string HttpGet(string url)
        {
            HttpWebRequest request = CreateWebRequest(url);
            request.Method = "GET";
            // response.Cookies = cookie.GetCookies(response.ResponseUri);
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var sr = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")))
                {
                    var result = sr.ReadToEnd();
                    return result;
                }
            }
        }

        protected HttpWebRequest CreateWebRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //request.ContentType = "text/html;charset=UTF-8";
            request.ContentType = this.ContentType;
            if (cookie != null)
                request.CookieContainer = this.Cookie;
            if (string.IsNullOrEmpty(this.AcceptLanguage))
            {
                WebHeaderCollection myWebHeaderCollection = request.Headers;
                myWebHeaderCollection.Add("Accept-Language", this.AcceptLanguage);
            }
            request.Accept = this.Accept;
            request.UseDefaultCredentials = true;
            request.UserAgent = this.UserAgent;
            request.Timeout = this.TimeOut;
            request.AllowAutoRedirect = this.AllowAutoRedirect;
            this.SetCertificatePolicy();
            return request;
        }

        /// <summary>
        /// POST文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="file">文件路径</param>
        /// <param name="postdata"></param>
        /// <returns></returns>
        public string HttpUploadFile(string url, string file, Dictionary<string, string> postdata)
        {
            return HttpUploadFile(url, file, postdata, Encoding.UTF8);
        }
        /// <summary>
        /// POST文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="file">文件路径</param>
        /// <param name="postdata">参数</param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public string HttpUploadFile(string url, string file, Dictionary<string, string> postdata, Encoding encoding)
        {
            return HttpUploadFile(url, new string[] { file }, postdata, encoding);
        }
        /// <summary>
        /// POST文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="files">文件路径</param>
        /// <param name="postdata">参数</param>
        /// <returns></returns>
        public string HttpUploadFile(string url, string[] files, Dictionary<string, string> postdata)
        {
            return HttpUploadFile(url, files, postdata, Encoding.UTF8);
        }
        /// <summary>
        /// POST文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="files">文件路径</param>
        /// <param name="postdata">参数</param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public string HttpUploadFile(string url, string[] files, Dictionary<string, string> postdata, Encoding encoding)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endbytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");


            HttpWebRequest request = CreateWebRequest(url);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            request.KeepAlive = true; ;
            request.AllowAutoRedirect = this.AllowAutoRedirect;
            if (this.Cookie != null)
                request.CookieContainer = this.Cookie;
            request.Credentials = CredentialCache.DefaultCredentials;

            using (Stream stream = request.GetRequestStream())
            {
                //1.1 key/value
                string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                if (postdata != null)
                {
                    foreach (string key in postdata.Keys)
                    {
                        stream.Write(boundarybytes, 0, boundarybytes.Length);
                        string formitem = string.Format(formdataTemplate, key, postdata[key]);
                        byte[] formitembytes = encoding.GetBytes(formitem);
                        stream.Write(formitembytes, 0, formitembytes.Length);
                    }
                }

                //1.2 file
                string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                byte[] buffer = new byte[4096];
                int bytesRead = 0;
                for (int i = 0; i < files.Length; i++)
                {
                    stream.Write(boundarybytes, 0, boundarybytes.Length);
                    string header = string.Format(headerTemplate, "file" + i, Path.GetFileName(files[i]));
                    byte[] headerbytes = encoding.GetBytes(header);
                    stream.Write(headerbytes, 0, headerbytes.Length);
                    using (FileStream fileStream = new FileStream(files[i], FileMode.Open, FileAccess.Read))
                    {
                        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            stream.Write(buffer, 0, bytesRead);
                        }
                    }
                }

                //1.3 form end
                stream.Write(endbytes, 0, endbytes.Length);
            }
            //2.WebResponse
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                return stream.ReadToEnd();
            }
        }


        /// <summary>
        /// 获得响应中的图像
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Stream GetResponseImage(string url)
        {
            Stream stream = null;
            try
            {
                HttpWebRequest request = CreateWebRequest(url);
                request.KeepAlive = true;
                request.Method = "GET";
                HttpWebResponse res = (HttpWebResponse)request.GetResponse();
                stream = res.GetResponseStream();
                return stream;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 正则获取匹配的第一个值
        /// </summary>
        /// <param name="html"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private string GetStringByRegex(string html, string pattern)
        {
            Regex re = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matchs = re.Matches(html);
            if (matchs.Count > 0)
            {
                return matchs[0].Groups[1].Value;
            }
            else
                return "";
        }
        /// <summary>
        /// 正则验证返回的response是否正确
        /// </summary>
        /// <param name="html">Html内容</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns></returns>
        private bool VerifyResponseHtml(string html, string pattern)
        {
            Regex re = new Regex(pattern);
            return re.IsMatch(html);
        }
        //注册证书验证回调事件，在请求之前注册
        private void SetCertificatePolicy()
        {
            ServicePointManager.ServerCertificateValidationCallback
                       += RemoteCertificateValidate;
        }
        /// <summary> 
        /// 远程证书验证，固定返回true
        /// </summary> 
        private static bool RemoteCertificateValidate(object sender, X509Certificate cert,
            X509Chain chain, SslPolicyErrors error)
        {
            return true;
        }
    }
}
