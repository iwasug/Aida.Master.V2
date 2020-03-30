namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TModulUpload")]
    public partial class TModulUpload
    {
        [Key]
        [StringLength(3)]
        public string ApprID { get; set; }

        [StringLength(100)]
        public string ApprName { get; set; }
    }
}
