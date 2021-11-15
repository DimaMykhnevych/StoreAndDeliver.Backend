using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreAndDeliver.DataLayer.Migrations
{
    public partial class AddCargoSnapshotsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CargoSnapshots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CargoSessionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    EnvironmentSettingId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoSnapshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CargoSnapshots_CargoSessions_CargoSessionId",
                        column: x => x.CargoSessionId,
                        principalTable: "CargoSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CargoSnapshots_EnvironmentSettings_EnvironmentSettingId",
                        column: x => x.EnvironmentSettingId,
                        principalTable: "EnvironmentSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CargoSnapshots_CargoSessionId",
                table: "CargoSnapshots",
                column: "CargoSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoSnapshots_EnvironmentSettingId",
                table: "CargoSnapshots",
                column: "EnvironmentSettingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CargoSnapshots");
        }
    }
}
