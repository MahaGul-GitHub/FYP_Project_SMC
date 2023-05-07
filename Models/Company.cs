namespace SwissMetroCookware.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Company")]
    public partial class Company
    {
        public int id { get; set; }

        [Required (ErrorMessage = "Company Name must not be empty")]
        public string name { get; set; }

        [Required]
        [StringLength(70)]
        public string email { get; set; }

        [Required]
        public string contact1 { get; set; }

        [Required]
        [StringLength(500)]
        public string address { get; set; }

        [StringLength(500)]
        public string logo { get; set; }

        [NotMapped]
        public HttpPostedFileBase pic { get; set; }


        [Required]
        [StringLength(4000)]
        public string description { get; set; }

        [Required]
        public string contact2 { get; set; }

        [Required]
        [StringLength(50)]
        public string fax { get; set; }
    }
}
