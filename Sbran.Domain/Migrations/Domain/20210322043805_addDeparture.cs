using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sbran.Domain.Migrations.Domain
{
    public partial class addDeparture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departures",
                schema: "domain",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    SendingCountry = table.Column<string>(type: "text", nullable: true),
                    CityOfBusiness = table.Column<string>(type: "text", nullable: true),
                    SourceOfFinancing = table.Column<string>(type: "text", nullable: true),
                    BasicOfDeparture = table.Column<string>(type: "text", nullable: true),
                    HostOrganization = table.Column<string>(type: "text", nullable: true),
                    PlaceOfResidence = table.Column<string>(type: "text", nullable: true),
                    PurposeOfTheTrip = table.Column<string>(type: "text", nullable: true),
                    JustificationOfTheBusiness = table.Column<string>(type: "text", nullable: true),
                    DateStart = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DateEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EmployeeUid = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departures", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_Departures_Employees_EmployeeId1",
                        column: x => x.EmployeeId1,
                        principalSchema: "domain",
                        principalTable: "Employees",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Departures_Employees_EmployeeUid",
                        column: x => x.EmployeeUid,
                        principalSchema: "domain",
                        principalTable: "Employees",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departures_EmployeeId1",
                schema: "domain",
                table: "Departures",
                column: "EmployeeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Departures_EmployeeUid",
                schema: "domain",
                table: "Departures",
                column: "EmployeeUid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departures",
                schema: "domain");
        }
    }
}
