using AIDA.Master.Infrastucture.Constants;
using AIDA.Master.Infrastucture.MailService;
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
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AIDA.Master.Web.Controllers
{
    [AuthenticationActionFilter(ModuleCode.MasterSalesHier)]
    public class HierSalesController : Controller
    {
        public ActionResult Index()
        {
            HierSalesBusiness business = new HierSalesBusiness();
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

            HierSalesBusiness business = new HierSalesBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            resp = business.GetDatatable(model);

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

            HierSalesBusiness business = new HierSalesBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            resp = business.GetDatatableCustomer(model);

            return new MyJsonResult(resp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ExportSales(JDatatableViewModel model)
        {
            HierSalesBusiness business = new HierSalesBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            AlertMessage alert = business.ExportSalesToExcel(model);

            if (alert.Status == 1)
            {
                var bytes = alert.Data as byte[];

                return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, string.Format("HirarkiSales-{0}.xlsx", DateTime.UtcNow.ToUtcID().ToString("yyyyMMdd-HHmm")));
            }

            return RedirectToAction("Index");
        }

        public ActionResult ImportSales()
        {
            HierSalesBusiness business = new HierSalesBusiness();
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

        public ActionResult PreviewImportSales()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PreviewImportSales(ImportSalesViewModel model)
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
                HierSalesBusiness business = new HierSalesBusiness();
                business.SetUserAuth(ViewBag.UserAuth);

                alert = business.Preview(model);
            }

            TempData["AlertMessage"] = alert;

            if (alert.Data != null)
            {
                TempData["ImportSales"] = alert.Data;

                return View(alert.Data);
            }
            else
            {
                return RedirectToAction("Import");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ImportSales(ImportSalesViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            model = null;

            if (TempData["ImportSales"] != null)
            {
                model = TempData["ImportSales"] as ImportSalesViewModel;
            }
            else
            {
                alert.Text = StaticMessage.ERR_INVALID_INPUT;
            }

            if (model != null)
            {
                HierSalesBusiness business = new HierSalesBusiness(new SmtpService());
                business.SetUserAuth(ViewBag.UserAuth);

                alert = await business.ImportSales(model);
            }

            TempData["AlertMessage"] = alert;

            return RedirectToAction("Index");
        }

        public ActionResult DownloadTemplate()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path = Path.Combine(path, "DocTemplate", "UploadTemplate", "template-hirarkisales.xlsx");

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