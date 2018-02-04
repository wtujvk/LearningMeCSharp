using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace wtujvk.LearningMeCSharp.ToolStandard.Utils.Encrypts
{
    /// <summary>
    /// 3DES加解密算法帮助类
    /// </summary>
    public static class DESHelper
    {
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="text">要加密的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>加密后并经base64编码的字符串</returns>
        /// <remarks>静态方法，采用默认UTF8编码</remarks>
        public static string Encrypt(string text, string key)
        {
            return Encrypt(text, key, ASCIIEncoding.UTF8);
        }

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="text">要加密的字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>加密后并经base64编码的字符串</returns>
        /// <remarks>重载，指定编码方式</remarks>
        public static string Encrypt(string text, string key, Encoding encoding)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

            DES.Key = hashMD5.ComputeHash(encoding.GetBytes(key));
            DES.Mode = CipherMode.ECB;

            ICryptoTransform DESEncrypt = DES.CreateEncryptor();


            byte[] Buffer = encoding.GetBytes(text);
            return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="text">要解密的字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>解密后的字符串</returns>
        /// <remarks>静态方法，采用默认ascii编码</remarks>
        public static string Decrypt(string text, string key)
        {
            return Decrypt(text, key, ASCIIEncoding.UTF8);
        }

        /// <summary>
        /// 3des解密字符串
        /// </summary>
        /// <param name="text">要解密的字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>解密后的字符串</returns>
        /// <remarks>静态方法，指定编码方式</remarks>
        public static string Decrypt(string text, string key, Encoding encoding)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

            DES.Key = hashMD5.ComputeHash(encoding.GetBytes(key));
            DES.Mode = CipherMode.ECB;

            ICryptoTransform DESDecrypt = DES.CreateDecryptor();

            string result = "";
            try
            {
                byte[] Buffer = Convert.FromBase64String(text);
                result = encoding.GetString(DESDecrypt.TransformFinalBlock
                    (Buffer, 0, Buffer.Length));
            }
            catch (Exception e)
            {
                throw (new Exception("Invalid Key or input string is not a valid base64 string", e));
            }
            return result;
        }
    }
}
