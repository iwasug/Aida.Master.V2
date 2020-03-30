namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CollectionAchievementAR90
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Plant { get; set; }

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

        [StringLength(10)]
        public string Elegibility { get; set; }

        public float? INCSLM { get; set; }

        public float? INCFSS { get; set; }

        public float? INCASM { get; set; }
    }
}
