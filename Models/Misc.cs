namespace SwissMetroCookware.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Misc")]
    public partial class Misc
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [StringLength(50)]
        public string year { get; set; }

        [StringLength(4000)]
        public string description { get; set; }

        [StringLength(50)]
        public string amount { get; set; }
    }
}
