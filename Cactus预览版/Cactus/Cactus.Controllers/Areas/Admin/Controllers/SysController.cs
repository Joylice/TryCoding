using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Cactus.Common;
using Cactus.Model.Sys;
using Cactus.Model.Sys.Enums;
using Cactus.Controllers.Expand;
using Cactus.Controllers.Filters;
using Cactus.Model.Other;

namespace Cactus.Controllers.Areas.Admin.Controllers
{
    public class SysController : PowerBaseController
    {

        public ActionResult Index()
        {
            return View();
        }
        
        //初步完成
        #region 个人中心
        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(CookieHelper.GetCookieValue("Admin"));
            
            //移除缓存的登录用户信息
            CacheHelper.RemoveAllCache(Constant.CacheKey.LoginUserInfoCacheKey + "_" + ticket.Name);

            //用户注销
            
            //FormsAuthentication.SignOut();

            CookieHelper.ClearCookie("Admin");
            //return Redirect("/AdminLogin");
            //return View();

            if (HttpContext.Request.IsAjaxRequest())
            {
                return Json(new
                {
                    success = true,
                    msg = "退出成功"
                }, JsonRequestBehavior.AllowGet);
            }
            else {
                return Redirect("/AdminLogin/Index");
            }
        }
        [Power(IsSuper = false, PowerId = 1008)]
        public ActionResult CenterUser()
        {
            return View("CenterUser");
        }
        [Power(IsSuper = false, PowerId = 1009)]
        public ActionResult CenterAlterPwd()
        {
            return View();
        }
        [Power(IsSuper = false, PowerId = 1009)]
        public ActionResult CenterAlterPwd(string pwded, string pwding)
        {
            pwded = pwded.Trim();
            pwding = pwding.Trim();
            if (pwded == pwding)
            {
                return Json(new { error = "旧密码与新密码相同", pass = false });
            }
            if (LoginUser.Password == CryptoHelper.MD5Encrypt(pwded))
            {
                var act = this.userServer.Find(LoginUser.User_Id);
                act.Password = CryptoHelper.MD5Encrypt(pwding);
                this.userServer.Update(act);
                if (Constant.CacheKey.List[Constant.CacheKey.LoginUserInfoCacheKey].Count() > 0)
                {
                    HttpRuntime.Cache.Remove(Constant.CacheKey.LoginUserInfoCacheKey + "_Admin_" + LoginUser.User_Id);
                }
                return Json(new { url = "/Admin/CenterUser", pass = true });
            }
            else
            {
                return Json(new { error = "旧密码错误", pass = false });
            }
        }
        [Power(IsSuper = false, PowerId = 1010)]
        public ActionResult CenterAlterFace()
        {
            if (Request.HttpMethod == "GET")
            {
                return View();
            }
            else
            {
                var avatarFile = Request.Files["AvatarFile"];
                if (avatarFile != null && avatarFile.ContentLength > 0)
                {
                    if (avatarFile.ContentLength <= MyPath.fileSize * 1024)
                    {
                        var avatarName = avatarFile.FileName;
                        var avatarExt = Path.GetExtension(avatarName);

                        if (!String.IsNullOrEmpty(avatarExt)
                            && Config.ImgExtensions.Contains(avatarExt))
                        {
                            if (!System.IO.Directory.Exists(MyPath.TempPath)) {
                                System.IO.Directory.CreateDirectory(MyPath.TempPath);
                            }
                            //保存原图
                            var savePath = Path.Combine(MyPath.TempPath, avatarName);
                            avatarFile.SaveAs(savePath);
                            if (!System.IO.Directory.Exists(MyPath.AvatarPath))
                            {
                                System.IO.Directory.CreateDirectory(MyPath.AvatarPath);
                            }
                            //缩略图路径
                            var thumbPath = Path.Combine(MyPath.AvatarPath, "Avatar_" + LoginUser.User_Id + avatarExt);

                            //生成头像缩略图
                            ImageHelper.MakeThumbnailImage(savePath, thumbPath, 48, 48, "HW");

                            LoginUser.Avatar = MyPath.Web_AvatarPath+"/"+"Avatar_" + LoginUser.User_Id + avatarExt;
                            System.IO.File.Delete(savePath);

                            Model.Sys.User u = this.userServer.Find(LoginUser.User_Id);
                            u.Avatar = LoginUser.Avatar;
                            this.userServer.Update(u);

                            return Json(new { pass = true, url = LoginUser.Avatar });
                        }
                        else
                        {
                            return Json(new { error = "上传文件类型错误", pass = false });
                        }
                    }
                    else
                    {
                        return Json(new { error = "上传文件大小超出限制,200k以内", pass = false });
                    }
                }
                else
                {
                    return Json(new { error = "上传文件错误", pass = false });
                }
            }
        }
        [Power(IsSuper = false, PowerId = 1011)]
        public ActionResult CenterAlterInfo()
        {
            return View();
        }
        [Power(IsSuper = false, PowerId = 1011)]
        public ActionResult CenterAlterInfo(User u) {
            try
            {
                var act = this.userServer.Find(LoginUser.User_Id);
                act.NickName = u.NickName;
                act.Email = u.Email;
                act.Phone = u.Phone;
                act.Qq = u.Qq;
                this.userServer.Update(act);
                if (Constant.CacheKey.List[Constant.CacheKey.LoginUserInfoCacheKey].Count() > 0)
                {
                    HttpRuntime.Cache.Remove(Constant.CacheKey.LoginUserInfoCacheKey + "_Admin_" + LoginUser.User_Id);
                }
                return Json(new { pass = true });
            }
            catch (Exception e){
                return Json(new { pass = false,error=e.Message });
            }
        }
        #endregion

