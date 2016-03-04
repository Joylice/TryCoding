using Cactus.Common;
using Cactus.Model.Sys;
using Cactus.Model.Sys.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cactus.IService;
using Autofac;
using Autofac.Integration.Mvc;

namespace Cactus.Controllers.Expand
{
    public class PowerBaseController : AdminBaseController
    {
        public IActionServer actionServer = AutofacDependencyResolver.Current.ApplicationContainer.Resolve<IActionServer>();
        public IRoleServer roleServer = AutofacDependencyResolver.Current.ApplicationContainer.Resolve<IRoleServer>();
        public IPowerConfigService powerConfigService = AutofacDependencyResolver.Current.ApplicationContainer.Resolve<IPowerConfigService>();
        
        protected PowerConfig Power = null;

        /// <summary>
        /// 获取已验证用户信息
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {

            base.OnAuthorization(filterContext);

            //获取已登录用户信息
            if (LoginUser == null)
            {
                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new ContentResult() { Content = "<a href=\"/AdminLogin/Index\">请登录</a>" };
                }
                else
                {
                    filterContext.Result = new RedirectResult("/AdminLogin/Index");
                }
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (LoginUser == null)
            {
                filterContext.Result = new RedirectResult("/AdminLogin");
            }

            Power = CacheHelper.GetCache(Constant.CacheKey.PowerConfigCacheKey) as PowerConfig;
            if (Power == null)
            {
                Power = powerConfigService.LoadConfig(Constant.PowerConfigPath);
                CacheHelper.SetCache(Constant.CacheKey.PowerConfigCacheKey, Power);
            }
        }

        /// <summary>
        /// 返回结果前附加数据
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if (LoginUser != null)
            {
                ViewData["LoginUser"] = LoginUser;
            }
            if (Power != null)
            {
                ViewData["Power"] = Power;
            }
        }
    }
}
