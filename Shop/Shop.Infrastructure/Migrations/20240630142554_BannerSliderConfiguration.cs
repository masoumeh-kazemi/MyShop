using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BannerSliderConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Specifications",
                schema: "product",
                table: "Specifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                schema: "product",
                table: "Images");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specifications",
                schema: "product",
                table: "Specifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                schema: "product",
                table: "Images",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Banner",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Link = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Slider",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slider", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_ProductId",
                schema: "product",
                table: "Specifications",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                schema: "product",
                table: "Images",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banner");

            migrationBuilder.DropTable(
                name: "Slider");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specifications",
                schema: "product",
                table: "Specifications");

            migrationBuilder.DropIndex(
                name: "IX_Specifications_ProductId",
                schema: "product",
                table: "Specifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                schema: "product",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ProductId",
                schema: "product",
                table: "Images");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specifications",
                schema: "product",
                table: "Specifications",
                columns: new[] { "ProductId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                schema: "product",
                table: "Images",
                columns: new[] { "ProductId", "Id" });
        }
    }
}
