using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wtujvk.LearningMeCSharp.MvcDemo.Models;
using wtujvk.LearningMeCSharp.Utils;

namespace wtujvk.LearningMeCSharp.MvcDemo.Controllers
{
    public class HomeController : Controller
    {
        //session键值
        const string SessionKey = "TEST-SESSIONKEY";
        // GET: Home
        public ActionResult Index()
        {
            //判断session是否存在,如果存在就是输出
            var person = Session[SessionKey];
            var count= Session.Count;
            ViewBag.Person = person;
            ViewBag.Sesscount = count;
            return View();
        }
        //清除session
        public ActionResult LogOut()
        {
            Session.Remove(SessionKey);
            ViewBag.Msg = "session已经清除";
            return View();
        }
        //session赋值
        public ActionResult LoginIn()
        {
           var person = new Person() { Name= RandomChinese.GetRandChineseName(), Age=StringExtend.GetRanAge(800)};
            Session[SessionKey] = person;
            ViewBag.Msg = "session已经赋值"+person.Id;
            return View();
        }
        /// <summary>
        /// 清除全部
        /// </summary>
        /// <returns></returns>
        public ActionResult ClearAll()
        {
            Session.Clear();
            // Session.Abandon();
            ViewBag.Msg = "session已经清除-ClearAll";
            return View();
        }
    }
}