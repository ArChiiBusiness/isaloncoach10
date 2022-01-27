using Microsoft.EntityFrameworkCore.Migrations;

namespace isaloncoach10.Data.Migrations
{
    public partial class update7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AverageBill",
                table: "Target",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AverageClientVisitsYear",
                table: "Target",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RetailPercent",
                table: "Target",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalYearTarget",
                table: "Target",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "WeeksBetweenAppointments",
                table: "Target",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageBill",
                table: "Target");

            migrationBuilder.DropColumn(
                name: "AverageClientVisitsYear",
                table: "Target");

            migrationBuilder.DropColumn(
                name: "RetailPercent",
                table: "Target");

            migrationBuilder.DropColumn(
                name: "TotalYearTarget",
                table: "Target");

            migrationBuilder.DropColumn(
                name: "WeeksBetweenAppointments",
                table: "Target");
        }
    }
}
