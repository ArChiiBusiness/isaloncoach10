using Microsoft.EntityFrameworkCore.Migrations;

namespace isaloncoach10.Data.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentUrl",
                table: "Submissions");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "Submissions",
                newName: "TotalIndividualClientVisitsYear");

            migrationBuilder.RenameColumn(
                name: "TotalTakings",
                table: "Submissions",
                newName: "TotalClientsInDatabase");

            migrationBuilder.RenameColumn(
                name: "MonthNumber",
                table: "Submissions",
                newName: "TotalClientVisitsYear");

            migrationBuilder.AddColumn<int>(
                name: "ClientVisitsMonth",
                table: "Submissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NewClientsMonth",
                table: "Submissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "PastYearTotalTakings",
                table: "Submissions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RebooksMonth",
                table: "Submissions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RetailMonthUSD",
                table: "Submissions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TargetClientsMonth",
                table: "Submissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "TargetMonthUSD",
                table: "Submissions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalMonthlyTakings",
                table: "Submissions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "WageBillMonthUSD",
                table: "Submissions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientVisitsMonth",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "NewClientsMonth",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "PastYearTotalTakings",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "RebooksMonth",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "RetailMonthUSD",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "TargetClientsMonth",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "TargetMonthUSD",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "TotalMonthlyTakings",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "WageBillMonthUSD",
                table: "Submissions");

            migrationBuilder.RenameColumn(
                name: "TotalIndividualClientVisitsYear",
                table: "Submissions",
                newName: "Year");

            migrationBuilder.RenameColumn(
                name: "TotalClientsInDatabase",
                table: "Submissions",
                newName: "TotalTakings");

            migrationBuilder.RenameColumn(
                name: "TotalClientVisitsYear",
                table: "Submissions",
                newName: "MonthNumber");

            migrationBuilder.AddColumn<string>(
                name: "DocumentUrl",
                table: "Submissions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
