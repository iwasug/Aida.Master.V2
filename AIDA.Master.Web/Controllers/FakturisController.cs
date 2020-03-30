using AIDA.Master.Infrastucture.Constants;
using AIDA.Master.Service.Businesses;
using AIDA.Master.Service.Localizations;
using AIDA.Master.Service.Models;
using AIDA.Master.Web.Attributes;
using AIDA.Master.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AIDA.Master.Web.Controllers
{
    [AuthenticationActionFilter(ModuleCode.Fakturis)]
    public class FakturisController : Controller
    {
        public ActionResult Index()
        {
            FakturisBusiness business = new FakturisBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            ViewBag.IsEditable = business.IsEditable();

            return View();
        }

        public ActionResult Add()
        {
            return View("Edit", new FakturisViewModel());
        }

        public ActionResult Edit(int? id = null)
        {
            ViewBag.IsEdit = true;

            if (id == null)
            {
                return RedirectToAction("Index");
            }

            FakturisBusiness business = new FakturisBusiness();

            FakturisViewModel model = business.GetDetail(id.Value);

            if (model == null)
            {
                TempData["AlertMessage"] = new AlertMessage(StaticMessage.ERR_DATA_NOT_FOUND);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Datatable(JDatatableViewModel model)
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("Index");
            }

            JDatatableResponse resp = new JDatatableResponse();

            FakturisBusiness business = new FakturisBusiness();
            business.SetUserAuth(ViewBag.UserAuth);

            resp = business.GetDatatableByQuery(model);

            return new MyJsonResult(resp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(FakturisViewModel model)
        {
            AlertMessage alert = new AlertMessage();

            FakturisBusiness business = new FakturisBusiness();

            if (!ModelState.IsValid)
            {
                alert.Text = string.Join(System.Environment.NewLine, ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
            }
            else
            {
                business.SetUserAuth(ViewBag.UserAuth);

                alert = business.Add(model);
            }

            TempData["AlertMessage"] = alert;

            if (alert.Status == 1)
            {
                return RedirectToAction("Index");
            }

            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FakturisViewModel model)
        {
            ViewBag.IsEdit = true;

            AlertMessage alert = new AlertMessage();

            FakturisBusiness business = new FakturisBusiness();

            if (!ModelState.IsValid)
            {
                alert.Text = string.Join(System.Environment.NewLine, ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
            }
            else
            {
                business.SetUserAuth(ViewBag.UserAuth);

                alert = business.Edit(model);
            }

            TempData["AlertMessage"] = alert;

            if (alert.Status == 1)
            {
                return RedirectToAction("Index");
            }

            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAccess(ASMUpdateAccessViewModel model)
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("Index");
            }

            AlertMessage alert = new AlertMessage();

            ASMBusiness business = new ASMBusiness();

            if (!ModelState.IsValid)
            {
                alert.Text = string.Join(System.Environment.NewLine, ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
            }
            else
            {
                business.SetUserAuth(ViewBag.UserAuth);

                alert = business.UpdateAccess(model);
            }

            return new MyJsonResult(alert, JsonRequestBehavior.AllowGet);
        }
    }
}