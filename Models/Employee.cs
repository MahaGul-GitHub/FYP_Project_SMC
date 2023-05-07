namespace SwissMetroCookware.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            Admins = new HashSet<Admin>();
            Managers = new HashSet<Manager>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be between 3-50")]
        [RegularExpression("^[A-Za-z0-9_\x20]*$", ErrorMessage = "Name can not contain an special characters")]
        public string name { get; set; }

        [Required]
        [StringLength(500 , MinimumLength = 50)]
        public string address { get; set; }

        [Required]
        [StringLength(50 , MinimumLength = 12 , ErrorMessage = "Invalid Email")]
        public string email { get; set; }

        [Required]
        [StringLength(11 , MinimumLength = 11)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Special characters are not allowed")]
        public string contact { get; set; }

        [StringLength(500)]
        public string picture { get; set; }

        [NotMapped]
        public HttpPostedFileBase pic { get; set; }


        [Column(TypeName = "numeric")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Special characters are not allowed")]
        public decimal salary { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("^[A-Za-z0-9_\x20]*$", ErrorMessage = "Name can not contain an special characters")]
        public string designation { get; set; }

        [StringLength(50)]
        public string gender { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Admin> Admins { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Manager> Managers { get; set; }
    }
}
