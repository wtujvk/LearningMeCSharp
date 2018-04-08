using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using wtujvk.LearningMeCSharp.Entities;
using wtujvk.LearningMeCSharp.ToolStandard;
using wtujvk.LearningMeCSharp.ToolStandard.Utils;

namespace wtujvk.LearningMeCSharp.MysqlEFCore.ext
{
    /// <summary>
    ///数据初始化帮助 
    /// </summary>
    public class DbInitializer
    {
        /// <summary>
        /// 数据初始化
        /// </summary>
        /// <param name="context"></param>
        public static void Initialize(MySqldBContext context)
        {
            try
            {
                //1.创建用户
                context.Database.EnsureCreated();
                var bsuser = new BsUser()
                {
                    Code = "15817158662",
                    Name = "管理员",
                    LoginName = "admin",
                    Password = "123456".Md5(),
                    CreateTime = DateTime.Now,
                    IsActive = true
                };
                context.BsUser.AddOrUpdate(c => c.Code == bsuser.Code, bsuser);
                context.SaveChanges();
                //2.创建角色
                var adminrole = new Admin_Role()
                {
                    IsSys = 1,
                    Name = "系统管理员",
                    Remark = "拥有最高权限",
                    RoleLevel = 1,
                    WebSiteId = 0
                };
                context.Admin_Role.AddOrUpdate(c => c.IsSys == 1, adminrole);
                context.SaveChanges();
                //3.创建菜单
                var adminMenu1 = new Admin_Menu()
                {
                    ParentID = 0,
                    OperTime = DateTime.Now,
                    Text = "系统管理",
                    Url = "#",
                    OrderBy = 1,
                    Isexpand = true,
                    TreeId = "系统管理",
                    SysArea = "",
                    SysController = "",
                    SysAction = "",
                    Remark = "初始化创建",
                    IsShow = true
                };
                var adminmenu6 = new Admin_Menu()
                {
                    Isexpand = true,
                    ParentID = 0,
                    OperTime = DateTime.Now,
                    IsShow = true,
                    OrderBy = 10,
                    Remark = "系统创建演示",
                    Text = "工具帮助",
                    TreeId = "常用工具",
                    Url = "#",
                    SysController = "",
                    SysArea = "",
                    SysAction = ""
                };
                context.Admin_Menu.AddOrUpdate(c => c.Text == adminMenu1.Text, adminMenu1);
                context.Admin_Menu.AddOrUpdate(c => c.Text == adminmenu6.Text, adminmenu6);
                context.SaveChanges();
                var adminMenu2 = new Admin_Menu()
                {
                    ParentID = adminMenu1.Id,
                    OperTime = DateTime.Now,
                    Text = "系统管理",
                    Url = "/SysAdmin/Menu/List",
                    OrderBy = 4,
                    Isexpand = true,
                    TreeId = "系统管理",
                    SysArea = "SysAdmin",
                    SysController = "Menu",
                    SysAction = "List",
                    Remark = "初始化-菜单列表",
                    IsShow = true
                };
                var adminMenu3 = new Admin_Menu()
                {
                    ParentID = adminMenu1.Id,
                    OperTime = DateTime.Now,
                    Text = "添加系统菜单",
                    Url = "/SysAdmin/Menu/AddView",
                    OrderBy = 6,
                    Isexpand = true,
                    TreeId = "系统管理-添加菜单",
                    SysArea = "SysAdmin",
                    SysController = "Menu",
                    SysAction = "AddView",
                    Remark = "初始化-添加菜单列表",
                    IsShow = true
                };
                var adminMenu4 = new Admin_Menu()
                {
                    ParentID = adminMenu1.Id,
                    OperTime = DateTime.Now,
                    Text = "编辑系统管理",
                    Url = "/SysAdmin/Menu/EditeView",
                    OrderBy = 8,
                    Isexpand = true,
                    TreeId = "系统管理",
                    SysArea = "SysAdmin",
                    SysController = "Menu",
                    SysAction = "EditeView",
                    Remark = "初始化-编辑菜单列表",
                    IsShow = true
                };

                var adminmenu7 = new Admin_Menu()
                {
                    ParentID = adminmenu6.Id,
                    Isexpand = true,
                    Url = "/Demo/Home/UeditorDemo",
                    IsShow = true,
                    OperTime = DateTime.Now,
                    OrderBy = 12,
                    Text = "百度编辑器示例",
                    TreeId = "工具帮助-百度编辑器",
                    Remark = "初始化-美图秀秀web",
                    SysArea = "Demo",
                    SysController = "Home",
                    SysAction = "UeditorDemo"
                };
                var adminmenu8 = new Admin_Menu()
                {
                    ParentID = adminmenu6.Id,
                    Isexpand = true,
                    Url = "/Demo/Home/MeiTuXx",
                    IsShow = true,
                    OperTime = DateTime.Now,
                    OrderBy = 14,
                    Text = "截图上传之美图秀秀",
                    TreeId = "工具帮助-截图上传之美图秀秀",
                    Remark = "初始化-截图上传之美图秀秀",
                    SysArea = "Demo",
                    SysController = "Home",
                    SysAction = "MeiTuXx"
                };
                var adminmenulst = new List<Admin_Menu>() { adminMenu1, adminMenu2, adminMenu3, adminMenu4, adminmenu6, adminmenu7, adminmenu8 };
                adminmenulst.ForEach(c => context.Admin_Menu.AddOrUpdate(p => p.Text == c.Text, c));
                //4.创建用户-角色关系
                var adminroleUser = new Admin_UserRole()
                {
                    RoleId = adminrole.Id,
                    UserId = bsuser.ID
                };
                context.Admin_UserRole.AddOrUpdate(c => c.RoleId == adminrole.Id && c.UserId == bsuser.ID, adminroleUser);
                context.SaveChanges();
                //5.创建 角色-菜单关系
                foreach (var item in adminmenulst)
                {
                    context.Admin_RoleMenu.AddOrUpdate(c => c.MenuId == item.Id && c.RoleId == adminrole.Id, new Admin_RoleMenu()
                    {
                        MenuId = item.Id,
                        RoleId = adminrole.Id
                    });
                }
                context.SaveChanges();
                var bearerEntity = new BearerEntity() { Access_token=Guid.NewGuid().ToString("N"), Access_token_Expires=DateTime.Now.AddMinutes(5), AppName= "wtujvk.LearningMeCSharp", AppSecret="1223", Refresh_token=Guid.NewGuid().ToString("N"), Refresh_Token_Expires=DateTime.Now.AddDays(7), StateCode=1, UserId="10086", UserName="wtujvk" };
                context.BearerEntity.AddOrUpdate(c => c.AppName==bearerEntity.AppName, bearerEntity);
                context.SaveChanges(); ;
            }
            catch(DbUpdateException ex)
            {
                string msg = string.Format($"{ex.Message}-{ex.Source}-{ex.TargetSite}-{ex.StackTrace}");
                //List<string> errorMessages = new List<string>();
                //foreach (var validationResult in ex.Data)
                //{
                //    //string entityName = validationResult.Entry.Entity.GetType().Name;
                //    //foreach (DbValidationError error in validationResult.ValidationErrors)
                //    //{
                //    //    errorMessages.Add(entityName + "." + error.PropertyName + ": " + error.ErrorMessage);
                //    //}
                //}
                LoggerFactory.Instance.Logger_Error(ex);
            }
            catch (Exception ex)
            {
               LoggerFactory.Instance.Logger_Error(ex);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class Ext
    {
        public static void AddOrUpdate<T>(this DbSet<T> ds, Expression<Func<T, bool>> exp, params T[] t) where T : class, IEntity
        {
            if (ds == null || exp == null || t == null)
            {
                //throw new ArgumentNullException("添加的内容为null");
            }
            else
            {
                if (!ds.Where(exp).Any())
                {
                    t = t.Where(c => c != null).ToArray();
                    ds.AddRange(t);
                }
            }
        }
    }
}
