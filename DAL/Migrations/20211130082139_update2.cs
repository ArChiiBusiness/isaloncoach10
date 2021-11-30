﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace isaloncoach10.Data.Migrations
{
    public partial class update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.CreateTable(
                name: "Responses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalMonthlyTakings = table.Column<double>(type: "float", nullable: false),
                    RetailMonthUSD = table.Column<double>(type: "float", nullable: false),
                    WageBillMonthUSD = table.Column<double>(type: "float", nullable: false),
                    ClientVisitsMonth = table.Column<int>(type: "int", nullable: false),
                    TargetMonthUSD = table.Column<double>(type: "float", nullable: false),
                    TargetClientsMonth = table.Column<int>(type: "int", nullable: false),
                    RebooksMonth = table.Column<double>(type: "float", nullable: false),
                    TotalClientVisitsYear = table.Column<int>(type: "int", nullable: false),
                    TotalIndividualClientVisitsYear = table.Column<int>(type: "int", nullable: false),
                    NewClientsMonth = table.Column<int>(type: "int", nullable: false),
                    PastYearTotalTakings = table.Column<double>(type: "float", nullable: false),
                    TotalClientsInDatabase = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responses", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Responses");

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientVisitsMonth = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewClientsMonth = table.Column<int>(type: "int", nullable: false),
                    PastYearTotalTakings = table.Column<double>(type: "float", nullable: false),
                    RebooksMonth = table.Column<double>(type: "float", nullable: false),
                    RetailMonthUSD = table.Column<double>(type: "float", nullable: false),
                    SalonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetClientsMonth = table.Column<int>(type: "int", nullable: false),
                    TargetMonthUSD = table.Column<double>(type: "float", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalClientVisitsYear = table.Column<int>(type: "int", nullable: false),
                    TotalClientsInDatabase = table.Column<int>(type: "int", nullable: false),
                    TotalIndividualClientVisitsYear = table.Column<int>(type: "int", nullable: false),
                    TotalMonthlyTakings = table.Column<double>(type: "float", nullable: false),
                    WageBillMonthUSD = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.Id);
                });
        }
    }
}
