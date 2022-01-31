using Microsoft.EntityFrameworkCore.Migrations;

namespace isaloncoach10.Data.Migrations
{
    public partial class update9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalClientsInDatabase",
                table: "Target");

            migrationBuilder.DropColumn(
                name: "TotalClientsInDatabase",
                table: "Actual");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalClientsInDatabase",
                table: "Target",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalClientsInDatabase",
                table: "Actual",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
