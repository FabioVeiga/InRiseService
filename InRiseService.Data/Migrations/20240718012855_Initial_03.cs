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
                name: "ComputerId",
                table: "ImagensProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImagensProducts_ComputerId",
                table: "ImagensProducts",
                column: "ComputerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagensProducts_Computers_ComputerId",
                table: "ImagensProducts",
                column: "ComputerId",
                principalTable: "Computers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagensProducts_Computers_ComputerId",
                table: "ImagensProducts");

            migrationBuilder.DropIndex(
                name: "IX_ImagensProducts_ComputerId",
                table: "ImagensProducts");

            migrationBuilder.DropColumn(
                name: "ComputerId",
                table: "ImagensProducts");
        }
    }
}
