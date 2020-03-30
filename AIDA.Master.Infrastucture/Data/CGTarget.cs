namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CGTarget")]
    public partial class CGTarget
    {
        [Required]
        [StringLength(10)]
        public string RayonCode { get; set; }

        [Required]
        [StringLength(20)]
        public string CustGroup { get; set; }

        public int Bulan { get; set; }

        public int Tahun { get; set; }

        public float Target { get; set; }

        public int ID { get; set; }

        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
