namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IncentiveType")]
    public partial class IncentiveType
    {
        [Key]
        [StringLength(10)]
        public string IncTypeID { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
