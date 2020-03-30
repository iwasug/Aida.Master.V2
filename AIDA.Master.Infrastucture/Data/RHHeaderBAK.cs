namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RHHeaderBAK")]
    public partial class RHHeaderBAK
    {
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        public string RayonCode { get; set; }

        [Required]
        [StringLength(4)]
        public string Plant { get; set; }

        [Required]
        [StringLength(4)]
        public string RayonType { get; set; }

        public int BUM { get; set; }

        public int NSM { get; set; }

        public int ASM { get; set; }

        public int FSS { get; set; }

        public int SLM { get; set; }

        [Column(TypeName = "date")]
        public DateTime ValidFrom { get; set; }

        [Column(TypeName = "date")]
        public DateTime ValidTo { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}
