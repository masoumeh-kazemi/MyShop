using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CompleteOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                schema: "order",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_OrderId",
                schema: "order",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_OrderId",
                schema: "order",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "ShippingMethodId",
                schema: "order",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ActiveAddress",
                schema: "order",
                table: "Addresses");

            migrationBuilder.AddColumn<int>(
                name: "Discount_DiscountAmount",
                schema: "order",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discount_DiscountTitle",
                schema: "order",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShippingMethod_ShippingCost",
                schema: "order",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingMethod_ShippingType",
                schema: "order",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                schema: "order",
                table: "Addresses",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PostalAddress",
                schema: "order",
                table: "Addresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NationalCode",
                schema: "order",
                table: "Addresses",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "order",
                table: "Addresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Family",
                schema: "order",
                table: "Addresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                schema: "order",
                table: "Addresses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                schema: "order",
                table: "Items",
                columns: new[] { "OrderId", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_OrderId",
                schema: "order",
                table: "Addresses",
                column: "OrderId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                schema: "order",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_OrderId",
                schema: "order",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Discount_DiscountAmount",
                schema: "order",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Discount_DiscountTitle",
                schema: "order",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingMethod_ShippingCost",
                schema: "order",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingMethod_ShippingType",
                schema: "order",
                table: "Orders");

            migrationBuilder.AddColumn<long>(
                name: "ShippingMethodId",
                schema: "order",
                table: "Orders",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                schema: "order",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "PostalAddress",
                schema: "order",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "NationalCode",
                schema: "order",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "order",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Family",
                schema: "order",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                schema: "order",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<bool>(
                name: "ActiveAddress",
                schema: "order",
                table: "Addresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                schema: "order",
                table: "Items",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_OrderId",
                schema: "order",
                table: "Items",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_OrderId",
                schema: "order",
                table: "Addresses",
                column: "OrderId");
        }
    }
}
