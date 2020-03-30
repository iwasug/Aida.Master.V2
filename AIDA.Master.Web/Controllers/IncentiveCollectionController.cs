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
    [AuthenticationActionFilter(ModuleCode.IncentiveCollection)]
    public class IncentiveCollectionController : Controller
    {
        public ActionResult Index(string p, int? b)
        {
            IncentiveCollectionBusiness business = new IncentiveCollectionBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            if (RoleCode.NSM.Equals(ViewBag.UserAuth.RoleCode))
            {
                ViewBag.ListBranch = business.GetListBranch();
            }

            IncentiveCollectionReportViewModel model = business.GetReport(p, b);

            return View(model);
        }

        [HttpPost]
        public ActionResult ExportReport(string p, int? b)
        {
            IncentiveCollectionBusiness business = new IncentiveCollectionBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            AlertMessage alert = business.ExportReport(p, b);

            if (alert.Status == 1)
            {
                var bytes = alert.Data as byte[];

                return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, string.Format("IncentiveCollection-{0}.xlsx", p));
            }

            return RedirectToAction("Index");
        }

        public ActionResult Import()
        {
            IncentiveCollectionBusiness business = new IncentiveCollectionBusiness();
            business.SetUserAuth(ViewBag.UserAuth);
            return View();
        }


    }
}