using System;
using Microsoft.EntityFrameworkCore.Metadata;
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
            migrationBuilder.DropForeignKey(
                name: "FK_MotherBoards_Categories_CategoryId",
                table: "MotherBoards");

            migrationBuilder.DropForeignKey(
                name: "FK_Processors_Categories_CategoryId",
                table: "Processors");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Processors_CategoryId",
                table: "Processors");

            migrationBuilder.DropIndex(
                name: "IX_MotherBoards_CategoryId",
                table: "MotherBoards");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Processors");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "MotherBoards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Processors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "MotherBoards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeleteIn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    InsertIn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdateIn = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Processors_CategoryId",
                table: "Processors",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MotherBoards_CategoryId",
                table: "MotherBoards",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_MotherBoards_Categories_CategoryId",
                table: "MotherBoards",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Processors_Categories_CategoryId",
                table: "Processors",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
