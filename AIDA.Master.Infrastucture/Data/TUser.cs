namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUser")]
    public partial class TUser
    {
        [Key]
        public Guid UserID { get; set; }

        public int? IsActive { get; set; }

        public int? EmpID { get; set; }

        [StringLength(255)]
        public string PasswordHash { get; set; }

        [StringLength(255)]
        public string SecurityStamp { get; set; }

        [StringLength(255)]
        public string UserName { get; set; }

        public int? IsAsk { get; set; }

        public int? IsLock { get; set; }

        public virtual ICollection<TUserRole> TUserRole { get; set; }
    }
}
