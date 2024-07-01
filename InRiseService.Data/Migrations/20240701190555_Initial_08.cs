using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InRiseService.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_08 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcessorId",
                table: "ImagensProducts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TowerId",
                table: "ImagensProducts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VideoBoardId",
                table: "ImagensProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImagensProducts_ProcessorId",
                table: "ImagensProducts",
                column: "ProcessorId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagensProducts_TowerId",
                table: "ImagensProducts",
                column: "TowerId");

            migrationBuilder.CreateIndex(
                name: "IX_ImagensProducts_VideoBoardId",
                table: "ImagensProducts",
                column: "VideoBoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagensProducts_Processors_ProcessorId",
                table: "ImagensProducts",
                column: "ProcessorId",
                principalTable: "Processors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagensProducts_Towers_TowerId",
                table: "ImagensProducts",
                column: "TowerId",
                principalTable: "Towers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagensProducts_VideosBoard_VideoBoardId",
                table: "ImagensProducts",
                column: "VideoBoardId",
                principalTable: "VideosBoard",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagensProducts_Processors_ProcessorId",
                table: "ImagensProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ImagensProducts_Towers_TowerId",
                table: "ImagensProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ImagensProducts_VideosBoard_VideoBoardId",
                table: "ImagensProducts");

            migrationBuilder.DropIndex(
                name: "IX_ImagensProducts_ProcessorId",
                table: "ImagensProducts");

            migrationBuilder.DropIndex(
                name: "IX_ImagensProducts_TowerId",
                table: "ImagensProducts");

            migrationBuilder.DropIndex(
                name: "IX_ImagensProducts_VideoBoardId",
                table: "ImagensProducts");

            migrationBuilder.DropColumn(
                name: "ProcessorId",
                table: "ImagensProducts");

            migrationBuilder.DropColumn(
                name: "TowerId",
                table: "ImagensProducts");

            migrationBuilder.DropColumn(
                name: "VideoBoardId",
                table: "ImagensProducts");
        }
    }
}
