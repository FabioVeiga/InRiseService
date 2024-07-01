using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InRiseService.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_09 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriceId",
                table: "Towers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Towers_PriceId",
                table: "Towers",
                column: "PriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Towers_Prices_PriceId",
                table: "Towers",
                column: "PriceId",
                principalTable: "Prices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Towers_Prices_PriceId",
                table: "Towers");

            migrationBuilder.DropIndex(
                name: "IX_Towers_PriceId",
                table: "Towers");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "Towers");
        }
    }
}
