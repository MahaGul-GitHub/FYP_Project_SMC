namespace SwissMetroCookware.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Sale")]
    public partial class Sale
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [Range (0 , 100 , ErrorMessage = "Percentage must be between 0 to 100")]
        public int percentage { get; set; }

    }
}
