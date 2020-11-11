using Microsoft.EntityFrameworkCore.Migrations;

namespace Sneaker_Bar.Migrations
{
    public partial class initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "comment",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_comment_UserId1",
                table: "comment",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_comment_AspNetUsers_UserId1",
                table: "comment",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comment_AspNetUsers_UserId1",
                table: "comment");

            migrationBuilder.DropIndex(
                name: "IX_comment_UserId1",
                table: "comment");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "comment");
        }
    }
}
