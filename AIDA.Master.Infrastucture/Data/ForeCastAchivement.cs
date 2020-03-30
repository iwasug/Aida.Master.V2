namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ForeCastAchivement")]
    public partial class ForeCastAchivement
    {
        public long ID { get; set; }

        [StringLength(4)]
        public string Plant { get; set; }

        [StringLength(3)]
        public string Division { get; set; }

        [StringLength(10)]
        public string FSS { get; set; }

        public decimal? ForeCastAchieve { get; set; }

        public int? Bulan { get; set; }

        public int? Tahun { get; set; }

        [StringLength(50)]
        public string UpdateBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
