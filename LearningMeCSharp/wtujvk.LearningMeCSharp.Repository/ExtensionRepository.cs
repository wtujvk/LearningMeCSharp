
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using wtujvk.LearningMeCSharp.Entities;
using wtujvk.LearningMeCSharp.IRepository;
using wtujvk.LearningMeCSharp.ToolStandard;
using Z.EntityFramework.Plus;

namespace wtujvk.LearningMeCSharp.Repository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExtensionRepository<T>: IExtensionRepository<T> where T:class,IEntity 
    {
        #region Constructors
        ///// <summary>
        ///// 这个在IoC注入时走它
        ///// </summary>
        //public ExtensionRepository() : this(null)
        //{

        //}

        public ExtensionRepository(IDbContext db)
        {
            Db = db;
            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<MySqlDbContext, Configuration>());
        }
        /// <summary>
        /// EF数据上下文
        /// </summary>
        private IDbContext Db;

        public void Insert(T entity, bool savechages = true)
        {
            Db.Set<T>().Add(entity as T);
            if (savechages) {
                Db.SaveChanges();
            }
        }

        public void Delete(Expression<Func<T, bool>> exp)
        {
            Db.Set<T>().Where(exp).Delete();
        }

        public void BulkInsert(IEnumerable<T> lst)
        {
            Db.Set<T>().AddRange(lst);
        }
        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="update"></param>
        public void UpdateFromQurey(Expression<Func<T, bool>> exp, Expression<Func<T, T>> update)
        {
            Db.Set<T>().Where(exp).Update(update);
        }
        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public IQueryable<T> GetQuery(Expression<Func<T, bool>> exp)
        {
           return Db.Set<T>().Where(exp);
        }

        public void RunTranscation(Action action)
        {
            var database= Db.GetDatabase();
            using(var tran = database.BeginTransaction())
            {
                try
                {
                    action?.Invoke();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                  LoggerFactory.Instance.Logger_Error(ex);
                }
            }
        }

        public async Task<IQueryable<T>> GetQueryAsync(Expression<Func<T, bool>> exp)
        {
            var q= Db.Set<T>().Where(exp);
            return await Task.FromResult(q);
        }
        #endregion

    }
}
