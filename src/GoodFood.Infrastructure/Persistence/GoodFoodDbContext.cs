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

        builder.Entity<MenuLineData>()
            .OwnsOne(e => e.Details, b =>
            {
                b.OwnsOne(b => b.Food);
                b.ToJson();
            });

        builder.Entity<OrderLineData>()
            .HasKey(o => new { o.OrderId, o.FoodId });
        var foodCategories = new List<FoodCategoryData>
        {
            new() { Id = 1, Name = "غذای ایرانی" },
            new() { Id = 2, Name = "خوراک" },
            new() { Id = 3, Name = "اقتصادی" },
            new() { Id = 4, Name = "پیش غذا" },
            new() { Id = 5, Name = "نوشیدنی" }
        };

        var foods = new List<FoodData>{
            new() { Id = 8, Name = "چلو خورشت قیمه", Description = "80 گرم گوشت گوسفندی، 250 گرم برنج محلی", CategoryId = 3, ImagePath = "8.jpg" },
            new() { Id = 9, Name = "چلو خورشت قورمه", Description = "80 گرم گوشت گوسفندی، 250 گرم برنج محلی", CategoryId = 3, ImagePath = "9.jpg" },
            new() { Id = 10, Name = "سبزی پلو با گوشت", Description = "200 گرم گوشت، 250 گرم برنج محلی", CategoryId = 3, ImagePath = "10.jpg" },
            new() { Id = 12, Name = "سالاد فصل", Description = "کاهو، گوجه، خیار، هویج رنده شده", CategoryId = 4, ImagePath = "12.jpg" },
            new() { Id = 13, Name = "ماست", Description = "ماست بورانی", CategoryId = 4, ImagePath = "13.jpg" },
            new() { Id = 14, Name = "نوشابه", Description = "نوشابه قوطی", CategoryId = 5, ImagePath = "14.jpg" },
            new() { Id = 4, Name = "خوراک ماهی", Description = "450 گرم ماهی قزل آلا شکم پر، دورچین متناسب فصل", CategoryId = 2, ImagePath = "4.jpg" },
            new() { Id = 7, Name = "کباب شیشلیک", Description = "کباب شیشلیک گوسفندی 450 گرمی، دورچین متناسب فصل", CategoryId = 2, ImagePath = "7.jpg" },
            new() { Id = 1, Name = "چلو کباب کوبیده", Description = "دو عدد کباب کوبیده مخلوط گوشت گوساله و گوسفندی 130 گرمی، 450 گرم برنج ایرانی، دورچین متناسب فصل", CategoryId = 1, ImagePath = "1.jpg" },
            new() { Id = 11, Name = "سوپ جو", Description = "مرغ ریش شده، جو پرک، سبزی سوپ", CategoryId = 4, ImagePath = "11.jpg" },
            new() { Id = 15, Name = "دوغ", Description = "گازدار", CategoryId = 5, ImagePath = "15.jpg" },
            new() { Id = 2, Name = "چلو جوجه کباب", Description = "جوجه کباب سینه مرغ زعفرانی 350 گرمی، 450 گرم برنج ایرانی، دورچین متناسب فصل", CategoryId = 1, ImagePath = "2.jpg" },
            new() { Id = 3, Name = "چلو مرغ", Description = "یک تکه مرغ سس پز 480 گرمی، 450 گرم برنج ایرانی، دورچین متناسب فصل", CategoryId = 1, ImagePath = "3.jpg" },
            new() { Id = 5, Name = "خوراک مرغ", Description = "یک تکه مرغ سس پز 480 گرمی، دورچین متناسب فصل", CategoryId = 2, ImagePath = "5.jpg" },
            new() { Id = 6, Name = "خوراک گوشت بره", Description = "500 گرم گوشت گوسفندی، دورچین متناسب فصل", CategoryId = 2, ImagePath = "6.jpg" }
        };

        builder.Entity<FoodData>().HasData(foods);
        builder.Entity<FoodCategoryData>().HasData(foodCategories);

        base.OnModelCreating(builder);
    }
}
