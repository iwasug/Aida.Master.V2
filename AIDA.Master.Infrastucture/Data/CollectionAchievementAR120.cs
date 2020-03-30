namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CollectionAchievementAR120
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NSM { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ASM { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FSS { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SLM { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(150)]
        public string st_NSM { get; set; }

        [Key]
        [Column(Order = 5)]
        public bool st_ASM { get; set; }

        [Key]
        [Column(Order = 6)]
        public bool st_FSS { get; set; }

        [Key]
        [Column(Order = 7)]
        public bool st_SLM { get; set; }

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
        public float CollectionActual { get; set; }

        [Key]
        [Column(Order = 11)]
        public float CollectionTarget { get; set; }

        [Key]
        [Column(Order = 12)]
        public float CollectionAchieve { get; set; }

        public float? Insentive { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        [Key]
        [Column(Order = 14)]
        public DateTime UpdatedOn { get; set; }

        [Key]
        [Column(Order = 15)]
        [StringLength(10)]
        public string Elegibility { get; set; }
    }
}
