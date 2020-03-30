using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace Radyalabs.Core.Helper
{
    public class SiteHelper
    {
        public static string GetBaseUrl()
        {
            string port = System.Configuration.ConfigurationManager.AppSettings.Get("WebPort");
            string hostname = System.Configuration.ConfigurationManager.AppSettings.Get("WebHostName");
            string rootSegment = System.Configuration.ConfigurationManager.AppSettings.Get("WebRootSegment");

            string urlAuthority = HttpContext.Current.Request.Url.Authority;
            string applicationPath = HttpContext.Current.Request.ApplicationPath;

            string[] parts = urlAuthority.Split(':');

            //string baseUrl = System.Web.HttpContext.Current.Request.Url.Scheme + "://"
            //    + HttpContext.Current.Request.Url.Authority 
            //    + System.Configuration.ConfigurationManager.AppSettings.Get("WebPort")
            //    + HttpContext.Current.Request.ApplicationPath.TrimEnd('/')
            //    + "/";


            string baseUrl = System.Web.HttpContext.Current.Request.Url.Scheme + "://";

            if (urlAuthority.Equals(hostname + ":" + port) && !string.IsNullOrEmpty(rootSegment))
            {
                baseUrl = "/" + rootSegment;
            }
            else if (!string.IsNullOrEmpty(port) && parts.Length > 1 && port.Equals(parts[1]))
            {
                baseUrl += urlAuthority;
            }
            else {
                baseUrl += urlAuthority
                    + (string.IsNullOrEmpty(port) ? "" : ":" + port);
            }

            baseUrl = baseUrl + applicationPath.TrimEnd('/') + "/";

            return baseUrl;
        }

        public static string GetBaseUrl2()
        {
            // variables  
            string Authority = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority).TrimStart('/').TrimEnd('/');
            string ApplicationPath = System.Web.HttpContext.Current.Request.ApplicationPath.TrimStart('/').TrimEnd('/');

            // add trailing slashes if necessary  
            if (Authority.Length > 0)
            {
                Authority += "/";
            }

            if (ApplicationPath.Length > 0)
            {
                ApplicationPath += "/";
            }

            // return  
            return string.Format("{0}{1}", Authority, ApplicationPath);
        }

        public static string GetCurrentUrl()
        {
            return HttpContext.Current.Request.Url.AbsoluteUri;
        }
    }
}
