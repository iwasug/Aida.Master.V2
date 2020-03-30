namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustGorupAchivementSumm")]
    public partial class CustGorupAchivementSumm
    {
        public long ID { get; set; }

        [StringLength(15)]
        public string RayonCode { get; set; }

        public float? CustGroupActual { get; set; }

        public float? CustGroupTarget { get; set; }

        public int? Bulan { get; set; }

        public int? Tahun { get; set; }

        [StringLength(50)]
        public string UpdateBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        [StringLength(20)]
        public string CustGroup { get; set; }
    }
}
