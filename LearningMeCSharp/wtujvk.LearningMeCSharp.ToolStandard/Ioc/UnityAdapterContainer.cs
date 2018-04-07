using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity;
using Unity.Lifetime;
using Unity.Registration;
using Unity.Resolution;
using wtujvk.LearningMeCSharp.ToolStandard.Utils;

namespace wtujvk.LearningMeCSharp.ToolStandard.Ioc
{
    public class UnityAdapterContainer : UnityContainer, ILindContainer
    {
        public bool IsRegistered(Type type)
        {
            return UnityContainerExtensions.IsRegistered(this, type);
        }

        public bool IsRegistered<T>()
        {
            return UnityContainerExtensions.IsRegistered<T>(this);
        }

        public void Register(Type service, Type implement, string serviceName = null, LifeCycle life = LifeCycle.Singleton)
        {
            CheckType(implement);
            if (life == LifeCycle.Singleton)
            {
                if (serviceName.IsVisable())
                {
                    UnityContainerExtensions.RegisterSingleton(this, service, implement, serviceName);
                }
                else
                {
                    UnityContainerExtensions.RegisterSingleton(this, service, implement);
                }
            }
            else
            {
                LifetimeManager lifeTimeManager = new TransientLifetimeManager();
                UnityContainerExtensions.RegisterType(this, service, implement, lifeTimeManager);
            }

        }

        public void RegisterGeneric(Type service, Type implement, LifeCycle life = LifeCycle.Singleton)
        {
            CheckType(implement);
            LifetimeManager lifeTimeManager = new ExternallyControlledLifetimeManager();
            if (life == LifeCycle.Instance)
            {
                lifeTimeManager = new TransientLifetimeManager();
            }
            UnityContainerExtensions.RegisterType(this, service, implement, lifeTimeManager);
            
        }

        public TService Resolve<TService>() where TService : class
        {
            return UnityContainerExtensions.Resolve<TService>(this);
        }

        public object Resolve(Type service)
        {
            return UnityContainerExtensions.Resolve(this, service);
        }

        public object Resolve(Type service, params object[] param)
        {
            var overrides = Array.ConvertAll(param, i => new ParameterOverride(i.GetType().FullName, i) { });
            return UnityContainerExtensions.Resolve(this, service, overrides);
        }

        public TService Resolve<TService>(params object[] param) where TService : class
        {
            return UnityContainerExtensions.Resolve<TService>(this, Array.ConvertAll(param, i => new ParameterOverride(i.GetType().FullName, i) { }));
        }

        public TService ResolveNamed<TService>(string serviceName) where TService : class
        {
            return UnityContainerExtensions.Resolve<TService>(this, serviceName);
        }

        public TService ResolveNamed<TService>(string serviceName, params object[] param) where TService : class
        {
            return UnityContainerExtensions.Resolve<TService>(this, Array.ConvertAll(param, i => new ParameterOverride(i.GetType().FullName, i)));
        }

        public void Register<TService, TImplementer>(string serviceName, LifeCycle life)
          where TService : class
          where TImplementer : class, TService
        {
            CheckType<TImplementer>();
            UnityContainerExtensions.RegisterType<TService, TImplementer>(this, serviceName);
        }
        
        public bool IsCanBeResolve<T>()
        {
            return IsCanBeResolve(typeof(T));
        }

        public bool IsCanBeResolve(Type type)
        {
            if (type.IsInterface || type.IsAbstract)
            {
                return false;
            }
            return true;
        }

        internal void CheckType(Type type)
        {
            if (!IsCanBeResolve(type))
            {
                throw new ArgumentNullException("注入的实现类型"+type.FullName+" 不能为接口或者抽象类");
            }
        }
        internal void CheckType<T>()
        {
            if (!IsCanBeResolve<T>())
            {
                throw new ArgumentNullException("注入的实现类型 "+typeof(T).FullName+" 不能为接口或者抽象类");
            }
        }
    }
}
