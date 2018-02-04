using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace wtujvk.LearningMeCSharp.ToolStandard.Utils
{
    /// <summary>
    /// 序列化与反序列化到内存
    /// </summary>
    public class SerializationHelper
    {
        private static object lockObj = new object();

        public SerializationHelper()
        {
        }

        #region XML
        /// <summary>
        /// XML序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeToXml(object obj)
        {
            string s = "";
            using (MemoryStream ms = new MemoryStream())
            {

                try
                {
                    XmlSerializer serializer = new XmlSerializer(obj.GetType());
                    ms.Seek(0, SeekOrigin.Begin);
                    serializer.Serialize(ms, obj);
                    s = Encoding.ASCII.GetString(ms.ToArray());
                }
                catch (SerializationException e)
                {
                    Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                    throw;
                }
            }
            return s;
        }
        /// <summary>
        /// XML返序列化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static object DeserializeFromXml(Type type, string s)
        {
            return DeserializeFromXml<object>(s);
        }
        /// <summary>
        /// XML泛型反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static T DeserializeFromXml<T>(string s) where T : class, new()
        {
            var o = new T();
           try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                o = serializer.Deserialize(new StringReader(s)) as T;
            }
            catch (SerializationException e)
            {
                throw new Exception("Failed to deserialize. Reason: " + e.Message);
            }
            return o;
        }
        #endregion

        #region Binary
        /// <summary>
        /// 二进制序列化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] SerializeToBinary(object value)
        {
            BinaryFormatter serializer = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();
            memStream.Seek(0, 0);
            serializer.Serialize(memStream, value);
            return memStream.ToArray();
        }
        /// <summary>
        /// 二进制反序列化
        /// </summary>
        /// <param name="someBytes"></param>
        /// <returns></returns>
        public static object DeserializeFromBinary(byte[] someBytes)
        {
            IFormatter bf = new BinaryFormatter();
            object res = null;
            if (someBytes == null)
                return null;
            using (var memoryStream = new MemoryStream())
            {
                memoryStream.Write(someBytes, 0, someBytes.Length);
                memoryStream.Seek(0, 0);
                memoryStream.Position = 0;
                res = bf.Deserialize(memoryStream);
            }
            return res;
        }
        
        #endregion

        #region Binary File
        /// <summary>
        /// 二进制序列化到磁盘
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="obj"></param>
        public static void SerializableToBinaryFile(string fileName, object obj)
        {
            lock (lockObj)
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, obj);
                }
            }
        }
        /// <summary>
        /// 二进制反序列化从磁盘到内存对象
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static object DeserializeFromBinaryFile(string fileName)
        {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return formatter.Deserialize(fs);
                }
            }
            catch (Exception)
            {
                return null;
            }

        }
        #endregion

        #region XML File
        /// <summary>
        /// XML将对象序列化到磁盘文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="obj"></param>
        public static void SerializeToXmlFile(string fileName, object obj)
        {
            lock (lockObj)
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    new XmlSerializer(obj.GetType()).Serialize(fs, obj);
                }
            }
        }
        /// <summary>
        /// XML反序列化从磁盘到内存对象
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static object DeserializeFromXmlFile(string fileName)
        {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    return new XmlSerializer(typeof(object)).Deserialize(fs);
                }
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// 泛型版本：XML将对象序列化到磁盘文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="obj"></param>
        public static void SerializeToXmlFile<T>(string fileName, T obj) where T : class
        {
            try
            {
                lock (lockObj)
                {
                    using (FileStream fs = new FileStream(fileName, FileMode.Create))
                    {
                        new XmlSerializer(typeof(T)).Serialize(fs, obj);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// 泛型版本：XML反序列化从磁盘到内存对象
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T DeserializeFromXmlFile<T>(string fileName) where T : class
        {
            try
            {
                if (!File.Exists(fileName))
                    return null;
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    return new XmlSerializer(typeof(T)).Deserialize(fs) as T;
                }
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

        /// <summary>
        /// 创建一个应用了ClassCreatorAttribute、DateLastUpdatedAttribute的类型
        /// </summary>
        /// <returns></returns>
        
    }
}
