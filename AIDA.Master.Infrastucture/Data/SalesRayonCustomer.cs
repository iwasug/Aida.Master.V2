namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalesRayonCustomer")]
    public partial class SalesRayonCustomer
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string RAYONCODE { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string CUSTOMER { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(4)]
        public string GROUP { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(3)]
        public string DIVISION { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(20)]
        public string MATERIAL { get; set; }

        [StringLength(20)]
        public string UOM { get; set; }

        public float? Quantity { get; set; }

        public float? SalesAchieve { get; set; }

        public DateTime? Date { get; set; }

        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
