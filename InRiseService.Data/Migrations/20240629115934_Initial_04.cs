using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InRiseService.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MonitorScreenId",
                table: "ImagensProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImagensProducts_MonitorScreenId",
                table: "ImagensProducts",
                column: "MonitorScreenId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagensProducts_MonitorsScreen_MonitorScreenId",
                table: "ImagensProducts",
                column: "MonitorScreenId",
                principalTable: "MonitorsScreen",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagensProducts_MonitorsScreen_MonitorScreenId",
                table: "ImagensProducts");

            migrationBuilder.DropIndex(
                name: "IX_ImagensProducts_MonitorScreenId",
                table: "ImagensProducts");

            migrationBuilder.DropColumn(
                name: "MonitorScreenId",
                table: "ImagensProducts");
        }
    }
}
