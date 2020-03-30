namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RHeader")]
    public partial class RHeader
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RHeader()
        {
            RDetail = new HashSet<RDetail>();
            RDetail1 = new HashSet<RDetail>();
            RHHeader = new HashSet<RHHeader>();
        }

        [Key]
        [StringLength(4)]
        public string RayonType { get; set; }

        [Required]
        [StringLength(3)]
        public string SalesGroup { get; set; }

        [Required]
        [StringLength(150)]
        public string Description { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RDetail> RDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RDetail> RDetail1 { get; set; }

        public virtual SalesGroup SalesGroup1 { get; set; }

        public virtual SalesGroup SalesGroup2 { get; set; }

        public virtual SalesGroup SalesGroup3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RHHeader> RHHeader { get; set; }
    }
}
