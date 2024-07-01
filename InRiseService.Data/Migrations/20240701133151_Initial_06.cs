using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InRiseService.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_06 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriceId",
                table: "PowerSupplies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PowerSupplyId",
                table: "ImagensProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PowerSupplies_PriceId",
                table: "PowerSupplies",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagensProducts_PowerSupplyId",
                table: "ImagensProducts",
                column: "PowerSupplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagensProducts_PowerSupplies_PowerSupplyId",
                table: "ImagensProducts",
                column: "PowerSupplyId",
                principalTable: "PowerSupplies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PowerSupplies_Prices_PriceId",
                table: "PowerSupplies",
                column: "PriceId",
                principalTable: "Prices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagensProducts_PowerSupplies_PowerSupplyId",
                table: "ImagensProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_PowerSupplies_Prices_PriceId",
                table: "PowerSupplies");

            migrationBuilder.DropIndex(
                name: "IX_PowerSupplies_PriceId",
                table: "PowerSupplies");

            migrationBuilder.DropIndex(
                name: "IX_ImagensProducts_PowerSupplyId",
                table: "ImagensProducts");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "PowerSupplies");

            migrationBuilder.DropColumn(
                name: "PowerSupplyId",
                table: "ImagensProducts");
        }
    }
}
