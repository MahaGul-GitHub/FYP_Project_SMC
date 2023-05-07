namespace SwissMetroCookware.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Manager")]
    public partial class Manager
    {
        public int id { get; set; }

        public int employee_fid { get; set; }

        [Required]
        [StringLength(50 , MinimumLength = 12 , ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }

        [Required]
        [StringLength(15 , MinimumLength = 8 , ErrorMessage ="Password length must be between 8-15 characters")]
        public string password { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
