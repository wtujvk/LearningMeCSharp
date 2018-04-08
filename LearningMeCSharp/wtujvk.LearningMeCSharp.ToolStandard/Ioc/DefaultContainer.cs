using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace wtujvk.LearningMeCSharp.ToolStandard.Ioc
{
    /// <summary>
    /// 默认的容器
    /// 功能：提供对模块的注册和生产
    /// </summary>
    public class DefaultContainer : ILindContainer
    {
        static ConcurrentDictionary<string, Type> containerList = new ConcurrentDictionary<string, Type>();

        #region 注册组件
        /// <summary>
        /// 注册泛型对象
        /// </summary>
        /// <param name="service"></param>
        /// <param name="implement"></param>
        /// <param name="life"></param>
        public void RegisterGeneric(Type service, Type implement, LifeCycle life = LifeCycle.Singleton)
        {
            containerList.TryAdd(service.Name, implement);
        }
        /// <summary>
        /// 注册标准对象
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementer"></typeparam>
        /// <param name="serviceName"></param>
        /// <param name="life"></param>
        public void Register<TService, TImplementer>(string serviceName = null, LifeCycle life = LifeCycle.Singleton)
                where TService : class
                where TImplementer : class, TService
        {
            containerList.TryAdd(typeof(TService).Name, typeof(TImplementer));
        }
        #endregion

        #region 生产组件
        /// <summary>
        /// 生产普通对象
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public TService Resolve<TService>() where TService : class
        {
            Type obj;
            containerList.TryGetValue(typeof(TService).Name, out obj);
            if (obj.ContainsGenericParameters)
            {
                //泛型
                return (TService)Activator.CreateInstance(obj.MakeGenericType(typeof(TService).GetGenericArguments()));
            }
            else
            {
                //普通类型
                return (TService)Activator.CreateInstance(obj);
            }

        }
        /// <summary>
        /// 生产对象，对象的构造方法可以带参数
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        public TService Resolve<TService>(params object[] param) where TService : class
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 生产对象，通过显示的名称
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public TService ResolveNamed<TService>(string serviceName) where TService : class
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 生产对象－通过显示和名称和参数
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="serviceName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public TService ResolveNamed<TService>(string serviceName, params object[] param) where TService : class
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 生产对象
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public object Resolve(Type service)
        {
            return Resolve(service, null);
        }
        /// <summary>
        /// 生产对象-带构造方法的参数
        /// </summary>
        /// <param name="service"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public object Resolve(Type service, params object[] param)
        {
            Type obj;
            containerList.TryGetValue(service.GetType().Name, out obj);
            if (obj.ContainsGenericParameters)
            {
                //泛型
                return Activator.CreateInstance(obj.MakeGenericType(service.GetType().GetGenericArguments()));
            }
            else
            {
                //普通类型
                return Activator.CreateInstance(obj);
            }
        }

        public void Register(Type service, Type implement, string serviceName = null, LifeCycle life = LifeCycle.Singleton)
        {
            throw new NotImplementedException();
        }

        public bool IsRegistered(Type type)
        {
            return containerList.ContainsKey(type.Name);
        }

        public bool IsRegistered<T>()
        {
            return containerList.ContainsKey(typeof(T).FullName);
        }
        /// <summary>
        /// 接口和抽象类不能作为输出对象，注入的时候要注意
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsCanBeResolve(Type type)
        {
            if (type.IsInterface || type.IsAbstract)
            {
                return false;
            }
            return true;
        }

        public bool IsCanBeResolve<T>()
        {
            var t = typeof(T);
            return IsCanBeResolve(t);
        }
        #endregion

    }
}
