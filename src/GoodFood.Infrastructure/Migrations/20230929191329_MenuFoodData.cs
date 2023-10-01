using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodFood.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MenuFoodData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuLines",
                table: "MenuLines");

            migrationBuilder.AlterColumn<int>(
                name: "FoodId",
                table: "MenuLines",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MenuLines",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuLines",
                table: "MenuLines",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MenuLines_FoodId",
                table: "MenuLines",
                column: "FoodId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuLines_Foods_FoodId",
                table: "MenuLines",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuLines_Foods_FoodId",
                table: "MenuLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuLines",
                table: "MenuLines");

            migrationBuilder.DropIndex(
                name: "IX_MenuLines_FoodId",
                table: "MenuLines");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MenuLines");

            migrationBuilder.AlterColumn<int>(
                name: "FoodId",
                table: "MenuLines",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuLines",
                table: "MenuLines",
                column: "FoodId");
        }
    }
}
