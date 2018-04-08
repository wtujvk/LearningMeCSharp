using System;
using System.Collections.Generic;
using System.Text;
using wtujvk.LearningMeCSharp.IRepository;
using wtujvk.LearningMeCSharp.MysqlEFCore;
using wtujvk.LearningMeCSharp.ToolStandard.Ioc;

namespace wtujvk.LearningMeCSharp.ToolStandard.Modules
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class ModuleExtensions
    {
        /// <summary>
        /// 注册一个ef上下文
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static ModuleManager UseMySqldBContext(this ModuleManager configuration)
        {
            configuration.RegisterModule<IDbContext, MySqldBContext>(LifeCycle.Instance);
            return configuration;
        }

    }
}
