using GoodFood.Infrastructure.Persistence.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoodFood.Infrastructure.Persistence;
public class GoodFoodDbContext : IdentityDbContext<ApplicationUser>
{
    public GoodFoodDbContext(DbContextOptions<GoodFoodDbContext> options)
    : base(options)
    {
    }

    public DbSet<FoodData> Foods { get; set; }
    public DbSet<MenuLineData> MenuLines { get; set; }
    public DbSet<FoodCategoryData> FoodCategories { get; set; }

    public DbSet<CartData> Carts { get; set; }
    public DbSet<CartLineData> CartLines { get; set; }

    public DbSet<OrderData> Orders { get; set; }
    public DbSet<OrderLineData> OrderLines { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<FoodData>()
            .HasOne(e => e.MenuLine)
            .WithOne(e => e.Food)
            .HasForeignKey<MenuLineData>(e => e.FoodId)
            .IsRequired();

        builder.Entity<OrderLineData>()
            .HasKey(o => new { o.OrderId, o.FoodId });

        base.OnModelCreating(builder);
    }
}
