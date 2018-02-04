using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using wtujvk.LearningMeCSharp.ToolStandard.Logger;
using wtujvk.LearningMeCSharp.ToolStandard.Utils;

namespace wtujvk.LearningMeCSharp.ToolStandard.Conf
{
    /// <summary>
    /// 配置信息实体
    /// </summary>
    public class ConfigModel
    {
        public ConfigModel()
        {
            //Logger = new Logger();
            //Redis = new Redis();
        }

        /// <summary>
        /// 启用属性变化跟踪
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public int PropertyChanged { get; set; }
        
        /// <summary>
        /// 日志相关
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public Logger Logger { get; set; }
        
        /// <summary>
        /// redis相关
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public Redis Redis { get; set; }
        /// <summary>
        /// Messaging消息相关
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public Messaging Messaging { get; set; }
        /// <summary>
        /// 使用SmtpClient发送邮件的配置
        /// </summary>
        /// 
        [XmlElement(Order =8)]
        public EmailMessage EmailMessage { get; set; }
        /// <summary>
        /// Ioc容器配置
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
        public IocContainer IocContaion { get; set; }

      

    }

    /// <summary>
    /// 缓存Caching(Redis,RunTime)
    /// </summary>
    public class Caching
    {
        #region 缓存Caching(Redis,RunTime)
        /// <summary>
        /// 缓存提供者:RuntimeCache,RedisCache
        /// </summary>
        [DisplayName("缓存提供者:RuntimeCache,RedisCache")]
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string Provider { get; set; }
        /// <summary>
        /// 缓存过期时间(minutes)
        /// </summary>
        [DisplayName("缓存过期时间(minutes)")]
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public int ExpireMinutes { get; set; }
        #endregion
    }
    /// <summary>
    /// Socket数据传递的配置
    /// </summary>
    public class Socket
    {
        #region Socket数据传递的配置
        /// <summary>
        /// Socket通讯地址
        /// </summary>
        [DisplayName("Socket通讯地址")]
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string ServerHost { get; set; }
        /// <summary>
        /// Socket数据传输的端口
        /// </summary>
        [DisplayName("Socket数据传输的端口")]
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public int DataPort { get; set; }
        /// <summary>
        /// Socket远程命令调用（RPC）的端口
        /// </summary>
        [DisplayName("Socket远程命令调用（RPC）的端口")]
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public int CommandPort { get; set; }

        #endregion
    }
  
    /// <summary>
    /// 日志相关
    /// 日志Logger(File,Log4net,MongoDB)
    /// </summary>
    public class Logger
    {
        private LoggerType loggerType;
        private Level level;
        #region 日志Logger(File,Log4net,MongoDB)
        /// <summary>
        /// 日志实现方式：File,Log4net,MongoDB
        /// </summary>
        [DisplayName("日志实现方式：File,Log4net,MongoDB")]
        [XmlIgnore]
        public string Type { get; set; }

        /// <summary>
        /// 日志实现方式：File(Normal),EmptyLogger
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public LoggerType LoggerType
        {
            get
            {
                loggerType = loggerType.GetEnumFromStr<LoggerType>(Type);
                return loggerType;
            }
            set
            {
                loggerType = value;
                Type = loggerType.ToString();
            }
        }

        [XmlIgnore]
        public string LevelStr { get; set; }

        /// <summary>
        /// 日志级别：DEBUG|INFO|WARN|ERROR|FATAL|OFF
        /// </summary>
        [DisplayName("日志级别：DEBUG|INFO|WARN|ERROR|FATAL|OFF")]
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public Level Level { get => level; set { level = value;LevelStr = level.ToString(); } }


      

        /// <summary>
        /// 日志记录的项目名称
        /// </summary>
        [DisplayName("日志记录的项目名称")]
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string ProjectName { get; set; }
     

        #endregion
    }

    /// <summary>
    /// 消息机制相关配置
    /// </summary>
    public class Messaging
    {
        #region 消息Messaging(Email,SMS,RTX)
       
