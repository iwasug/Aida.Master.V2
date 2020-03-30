namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RHTeam")]
    public partial class RHTeam
    {
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        public string RayonCode { get; set; }

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

        public virtual ASM ASM1 { get; set; }

        public virtual ASM ASM2 { get; set; }

        public virtual ASM ASM3 { get; set; }

        public virtual ASM ASM4 { get; set; }

        public virtual BUM BUM1 { get; set; }

        public virtual BUM BUM2 { get; set; }

        public virtual BUM BUM3 { get; set; }

        public virtual BUM BUM4 { get; set; }

        public virtual NSM NSM1 { get; set; }

        public virtual NSM NSM2 { get; set; }

        public virtual NSM NSM3 { get; set; }

        public virtual NSM NSM4 { get; set; }

        public virtual SLM SLM1 { get; set; }

        public virtual SLM SLM2 { get; set; }

        public virtual SLM SLM3 { get; set; }

        public virtual SLM SLM4 { get; set; }
    }
}
