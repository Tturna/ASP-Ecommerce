using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class ReviewFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviewModel_Products_ProductModelId",
                table: "ProductReviewModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductReviewModel",
                table: "ProductReviewModel");

            migrationBuilder.DropIndex(
                name: "IX_ProductReviewModel_ProductModelId",
                table: "ProductReviewModel");

            migrationBuilder.DropColumn(
                name: "ProductModelId",
                table: "ProductReviewModel");

            migrationBuilder.RenameTable(
                name: "ProductReviewModel",
                newName: "ProductReviews");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductReviews",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ProductReviews",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductReviews",
                table: "ProductReviews",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ProductId",
                table: "ProductReviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_UserId",
                table: "ProductReviews",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviews_AspNetUsers_UserId",
                table: "ProductReviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviews_Products_ProductId",
                table: "ProductReviews",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviews_AspNetUsers_UserId",
                table: "ProductReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviews_Products_ProductId",
                table: "ProductReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductReviews",
                table: "ProductReviews");

            migrationBuilder.DropIndex(
                name: "IX_ProductReviews_ProductId",
                table: "ProductReviews");

            migrationBuilder.DropIndex(
                name: "IX_ProductReviews_UserId",
                table: "ProductReviews");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductReviews");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProductReviews");

            migrationBuilder.RenameTable(
                name: "ProductReviews",
                newName: "ProductReviewModel");

            migrationBuilder.AddColumn<int>(
                name: "ProductModelId",
                table: "ProductReviewModel",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductReviewModel",
                table: "ProductReviewModel",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviewModel_ProductModelId",
                table: "ProductReviewModel",
                column: "ProductModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviewModel_Products_ProductModelId",
                table: "ProductReviewModel",
                column: "ProductModelId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
