using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDA.Master.Service.Models
{
    public class SalesmanViewModel
    {
        [JsonProperty("nik")]
        public int NIK { get; set; }

        [JsonProperty("fullname")]
        public string Fullname { get; set; }

        [JsonProperty("is_role")]
        public bool IsRole { get; set; }

        [JsonProperty("dt_valid_from")]
        public DateTime ValidFromDate { get; set; }

        [JsonProperty("f_valid_from")]
        public string FormattedValidFrom { get; set; }

        [JsonProperty("dt_valid_to")]
        public DateTime ValidToDate { get; set; }

        [JsonProperty("f_valid_to")]
        public string FormattedValidTo { get; set; }

        [JsonProperty("remarks")]
        public string Remarks { get; set; }
    }
}
