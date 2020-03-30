namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SalesIncentivesASM_SD1
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ASM { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FSS { get; set; }

        [Key]
        [Column(Order = 2)]
        public float SalesAchieve { get; set; }

        [Key]
        [Column(Order = 3)]
        public float SalesTarget { get; set; }

        [Key]
        [Column(Order = 4)]
        public float PercentAchieve { get; set; }

        [Key]
        [Column(Order = 5)]
        public float AdjustAchieve { get; set; }

        [Key]
        [Column(Order = 6)]
        public float RunningBudget { get; set; }

        [Key]
        [Column(Order = 7)]
        public float Incentives { get; set; }

        [Key]
        [Column(Order = 8)]
        public float MaxBudget { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Quarter { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Tahun { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        [Key]
        [Column(Order = 12)]
        public DateTime UpdatedOn { get; set; }
    }
}
