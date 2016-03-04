using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cactus.Common
{
    public class IOHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteLog(Exception ex)
        {
            WriteLog("Data:" + ex.Data
                + " InnerException:" + ex.InnerException
                + " Message:" + ex.Message
                + " Source:" + ex.Source
                + " StackTrace:" + ex.StackTrace
                + " TargetSite:" + ex.TargetSite);
        }
        /// <summary>
        /// 写log
        /// </summary>
        /// <param name="InfoStr"></param>
        public static void WriteLog(string InfoStr)
        {
            try
            {
                string logPath = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "Log";
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }
                string filepath = logPath + Path.DirectorySeparatorChar + "log_" + DateTime.Now.ToString("yyyyMMddHH") + ".txt";
                System.IO.StreamWriter sw = new System.IO.StreamWriter(filepath, true, System.Text.Encoding.UTF8);
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                sw.WriteLine(InfoStr);
                sw.WriteLine("");
                sw.Close();
                sw.Dispose();
            }
            catch
            {

            }
        }

    }
}
