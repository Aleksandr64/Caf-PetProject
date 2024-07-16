using Cafe.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Infrastructure.Context;

public static class Mapping
{
    public static void AddDishMapping(this ModelBuilder modelBuilder)
    {
        var dish = modelBuilder.Entity<Dish>();
        dish.HasKey(d => d.Id);
        dish.Property(d => d.Title).IsRequired().HasMaxLength(255);
        dish.Property(d => d.Description).IsRequired().HasMaxLength(1000);
        dish.Property(d => d.Price).IsRequired();
        dish.Property(d => d.ImageUrl).HasMaxLength(1000);
        dish.Property(o => o.DateCreate).IsRequired();
        dish.Property(o => o.DateUpdate).IsRequired();
    }

    public static void AddOrderMapping(this ModelBuilder modelBuilder)
    {
        var order = modelBuilder.Entity<Order>();
        order.HasKey(o => o.Id);
        order.Property(o => o.CustomerName).IsRequired().HasMaxLength(255);
        order.Property(o => o.PhoneNumber).IsRequired().HasMaxLength(50);
        order.Property(o => o.Address).IsRequired().HasMaxLength(500);
        order.Property(o => o.EmailAddres).IsRequired().HasMaxLength(255);
        order.Property(o => o.TotalAmount).IsRequired();
        order.Property(o => o.UserName).HasMaxLength(255);
        order.Property(o => o.DateCreate).IsRequired();
        order.Property(o => o.DateUpdate).IsRequired();

        order.HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserName)
            .HasPrincipalKey(u => u.UserName);
    }

    public static void AddOrderItemMapping(this ModelBuilder modelBuilder)
    {
        var orderItem = modelBuilder.Entity<OrderItem>();
        orderItem.HasKey(oi => oi.Id);
        orderItem.Property(oi => oi.Quantity).IsRequired();
        orderItem.Property(oi => oi.DishId).IsRequired();
        orderItem.Property(oi => oi.DateCreate).IsRequired();
        orderItem.Property(oi => oi.DateUpdate).IsRequired();

        orderItem.HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);

        orderItem.HasOne(oi => oi.Dish)
            .WithMany(d => d.OrderItems)
            .HasForeignKey(oi => oi.DishId);
    }

    public static void AddTokenMapping(this ModelBuilder modelBuilder)
    {
        var token = modelBuilder.Entity<Token>();
        token.HasKey(t => t.Id);
        token.Property(t => t.RefreshToken).IsRequired().HasMaxLength(500);
        token.Property(t => t.RefreshTokenExpiredTime).IsRequired();
        token.Property(t => t.DateCreate).IsRequired();
        token.Property(t => t.DateUpdate).IsRequired();

        token.HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public static void AddUserMapping(this ModelBuilder modelBuilder)
    {
        var user = modelBuilder.Entity<User>();
        user.HasKey(u => u.Id);
        user.Property(u => u.FirstName).IsRequired().HasMaxLength(255);
        user.Property(u => u.LastName).IsRequired().HasMaxLength(255);
        user.Property(u => u.UserName).IsRequired().HasMaxLength(255);
        user.Property(u => u.Email).IsRequired().HasMaxLength(255);
        user.Property(u => u.PhoneNumber).IsRequired().HasMaxLength(50);
        user.Property(u => u.PasswordHash).IsRequired();
        user.Property(u => u.PasswordSalt).IsRequired();
        user.Property(u => u.Role).IsRequired().HasMaxLength(50);
        user.Property(u => u.DateCreate).IsRequired();
        user.Property(u => u.DateUpdate).IsRequired();

        user.HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserName)
            .HasPrincipalKey(u => u.UserName);
    }
}