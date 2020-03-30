namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OpenBalanceMonthlyHist")]
    public partial class OpenBalanceMonthlyHist
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TAHUN { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BULAN { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PLANT { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(20)]
        public string REFERENCE { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(6)]
        public string CUSTOMER { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DUEDATE { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(4)]
        public string CG1 { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(3)]
        public string PH3 { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(14)]
        public string MATERIAL { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal AMOUNT_09 { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(10)]
        public string INTERV { get; set; }
    }
}
