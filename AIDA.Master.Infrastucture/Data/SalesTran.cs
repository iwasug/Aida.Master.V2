namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalesTran")]
    public partial class SalesTran
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string SalesOrder { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string Invoice { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(4)]
        public string Plant { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string Customer { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(3)]
        public string Division { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(20)]
        public string Material { get; set; }

        [Key]
        [Column(Order = 6)]
        public decimal Total { get; set; }

        [Key]
        [Column(Order = 7)]
        public decimal PrincipalDiscount { get; set; }

        [Key]
        [Column(Order = 8)]
        public decimal APLDiscount { get; set; }

        public DateTime? DocumentDate { get; set; }

        public float? Quantity { get; set; }

        [StringLength(20)]
        public string UOM { get; set; }

        public bool? IsMapped { get; set; }

        [StringLength(2)]
        public string Code { get; set; }
    }
}
