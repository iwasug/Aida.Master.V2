namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalesTranExtended")]
    public partial class SalesTranExtended
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string SALESORDER { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string INVOICE { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(4)]
        public string PLANT { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string CUSTOMER { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(3)]
        public string DIVISION { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(20)]
        public string MATERIAL { get; set; }

        [Key]
        [Column(Order = 6)]
        public decimal TOTAL { get; set; }

        [Key]
        [Column(Order = 7)]
        public decimal PRINCIPALDISCOUNT { get; set; }

        [Key]
        [Column(Order = 8)]
        public decimal APLDISCOUNT { get; set; }

        public DateTime? DOCUMENTDATE { get; set; }

        public float? Quantity { get; set; }

        [StringLength(20)]
        public string UOM { get; set; }

        [StringLength(10)]
        public string RayonCode { get; set; }

        [StringLength(4)]
        public string RayonType { get; set; }

        public int? BUM { get; set; }

        public int? NSM { get; set; }

        public int? ASM { get; set; }

        public int? FSS { get; set; }

        public int? SLM { get; set; }

        [StringLength(3)]
        public string AchiGroup { get; set; }

        public DateTime? LastUpdate { get; set; }

        [StringLength(2)]
        public string Code { get; set; }
    }
}
