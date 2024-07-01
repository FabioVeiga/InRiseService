using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InRiseService.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriceId",
                table: "MonitorsScreen",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MonitorsScreen_PriceId",
                table: "MonitorsScreen",
                column: "PriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_MonitorsScreen_Prices_PriceId",
                table: "MonitorsScreen",
                column: "PriceId",
                principalTable: "Prices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonitorsScreen_Prices_PriceId",
                table: "MonitorsScreen");

            migrationBuilder.DropIndex(
                name: "IX_MonitorsScreen_PriceId",
                table: "MonitorsScreen");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "MonitorsScreen");
        }
    }
}
