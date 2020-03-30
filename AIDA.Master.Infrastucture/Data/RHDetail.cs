namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RHDetail")]
    public partial class RHDetail
    {
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        public string RayonCode { get; set; }

        [Required]
        [StringLength(6)]
        public string Customer { get; set; }

        [ForeignKey("Customer")]
        public virtual TCustomer CustomerObj { get; set; }

        [Column(TypeName = "date")]
        public DateTime ValidFrom { get; set; }

        [Column(TypeName = "date")]
        public DateTime ValidTo { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}
