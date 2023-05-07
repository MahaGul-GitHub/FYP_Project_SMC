namespace SwissMetroCookware.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Wishlists = new HashSet<Wishlist>();
            Feedbacks = new HashSet<Feedback>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [StringLength(500)]
        public string description { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cost_price { get; set; }

        [Column(TypeName = "numeric")]
        public decimal sale_price { get; set; }


        [StringLength(500)]
        public string picture { get; set; }

        [NotMapped]
        public HttpPostedFileBase pic { get; set; }

        public int sub_category_fid { get; set; }


        [StringLength(50)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Special characters are not allowed")]
        [Range (0 , 100 , ErrorMessage ="Percenage must be between 0 and 100")]
        public string sale_percent { get; set; }

        [StringLength(50)]
        public string featured_product { get; set; }

        public int quantity { get; set; }

        [NotMapped]
        public int quan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }


        public virtual SubCategory SubCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wishlist> Wishlists { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
