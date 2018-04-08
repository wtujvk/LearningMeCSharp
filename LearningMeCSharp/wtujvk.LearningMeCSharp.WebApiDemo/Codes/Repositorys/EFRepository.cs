using Demo.LindAgile.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace wtujvk.LearningMeCSharp.WebApiDemo.Codes.Repositorys
{
    /// <summary>
    /// ef仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EFRepository<TEntity> : IExtensionRepository<TEntity> where TEntity : class, IEntity
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public EFRepository(DbContext db)
        {
            Db = db;
        }
        /// <summary>
        /// EF数据上下文
        /// </summary>
        protected DbContext Db;
        public IQueryable AsNoTacking(IQueryable<TEntity> query)
        {
           return query.AsNoTracking();
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="item"></param>
        public void Delete(TEntity item)
        {
            if (item != null)
            {
                //物理删除
                Db.Set<TEntity>().Attach(item as TEntity);
                Db.Entry(item).State = EntityState.Deleted;
                Db.Set<TEntity>().Remove(item as TEntity);
                this.SaveChanges();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        public void DeleteFromQuery(Expression<Func<TEntity, bool>> expression)
        {
             GetQuery(expression).DeleteFromQuery();
        }
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
           return Db.Set<TEntity>().AsNoTracking().FirstOrDefault(predicate);
        }
        /// <summary>
        /// 主键获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Find(params object[] id)
        {
            return Db.Set<TEntity>().Find(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate)
        {
            return Db.Set<TEntity>().Where(predicate);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetQuery()
        {
            return Db.Set<TEntity>();
        }
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="items"></param>
        public void Insert(IEnumerable<TEntity> items)
        {
            if (items != null)
            {
                items = items.Where(c => c != null);
                Db.AddRange(items);
            }
            else
            {
                throw new ArgumentException("items不能为null");
            }
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="item"></param>
        public void Insert(TEntity item)
        {
            if(item==null) throw new ArgumentNullException("item不能为null");
           
                Db.Entry(item as TEntity);
                Db.Set<TEntity>().Add(item as TEntity);
                this.SaveChanges();
                    
        }
        /// <summary>
        /// 设置上下文
        /// </summary>
        /// <param name="db"></param>
        public void SetDataContext(object db)
        {
            try
            {
                Db = db as DbContext;
            }
            catch (Exception)
            {
                throw new ArgumentException("EF.SetDataContext要求上下文为DbContext类型");
            }
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="item"></param>
        public void Update(TEntity item)
        {
            if (item != null)
            {
                Db.Set<TEntity>().Attach(item);
                Db.Entry(item).State = EntityState.Modified;
                this.SaveChanges();
            }
            throw new ArgumentNullException("参数不能为null");
        }

        /// <summary>
        /// 更新指定字段
        /// </summary>
        /// <param name="item"></param>
        /// <param name="properties"></param>
        public void Update(TEntity item, params Expression<Func<TEntity, object>>[] properties)
        {
            if (item != null)
            {
                Db.Set<TEntity>().Attach(item);
                foreach (var field in properties)
                    Db.Entry(item).Property(field).IsModified = true;
                this.SaveChanges();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="updateExpression"></param>
        public void UpdateFromQuery(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TEntity>> updateExpression)
        {
            GetQuery(expression).UpdateFromQuery(updateExpression);
        }
        /// <summary>
        /// 提交到数据库
        /// 有异常必须throw，否则会影响分布式事务的回滚失效
        /// </summary>
        public int SaveChanges()
        {
            try
            {
              return Db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                logger.Error(ex);
                throw new DbUpdateConcurrencyException("Lind.DDD框架在更新时引起了乐观并发，后修改的数据不会被保存",null);
            }
            //catch (DbEntityValidationException ex)
            //{
            //    List<string> errorMessages = new List<string>();
            //    foreach (DbEntityValidationResult validationResult in ex.EntityValidationErrors)
            //    {
            //        string entityName = validationResult.Entry.Entity.GetType().Name;
            //        foreach (DbValidationError error in validationResult.ValidationErrors)
            //        {
            //            errorMessages.Add(entityName + "." + error.PropertyName + ": " + error.ErrorMessage);
            //        }
            //    }
            //    logger.Error(ex);
            //    throw;
            //}
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }

        }
    }
    /// <summary>
    /// 扩展的仓储实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class MyEFREpository<TEntity> : EFRepository<TEntity> where TEntity : class, IEntity
    {
        public MyEFREpository(MysqlDbContext db):base(db)
        {

        }
    }
}
