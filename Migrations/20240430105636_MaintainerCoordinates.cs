using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class MaintainerCoordinates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MaintainerLatitude",
                table: "AspNetUsers",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MaintainerLongitude",
                table: "AspNetUsers",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaintainerLatitude",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MaintainerLongitude",
                table: "AspNetUsers");
        }
    }
}
