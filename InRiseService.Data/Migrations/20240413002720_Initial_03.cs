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
            migrationBuilder.AddColumn<bool>(
                name: "IsBilling",
                table: "UserAddresses",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBilling",
                table: "UserAddresses");
        }
    }
}
