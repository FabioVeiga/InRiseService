using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InRiseService.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriceId",
                table: "MemoriesRom",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MemoryRamId",
                table: "ImagensProducts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemoryRomId",
                table: "ImagensProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemoriesRom_PriceId",
                table: "MemoriesRom",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagensProducts_MemoryRamId",
                table: "ImagensProducts",
                column: "MemoryRamId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagensProducts_MemoryRomId",
                table: "ImagensProducts",
                column: "MemoryRomId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagensProducts_MemoriesRam_MemoryRamId",
                table: "ImagensProducts",
                column: "MemoryRamId",
                principalTable: "MemoriesRam",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagensProducts_MemoriesRom_MemoryRomId",
                table: "ImagensProducts",
                column: "MemoryRomId",
                principalTable: "MemoriesRom",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MemoriesRom_Prices_PriceId",
                table: "MemoriesRom",
                column: "PriceId",
                principalTable: "Prices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagensProducts_MemoriesRam_MemoryRamId",
                table: "ImagensProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ImagensProducts_MemoriesRom_MemoryRomId",
                table: "ImagensProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_MemoriesRom_Prices_PriceId",
                table: "MemoriesRom");

            migrationBuilder.DropIndex(
                name: "IX_MemoriesRom_PriceId",
                table: "MemoriesRom");

            migrationBuilder.DropIndex(
                name: "IX_ImagensProducts_MemoryRamId",
                table: "ImagensProducts");

            migrationBuilder.DropIndex(
                name: "IX_ImagensProducts_MemoryRomId",
                table: "ImagensProducts");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "MemoriesRom");

            migrationBuilder.DropColumn(
                name: "MemoryRamId",
                table: "ImagensProducts");

            migrationBuilder.DropColumn(
                name: "MemoryRomId",
                table: "ImagensProducts");
        }
    }
}
