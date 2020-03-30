namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TTempSalesTarget")]
    public partial class TTempSalesTarget
    {
        public long ID { get; set; }

        public long? IDAppr { get; set; }

        [Required]
        [StringLength(10)]
        public string RayonCode { get; set; }

        [StringLength(3)]
        public string AchiGroup { get; set; }

        public float? Target { get; set; }

        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? Bulan { get; set; }

        public int? Tahun { get; set; }

        public int? SLM { get; set; }

        public int? FSS { get; set; }

        [StringLength(5)]
        public string Division { get; set; }

        [StringLength(20)]
        public string Material { get; set; }
    }
}
