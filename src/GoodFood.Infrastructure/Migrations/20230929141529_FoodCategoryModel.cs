using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodFood.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FoodCategoryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_FoodCategoryData_CategoryId",
                table: "Foods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodCategoryData",
                table: "FoodCategoryData");

            migrationBuilder.RenameTable(
                name: "FoodCategoryData",
                newName: "FoodCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodCategories",
                table: "FoodCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_FoodCategories_CategoryId",
                table: "Foods",
                column: "CategoryId",
                principalTable: "FoodCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_FoodCategories_CategoryId",
                table: "Foods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodCategories",
                table: "FoodCategories");

            migrationBuilder.RenameTable(
                name: "FoodCategories",
                newName: "FoodCategoryData");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodCategoryData",
                table: "FoodCategoryData",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_FoodCategoryData_CategoryId",
                table: "Foods",
                column: "CategoryId",
                principalTable: "FoodCategoryData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
