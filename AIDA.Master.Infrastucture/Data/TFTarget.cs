namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TFTarget")]
    public partial class TFTarget
    {
        public int NIK { get; set; }

        [Required]
        [StringLength(4)]
        public string Plant { get; set; }

        [Required]
        [StringLength(3)]
        public string Division { get; set; }

        public float Target { get; set; }

        public int? Bulan { get; set; }

        public int? Tahun { get; set; }

        public int ID { get; set; }

        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public virtual TFConfig TFConfig { get; set; }

        public virtual TFConfig TFConfig1 { get; set; }

        public virtual TFConfig TFConfig2 { get; set; }

        public virtual TFConfig TFConfig3 { get; set; }
    }
}
