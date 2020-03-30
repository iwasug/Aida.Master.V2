namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RDetail")]
    public partial class RDetail
    {
        public int ID { get; set; }

        [Required]
        [StringLength(4)]
        public string RayonType { get; set; }

        [Required]
        [StringLength(3)]
        public string AchiGroup { get; set; }

        [Required]
        [StringLength(3)]
        public string Division { get; set; }

        [StringLength(20)]
        public string Material { get; set; }

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

        public virtual RHeader RHeader { get; set; }

        public virtual RHeader RHeader1 { get; set; }
    }
}
