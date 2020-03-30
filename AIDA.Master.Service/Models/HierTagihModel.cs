using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDA.Master.Service.Models
{
    public class HierTagihViewModel
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

        [JsonProperty("collector_nik")]
        public int? CollectorNik { get; set; }

        [JsonProperty("collector_fullname")]
        public string CollectorFullname { get; set; }

        [JsonProperty("fakturis_nik")]
        public int? FakturisNik { get; set; }

        [JsonProperty("fakturis_fullname")]
        public string FakturisFullname { get; set; }

        [JsonProperty("spv_fakturis_nik")]
        public int? SPVFakturisNik { get; set; }

        [JsonProperty("spv_fakturis_fullname")]
        public string SPVFakturisFullname { get; set; }

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
