namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Plant")]
    public partial class Plant
    {
        [Key]
        [Column("Plant", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Plant1 { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(150)]
        public string Description { get; set; }
    }
}
