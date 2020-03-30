namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductFocusAchievementSumm")]
    public partial class ProductFocusAchievementSumm
    {
        public long ID { get; set; }

        [StringLength(15)]
        public string RayonCode { get; set; }

        public float? ProducFocusActual { get; set; }

        public float? ProductFocusTarget { get; set; }

        public int? Bulan { get; set; }

        public int? Tahun { get; set; }

        [StringLength(50)]
        public string UpdateBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
