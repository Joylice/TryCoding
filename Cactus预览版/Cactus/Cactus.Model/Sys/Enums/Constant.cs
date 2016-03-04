using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Cactus.Model.Sys.Enums
{
    public class Constant
    {

        /// <summary>
        /// 站点配置文件路径
        /// </summary>
        public static string SiteConfigPath = HttpContext.Current.Server.MapPath("/Configuration/SiteConfig.config");
        /// <summary>
        /// 权限配置文件路径
        /// </summary>
        public static string PowerConfigPath = HttpContext.Current.Server.MapPath("/Configuration/PowerConfig.config");
        /// <summary>
        /// 商户权限配置文件路径
        /// </summary>
        public static string StoreActionPath = HttpContext.Current.Server.MapPath("/Configuration/StoreAction.config");
        /// <summary>
        /// 站点缓存键集合
        /// </summary>

        public static class CacheKey
        {
            /// <summary>
            /// 站点信息缓存key
            /// </summary>
            public static string SiteConfigCacheKey = "CACHE_SITE_CONFIG";
            /// <summary>
            /// 权限信息缓存key
            /// </summary>
            public static string PowerConfigCacheKey = "CACHE_POWER_CONFIG";
            /// <summary>
            /// 登陆信息缓存key
            /// </summary>
            public static string LoginUserInfoCacheKey = "CACHE_LOGIN_USER";

            public static Dictionary<string, string> List = new Dictionary<string, string>();
            static CacheKey() {
                List.Add(SiteConfigCacheKey, "站点信息缓存");
                List.Add(PowerConfigCacheKey, "权限信息缓存");
                List.Add(LoginUserInfoCacheKey, "登陆信息缓存");
            }
        }
    }
}
