using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wtujvk.LearningMeCSharp.ToolStandard.Conf;

namespace wtujvk.LearningMeCSharp.CoreMvc.Codes
{
    /// <summary>
    /// 完整基础数据
    /// </summary>
    public class AppDataInit
    {
        /// <summary>
        /// 网站根目录物理路径
        /// </summary>
        public static string WebRoot { get; set; }

        internal static void Init()
        {
            ConfigManager.AppBaseDirctory = WebRoot;
        }

    }
}
