using Radyalabs.Core.LogHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AIDA.Master.Infrastucture.MailService
{
    public class SmtpService : IMailService
    {
        private SmtpClient _smtpClient;

        private string _dirLayout;

        private string _layoutView;

        private string _smtpHost;
        private string _smtpUsername;
        private string _smtpPassword;
        private string _smtpPort;
        private bool _isEnableSsl;
        private bool _isUseDefaultCredential;
        private bool _isConsoleApp;

        private ILogHelper _logHelper;

        public SmtpService()
        {
            string rootDir = (HttpContext.Current != null)
                ? HttpContext.Current.Server.MapPath("~/")
                : AppDomain.CurrentDomain.BaseDirectory;

            _dirLayout = Path.Combine(rootDir, "DocTemplate", "EmailContent");

            Init();
        }

        private void Init()
        {
            _logHelper = new Log4NetHelper();

            _layoutView = "_EmailLayout.html";

            _smtpHost = ConfigurationManager.AppSettings.Get("SMTPHost");
            _smtpUsername = ConfigurationManager.AppSettings.Get("SMTPUsername");
            _smtpPassword = ConfigurationManager.AppSettings.Get("SMTPPassword");
            _smtpPort = ConfigurationManager.AppSettings.Get("SMTPPort");

            string enableSsl = System.Configuration.ConfigurationManager.AppSettings.Get("SMTPEnableSSL");

            if (!string.IsNullOrEmpty(enableSsl))
            {
                _isEnableSsl = Convert.ToBoolean(enableSsl);
            }

            string isUseDefaultCredential = System.Configuration.ConfigurationManager.AppSettings.Get("SMTPUseDefaultCredentials");

            if (!string.IsNullOrEmpty(isUseDefaultCredential))
            {
                _isUseDefaultCredential = Convert.ToBoolean(isUseDefaultCredential);
            }
        }

        public string GetDirLayout()
        {
            return _dirLayout;
        }

        private string GetContentWithParams(string view, Dictionary<string, string> dcParam)
        {
            try
            {
                string path = Path.Combine(_dirLayout, view);
                string content = File.ReadAllText(path);

                if (dcParam != null && dcParam.Count > 0)
                {
                    foreach (var item in dcParam)
                    {
                        content = content.Replace(item.Key, item.Value);
                    }
                }

                return content;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public string GetBodyFromView(string view, Dictionary<string, string> dcParam)
        {
            string body = null;

            string content = GetContentWithParams(view, dcParam);

            Dictionary<string, string> dcParamLayout = new Dictionary<string, string>();
            dcParamLayout["{:BODY_CONTENT}"] = content;

            body = GetContentWithParams(_layoutView, dcParamLayout);

            return body;
        }

        public async Task Send(string view, Dictionary<string, string> dcParam, string subject, List<string> listTo, List<string> listAttachment = null)
        {
            string body = GetBodyFromView(view, dcParam);

            await Send(body, subject, listTo, listAttachment);
        }

        public async Task Send(string body, string subject, List<string> listTo, List<string> listAttachment = null)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(_smtpUsername);
                //mail.ReplyTo = new MailAddress(_smtpUsername);

                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = body;

                DateTime dt = DateTime.Now;

                string logContent = "subject: " + mail.Subject + System.Environment.NewLine;
                logContent += "from: " + _smtpUsername + System.Environment.NewLine;

                logContent += "to: ";

                foreach (string strTo in listTo)
                {
                    mail.To.Add(strTo);

                    logContent += strTo + ", ";
                }

                string strBccTesting = ConfigurationManager.AppSettings.Get("EmailTestingBcc");

                if (!string.IsNullOrEmpty(strBccTesting))
                {
                    string[] arrBcc = strBccTesting.Split(',');

                    if (arrBcc.Length > 0)
                    {
                        for (int i = 0; i < arrBcc.Length; i++)
                        {
                            mail.Bcc.Add(arrBcc[i]);
                        }
                    }
                }

                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                if (listAttachment != null)
                {
                    logContent += "attachment: " + System.Environment.NewLine;

                    foreach (var file in listAttachment)
                    {
                        // Create  the file attachment for this e-mail message.
                        Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);

                        // Add time stamp information for the file.
                        ContentDisposition disposition = data.ContentDisposition;
                        disposition.CreationDate = System.IO.File.GetCreationTime(file);
                        disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                        disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                        // Add the file attachment to this e-mail message.

                        mail.Attachments.Add(data);

                        logContent += "- " + file + System.Environment.NewLine;
                    }

                    logContent += System.Environment.NewLine;
                }

                logContent += "----------------------------------------------" + System.Environment.NewLine;
                logContent += body;

                //_logHelper.Write("EmailSent", DateTime.Now, logContent, null);

                try
                {
                    using (SmtpClient smtpClient = new SmtpClient(_smtpHost))
                    {
                        if (_isUseDefaultCredential)
                        {
                            smtpClient.UseDefaultCredentials = true;
                        }
                        else
                        {
                            smtpClient.UseDefaultCredentials = false;
                            smtpClient.Credentials = new System.Net.NetworkCredential(_smtpUsername, _smtpPassword);
                        }

                        int port = 0;

                        if (int.TryParse(_smtpPort, out port))
                        {
                            smtpClient.Port = port;
                        }

                        smtpClient.EnableSsl = _isEnableSsl;

                        //ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                        await smtpClient.SendMailAsync(mail);
                    }
                }
                catch (Exception ex)
                {
                    _logHelper.Write("EmailError", DateTime.Now, null, "System", ex);
                }
            }
        }
    }
}
