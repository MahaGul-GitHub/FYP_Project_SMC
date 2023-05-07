namespace SwissMetroCookware.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Message")]
    public partial class Message
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("^[A-Za-z0-9_\x20]*$", ErrorMessage = "Name can not contain an special characters")]
        public string first_name { get; set; }


        [Required]
        [StringLength(11, MinimumLength = 11)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Special characters are not allowed")]
        public string contact { get; set; }

        [Required]
        [StringLength(200)]
        public string subject { get; set; }

        [Required]
        [StringLength(4000)]
        public string body { get; set; }

        [StringLength(50)]
        public string status { get; set; }

        [Required]
        [StringLength(50)]
        public string email { get; set; }



    }
}
