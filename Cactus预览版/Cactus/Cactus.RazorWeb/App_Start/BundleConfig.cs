using System.Web;
using System.Web.Optimization;

namespace Cactus.RazorWeb
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //jquery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            //验证库
            bundles.Add(new ScriptBundle("~/bundles/validate").Include(
                        "~/Scripts/jquery.validate1.13.js",
                        "~/Scripts/jquery.validate.message_cn.js"));
            //ajax扩展支持
            bundles.Add(new ScriptBundle("~/bundles/unobtrusive").Include(
            "~/Scripts/jquery.unobtrusive-ajax.js"));
            //ajax扩展支持
            bundles.Add(new ScriptBundle("~/bundles/LoadLayer").Include(
            "~/Content/Admin/LoadLayer.js"));
            //config.js
            bundles.Add(new ScriptBundle("~/bundles/config").Include(
            "~/Scripts/config.js"));
            //upload2 js
            bundles.Add(new ScriptBundle("~/bundles/upload2").Include(
            "~/Scripts/jquery.upload2.js"));
            //tipso js
            bundles.Add(new ScriptBundle("~/bundles/tipso").Include(
            "~/Components/tipso/js/tipso.js"));
            //tipso css
            bundles.Add(new StyleBundle("~/Content/tipso").Include(
            "~/Components/tipso/css/tipso.css"));
            //fonts
            bundles.Add(new StyleBundle("~/Content/fonts-css").Include(
                "~/Content/Public/fonts-css/font-awesome.css"));
            //pure0.6.0
            bundles.Add(new StyleBundle("~/Content/pure").Include(
                        "~/Content/Public/pure0.6.0/pure.css"));
            //pure0.6.0ie
            bundles.Add(new StyleBundle("~/Content/pure-ie").Include(
                        "~/Content/Public/pure0.6.0/grids-responsive-old-ie.css"));
            //admin css
            bundles.Add(new StyleBundle("~/Content/admincss").Include(
                        "~/Content/Admin/admin.css"));
            //测试时开启压缩,默认不开启需要手动
            BundleTable.EnableOptimizations = true;
            //或者修改此处debug为false开启
            //<compilation debug="false" targetFramework="4.0">
        }
    }
}