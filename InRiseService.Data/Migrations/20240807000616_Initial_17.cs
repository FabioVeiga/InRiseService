using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InRiseService.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ValueClassification",
                table: "VideosBoard",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ValueClassification",
                table: "Towers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ValueClassification",
                table: "Processors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ValueClassification",
                table: "PowerSupplies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ValueClassification",
                table: "MotherBoards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ValueClassification",
                table: "MonitorsScreen",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ValueClassification",
                table: "MemoriesRom",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ValueClassification",
                table: "MemoriesRam",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ValueClassification",
                table: "Coolers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValueClassification",
                table: "VideosBoard");

            migrationBuilder.DropColumn(
                name: "ValueClassification",
                table: "Towers");

            migrationBuilder.DropColumn(
                name: "ValueClassification",
                table: "Processors");

            migrationBuilder.DropColumn(
                name: "ValueClassification",
                table: "PowerSupplies");

            migrationBuilder.DropColumn(
                name: "ValueClassification",
                table: "MotherBoards");

            migrationBuilder.DropColumn(
                name: "ValueClassification",
                table: "MonitorsScreen");

            migrationBuilder.DropColumn(
                name: "ValueClassification",
                table: "MemoriesRom");

            migrationBuilder.DropColumn(
                name: "ValueClassification",
                table: "MemoriesRam");

            migrationBuilder.DropColumn(
                name: "ValueClassification",
                table: "Coolers");
        }
    }
}
