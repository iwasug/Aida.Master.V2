namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RHHeaderExclude")]
    public partial class RHHeaderExclude
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
        [StringLength(4)]
        public string Plant { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(4)]
        public string RayonType { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BUM { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NSM { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ASM { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FSS { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SLM { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "date")]
        public DateTime ValidFrom { get; set; }

        [Key]
        [Column(Order = 10, TypeName = "date")]
        public DateTime ValidTo { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Key]
        [Column(Order = 12)]
        public DateTime CreatedOn { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        [Key]
        [Column(Order = 14)]
        public DateTime UpdatedOn { get; set; }
    }
}
