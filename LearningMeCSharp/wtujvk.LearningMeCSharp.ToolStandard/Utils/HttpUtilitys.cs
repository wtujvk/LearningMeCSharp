using System;
using System.Collections.Generic;
using System.Text;

namespace wtujvk.LearningMeCSharp.ToolStandard.Utils
{
    /// <summary>
    /// url编码
    /// </summary>
    public class HttpUtilitys
    {
        #region 旧代码
        ///// <summary>
        ///// UrlEncode重写：小写转大写，特殊字符特换
        ///// </summary>
        ///// <param name="strSrc">原字符串</param>
        ///// <param name="encoding">编码方式</param>
        ///// <param name="bToUpper">是否转大写</param>
        ///// <returns></returns>
        //  public string UrlEncode(string strSrc, System.Text.Encoding encoding, bool bToUpper)
        //  {
        //      System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
        //      for (int i = 0; i < strSrc.Length; i++)
        //      {
        //          string t = strSrc[i].ToString();
        //          string k = HttpUtility.UrlEncode(t, encoding);
        //          if (t == k)
        //          {
        //              stringBuilder.Append(t);
        //          }
        //          else
        //          {
        //              if (bToUpper)
        //                  stringBuilder.Append(k.ToUpper());
        //              else
        //                  stringBuilder.Append(k);
        //          }
        //      }
        //      if (bToUpper)
        //          return stringBuilder.ToString().Replace("+", "%2B");
        //      else
        //          return stringBuilder.ToString();
        //  } 
        #endregion

        public static string UrlEncode(string value)
        {
            return System.Net.WebUtility.UrlEncode(value);
        }

        public static string UrlDecode(string encodedValue)
        {
            return System.Net.WebUtility.UrlDecode(encodedValue);
        }
    }
}
