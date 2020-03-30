namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdminExclusive")]
    public partial class AdminExclusive
    {
        public AdminExclusive()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NIK { get; set; }

        [Required]
        [StringLength(150)]
        public string FullName { get; set; }

        [Required]
        [StringLength(3)]
        public string RayonType { get; set; }

        [Column(TypeName = "date")]
        public DateTime ValidFrom { get; set; }

        [Column(TypeName = "date")]
        public DateTime ValidTo { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}
