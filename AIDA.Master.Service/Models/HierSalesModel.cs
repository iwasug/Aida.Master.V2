using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDA.Master.Service.Models
{
    public class HierSalesViewModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("rayon_code")]
        public string RayonCode { get; set; }

        [JsonProperty("rayon_desc")]
        public string RayonDesc { get; set; }

        [JsonProperty("plant_code")]
        public string PlantCode { get; set; }

        [JsonProperty("plant_desc")]
        public string PlantDesc { get; set; }

        [JsonProperty("rayon_type")]
        public string RayonType { get; set; }

        [JsonProperty("bum_nik")]
        public int BUMNik { get; set; }

        [JsonProperty("bum_fullname")]
        public string BUMFullname { get; set; }

        [JsonProperty("nsm_nik")]
        public int NSMNik { get; set; }

        [JsonProperty("nsm_fullname")]
        public string NSMFullname { get; set; }

        [JsonProperty("asm_nik")]
        public int ASMNik { get; set; }

        [JsonProperty("asm_fullname")]
        public string ASMFullname { get; set; }

        [JsonProperty("fss_nik")]
        public int FSSNik { get; set; }

        [JsonProperty("fss_fullname")]
        public string FSSFullname { get; set; }

        [JsonProperty("slm_nik")]
        public int SLMNik { get; set; }

        [JsonProperty("slm_fullname")]
        public string SLMFullname { get; set; }

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
