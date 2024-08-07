using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InRiseService.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ImagensProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImagensProducts_CategoryId",
                table: "ImagensProducts",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagensProducts_Categories_CategoryId",
                table: "ImagensProducts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagensProducts_Categories_CategoryId",
                table: "ImagensProducts");

            migrationBuilder.DropIndex(
                name: "IX_ImagensProducts_CategoryId",
                table: "ImagensProducts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ImagensProducts");
        }
    }
}
