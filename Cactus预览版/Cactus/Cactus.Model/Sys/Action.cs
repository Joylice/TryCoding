using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cactus.Model.Sys
{
    public class Action
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Action_Id { get; set; }

        /// <summary>
        /// 操作名
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 操作的链接
        /// </summary>
        public string ActionUrl { get; set; }
    }
}
