using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radyalabs.Core.Model
{
    public class JsonAlertModel
    {
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "data")]
        public object Data { get; set; }
    }
}
