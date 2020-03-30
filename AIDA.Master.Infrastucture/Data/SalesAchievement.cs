namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalesAchievement")]
    public partial class SalesAchievement
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string RayonCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(4)]
        public string Group { get; set; }

        [Key]
        [Column(Order = 2)]
        public float SalesAchieve { get; set; }

        [Key]
        [Column(Order = 3)]
        public float SalesTarget { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Bulan { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Tahun { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        [Key]
        [Column(Order = 7)]
        public DateTime UpdatedOn { get; set; }
    }
}
