using System;
using System.Collections.Generic;
using System.Text;

namespace wtujvk.LearningMeCSharp.ToolStandard.Utils
{
    public class MD5Helper
    {
        public static string Encrypt(string text)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider MD5CSP = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] MD5Source = System.Text.Encoding.UTF8.GetBytes(text);
            byte[] MD5Out = MD5CSP.ComputeHash(MD5Source);
            return Convert.ToBase64String(MD5Out);
        }
    }
}
