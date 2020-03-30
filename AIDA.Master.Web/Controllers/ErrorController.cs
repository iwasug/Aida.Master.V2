using AIDA.Master.Service.Businesses;
using AIDA.Master.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AIDA.Master.Web.Controllers
{
    public class ErrorController : Controller
    {
        private UserAuthenticated _userAuth;

        public ErrorController()
        {
            AccountBusiness business = new AccountBusiness();

            _userAuth = business.GetUserAuth();
        }

        public ActionResult Index404()
        {
            if (_userAuth != null)
            {
                ViewBag.UserAuth = _userAuth;

                return View("Index404");
            }
            else
            {
                return View("Index404NoSidebar");
            }
        }

        public ActionResult Index500()
        {
            if (_userAuth != null)
            {
                ViewBag.UserAuth = _userAuth;

                return View("Index500");
            }
            else
            {
                return View("Index500NoSidebar");
            }
        }
    }
}