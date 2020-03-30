namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TClaim")]
    public partial class TClaim
    {
        [Key]
        public int ClaimID { get; set; }

        [StringLength(255)]
        public string ClaimType { get; set; }

        [StringLength(255)]
        public string ClaimValue { get; set; }

        public Guid UserID { get; set; }
    }
}
