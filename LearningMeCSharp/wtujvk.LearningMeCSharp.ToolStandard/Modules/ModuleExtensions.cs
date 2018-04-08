using System;
using System.Collections.Generic;
using System.Text;
using wtujvk.LearningMeCSharp.ToolStandard.Ioc;
using wtujvk.LearningMeCSharp.ToolStandard.Logger.Implements;
using wtujvk.LearningMeCSharp.ToolStandard.Utils.Adapter;
using wtujvk.LearningMeCSharp.ToolStandard.Utils.SerializingObject;

namespace wtujvk.LearningMeCSharp.ToolStandard.Modules
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class ModuleExtensions
    {
        #region 容器和插件
        /// <summary>
        /// 注册一个默认的容器，接口IContainer，用来存储对象与接口的映射关系
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static ModuleManager UseDefaultContainer(this ModuleManager configuration)
        {
            configuration.SetContainer(new DefaultContainer());
            return configuration;
        }

        ///// <summary>
        ///// 注册一个autofac容器，接口IContainer
        ///// </summary>
        ///// <param name="configuration"></param>
        ///// <returns></returns>
        //public static ModuleManager UseAutofac(this ModuleManager configuration)
        //{
        //    configuration.SetContainer(new AutofacContainer());
        //    return configuration;
        //}

        /// <summary>
        /// 注册一个unity容器，接口IContainer
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static ModuleManager UseUnity(this ModuleManager configuration)
        {
            configuration.SetContainer(new UnityAdapterContainer());
            return configuration;
        }

        #endregion

        #region 日志
        /// <summary>
        /// 注册一个Lind框架的日志组件，接口ILogger 文本日志
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static ModuleManager UseNormalLogger(this ModuleManager configuration)
        {
            configuration.RegisterModule<ILogger, NormalLogger>();
            return configuration;
        }
        #endregion
        /// <summary>
        /// 使用第三方Newtonsoft实现json序列化
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static ModuleManager UseNewtonsoftSerializer(this ModuleManager configuration)
        {
            configuration.RegisterModule<IObjectSerializer, NewtonsoftSerializer>();
            return configuration;
        }
        ///// <summary>
        ///// 使用RedisCache缓存
        ///// </summary>
        ///// <param name="configuration"></param>
        ///// <returns></returns>
        //public static ModuleManager UseRedisCache(this ModuleManager configuration)
        //{
        //    configuration.RegisterModule<ICache, RedisCache>();
        //    return configuration;
        //}
    }
}
