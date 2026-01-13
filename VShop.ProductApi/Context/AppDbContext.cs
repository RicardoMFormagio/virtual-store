using Microsoft.EntityFrameworkCore;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    private DbSet<Category> Categories { get; set; }
    private DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Category>().HasKey(c => c.CategoryId);
        mb.Entity<Category>().Property(c => c.Name).HasMaxLength(100).IsRequired();

        mb.Entity<Product>().HasKey(p => p.Id);
        mb.Entity<Product>().Property(p => p.Name).HasMaxLength(100).IsRequired();
        mb.Entity<Product>().Property(p => p.Description).HasMaxLength(255).IsRequired();
        mb.Entity<Product>().Property(p => p.ImageURL).HasMaxLength(255).IsRequired();
        mb.Entity<Product>().Property(p => p.Price).HasPrecision(12, 2).IsRequired();

        mb.Entity<Category>().HasMany(c => c.Products).WithOne(p => p.Category)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        mb.Entity<Category>().HasData(
        new Category
        {
            CategoryId = 1,
            Name = "Scholar Materials",
        },
        new Category
        {
            CategoryId = 2,
            Name = "Accessories",
        }
        );
        
        

    }
}