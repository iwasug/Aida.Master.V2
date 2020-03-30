namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CallIncentivesASM")]
    public partial class CallIncentivesASM
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ASM { get; set; }

        [StringLength(50)]
        public string ASM_Name { get; set; }

        public float? CallTarget { get; set; }

        public float? CallActual { get; set; }

        public float? CallAch { get; set; }

        public float? CallAchAdjust { get; set; }

        public float? CallIncentives { get; set; }

        public int? Quarter { get; set; }

        public int? Tahun { get; set; }

        [StringLength(50)]
        public string UpdateBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
