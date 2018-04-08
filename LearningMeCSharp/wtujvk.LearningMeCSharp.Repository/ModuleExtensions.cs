using System;
using System.Collections.Generic;
using System.Text;
using wtujvk.LearningMeCSharp.IRepository;
using wtujvk.LearningMeCSharp.Repository;

namespace wtujvk.LearningMeCSharp.ToolStandard.Modules
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class ModuleExtensions
    {
        /// <summary>
        /// IExtensionRepository 泛型仓储
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static ModuleManager UseExtensionRepository(this ModuleManager configuration)
        {
            configuration.RegisterGenericModule(typeof(IExtensionRepository<>), typeof(ExtensionRepository<>));
            return configuration;
        }
    }
}
