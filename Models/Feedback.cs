namespace SwissMetroCookware.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Feedback")]
    public partial class Feedback
    {
        public int id { get; set; }

        [Column("feedback")]
        [Required]
        [StringLength(2500)]
        public string feedback1 { get; set; }

        public int customer_fid { get; set; }

        public int product_fid { get; set; }

        [Range (1 , 5 , ErrorMessage = "Choose a number between 1 and 5")]
        public int rating { get; set; }



        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }


    }
}
