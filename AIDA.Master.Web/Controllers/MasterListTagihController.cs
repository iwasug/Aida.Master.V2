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
    [AuthenticationActionFilter(ModuleCode.MasterTagihList)]
    public class MasterListTagihController : Controller
    {
        public ActionResult Index(string rayon)
        {
            MasterListBusiness business = new MasterListBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            ViewBag.ListRayon = business.GetRayonTagihByQuery();
            ViewBag.SelectedRayon = rayon;
            ViewBag.IsEditable = business.IsEditableTagih();

            return View();
        }

        public ActionResult CheckDouble()
        {
            return View();
        }

        public ActionResult Import()
        {
            MasterListBusiness business = new MasterListBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            if (! business.IsEditableTagih())
            {
                TempData["AlertMessage"] = new AlertMessage(StaticMessage.ERR_ACCESS_DENIED);

                return RedirectToAction("Index");
            }

            MasterImportViewModel model = null;

            if (TempData["ImportMasterList"] != null)
            {
                model = TempData["ImportMasterList"] as MasterImportViewModel;
            }

            return View(model);
        }

        public ActionResult Preview()
        {
            return RedirectToAction("Index");
        }

        public ActionResult DownloadTemplate()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path = Path.Combine(path, "DocTemplate", "UploadTemplate", "template-masterlist.xlsx");

            if (!System.IO.File.Exists(path))
            {
                return RedirectToAction("Index", "Dashboard");
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = Path.GetFileName(path);

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [HttpPost]
        public ActionResult Datatable(RayonDatatableViewModel model)
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("Index");
            }

            JDatatableResponse resp = new JDatatableResponse();

            MasterListBusiness business = new MasterListBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            resp = business.GetDatatableTagihByQuery(model);

            return new MyJsonResult(resp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Export(RayonDatatableViewModel model)
        {
            MasterListBusiness business = new MasterListBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            AlertMessage alert = business.ExportMasterListTagih(model);

            if (alert.Status == 1)
            {
                var bytes = alert.Data as byte[];

                return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, string.Format("MasterListTagih-{0}.xlsx", DateTime.UtcNow.ToUtcID().ToString("yyyyMMdd-HHmm")));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Preview(ImportMasterListViewModel model)
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
                MasterListBusiness business = new MasterListBusiness();
                business.SetUserAuth(ViewBag.UserAuth);

                alert = business.PreviewTagih(model);
            }

            TempData["AlertMessage"] = alert;

            if (alert.Data != null)
            {
                TempData["ImportMasterList"] = alert.Data;

                return View(alert.Data);
            }
            else
            {
                return RedirectToAction("Import");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(ImportMasterListViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            model = null;

            if (TempData["ImportMasterList"] != null)
            {
                model = TempData["ImportMasterList"] as ImportMasterListViewModel;
            }

            if (model != null)
            {
                MasterListBusiness business = new MasterListBusiness();
                business.SetUserAuth(ViewBag.UserAuth);

                alert = business.ImportTagih(model);
            }

            else
            {
                alert.Text = StaticMessage.ERR_INVALID_INPUT;
            }

            TempData["AlertMessage"] = alert;

            return RedirectToAction("Index");
        }
    }
}