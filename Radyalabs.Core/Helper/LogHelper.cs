using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Radyalabs.Core.Helper
{
    public class LogHelper
    {
        public static void Write(DateTime dt, string logType, string message, Exception ex = null, bool isIncludeTime = true)
        {
            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/"), 
                "Log", 
                logType, 
                "Log_" + logType + "_" + dt.ToString("yyMMdd_HHmmss_fff") + ".txt");

            log4net.GlobalContext.Properties["VarLogFilePath"] = path;
            log4net.Config.XmlConfigurator.Configure();

            ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            string tempMessage = "";

            if (isIncludeTime)
            {
                tempMessage = dt.ToString("dddd, dd MMMM yyyy HH:mm:ss,ffff") 
                    + System.Environment.NewLine
                    + System.Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(message))
            {
                tempMessage += message;
            }

            logger.Info(tempMessage, ex);
        }
    }
}
