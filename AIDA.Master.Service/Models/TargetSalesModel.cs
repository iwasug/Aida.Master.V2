using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDA.Master.Service.Models
{
    public class TargetSalesDatatableViewModel : JDatatableViewModel
    {
        [JsonProperty("periode")]
        public string Periode { get; set; }

        [JsonProperty("rayon_code")]
        public string RayonCode { get; set; }
    }
    public class TargetSalesViewModel
    {

        [JsonProperty("rayon_code")]
        public string RayonCode { get; set; }

        [JsonProperty("slm_nik")]
        public int SLM_NIK { get; set; }
        [JsonProperty("slm_name")]
        public string SLM_Name { get; set; }

        [JsonProperty("fss_nik")]
        public int FSS_NIK { get; set; }
        [JsonProperty("fss_name")]
        public string FSS_Name { get; set; }

        [JsonProperty("achi_group")]
        public string AchiGroup { get; set; }

        [JsonProperty("division")]
        public string Division { get; set; }

        [JsonProperty("material")]
        public string Material { get; set; }
        [JsonProperty("bulan")]
        public int Bulan { get; set; }

        [JsonProperty("tahun")]
        public int Tahun { get; set; }

        [JsonProperty("target")]
        public string Target { get; set; }
    }
}
