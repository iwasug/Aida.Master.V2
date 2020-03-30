namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomerGroup")]
    public partial class CustomerGroup
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string CustGroup { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(6)]
        public string Customer { get; set; }

        [StringLength(255)]
        public string CustName { get; set; }

        [StringLength(10)]
        public string RayonCode { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ValidFrom { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ValidTo { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        [StringLength(50)]
        public string UpdateBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
