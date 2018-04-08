using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace wtujvk.LearningMeCSharp.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IExtensionRepository<T> where T : class
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="savechages">是否马上savechages</param>
        void Insert(T entity,bool savechages=true);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="exp"></param>
        void Delete(Expression<Func<T, bool>> exp);
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="lst"></param>
        void BulkInsert(IEnumerable<T> lst);
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="update"></param>
        void UpdateFromQurey(Expression<Func<T, bool>> exp, Expression<Func<T, T>> update);
        /// <summary>
        /// 获取查询表达式
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        IQueryable<T> GetQuery(Expression<Func<T, bool>> exp);

        Task<IQueryable<T>> GetQueryAsync(Expression<Func<T, bool>> exp);
    }
}
