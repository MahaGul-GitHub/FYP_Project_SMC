namespace SwissMetroCookware.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Wishlist")]
    public partial class Wishlist
    {
        public int id { get; set; }

        public int customer_fid { get; set; }

        public int product_fid { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }
    }
}
