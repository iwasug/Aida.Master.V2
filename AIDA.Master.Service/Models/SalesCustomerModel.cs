using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDA.Master.Service.Models
{
    public class RayonDatatableViewModel : JDatatableViewModel
    {
        [JsonProperty("rayon")]
        public string RayonCode { get; set; }
    }

    public class SalesCustomerViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("rayon_code")]
        public string RayonCode { get; set; }

        [JsonProperty("customer_code")]
        public string CustomerCode { get; set; }

        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty("slm_nik")]
        public int SLMNik { get; set; }

        [JsonProperty("slm_fullname")]
        public string SLMFullname { get; set; }

        [JsonProperty("fss_nik")]
        public int FSSNik { get; set; }

        [JsonProperty("fss_fullname")]
        public string FSSFullname { get; set; }

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
