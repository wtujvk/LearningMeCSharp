using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace wtujvk.LearningMeCSharp.IRepository
{
    /// <summary>
    /// dbcontext抽象接口
    /// </summary>
    public interface IDbContext
    {
        ////获取dbset
        DbSet<T> Set<T>() where T : class;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        DatabaseFacade GetDatabase();
    }
}
