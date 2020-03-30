namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUserRole")]
    public partial class TUserRole
    {
        [Key]
        [Column(Order = 0)]
        public Guid UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual TUser TUser { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid RoleID { get; set; }

        [ForeignKey("RoleID")]
        public virtual TRole TRole { get; set; }
    }
}
