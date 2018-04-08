using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace wtujvk.LearningMeCSharp.WebApiDemo.Codes
{
    /// <summary>
    /// 
    /// </summary>
    public static class EFExtend
    {
        #region 同步
        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="Tentity"></typeparam>
        /// <param name="query"></param>
        /// <param name="updateFactory"></param>
        /// <returns>The number of rows affected</returns>
        public static int UpdateFromQuery<Tentity>(this IQueryable<Tentity> query, Expression<Func<Tentity, Tentity>> updateFactory)
            where Tentity : class, IEntity
        {
            return BatchUpdateExtensions.Update(query, updateFactory);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="Tentity"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static int DeleteFromQuery<Tentity>(this IQueryable<Tentity> query) where Tentity : class, IEntity
        {
            return BatchDeleteExtensions.Delete(query);
        }
        #endregion
        #region 异步
        /// <summary>
        /// 修改-异步
        /// </summary>
        /// <typeparam name="Tentity"></typeparam>
        /// <param name="query"></param>
        /// <param name="updateFactory"></param>
        /// <returns></returns>
        public static async Task<int> UpdateFromQueryAsync<Tentity>(this IQueryable<Tentity> query, Expression<Func<Tentity, Tentity>> updateFactory)
            where Tentity : class, IEntity
        {
            return await BatchUpdateExtensions.UpdateAsync(query, updateFactory);
        }
        /// <summary>
        /// 删除-异步
        /// </summary>
        /// <typeparam name="Tentity"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static async Task<int> DeleteFromQueryAsync<Tentity>(this IQueryable<Tentity> query) where Tentity : class, IEntity
        {
            return await BatchDeleteExtensions.DeleteAsync(query);
        }
        #endregion
    }
}
