using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.SessionState;
using System.Web.Security;
using Cactus.Controllers.Expand;
using Cactus.IService;
using Cactus.Model.Sys.Enums;
using Cactus.Model.Sys;
using Cactus.Common;
using Cactus.Controllers.Filters;

namespace Cactus.Controllers
{

    [Exception]
    [SessionState(SessionStateBehavior.Disabled)]
    public class AdminLoginController : AdminBaseController
    {
        public AdminLoginController(IUserServer userServer)
        {
            this.userServer = userServer;
        }
        [HttpGet]
        public ActionResult Index()
        {
            if (this.LoginUser != null)
            {
                return Redirect("/Admin/");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Index(string VCode, string UserName, string Password)
        {
            //最后判断来源
            if (CryptoHelper.MD5Encrypt(VCode.Trim()) == CookieHelper.GetCookieValue("code"))
            {
                User u = userServer.CheckLogin(UserName.Trim(), Password.Trim());
                if (u == null)
                {
                    return Json(new { error = "用户不存在", pass = false });
                }
                else
                {
                    //设置用户登录状态
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1111,
                    "Admin_" + u.User_Id.ToString(),
                    DateTime.Now,
                    DateTime.Now.AddDays(1),
                    true,
                    "Admin_" + u.User_Id.ToString());
                    //加密Ticket，变成一个加密的字符串。
                    string cookieValue = FormsAuthentication.Encrypt(ticket);
                    CookieHelper.SetCookie("Admin", cookieValue);
                    //更新用户登录信息
                    u.LastLoginTime = DateTime.Now;
                    u.LastLoginIp = HttpHelper.GetClientIPAddress();
                    userServer.Update(u);
                    //加入缓存
                    CacheHelper.SetCache(Constant.CacheKey.LoginUserInfoCacheKey + "_Admin_" + u.User_Id, u);
                    //清除验证码code
                    CookieHelper.ClearCookie("code");
                    return Json(new { pass = true });
                }
            }
            else
            {
                return Json(new { error = "验证码错误", pass = false });
            }
        }
        [HttpGet]
        public void VCode()
        {
            VCodeHelper c = new VCodeHelper();
            string code = c.CreateVerifyCodeImage(4, 0, true, true);//验证码数据填写
            code = CryptoHelper.MD5Encrypt(code);
            CookieHelper.SetCookie("code", code);
        }
    }
}
