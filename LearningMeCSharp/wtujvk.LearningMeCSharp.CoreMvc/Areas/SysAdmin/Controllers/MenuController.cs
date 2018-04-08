using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace wtujvk.LearningMeCSharp.CoreMvc.Areas.SysAdmin.Controllers
{
    /// <summary>
    /// 系统菜单管理
    /// </summary>
    [Area("SysAdmin")]
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 菜单列表
        /// </summary>
        /// <returns></returns>
        public IActionResult List()
        {
            return View();
        }
        /// <summary>
        /// 添加菜单-页面
        /// </summary>
        /// <returns></returns>
        public IActionResult AddView()
        {
            return View();
        }
        /// <summary>
        /// 修改菜单-页面
        /// </summary>
        /// <returns></returns>
        public IActionResult EditeView()
        {
            return View();
        }

    }
}