
//using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using wtujvk.LearningMeCSharp.ToolStandard;
using wtujvk.LearningMeCSharp.ToolStandard.factorys;
using wtujvk.LearningMeCSharp.ToolStandard.Ioc;
using wtujvk.LearningMeCSharp.ToolStandard.Modules;

namespace MyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();                  
        }

        static void Run()
        {
            var module = ModuleManager.Create()
                 .UseUnity()
                 .UseNormalLogger()
                 .UseNewtonsoftSerializer()
                 .RegisterGenericModule(typeof(IM<>), typeof(MM<>))
                 .RegisterModule<IA, A>()
                 ;
            var aa = ModuleManager.Resolve<IM<A>>();
            var pp = ModuleManager.IsRegistered<IM<BB>>();
            var cc = ModuleManager.Resolve <IM<BB>>();
            var container3 = IoCFactory.Current.IocContainer;
           //var res= container3.IsRegistered<IM<A>>();
            container3.Register<IA, A>();
            container3.RegisterGeneric(typeof(IM<>), typeof(MM<>), LifeCycle.Singleton);
           
            //container3.Register<IM<A>, MM<A>>();
           var res3= container3.IsRegistered<IM<A>>();
            var res4= container3.IsRegistered<IA>();
            var res5 = container3.IsRegistered<IM<BB>>();
            var ima = container3.Resolve<IM<A>>();
            ima.Add(new A() { Name = "哇哈哈" });

        }
    } 
    
    class A:IA
    {
        public string Name { get; set; }

        public string GetName()
        {
            return DateTime.Now.ToString();
        }
    }
    public interface IA
    {
        string GetName();
    }
    public class BB:IB
    {
        public int Id { get; set; }
    }
    public class IB { }
    interface IM<T>
    {
        void Add(T t); 
    }
    class MM<T> : IM<T>
    {
        public List<T> list = new List<T>();
        public void Add(T t)
        {
            // throw new NotImplementedException();
            list.Add(t);
        }
    }
}
                                             