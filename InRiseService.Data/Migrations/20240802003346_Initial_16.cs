using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InRiseService.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OrderHistorics_OrderId",
                table: "OrderHistorics",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistorics_Orders_OrderId",
                table: "OrderHistorics",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistorics_Orders_OrderId",
                table: "OrderHistorics");

            migrationBuilder.DropIndex(
                name: "IX_OrderHistorics_OrderId",
                table: "OrderHistorics");
        }
    }
}
