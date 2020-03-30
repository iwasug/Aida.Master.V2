using AIDA.Master.Infrastucture.Constants;
using AIDA.Master.Service.Businesses;
using AIDA.Master.Service.Localizations;
using AIDA.Master.Service.Models;
using AIDA.Master.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AIDA.Master.Web.Controllers
{
    [AuthenticationActionFilter(ModuleCode.ImportCollection)]
    public class CollectionController : Controller
    {
        // GET: Collection
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(ImportCollectionModel model)
        {
            AlertMessage alert = new AlertMessage();

            CollectionBusiness business = new CollectionBusiness();

            alert = business.ImportCollection(model);

            return RedirectToAction("Index");
        }
    }
}