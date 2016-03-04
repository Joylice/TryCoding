using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Cactus.Extension;
//using Cactus.Extension.MvcXmlRouting;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace Cactus.RazorWeb
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
            
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //路由改写
            //InitializeRoutes(RouteTable.Routes);
            //原始路由
            AreaRegistration.RegisterAllAreas();//域注册必须在路由之前
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            
            //InitializeLogger();

            InitializeServiceLocator();

        }

        protected void Session_Start()
        {
            //RecordVisitorInfo();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            if (ex != null)
            {
                LogHelper<MvcApplication>.Log(ex);
                // Server.ClearError();
            }
        }

        

        protected virtual void InitializeServiceLocator()
        {
            IocConfig.BuildMvcContainer();
        }

        protected virtual void InitializeLogger()
        {
            const string logConfigPath = "~/Configuration/Log4net.config";
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(Server.MapPath(logConfigPath)));
        }


    }
}