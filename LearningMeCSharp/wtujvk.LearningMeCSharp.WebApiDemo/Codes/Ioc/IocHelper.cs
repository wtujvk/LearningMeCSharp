using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace wtujvk.LearningMeCSharp.WebApiDemo.Codes.Ioc
{
   
    /// <summary>
    /// ioc帮助类
    /// </summary>
    public class IocHelper
    {
        private static readonly UnityContainer _container;
        static IocHelper()
        {
            _container = new UnityContainer();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInterface">接口</typeparam>
        /// <typeparam name="TImplementation">实现</typeparam>
        /// <param name="life">Instance实例对象每次不同，Singleton每次相同</param>
        public static void Register<TInterface, TImplementation>(LifeCycle life = LifeCycle.Singleton) where TImplementation : TInterface
        {
            LifetimeManager lifetimeManager = (life == LifeCycle.Singleton) ? (LifetimeManager)new ContainerControlledLifetimeManager() : new TransientLifetimeManager();
            _container.RegisterType<TInterface, TImplementation>(lifetimeManager);
        }
        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="service"></param>
        /// <param name="implement"></param>
        /// <param name="life"></param>
        public static void Register(Type service, Type implement, LifeCycle life = LifeCycle.Singleton)
        {
            LifetimeManager lifetimeManager = (life == LifeCycle.Singleton) ? (LifetimeManager)new ContainerControlledLifetimeManager() : new TransientLifetimeManager();
            _container.RegisterType(service, implement, lifetimeManager);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsRegistered<T>()
        {
            return _container.IsRegistered<T>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeToCheck"></param>
        /// <returns></returns>
        public static bool IsRegistered(Type typeToCheck)
        {
            return _container.IsRegistered(typeToCheck); 
        }
        public static T Get<T>()
        {
            return _container.Resolve<T>();
        }

        static void CheckTypeCanRegister(Type type)
        {
            if (type.IsAbstract || type.IsInterface)
            {

            }
        }
    }

    /// <summary>
    /// 生命周期
    /// </summary>
    public enum LifeCycle
    {
        /// <summary>
        /// 实例对象-TransientLifetimeManager(瞬态生命周期)
        /// </summary>
        Instance,
        /// <summary>
        /// 单例对象-ExternallyControlledLifetimeManager 
        /// 在默认情况下，使用这个生命周期管理器，每次调用Resolve都会返回同一对象（单件实例），如果被GC回收后再次调用Resolve方法将会重新创建新的对象
        /// </summary>
        Singleton
    }
}
