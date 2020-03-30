using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDA.Master.Service.Models
{
    public class AlertMessage
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("message")]
        public string Text { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }

        public AlertMessage() { }

        public AlertMessage(string text)
        {
            Status = 0;
            Text = text;
        }

        public AlertMessage(int status, string text)
        {
            Status = status;
            Text = text;
        }
    }
}