        /// <summary>
        /// 消息机制－Email账号
        /// </summary>
        [DisplayName("消息机制－Email账号")]
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string Email_UserName { get; set; }
        /// <summary>
        /// 消息机制－Email登陆密码
        /// </summary>
        [DisplayName("消息机制－Email登陆密码")]
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string Email_Password { get; set; }
        /// <summary>
        /// 消息机制－Email主机头
        /// </summary>
        [DisplayName("消息机制－Email主机头")]
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string Email_Host { get; set; }
        /// <summary>
        /// 消息机制－Email端口
        /// </summary>
        [DisplayName("消息机制－Email端口")]
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public int Email_Port { get; set; }
        /// <summary>
        /// 消息机制-Email地址
        /// </summary>
        [DisplayName("消息机制-Email地址")]
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public string Email_Address { get; set; }
        /// <summary>
        /// 消息机制-Email显示的名称
        /// </summary>
        [DisplayName("消息机制-Email显示的名称")]
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public string Email_DisplayName { get; set; }
        /// <summary>
        /// 消息机制－Rtx-发送消息的Api
        /// </summary>
        [DisplayName("消息机制－Rtx-发送消息的Api")]
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public string RtxApi { get; set; }
        /// <summary>
        /// 消息机制－SMS－网关
        /// </summary>
        [DisplayName("消息机制－SMS－网关")]
        [System.Xml.Serialization.XmlElementAttribute(Order = 28)]
        public string SMSGateway { get; set; }
        /// <summary>
        /// 消息机制－SMS－加密方式
        /// </summary>
        [DisplayName("消息机制－SMS－加密方式")]
        [System.Xml.Serialization.XmlElementAttribute(Order = 29)]
        public string SMSSignType { get; set; }
        /// <summary>
        /// 消息机制－SMS－字符编码
        /// </summary>
        [DisplayName("消息机制－SMS－字符编码")]
        [System.Xml.Serialization.XmlElementAttribute(Order = 30)]
        public string SMSCharset { get; set; }
        /// <summary>
        /// 消息机制－SMS－短信密钥
        /// </summary>
        [DisplayName("消息机制－SMS－短信密钥")]
        [System.Xml.Serialization.XmlElementAttribute(Order = 31)]
        public string SMSKey { get; set; }
        /// <summary>
        /// 消息机制－SMS－项目ID
        /// </summary>
        [DisplayName("消息机制－SMS－项目ID")]
        [System.Xml.Serialization.XmlElementAttribute(Order = 32)]
        public int SMSItemID { get; set; }

        #endregion
    }
    /// <summary>
    /// 使用SmtpClient发送邮件
    /// </summary>
    public class EmailMessage
    {
        /// <summary>
        /// Email主机头-host 例如：smtp.163.com
        /// </summary>
        [DisplayName("Email主机头")]
        [XmlElement(Order = 1)]
        public string Email_Host { get; set; }
        /// <summary>
        /// Email端口-port
        /// </summary>
        [DisplayName("消息机制－Email端口")]
        [XmlElement(Order = 2)]
        public int Email_Port { get; set; }
        /// <summary>
        /// Email账号用户名 例如: feiyang
        /// The user name associated with the credentials
        /// </summary>
        [DisplayName("消息机制－Email用户名")]
        [XmlElementAttribute(Order =3)]
        public string Email_UserName { get; set; }
        /// <summary>
        /// Email授权码,邮箱客户端授权码 例如: 1234
        /// The password for the user name associated with the credentials
        /// </summary>
        [DisplayName("消息机制－Email登陆密码")]
        [XmlElement(Order = 4)]
        public string Email_Password { get; set; }
        /// <summary>
        /// Email地址 例如：你的邮箱号@163.com
        /// A System.String that contains an e-mail address
        /// </summary>
        [DisplayName("Email地址")]
        [XmlElement(Order = 5)]
        public string Email_Address { get; set; }
        /// <summary>
        /// Email显示给对方的名称 例如：xxx
        /// A System.String that contains the display name associated with address. This parameter can be null.
        /// </summary>
        [DisplayName("Email显示的名称")]
        [XmlElement(Order = 6)]
        public string Email_DisplayName { get; set; }
    }

    /// <summary>
    /// Redis相关配置
    /// </summary>
    public class Redis
    {
        #region Redis
        /// <summary>
        /// redis缓存的连接串
        /// var conn = ConnectionMultiplexer.Connect("contoso5.redis.cache.windows.net,password=...");
        /// </summary>
        [DisplayName("StackExchange.redis缓存的连接串")]
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string Host { get; set; }
        [DisplayName("StackExchange.redis代理模式（可选0:无，1：TW")]
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public int Proxy { get; set; }
        #endregion
    }
  
    /// <summary>
    /// IOC容器相关
    /// </summary>
    public class IocContainer
    {
        /// <summary>
        /// 容器类型，0:unity,1:autofac
        /// 需要在config中配置对象的容器声明
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public int IoCType { get; set; }
        /// 数据集缓存策略：EntLib,Redis
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string AoP_CacheStrategy { get; set; }
    }
    
}
