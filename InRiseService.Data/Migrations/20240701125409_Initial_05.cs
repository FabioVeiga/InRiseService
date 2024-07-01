using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InRiseService.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriceId",
                table: "MotherBoards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MotherBoardId",
                table: "ImagensProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MotherBoards_PriceId",
                table: "MotherBoards",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagensProducts_MotherBoardId",
                table: "ImagensProducts",
                column: "MotherBoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagensProducts_MotherBoards_MotherBoardId",
                table: "ImagensProducts",
                column: "MotherBoardId",
                principalTable: "MotherBoards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MotherBoards_Prices_PriceId",
                table: "MotherBoards",
                column: "PriceId",
                principalTable: "Prices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagensProducts_MotherBoards_MotherBoardId",
                table: "ImagensProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_MotherBoards_Prices_PriceId",
                table: "MotherBoards");

            migrationBuilder.DropIndex(
                name: "IX_MotherBoards_PriceId",
                table: "MotherBoards");

            migrationBuilder.DropIndex(
                name: "IX_ImagensProducts_MotherBoardId",
                table: "ImagensProducts");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "MotherBoards");

            migrationBuilder.DropColumn(
                name: "MotherBoardId",
                table: "ImagensProducts");
        }
    }
}
