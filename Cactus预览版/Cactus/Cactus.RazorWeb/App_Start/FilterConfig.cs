using System.Web;
using System.Web.Mvc;

namespace Cactus.RazorWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new Cactus.Controllers.Filters.ExceptionAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}