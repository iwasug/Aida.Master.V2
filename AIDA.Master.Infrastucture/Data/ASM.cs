namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ASM")]
    public partial class ASM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ASM()
        {
            RHHeader = new HashSet<RHHeader>();
            RHHeader1 = new HashSet<RHHeader>();
            RHTeam = new HashSet<RHTeam>();
            RHTeam1 = new HashSet<RHTeam>();
            RHTeam2 = new HashSet<RHTeam>();
            RHTeam3 = new HashSet<RHTeam>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NIK { get; set; }

        [Required]
        [StringLength(150)]
        public string FullName { get; set; }

        public bool Role { get; set; }

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

        [Required]
        [StringLength(4)]
        public string DefaultRayonType { get; set; }

        public int? AllowedByNIK { get; set; }

        public DateTime? UploadValidTo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RHHeader> RHHeader { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RHHeader> RHHeader1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RHTeam> RHTeam { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RHTeam> RHTeam1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RHTeam> RHTeam2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RHTeam> RHTeam3 { get; set; }
    }
}
