using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trend_Fashion_Strore.Migrations
{
    /// <inheritdoc />
    public partial class AddproptoSaleModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ColorId",
                table: "Sales",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_SizeId",
                table: "Sales",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Colors_ColorId",
                table: "Sales",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "ColorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Sizes_SizeId",
                table: "Sales",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "SizeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Colors_ColorId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Sizes_SizeId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_ColorId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_SizeId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "Sales");
        }
    }
}
