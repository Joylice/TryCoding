using Cactus.Model.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cactus.IService
{
    public interface IPowerConfigService
    {
        PowerConfig LoadConfig(string configPath);
        void SaveConfig(PowerConfig config, string configPath);
    }
}
