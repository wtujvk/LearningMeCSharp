using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using wtujvk.LearningMeCSharp.ToolStandard.Utils;

namespace wtujvk.LearningMeCSharp.ToolStandard
{
    /// <summary>
    /// 日志级别
    /// </summary>
    public enum Level
    {
        /// <summary>
        /// 所有日志，记录DEBUG|INFO|WARN|ERROR|FATAL级别的日志
        /// </summary>
        DEBUG,
        /// <summary>
        /// 记录INFO|WARN|ERROR|FATAL级别的日志
        /// </summary>
        INFO,
        /// <summary>
        /// 记录WARN|ERROR|FATAL级别的日志
        /// </summary>
        WARN,
        /// <summary>
        /// 记录ERROR|FATAL级别的日志
        /// </summary>
        ERROR,
        /// <summary>
        /// 记录FATAL级别的日志
        /// </summary>
        FATAL,
        /// <summary>
        /// 关闭日志功能
        /// </summary>
        OFF,
    }

    /// <summary>
    /// 日志核心基类
    /// 模版方法模式，对InputLogger开放，对其它日志逻辑隐藏，InputLogger可以有多种实现
    /// </summary>
    [Serializable]
    public abstract class LoggerBase : ILogger
    {
        /// <summary>
        /// 
        /// </summary>
        protected string _defaultLoggerName = DateTime.Now.ToString("yyyyMMdd");

        /// <summary>
        /// 日志文件地址
        /// 优化级为mvc方案地址，网站方案地址，console程序地址
        /// </summary>
        [ThreadStatic]
        static protected string FileUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LoggerDir");

        /// <summary>
        /// 日志持久化的方法，派生类必须要实现自己的方式
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        protected abstract void InputLogger(Level level, string message);

        #region ILogger 成员

        /// <summary>
        /// 占位符
        /// </summary>
        const int LEFTTAG = 7;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public virtual void Logger_Info(string message)
        {
            InputLogger(Level.INFO, message);
            Trace.WriteLine(message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public virtual void Logger_Error(Exception ex)
        {
            InputLogger(Level.ERROR, ex.Message);
            Trace.WriteLine(ex.Message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public virtual void Logger_Debug(string message)
        {
            InputLogger(Level.DEBUG, message);
            Trace.WriteLine(message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public virtual void Logger_Fatal(string message)
        {
            InputLogger(Level.FATAL, message);
            Trace.WriteLine(message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public virtual void Logger_Warn(string message)
        {
            InputLogger(Level.FATAL, message);
            Trace.WriteLine(message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ILogger SetPath(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                FileUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LoggerDir", path);
            }
             return this;
        }

        public virtual void Logger_Timer(string message,Action action,Level level=Level.DEBUG)
        {
            Logger_Timer(message, level, action);
        }
        internal virtual void Logger_Timer(string message, Level level,Action action)
        {
            if (message.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException("message参数不能为null");
            }
            StringBuilder str = new StringBuilder();
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            str.Append(message);
            action?.Invoke();
            str.Append("Logger_Timer:代码段运行时间(" + sw.ElapsedMilliseconds + "毫秒)");
            InputLogger(level,str.ToString());
            sw.Stop();
        }

        public void Logger_Exception(string message, Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                InputLogger(Level.ERROR,"Logger_Exception:" + message + "代码段出现异常,信息为" + ex.Message);
            }
        }
        #endregion
    }
}
