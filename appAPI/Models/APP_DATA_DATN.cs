using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace appAPI.Models
{
    public class APP_DATA_DATN : IdentityDbContext<Account>
    {
        public APP_DATA_DATN()
        {
            
        }
        public APP_DATA_DATN(DbContextOptions options) : base(options)
        {
        }
       
        public DbSet<Attributes> Attributes { get; set; }
        public DbSet<Banner> Banner { get; set; }
        public DbSet<Carts> Carts { get; set; }
        public DbSet<Cart_details> Cart_Details { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Designer> Designer { get; set; }
        public DbSet<Files>  Files { get; set; }
        public DbSet<Menus> Menus { get; set; }
        public DbSet<Menu_items> Menu_items { get; set; }
        public DbSet<Options> Options { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<order_trackings> Order_Trackings { get; set; }
        public DbSet<Order_details> Order_Details { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Post_categories> Post_Categories { get; set; }
        public DbSet<Post_metas> Post_Metas { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Post_tags> Post_Tags { get; set; }
        public DbSet<Product_attributes> product_Attributes { get; set; }
        public DbSet<Product_variants> product_variants { get; set; }
        public DbSet<Q_A> Q_As { get; set; }
        public DbSet<Seo> Seo { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }
        public DbSet<Designer> Designers { get; set; }
        public DbSet<Order_Vouchers> OrderVouchers { get; set; }
        public DbSet<Vouchers> Vouchers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=ADMIN;Initial Catalog=DUANTOTNGHIEP04;Integrated Security=True;Trust Server Certificate=True");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }


    }
}
