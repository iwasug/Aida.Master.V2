namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PFTarget")]
    public partial class PFTarget
    {
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        public string RayonCode { get; set; }

        [StringLength(20)]
        public string Material { get; set; }

        public int Bulan { get; set; }

        public int Tahun { get; set; }

        public float Target { get; set; }

        public int? slm { get; set; }

        public int? fss { get; set; }

        [StringLength(3)]
        public string Division { get; set; }

        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
