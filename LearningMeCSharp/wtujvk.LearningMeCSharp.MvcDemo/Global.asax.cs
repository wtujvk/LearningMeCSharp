using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using wtujvk.LearningMeCSharp.Utils;

namespace wtujvk.LearningMeCSharp.MvcDemo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            LoggerFactory.Instance.Logger_Debug("初始化。。。。。");
        }
        void Session_Start(Object sender, EventArgs e)
        {
            LoggerFactory.Instance.Logger_Debug("创建session");
        }
        void Session_End(Object sender, EventArgs e)
        {
            LoggerFactory.Instance.Logger_Debug("清除session");
        }
        void Application_Error(object sender, EventArgs e)
        {
            try
            {
                Exception lastError = Server.GetLastError();
                string strExceptionMessage = string.Empty;
                if (lastError != null)
                {
                    //strExceptionMessage =$"{lastError.Message},{lastError.Source}-{lastError.TargetSite}-{lastError.StackTrace}";
                    LoggerFactory.Instance.Logger_Error(lastError);
                }
                Server.ClearError();//清除异常
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Server.ClearError();
            }
        }
    }
}
