using Microsoft.EntityFrameworkCore;
using StreamShop.API.Models;

namespace StreamShop.API.Context;

public class SQLiteDbContext : DbContext
{
    public DbSet<ProductImages> ProductImages { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Category> Category { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=app.db;Cache=Shared");
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>()
            .HasOne(c => c.Category);
        
        modelBuilder.Entity<Product>()
            .HasMany(b => b.Images)
            .WithOne();

        modelBuilder.Entity<Product>()
            .Navigation(b => b.Images)
            .UsePropertyAccessMode(PropertyAccessMode.Property);
    }
}