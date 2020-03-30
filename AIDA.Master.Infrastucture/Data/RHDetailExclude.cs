namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RHDetailExclude")]
    public partial class RHDetailExclude
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string RayonCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(6)]
        public string Customer { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "date")]
        public DateTime ValidFrom { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "date")]
        public DateTime ValidTo { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Key]
        [Column(Order = 6)]
        public DateTime CreatedOn { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        [Key]
        [Column(Order = 8)]
        public DateTime UpdatedOn { get; set; }
    }
}
