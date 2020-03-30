using AIDA.Master.Infrastucture.Constants;
using AIDA.Master.Service.Businesses;
using AIDA.Master.Service.Localizations;
using AIDA.Master.Service.Models;
using AIDA.Master.Web.Attributes;
using AIDA.Master.Web.Filters;
using Radyalabs.Core.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AIDA.Master.Web.Controllers
{
    [AuthenticationActionFilter(ModuleCode.MasterTagihHier)]
    public class HierTagihController : Controller
    {
        public ActionResult Index()
        {
            HierTagihBusiness business = new HierTagihBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            ViewBag.IsEditable = business.IsEditable();

            return View();
        }

        [HttpPost]
        public ActionResult DatatableIndex(JDatatableViewModel model)
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("Index");
            }

            JDatatableResponse resp = new JDatatableResponse();

            HierTagihBusiness business = new HierTagihBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            resp = business.GetDatatableByQuery(model);

            return new MyJsonResult(resp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Rayon(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            ViewBag.Rayon = id;

            return View();
        }

        [HttpPost]
        public ActionResult DatatableRayon(RayonDatatableViewModel model)
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("Index");
            }

            JDatatableResponse resp = new JDatatableResponse();

            HierTagihBusiness business = new HierTagihBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            resp = business.GetDatatableCustomer(model);

            return new MyJsonResult(resp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ExportTagih(JDatatableViewModel model)
        {
            HierTagihBusiness business = new HierTagihBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            AlertMessage alert = business.ExportTagihToExcel(model);

            if (alert.Status == 1)
            {
                var bytes = alert.Data as byte[];

                return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, string.Format("HirarkiTagih-{0}.xlsx", DateTime.UtcNow.ToUtcID().ToString("yyyyMMdd-HHmm")));
            }

            return RedirectToAction("Index");
        }

        public ActionResult ImportTagih()
        {
            HierTagihBusiness business = new HierTagihBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            if (!business.IsEditable())
            {
                TempData["AlertMessage"] = new AlertMessage(StaticMessage.ERR_ACCESS_DENIED);

                return RedirectToAction("Index");
            }

            List<HierSalesViewModel> model = null;

            if (TempData["_Data"] != null)
            {
                model = TempData["_Data"] as List<HierSalesViewModel>;
            }

            return View(model);
        }

        public ActionResult PreviewImportTagih()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PreviewImportTagih(ImportTagihViewModel model)
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
                HierTagihBusiness business = new HierTagihBusiness();
                business.SetUserAuth(ViewBag.UserAuth);

                alert = business.Preview(model);
            }

            TempData["AlertMessage"] = alert;

            if (alert.Data != null)
            {
                TempData["ImportTagih"] = alert.Data;

                return View(alert.Data);
            }
            else
            {
                return RedirectToAction("ImportTagih");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportTagih(ImportTagihViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            model = null;

            if (TempData["ImportTagih"] != null)
            {
                model = TempData["ImportTagih"] as ImportTagihViewModel;
            }
            else
            {
                alert.Text = StaticMessage.ERR_INVALID_INPUT;
            }

            if (model != null)
            {
                HierTagihBusiness business = new HierTagihBusiness();
                business.SetUserAuth(ViewBag.UserAuth);

                alert = business.ImportTagih(model);
            }

            TempData["AlertMessage"] = alert;

            return RedirectToAction("Index");
        }

        public ActionResult DownloadTemplate()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path = Path.Combine(path, "DocTemplate", "UploadTemplate", "template-hirarkitagih.xlsx");

            if (!System.IO.File.Exists(path))
            {
                return RedirectToAction("Index", "Dashboard");
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = Path.GetFileName(path);

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}