using AIDA.Master.Infrastucture.Constants;
using AIDA.Master.Service.Businesses;
using AIDA.Master.Service.Localizations;
using AIDA.Master.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AIDA.Master.Web.Filters
{
    public class AuthenticationActionFilter : ActionFilterAttribute, IActionFilter, IExceptionFilter
    {
        protected UserAuthenticated userAuth;

        protected string _module;

        public AuthenticationActionFilter(string module = null)
        {
            _module = module;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
                filterContext.HttpContext.Trace.Write("(Controller)Action Executing: " +
                filterContext.ActionDescriptor.ActionName);

            base.OnActionExecuting(filterContext);
            var url = new UrlHelper(filterContext.RequestContext);

            var viewBag = filterContext.Controller.ViewBag;

            AccountBusiness business = new AccountBusiness();

            try
            {
                userAuth = business.GetUserAuth();

                if (userAuth != null)
                {

                    if (!userAuth.IsActive)
                    {
                        business.Logout();

                        filterContext.Controller.TempData["AlertMessage"] = new AlertMessage(StaticMessage.ERR_USER_INACTIVE);
                        filterContext.Result = new RedirectResult(url.Action("Login", "Account"));
                    }
                    else if (! userAuth.IsRoleValid)
                    {
                        business.Logout();

                        filterContext.Controller.TempData["AlertMessage"] = new AlertMessage(StaticMessage.ERR_ROLE_INVALID);
                        filterContext.Result = new RedirectResult(url.Action("Login", "Account"));
                    }
                    else
                    {
                        viewBag.UserAuth = userAuth;

                        if (! IsModuleAllowed(userAuth.RoleCode))
                        {
                            filterContext.Controller.TempData["AlertMessage"] = new AlertMessage(StaticMessage.ERR_ACCESS_DENIED);
                            filterContext.Result = new RedirectResult(url.Action("Index", "Dashboard"));
                        }
                    }
                }
                else
                {
                    business.Logout();

                    filterContext.Controller.TempData["AlertMessage"] = new AlertMessage(StaticMessage.ERR_SESSION_EXPIRED);
                    filterContext.Result = new RedirectResult(url.Action("Login", "Account"));
                }
            }
            catch (Exception ex)
            {
                business.Logout();

                filterContext.Result = new RedirectResult(url.Action("Login", "Account"));
            }
        }

        void IExceptionFilter.OnException(ExceptionContext context)
        {
            AccountBusiness business = new AccountBusiness();

            userAuth = business.GetUserAuth();

            if (userAuth != null)
            {
                context.Controller.ViewBag.UserAuth = userAuth;

                context.Result = new ViewResult
                {
                    ViewName = "~/Views/Error/Index500.cshtml",
                    TempData = context.Controller.TempData,
                    ViewData = context.Controller.ViewData
                };
            }
            else
            {
                business.Logout();

                var url = new UrlHelper(context.RequestContext);

                context.Controller.TempData["AlertMessage"] = new AlertMessage(StaticMessage.ERR_SESSION_EXPIRED);
                context.Result = new RedirectResult(url.Action("Login", "Account"));
            }
            
            context.ExceptionHandled = true;
        }

        private bool IsModuleAllowed(string roleCode)
        {
            if (string.IsNullOrEmpty(_module)) return true;

            if (ModuleCode.DcRole[_module].Exists(x => x.Equals(roleCode)))
            {
                return true;
            }
            else
            {
                return false;
            }

            //if (listModule != null && listModule.FirstOrDefault(x => x.Equals(_module)) != null)
            //{
            //    return true;
            //}

            //return false;
        }
    }
}