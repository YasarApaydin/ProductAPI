using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProductsAPI.Models
{
    public class ProductsContext: IdentityDbContext<AppUser,AppRole,int>
    {
        public ProductsContext(DbContextOptions<ProductsContext> options): base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(new Product { ProductId = 1, ProductName = "IPhone 15", Price = 6000, IsActive = true });
            modelBuilder.Entity<Product>().HasData(new Product { ProductId = 2, ProductName = "IPhone 16", Price = 7000, IsActive = true });
            modelBuilder.Entity<Product>().HasData(new Product { ProductId = 3, ProductName = "IPhone 17", Price = 8000, IsActive = true });
            modelBuilder.Entity<Product>().HasData(new Product { ProductId = 4, ProductName = "IPhone 18", Price = 9000, IsActive = false });
            modelBuilder.Entity<Product>().HasData(new Product { ProductId = 5, ProductName = "IPhone 19", Price = 10000, IsActive = true });



        }
        public DbSet<Product> Products { get; set; }
    }
}
