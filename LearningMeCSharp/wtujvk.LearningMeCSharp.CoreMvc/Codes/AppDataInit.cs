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
        /// 网站根目录物理路径 wwwroot 静态资源文件使用这个
        /// Gets or sets the absolute path to the directory that contains the web-servable application content files.
        /// </summary>
        public static string WebRoot { get; set; }
        /// <summary>
        /// bin目录的上级目录
        /// Gets or sets the absolute path to the directory that contains the application content files
        /// </summary>
        public static string ContentRootPath { get; set; }

        internal static void Init()
        {
            ConfigManager.AppBaseDirctory = WebRoot;
        }
        /// <summary>
        /// 资源目录名
        /// </summary>
        public const string UploadTempDir = "upload";
    }
}
