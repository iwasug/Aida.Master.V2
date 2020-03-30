namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ElegibilityHeader")]
    public partial class ElegibilityHeader
    {
        [Key]
        [StringLength(10)]
        public string ElegID { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        [StringLength(1)]
        public string CalculationType { get; set; }

        public float? MinPercentage { get; set; }

        public float? MaxPercentage { get; set; }
    }
}
