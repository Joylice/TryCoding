using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Cactus.Controllers.Expand
{
    public class BaseController : Controller
    {

        protected virtual void OnException(ExceptionContext filterContext) {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new ContentResult()
                {
                    Content = "Error",
                    ContentEncoding = Encoding.UTF8
                };
            }
            else
            {
                filterContext.Result = new RedirectResult("Error/Page404");
            }

            filterContext.ExceptionHandled = true;
        }

    }
}
