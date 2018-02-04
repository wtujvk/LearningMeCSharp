using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using wtujvk.LearningMeCSharp.ToolStandard.Conf;
using wtujvk.LearningMeCSharp.ToolStandard.Utils;

namespace wtujvk.LearningMeCSharp.ToolStandard.Messages
{
    /// <summary>
    /// RTX消息服务
    /// </summary>
    internal class RTXMessageManager : IMessageManager
    {
        #region Singleton
        private static object lockObj = new object();
        public static RTXMessageManager Instance;
        static RTXMessageManager()
        {
            lock (lockObj)
            {
                if (Instance == null)
                    Instance = new RTXMessageManager();
            }
        }
        private RTXMessageManager()
        { }
        #endregion

        private string EncodingString(string str)
        {
            // return HttpUtility.UrlEncode(str, Encoding.GetEncoding("GB2312"));
            return HttpUtilitys.UrlEncode(str);
        }

        private string rtxUrl = ConfigManager.Config.Messaging.RtxApi ??
            "http://192.168.1.8:8012/sendnotifynew.cgi?";


        public int Send(string recipient, string subject, string body)
        {
            return Send(recipient, subject, body, null, null);
        }

        public int Send(string recipient, string subject, string body, Action errorAction = null, Action successAction = null)
        {
            return Send(new List<string> { recipient }, subject, body, successAction, errorAction);
        }

        public int Send(IEnumerable<string> recipients, string subject, string body, Action errorAction = null, Action successAction = null)
        {
            LoggerFactory.Instance.Logger_Info("message to:" + string.Join(",", recipients));
            var dic = new Dictionary<string, string>()
                     {    {"title",EncodingString(subject)},
                          {"msg",EncodingString(body)},
                          {"receiver", EncodingString(string.Join(",",recipients))},
                          {"delaytime","0"}
                      };
            StringBuilder str = new StringBuilder();
            foreach (var item in dic)
            {
                str.Append(item.Key + "=" + item.Value + "&");
            }
            var result = new HttpClient().GetAsync(rtxUrl + str.ToString()).Result;
            if (result.StatusCode != HttpStatusCode.OK && errorAction != null)//需要改
            {
                errorAction();
            }
            else
                if (successAction != null)
            {
                successAction();
            }

            return Convert.ToInt32(result.StatusCode);
        }
    }
}
