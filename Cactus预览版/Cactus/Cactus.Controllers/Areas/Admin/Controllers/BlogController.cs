using Cactus.Controllers.Expand;
using Cactus.Controllers.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cactus.Controllers.Areas.Admin.Controllers
{
    public class BlogController : PowerBaseController
    {
        //
        // GET: /Admin/Blog/
        [Power(IsSuper = false, PowerId = 2001)]
        public ActionResult Index()
        {
            return View();
        }
        [Power(IsSuper = false, PowerId = 2002)]
        public ActionResult FileManage()
        {
            return View();
        }
    }
}
