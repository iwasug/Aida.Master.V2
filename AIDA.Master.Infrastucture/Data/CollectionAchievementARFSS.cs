namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CollectionAchievementARFSS")]
    public partial class CollectionAchievementARFSS
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NIK { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Bulan { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Tahun { get; set; }

        [Key]
        [Column(Order = 3)]
        public float CollectionActual { get; set; }

        [Key]
        [Column(Order = 4)]
        public float CollectionTarget { get; set; }

        [Key]
        [Column(Order = 5)]
        public float CollectionAchieve { get; set; }

        public float? Insentive { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        [Key]
        [Column(Order = 7)]
        public DateTime UpdatedOn { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(10)]
        public string Elegibility { get; set; }
    }
}
