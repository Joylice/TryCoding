using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Cactus.RazorWeb.Expand
{
    public static class ExpandController
    {
        
        /// <summary>
        /// 得到单条错误信息
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string GetModelErros(this ModelStateDictionary dic)
        {
            string errors = "";
            if (!dic.IsValid)
            {
                //获取第一个
                errors = dic.Keys.First<string>();
                
            }
            return errors;
        }
    }
}