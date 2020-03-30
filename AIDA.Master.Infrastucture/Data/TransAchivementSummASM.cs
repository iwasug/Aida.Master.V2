namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TransAchivementSummASM")]
    public partial class TransAchivementSummASM
    {
        public long ID { get; set; }

        public int? ASM { get; set; }

        public int? TRANSACHIEVE { get; set; }

        public int? TRANSTARGET { get; set; }

        public int? Quarter { get; set; }

        public int? Tahun { get; set; }

        [StringLength(50)]
        public string UpdateBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
