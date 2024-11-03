using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace appAPI.Models
{
    public class APP_DATA_DATN : IdentityDbContext<Account>
    {
        private readonly IConfiguration _configuration;
        public APP_DATA_DATN(IConfiguration configuration, DbContextOptions<APP_DATA_DATN> options) : base(options)
        {
            _configuration = configuration;
        }
        public DbSet<Activity_history> Activity_history { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Banner> Banner { get; set; }
        public DbSet<Carts> Carts { get; set; }
        public DbSet<Cart_details> Cart_Details { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Designer> Designer { get; set; }
        public DbSet<Discount> Discount { get; set; }
        public DbSet<Files>  Files { get; set; }
        public DbSet<Menus> Menus { get; set; }
        public DbSet<Menu_items> Menu_items { get; set; }
        public DbSet<Options> Options { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<order_trackings> Order_Trackings { get; set; }
        public DbSet<Order_details> Order_Details { get; set; }
        public DbSet<Order_Vouchers> OrderVouchers { get; set; }
        public DbSet<P_variants_discount> p_Variants_Discounts { get; set; }
        public DbSet<Post_categories> Post_Categories { get; set; }
        public DbSet<Post_tags> Post_Tags { get; set; }
        public DbSet<Product_Posts> Posts { get; set; }
        public DbSet<Product_variants> product_variants { get; set; }
        public DbSet<Product_variants_wishlist> Product_Variants_Wishlists { get; set; }
        public DbSet<Q_A> Q_As { get; set; }
        public DbSet<Seo> Seo { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }
        public DbSet<Vouchers> Vouchers { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Textile_technology> Textile_Technologies { get; set; }

        public DbSet<UserVouchers> userVouchers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
