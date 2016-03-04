using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cactus.SQLiteService;
using Cactus.MSSQLService;
using Cactus.MySQLService;
using Cactus.Model;
using Cactus.Common;


namespace Cactus.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //sqlite
            sqlite_Test();
            //mysql
            //mysql_Test();
            //mssql
            //mssql_Test();
        }
        #region sqlite
        static Cactus.SQLiteService.UserServer sqlite_user = new SQLiteService.UserServer();
        static Cactus.SQLiteService.RoleServer sqlite_role = new SQLiteService.RoleServer();
        static Cactus.SQLiteService.ActionServer sqlite_action = new SQLiteService.ActionServer();
        static Cactus.SQLiteService.PowerConfigService sqlite_power = new SQLiteService.PowerConfigService();
        static void sqlite_Test() {
            //sqlite_ActionGroupTest();
            //sqlite_ActionTest();
            //sqlite_RoleTest();
            //sqlite_UserTest();
            Model.Sys.PowerConfig p=new Model.Sys.PowerConfig();
            p.PowerGroupList = new List<Model.Sys.PowerGroup>();
            List<Model.Sys.Power> pl = new List<Model.Sys.Power>();
            pl.Add(new Model.Sys.Power() { Name = "角色管理", ParamStr = "/Admin/Sys/RoleList", IsShow = true, GroupId = 101, Id = 1001 });
            pl.Add(new Model.Sys.Power() { Name = "添加角色", ParamStr = "", IsShow = false, GroupId = 101, Id = 1002 });
            pl.Add(new Model.Sys.Power() { Name = "更新角色", ParamStr = "", IsShow = false, GroupId = 101, Id = 1003 });
            pl.Add(new Model.Sys.Power() { Name = "删除角色", ParamStr = "", IsShow = false, GroupId = 101, Id = 1004 });
            pl.Add(new Model.Sys.Power() { Name = "权限分配", ParamStr = "", IsShow = true, GroupId = 101, Id = 1005 });
            p.PowerGroupList.Add(new Model.Sys.PowerGroup() { GroupName = "网站设置", Id = 101, PowerList = pl,IsShow=true });
            p.PowerGroupList.Add(new Model.Sys.PowerGroup() { GroupName = "博客管理", Id = 100, PowerList = pl, IsShow = true });

            sqlite_power.SaveConfig(p, @"D:\vs2010Workspace\Cactus\PowerConfig.config");
            Console.WriteLine("1111");
            Console.ReadKey();
        }
        static void sqlite_ActionTest()
        {
            try
            {
                Console.WriteLine("ActionTest Test");
                sqlite_action.Insert(new Model.Sys.Action()
                { 
                    ActionName="TestAction1", ActionUrl="www.hao123.com"
                });
                sqlite_action.Insert(new Model.Sys.Action()
                {
                    ActionName = "TestAction2",
                    ActionUrl = "www.hao123.com"
                });
                sqlite_action.Delete("2");
                sqlite_action.Update(new Model.Sys.Action()
                {
                    ActionName = "TestAction1111",
                    ActionUrl = "www.hao123.com",
                    Action_Id=1
                });
                Model.Sys.Action ac = sqlite_action.Find(1);
                Console.WriteLine("Action_Id:" + ac.Action_Id);
                Console.WriteLine("ActionName:" + ac.ActionName);
                Console.WriteLine("ActionUrl:" + ac.ActionUrl);
                Console.WriteLine("ActionTest Ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ActionTest Error");
                ConsoleError(ex);
            }
            
        }
        static void sqlite_RoleTest()
        {
            try
            {
                Console.WriteLine("RoleTest Test");
                sqlite_role.Insert(new Model.Sys.Role()
                { 
                    RoleName="RoleTest1", ActionIds=""
                });
                sqlite_role.Insert(new Model.Sys.Role()
                {
                    RoleName = "RoleTest2",
                    ActionIds = ""
                });
                sqlite_role.Delete("2");
                sqlite_role.Update(new Model.Sys.Role()
                {
                    RoleName = "RoleTest2222",
                    ActionIds = "1,2,3",
                    Role_Id=1
                });
                Model.Sys.Role re = sqlite_role.Find(1);
                Console.WriteLine("Role_Id:" + re.Role_Id);
                Console.WriteLine("RoleName:" + re.RoleName);
                Console.WriteLine("ActionIds:" + re.ActionIds);
                Console.WriteLine("RoleTest Ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine("RoleTest Error");
                ConsoleError(ex);
            }
        }
        static void sqlite_UserTest()
        {
            
            try
            {
                Console.WriteLine("UserTest Test");
                //sqlite_user.Insert(new Model.Sys.User()
                //{
                //    AddTime = DateTime.Now,
                //    Avatar = "/image.png",
                //    Email = "702295399@qq.com",
                //    IsSuperUser = true,
                //    LastLoginIp = "127.0.0.1",
                //    LastLoginTime = DateTime.Now,
                //    Name = "702295399@qq.com",
                //    NickName = "漫漫洒洒2",
                //    Password = CryptoHelper.MD5Encrypt("123456789"),
                //    Phone = "138888888888",
                //    Qq = "702295399",
                //    RoleId = 1
                //});
                //sqlite_user.Delete("2");
                sqlite_user.Update(new Model.Sys.User()
                {
                    AddTime = DateTime.Now,
                    Avatar = "/image.png",
                    Email = "702295399@qq.com",
                    IsSuperUser = false,
                    LastLoginIp = "127.0.0.1",
                    LastLoginTime = DateTime.Now,
                    Name = "702295399@qq.com",
                    NickName = "漫漫洒洒1111",
                    Password = CryptoHelper.MD5Encrypt("123456789"),
                    Phone = "138888888888",
                    Qq = "702295399",
                    RoleId = 1,
                    User_Id = 1
                });

                //Model.Sys.User ur = sqlite_user.Find(1);
                //int count=0;
                
                //List<Model.Sys.User> list=sqlite_user.ToPagedList(10, 20, "User_Id desc", out count);
                //foreach (var ur in list) {
                //    Console.WriteLine("User_Id:" + ur.User_Id 
                //        + "\t" + "Avatar:" + ur.Avatar
                //        + "\t" + "Email:" + ur.Email
                //        + "\t" + "IsSuperUser:" + ur.IsSuperUser
                //        + "\t" + "LastLoginIp:" + ur.LastLoginIp
                //        + "\t" + "LastLoginTime:" + ur.LastLoginTime
                //        + "\t" + "Name:" + ur.Name
                //        + "\t" + "NickName:" + ur.NickName
                //        + "\t" + "Password:" + ur.Password
                //        + "\t" + "Phone:" + ur.Phone
                //        + "\t" + "Qq:" + ur.Qq
                //        + "\t" + "Role_Id:" + ur.Role.Role_Id
                //        + "\t" + "RoleName:" + ur.Role.RoleName);
                //}

                Console.WriteLine("UserTest Ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine("UserTest Error");
                ConsoleError(ex);
            }
        }

        #endregion

        #region mysql
        static Cactus.MySQLService.UserServer mysql_user = new MySQLService.UserServer();
        static Cactus.MySQLService.RoleServer mysql_role = new MySQLService.RoleServer();
        static Cactus.MySQLService.ActionServer mysql_action = new MySQLService.ActionServer();
        static void mysql_Test()
        {
            //mysql_ActionGroupTest();
            //mysql_ActionTest();
            //mysql_RoleTest();
            mysql_UserTest();
            Console.ReadKey();
        }
        static void mysql_ActionTest()
        {
            try
            {
                Console.WriteLine("mysql_ActionTest Test");
                mysql_action.Insert(new Model.Sys.Action()
                {
                    ActionName = "TestAction1",
                    ActionUrl = "www.hao123.com"
                });
                mysql_action.Insert(new Model.Sys.Action()
                {
                    ActionName = "TestAction2",
                    ActionUrl = "www.hao123.com"
                });
                mysql_action.Delete("2");
                mysql_action.Update(new Model.Sys.Action()
                {
                    ActionName = "TestAction1111",
                    ActionUrl = "www.hao123.com",
                    Action_Id = 1
                });
                Model.Sys.Action ac = mysql_action.Find(1);
                Console.WriteLine("Action_Id:" + ac.Action_Id);
                Console.WriteLine("ActionName:" + ac.ActionName);
                Console.WriteLine("ActionUrl:" + ac.ActionUrl);
                Console.WriteLine("mysql_ActionTest Ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine("mysql_ActionTest Error");
                ConsoleError(ex);
            }

        }
        static void mysql_RoleTest()
        {
            try
            {
                Console.WriteLine("mysql_RoleTest Test");
                mysql_role.Insert(new Model.Sys.Role()
                {
                    RoleName = "RoleTest1",
                    ActionIds = ""
                });
                mysql_role.Insert(new Model.Sys.Role()
                {
                    RoleName = "RoleTest2",
                    ActionIds = ""
                });
                mysql_role.Delete("2");
                mysql_role.Update(new Model.Sys.Role()
                {
                    RoleName = "RoleTest2222",
                    ActionIds = "1,2,3",
                    Role_Id = 1
                });
                Model.Sys.Role re = mysql_role.Find(1);
                Console.WriteLine("Role_Id:" + re.Role_Id);
                Console.WriteLine("RoleName:" + re.RoleName);
                Console.WriteLine("ActionIds:" + re.ActionIds);
                Console.WriteLine("mysql_RoleTest Ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine("mysql_RoleTest Error");
                ConsoleError(ex);
            }
        }
        static void mysql_UserTest()
        {

            try
            {
                Console.WriteLine("mysql_UserTest Test");
                mysql_user.Insert(new Model.Sys.User()
                {
                    AddTime = DateTime.Now,
                    Avatar = "/image.png",
                    Email = "702295399@qq.com",
                    IsSuperUser = true,
                    LastLoginIp = "127.0.0.1",
                    LastLoginTime = DateTime.Now,
                    Name = "702295399@qq.com",
                    NickName = "漫漫洒洒",
                    Password = "123456789",
                    Phone = "138888888888",
                    Qq = "702295399",
                    RoleId = 1
                });
                mysql_user.Insert(new Model.Sys.User()
                {
                    AddTime = DateTime.Now,
                    Avatar = "/image.png",
                    Email = "702295399@qq.com",
                    IsSuperUser = true,
                    LastLoginIp = "127.0.0.1",
                    LastLoginTime = DateTime.Now,
                    Name = "702295399@qq.com",
                    NickName = "漫漫洒洒2",
                    Password = "123456789",
                    Phone = "138888888888",
                    Qq = "702295399",
                    RoleId = 1
                });
                mysql_user.Delete("2");
                mysql_user.Update(new Model.Sys.User()
                {
                    AddTime = DateTime.Now,
                    Avatar = "/image.png",
                    Email = "702295399@qq.com",
                    IsSuperUser = false,
                    LastLoginIp = "127.0.0.1",
                    LastLoginTime = DateTime.Now,
                    Name = "702295399@qq.com",
                    NickName = "漫漫洒洒1111",
                    Password = "123456789",
                    Phone = "138888888888",
                    Qq = "702295399",
                    RoleId = 1,
                    User_Id = 1
                });

                Model.Sys.User ur = mysql_user.Find(1);
                Console.WriteLine("User_Id:" + ur.User_Id);
                Console.WriteLine("Avatar:" + ur.Avatar);
                Console.WriteLine("Email:" + ur.Email);
                Console.WriteLine("IsSuperUser:" + ur.IsSuperUser);
                Console.WriteLine("LastLoginIp:" + ur.LastLoginIp);
                Console.WriteLine("LastLoginTime:" + ur.LastLoginTime);
                Console.WriteLine("Name:" + ur.Name);
                Console.WriteLine("NickName:" + ur.NickName);
                Console.WriteLine("Password:" + ur.Password);
                Console.WriteLine("Phone:" + ur.Phone);
                Console.WriteLine("Qq:" + ur.Qq);
                Console.WriteLine("Role_Id:" + ur.Role.Role_Id);
                Console.WriteLine("RoleName:" + ur.Role.RoleName);
                Console.WriteLine("mysql_UserTest Ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine("mysql_UserTest Error");
                ConsoleError(ex);
            }
        }
        
        #endregion

        #region mssql
        static Cactus.MSSQLService.UserServer mssql_user = new MSSQLService.UserServer();
        static Cactus.MSSQLService.RoleServer mssql_role = new MSSQLService.RoleServer();
        static Cactus.MSSQLService.ActionServer mssql_action = new MSSQLService.ActionServer();
        static void mssql_Test()
        {
            //mssql_ActionGroupTest();
            //mssql_ActionTest();
            //mssql_RoleTest();
            mssql_UserTest();
            Console.ReadKey();
        }
        static void mssql_ActionTest()
        {
            try
            {
                Console.WriteLine("mssql_ActionTest Test");
                mssql_action.Insert(new Model.Sys.Action()
                {
                    ActionName = "TestAction1",
                    ActionUrl = "www.hao123.com"
                });
                mssql_action.Insert(new Model.Sys.Action()
                {
                    ActionName = "TestAction2",
                    ActionUrl = "www.hao123.com"
                });
                mssql_action.Delete("2");
                mssql_action.Update(new Model.Sys.Action()
                {
                    ActionName = "TestAction1111",
                    ActionUrl = "www.hao123.com",
                    Action_Id = 1
                });
                Model.Sys.Action ac = mssql_action.Find(1);
                Console.WriteLine("Action_Id:" + ac.Action_Id);
                Console.WriteLine("ActionName:" + ac.ActionName);
                Console.WriteLine("ActionUrl:" + ac.ActionUrl);
                Console.WriteLine("mssql_ActionTest Ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine("mssql_ActionTest Error");
                ConsoleError(ex);
            }

        }
        static void mssql_RoleTest()
        {
            try
            {
                Console.WriteLine("mssql_RoleTest Test");
                mssql_role.Insert(new Model.Sys.Role()
                {
                    RoleName = "RoleTest1",
                    ActionIds = ""
                });
                mssql_role.Insert(new Model.Sys.Role()
                {
                    RoleName = "RoleTest2",
                    ActionIds = ""
                });
                mssql_role.Delete("2");
                mssql_role.Update(new Model.Sys.Role()
                {
                    RoleName = "RoleTest2222",
                    ActionIds = "1,2,3",
                    Role_Id = 1
                });
                Model.Sys.Role re = mssql_role.Find(1);
                Console.WriteLine("Role_Id:" + re.Role_Id);
                Console.WriteLine("RoleName:" + re.RoleName);
                Console.WriteLine("ActionIds:" + re.ActionIds);
                Console.WriteLine("mssql_RoleTest Ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine("mssql_RoleTest Error");
                ConsoleError(ex);
            }
        }
        static void mssql_UserTest()
        {

            try
            {
                Console.WriteLine("mssql_UserTest Test");
                mssql_user.Insert(new Model.Sys.User()
                {
                    AddTime = DateTime.Now,
                    Avatar = "/image.png",
                    Email = "702295399@qq.com",
                    IsSuperUser = true,
                    LastLoginIp = "127.0.0.1",
                    LastLoginTime = DateTime.Now,
                    Name = "702295399@qq.com",
                    NickName = "漫漫洒洒",
                    Password = "123456789",
                    Phone = "138888888888",
                    Qq = "702295399",
                    RoleId = 1
                });
                mssql_user.Insert(new Model.Sys.User()
                {
                    AddTime = DateTime.Now,
                    Avatar = "/image.png",
                    Email = "702295399@qq.com",
                    IsSuperUser = true,
                    LastLoginIp = "127.0.0.1",
                    LastLoginTime = DateTime.Now,
                    Name = "702295399@qq.com",
                    NickName = "漫漫洒洒2",
                    Password = "123456789",
                    Phone = "138888888888",
                    Qq = "702295399",
                    RoleId = 1
                });
                mssql_user.Delete("2");
                mssql_user.Update(new Model.Sys.User()
                {
                    AddTime = DateTime.Now,
                    Avatar = "/image.png",
                    Email = "702295399@qq.com",
                    IsSuperUser = false,
                    LastLoginIp = "127.0.0.1",
                    LastLoginTime = DateTime.Now,
                    Name = "702295399@qq.com",
                    NickName = "漫漫洒洒1111",
                    Password = "123456789",
                    Phone = "138888888888",
                    Qq = "702295399",
                    RoleId = 1,
                    User_Id = 1
                });

                Model.Sys.User ur = mssql_user.Find(1);
                Console.WriteLine("User_Id:" + ur.User_Id);
                Console.WriteLine("Avatar:" + ur.Avatar);
                Console.WriteLine("Email:" + ur.Email);
                Console.WriteLine("IsSuperUser:" + ur.IsSuperUser);
                Console.WriteLine("LastLoginIp:" + ur.LastLoginIp);
                Console.WriteLine("LastLoginTime:" + ur.LastLoginTime);
                Console.WriteLine("Name:" + ur.Name);
                Console.WriteLine("NickName:" + ur.NickName);
                Console.WriteLine("Password:" + ur.Password);
                Console.WriteLine("Phone:" + ur.Phone);
                Console.WriteLine("Qq:" + ur.Qq);
                Console.WriteLine("Role_Id:" + ur.Role.Role_Id);
                Console.WriteLine("RoleName:" + ur.Role.RoleName);
                Console.WriteLine("mssql_UserTest Ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine("mssql_UserTest Error");
                ConsoleError(ex);
            }
        }
        #endregion

        static void ConsoleError(Exception ex)
        {
            Console.WriteLine("Data:" + ex.Data);
            Console.WriteLine("InnerException:" + ex.InnerException);
            Console.WriteLine("Message:" + ex.Message);
            Console.WriteLine("Source:" + ex.Source);
            Console.WriteLine("StackTrace:" + ex.StackTrace);
            Console.WriteLine("TargetSite:" + ex.TargetSite);
        }
    }
}
