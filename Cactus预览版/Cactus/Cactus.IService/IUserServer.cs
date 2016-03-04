using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cactus.IService
{
    public interface IUserServer : IBaseService<Cactus.Model.Sys.User>
    {
        //
        Cactus.Model.Sys.User CheckLogin(string userName, string pwd);
        //
        bool AlterPwd(int id,string oldPwd,string newPwd);
        //
        bool AlterFace(int id,string picUrl);

    }
}
