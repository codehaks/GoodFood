using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodFood.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FoodImagePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Foods",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Foods");
        }
    }
}
