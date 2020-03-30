namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CallTargetApprovalHeader")]
    public partial class CallTargetApprovalHeader
    {
        [Key]
        [StringLength(50)]
        public string HeaderID { get; set; }

        [StringLength(50)]
        public string UploadedBy { get; set; }

        public DateTime? UploadedOn { get; set; }

        [StringLength(50)]
        public string ApprovedBy { get; set; }

        public DateTime? ApprovedOn { get; set; }

        [Required]
        [StringLength(1)]
        public string Status { get; set; }

        [StringLength(200)]
        public string Reason { get; set; }
    }
}
