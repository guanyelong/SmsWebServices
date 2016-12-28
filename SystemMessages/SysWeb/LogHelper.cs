using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: log4net.Config.XmlConfigurator()]
namespace SysWeb
{
    public class LogHelper
    {
        public static ILog Logger { get; set; }

        static LogHelper()
        {
            LogHelper.Logger = LogManager.GetLogger("service");
        }

        public static void Info(string p)
        {
            LogHelper.Logger.Info(p);
        }

        public static void Error(Exception exception)
        {
            LogHelper.Logger.Error(exception.Message + exception.StackTrace);
        }

        public static void Error(string userInfo, Exception exception)
        {

            LogHelper.Logger.Error(userInfo + ":" + exception.Message + exception.StackTrace);
        }

        public static void Debug(string p)
        {
            LogHelper.Logger.Debug(p);
        }  
    }
}