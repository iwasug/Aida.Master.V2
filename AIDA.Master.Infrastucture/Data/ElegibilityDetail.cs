namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ElegibilityDetail")]
    public partial class ElegibilityDetail
    {
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        public string ElegID { get; set; }

        [Required]
        [StringLength(20)]
        public string TierType { get; set; }

        public float Bottom { get; set; }

        public float Top { get; set; }

        public float Amount { get; set; }

        [StringLength(1)]
        public string CalculationType { get; set; }
    }
}
