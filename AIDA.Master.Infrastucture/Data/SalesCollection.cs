namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalesCollection")]
    public partial class SalesCollection
    {
        public long ID { get; set; }

        [StringLength(6)]
        public string Plant { get; set; }

        [StringLength(20)]
        public string Customer { get; set; }

        public int? Bulan { get; set; }

        public int? Tahun { get; set; }

        [StringLength(3)]
        public string Group2 { get; set; }

        [StringLength(3)]
        public string Group1 { get; set; }

        [StringLength(3)]
        public string Channel { get; set; }

        public float? Open_item { get; set; }

        public float? Piutang_LE90 { get; set; }

        public float? PIUTANG_GT90 { get; set; }

        public float? PIUTANG_GT120 { get; set; }

        public float? Pembayaran { get; set; }

        public float? Iven { get; set; }
    }
}
