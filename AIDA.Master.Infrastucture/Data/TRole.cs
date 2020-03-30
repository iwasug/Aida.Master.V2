namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TRole")]
    public partial class TRole
    {
        [Key]
        public Guid RoleID { get; set; }

        [StringLength(255)]
        public string RoleName { get; set; }

        public int? IsManager { get; set; }

        public int? IsActive { get; set; }

        [StringLength(10)]
        public string status { get; set; }
    }
}
