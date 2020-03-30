namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UploadML2")]
    public partial class UploadML2
    {
        [Key]
        [Required]
        [StringLength(50)]
        public string ID { get; set; }

        //[Column(Order = 0)]
        [Required]
        [StringLength(10)]
        public string RayonCode { get; set; }

        //[Column(Order = 1)]
        [Required]
        [StringLength(6)]
        public string Customer { get; set; }

        [Required]
        public int CreatedByNIK { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
