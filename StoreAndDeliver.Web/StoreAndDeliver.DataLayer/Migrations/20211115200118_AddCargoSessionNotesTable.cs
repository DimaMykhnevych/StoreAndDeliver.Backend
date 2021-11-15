using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreAndDeliver.DataLayer.Migrations
{
    public partial class AddCargoSessionNotesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CargoSessionNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NoteCreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Content = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CargoSessionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoSessionNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CargoSessionNotes_CargoSessions_CargoSessionId",
                        column: x => x.CargoSessionId,
                        principalTable: "CargoSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CargoSessionNotes_CargoSessionId",
                table: "CargoSessionNotes",
                column: "CargoSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CargoSessionNotes");
        }
    }
}
