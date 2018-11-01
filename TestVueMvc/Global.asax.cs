using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TestVueMvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            var lastError = Server.GetLastError();
            if (lastError != null)
            {
                var httpError = lastError as HttpException;
                if (httpError != null)
                {
                    //ASP.NET的400与404错误不记录日志，并都以自定义404页面响应
                    var httpCode = httpError.GetHttpCode();
                    if (httpCode == 400 || httpCode == 404)
                    {
                        //Response.StatusCode = 404;//在IIS中配置自定义404页面
                        Server.ClearError();
                        HttpContext.Current.Response.Redirect("~/");
                        return;
                    }
                    //Logger.Default.Error("Application_Error_" + httpCode, httpError);
                }

                //对于路径错误不记录日志，并都以自定义404页面响应
                if (lastError.TargetSite.ReflectedType == typeof(System.IO.Path))
                {
                    Response.StatusCode = 404;
                    Server.ClearError();
                    return;
                }

                //Logger.Default.Error("Application_Error", lastError);
                //Response.StatusCode = 500;
                HttpContext.Current.Response.Redirect(@"~/error500");
                Server.ClearError();
            }
        }
    }
}
