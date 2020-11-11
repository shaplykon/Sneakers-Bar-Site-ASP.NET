using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Sneaker_Bar.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {         
            migrationBuilder.CreateTable(
                name: "sneakers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Price = table.Column<double>(nullable: false),
                    Model = table.Column<string>(nullable: false),
                    Company = table.Column<string>(nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    ImageData = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sneakers", x => x.Id);
                });

            

            migrationBuilder.CreateTable(
                name: "article",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    ImageData = table.Column<string>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    UserId1 = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_article_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

          
            migrationBuilder.CreateTable(
                name: "purchase",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    UserId1 = table.Column<string>(nullable: true),
                    SneakersId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    isConfirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase", x => x.PurchaseId);
                    table.ForeignKey(
                        name: "FK_purchase_sneakers_SneakersId",
                        column: x => x.SneakersId,
                        principalTable: "sneakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_purchase_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "comment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    ArticleId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_comment_article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_article_UserId1",
                table: "article",
                column: "UserId1");


            migrationBuilder.CreateIndex(
                name: "IX_comment_ArticleId",
                table: "comment",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_SneakersId",
                table: "purchase",
                column: "SneakersId");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_UserId1",
                table: "purchase",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "comment");

            migrationBuilder.DropTable(
                name: "purchase");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "article");

            migrationBuilder.DropTable(
                name: "sneakers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
