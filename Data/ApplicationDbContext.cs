using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OFFSTEP.Models;
namespace OFFSTEP.Data;
public class ApplicationDbContext : IdentityDbContext
{
 public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
 public DbSet<Product> Products { get; set; }
 public DbSet<Category> Categories { get; set; }
 public DbSet<Order> Orders { get; set; }
 public DbSet<OrderItem> OrderItems { get; set; }
 protected override void OnModelCreating(ModelBuilder builder)
 {
  base.OnModelCreating(builder);
  builder.Entity<Product>().HasIndex(x => x.Sku).IsUnique();
  builder.Entity<Product>().HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
  builder.Entity<OrderItem>().HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);
  builder.Entity<OrderItem>().HasOne(x => x.Order).WithMany(x => x.Items).HasForeignKey(x => x.OrderId);
 }
}
