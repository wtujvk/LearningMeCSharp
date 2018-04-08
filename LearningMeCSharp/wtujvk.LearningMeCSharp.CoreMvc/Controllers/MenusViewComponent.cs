using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wtujvk.LearningMeCSharp.Entities;
using wtujvk.LearningMeCSharp.IRepository;
using wtujvk.LearningMeCSharp.ToolStandard;

namespace wtujvk.LearningMeCSharp.CoreMvc.Controllers
{
    //[ViewComponent(Name ="Menus")]
    public class MenusViewComponent: ViewComponent
    {
        IExtensionRepository<Admin_Menu> Admin_Menurepository = ModuleManager.Resolve<IExtensionRepository<Admin_Menu>>();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var q = await Admin_Menurepository.GetQueryAsync(c=>true);
            var menuslt = q.ToList();
            ViewData["menuLst"] = menuslt;
            return View("_Menus");
        }
    }
}
