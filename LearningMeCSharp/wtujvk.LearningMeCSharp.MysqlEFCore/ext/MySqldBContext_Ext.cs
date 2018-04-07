using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
//using wtujvk.LearningMeCSharp.Interafces;
using wtujvk.LearningMeCSharp.IRepository;
using wtujvk.LearningMeCSharp.MysqlEFCore.ext;

namespace wtujvk.LearningMeCSharp.MysqlEFCore
{
    public partial class MySqldBContext : IDbContext
    {
        public DatabaseFacade GetDatabase()
        {
            return this.Database;
        }
    }
}
