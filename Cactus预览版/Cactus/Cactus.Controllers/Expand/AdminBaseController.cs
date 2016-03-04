using Autofac;
using Autofac.Integration.Mvc;
using Cactus.Common;
using Cactus.IService;
using Cactus.Model.Sys;
using Cactus.Model.Sys.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace Cactus.Controllers.Expand
{
    public class AdminBaseController : Controller
    {

        public IUserServer userServer = AutofacDependencyResolver.Current.ApplicationContainer.Resolve<IUserServer>();
        
        public ISiteConfigService siteConfigService = AutofacDependencyResolver.Current.ApplicationContainer.Resolve<ISiteConfigService>();


        /// <summary>
        /// 站点配置信息
        /// </summary>
        protected SiteConfig Config = null;

        /// <summary>
        /// 登录用户信息
        /// </summary>
        protected User LoginUser = null;

        /// <summary>
        /// 获取已验证用户信息
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            var userName = CookieHelper.GetCookieValue("Admin");
            if (!String.IsNullOrEmpty(userName))
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(userName);
                userName = ticket.Name;

                LoginUser = CacheHelper.GetCache(Constant.CacheKey.LoginUserInfoCacheKey + "_" + userName) as User;
                if (LoginUser == null)
                {
                    var j = userName.Substring("Admin_".Length, userName.Length - "Admin_".Length);

                    int id = int.Parse(j);
                    var user = userServer.Find(id);
                    if (user != null)
                    {
                        CacheHelper.SetCache(Constant.CacheKey.LoginUserInfoCacheKey + "_Admin_" + user.User_Id, user);
                        LoginUser = user;
                    }
                }
            }
            base.OnAuthorization(filterContext);
        }

        /// <summary>
        /// 执行前读取站点配置信息
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Config = CacheHelper.GetCache(Constant.CacheKey.SiteConfigCacheKey) as SiteConfig;
            if (Config == null)
            {
                Config = siteConfigService.LoadConfig(Constant.SiteConfigPath);
                CacheHelper.SetCache(Constant.CacheKey.SiteConfigCacheKey, Config);
            }
        }

        /// <summary>
        /// 返回结果前附加数据
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (Config != null)
            {
                ViewData["SiteConfig"] = Config;
            }
        }
    }
}
