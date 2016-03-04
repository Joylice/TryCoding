using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cactus.Model.Sys.Enums
{
    public class MyPath
    {
        public static string AppPath = AppDomain.CurrentDomain.BaseDirectory;

        public static string AvatarPath = AppPath + @"Upload\Avatar";

        public static string Web_AvatarPath = "/Upload/Avatar";

        public static string SysPath = AppPath + @"Upload\Sys";

        public static string Web_SysPath = "/Upload/Sys";

        public static string TempPath = AppPath + @"Upload\Temp";

        public static int fileSize = 200;

    }
}
