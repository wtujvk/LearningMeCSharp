using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wtujvk.LearningMeCSharp.WebApiDemo.Codes
{
    /// <summary>
    /// 
    /// </summary>
    public class MysqlDbContext:DbContext
    {
        public MysqlDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<LoginInfo> LoginInfo { get; set; }
    }
}
