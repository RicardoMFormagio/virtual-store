using Microsoft.EntityFrameworkCore;
using VShop.CartApi.Models;

namespace VShop.CartApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
    public DbSet<Product>? Products { get; set; }
    public DbSet<CartItem>? CartItems { get; set; }
    public DbSet<CartHeader>? CartHeaders { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        //Products
        mb.Entity<Product>().HasKey(c => c.Id);
        mb.Entity<Product>().Property(c => c.Id).ValueGeneratedNever();
        mb.Entity<Product>().Property(p => p.Name).HasMaxLength(100).IsRequired();
        mb.Entity<Product>().Property(p => p.Description).HasMaxLength(255).IsRequired();
        mb.Entity<Product>().Property(p => p.ImageURL).HasMaxLength(255).IsRequired();
        mb.Entity<Product>().Property(p => p.Price).HasPrecision(12, 2).IsRequired();
        
        //CartHeader
        mb.Entity<CartHeader>().Property(c => c.UserId).HasMaxLength(255).IsRequired();
        mb.Entity<CartHeader>().Property(c => c.CouponCode).HasMaxLength(100);
    }
}