        //初步完成
        #region 用户
        //修改头像
        [HttpGet]
        [Power(IsSuper = false, PowerId = 1017)]
        public ActionResult UserAlterFace(int id)
        {
            var act = this.userServer.Find(id);
            ViewData["User"] = act;
            return View();
        }
        [HttpPost]
        [Power(IsSuper = false, PowerId = 1017)]
        public ActionResult UserAlterFacePost(int Id)
        {
            var avatarFile = Request.Files["AvatarFile"];
            string Avatar = "";
            if (avatarFile != null && avatarFile.ContentLength > 0)
            {
                if (avatarFile.ContentLength <= 200 * 1024)
                {
                    var avatarName = avatarFile.FileName;
                    var avatarExt = Path.GetExtension(avatarName);

                    string FileType = avatarName.Substring(avatarName.LastIndexOf(".") + 1);//取得类型
                    FileType = FileType.ToLower();



                    if (!String.IsNullOrEmpty(avatarExt)
                        && Config.ImgExtensions.Contains(avatarExt))
                    {
                        avatarName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + FileType;
                        //保存原图
                        if (!System.IO.Directory.Exists(MyPath.TempPath))
                        {
                            System.IO.Directory.CreateDirectory(MyPath.AvatarPath);
                        }
                        var savePath = Path.Combine(MyPath.TempPath, avatarName);
                        avatarFile.SaveAs(savePath);
                        if (!System.IO.Directory.Exists(MyPath.AvatarPath))
                        {
                            System.IO.Directory.CreateDirectory(MyPath.AvatarPath);
                        }
                        //缩略图路径
                        var thumbPath = Path.Combine(MyPath.AvatarPath, "Avatar_" + Id + avatarExt);


                        //生成头像缩略图
                        ImageHelper.MakeThumbnailImage(savePath, thumbPath, 48, 48, "HW");
                        //记得删除原图
                        DirFileHelper.DeleteFile(savePath);

                        Avatar = MyPath.Web_AvatarPath + "/" + "Avatar_" + Id + avatarExt;

                        Model.Sys.User u = this.userServer.Find(Id);
                        u.Avatar = Avatar;
                        this.userServer.Update(u);

                        return Json(new { url = Avatar, pass = true });
                    }
                    else
                    {
                        return Json(new { error = "上传文件类型错误", pass = false });
                    }
                }
                else
                {
                    return Json(new { error = "上传文件大小超出限制", pass = false });
                }
            }
            else
            {
                return Json(new { error = "上传文件错误", pass = false });
            }
        }
        [Power(IsSuper = false, PowerId = 1016)]
        public ActionResult UserResetPwd(int id)
        {
            var act = this.userServer.Find(id);
            bool b = false;
            try
            {
                act.Password = CryptoHelper.MD5Encrypt("123456");
                this.userServer.Update(act);
                b = true;
            }
            catch
            {
                b = false;
            }
            if (b)
            {
                return Json(new { msg = "密码已经重置为：123456", pass = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { error = "重置失败", pass = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [Power(IsSuper = false, PowerId = 1018)]
        public ActionResult UserInfo(int id)
        {
            var act = this.userServer.Find(id);
            ViewData["User"] = act;
            return View();
        }
        [HttpGet]
        [Power(IsSuper = false, PowerId = 1015)]
        public ActionResult UserAdd()
        {
            ViewData["RoleList"] = this.roleServer.GetAll().ToList();
            return View();
        }
        [HttpPost]
        [Power(IsSuper = false, PowerId = 1015)]
        public ActionResult UserAdd(string UserName, string Password, string NickName, string Email, string Phone, string Qq, int RoleId)
        {

            var b = this.userServer.Insert(new Model.Sys.User()
            {
                Avatar = Config.DefaultAvatar,
                Name = UserName,
                Password = CryptoHelper.MD5Encrypt(Password),
                NickName = NickName,
                Email = Email,
                Phone = Phone,
                Qq = Qq,
                AddTime = DateTime.Now,
                IsSuperUser = false,
                RoleId = RoleId,
                LastLoginTime = DateTime.Now,
                LastLoginIp = "127.0.0.1"//目前这样写
            });

            if (b)
            {
                return Json(new { url = "/Admin/Admin/UserIndex", pass = true });
            }
            else
            {
                return Json(new { error = "添加失败", pass = false });
            }

        }
        [HttpGet]
        [Power(IsSuper = false, PowerId = 1014)]
        public ActionResult UserUpdate(int id)
        {
            ViewData["RoleList"] = this.roleServer.GetAll().ToList();
            ViewData["User"] = this.userServer.Find(id);
            return View();
        }
        [HttpPost]
        [Power(IsSuper = false, PowerId = 1014)]
        public ActionResult UserUpdate(string NickName, string Email, string Phone, string Qq, int RoleId, int User_Id)
        {
            Model.Sys.User muser = this.userServer.Find(User_Id);

            muser.NickName = NickName;
            muser.Email = Email;
            muser.Phone = Phone;
            muser.Qq = Qq;
            muser.RoleId = RoleId;

            bool b = false;
            try
            {
                this.userServer.Update(muser);
                b = true;
            }
            catch
            {
                b = false;
            }
            if (b)
            {
                return Json(new { url = "/Admin/Admin/UserIndex", pass = true });
            }
            else
            {
                return Json(new { error = "修改失败", pass = false });
            }

        }
        [Power(IsSuper = false, PowerId = 1013)]
        public ActionResult UserDelete(string ids)
        {
            try
            {
                //int[] list = Array.ConvertAll<string, int>(ids.Split(','), s => int.Parse(s));
                this.userServer.Delete(ids);
                return Json(new { url = "/Admin/UserIndex", pass = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { error = "删除失败", pass = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [Power(IsSuper = false, PowerId = 1012)]
        public ActionResult UserIndex()
        {
            int count = 0;
            PageTurnModel pageturn = new PageTurnModel() { ItemSize = 10 };
            pageturn.PageIndex = 1;
            var list = this.userServer.ToPagedList(pageturn.PageIndex.Value, pageturn.ItemSize, "User_Id", out  count);
            pageturn.CountSize = count;
            ViewData["UserList"] = list;
            ViewData["Pageturn"] = pageturn;
            return View("UserList");
        }
        [Power(IsSuper = false, PowerId = 1012)]
        public ActionResult UserList(int? page)
        {
            if (!page.HasValue) { page = 1; }
            PageTurnModel pageturn = new PageTurnModel() {  ItemSize=10 };
            pageturn.PageIndex = page;
            int count = 0;
            var list = this.userServer.ToPagedList(pageturn.PageIndex.Value, pageturn.ItemSize, "User_Id", out  count).Select(t => new
            {
                t.User_Id,
                t.Name,
                t.NickName,
                t.Role.RoleName,
                AddTime = t.AddTime.ToString(),
                LastLoginTime = t.LastLoginTime.ToString()
            });
            pageturn.CountSize = count;
            return Json(new { rows = list, pagination = pageturn, pass = true }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        //初步完成
        #region 角色
        [Power(IsSuper = false, PowerId = 1002)]
        [HttpGet]
        public ActionResult RoleAdd()
        {
            return View();
        }
        [Power(IsSuper = false, PowerId = 1002)]
        [HttpPost]
        public ActionResult RoleAdd(string RoleName, string ActionIds)
        {
            if (ModelState.IsValid)
            {
                var b = this.roleServer.Insert(new Model.Sys.Role()
                {
                    RoleName = RoleName,
                    ActionIds = ActionIds
                });

                if (b)
                {
                    return Json(new { url = "/Admin/Admin/RoleIndex", pass = true });
                }
                else
                {
                    return Json(new { error = "添加失败", pass = false });
                }
            }
            else
            {
                return Json(new { error = "", pass = false });
            }
        }
        [Power(IsSuper = false, PowerId = 1003)]
        [HttpGet]
        public ActionResult RoleUpdate(int id)
        {
            var act = this.roleServer.Find(id);
            ViewData["Role"] = act;
            return View(act);
        }
        [Power(IsSuper = false, PowerId = 1003)]
        [HttpPost]
        public ActionResult RoleUpdate(string RoleName, string ActionIds,int Id)
        {
            if (ModelState.IsValid)
            {

                Model.Sys.Role maction = new Model.Sys.Role()
                {
                    RoleName = RoleName,
                    Role_Id = Id,
                    ActionIds = ActionIds
                };
                bool b = false;
                try
                {
                    this.roleServer.Update(maction);
                    b = true;
                }
                catch
                {
                    b = false;
                }
                if (b)
                {
                    return Json(new { url = "/Admin/Admin/RoleIndex", pass = true });
                }
                else
                {
                    return Json(new { error = "修改失败", pass = false });
                }
            }
            else
            {
                return Json(new { error = "", pass = false });
            }
        }
        [Power(IsSuper = false, PowerId = 1004)]
        [HttpPost]
        public ActionResult RoleDelete(string ids)
        {
            try
            {
                //int[] list = Array.ConvertAll<string, int>(ids.Split(','), s => int.Parse(s));
                this.roleServer.Delete(ids);
                return Json(new { pass = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { error = "删除失败", pass = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [Power(IsSuper = false, PowerId = 1001)]
        public ActionResult RoleList(int? page)
        {
            PageTurnModel pageturn = new PageTurnModel() { ItemSize = 100 };
            if (!page.HasValue)
            {
                page = 1;
                pageturn.PageIndex = page;
                int count = 0;
                var list = this.roleServer
                    .ToPagedList(pageturn.PageIndex.Value, pageturn.ItemSize, "Role_Id", out count);
                pageturn.CountSize = count;

                ViewData["RoleList"] = list;
                return View();
            }
            else {
                pageturn.PageIndex = page;
                int count = 0;
                var list = this.roleServer
                    .ToPagedList(pageturn.PageIndex.Value, pageturn.ItemSize, "Role_Id", out count);
                pageturn.CountSize = count;

                return Json(new { rows = list.Select(t => new { t.Role_Id, t.RoleName }), pagination = pageturn, pass = true });
            }
        }
        #endregion

        //初步完成
        #region 网站参数
        public ActionResult SysIndex()
        {
            return View();
        }
        public ActionResult SysSave(SiteConfig site)
        {
            try
            {
                this.Config.SiteName = site.SiteName;
                this.Config.SiteTitle = site.SiteTitle;
                this.Config.SiteCrod = site.SiteCrod;
                this.Config.SiteKeywords = site.SiteKeywords;
                this.Config.SiteDescr = site.SiteDescr;
                this.Config.SiteCountCode = site.SiteCountCode;
                this.Config.SiteCopyRight = site.SiteCopyRight;
                this.Config.SiteStatus = site.SiteStatus;
                string s = HttpContext.Request.Form["SiteStatus"].ToString();
                this.siteConfigService.SaveConfig(this.Config, Model.Sys.Enums.Constant.SiteConfigPath);

                if (Constant.CacheKey.List[Constant.CacheKey.SiteConfigCacheKey].Count() > 0)
                {
                    HttpRuntime.Cache.Remove(Constant.CacheKey.SiteConfigCacheKey);
                }
                return Json(new
                {
                    pass = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e){
                return Json(new
                {
                    pass = false,
                    msg = e.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult SysImage() 
        {
            return View();
        }
        public ActionResult SysDefaultAvatar()
        {
            var avatarFile = Request.Files["SysDefaultAvatar"];
            if (avatarFile != null && avatarFile.ContentLength > 0)
            {
                if (avatarFile.ContentLength <= MyPath.fileSize * 1024)
                {
                    var avatarName = avatarFile.FileName;
                    var avatarExt = Path.GetExtension(avatarName);

                    if (!String.IsNullOrEmpty(avatarExt)
                        && Config.ImgExtensions.Contains(avatarExt))
                    {
                        if (!System.IO.Directory.Exists(MyPath.TempPath))
                        {
                            System.IO.Directory.CreateDirectory(MyPath.TempPath);
                        }
                        //保存原图
                        var savePath = Path.Combine(MyPath.TempPath, avatarName);
                        avatarFile.SaveAs(savePath);
                        if (!System.IO.Directory.Exists(MyPath.SysPath))
                        {
                            System.IO.Directory.CreateDirectory(MyPath.SysPath);
                        }
                        //缩略图路径
                        var thumbPath = Path.Combine(MyPath.SysPath, "Avatar" + avatarExt);

                        //生成头像缩略图
                        ImageHelper.MakeThumbnailImage(savePath, thumbPath, 48, 48, "HW");
                        System.IO.File.Delete(savePath);

                        this.Config.DefaultAvatar = MyPath.Web_SysPath+"/DefaultAvatar" + avatarExt;
                        this.siteConfigService.SaveConfig(this.Config, Model.Sys.Enums.Constant.SiteConfigPath);

                        if (Constant.CacheKey.List[Constant.CacheKey.SiteConfigCacheKey].Count() > 0)
                        {
                            HttpRuntime.Cache.Remove(Constant.CacheKey.SiteConfigCacheKey);
                        }

                        return Json(new { url = this.Config.DefaultAvatar, pass = true });
                    }
                    else
                    {
                        return Json(new { error = "上传文件类型错误", pass = false });
                    }
                }
                else
                {
                    return Json(new { error = "上传文件大小超出限制,200k以内", pass = false });
                }
            }
            else {
                return Json(new { error = "上传文件错误", pass = false });
            }
        }
        public ActionResult SysSiteLogo()
        {

            var avatarFile = Request.Files["SysSiteLogo"];
            if (avatarFile != null && avatarFile.ContentLength > 0)
            {
                if (avatarFile.ContentLength <= MyPath.fileSize * 1024)
                {
                    var avatarName = avatarFile.FileName;
                    var avatarExt = Path.GetExtension(avatarName);

                    if (!String.IsNullOrEmpty(avatarExt)
                        && Config.ImgExtensions.Contains(avatarExt))
                    {
                        if (!System.IO.Directory.Exists(MyPath.SysPath))
                        {
                            System.IO.Directory.CreateDirectory(MyPath.SysPath);
                        }
                        //保存原图
                        var savePath = Path.Combine(MyPath.SysPath, "SiteLogo" + avatarExt);
                        avatarFile.SaveAs(savePath);

                        this.Config.SiteLogo = MyPath.Web_SysPath + "/SiteLogo" + avatarExt;
                        this.siteConfigService.SaveConfig(this.Config, Model.Sys.Enums.Constant.SiteConfigPath);

                        if (Constant.CacheKey.List[Constant.CacheKey.SiteConfigCacheKey].Count() > 0)
                        {
                            HttpRuntime.Cache.Remove(Constant.CacheKey.SiteConfigCacheKey);
                        }

                        return Json(new { url = this.Config.SiteLogo, pass = true });
                    }
                    else
                    {
                        return Json(new { error = "上传文件类型错误", pass = false });
                    }
                }
                else
                {
                    return Json(new { error = "上传文件大小超出限制,200k以内", pass = false });
                }
            }
            else
            {
                return Json(new { error = "上传文件错误", pass = false });
            }
        }
        //清理全部缓存
        [HttpGet]
        public ActionResult CacheClear()
        {
            //清理全局缓存
            List<string> keys = new List<string>();
            // retrieve application Cache enumerator
            //检索应用程序缓存计数器
            IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator();
            //得到所有的键值
            while (enumerator.MoveNext())
            {
                keys.Add(enumerator.Key.ToString());
            }
            // 删除对应缓存
            for (int i = 0; i < keys.Count; i++)
            {
                HttpRuntime.Cache.Remove(keys[i]);
            }
            return Json(new
            {
                success = true,
                msg = "清理成功"
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CacheClearKey(string key)
        {
            try
            {
                if (Constant.CacheKey.List[key].Count() > 0)
                {
                    HttpRuntime.Cache.Remove(key);
                    return Json(new
                    {
                        success = true,
                        msg = "清理成功"
                    }, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(new
                    {
                        success = false,
                        msg = "清理失败"
                    }, JsonRequestBehavior.AllowGet);
                }
                
            }
            catch {
                return Json(new
                {
                    success = false,
                    msg = "清理失败"
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult CacheManage()
        {
            ViewData["CacheKey"] = Constant.CacheKey.List;
            return View();
        }
        #endregion

    }
}
