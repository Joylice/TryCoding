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

namespace Cactus.Controllers.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class PowerAttribute : FilterAttribute, IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //throw new NotImplementedException();
        }
        /// <summary>
        /// 权限标示名
        /// </summary>
        public int PowerId { get; set; }
        /// <summary>
        /// 是否超级管理员应用
        /// </summary>
        public bool IsSuper = false;

        protected User LoginUser = null;

        protected PowerConfig Power = null;

        public IPowerConfigService powerConfigService = AutofacDependencyResolver.Current.ApplicationContainer.Resolve<IPowerConfigService>();
        
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(CookieHelper.GetCookieValue("Admin"));


            LoginUser = CacheHelper.GetCache(Constant.CacheKey.LoginUserInfoCacheKey + "_" + ticket.Name) as User;
            
            bool b = false;

            if (IsSuper == false)
            {
                //非超级管理员专属操作

                //权限id集合
                string[] acts = LoginUser.Role.ActionIds.Split(',');

                Power = CacheHelper.GetCache(Constant.CacheKey.PowerConfigCacheKey) as PowerConfig;

                if (Power == null)
                {
                    Power = powerConfigService.LoadConfig(Constant.PowerConfigPath);
                    CacheHelper.SetCache(Constant.CacheKey.PowerConfigCacheKey, Power);
                }
                try
                {
                    if (Power != null)
                    {
                        //Power.PowerGroupList.Where(p=>p.PowerList.FirstOrDefault(t => t.Id == PowerId)==null);
                        foreach (var li in Power.PowerGroupList) {
                            var p=li.PowerList.FirstOrDefault(t => t.Id == PowerId);
                            if (p != null)
                            {
                                if (acts.Contains(p.Id.ToString()))
                                {
                                    //存在权限
                                    b = true;
                                    break;
                                }
                            }
                        }

                        //var p = Power.PowerList.FirstOrDefault(t => t.Id == PowerId);
                        //if (p != null)
                        //{
                        //    if (acts.Contains(p.Id.ToString()))
                        //    {
                        //        存在权限
                        //        b = true;
                        //    }
                        //}
                    }
                }
                catch
                {
                    b = false;
                }
            }
            //超级管理员都可以使用
            if (LoginUser.IsSuperUser)
            {
                b = true;
            }

            #region 无权限执行
            if (b == false)
            { //无权限执行
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new ContentResult()
                    {
                        Content = "无权访问",
                        ContentEncoding = Encoding.UTF8
                    };
                }
                else
                {
                    filterContext.Controller.ViewData["ErrorMessage"] = "无权访问";//filterContext.Exception.Message + " 亲！您犯错了哦！";//得到报错的内容
                    filterContext.Result = new ViewResult()//new一个url为Error视图
                    {
                        ViewName = "Error",/*在Shard文件夹下*/
                        ViewData = filterContext.Controller.ViewData//view视图的属性中的viewdata被赋值
                    };
                }
            }
            #endregion
        }
    }
}
