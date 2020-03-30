using Radyalabs.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AIDA.Master.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            UnityConfig.RegisterComponents();

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();

            HttpException httpException = exception as HttpException;

            if (httpException != null)
            {
                string action;

                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // page not found
                        action = "Index404";
                        break;
                    case 500:
                        // server error
                        action = "Index500";
                        break;
                    default:
                        action = "home";
                        break;
                }

                // clear error on server
                Server.ClearError();

                if ("home".Equals(action))
                {
                    Response.Redirect(SiteHelper.GetBaseUrl());
                }
                else
                {
                    Response.Redirect(String.Format("~/Error/{0}/?url={1}", action, HttpContext.Current.Request.Url.OriginalString));
                }
            }
        }
    }
}
