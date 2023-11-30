using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trend_Fashion_Strore.Migrations
{
    /// <inheritdoc />
    public partial class ToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductColorsAndSizes",
                columns: table => new
                {
                    ProductColorsAndSizeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductColorsAndSizes", x => x.ProductColorsAndSizeId);
                    table.ForeignKey(
                        name: "FK_ProductColorsAndSizes_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "ColorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductColorsAndSizes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductColorsAndSizes_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "SizeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductColorsAndSizes_ColorId",
                table: "ProductColorsAndSizes",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColorsAndSizes_ProductId",
                table: "ProductColorsAndSizes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColorsAndSizes_SizeId",
                table: "ProductColorsAndSizes",
                column: "SizeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductColorsAndSizes");
        }
    }
}
