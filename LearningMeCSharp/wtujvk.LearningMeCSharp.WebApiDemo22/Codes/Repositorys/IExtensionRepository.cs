using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using wtujvk.LearningMeCSharp.WebApiDemo.Codes;

namespace Demo.LindAgile.IRepository
{
    /// <summary>
    /// 仓储扩展接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IExtensionRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// 根据lambda返回结果集
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        System.Linq.IQueryable<TEntity> GetQuery(Expression<Func<TEntity,bool>> predicate);
        /// <summary>
        /// 根据lambda返回实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity Find(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 插入对象
        /// </summary>
        /// <param name="item"></param>
        void Insert(IEnumerable<TEntity> item);
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="expression">条件</param>
        /// <param name="updateExpression">更新</param>
        void UpdateFromQuery(Expression<Func<TEntity,bool>>expression,Expression<Func<TEntity,TEntity>>updateExpression);
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="item"></param>
        void DeleteFromQuery(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IQueryable AsNoTacking(IQueryable<TEntity>query);
    }
}
