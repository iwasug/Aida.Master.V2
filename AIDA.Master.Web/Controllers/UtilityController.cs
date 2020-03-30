using AIDA.Master.Infrastucture.Constants;
using AIDA.Master.Web.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AIDA.Master.Web.Controllers
{
    [AuthenticationActionFilter(null)]
    public class UtilityController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DownloadWI()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path = Path.Combine(path, "DocTemplate", "WI");

            //if (RoleCode.BUM.Equals(ViewBag.UserAuth.RoleCode))
            //{
            //    path = Path.Combine(path, "AIDAMaster-WI-BUM.docx");
            //}
            if (RoleCode.NSM.Equals(ViewBag.UserAuth.RoleCode))
            {
                path = Path.Combine(path, "AIDAMaster-WI-NSM.docx");
            }
            else if (RoleCode.RM.Equals(ViewBag.UserAuth.RoleCode))
            {
                path = Path.Combine(path, "AIDAMaster-WI-RM.docx");
            }
            else if (RoleCode.ASM.Equals(ViewBag.UserAuth.RoleCode))
            {
                path = Path.Combine(path, "AIDAMaster-WI-ASM.docx");
            }
            else if (RoleCode.FSS.Equals(ViewBag.UserAuth.RoleCode))
            {
                path = Path.Combine(path, "AIDAMaster-WI-Supervisor.docx");
            }
            else if (RoleCode.KaCab.Equals(ViewBag.UserAuth.RoleCode))
            {
                path = Path.Combine(path, "AIDAMaster-WI-KaCab.docx");
            }
            else if (RoleCode.AdminOperation.Equals(ViewBag.UserAuth.RoleCode))
            {
                path = Path.Combine(path, "AIDAMaster-WI-AdminOpr.docx");
            }

            if (!System.IO.File.Exists(path))
            {
                return RedirectToAction("Index", "Dashboard");
            }

            try
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                string fileName = Path.GetFileName(path);

                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Dashboard");
            }
        }
    }
}