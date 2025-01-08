using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product_Crud.Migrations
{
    /// <inheritdoc />
    public partial class ProductDbInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "colors",
                columns: table => new
                {
                    CId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_colors", x => x.CId);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    PId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    IsAviable = table.Column<bool>(type: "bit", nullable: false),
                    PDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.PId);
                });

            migrationBuilder.CreateTable(
                name: "details",
                columns: table => new
                {
                    DId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PId = table.Column<int>(type: "int", nullable: false),
                    CId = table.Column<int>(type: "int", nullable: false),
                    ProductsPId = table.Column<int>(type: "int", nullable: true),
                    ColorsCId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_details", x => x.DId);
                    table.ForeignKey(
                        name: "FK_details_colors_ColorsCId",
                        column: x => x.ColorsCId,
                        principalTable: "colors",
                        principalColumn: "CId");
                    table.ForeignKey(
                        name: "FK_details_products_ProductsPId",
                        column: x => x.ProductsPId,
                        principalTable: "products",
                        principalColumn: "PId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_details_ColorsCId",
                table: "details",
                column: "ColorsCId");

            migrationBuilder.CreateIndex(
                name: "IX_details_ProductsPId",
                table: "details",
                column: "ProductsPId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "details");

            migrationBuilder.DropTable(
                name: "colors");

            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
