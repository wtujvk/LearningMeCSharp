using System;
using System.Collections.Generic;
using System.Text;
using wtujvk.LearningMeCSharp.ToolStandard.Conf;
using wtujvk.LearningMeCSharp.ToolStandard.Ioc;

namespace wtujvk.LearningMeCSharp.ToolStandard.factorys
{
    /// <summary>
    /// 
    /// </summary>
    public class IoCFactory
    {
        private static readonly Lazy<IoCFactory> Lazy = new Lazy<IoCFactory>(() => new IoCFactory());
        #region Singleton

        #endregion
        public static IoCFactory Current => Lazy.Value;
        #region Members
        /// <summary>
        /// Get current configured IContainer
        /// </summary>
        public ILindContainer IocContainer { get;}
        #endregion
        #region Constructor
        public IoCFactory()
        {
            switch (ConfigManager.Config.IocContaion.IoCType)
            {
                case 0:
                    IocContainer = new UnityAdapterContainer();
                    break;
                case 1:
                  //  _CurrentContainer = new AutofacContainer();
                    break;
                default:
                    throw new ArgumentException("不支持此IoC类型");
            }

        }
        #endregion
    }
}
