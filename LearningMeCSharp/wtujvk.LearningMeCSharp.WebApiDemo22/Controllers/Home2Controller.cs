using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.LindAgile.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wtujvk.LearningMeCSharp.WebApiDemo.Codes;

namespace wtujvk.LearningMeCSharp.WebApiDemo.Controllers
{
    /// <summary>
    /// 
    /// </summary>
   // [Produces("application/json")]
   // [Area("api")]
    //[Route("/api/Home2/{action}")]
    public class Home2Controller : Controller
    {
        private IExtensionRepository<LoginInfo> logRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public Home2Controller(IExtensionRepository<LoginInfo> repository)
        {
            logRepository = repository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object Index()
        {
            return DateTime.Now;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object Index2()
        {
           int count= logRepository.GetQuery().Count();
            return new { DateTime.Now,count};
        }
    }
}