using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace wtujvk.LearningMeCSharp.ToolStandard.Utils.Encrypts
{
    /// <summary>
    /// 做一些加密的操作
    /// </summary>
    public class Security
    {
        public Security()
        {

        }

        private char[] arr36Base = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private static int[] T1 = { 5, 8, 3, 2, 0, 1, 9, 4, 6, 7 };
        private static int[] T2 = { 4, 5, 3, 2, 7, 0, 8, 9, 1, 6 };
        private static int[] KeyArray1 = { 3, 7, 0, 1, 5, 9, 3, 2, 6, 8 };
        private static int[] KeyArray2 = { 2, 8, 5, 4, 9, 3, 0, 7, 1, 6 };

        #region 由Simon新增的四个加解密方法

        #region 公共属性 静态成员 初始化DES密钥 By Simon

        private static byte[] Key_64 = { 47, 13, 92, 116, 78, 4, 218, 32 };
        private static byte[] Iv_64 = { 35, 114, 219, 39, 96, 66, 167, 3 };
        private static byte[] Key_192 = { 47, 13, 92, 116, 78, 4, 218, 32, 15, 167, 44, 80, 26, 250, 155, 112, 2, 94, 11, 204, 119, 35, 184, 194 };
        private static byte[] Iv_192 = { 35, 114, 219, 39, 96, 66, 167, 3, 42, 5, 62, 83, 184, 7, 209, 13, 145, 23, 200, 58, 173, 10, 121, 228 };
        #endregion


        #region 公共函数 静态成员 提供字符串MD5加密功能
        // MD5加密，通常用在用户密码的加密和验证
        public static string MD5_Encrypt(string val)
        {
            string EncryptPassword = EncryUtils.MD5(val);
            //EncryptPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(val, "MD5");
            return EncryptPassword;
        }
        #endregion

        #region 公共函数 静态成员 提供字符串DES加密功能
        // DES加密
        public static string DES_Encrypt(string val)
        {
            string Code = "";
            if (!val.Equals(""))
            {
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(Key_64, Iv_64), CryptoStreamMode.Write);
                StreamWriter sw = new StreamWriter(cs);
                sw.Write(val);
                sw.Flush();
                cs.FlushFinalBlock();
                ms.Flush();
                int Length = (int)ms.Length;

                Code = Convert.ToBase64String(ms.GetBuffer(), 0, Length);
            }
            return Code;
        }
        #endregion

        #region 公共函数 静态成员 提供字符串DES解密功能
        //
        /// <summary>
        ///  DES解密
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string DES_Decrypt(string val)
        {
            string Code = "";
            if (!val.Equals(""))
            {
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                byte[] buffer = Convert.FromBase64String(val);
                MemoryStream ms = new MemoryStream(buffer);
                CryptoStream cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(Key_64, Iv_64), CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cs);
                Code = sr.ReadToEnd();
            }
            return Code;
        }
        #endregion

        #region 线性加密
        #region 私有函数 整型 尾列加密
        private static int Sha_End(int val)
        {

            return T1[val];
        }
        #endregion

        #region	私有函数 整型 尾列解密
        private static int Sha_GetEnd(int val)
        {
            return T2[val];
        }
        #endregion

        #region	私有函数 整型 单步卷帘加密
        private static int Sha_Next(int val, int pre, bool flag)
        {
            int result = flag ? ((val + KeyArray1[pre]) % 10) : ((val + KeyArray2[pre]) % 10);
            return (result + 1) % 10;
        }
        #endregion

        #region	私有函数 整型 单步卷帘解密
        private static int Sha_Next2(int val, int pre, bool flag)
        {
            int result;
            val = (val + 9) % 10;
            if (flag)
            {
                val = val >= KeyArray1[pre] ? val : val + 10;
                result = val - KeyArray1[pre];
            }
            else
            {
                val = val >= KeyArray2[pre] ? val : val + 10;
                result = val - KeyArray2[pre];
            }
            return result;

        }
        #endregion

        #region	公有方法 整型 全数卷帘加密
        public static string Line_Encrypt(int intval)
        {
            return Line_Encrypt((long)intval);
        }
        public static string Line_Encrypt(long intval)
        {
            string val = intval.ToString();
            int i;
            string temp = "";
            bool flag = true;
            int temp2, temp3, temp4;
            //先求出最后一后数
            int pre = Sha_End(Convert.ToInt32(val.Substring(val.Length - 1, 1)));
            //从倒数第二位开始
            temp4 = pre;
            for (i = val.Length - 2; i >= 0; i--)
            {
                //取出该位数字
                temp3 = Convert.ToInt32(val.Substring(i, 1));
                //算出加密码,即倒数第二位加密后放在第一位。
                temp2 = Sha_Next(temp3, pre, flag);
                temp += temp2;
                pre = temp2;
                flag = !flag;
            }
            return temp + temp4;
        }
        #endregion

        #region	公有方法 整型 全数卷帘解密
        public static string Line_Decrypt(int intval)
        {
            string val = intval.ToString();
            int i;
            string temp = "";
            bool flag = true;
            int temp2, temp3, temp4;

            //先求出最后一后数
            int pre = Convert.ToInt32(val.Substring(val.Length - 1, 1));
            //从倒数第二位开始
            temp4 = Sha_GetEnd(pre);
            for (i = 0; i < val.Length - 1; i++)
            {
                //取出该位数字
                temp3 = Convert.ToInt32(val.Substring(i, 1));
                //算出加密码,即倒数第二位加密后放在第一位。

                temp2 = Sha_Next2(temp3, pre, flag);
                temp = temp2 + temp;
                pre = temp3;
                flag = !flag;
            }
            return temp + temp4;
        }
        #endregion
        #endregion

        #endregion // By Simon

        #region MD5, 3DES encrypt and decrypt methods
        /// <summary>
        /// MD5加密方法
        /// </summary>
        /// <param name="a_strValue">string to be encrypted</param>
        /// <returns>encrypted string</returns>
        public static string EncryptMD5String(string a_strValue)
        {
            try
            {
                //change to bytearray
                Byte[] hba = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).
                    ComputeHash(StringToByteArray(a_strValue));

                return ByteArrayToString(hba);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// Judge whether or not two string are the same
        /// </summary>
        /// <param name="a_str1"></param>
        /// <param name="a_str2"></param>
        /// <returns>比较他们是否相同</returns>
        public static bool IsSame(string a_str1, string a_str2)
        {
            try
            {
                Byte[] b1 = StringToByteArray(a_str1);
                Byte[] b2 = StringToByteArray(a_str2);
                if (b1.Length != b2.Length)
                {
                    return false;
                }

                for (int i = 0; i < b1.Length; i++)
                {
                    if (b1[i] != b2[i])
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// Convert string to Byte array
        /// </summary>
        /// <param name="s">string to be converted</param>
        /// <returns>字符转换成ByteArray</returns>
        public static Byte[] StringToByteArray(String s)
        {
            try
            {
                return Encoding.UTF8.GetBytes(s);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 转换Byte[]到字符
        /// </summary>
        /// <param name="a_arrByte">Byte Array to be converted</param>
        /// <returns>string converted from byte array</returns>
        public static string ByteArrayToString(Byte[] a_arrByte)
        {
            try
            {
                return Encoding.UTF8.GetString(a_arrByte);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="a_strString">string to be encrypted</param>
        /// <param name="a_strKey">key</param>
        /// <returns>string encrypted and encoded by base64</returns>
        /// <remarks>static method, use default ascii encode</remarks>
        public static string Encrypt3DES(string a_strString, string a_strKey)
        {
            try
            {
                TripleDESCryptoServiceProvider DES = new
                    TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

                DES.Key = hashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(a_strKey));
                DES.Mode = CipherMode.ECB;

                ICryptoTransform DESEncrypt = DES.CreateEncryptor();

                byte[] Buffer = ASCIIEncoding.ASCII.GetBytes(a_strString);
                return Convert.ToBase64String(DESEncrypt.TransformFinalBlock
                    (Buffer, 0, Buffer.Length));
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }//end method

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="a_strString">string to be encrypted</param>
        /// <param name="a_strKey">key</param>
        /// <param name="encoding">encoding method</param>
        /// <returns>string encrypted and encoded by base64</returns>
        /// <remarks>overload, assign encoding method</remarks>
        public static string Encrypt3DES(string a_strString, string a_strKey, Encoding encoding)
        {
            try
            {
                TripleDESCryptoServiceProvider DES = new
                    TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

                DES.Key = hashMD5.ComputeHash(encoding.GetBytes(a_strKey));
                DES.Mode = CipherMode.ECB;

                ICryptoTransform DESEncrypt = DES.CreateEncryptor();

                byte[] Buffer = encoding.GetBytes(a_strString);
                return Convert.ToBase64String(DESEncrypt.TransformFinalBlock
                    (Buffer, 0, Buffer.Length));
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_strString"></param>
        /// <param name="a_strKey"></param>
        /// <returns></returns>
        public static string Decrypt3DES(string a_strString, string a_strKey)
        {
            string result = "";
            try
            {
                TripleDESCryptoServiceProvider DES = new
                    TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

                DES.Key = hashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(a_strKey));
                DES.Mode = CipherMode.ECB;

                ICryptoTransform DESDecrypt = DES.CreateDecryptor();

                result = "";

                byte[] Buffer = Convert.FromBase64String(a_strString);
                result = ASCIIEncoding.ASCII.GetString(DESDecrypt.TransformFinalBlock
                    (Buffer, 0, Buffer.Length));
            }
            catch
            {
                //                throw(new Exception("Invalid Key or input string is not a valid base64 string" , e)) ;
            }

            return result;

        }//end method

        /// <summary>
        /// DES
        /// </summary>
        /// <param name="a_strString"></param>
        /// <param name="a_strKey"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Decrypt3DES(string a_strString, string a_strKey, Encoding encoding)
        {
            string result = "";
            try
            {
                TripleDESCryptoServiceProvider DES = new
                    TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

                DES.Key = hashMD5.ComputeHash(encoding.GetBytes(a_strKey));
                DES.Mode = CipherMode.ECB;

                ICryptoTransform DESDecrypt = DES.CreateDecryptor();

                result = "";

                byte[] Buffer = Convert.FromBase64String(a_strString);
                result = encoding.GetString(DESDecrypt.TransformFinalBlock
                    (Buffer, 0, Buffer.Length));
            }
            catch (Exception e)
            {
                throw (new Exception("Invalid Key or input string is not a valid base64 string", e));
            }

            return result;
        }//end method
        #endregion

        #region Encrypt and Decrypt methods for Brown Puppy System

        /// <summary>
        /// Encrypt an string
        /// </summary>
        /// <param name="pStr">String to be encrypted</param>
        /// <param name="pKey">Key string</param>
        /// <returns></returns>
        public string EncryptOcc(string pStr, string pKey)
        {

            string myString;
            myString = pKey + pStr;
            myString = TransEncrypt(myString);
            myString = StrToAscStr(myString);
            myString = TransEncrypt(myString);
            myString = DecStrToHex36Str(myString);
            myString = TransEncrypt(myString);
            return myString;
        }

        /// <summary>
        /// Decrypt an encrypted string
        /// </summary>
        /// <param name="pStr">Encrypted string</param>
        /// <param name="pKey">Key string</param>
        /// <returns></returns>
        public string DecryptOcc(string pStr, string pKey)
        {
            string myString;

            //HttpContext.Current.Response.Write("<hr>Now decrypt......<BR>");
            //HttpContext.Current.Response.Flush();

            myString = TransEncrypt(pStr);
            //HttpContext.Current.Response.Write("transencrypted string:" + myString + "<BR>");
            //HttpContext.Current.Response.Flush();

            myString = Hex36StrToDecStr(myString);
            //HttpContext.Current.Response.Write("Hex36StrToDecStr string:" + myString + "<BR>");
            //HttpContext.Current.Response.Flush();

            myString = TransEncrypt(myString);
            //HttpContext.Current.Response.Write("transencrypted string:" + myString + "<BR>");
            //HttpContext.Current.Response.Flush();

            myString = AscStrToStr(myString);
            //HttpContext.Current.Response.Write("AscStrToStr string:" + myString + "<BR>");
            //HttpContext.Current.Response.Flush();

            myString = TransEncrypt(myString);
            //HttpContext.Current.Response.Write("transencrypted string:" + myString + "<BR>");
            //HttpContext.Current.Response.Flush();


            try
            {
                if (!(myString.Substring(0, pKey.Length) == pKey))
                {
                    myString = "";
                }
                myString = myString.Substring(pKey.Length);
            }
            catch
            {
                //HttpContext.Current.Response.Write("Error occurs when decrypt!<BR>");
                myString = "";
            }

            return myString;
        }



        /// <summary>
        /// Change the charaters order, for each 4-digit group, move char 1 to char 4 position, move char 2 to char 3 order. 
        /// 1234 -> 4321, ABCD -> DCBA
        /// </summary>
        /// <param name="pStr"></param>
        /// <returns></returns>
        public string TransEncrypt(string pStr)
        {
            string strEncrypt = "";
            string c1, c2, c3, c4;

            for (int i = 0; i < pStr.Length; i = i + 4)
            {
                c1 = "";
                c2 = "";
                c3 = "";
                c4 = "";

                try
                {
                    c1 = pStr.Substring(i, 1);
                }
                catch
                {
                }

                try
                {
                    c2 = pStr.Substring(i + 1, 1);
                }
                catch
                {
                }

                try
                {
                    c3 = pStr.Substring(i + 2, 1);
                }
                catch
                {
                }

                try
                {
                    c4 = pStr.Substring(i + 3, 1);
                }
                catch
                {
                }

                strEncrypt = strEncrypt + c4 + c3 + c2 + c1;

            }

            return strEncrypt;
        }


        /// <summary>
        /// Convert a string to an Ascii string, each char in the string will be convert to its ascii code. 
        /// If the ascii code is less than 100, add 0 to the left.
        /// </summary>
        /// <param name="pStr"></param>
        /// <returns></returns>
        public string StrToAscStr(string pStr)
        {
            string strChar;
            byte byteChar;
            string ascStr;
            string strResult = "";
            for (int i = 0; i < pStr.Length; i++)
            {
                strChar = pStr.Substring(i, 1);
                byteChar = (byte)System.Convert.ToChar(strChar);
                if (byteChar < 100)
                {
                    ascStr = "0" + byteChar.ToString();
                }
                else
                {
                    ascStr = byteChar.ToString();
                }

                strResult += ascStr;
            }

            return strResult;
        }


        /// <summary>
        /// Convert an ascii code string to a string. Each 3-digit number will be treat as an ascii number
        /// </summary>
        /// <param name="pStr"></param>
        /// <returns></returns>
        public string AscStrToStr(string pStr)
        {
            string strResult = "";
            byte ascNum;
            if (pStr.Length % 3 != 0)
            {
                return "";
            }

            for (int i = 0; i < pStr.Length / 3; i++)
            {
                try
                {
                    ascNum = System.Convert.ToByte(pStr.Substring(i * 3, 3));
                }
                catch
                {
                    return "";
                }

                strResult += System.Convert.ToChar(System.Convert.ToByte(ascNum)).ToString();
            }

            return strResult;
        }

        /// <summary>
        /// Convert 10-based asscii number string to 36-based string
        /// Each 3-digit number will be converted to a 36-based string
        /// </summary>
        /// <param name="DecStr"></param>
        /// <returns></returns>
        public string DecStrToHex36Str(string DecStr)
        {
            string strResult = "";
            int intNum;
            for (int i = 0; i < DecStr.Length; i = i + 3)
            {
                if (i + 3 > DecStr.Length)
                {
                    intNum = System.Convert.ToInt32(DecStr.Substring(i, DecStr.Length - i));
                }
                else
                {
                    intNum = System.Convert.ToInt32(DecStr.Substring(i, 3));
                }

                strResult += DecToHex36(intNum);
            }

            return strResult;
        }

        /// <summary>
        /// Convert 36-based string to 10-based ascii number string
        /// </summary>
        /// <param name="Hex3Str"></param>
        /// <returns></returns>
        public string Hex36StrToDecStr(string Hex36Str)
        {
            string strResult = "";
            string Hex36;
            int intDec;

            for (int i = 0; i < Hex36Str.Length; i = i + 2)
            {
                if (i + 2 > Hex36Str.Length)
                {
                    Hex36 = Hex36Str.Substring(i, Hex36Str.Length - i);
                }
                else
                {
                    Hex36 = Hex36Str.Substring(i, 2);
                }

                intDec = Hex36ToDec(Hex36);

                if (intDec < 10)
                {
                    strResult += "00" + intDec.ToString();
                }
                else if (intDec < 100)
                {
                    strResult += "0" + intDec.ToString();
                }
                else
                {
                    strResult += intDec.ToString();
                }
            }

            return strResult;
        }

        /// <summary>
        /// Convert 36-based string to 10-based number
        /// Only accept 2 characters string
        /// </summary>
        /// <param name="StrHex3"></param>
        /// <returns></returns>
        public int Hex36ToDec(string StrHex36)
        {
            int intReturn = 0;
            char char1;
            char char2;
            if (StrHex36.Length > 2)
            {
                return 0;
            }

            if (StrHex36.Length == 2)
            {
                char1 = System.Convert.ToChar(StrHex36.Substring(0, 1));
                char2 = System.Convert.ToChar(StrHex36.Substring(1, 1));
            }
            else
            {
                char1 = System.Convert.ToChar(StrHex36);
                char2 = '0';
            }

            for (int i = 0; i < arr36Base.Length; i++)
            {
                if (arr36Base[i] == char1)
                {
                    intReturn += i * 36;
                }
            }

            for (int i = 0; i < arr36Base.Length; i++)
            {
                if (arr36Base[i] == char2)
                {
                    intReturn += i;
                }
            }


            return intReturn;

        }

        /// <summary>
        /// Convert 10-based number to 36-based string
        /// </summary>
        /// <param name="tnDec"></param>
        /// <returns></returns>
        public string DecToHex36(int intDec)
        {
            string strResult = "";
            int i, j;

            //we only accept number less than 1000
            if (intDec > 999)
            {
                return "";
            }

            i = intDec / 36;
            j = intDec % 36;

            strResult = System.Convert.ToString(arr36Base[i]) + System.Convert.ToString(arr36Base[j]);
            return strResult;
        }


        #endregion



        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="a_strString">需要加密的字符串</param>
        /// <param name="a_strKey">密匙</param>
        /// <returns></returns>
        public static string YHISJMDES1(string a_strString, string a_strKey)
        {
            string result = string.Empty;
            try
            {
                char[] jmString = a_strString.ToCharArray();
                char[] msString = a_strKey.ToCharArray();

                int msnumber = 0;
                for (int i = 0; i < msString.Length; i++)
                {
                    msnumber += msString[i];
                }

                for (int i = 0; i < jmString.Length; i++)
                {

                    jmString[i] = (char)(Convert.ToInt32(jmString[i]) + msnumber);
                    //     System.Console.WriteLine(jmString[i]);
                }

                for (int i = 0; i < jmString.Length; i++)
                {
                    result += string.Format("*{0}", Convert.ToInt32(jmString[i]));
                }

            }
            catch
            {
                // throw(new Exception("Invalid Key or input string is not a valid base64 string" , e)) ;
            }
            return result;
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="a_strString">需要加密的字符串</param>
        /// <param name="a_strKey">密匙</param>
        /// <returns></returns>
        public static string YHISJMDES2(string a_strString, string a_strKey)
        {
            string result = string.Empty;
            try
            {
                string[] jmString = a_strString.Split('*');
                char[] jmString1 = new char[jmString.Length - 1];
                char[] msString = a_strKey.ToCharArray();

                int msnumber = 0;
                for (int i = 0; i < msString.Length; i++)
                {
                    msnumber += msString[i];
                }

                for (int i = 1; i < jmString.Length; i++)
                {
                    if (string.Empty == jmString[i]) continue;
                    jmString1[i - 1] = (char)(Convert.ToInt32(jmString[i]) - msnumber);
                }
                for (int i = 0; i < jmString1.Length; i++)
                {
                    result += jmString1[i];
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }

            return result;
        }

        #region 加密解密文件
        //加密文件
        private static void EncryptData(String inName, String outName, byte[] desKey, byte[] desIV)
        {
            //Create the file streams to handle the input and output files.
            FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);

            //Create variables to help with read and write.
            byte[] bin = new byte[100]; //This is intermediate storage for the encryption.
            long rdlen = 0;              //This is the total number of bytes written.
            long totlen = fin.Length;    //This is the total length of the input file.
            int len;                     //This is the number of bytes to be written at a time.

            DES des = new DESCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(fout, des.CreateEncryptor(desKey, desIV), CryptoStreamMode.Write);

            //Read from the input file, then encrypt and write to the output file.
            while (rdlen < totlen)
            {
                len = fin.Read(bin, 0, 100);
                encStream.Write(bin, 0, len);
                rdlen = rdlen + len;
            }

            encStream.Close();
            fout.Close();
            fin.Close();
        }

        //解密文件
        private static void DecryptData(String inName, String outName, byte[] desKey, byte[] desIV)
        {
            //Create the file streams to handle the input and output files.
            FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);

            //Create variables to help with read and write.
            byte[] bin = new byte[100]; //This is intermediate storage for the encryption.
            long rdlen = 0;              //This is the total number of bytes written.
            long totlen = fin.Length;    //This is the total length of the input file.
            int len;                     //This is the number of bytes to be written at a time.

            DES des = new DESCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(fout, des.CreateDecryptor(desKey, desIV), CryptoStreamMode.Write);

            //Read from the input file, then encrypt and write to the output file.
            while (rdlen < totlen)
            {
                len = fin.Read(bin, 0, 100);
                encStream.Write(bin, 0, len);
                rdlen = rdlen + len;
            }

            encStream.Close();
            fout.Close();
            fin.Close();
        }
        #endregion
    }
}
