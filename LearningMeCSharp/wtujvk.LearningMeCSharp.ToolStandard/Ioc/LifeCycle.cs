using System;
using System.Collections.Generic;
using System.Text;

namespace wtujvk.LearningMeCSharp.ToolStandard.Ioc
{
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
