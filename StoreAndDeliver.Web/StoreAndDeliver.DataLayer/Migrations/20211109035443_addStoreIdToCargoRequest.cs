using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreAndDeliver.DataLayer.Migrations
{
    public partial class addStoreIdToCargoRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "CargoRequests",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_CargoRequests_StoreId",
                table: "CargoRequests",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoRequests_Stores_StoreId",
                table: "CargoRequests",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoRequests_Stores_StoreId",
                table: "CargoRequests");

            migrationBuilder.DropIndex(
                name: "IX_CargoRequests_StoreId",
                table: "CargoRequests");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "CargoRequests");
        }
    }
}
