namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RDIOrder")]
    public partial class RDIOrder
    {
        [Key]
        [StringLength(9)]
        public string SalesOrder { get; set; }

        [StringLength(6)]
        public string Customer { get; set; }

        [StringLength(50)]
        public string OrderStatus { get; set; }

        [Column(TypeName = "date")]
        public DateTime? SODate { get; set; }

        [StringLength(255)]
        public string OFCT { get; set; }
    }
}
