namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalesIncentivesFSS")]
    public partial class SalesIncentivesFSS
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FSS { get; set; }

        [Key]
        [Column(Order = 1)]
        public float SalesAchieve { get; set; }

        [Key]
        [Column(Order = 2)]
        public float SalesTarget { get; set; }

        [Key]
        [Column(Order = 3)]
        public float PercentAchieve { get; set; }

        [Key]
        [Column(Order = 4)]
        public float AdjustAchieve { get; set; }

        [Key]
        [Column(Order = 5)]
        public float RunningBudget { get; set; }

        [Key]
        [Column(Order = 6)]
        public float Incentives { get; set; }

        [Key]
        [Column(Order = 7)]
        public float MaxBudget { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Bulan { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Tahun { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        [Key]
        [Column(Order = 11)]
        public DateTime UpdatedOn { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(10)]
        public string RayonCode { get; set; }
    }
}
