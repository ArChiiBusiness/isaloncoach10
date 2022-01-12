using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace isaloncoach10.Data.Migrations
{
    public partial class update4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Response");

            migrationBuilder.DropColumn(
                name: "TargetMonthUSD",
                table: "Salon");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Salon",
                newName: "SalonId");

            migrationBuilder.CreateTable(
                name: "Actual",
                columns: table => new
                {
                    ActualId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    TotalTakings = table.Column<double>(type: "float", nullable: false),
                    RetailMonth = table.Column<double>(type: "float", nullable: false),
                    WageBillMonth = table.Column<double>(type: "float", nullable: false),
                    ClientVisitsMonth = table.Column<double>(type: "float", nullable: false),
                    RebooksMonth = table.Column<double>(type: "float", nullable: false),
                    ClientVisitsLastYear = table.Column<double>(type: "float", nullable: false),
                    IndividualClientVisitsLastYear = table.Column<double>(type: "float", nullable: false),
                    NewClientsMonth = table.Column<double>(type: "float", nullable: false),
                    TotalClientsInDatabase = table.Column<double>(type: "float", nullable: false),
                    SalonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actual", x => x.ActualId);
                    table.ForeignKey(
                        name: "FK_Actual_Salon_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salon",
                        principalColumn: "SalonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Target",
                columns: table => new
                {
                    TargetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    TotalTakings = table.Column<double>(type: "float", nullable: false),
                    RetailMonth = table.Column<double>(type: "float", nullable: false),
                    WageBillMonth = table.Column<double>(type: "float", nullable: false),
                    ClientVisitsMonth = table.Column<double>(type: "float", nullable: false),
                    RebooksMonth = table.Column<double>(type: "float", nullable: false),
                    ClientVisitsLastYear = table.Column<double>(type: "float", nullable: false),
                    IndividualClientVisitsLastYear = table.Column<double>(type: "float", nullable: false),
                    NewClientsMonth = table.Column<double>(type: "float", nullable: false),
                    TotalClientsInDatabase = table.Column<double>(type: "float", nullable: false),
                    SalonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Target", x => x.TargetId);
                    table.ForeignKey(
                        name: "FK_Target_Salon_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salon",
                        principalColumn: "SalonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actual_SalonId",
                table: "Actual",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_Target_SalonId",
                table: "Target",
                column: "SalonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actual");

            migrationBuilder.DropTable(
                name: "Target");

            migrationBuilder.RenameColumn(
                name: "SalonId",
                table: "Salon",
                newName: "Id");

            migrationBuilder.AddColumn<double>(
                name: "TargetMonthUSD",
                table: "Salon",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Response",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientVisitsMonth = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewClientsMonth = table.Column<int>(type: "int", nullable: false),
                    PastYearTotalTakings = table.Column<double>(type: "float", nullable: false),
                    RebooksMonth = table.Column<double>(type: "float", nullable: false),
                    RetailMonthUSD = table.Column<double>(type: "float", nullable: false),
                    SalonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetClientsMonth = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalClientVisitsYear = table.Column<int>(type: "int", nullable: false),
                    TotalClientsInDatabase = table.Column<int>(type: "int", nullable: false),
                    TotalIndividualClientVisitsYear = table.Column<int>(type: "int", nullable: false),
                    TotalMonthlyTakings = table.Column<double>(type: "float", nullable: false),
                    WageBillMonthUSD = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Response", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Response_Salon_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Response_SalonId",
                table: "Response",
                column: "SalonId");
        }
    }
}
