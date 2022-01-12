using Microsoft.EntityFrameworkCore.Migrations;

namespace isaloncoach10.Data.Migrations
{
    public partial class update5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                table: "Target");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "Target",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
