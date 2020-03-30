namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TransAchivementSumm")]
    public partial class TransAchivementSumm
    {
        public long ID { get; set; }

        [StringLength(15)]
        public string RAYONCODE { get; set; }

        public int? TRANSACHIEVE { get; set; }

        public int? TRANSTARGET { get; set; }

        public int? Bulan { get; set; }

        public int? Tahun { get; set; }

        [StringLength(50)]
        public string UpdateBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
