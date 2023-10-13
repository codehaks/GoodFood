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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<FoodData>()
            .HasOne(e => e.MenuLine)
            .WithOne(e => e.Food)
            .HasForeignKey<MenuLineData>(e => e.FoodId)
            .IsRequired();

        base.OnModelCreating(builder);
    }
}
