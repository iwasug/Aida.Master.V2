using AIDA.Master.Infrastucture.Constants;
using AIDA.Master.Infrastucture.MailService;
using AIDA.Master.Service.Models;
using Radyalabs.Core.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AIDA.Master.Web.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> EmailBody()
        {
            DateTime today = DateTime.UtcNow.ToUtcID();

            IMailService _mailService = new SmtpService();

            UserAuthenticated _userAuth = new UserAuthenticated()
            {
                Fullname = "FSS Uploader",
                NIK = 10001234
            };

            List<SalesCustomerViewModel> listData = new List<SalesCustomerViewModel>();

            listData.Add(new SalesCustomerViewModel()
            {
                Id = "232323",
                RayonCode = "RXDA23874324",
                SLMFullname = "Salesman Satu",
                CustomerCode = "234234",
                CustomerName = "Kimia Farma, AP",
                FormattedValidFrom = "2019-09-01",
                FormattedValidTo = "9999-12-31"
            });

            listData.Add(new SalesCustomerViewModel()
            {
                Id = "232323",
                RayonCode = "RXDA23874324",
                SLMFullname = "Salesman Dua",
                CustomerCode = "234234",
                CustomerName = "Kimia Farma, AP",
                FormattedValidFrom = "2019-09-01",
                FormattedValidTo = "9999-12-31"
            });

            listData.Add(new SalesCustomerViewModel()
            {
                Id = "232323",
                RayonCode = "RXDA23874324",
                SLMFullname = "Salesman Tiga",
                CustomerCode = "234234",
                CustomerName = "Kimia Farma, AP",
                FormattedValidFrom = "2019-09-01",
                FormattedValidTo = "9999-12-31"
            });

            listData.Add(new SalesCustomerViewModel()
            {
                Id = "232323",
                RayonCode = "RXDA23874324",
                SLMFullname = "Salesman Empat",
                CustomerCode = "234234",
                CustomerName = "Kimia Farma, AP",
                FormattedValidFrom = "2019-09-01",
                FormattedValidTo = "9999-12-31"
            });

            listData.Add(new SalesCustomerViewModel()
            {
                Id = "232323",
                RayonCode = "RXDA23874324",
                SLMFullname = "Salesman Lima",
                CustomerCode = "234234",
                CustomerName = "Kimia Farma, AP",
                FormattedValidFrom = "2019-09-01",
                FormattedValidTo = "9999-12-31"
            });

            StringBuilder htmlRows = new StringBuilder();
            foreach (var data in listData)
            {
                htmlRows.Append("<tr>");
                htmlRows.Append($"<td style=\"text-align: center; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 11px; color: #333333;\">{data.Id}</td>");
                htmlRows.Append($"<td style=\"text-align: center; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 11px; color: #333333;\">{data.RayonCode}</td>");
                htmlRows.Append($"<td style=\"text-align: left; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 11px; color: #333333;\">{data.SLMFullname}</td>");
                htmlRows.Append($"<td style=\"text-align: center; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 11px; color: #333333;\">{data.CustomerCode}</td>");
                htmlRows.Append($"<td style=\"text-align: left; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 11px; color: #333333;\">{data.CustomerName}</td>");
                htmlRows.Append($"<td style=\"text-align: center; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 11px; color: #333333;\">{data.FormattedValidFrom}</td>");
                htmlRows.Append($"<td style=\"text-align: center; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 11px; color: #333333;\">{data.FormattedValidTo}</td>");
                htmlRows.Append("</tr>");
            }

            string partialView = System.IO.File.ReadAllText(Path.Combine(_mailService.GetDirLayout(), EmailConstant.ViewPartialMasterListDouble));
            partialView = partialView.Replace("{:ROW_DOUBLE_MASTER_LIST}", htmlRows.ToString());

            Dictionary<string, string> dcParams = new Dictionary<string, string>()
            {
                { "{:FULLNAME}", "NSM ABCD" },
                { "{:UPLOADER_NIK}", _userAuth.NIK.ToString() },
                { "{:UPLOADER_NAME}", _userAuth.Fullname },
                { "{:FORMATTED_DATE}", today.ToString("yyyy-MM-dd HH:mm") },
            };

            
            string view = EmailConstant.ViewMasterListSalesUploaded;
            string content = _mailService.GetBodyFromView(view, dcParams);
            content = content.Replace("{:PARTIAL_MASTER_LIST_DOUBLE}", partialView);
            string subject = EmailConstant.SubjectPrefix + string.Format("{0} [{1}] Upload Hirarki Sales", _userAuth.Fullname, _userAuth.NIK);
            List<string> listTo = new List<string>() { "ega@radyalabs.com" };

            //await _mailService.Send(view, subject, listTo);

            return Content(content);
        }
    }
}