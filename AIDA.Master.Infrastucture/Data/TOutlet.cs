namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TOutlet")]
    public partial class TOutlet
    {
        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Tahun { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Bulan { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string RayonCode { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SLM { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(150)]
        public string SLM_Name { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(25)]
        public string Customer { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(150)]
        public string CustomerName { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "numeric")]
        public decimal TotalSales { get; set; }

        [Key]
        [Column(Order = 9, TypeName = "numeric")]
        public decimal TotalReturn { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TotalRow { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Status { get; set; }
    }
}
