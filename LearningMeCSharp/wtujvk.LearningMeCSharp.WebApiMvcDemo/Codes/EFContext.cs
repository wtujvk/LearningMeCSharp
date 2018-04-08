using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace wtujvk.LearningMeCSharp.WebApiMvcDemo.Codes
{
    public class EFContext:DbContext
    {

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                //Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
               .SelectMany(x => x.ValidationErrors)
               .Select(x => x.ErrorMessage);
                //Join the list to a single string.
                var fullErrorMessage = string.Join(";", errorMessages);
                //Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are:", fullErrorMessage);
                //Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }
    }

    public class Repos
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public Repos(DbContext db)
        {

        }
        DbContext Db;
        /// <summary>
        /// 提交到数据库
        /// 有异常必须throw，否则会影响分布式事务的回滚失效
        /// </summary>
        protected virtual void SaveChanges()
        {
            try
            {
                Db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                logger.Error(ex);
                throw new DbUpdateConcurrencyException("Lind.DDD框架在更新时引起了乐观并发，后修改的数据不会被保存");
            }
            catch (DbEntityValidationException ex)
            {
                List<string> errorMessages = new List<string>();
                foreach (DbEntityValidationResult validationResult in ex.EntityValidationErrors)
                {
                    string entityName = validationResult.Entry.Entity.GetType().Name;
                    foreach (DbValidationError error in validationResult.ValidationErrors)
                    {
                        errorMessages.Add(entityName + "." + error.PropertyName + ": " + error.ErrorMessage);
                    }
                }
                logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw;
            }

        }

    }
}