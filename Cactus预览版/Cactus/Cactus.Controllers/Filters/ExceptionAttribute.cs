using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Cactus.Controllers.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class ExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new ContentResult()
                {
                    Content = "Error",
                    ContentEncoding = Encoding.UTF8
                };
            }
            else {
                filterContext.Result = new RedirectResult("/Error/Page404");//new一个url为Error视图
            }

            filterContext.ExceptionHandled = true;
        }
    }



}
