namespace SwissMetroCookware.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Newsletter")]
    public partial class Newsletter
    {
        public int id { get; set; }

        [Required]
        [StringLength(50 , MinimumLength = 12 , ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }
    }
}
