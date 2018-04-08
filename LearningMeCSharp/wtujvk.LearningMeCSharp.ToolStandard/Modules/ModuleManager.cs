using System;
using System.Collections.Generic;
using System.Text;
using wtujvk.LearningMeCSharp.ToolStandard.Ioc;

namespace wtujvk.LearningMeCSharp.ToolStandard
{
    /// <summary>
    /// 设计一个模块化注册机制
    /// 模块和插件不同，前者是具体组件的概念，如ioc,aop,缓存,日志这些是模块；而插件一般指一种接口对应的一种实现，它包括了模块化的设计， 
    /// 模块都在插件库里存在，并且它还包括了其它的多态化组件
    /// </summary>
    public partial class ModuleManager
    {
        #region 模块初始化
        /// <summary>
        /// 当前模块对象
        /// </summary>
        static ModuleManager instance;
        /// <summary>
        /// 当前容器对象
        /// </summary>
        static ILindContainer currentContainer;
        private ModuleManager() { }
        /// <summary>
        /// 构建模块 & 注册容器
        /// </summary>
        /// <returns></returns>
        public static ModuleManager Create()
        {
            instance = new ModuleManager();
            return instance;
        }
        #endregion

        #region 设置容器
        /// <summary>
        /// 设置容器
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public ModuleManager SetContainer(ILindContainer container)
        {
            currentContainer = container;
            return this;
        }
        #endregion

        #region 注册模块到容器

        /// <summary>
        /// 注册普通类型
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplement"></typeparam>
        /// <param name="lifeCycle"></param>
        /// <returns></returns>
        public ModuleManager RegisterModule<TService, TImplement>()
         where TService : class
         where TImplement : class, TService
        {
            return RegisterModule<TService, TImplement>(null, LifeCycle.Singleton);
        }
        /// <summary>
        /// 注册普通类型
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplement"></typeparam>
        /// <param name="serviceName"></param>
        /// <param name="lifeCycle"></param>
        /// <returns></returns>
        public ModuleManager RegisterModule<TService, TImplement>(LifeCycle lifeCycle)
           where TService : class
           where TImplement : class, TService
        {
            return RegisterModule<TService, TImplement>(null, lifeCycle);
        }
        /// <summary>
        /// 注册普通类型
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplement"></typeparam>
        /// <returns></returns>
        public ModuleManager RegisterModule<TService, TImplement>(string serviceName, LifeCycle lifeCycle)
            where TService : class
            where TImplement : class, TService
        {
            currentContainer.Register<TService, TImplement>(serviceName, lifeCycle);
            return this;
        }

        /// <summary>
        /// 注册泛型
        /// </summary>
        /// <param name="t"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public ModuleManager RegisterGenericModule(Type t, Type m, LifeCycle lifeCycle = LifeCycle.Singleton)
        {
            currentContainer.RegisterGeneric(t, m, lifeCycle);
            return this;
        }


        #endregion


        #region 使用模块从容器
        /// <summary>
        /// 从容器中取了对象
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static TService Resolve<TService>() where TService : class
        {
            return currentContainer.Resolve<TService>();
        }
        /// <summary>
        /// 从容器中取了对象
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static TService Resolve<TService>(params object[] param) where TService : class
        {
            return currentContainer.Resolve<TService>(param);
        }
        /// <summary>
        /// 从容器中取了对象
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static object Resolve(Type service)
        {
            return currentContainer.Resolve(service);
        }
        /// <summary>
        /// 从容器中取了对象
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static object Resolve(Type service, params object[] param)
        {
            return currentContainer.Resolve(service, param);
        }
        /// <summary>
        /// 是否注册了这个类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsRegistered(Type type)
        {
            return currentContainer.IsRegistered(type);
        }
        /// <summary>
        /// 是否注册了这个类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsRegistered<T>()
        {
            return currentContainer.IsRegistered<T>();
        }

        #endregion
    }
}
