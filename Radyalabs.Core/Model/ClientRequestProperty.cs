using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radyalabs.Core.Model
{
    public class ClientRequestProperty
    {
        public ClientRequestProperty()
        {

        }

        public ClientRequestProperty(string ipAddress, string action, string hostname, string platform, string username = "")
        {
            IPAddress = ipAddress;
            Action = action;
            Hostname = hostname;
            Platform = platform;
            Usename = username;
        }

        public string Usename { get; set; }
        public string IPAddress { get; set; }
        public string Action { get; set; }
        public string Hostname { get; set; }
        public string Platform { get; set; }
    }
}
