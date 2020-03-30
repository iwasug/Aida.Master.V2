namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RTHeader")]
    public partial class RTHeader
    {
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        public string RayonCode { get; set; }

        [Required]
        [StringLength(4)]
        public string Plant { get; set; }

        public int? ASM { get; set; }

        [ForeignKey("ASM")]
        public virtual ASM ASMObj { get; set; }

        public int? FSS { get; set; }

        [ForeignKey("FSS")]
        public virtual FSS FSSObj { get; set; }

        public int? SLM { get; set; }

        [ForeignKey("SLM")]
        public virtual SLM SLMObj { get; set; }

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

        public int? NSM { get; set; }

        [ForeignKey("NSM")]
        public virtual NSM NSMObj { get; set; }

        //[Column(TypeName = "int")]
        public int? SPVFakturis { get; set; }

        //[ForeignKey("SPVFakturis")]
        //public virtual TSPVFakturis SPVFakturisObj { get; set; }

        //[Column(TypeName = "int")]
        public int? Fakturis { get; set; }

        //[ForeignKey("Fakturis")]
        //public virtual TFakturis FakturisObj { get; set; }

        //[Column(TypeName = "int")]
        public int? Collector { get; set; }

        //[ForeignKey("Collector")]
        //public virtual TCollector CollectorObj { get; set; }
    }
}
