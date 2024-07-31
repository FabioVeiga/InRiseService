using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InRiseService.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistorics_OrderStatuses_StatusId",
                table: "OrderHistorics");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "OrderHistorics",
                newName: "OrderStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderHistorics_StatusId",
                table: "OrderHistorics",
                newName: "IX_OrderHistorics_OrderStatusId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "OrderHistorics",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistorics_OrderStatuses_OrderStatusId",
                table: "OrderHistorics",
                column: "OrderStatusId",
                principalTable: "OrderStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistorics_OrderStatuses_OrderStatusId",
                table: "OrderHistorics");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "OrderHistorics");

            migrationBuilder.RenameColumn(
                name: "OrderStatusId",
                table: "OrderHistorics",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderHistorics_OrderStatusId",
                table: "OrderHistorics",
                newName: "IX_OrderHistorics_StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistorics_OrderStatuses_StatusId",
                table: "OrderHistorics",
                column: "StatusId",
                principalTable: "OrderStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
