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
    /// 基本仓储操作接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class,IEntity
    {
        /// <summary>
        /// 通过主键拿一个对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Find(params object[] id);
        /// <summary>
        /// 拿到可查询结果集
        /// </summary>
        /// <returns></returns>
        System.Linq.IQueryable<TEntity> GetQuery();
        /// <summary>
        /// 设置当前数据库上下文对象，与具体ORM无关，上下文应该继承Lind.DDD.UnitOfWork.IDataContext
        /// </summary>
        void SetDataContext(object db);
        /// <summary>
        /// 插入对象
        /// </summary>
        /// <param name="item"></param>
        void Insert(TEntity item);
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="item"></param>
        void Update(TEntity item);
        /// <summary>
        /// 更新部分字段
        /// </summary>
        /// <param name="item"></param>
        /// <param name="fields"></param>
        void Update(TEntity item, params Expression<Func<TEntity, object>>[] properties);
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="item"></param>
        void Delete(TEntity item);
        /// <summary>
        /// 保存到数据库
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}
