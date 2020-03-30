using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using AIDA.Master.Web.Filters;
using Radyalabs.Core.Helper;
using AIDA.Master.Service.Businesses;
using AIDA.Master.Service.Models;
using AIDA.Master.Infrastucture.Data;
using System;
using AIDA.Master.Service.Identities;

namespace AIDA.Master.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser, Guid> _userManager;

        public AccountController() { }

        public AccountController(UserManager<IdentityUser, Guid> userManager)
        {
            _userManager = userManager;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            string accountId = AppCookieHelper.Get<string>();

            if (!string.IsNullOrEmpty(accountId))
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            AccountBusiness accountBO = new AccountBusiness(_userManager);

            AlertMessage alert = new AlertMessage();

            if (!ModelState.IsValid)
            {
                alert.Text = string.Join(System.Environment.NewLine, ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
            }
            else
            {
                alert = accountBO.Login(model);
            }

            if (alert.Status == 1)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            TempData["AlertMessage"] = alert;

            return View(model);
        }

        [Authorize]
        public ActionResult Logout(int exp = 0)
        {
            new AccountBusiness().Logout();

            return RedirectToAction("Login");
        }
    }
}