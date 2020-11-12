using Microsoft.EntityFrameworkCore.Migrations;

namespace Sneaker_Bar.Migrations
{
    public partial class articlechanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentsAmount",
                table: "article",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "article",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentsAmount",
                table: "article");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "article");
        }
    }
}
