namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CallActual")]
    public partial class CallActual
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

        public int? Status { get; set; }

        public DateTime? TimeIn { get; set; }

        public DateTime? TimeOut { get; set; }

        public int? ReasonCode { get; set; }

        [Column(TypeName = "text")]
        public string ReasonDesc { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        [StringLength(6)]
        public string Customer { get; set; }

        [StringLength(1)]
        public string RecordStatus { get; set; }

        [StringLength(50)]
        public string ApprovedBy { get; set; }

        public DateTime? ApprovedOn { get; set; }

        [StringLength(1)]
        public string ApprovedStatus { get; set; }
    }
}
