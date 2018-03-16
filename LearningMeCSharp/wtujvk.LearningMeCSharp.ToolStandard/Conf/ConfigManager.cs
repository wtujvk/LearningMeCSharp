using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using wtujvk.LearningMeCSharp.ToolStandard.Logger;
using wtujvk.LearningMeCSharp.ToolStandard.Messages;
using wtujvk.LearningMeCSharp.ToolStandard.Utils;

namespace wtujvk.LearningMeCSharp.ToolStandard.Conf
{
    /// <summary>
    /// 框架级配置信息初始化，默认使用xml进行存储
    /// </summary>
    public class ConfigManager
    {
        /// <summary>
        /// 网站基础数据目录
        /// </summary>
        public static string AppBaseDirctory = AppDomain.CurrentDomain.BaseDirectory;

        #region Constructors & Fields
        private static ConfigModel _config;
        private static string _fileName = string.Empty;// Path.Combine(AppBaseDirctory, "ConfigConstants.xml");
        private static object _lockObj = new object();

        static ConfigManager()
        {
            // _fileName = Path.Combine(AppBaseDirctory, "ConfigConstants.xml");


            ////_init.Logger.Level = Level.DEBUG;
            ////_init.Logger.ProjectName = "wtujvk.LearningMeCSharp";
            ////_init.Logger.Type = "File";
            //_init.Redis.Host = "localhost:6379";
            //_init.Redis.Proxy = 0;
            //_init.IocContaion.IoCType = 0;
            _init = new ConfigModel()
            {
                Logger = new Logger()
                {
                    Level = Level.DEBUG,
                    ProjectName = "wtujvk.LearningMeCSharp",
                    LoggerType = LoggerType.NormalLogger,
                    LevelStr = Level.DEBUG.ToString(),
                    Type = LoggerType.NormalLogger.ToString()
                },
                Redis = new Redis()
                {
                    Host = "localhost:6379", Proxy = 0
                },
                IocContaion = new IocContainer()
                {
                    IoCType = 0, AoP_CacheStrategy = ""
                },
                Messaging = new Messaging()
                {
                    Email_Address = "*",
                    Email_DisplayName = "*",
                    Email_Host = "*",
                    Email_Password = "*",
                    Email_UserName = "*",
                    RtxApi = "*",
                    SMSCharset = "*",
                    SMSGateway = "*",
                    SMSKey = "*",
                    SMSSignType = MessageType.Email.ToString()
                },
                EmailMessage = new EmailMessage()
                {
                    Email_Address = "wtujvk110@163.com",
                    Email_Host = "smtp.163.com",
                    Email_Port = 25,
                    Email_UserName = "wtujvk110@163.com",
                    Email_Password = "wtujvk110",
                    Email_DisplayName = "赵小黑"
                },
                PropertyChanged = 0
            };
        }
        //ConfigManager()
        //{
        //    _fileName = Path.Combine(AppBaseDirctory, "ConfigConstants.xml");
        //}
        /// <summary>
        /// 模型初始化
        /// </summary>
        private static ConfigModel _init;
        #endregion

        /// <summary>
        /// 配置字典,单例模式
        /// </summary>
        /// <returns></returns>
        public static ConfigModel Config
        {
            get
            {
                if (_fileName.IsNullOrWhiteSpace())
                {
                    _fileName = Path.Combine(AppBaseDirctory, "ConfigConstants.xml");
                }
                if (_config == null)
                {
                    lock (_lockObj)
                    {
                        XmlElement xml = null;
                        ConfigModel old = null;
                        if (File.Exists(_fileName))
                        {
                            old = SerializationHelper.DeserializeFromXml<ConfigModel>(_fileName);
                            if (old != null)
                            {
                                var configXml = new XmlDocument();
                                configXml.Load(_fileName);
                                xml = configXml.DocumentElement;
                            }
                        }

                        if (old == null || xml.ChildNodes.Count != typeof(ConfigModel).GetProperties().Count())
                        {
                            SerializationHelper.SerializeToXmlFile(_fileName, _init);
                            _config = _init;
                        }
                        else
                        {
                            _config = old;
                        }
                    }

                }
                return _config;
            }
        }
    }
}

