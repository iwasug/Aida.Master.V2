using log4net;
using Radyalabs.Core.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Radyalabs.Core.Helper;
using System.Configuration;
using System.Data.SqlClient;

namespace Radyalabs.Core.LogHelper
{
    public class Log4NetHelper : ILogHelper
    {
        public Log4NetHelper()
        {
            
        }

        public void Write(string logtype, DateTime dt, string message, string createBy, Exception ex = null, bool isWriteTime = true)
        {
            string dir = (HttpContext.Current != null)
                ? HttpContext.Current.Server.MapPath("~/")
                : AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(dir,
                "Log",
                logtype,
                "Log_" + logtype + "_" + dt.ToString("yyMMdd") + ".txt");

            log4net.GlobalContext.Properties["VarLogFilePath"] = path;
            log4net.Config.XmlConfigurator.Configure();

            ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            string tempMessage = "";

            if (isWriteTime)
            {
                tempMessage = System.Environment.NewLine
                    + "***** " + string.Format("[{0}] ", logtype) + dt.ToString("dddd, dd MMMM yyyy HH:mm:ss,ffff") + " *****"
                    + System.Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(message))
            {
                tempMessage += message;
            }
            Write2Db(logtype, DateTime.Now, message, ex, createBy);
            logger.Info(tempMessage, ex);
        }

        private void Write2Db(string logtype, DateTime dt, string message, Exception ex = null, string createdBy = null)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["_AIDAEntities"].ConnectionString;
                string query = "INSERT INTO TRACELOG(TYPE, MESSAGE, EX, CREATEDDATE, CREATEDBY) VALUES(@TYPE, @MESSAGE, @EX, GETDATE(), @CREATEDBY)";
                Dictionary<string, object> dcParams = new Dictionary<string, object>();
                dcParams["@TYPE"] = logtype;
                dcParams["@MESSAGE"] = message;
                dcParams["@EX"] = ex == null ? "" : ex.ToString();
                dcParams["@CREATEDBY"] = createdBy;
                SqlHelper.ExecuteQueryManipulation(connString, query, dcParams);
            }
            catch (Exception)
            {

            }
            
        }
    }
}
