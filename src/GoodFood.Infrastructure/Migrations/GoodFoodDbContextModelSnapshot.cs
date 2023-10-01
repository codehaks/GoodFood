﻿// <auto-generated />
using GoodFood.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GoodFood.Infrastructure.Migrations
{
    [DbContext(typeof(GoodFoodDbContext))]
    partial class GoodFoodDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0-rc.1.23419.6");

            modelBuilder.Entity("GoodFood.Infrastructure.Persistence.Models.FoodCategoryData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("FoodCategories");
                });

            modelBuilder.Entity("GoodFood.Infrastructure.Persistence.Models.FoodData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("GoodFood.Infrastructure.Persistence.Models.MenuLineData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Count")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FoodId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FoodId")
                        .IsUnique();

                    b.ToTable("MenuLines");
                });

            modelBuilder.Entity("GoodFood.Infrastructure.Persistence.Models.FoodData", b =>
                {
                    b.HasOne("GoodFood.Infrastructure.Persistence.Models.FoodCategoryData", "Category")
                        .WithMany("Foods")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("GoodFood.Infrastructure.Persistence.Models.MenuLineData", b =>
                {
                    b.HasOne("GoodFood.Infrastructure.Persistence.Models.FoodData", "Food")
                        .WithOne("MenuLine")
                        .HasForeignKey("GoodFood.Infrastructure.Persistence.Models.MenuLineData", "FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");
                });

            modelBuilder.Entity("GoodFood.Infrastructure.Persistence.Models.FoodCategoryData", b =>
                {
                    b.Navigation("Foods");
                });

            modelBuilder.Entity("GoodFood.Infrastructure.Persistence.Models.FoodData", b =>
                {
                    b.Navigation("MenuLine")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
