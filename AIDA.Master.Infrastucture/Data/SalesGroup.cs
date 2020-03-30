namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalesGroup")]
    public partial class SalesGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SalesGroup()
        {
            RHeader = new HashSet<RHeader>();
            RHeader1 = new HashSet<RHeader>();
            RHeader2 = new HashSet<RHeader>();
        }

        [StringLength(3)]
        public string ID { get; set; }

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
        public virtual ICollection<RHeader> RHeader { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RHeader> RHeader1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RHeader> RHeader2 { get; set; }
    }
}
