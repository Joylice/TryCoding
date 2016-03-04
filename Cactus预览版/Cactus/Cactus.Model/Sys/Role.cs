using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cactus.Model.Sys
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Role_Id { get; set; }

        /// <summary>
        /// 管理员角色
        /// </summary>
        public virtual  ICollection<User> Users { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName { get; set; }

        public string ActionIds { get; set; }
    }
}
