using Cactus.Model.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cactus.IService
{
    public interface ISiteConfigService
    {
        void SaveConfig(SiteConfig config, string configPath);

        SiteConfig LoadConfig(string configPath);
    }
}
