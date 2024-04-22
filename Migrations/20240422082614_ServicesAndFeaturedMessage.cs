using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class ServicesAndFeaturedMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FeaturedMaintainerMessage",
                table: "AspNetUsers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaintainerServices",
                table: "AspNetUsers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeaturedMaintainerMessage",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MaintainerServices",
                table: "AspNetUsers");
        }
    }
}
