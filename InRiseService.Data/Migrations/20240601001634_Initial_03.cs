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
            migrationBuilder.RenameColumn(
                name: "Potency",
                table: "MemoriesRam",
                newName: "Capacity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "MemoriesRam",
                newName: "Potency");
        }
    }
}
