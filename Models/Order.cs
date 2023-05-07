namespace SwissMetroCookware.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int id { get; set; }

        [Required]
        public DateTime date { get; set; }
        
        [Required]
        [StringLength(100)]
        public string status { get; set; }


        public int customer_fid { get; set; }

        [Required]
        [StringLength(200)]
        public string payment_type { get; set; }

        [Required]
        [StringLength(500)]
        public string address { get; set; }

        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Special characters are not allowed")]
        [StringLength(11 , MinimumLength = 11 , ErrorMessage ="Invalid Phone Number")]
        public string contact { get; set; }

        [Required]
        [StringLength(50 , MinimumLength = 12 , ErrorMessage = "Invalid Email")]
        public string email { get; set; }

        [Required]
        [StringLength(50)]
        public string province { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "Special characters are not allowed")]
        public int? amount { get; set; }

        [StringLength(50)]
        public string logistic { get; set; }


        public virtual Customer Customer { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }


    }
}
