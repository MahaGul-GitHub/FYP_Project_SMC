namespace SwissMetroCookware.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderDetail()
        {

        }

        public int id { get; set; }

        public int order_fid { get; set; }

        public int quantity { get; set; }

        public int product_fid { get; set; }

        [Column(TypeName = "numeric")]
        public decimal cost_price { get; set; }

        [Column(TypeName = "numeric")]
        public decimal sale_price { get; set; }

        [StringLength(100)]
        public string feedback_status { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }


    }
}
