namespace SwissMetroCookware.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Phonebook")]
    public partial class Phonebook
    {
        public int id { get; set; }


        [Required]
        [StringLength(100)]
        [RegularExpression("^[A-Za-z0-9_\x20]*$", ErrorMessage = "Name can not contain an special characters")]
        public string name { get; set; }

        [Required]
        [StringLength(4000)]
        public string notes { get; set; }


        [Required]
        [StringLength(11)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Special characters are not allowed")]
        public string phone { get; set; }

        [StringLength(100)]
        public string company { get; set; }
    }
}
