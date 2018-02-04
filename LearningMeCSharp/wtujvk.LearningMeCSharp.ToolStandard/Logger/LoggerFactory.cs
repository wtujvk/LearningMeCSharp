using System;
using System.Collections.Generic;
using System.Text;
using wtujvk.LearningMeCSharp.ToolStandard.Conf;
using wtujvk.LearningMeCSharp.ToolStandard.Logger;
using wtujvk.LearningMeCSharp.ToolStandard.Logger.Implements;

namespace wtujvk.LearningMeCSharp.ToolStandard
{
    /// <summary>
    /// 日志生产类
    /// Singleton模式和策略模式和工厂模式
    /// </summary>
    public sealed class LoggerFactory : ILogger
    {
        #region Logger有多种实现时,需要使用Singleton模式
        /// <summary>
        /// 对外不能创建类的实例
        /// </summary>
        private LoggerFactory()
        {
            var logtype = LoggerType.NormalLogger;
            if (ConfigManager.Config != null)
                logtype = ConfigManager.Config.Logger.LoggerType;

            switch (logtype)
            {
                case LoggerType.NormalLogger:
                    iLogger = new NormalLogger();
                    break;
                case LoggerType.EmptyLogger:
                    iLogger = new EmptyLogger();
                    break;
                default:
                    iLogger = new EmptyLogger();
                    break;
            }

        }

        /// <summary>
        /// 日志级别
        /// </summary>
        private static Level level = ConfigManager.Config.Logger==null?Level.DEBUG:ConfigManager.Config.Logger.Level;
        /// <summary>
        /// 线程锁
        /// </summary>
        private static object lockObj = new object();
        /// <summary>
        /// 日志工厂
        /// </summary>
        private static LoggerFactory instance;
        /// <summary>
        /// 日志提供者，只在本类中实例化
        /// </summary>
        private ILogger iLogger;
        /// <summary>
        /// 单例模式的日志工厂对象
        /// </summary>
        public static LoggerFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new LoggerFactory();
                        }
                    }
                }
                return instance;
            }
        }

        #endregion

        #region ILogger 成员
        /// <summary>
        /// 记录代码段执行时间
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="action">要执行的代码段</param>
        public void Logger_Timer(string message, Action action)
        {
            iLogger.Logger_Timer(message, action);
        }
        /// <summary>
        /// 记录代码段执行时出现的异常信息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="action">要执行的代码段</param>
        public void Logger_Exception(string message, Action action)
        {
            iLogger.Logger_Exception(message, action);
        }
        /// <summary>
        /// Debug级别的日志
        /// </summary>
        /// <param name="message"></param>
        public void Logger_Debug(string message)
        {
            if (level <= Level.DEBUG)
                iLogger.Logger_Debug(message);
        }
        /// <summary>
        /// Info级别的日志
        /// </summary>
        /// <param name="message"></param>
        public void Logger_Info(string message)
        {
            if (level <= Level.INFO)
                iLogger.Logger_Info(message);
        }
        /// <summary>
        /// Warn级别的日志
        /// </summary>
        /// <param name="message"></param>
        public void Logger_Warn(string message)
        {
            if (level <= Level.WARN)
                iLogger.Logger_Warn(message);
        }
        /// <summary>
        /// Error级别的日志
        /// </summary>
        /// <param name="ex"></param>
        public void Logger_Error(Exception ex)
        {
            if (level <= Level.ERROR)
                iLogger.Logger_Error(ex);
        }
        /// <summary>
        /// Fatal级别的日志
        /// </summary>
        /// <param name="message"></param>
        public void Logger_Fatal(string message)
        {
            if (level <= Level.FATAL)
                iLogger.Logger_Fatal(message);
        }

        public void Logger_Timer(string message, Action action, Level level = Level.DEBUG)
        {
            iLogger.Logger_Timer(message, action, level);
        }
        #endregion
    }
}
