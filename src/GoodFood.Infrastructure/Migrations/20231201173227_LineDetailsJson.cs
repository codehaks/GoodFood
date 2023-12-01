using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodFood.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LineDetailsJson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "MenuLines",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "MenuLines");
        }
    }
}
