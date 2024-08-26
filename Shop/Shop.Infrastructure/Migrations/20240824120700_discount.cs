using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class discount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                schema: "order",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "DiscountPrice",
                schema: "order",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPrice",
                schema: "order",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "DiscountPercentage",
                schema: "order",
                table: "Items",
                type: "int",
                nullable: true);
        }
    }
}
