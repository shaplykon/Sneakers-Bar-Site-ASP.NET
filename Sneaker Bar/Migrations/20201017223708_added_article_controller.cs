using Microsoft.EntityFrameworkCore.Migrations;

namespace Sneaker_Bar.Migrations
{
    public partial class added_article_controller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_article_users_userId",
                table: "article");

            migrationBuilder.DropForeignKey(
                name: "FK_comment_article_articleId",
                table: "comment");

            migrationBuilder.DropForeignKey(
                name: "FK_purchase_sneakers_sneakersId",
                table: "purchase");

            migrationBuilder.DropForeignKey(
                name: "FK_purchase_users_userId",
                table: "purchase");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "purchase",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "sneakersId",
                table: "purchase",
                newName: "SneakersId");

            migrationBuilder.RenameColumn(
                name: "purchaseId",
                table: "purchase",
                newName: "PurchaseId");

            migrationBuilder.RenameIndex(
                name: "IX_purchase_userId",
                table: "purchase",
                newName: "IX_purchase_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_purchase_sneakersId",
                table: "purchase",
                newName: "IX_purchase_SneakersId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "comment",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "comment",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "text",
                table: "comment",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "comment",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "articleId",
                table: "comment",
                newName: "ArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_comment_articleId",
                table: "comment",
                newName: "IX_comment_ArticleId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "article",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "article",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "text",
                table: "article",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "article",
                newName: "Date");

            migrationBuilder.RenameIndex(
                name: "IX_article_userId",
                table: "article",
                newName: "IX_article_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "article",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "article",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageData",
                table: "article",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_article_users_UserId",
                table: "article",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comment_article_ArticleId",
                table: "comment",
                column: "ArticleId",
                principalTable: "article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_purchase_sneakers_SneakersId",
                table: "purchase",
                column: "SneakersId",
                principalTable: "sneakers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_purchase_users_UserId",
                table: "purchase",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_article_users_UserId",
                table: "article");

            migrationBuilder.DropForeignKey(
                name: "FK_comment_article_ArticleId",
                table: "comment");

            migrationBuilder.DropForeignKey(
                name: "FK_purchase_sneakers_SneakersId",
                table: "purchase");

            migrationBuilder.DropForeignKey(
                name: "FK_purchase_users_UserId",
                table: "purchase");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "article");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "purchase",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "SneakersId",
                table: "purchase",
                newName: "sneakersId");

            migrationBuilder.RenameColumn(
                name: "PurchaseId",
                table: "purchase",
                newName: "purchaseId");

            migrationBuilder.RenameIndex(
                name: "IX_purchase_UserId",
                table: "purchase",
                newName: "IX_purchase_userId");

            migrationBuilder.RenameIndex(
                name: "IX_purchase_SneakersId",
                table: "purchase",
                newName: "IX_purchase_sneakersId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "comment",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "comment",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "comment",
                newName: "text");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "comment",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "ArticleId",
                table: "comment",
                newName: "articleId");

            migrationBuilder.RenameIndex(
                name: "IX_comment_ArticleId",
                table: "comment",
                newName: "IX_comment_articleId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "article",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "article",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "article",
                newName: "text");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "article",
                newName: "date");

            migrationBuilder.RenameIndex(
                name: "IX_article_UserId",
                table: "article",
                newName: "IX_article_userId");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "article",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "text",
                table: "article",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_article_users_userId",
                table: "article",
                column: "userId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comment_article_articleId",
                table: "comment",
                column: "articleId",
                principalTable: "article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_purchase_sneakers_sneakersId",
                table: "purchase",
                column: "sneakersId",
                principalTable: "sneakers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_purchase_users_userId",
                table: "purchase",
                column: "userId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
