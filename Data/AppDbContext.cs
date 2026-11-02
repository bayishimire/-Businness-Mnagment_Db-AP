using Microsoft.EntityFrameworkCore;
using Businness_Mnagment_Db_API.Models;

namespace Businness_Mnagment_Db_API.Data;

public class AppDbContext : DbContext
{
    // Constructor: receives database configuration through Dependency Injection
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Represents the Products table in MySQL
    public DbSet<Products> Products { get; set; }

    // Represents the Customers table
    public DbSet<Customers> Customers { get; set; }

    // Represents the Categories table
    public DbSet<Categories> Categories { get; set; }

    // Represents the Payments table
    public DbSet<Payments> Payments { get; set; }

    // Represents the Orders table
    public DbSet<Orders> Orders { get; set; }

    // Represents the OrderItems table
    public DbSet<OrderItems> OrderItems { get; set; }

    
    
    
    // Configure primary keys explicitly
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Products>()
            .HasKey(p => p.ProductId);

        modelBuilder.Entity<Customers>()
            .HasKey(c => c.CustomerId);

        modelBuilder.Entity<Categories>()
            .HasKey(c => c.CategoryId);

        modelBuilder.Entity<Payments>()
            .HasKey(p => p.PaymentId);

        modelBuilder.Entity<Orders>()
            .HasKey(o => o.OrderId);

        modelBuilder.Entity<OrderItems>()
            .HasKey(o => o.OrderItemId);
    }
}