using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace wtujvk.LearningMeCSharp.ToolStandard.Logger.Implements
{
    /// <summary>
    /// 以普通的文字流的方式写日志
    /// </summary>
    internal class NormalLogger : LoggerBase
    {
        static readonly object objLock = new object();
        protected override void InputLogger(Level level, string message)
        {
            string filePath = Path.Combine(FileUrl, DateTime.Now.ToLongDateString() + "_" + System.Diagnostics.Process.GetCurrentProcess().Id + ".log");

            if (!System.IO.Directory.Exists(FileUrl))
                System.IO.Directory.CreateDirectory(FileUrl);

            lock (objLock)//防止多线程读写冲突
            {
                //using (System.IO.StreamWriter srFile = new System.IO.StreamWriter(filePath, true))
                //{
                //    //srFile.WriteLine(string.Format("{0}{1}{2}"
                //    //    , DateTime.Now.ToString().PadRight(20)
                //    //    , ("[ThreadID:" + Thread.CurrentThread.ManagedThreadId.ToString() + "]").PadRight(14)
                //    //    , message));
                //    srFile.WriteLine(string.Format("{0}{1}{2}{3}"
                //           , DateTime.Now.ToString().PadRight(18)
                //           , ("[Id:" + Thread.CurrentThread.ManagedThreadId.ToString().PadLeft(2, '0') + "]").PadRight(6)
                //           , level.ToString()
                //           , message));
                //}
                var msg = string.Format("{0}{1}{2}{3}"
                           , DateTime.Now.ToString().PadRight(18)
                           , ("[Id:" + Thread.CurrentThread.ManagedThreadId.ToString().PadLeft(2, '0') + "]").PadRight(6)
                           , level.ToString()
                           , message);
                File.AppendAllText(filePath, message);
                
            }
        }
    }
}
