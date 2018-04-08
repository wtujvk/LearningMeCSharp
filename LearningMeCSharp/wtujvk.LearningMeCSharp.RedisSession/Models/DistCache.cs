using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace wtujvk.LearningMeCSharp.RedisSession.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class DistCache
    {
        [Key]
        [MaxLength(900)]
        public string Id { get; set; }

        public string Value { get; set; }

        public DateTime ExpiresAtTime { get; set; }

        public DateTimeOffset AbsoluteExpiration { get; set; }
    }
}
