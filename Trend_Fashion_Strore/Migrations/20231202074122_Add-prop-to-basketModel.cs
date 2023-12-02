using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trend_Fashion_Strore.Migrations
{
    /// <inheritdoc />
    public partial class AddproptobasketModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MoneyTransformNum",
                table: "Basekts",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PaymentVerification",
                table: "Basekts",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoneyTransformNum",
                table: "Basekts");

            migrationBuilder.DropColumn(
                name: "PaymentVerification",
                table: "Basekts");
        }
    }
}
