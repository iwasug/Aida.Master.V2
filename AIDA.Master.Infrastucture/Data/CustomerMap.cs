namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomerMap")]
    public partial class CustomerMap
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(6)]
        public string OldCustomer { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(6)]
        public string NewCustomer { get; set; }
    }
}
