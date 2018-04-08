using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace wtujvk.LearningMeCSharp.WebApiDemo.Codes
{
    /// <summary>
    /// 日志信息
    /// </summary>
    public class LoginInfo:IEntity
    {
        public LoginInfo()
        {
            var ex = new Exception();
            var msg = $"{ex.Message}{ex.Source}{ex.TargetSite}{ex.HelpLink}{ex.StackTrace}";
        }
        [DatabaseGeneratedAttribute( DatabaseGeneratedOption.Identity)]
        [Key]
         public long Id { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
         [MaxLength(100)]
         public string AppName { get; set; }
        /// <summary>
        /// key
        /// </summary>
         [MaxLength(200)]
         public string AppKey { get; set; }
         public DateTime AddTime { get; set; }
        /// <summary>
        /// 线程id
        /// </summary>
         public int ThreadId { get; set; }
        /// <summary>
        /// 日志级别
        /// </summary>
         [MaxLength(50)]
         public string LogLevel { get; set; }
        /// <summary>
        /// 日志信息
        /// </summary>
        [MaxLength(500)]
        public string Message { get; set; }
        /// <summary>
        /// 是否含有内部
        /// </summary>
        public bool HasInnerException { get; set; }
        /// <summary>
        /// 
        /// Gets or sets the name of the application or the object that causes the error
        /// The name of the application or the object that causes the error
        /// </summary>
        [MaxLength(500)]
        public string ExSource { get; set; }
        /// <summary>
        ///  Gets the method that throws the current exception
        /// </summary>
        [MaxLength(500)]
        public string ExTargetSite { get; set; }
        /// <summary>
        /// 异常堆栈信息
        /// A string that describes the immediate frames of the call stack
        /// </summary>
        [MaxLength(8000)]
        public string StackTrace { get; set; }
        /// <summary>
        /// 异常类型名称
        /// </summary>
        [MaxLength(500)]
        public string ExceptionName { get; set; }

    }
}
