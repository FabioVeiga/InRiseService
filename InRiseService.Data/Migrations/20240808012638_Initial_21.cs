using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InRiseService.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoftwareId",
                table: "ImagensProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImagensProducts_SoftwareId",
                table: "ImagensProducts",
                column: "SoftwareId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagensProducts_Softwares_SoftwareId",
                table: "ImagensProducts",
                column: "SoftwareId",
                principalTable: "Softwares",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagensProducts_Softwares_SoftwareId",
                table: "ImagensProducts");

            migrationBuilder.DropIndex(
                name: "IX_ImagensProducts_SoftwareId",
                table: "ImagensProducts");

            migrationBuilder.DropColumn(
                name: "SoftwareId",
                table: "ImagensProducts");
        }
    }
}
