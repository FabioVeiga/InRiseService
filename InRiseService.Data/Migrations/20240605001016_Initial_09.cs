﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InRiseService.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_09 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coolers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Air = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Refrigeration = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FanQuantity = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    FanDiametric = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    MaxVelocit = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    Dimension = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    InsertIn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateIn = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeleteIn = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coolers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coolers");
        }
    }
}
