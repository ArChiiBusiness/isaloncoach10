using Microsoft.EntityFrameworkCore.Migrations;

namespace isaloncoach10.Data.Migrations
{
    public partial class update6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "WagePercent",
                table: "Target",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WagePercent",
                table: "Target");
        }
    }
}
