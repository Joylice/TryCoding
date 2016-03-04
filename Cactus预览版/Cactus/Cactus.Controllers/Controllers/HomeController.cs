using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cactus.Controllers.Filters;

namespace Cactus.Controllers
{
    [Exception]
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index(int? page)
        {
            return View();
        }

        public ActionResult Detail(int? id)
        {
            return View();
        }

        public ActionResult Search(string key,int? page)
        {
            return View();
        }
    }
}
