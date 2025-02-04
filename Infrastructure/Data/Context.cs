using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public sealed class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Cart> Carts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>().OwnsMany(c => c.Products);
        modelBuilder.Entity<Order>().OwnsMany(o => o.OrderItems);

        modelBuilder.Entity<User>().HasData(new User {
            Id = Guid.Parse("58dd8e45-25cc-4a25-b6b5-3e250e17d3d8"),
            Login = "Admin",
            Password = "Password",
            Role = Domain.Enums.Role.Manager
        });
    }
}