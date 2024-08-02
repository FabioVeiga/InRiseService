using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InRiseService.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistorics_OrderStatuses_OrderStatusId",
                table: "OrderHistorics");

            migrationBuilder.DropIndex(
                name: "IX_OrderHistorics_OrderStatusId",
                table: "OrderHistorics");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OrderHistorics_OrderStatusId",
                table: "OrderHistorics",
                column: "OrderStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistorics_OrderStatuses_OrderStatusId",
                table: "OrderHistorics",
                column: "OrderStatusId",
                principalTable: "OrderStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
