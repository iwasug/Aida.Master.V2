namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TCustomer")]
    public partial class TCustomer
    {
        [Key]
        [StringLength(25)]
        public string CustomerCode { get; set; }

        [StringLength(200)]
        public string CustomerName { get; set; }

        [StringLength(60)]
        public string Address { get; set; }

        [StringLength(30)]
        public string PhoneNo { get; set; }

        [StringLength(3)]
        public string IndCode1 { get; set; }

        [StringLength(6)]
        public string IndCode2 { get; set; }

        [StringLength(5)]
        public string IndCode3 { get; set; }

        [StringLength(8)]
        public string IndCode4 { get; set; }

        [StringLength(4)]
        public string Plant { get; set; }

        [StringLength(4)]
        public string SalesOffice { get; set; }
    }
}
