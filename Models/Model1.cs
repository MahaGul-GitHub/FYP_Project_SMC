namespace SwissMetroCookware.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<DeliveryCharge> DeliveryCharges { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Logistic> Logistics { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Misc> Miscs { get; set; }
        public virtual DbSet<Newsletter> Newsletters { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Phonebook> Phonebooks { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<Wishlist> Wishlists { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }

        public virtual DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.SubCategories)
                .WithRequired(e => e.Category)
                .HasForeignKey(e => e.category_fid)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Company>()
                .Property(e => e.fax)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.customer_fid)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Wishlists)
                .WithRequired(e => e.Customer)
                .HasForeignKey(e => e.customer_fid)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Customer>()
               .HasMany(e => e.Feedbacks)
               .WithRequired(e => e.Customer)
               .HasForeignKey(e => e.customer_fid)
               .WillCascadeOnDelete(true);


            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Admins)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.employee_fid)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Managers)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.employee_fid)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .HasForeignKey(e => e.order_fid)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Feedbacks)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.product_fid)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.product_fid)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Wishlists)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.product_fid)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<SubCategory>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.SubCategory)
                .HasForeignKey(e => e.sub_category_fid)
                .WillCascadeOnDelete(true);
        }
    }
}
