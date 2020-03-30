using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIDA.Master.Service.Models
{
    public class SupervisorViewModel
    {
        [JsonProperty("nik")]
        public int NIK { get; set; }

        [JsonProperty("fullname")]
        public string Fullname { get; set; }

        [JsonProperty("is_role")]
        public bool IsRole { get; set; }

        [JsonProperty("allow_write_by")]
        public int? AllowWriteBy { get; set; }

        [JsonProperty("is_able_to_upload")]
        public bool IsAbleToUpload { get; set; }

        [JsonProperty("upload_valid_to")]
        public DateTime? UploadValidTo { get; set; }

        [JsonProperty("f_upload_valid_to")]
        public string FormattedUploadValidTo { get; set; }

        [JsonProperty("default_rayon_type")]
        public string DefaultRayonType { get; set; }

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

    public class SupervisorUpdateAccessViewModel
    {
        public List<SupervisorNIKValUpdateAccess> ListData { get; set; }
    }

    public class SupervisorNIKValUpdateAccess
    {
        public int NIK { get; set; }

        public bool IsAllowed { get; set; }

        public DateTime? UploadValidTo { get; set; }

        public string FormattedUploadValidTo { get; set; }
    }
}
