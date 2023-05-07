namespace SwissMetroCookware.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        public int id { get; set; }

        public int employee_fid { get; set; }

        [Required]
        [StringLength(15 , MinimumLength = 8 , ErrorMessage = "Password must be atleast 8 characters long, and less than 15 characters")]
        public string password { get; set; }

        [Required]
        [StringLength(50 , MinimumLength = 10 , ErrorMessage = "Length must be between 10 and 50 characters")]
        public string email { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
