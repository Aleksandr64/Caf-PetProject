using Cafe.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Infrastructure.Context;

public class CafeDbContext : DbContext
{
    public CafeDbContext(DbContextOptions<CafeDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CS_AS");
        modelBuilder.AddDishMapping();
        modelBuilder.AddOrderMapping();
        modelBuilder.AddOrderItemMapping();
        modelBuilder.AddTokenMapping();
        modelBuilder.AddUserMapping();
    }
    
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Token> Tokens { get; set; }
}