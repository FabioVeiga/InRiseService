using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InRiseService.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Softwares",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Softwares_CategoryId",
                table: "Softwares",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Softwares_Categories_CategoryId",
                table: "Softwares",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Softwares_Categories_CategoryId",
                table: "Softwares");

            migrationBuilder.DropIndex(
                name: "IX_Softwares_CategoryId",
                table: "Softwares");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Softwares");
        }
    }
}
