namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TApproval")]
    public partial class TApproval
    {
        public long ID { get; set; }

        [StringLength(3)]
        public string ApprID { get; set; }

        public int? SLM { get; set; }

        public int? FSS { get; set; }

        public int? ASM { get; set; }

        public int? NSM { get; set; }

        [StringLength(255)]
        public string RSLM { get; set; }

        [StringLength(255)]
        public string RFSS { get; set; }

        [StringLength(255)]
        public string RASM { get; set; }

        [StringLength(255)]
        public string RNSM { get; set; }

        public int? STSLM { get; set; }

        public int? STFSS { get; set; }

        public int? STASM { get; set; }

        public int? STNSM { get; set; }

        public int? Bulan { get; set; }

        public int? Tahun { get; set; }
    }
}
