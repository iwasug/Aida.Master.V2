using AIDA.Master.Infrastucture.Constants;
using AIDA.Master.Service.Businesses;
using AIDA.Master.Service.Models;
using AIDA.Master.Web.Filters;
using Radyalabs.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AIDA.Master.Web.Controllers
{
    [AuthenticationActionFilter(ModuleCode.IncentiveSales)]
    public class IncentiveSalesController : Controller
    {
        public ActionResult Index(string p, int? t = null, int? b = null)
        {
            IncentiveSalesBusiness business = new IncentiveSalesBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            if (RoleCode.NSM.Equals(ViewBag.UserAuth.RoleCode))
            {
                ViewBag.ListBranch = business.GetListBranch();
            }

            IncentiveSalesReportViewModel model = business.GetReport(p, t, b);

            return View(model);
        }

        [HttpPost]
        public ActionResult ExportReport(string p, int? t, int? b)
        {
            IncentiveSalesBusiness business = new IncentiveSalesBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            AlertMessage alert = business.ExportReport(p, t, b);

            if (alert.Status == 1)
            {
                var bytes = alert.Data as byte[];

                return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, string.Format("IncentiveSales-{0}.xlsx", p));
            }

            return RedirectToAction("Index");
        }
    }
}