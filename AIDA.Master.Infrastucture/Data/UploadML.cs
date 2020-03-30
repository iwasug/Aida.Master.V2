namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UploadML")]
    public partial class UploadML
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string RayonCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(6)]
        public string Customer { get; set; }
    }
}
