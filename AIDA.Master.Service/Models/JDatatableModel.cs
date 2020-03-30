using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDA.Master.Service.Models
{
    public class JDatatableViewModel
    {
        [JsonProperty("keyword")]
        public string Keyword { get; set; }

        [JsonProperty("length")]
        public int Length { get; set; }

        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("order_column")]
        public int IndexOrderCol { get; set; }

        [JsonProperty("order_type")]
        public string OrderType { get; set; }
    }

    public class JDatatableResponse
    {
        [JsonProperty("iTotalRecords")]
        public long TotalRecords { get; set; }

        [JsonProperty("iTotalDisplayRecords")]
        public long TotalDisplayRecords { get; set; }

        [JsonProperty("aaData")]
        public object Data { get; set; }

        public JDatatableResponse()
        {
            Data = new List<object>();
        }
    }
}
