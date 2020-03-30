namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InvoceCorrectionAchievement")]
    public partial class InvoceCorrectionAchievement
    {
        [Key]
        [Column(Order = 0)]
        public long ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NSM { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ASM { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FSS { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SLM { get; set; }

        [Key]
        [Column(Order = 5)]
        public bool st_NSM { get; set; }

        [Key]
        [Column(Order = 6)]
        public bool st_ASM { get; set; }

        [Key]
        [Column(Order = 7)]
        public bool st_FSS { get; set; }

        [Key]
        [Column(Order = 8)]
        public bool st_SLM { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Bulan { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Tahun { get; set; }

        [Key]
        [Column(Order = 11)]
        public float INVCHIEVE { get; set; }

        [Key]
        [Column(Order = 12)]
        public float INVTARGET { get; set; }

        public float? Insentive { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        [Key]
        [Column(Order = 14)]
        public DateTime UpdatedOn { get; set; }
    }
}
