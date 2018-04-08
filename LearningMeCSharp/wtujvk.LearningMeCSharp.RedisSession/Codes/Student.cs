using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wtujvk.LearningMeCSharp.RedisSession.Codes
{
    /// <summary>
    /// 学生
    /// </summary>
    public class Student
    {
        public Student()
        {
            Id = GetHashCode();
        }
        public int Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 分数
        /// </summary>
        public UInt16 Score { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime RecodeTime { get; set; } = DateTime.Now;
    }
}
