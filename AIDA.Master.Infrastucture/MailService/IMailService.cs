using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDA.Master.Infrastucture.MailService
{
    public interface IMailService
    {
        string GetDirLayout();

        string GetBodyFromView(string view, Dictionary<string, string> dcParam);

        Task Send(string body, string subject, List<string> listTo, List<string> listAttachment = null);

        Task Send(string view, Dictionary<string, string> dcParam, string subject, List<string> listTo, List<string> listAttachment = null);
    }
}
