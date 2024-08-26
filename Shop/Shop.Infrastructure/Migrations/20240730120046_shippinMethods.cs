using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class shippinMethods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingMethod",
                table: "ShippingMethod");

            migrationBuilder.RenameTable(
                name: "ShippingMethod",
                newName: "ShippingMethods");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingMethods",
                table: "ShippingMethods",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingMethods",
                table: "ShippingMethods");

            migrationBuilder.RenameTable(
                name: "ShippingMethods",
                newName: "ShippingMethod");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingMethod",
                table: "ShippingMethod",
                column: "Id");
        }
    }
}
