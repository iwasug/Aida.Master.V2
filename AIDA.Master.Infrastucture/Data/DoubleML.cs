namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DoubleML")]
    public partial class DoubleML
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string RayonCode { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "text")]
        public string SLMName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(6)]
        public string Customer { get; set; }

        [Column(TypeName = "text")]
        public string Name { get; set; }
    }
}
