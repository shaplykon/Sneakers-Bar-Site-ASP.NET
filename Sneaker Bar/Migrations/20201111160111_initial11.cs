using Microsoft.EntityFrameworkCore.Migrations;

namespace Sneaker_Bar.Migrations
{
    public partial class initial11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_article_AspNetUsers_UserId1",
                table: "article");

            migrationBuilder.DropIndex(
                name: "IX_article_UserId1",
                table: "article");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "article");

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "article",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "article");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "article",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_article_UserId1",
                table: "article",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_article_AspNetUsers_UserId1",
                table: "article",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
