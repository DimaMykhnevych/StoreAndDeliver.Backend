using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreAndDeliver.DataLayer.Migrations
{
    public partial class FixTypoErrorInCargoSessionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CargoSeesions");

            migrationBuilder.CreateTable(
                name: "CargoSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CarrierId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CargoRequestId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CargoSessions_CargoRequests_CargoRequestId",
                        column: x => x.CargoRequestId,
                        principalTable: "CargoRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CargoSessions_Carriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CargoSessions_CargoRequestId",
                table: "CargoSessions",
                column: "CargoRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoSessions_CarrierId",
                table: "CargoSessions",
                column: "CarrierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CargoSessions");

            migrationBuilder.CreateTable(
                name: "CargoSeesions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CargoRequestId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CarrierId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoSeesions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CargoSeesions_CargoRequests_CargoRequestId",
                        column: x => x.CargoRequestId,
                        principalTable: "CargoRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CargoSeesions_Carriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CargoSeesions_CargoRequestId",
                table: "CargoSeesions",
                column: "CargoRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoSeesions_CarrierId",
                table: "CargoSeesions",
                column: "CarrierId");
        }
    }
}
