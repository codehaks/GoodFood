using GoodFood.Infrastructure.Persistence.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FoodData>()
            .HasOne(e => e.MenuLine)
            .WithOne(e => e.Food)
            .HasForeignKey<MenuLineData>(e => e.FoodId)
            .IsRequired();
    }
}
