using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace wtujvk.LearningMeCSharp.ToolStandard.Utils.Encrypts
{
    public class EncryUtils
    {
        /// <summary>
      /// 签名算法
      /// </summary>
      /// <param name="str"></param>
      /// <returns></returns>
        public static string GetSha1(string str)
        {
            //建立SHA1对象
            using (SHA1 sha = new SHA1CryptoServiceProvider())
            {
                //将mystr转换成byte[] 
                ASCIIEncoding enc = new ASCIIEncoding();
                byte[] dataToHash = enc.GetBytes(str);
                //Hash运算
                byte[] dataHashed = sha.ComputeHash(dataToHash);
                //将运算结果转换成string
                string hash = BitConverter.ToString(dataHashed).Replace("-", "");
                return hash;
            }
        }

        /// <summary>
        /// 获取大写的MD5签名结果 (微信，默认使用GB2312编码)
        /// </summary>
        /// <param name="encypStr"></param>
        /// <param name="charset">默认使用GB2312编码</param>
        /// <returns></returns>
        public static string MD5(string encypStr, string charset= "GB2312")
        {
            string retStr;
            var m5 = new MD5CryptoServiceProvider();
            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;
            //使用GB2312编码方式把字符串转化为字节数组．
            try
            {
               inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
            }
            catch (Exception)
            {
                inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
            }
            outputBye = m5.ComputeHash(inputBye);
            retStr = BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();
            return retStr;
        }
    }
}
