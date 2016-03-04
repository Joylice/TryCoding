using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cactus.Model.Sys
{
    public class PowerConfig
    {
        public List<PowerGroup> PowerGroupList { get; set; }
        //public List<Power> PowerList { get; set; }
    }
    public class Power {
        public string ParamStr { get; set; }
        public string Name { get; set; }
        public bool IsShow { get; set; }
        public int GroupId { get; set; }
        public int Id { get; set; }
    }
    public class PowerGroup
    {
        public string GroupName { get; set; }
        public bool IsShow { get; set; }
        public int Id { get; set; }
        public List<Power> PowerList { get; set; }
    }
}
