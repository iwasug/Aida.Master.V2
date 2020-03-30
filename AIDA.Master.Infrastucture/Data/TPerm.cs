namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TPerm")]
    public partial class TPerm
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PermID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GroupID { get; set; }

        [StringLength(255)]
        public string MenuName { get; set; }

        [StringLength(255)]
        public string MenuTag { get; set; }

        [StringLength(100)]
        public string ContName { get; set; }

        [StringLength(100)]
        public string ActName { get; set; }
    }
}
