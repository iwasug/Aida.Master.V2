namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ElegibilityRayonType")]
    public partial class ElegibilityRayonType
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(4)]
        public string RayonType { get; set; }

        [Required]
        [StringLength(10)]
        public string ElegID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string IncTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}