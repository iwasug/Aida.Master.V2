namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Parameter")]
    public partial class Parameter
    {
        [Key]
        [StringLength(25)]
        public string ParamID { get; set; }

        [Required]
        [StringLength(150)]
        public string ParamDescription { get; set; }

        [Required]
        [StringLength(50)]
        public string ParamValue { get; set; }

        [Required]
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}
