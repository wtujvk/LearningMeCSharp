using System;
using System.Collections.Generic;
using System.Text;

namespace wtujvk.LearningMeCSharp.ToolStandard.Logger.Implements
{
    /// <summary>
    /// 空日志实现者
    /// </summary>
    internal class EmptyLogger : LoggerBase
    {
        protected override void InputLogger(Level level,string message)
        {
            Console.WriteLine(message);
        }
    }
}
