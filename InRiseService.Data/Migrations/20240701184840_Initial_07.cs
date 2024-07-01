using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InRiseService.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_07 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriceId",
                table: "Processors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Processors_PriceId",
                table: "Processors",
                column: "PriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Processors_Prices_PriceId",
                table: "Processors",
                column: "PriceId",
                principalTable: "Prices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processors_Prices_PriceId",
                table: "Processors");

            migrationBuilder.DropIndex(
                name: "IX_Processors_PriceId",
                table: "Processors");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "Processors");
        }
    }
}
