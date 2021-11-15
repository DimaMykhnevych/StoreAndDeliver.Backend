using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreAndDeliver.DataLayer.Migrations
{
    public partial class AddValueToCargoSnapshots : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "CargoSnapshots",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "CargoSnapshots");
        }
    }
}
