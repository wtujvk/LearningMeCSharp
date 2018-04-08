using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wtujvk.LearningMeCSharp.MvcDemo.Models
{
    /// <summary>
    /// 人
    /// </summary>
    [Serializable]
    public class Person
    {
        public Person()
        {
            Id = GetHashCode();
        }
        public int Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public UInt16 Age { get; set; }
        /// <summary>
        /// 创建的时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}