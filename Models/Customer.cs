namespace SwissMetroCookware.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Customer")]
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Orders = new HashSet<Order>();
            Wishlists = new HashSet<Wishlist>();
            Feedbacks = new HashSet<Feedback>();

        }

        public int id { get; set; }

        [Required]
        [StringLength(50 , MinimumLength = 3 , ErrorMessage = "Length must be between 3-50")]
        [RegularExpression("^[A-Za-z0-9_\x20]*$" , ErrorMessage = "Name can not contain an special characters")]
        public string name { get; set; }

        [Required]
        [StringLength(50 , MinimumLength = 12 , ErrorMessage ="Invalid Email Address")]
        public string email { get; set; }

        [Required]
        [StringLength(500)]
        public string address { get; set; }

        [Required]
        [StringLength(15 , MinimumLength = 8 , ErrorMessage ="Length of pasword must be between 8-15 characters")]
        public string password { get; set; }

        [Required]
        [StringLength(11 , MinimumLength = 11 , ErrorMessage ="Phone number must contain 11 digits")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Special characters are not allowed")]
        public string contact { get; set; }

        [Required]
        [StringLength(50)]
        public string province { get; set; }

        [StringLength(500)]
        public string picture { get; set; }

        [NotMapped]
        public HttpPostedFileBase pic { get; set; }


        [Required]
        [StringLength(50)]
        public string gender { get; set; }

        [Required]
        [StringLength(50)]
        public string dob { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wishlist> Wishlists { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
