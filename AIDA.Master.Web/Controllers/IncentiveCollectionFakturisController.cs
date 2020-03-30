using AIDA.Master.Infrastucture.Constants;
using AIDA.Master.Service.Businesses;
using AIDA.Master.Service.Models;
using AIDA.Master.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AIDA.Master.Web.Controllers
{
    [AuthenticationActionFilter(ModuleCode.IncentiveCollectionFakturis)]
    public class IncentiveCollectionFakturisController : Controller
    {
        public ActionResult Index()
        {
            IncentiveCollectionBusiness business = new IncentiveCollectionBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            IncentiveCollectionReportViewModel model = new IncentiveCollectionReportViewModel();

            ViewBag.ListBranch = business.GetListBranch();

            return View(model);
        }
        
        [HttpPost]
        public ActionResult Index(string p, string b)
        {
            IncentiveCollectionBusiness business = new IncentiveCollectionBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            IncentiveCollectionReportViewModel model = business.GetReportFakturis(p, b);

            ViewBag.ListBranch = business.GetListBranch();

            return View(model);
        }

        [HttpPost]
        public ActionResult ExportReport(string p, string b)
        {
            IncentiveCollectionBusiness business = new IncentiveCollectionBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            AlertMessage alert = business.ExportReportFakturis(p, b);

            if (alert.Status == 1)
            {
                var bytes = alert.Data as byte[];

                return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, string.Format("IncentiveCollection-Fakturis-{0}.xlsx", p));
            }

            return RedirectToAction("Index");
        }
    }
}