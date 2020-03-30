namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CallTarget")]
    public partial class CallTarget
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string RayonCode { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "date")]
        public DateTime Date { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sequence { get; set; }

        [Required]
        [StringLength(6)]
        public string Customer { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        [Required]
        [StringLength(1)]
        public string Status { get; set; }
    }
}
