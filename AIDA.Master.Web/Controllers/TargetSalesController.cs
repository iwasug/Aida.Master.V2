using AIDA.Master.Infrastucture.Constants;
using AIDA.Master.Service.Businesses;
using AIDA.Master.Service.Localizations;
using AIDA.Master.Service.Models;
using AIDA.Master.Web.Attributes;
using AIDA.Master.Web.Filters;
using Radyalabs.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AIDA.Master.Web.Controllers
{
    [AuthenticationActionFilter(ModuleCode.MasterSalesTarget)]
    public class TargetSalesController : Controller
    {
        // GET: TargetSales
        public ActionResult Index()
        {
            TargetSalesBusiness business = new TargetSalesBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            ViewBag.ListRayon = business.GetRayonSalesByUserAuth();

            ViewBag.IsEditable = business.IsEditable();

            return View();
        }

        [HttpPost]
        public ActionResult DatatableIndex(TargetSalesDatatableViewModel model)
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("Index");
            }

            JDatatableResponse resp = new JDatatableResponse();

            TargetSalesBusiness business = new TargetSalesBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            resp = business.GetDatatable(model);

            return new MyJsonResult(resp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ExportTargetSales(TargetSalesDatatableViewModel model)
        {
            TargetSalesBusiness business = new TargetSalesBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            AlertMessage alert = business.ExportTargetSalesToExcel(model);

            if (alert.Status == 1)
            {
                var bytes = alert.Data as byte[];

                return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, string.Format("TargetSales-{0}.xlsx", DateTime.UtcNow.ToUtcID().ToString("yyyyMMdd-HHmm")));
            }

            return RedirectToAction("Index");
        }

        public ActionResult ImportTargetSales()
        {
            TargetSalesBusiness business = new TargetSalesBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            //if (!business.IsEditable())
            //{
            //    TempData["AlertMessage"] = new AlertMessage(StaticMessage.ERR_ACCESS_DENIED);

            //    return RedirectToAction("Index");
            //}

            List<HierSalesViewModel> model = null;

            if (TempData["_Data"] != null)
            {
                model = TempData["_Data"] as List<HierSalesViewModel>;
            }

            return View(model);
        }

        public ActionResult DownloadTemplate()
        {
            TargetSalesBusiness business = new TargetSalesBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            DateTime today = DateTime.UtcNow.ToUtcID().Date;

            AlertMessage alert = business.DownloadTemplate(today.Month, today.Year);

            if (alert.Status == 1)
            {
                var bytes = alert.Data as byte[];

                return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, string.Format("TemplateTargetSales-{0}.xlsx", DateTime.UtcNow.ToUtcID().ToString("yyyyMMdd-HHmm")));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PreviewImportTargetSales(ImportTargetSalesViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            if (!ModelState.IsValid)
            {
                alert.Text = string.Join(System.Environment.NewLine, ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
            }
            else
            {
                TargetSalesBusiness business = new TargetSalesBusiness();
                business.SetUserAuth(ViewBag.UserAuth);

                alert = business.Preview(model);
            }

            TempData["AlertMessage"] = alert;

            if (alert.Data != null)
            {
                TempData["ImportTargetSales"] = alert.Data;

                return View(alert.Data);
            }
            else
            {
                return RedirectToAction("Import");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportTargetSales(ImportTargetSalesViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            model = null;

            if (TempData["ImportTargetSales"] != null)
            {
                model = TempData["ImportTargetSales"] as ImportTargetSalesViewModel;
            }
            else
            {
                alert.Text = StaticMessage.ERR_INVALID_INPUT;
            }

            if (model != null)
            {
                TargetSalesBusiness business = new TargetSalesBusiness();
                business.SetUserAuth(ViewBag.UserAuth);

                alert = business.ImportTargetSales(model);
            }

            TempData["AlertMessage"] = alert;

            return RedirectToAction("Index");
        }
    }
}